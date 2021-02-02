using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text;
using System.Data;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data.OleDb;
using System.Reflection;
using System.Net;
using Microsoft.Win32;

namespace orGenta
{
    public partial class ViewForm : System.Windows.Forms.Form
    {
        #region Filtering Done Items

        private void menuShowDone_Click(object sender, System.EventArgs e)
        {
            if (this.menuShowDone.Checked)
            {
                // Switch to the view hiding Done items
                this.tBarQuickBtns.Buttons[14].ToolTipText = orGentaResources.ShowDone;
                this.tBarQuickBtns.Buttons[14].ImageIndex = 8;
                TreeNode tnDisposer = FullTreeView.Nodes[0];
                tnDisposer = RebuildTreeFromDone(tnDisposer);
                this.CategoryHierTV.BeginUpdate();
                tnDisposer = ResetTopNode(tnDisposer);
                this.menuShowDone.Checked = false;
                ShowDone = false;
                this.CategoryHierTV.Refresh();
            }
            else
            {
                // Switch to the view showing Done items
                this.CategoryHierTV.BeginUpdate();
                this.tBarQuickBtns.Buttons[14].ToolTipText = orGentaResources.HideDone;
                this.tBarQuickBtns.Buttons[14].ImageIndex = 9;
                TreeNode tnDisposer = this.CategoryHierTV.Nodes[0];
                this.CategoryHierTV.Nodes[0].Remove();
                tnDisposer = null;
                this.CategoryHierTV.Nodes.Add((TreeNode)FullTreeView.Nodes[0].Clone());
                ReplayDoneTVupdates();
                tnDisposer = FullTreeView.Nodes[0];
                tnDisposer = RebuildTreeFromDone(tnDisposer);
                this.CategoryHierTV.EndUpdate();
                this.CategoryHierTV.CollapseAll();
                this.catInvisLabel.Visible = false;
                this.CategoryHierTV.Nodes[0].Expand();
                this.menuShowDone.Checked = true;
                ShowDone = true;
                this.CategoryHierTV.Refresh();
            }

            if (!endRoutinesRunning)
            {
                DelayedBuildGrid = true;
                this.ViewFormTimer.Enabled = true;
            }
        }

        #endregion

        #region Menu db Cleanup and Synchronization

        private void menuCleanup_Click(object sender, System.EventArgs e)
        {
            string WarnMsg = orGentaResources.CleanupWarning.Replace(";", Environment.NewLine);
            DialogResult UserDeleteReply;
            UserDeleteReply = MessageBox.Show(this, WarnMsg, "Cleanup Utility",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button2);
            if (UserDeleteReply != DialogResult.OK)
                { return; }

            // Save everything first
            endRoutines();
            endRoutinesRun = false;
            DelayedBuildGrid = false;
            this.Cursor = Cursors.WaitCursor;
            File.Copy(CatHierSaveFileName, CatHierSaveFileName + ".rescue", true);
            File.Copy(this.oleorGentaDbConx.DataSource, this.oleorGentaDbConx.DataSource + ".rescue", true);
            this.CategoryHierTV.BeginUpdate();
            this.catInvisLabel.Visible = false;
            this.CategoryHierTV.ExpandAll();
            this.CategoryHierTV.SelectedNode = this.CategoryHierTV.Nodes[0];
            CategoryHierTV.SelectedNode.EnsureVisible();

            // Cleanup different item numbers with same text
            string GetMaxCmd = "Select MAX(ItemKey) AS MaxKey, TextItem INTO jeffTemp From Items Group By TextItem HAVING COUNT(*) > 1";
            oleorGentaDbConx.Open();
            OleDbCommand SaveMaxDupes = new OleDbCommand(GetMaxCmd, this.oleorGentaDbConx);
            int HowManyDupes = 0;
            try
                { HowManyDupes = SaveMaxDupes.ExecuteNonQuery(); }
            catch { }
            if (HowManyDupes > 0)
                {RemoveDuplicateItems();}

            UnlinkedAndDupeNodes();
            string DropDupeTabCmd = "DROP TABLE jeffTemp";
            OleDbCommand DropDupeTab = new OleDbCommand(DropDupeTabCmd, this.oleorGentaDbConx);
            try
                { int DropResult = DropDupeTab.ExecuteNonQuery(); }
            catch { }

            // Add Items not in tView
            TreeNode UnassignedParent = FindNodeInTV(UnassignedNode, "", false, "");
            foreach (DataRow dBitemRetrieved in this.ItemsMemImage.Tables[0].Rows)
                {AddUnassignedLostItems(UnassignedParent, dBitemRetrieved);}
            DropDupeTab = null;
            SaveMaxDupes = null;
            oleorGentaDbConx.Close();

            // Re-establish user interface display
            this.CategoryHierTV.SelectedNode = this.CategoryHierTV.Nodes[0];
            this.CategoryHierTV.SelectedNode.EnsureVisible();
            this.CategoryHierTV.EndUpdate();
            BuildGrid();
            DelayedBuildGrid = false;
            this.ViewFormTimer.Enabled = false;
            this.Cursor = Cursors.Default;
        }

        private void AddUnassignedLostItems(TreeNode UnassignedParent, DataRow dBitemRetrieved)
        {
            string ItemToFind = dBitemRetrieved["ItemKey"].ToString();
            int FoundItem = ItemIDs.IndexOf(ItemToFind);
            if (FoundItem < 0)
            {
                TreeNode newAddCatNode = new TreeNode("-");
                newAddCatNode.Tag = "i-" + ItemToFind;
                UnassignedParent.Nodes.Add(newAddCatNode);
                Console.WriteLine("i-{0} Item added to Unassigned", ItemToFind.ToString());
            }
        }

        private void RemoveDuplicateItems()
        {
            string DelDupesCmd = "DELETE Items.* FROM Items, jeffTemp WHERE (Items.ItemKey <> jeffTemp.MaxKey) AND Items.TextItem = jeffTemp.TextItem";
            OleDbCommand DeleteDupes = new OleDbCommand(DelDupesCmd, this.oleorGentaDbConx);
            try
            {
                int HowManyRemoved = DeleteDupes.ExecuteNonQuery();
                Console.WriteLine("{0} duplicate Items removed", HowManyRemoved.ToString());
                // TODO: remove orphaned notes?
            }
            catch { }
            this.ItemsMemImage.Clear();
            this.ItemsDataAdapter.Fill(this.ItemsMemImage);
        }

        private void UnlinkedAndDupeNodes()
        {
            bool HasMoreNodes = true;
            tvSelectionWaterfall = true;
            ArrayList GoodNodes = new ArrayList();
            ItemIDs.Clear();
            while (HasMoreNodes)
            {
                CategoryHierTV.SelectedNode = CategoryHierTV.SelectedNode.NextVisibleNode;
                if (CategoryHierTV.SelectedNode == null)
                { HasMoreNodes = false; }
                else if (CategoryHierTV.SelectedNode.Text == "-")
                {
                    string dBitemNum = CategoryHierTV.SelectedNode.Tag.ToString().Substring(2);
                    ItemIDs.Add(dBitemNum);
                    DataRow[] dBitemRetrievedSet = this.ItemsMemImage.Tables[0].Select("ItemKey = " + dBitemNum);
                    if (dBitemRetrievedSet.Length == 0)
                    {
                        // Didn't find it, delete node
                        Console.WriteLine("Dropping treenode {0} with unmatched item {1}",
                            CategoryHierTV.SelectedNode.FullPath, dBitemNum.ToString());
                        TreeNode PreviousNode = CategoryHierTV.SelectedNode.PrevVisibleNode;
                        CategoryHierTV.SelectedNode.Remove();
                        CategoryHierTV.SelectedNode = PreviousNode;
                    }
                    else
                    {
                        // See if item is a duplicate within parent node
                        string KeyPlusPath = dBitemNum + CategoryHierTV.SelectedNode.FullPath;
                        if (GoodNodes.IndexOf(KeyPlusPath) >= 0)
                        {
                            Console.WriteLine("Dropping treenode {0} with duplicate item {1}",
                                CategoryHierTV.SelectedNode.FullPath, dBitemNum.ToString());
                            TreeNode PreviousNode = CategoryHierTV.SelectedNode.PrevVisibleNode;
                            CategoryHierTV.SelectedNode.Remove();
                            CategoryHierTV.SelectedNode = PreviousNode;
                        }
                        else
                        { GoodNodes.Add(KeyPlusPath); }
                    }
                }
            }
        }
        #endregion

        #region Other Menu Selection Events
        private void menuItemExit_Click(object sender, System.EventArgs e)
        {
            endRoutines();
            this.Close();
            Application.Exit();
        }

        private void menuItemAbout_Click(object sender, System.EventArgs e)
        {
            using (About AboutBox = new About())
            {
                AboutBox.txtVersionNumber.Text = "Version " +
                    softwareVersion.Major.ToString() + "." + softwareVersion.Minor.ToString()
                    + "." + softwareVersion.Build.ToString();
                AboutBox.txtBuildNumber.Text = "Build " + softwareVersion.Revision.ToString();
                AboutBox.txtDotNetVsn.Text = "Runtime " + Environment.Version.ToString();
                AboutBox.ShowDialog(this);
            }
        }

        private void menuExpandAll_Click(object sender, System.EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            this.CategoryHierTV.BeginUpdate();
            this.catInvisLabel.Visible = false;
            this.CategoryHierTV.ExpandAll();
            CollapseFrozenNodes(this.CategoryHierTV.Nodes[0]);
            try
            { this.CategoryHierTV.SelectedNode.EnsureVisible(); }
            catch { }
            this.CategoryHierTV.EndUpdate();
            DelayedBuildGrid = true;
            this.ViewFormTimer.Enabled = true;
        }

        private void menuCollapseAll_Click(object sender, System.EventArgs e)
        {
            collapsingFlag = true;
            this.CategoryHierTV.CollapseAll();
            this.catInvisLabel.Visible = false;
            //	Expand the Main node though
            this.CategoryHierTV.Nodes[0].Expand();
            this.CategoryHierTV.SelectedNode = this.CategoryHierTV.GetNodeAt(1, 1);
            collapsingFlag = false;
            SaveEditedRow = 0;
            SaveEditedCol = 2;
            DelayedBuildGrid = true;
            this.ViewFormTimer.Enabled = true;
        }

        private void menuRegistration_Click(object sender, System.EventArgs e)
        {
            RegisterEntry GetRegistration = new RegisterEntry();
            if (GetRegistration.ShowDialog(this) == DialogResult.OK)
            {
                SoftwareIsRegistered = false;
                string RegistrationResult = "";
                string RegistrationEntered = "Shareware Key " + GetRegistration.txtKeyEntered.Text;
                string softwareIDretrieved = GetSoftwareKey();
                if (orGregistered(softwareIDretrieved, RegistrationEntered))
                {
                    oleorGentaDbConx.Open();
                    string AddSysKey = "INSERT INTO Items(Recurrence, TextItem) VALUES ('syskey', '" + RegistrationEntered + "')";
                    OleDbCommand KeyCmdBuilder = new OleDbCommand(AddSysKey, this.oleorGentaDbConx);
                    try
                    {
                        KeyCmdBuilder.ExecuteNonQuery();
                        nextItemkey[0]++;
                        SoftwareIsRegistered = true;
                        this.menuRegistration.Enabled = false;
                        RegistrationResult = orGentaResources.ThanksRegistered;
                    }
                    catch (Exception ex)
                    { RegistrationResult = orGentaResources.RegistrationFailure + ex.ToString(); }
                    oleorGentaDbConx.Close();
                }
                else
                { RegistrationResult = orGentaResources.BadRegistration; }
                MessageBox.Show(this, RegistrationResult);
            }
            GetRegistration.Dispose();
        }

        private void menuCalendar_Click(object sender, System.EventArgs e)
        {
            using (CalendarForm TaskCalendar = new CalendarForm(ref ItemsMemImage, ref nextItemkey))
            { TaskCalendar.Show(); }
        }

        private void menuExpandNode_Click(object sender, System.EventArgs e)
        {
            this.CategoryHierTV.BeginUpdate();
            this.CategoryHierTV.SelectedNode.ExpandAll();
            if (this.CategoryHierTV.SelectedNode.ForeColor != FrozenColor)
            { CollapseFrozenNodes(this.CategoryHierTV.SelectedNode); }
            this.CategoryHierTV.EndUpdate();
        }

        private void menuExpandOnlyThis_Click(object sender, System.EventArgs e)
        {
            TreeNode SaveMyNode;
            try
            { SaveMyNode = this.CategoryHierTV.SelectedNode; }
            catch
            { return; }
            menuCollapseAll_Click(sender, null);
            this.CategoryHierTV.SelectedNode = SaveMyNode;
            SaveMyNode.EnsureVisible();
            SaveMyNode.Expand();
        }

        private void menuCollapseNode_Click(object sender, System.EventArgs e)
        {
            CollapseWaterfall = true;
            collapsingFlag = true;
            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            CollapseNodeCascade(this.CategoryHierTV.SelectedNode);
            collapsingFlag = false;
            CollapseWaterfall = false;
            AdjustCatInvisLabel();
            BuildGrid();
            this.Cursor = Cursors.Default;
        }

        private void CollapseNodeCascade(TreeNode NodeToCollapse)
        {
            foreach (TreeNode ChildToCollaspe in NodeToCollapse.Nodes)
            { CollapseNodeCascade(ChildToCollaspe); }
            NodeToCollapse.Collapse();
        }

        private void tBarQuickBtns_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
        {
            switch (this.tBarQuickBtns.Buttons.IndexOf(e.Button))
            {
                case 1:
                    menuPrint_Click(this.tBarQuickBtns, null);
                    break;
                case 2:
                    menuItemPrint_Click(this.tBarQuickBtns, null);
                    break;
                case 4:
                    menuTrayed_Click(this.tBarQuickBtns, null);
                    break;
                case 6:
                    menuItemFindCategory_Click(this.tBarQuickBtns, null);
                    break;
                case 7:
                    menuItemFindItem_Click(this.tBarQuickBtns, null);
                    break;
                case 8:
                    menuItemFindNote_Click(this.tBarQuickBtns, null);
                    break;
                case 9:
                    menuItemFindNext_Click(this.tBarQuickBtns, null);
                    break;
                case 10:
                    menuItemOccursAgain_Click(this.tBarQuickBtns, null);
                    break;
                case 12:
                    menuAssignTo_Click(this.tBarQuickBtns, null);
                    break;
                case 14:
                    menuShowDone_Click(this.tBarQuickBtns, null);
                    break;
                case 16:
                    menuExpandNode_Click(this.tBarQuickBtns, null);
                    break;
                case 17:
                    menuExpandOnlyThis_Click(this.tBarQuickBtns, null);
                    break;
                case 18:
                    menuCollapseNode_Click(this.tBarQuickBtns, null);
                    break;
                case 20:
                    menuCalendar_Click(this.tBarQuickBtns, null);
                    break;
                case 22:
                    menuSortCats_Click(this.tBarQuickBtns, null);
                    break;
                case 23:
                    menuSortItems_Click(this.tBarQuickBtns, null);
                    break;
            }
        }

        private void menuDropCategories_Click(object sender, System.EventArgs e)
        {
            string WarnMsg = orGentaResources.DropCatWarning.Replace(";", Environment.NewLine);
            DialogResult UserDeleteReply;
            UserDeleteReply = MessageBox.Show(this, WarnMsg, "Delete Categories",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button2);
            if (UserDeleteReply != DialogResult.OK)
            { return; }

            // save everything first
            endRoutines();
            endRoutinesRun = false;
            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            File.Move(CatHierSaveFileName, CatHierSaveFileName + ".rescue");
            string treeSeedFile = Environment.CurrentDirectory + "\\CategSeed.stv";
            File.Copy(treeSeedFile, CatHierSaveFileName);

            // Now Load the puppy
            this.CategoryHierTV.Nodes.Clear();
            loadTree(this.CategoryHierTV, CatHierSaveFileName);
            this.CategoryHierTV.Nodes[0].Expand();
            this.CategoryHierTV.SelectedNode = this.CategoryHierTV.GetNodeAt(1, 1);
            PrevVisibleTopTVnode = this.CategoryHierTV.TopNode;
            // Add all dB items to Unassigned
            TreeNode UnassignedParent = FindNodeInTV(UnassignedNode, "", false, "");
            foreach (DataRow dBitemRetrieved in this.ItemsMemImage.Tables[0].Rows)
            {
                int KeyToMatch = Convert.ToInt16(dBitemRetrieved["ItemKey"]);
                TreeNode newAddCatNode = new TreeNode("-");
                newAddCatNode.Tag = "i-" + KeyToMatch.ToString();
                UnassignedParent.Nodes.Add(newAddCatNode);
            }
            FullTreeView.Nodes.Clear();
            DoneTreeView.Nodes.Clear();
            RebuildMirrorTreeViews();
            BuildGrid();
            this.Cursor = Cursors.Default;
        }

        private void menuResetWidths_Click(object sender, System.EventArgs e)
        {
            SavedColWidths = DefaultColWidths;
            setGridColWidths();
        }

        private void menuTrayed_Click(object sender, System.EventArgs e)
        {
            string TitleHolder = "orGentax";
            this.trayIconTrayed.Text = TitleHolder.Substring(0, 7);
            this.trayIconTrayed.Visible = true;
            this.Visible = false;
            RunningMinimal = true;
        }

        private void menuZoomItem_Click(object sender, System.EventArgs e)
        {
            using (ZoomedItem ThisItemZoom = new ZoomedItem())
            {
                int ActiveRow = this.ItemGrid.CurrentCell.RowNumber;
                ThisItemZoom.txtZoomBox.Text = this.ItemGrid[ActiveRow, 2].ToString();
                ThisItemZoom.txtZoomBox.SelectionStart = ThisItemZoom.txtZoomBox.Text.Length;
                ThisItemZoom.txtZoomBox.ReadOnly = true;
                ThisItemZoom.txtZoomBox.BackColor = Color.White;
                ThisItemZoom.Show();
            }
        }

        private void ctxDelete_Click(object sender, System.EventArgs e)
        {
            this.ItemGrid.Select(this.ItemGrid.CurrentCell.RowNumber);
            SendKeys.Send("{DELETE}");
        }

        private void ctxMenuTV_Popup(object sender, System.EventArgs e)
        {
            if (this.CategoryHierTV.SelectedNode.ForeColor == FrozenColor)
            { this.ctxMenuTV.MenuItems[3].Text = orGentaResources.OpenUnfrozen; }
            else
            { this.ctxMenuTV.MenuItems[3].Text = orGentaResources.FreezeClosed; }
        }

        private void ctxFreezeNode_Click(object sender, System.EventArgs e)
        {
            if (this.ctxFreezeNode.Text == "Open Unfrozen")
            { CategoryHierTV.SelectedNode.ForeColor = Color.FromName("WindowText"); }
            else
            { CategoryHierTV.SelectedNode.ForeColor = FrozenColor; }
            if (!ShowDone)
            {
                AddToDoneReplay("Color", CategoryHierTV.SelectedNode,
                CategoryHierTV.SelectedNode.FullPath, CategoryHierTV.SelectedNode, null);
            }
        }

        private void CollapseFrozenNodes(TreeNode StartingNode)
        {
            CollapseWaterfall = true;
            collapsingFlag = true;
            foreach (TreeNode CollapseNode in StartingNode.Nodes)
            { FreezeCollapseCascade(CollapseNode); }
            collapsingFlag = false;
            CollapseWaterfall = false;
        }

        private void FreezeCollapseCascade(TreeNode CollapseNode)
        {
            if (CollapseNode.ForeColor == FrozenColor)
            { CollapseNode.Collapse(); }
            else
            {
                foreach (TreeNode ChildCollapseNode in CollapseNode.Nodes)
                { FreezeCollapseCascade(ChildCollapseNode); }
            }
        }

        private void menuColorReset_Click(object sender, System.EventArgs e)
        {
            foreach (TreeNode ColorCheckNode in this.CategoryHierTV.Nodes[0].Nodes)
            { CheckNodeColorFor(ColorCheckNode); }
        }

        private void CheckNodeColorFor(TreeNode ColorCheckNode)
        {
            Color thisNodeColor = ColorCheckNode.ForeColor;
            if ((thisNodeColor != FrozenColor) && (thisNodeColor != Color.LightSlateGray))
            { ColorCheckNode.ForeColor = Color.FromName("WindowText"); }
            foreach (TreeNode KidColorCheckNode in ColorCheckNode.Nodes)
            { CheckNodeColorFor(KidColorCheckNode); }
        }

        #endregion

        #region Menu Events for Finding Items, Notes, and Categories
        private void menuItemFindCategory_Click(object sender, System.EventArgs e)
        {
            this.pnlCatFind.Visible = true;
            this.txtCatSearch.Text = "";
            this.txtCatSearch.Focus();
        }

        private void txtCatSearch_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string CatToFind = this.txtCatSearch.Text;
            this.pnlCatFind.Visible = false;
            if (CatToFind == "")
            { return; }
            TreeNode FoundNode;
            if (findReplaying)
            {
                FoundNode = FindNodeInTV(CatToFind, "", true,
                this.CategoryHierTV.SelectedNode.FullPath);
            }
            else
            { FoundNode = FindNodeInTV(CatToFind, "", true, ""); }

            lastExecutedSearch = "Category";
            FoundNode.EnsureVisible();
            this.catInvisLabel.Visible = false;
            if ((FoundNode.Text == "Main") && (!RunningMinimal))
            { MessageBox.Show(this, orGentaResources.NotFound); }
            SaveEditedRow = -1;
            this.CategoryHierTV.SelectedNode = FoundNode;
            this.CategoryHierTV.Focus();
        }

        private void txtCatSearch_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)27)
            { this.txtCatSearch.Text = ""; }
            HandleEnterAndEscape(e);
        }

        private void menuItemFindItem_Click(object sender, System.EventArgs e)
        {
            this.pnlItemFind.Visible = true;
            this.txtItemSearch.Text = "";
            this.txtItemSearch.Focus();
        }

        private void txtItemSearch_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)27)
            { this.txtItemSearch.Text = ""; }
            HandleEnterAndEscape(e);
        }

        private void txtItemSearch_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string ItemToFind = this.txtItemSearch.Text;
            this.pnlItemFind.Visible = false;
            if (ItemToFind == "")
            {
                PreviousMatchedItem = "";
                return;
            }

            // change embedded single quotes for SQL
            ItemToFind = ItemToFind.Replace("'", "''");
            string MatchText = "TextItem LIKE '*" + ItemToFind + "*'";
            if (!ShowDone)
                { MatchText = MatchText + " AND (Done = false)"; }
            DataRow[] dBitemRetrievedSet = this.ItemsMemImage.Tables[0].Select(MatchText);
            if (dBitemRetrievedSet.Length == 0)
            {
                this.txtItemSearch.Text = "";
                PreviousMatchedItem = "";
                if (!RunningMinimal)
                    { MessageBox.Show(this, orGentaResources.NotFound); }
                return;
            }
            SaveEditedRow = -1;
            //	if in Replay (F3) mode, we need to repeat search past last match
            string dBitemNum = "";
            string failedMatchItem = "";
            if (findReplaying)
            {
                DataGridColumnStyle dgCol;
                dgCol = this.ItemGrid.TableStyles[0].GridColumnStyles[2];
                int gridCurrentRow = this.ItemGrid.CurrentCell.RowNumber;
                this.ItemGrid.EndEdit(dgCol, gridCurrentRow, false);
                try
                    {LocatePrevMatch(dBitemRetrievedSet, ref dBitemNum, ref failedMatchItem);}
                catch
                {
                    dBitemNum = "i-" + dBitemRetrievedSet[0].ItemArray[0].ToString();
                    PreviousMatchedItem = dBitemNum;
                }
            }
            else
            {
                // just a straight match without a replay
                dBitemNum = "i-" + dBitemRetrievedSet[0].ItemArray[0].ToString();
                PreviousMatchedItem = dBitemNum;
            }
            bool FoundItem = LookForItem(this.CategoryHierTV.Nodes[0].Nodes, dBitemNum,
                false, true, false);

            // Add to Unassigned if we item is in memory dB but not in the TreeView
            if (!FoundItem)
            {
                TreeNode newAddCatNode = new TreeNode("-");
                newAddCatNode.Tag = dBitemNum;
                TreeNode UnassignedParent = FindNodeInTV(UnassignedNode, "", false, "");
                UnassignedParent.Nodes.Add(newAddCatNode);
                this.CategoryHierTV.SelectedNode = newAddCatNode;
                if (!ShowDone)
                {
                    AddToDoneReplay("Add", (TreeNode)newAddCatNode.Clone(),
                    UnassignedParent.FullPath, UnassignedParent, null);
                }
                BuildGrid();
            }
            lastExecutedSearch = "Item";

            // set focus and trigger editing of item
            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            this.CategoryHierTV.SelectedNode.EnsureVisible();
            this.ItemGrid.Focus();
            this.Cursor = Cursors.Default;
            EditItemMatchingCategory = true;
            this.ViewFormTimer.Enabled = true;
        }

        private void LocatePrevMatch(DataRow[] dBitemRetrievedSet, ref string dBitemNum, ref string failedMatchItem)
        {
            //	walk up to previous matched item
            for (int i = 0; i < dBitemRetrievedSet.Length; i++)
            {
                dBitemNum = "i-" + dBitemRetrievedSet[i].ItemArray[0].ToString();
                if ((dBitemNum == PreviousMatchedItem) && (i < dBitemRetrievedSet.Length - 1))
                {
                    dBitemNum = "i-" + dBitemRetrievedSet[i + 1].ItemArray[0].ToString();
                    failedMatchItem = dBitemRetrievedSet[i + 1].ItemArray[2].ToString();
                    break;
                }
            }
            if (dBitemNum == PreviousMatchedItem)
            {
                //	we walked through them all, no more to be found
                MessageBox.Show(this, orGentaResources.NotFound);
                this.txtItemSearch.Text = "";
                PreviousMatchedItem = "";
            }
            else
                { PreviousMatchedItem = dBitemNum; }
        }

        private void menuItemFindNote_Click(object sender, System.EventArgs e)
        {
            this.pnlNoteFind.Visible = true;
            this.txtNoteSearch.Text = "";
            this.txtNoteSearch.Focus();
        }

        private void txtNoteSearch_KeyPress(object sender,
            System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)27)
            { this.txtNoteSearch.Text = ""; }
            HandleEnterAndEscape(e);
        }

        private void txtNoteSearch_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string NoteToFind = this.txtNoteSearch.Text;
            this.pnlNoteFind.Visible = false;
            if (NoteToFind == "")
            { return; }
            string noteTextToSearch = "'%" + NoteToFind + "%'";
            string GetItemCmd = "SELECT Notes.ItemKey FROM Items,Notes WHERE NoteText LIKE " + noteTextToSearch + " AND (Items.ItemKey=Notes.ItemKey)";
            oleorGentaDbConx.Open();
            OleDbCommand NotesCmdBuilder = new OleDbCommand(GetItemCmd, this.oleorGentaDbConx);
            string ItemWithNote = "";
            OleDbDataReader NoteReader = NotesCmdBuilder.ExecuteReader(CommandBehavior.CloseConnection);
            NotesCmdBuilder.Dispose();
            //	if in Replay (F3) mode, we need to repeat search past last match
            if (findReplaying)
            {
                string PrevMatchedNoteItem = this.CategoryHierTV.SelectedNode.Tag.ToString().Substring(2);
                while (NoteReader.Read())
                {
                    if (PrevMatchedNoteItem == NoteReader.GetInt32(0).ToString())
                    {
                        if (NoteReader.Read())
                        {
                            ItemWithNote = NoteReader.GetInt32(0).ToString();
                            break;
                        }
                    }
                }
            }
            else
            {
                if (NoteReader.Read())
                { ItemWithNote = NoteReader.GetInt32(0).ToString(); }
            }
            NoteReader.Close();

            if (ItemWithNote == "")
            {
                //	Nothing returned or walked past last match
                this.txtNoteSearch.Text = "";
                if (!RunningMinimal)
                { MessageBox.Show(this, orGentaResources.NotFound); }
                return;
            }
            SaveEditedRow = -1;

            // Select the item's CurrentCell
            string dBitemNum = "i-" + ItemWithNote.ToString();
            LookForItem(this.CategoryHierTV.Nodes[0].Nodes, dBitemNum, false, true, false);
            this.ItemGrid.Focus();
            this.CategoryHierTV.SelectedNode.EnsureVisible();
            lastExecutedSearch = "Note";

            // Set trigger to edit the Note for this item
            EditItemMatchingCategory = true;
            AutoPopNote = true;
            this.ViewFormTimer.Enabled = true;
        }

        private void menuItemFindNext_Click(object sender, System.EventArgs e)
        {
            if ((this.CategoryHierTV.SelectedNode.Text != "-") && (lastExecutedSearch != "Category"))
            { return; }
            findReplaying = true;
            switch (lastExecutedSearch)
            {
                case "Category":
                    txtCatSearch_Validating(new object(), new CancelEventArgs());
                    break;
                case "Item":
                    txtItemSearch_Validating(new object(), new CancelEventArgs());
                    break;
                case "Note":
                    txtNoteSearch_Validating(new object(), new CancelEventArgs());
                    break;
            }
            findReplaying = false;
        }

        private void menuItemOccursAgain_Click(object sender, System.EventArgs e)
        {
            int CurrentRow = this.ItemGrid.CurrentCell.RowNumber;
            if (RowXrefArray[CurrentRow].ToString().IndexOf("-") != RowXrefArray[CurrentRow].ToString().Length - 1)
            {
                MessageBox.Show(this, orGentaResources.errSelectItemsV2);
                return;
            }

            // Initialize Array to save matches as necessary
            string LookingFor = this.CategoryHierTV.SelectedNode.Tag.ToString();
            if (LookingFor != prevLookForOccursAgain)
            {
                NextOccurMatches.Clear();
                prevLookForOccursAgain = LookingFor;
            }
            string CurrentNode = this.CategoryHierTV.SelectedNode.FullPath;
            NextOccurMatches.Add(CurrentNode);
            bool FoundNextMatch = LookForItem(this.CategoryHierTV.Nodes, LookingFor,
                false, true, true);
            if (FoundNextMatch)
            {
                DupeRelocate = true;
                this.ViewFormTimer.Enabled = true;
            }
            else
            {
                MessageBox.Show(this, orGentaResources.NoMoreRepeats);
                prevLookForOccursAgain = "";
            }
        }
        #endregion

        #region Manually Assigning Items to Categories
        private void menuAssignTo_Click(object sender, System.EventArgs e)
        {
            this.ItemGrid.Select(this.ItemGrid.CurrentCell.RowNumber);
            ArrayList AssignedItems = new ArrayList();
            bool gridHasItemsSelected = false;
            TreeNode NodeToAssign = CategoryHierTV.Nodes[0];
            for (int i = 0; i < RowXrefArray.Count; i++)
            {
                NodeToAssign = NodeToAssign.NextVisibleNode;
                if ((this.ItemGrid.IsSelected(i)) && (RowXrefArray[i].ToString().IndexOf("-") == RowXrefArray[i].ToString().Length - 1))
                {
                    gridHasItemsSelected = true;
                    AssignedItems.Add(NodeToAssign.Tag.ToString());
                }
            }
            if (!gridHasItemsSelected)
            {
                MessageBox.Show(this, orGentaResources.errSelectItems);
                return;
            }
            CatAssignForm GetCatsFrom = new CatAssignForm();
            foreach (TreeNode scanNode in this.CategoryHierTV.Nodes[0].Nodes)
            { AddToAssignCats(scanNode, GetCatsFrom.chkListAssignedCats); }

            // Show the assignment form
            if (GetCatsFrom.ShowDialog(this) == DialogResult.OK)
            {
                for (int i = 0; i < AssignedItems.Count; i++)
                { AddCatsForItem(AssignedItems[i].ToString(), GetCatsFrom); }
                DelayedBuildGrid = true;
                this.ViewFormTimer.Enabled = true;
            }
            GetCatsFrom.Dispose();
        }

        private void AddCatsForItem(string ItemNumber, CatAssignForm GetCatsFrom)
        {
            TreeNode newAddedNode = new TreeNode("-");
            newAddedNode.Tag = ItemNumber;
            int newNodeDBkey = Convert.ToInt16(ItemNumber.Substring(2));
            for (int j = 0; j < GetCatsFrom.chkListAssignedCats.CheckedItems.Count; j++)
            {
                string NodeToFind = GetCatsFrom.chkListAssignedCats.CheckedItems[j].ToString();
                TreeNode ParentNode = FindNodeInTV(NodeToFind, "", false, "");

                // Don't assign the item if it's already there though
                if (!AlreadyAssigned(ParentNode.Nodes, newNodeDBkey))
                {
                    ParentNode.Nodes.Add((TreeNode)newAddedNode.Clone());
                    if (!ShowDone)
                    {
                        AddToDoneReplay("Add", (TreeNode)newAddedNode.Clone(),
                        ParentNode.FullPath, ParentNode, null);
                    }
                }

                // Remove item from trash if it's there
                string ThisItemToFind = newAddedNode.Tag.ToString();
                bool FoundInTrash = LookForItem(FindNodeInTV(TrashCanNode, "", false, "").Nodes, ThisItemToFind, false, true, false);
                if (FoundInTrash)
                {
                    if (!ShowDone)
                    { AddToDoneReplay("Remove", null, CategoryHierTV.SelectedNode.FullPath, CategoryHierTV.SelectedNode, null); }
                    this.CategoryHierTV.SelectedNode.Remove();
                }
            }
        }

        private void AddToAssignCats(TreeNode scanNode, CheckedListBox boxTarget)
        {
            try
            { string scanTag = scanNode.Tag.ToString(); }
            catch
            {
                if (scanNode.Text != "Add Category")
                {
                    boxTarget.Items.Add(scanNode.FullPath);

                    // call myself recursively for my children
                    foreach (TreeNode childNode in scanNode.Nodes)
                    { AddToAssignCats(childNode, boxTarget); }
                }
            }
        }
        #endregion

        #region Notes to Items
        private void menuNotesToItems_Click(object sender, System.EventArgs e)
        {
            NotesToItemPrefs GetNotesToItemPrefs = new NotesToItemPrefs();
            if (GetNotesToItemPrefs.ShowDialog(this) == DialogResult.OK)
            {
                bool PghsToItems = GetNotesToItemPrefs.btnOnePerPgh.Checked;
                bool Use1stSentence = GetNotesToItemPrefs.btnUseFirst.Checked;
                //	write temporary import work file of visible items
                string WorkImportName = Environment.CurrentDirectory + "\\ImportWrk.tmp";
                StreamWriter streamToImport = new StreamWriter(WorkImportName);
                this.Cursor = Cursors.WaitCursor;
                Application.DoEvents();
                DataRowCollection ItemRows = this.iGridDataSource.Tables[0].Rows;
                for (int i = 0; i < RowXrefArray.Count; i++)
                {
                    if (RowXrefArray[i].ToString().IndexOf("-") == RowXrefArray[i].ToString().Length - 1)
                    {
                        if (ItemRows[i]["HasNote"].ToString() == "True")
                        {
                            string ItemInDB = ItemIDs[Convert.ToInt32(ItemRows[i]["ItemKey"]) - 1].ToString();
                            ProcessOneNote(PghsToItems, Use1stSentence, ItemInDB, streamToImport);
                        }
                    }
                }
                streamToImport.Flush();
                streamToImport.Close();
                streamToImport.Dispose();
                ImportItemsFromFile(WorkImportName);
            }
            GetNotesToItemPrefs.Dispose();
            this.Cursor = Cursors.Default;
        }

        private void ProcessOneNote(bool PghsToItems, bool Use1stSentence, string ActiveItem, StreamWriter streamToImport)
        {
            string NoteToParse;
            string GetItemCmd = "SELECT NoteText FROM Notes WHERE ItemKey = " + ActiveItem;
            oleorGentaDbConx.Open();
            using (OleDbCommand NotesCmdBuilder = new OleDbCommand(GetItemCmd, this.oleorGentaDbConx))
            {
                try
                { NoteToParse = (string)NotesCmdBuilder.ExecuteScalar(); }
                catch
                { NoteToParse = null; }
                finally
                { oleorGentaDbConx.Close(); }
            }

            if (NoteToParse == null)
            { return; }

            // Remove any extra spaces
            char[] NoteChars = NoteToParse.ToCharArray();
            char Blinky = Convert.ToChar("\xF0");
            char Spacer = Convert.ToChar(" ");
            char ReturnGuy = Convert.ToChar("\r");
            int i = 0;
            for (int j = 1; j < NoteChars.Length; j++)
            {
                i = NoteChars.Length - j;
                if (NoteChars[i] == ReturnGuy)
                { NoteChars[i] = Blinky; }
                if ((NoteChars[i] == Spacer) || (NoteChars[i] == Blinky))
                {
                    if (NoteChars[i - 1] == Spacer)
                    { NoteChars[i] = Blinky; }
                    if (NoteChars[i - 1].ToString() == "\n")
                    {// tag for delete
                        NoteChars[i] = Blinky;
                    }
                }
            }

            NoteToParse = new String(NoteChars);
            NoteToParse = NoteToParse.Replace(Blinky.ToString(), "");

            // Split into paragraphs and then process each pgh
            string[] NotePghs = NoteToParse.Split(new Char[] { '\n' });
            string WorkingPghHold;
            for (i = 0; i < NotePghs.Length; i++)
            {
                WorkingPghHold = NotePghs[i];
                if (WorkingPghHold != "")
                { ParseAparagraph(WorkingPghHold, PghsToItems, Use1stSentence, streamToImport); }
            }
        }

        private void ParseAparagraph(string WorkingPghHold, bool PghsToItems, bool Use1stSentence, StreamWriter streamToImport)
        {
            string[] NoteSentences = WorkingPghHold.Split(new Char[] { '.' });
            string NewItemText;
            if (PghsToItems)
            {
                //	If paragraph is only 1 sentence then that's all we save
                if (NoteSentences.Length == 1)
                {
                    NewItemText = NoteSentences[0].Trim();
                    if (NewItemText != "")
                    { streamToImport.WriteLine(NewItemText); }
                    return;
                }
                int StartNote = 0;
                int EndNote = NoteSentences.Length - 1;
                if (Use1stSentence)
                {
                    NewItemText = NoteSentences[0].Trim();
                    StartNote = 1;
                }
                else
                {
                    int LastSentNum = NoteSentences.Length - 1;
                    NewItemText = NoteSentences[LastSentNum].Trim();
                    EndNote = EndNote - 1;

                    // handle double line-feeds which causes the last sentence to be blank
                    if (NewItemText == "")
                    {
                        LastSentNum--;
                        NewItemText = NoteSentences[LastSentNum].Trim();
                        EndNote = EndNote - 1;
                    }
                }
                if (NewItemText != "")
                { streamToImport.WriteLine(NewItemText); }

                // Remainder of the Pgh becomes a note to the new item
                int NoteLen = EndNote - StartNote + 1;
                string NoteValue = String.Join(".", NoteSentences, StartNote, NoteLen);
                if (NoteValue.Length > 5)
                { streamToImport.WriteLine("Note: " + NoteValue.Trim()); }
            }
            else
            {
                //	Each sentence becomes an item
                for (int j = 0; j < NoteSentences.Length; j++)
                {
                    NewItemText = NoteSentences[j].Trim();
                    if (NewItemText != "")
                    { streamToImport.WriteLine(NewItemText); }
                }
            }
        }
        #endregion

        #region Importing Items
        private void menuItemImportItems_Click(object sender, System.EventArgs e)
        {
            this.ImportItemsDialog.ShowDialog(this);
            string FileToImport = this.ImportItemsDialog.FileName;
            this.ImportItemsDialog.Dispose();
            if (FileToImport != "")
            { ImportItemsFromFile(FileToImport); }
        }

        private void ImportItemsFromFile(string FileToImport)
        {
            this.Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            bool MidStreamCancel = false;
            //	Switch to view showing Done Items before importing
            if (!ShowDone)
                { menuShowDone_Click(new object(), new System.EventArgs()); }
            this.Cursor = Cursors.WaitCursor;

            object[] RowLoadData = MakeImportDataTable();

            // Read the file first to see how big it is
            StreamReader ImportReader = new StreamReader(FileToImport);
            String ImportedItem;
            int SizeOfImportFile = 0;
            while ((ImportedItem = ImportReader.ReadLine()) != null)
                { SizeOfImportFile++; }
            ImportReader.Close();

            ItemImportStatus ImportStatDisplay = DisplayImportStatus(SizeOfImportFile);

            ImportReader = new StreamReader(FileToImport);
            int ImportReadCount = 0;
            LoadImportToTable(ref MidStreamCancel, RowLoadData, ImportReader, ref ImportedItem, ImportStatDisplay, ref ImportReadCount);

            ImportReader.Close();
            ImportReader.Dispose();

            // Loop through the category tree to assign the new imported items
            ImportStatDisplay.btnCancel.Enabled = false;
            ImportStatDisplay.lblStatusMsg.Text = orGentaResources.Categorizing;
            ImportStatDisplay.Refresh();
            Application.DoEvents();
            if (!MidStreamCancel)
            {
                foreach (TreeNode CheckNode in this.CategoryHierTV.Nodes[0].Nodes)
                    { AssignImports(CheckNode); }
            }

            //	Put items that didn't match any categories into Unassigned
            DataRow[] dBitemRetrievedSet = ImportedItems.Select("HadMatch = false");
            if (dBitemRetrievedSet.Length > 0)
            {
                foreach (DataRow dBitemRetrieved in dBitemRetrievedSet)
                {
                    TreeNode newAddCatNode = BuildaNewTreeNode(dBitemRetrieved);
                    FindNodeInTV(UnassignedNode, "", false, "").Nodes.Add(newAddCatNode);
                }
            }
            ImportedItems.Dispose();

            ImportStatDisplay.lblStatusMsg.Text = orGentaResources.Saving;
            ImportStatDisplay.Refresh();
            Application.DoEvents();
            saveTree(this.CategoryHierTV, CatHierSaveFileName);
            this.ItemsDataAdapter.Update(this.ItemsMemImage);
            FullTreeView.Nodes[0].Remove();
            DoneTreeView.Nodes[0].Remove();
            RebuildMirrorTreeViews();
            ImportStatDisplay.Close();
            ImportStatDisplay.Dispose();
            this.Cursor = Cursors.Default;
        }

        private object[] MakeImportDataTable()
        {
            // Create temporary DataTable to hold imported stuff
            ImportedItems = new DataTable();
            using (DataColumn ImportCol0 = new DataColumn("TextItem"))
            { ImportedItems.Columns.Add(ImportCol0); }
            using (DataColumn ImportCol1 = new DataColumn("HadMatch"))
            {
                ImportCol1.DataType = System.Type.GetType("System.Boolean");
                ImportedItems.Columns.Add(ImportCol1);
            }
            using (DataColumn ImportCol2 = new DataColumn("ItemKey"))
            {
                ImportCol2.DataType = System.Type.GetType("System.Int16");
                ImportedItems.Columns.Add(ImportCol2);
            }
            object[] RowLoadData = new object[3];
            return RowLoadData;
        }

        private ItemImportStatus DisplayImportStatus(int SizeOfImportFile)
        {
            // Pop-up status form and process the import
            ItemImportStatus ImportStatDisplay = new ItemImportStatus();
            ImportStatDisplay.pBarImport.Maximum = SizeOfImportFile;
            ImportStatDisplay.lblStatusMsg.Text = orGentaResources.Importing;
            ImportStatDisplay.Show();
            ImportStatDisplay.Refresh();
            Application.DoEvents();
            this.Cursor = Cursors.WaitCursor;
            return ImportStatDisplay;
        }

        private void LoadImportToTable(ref bool MidStreamCancel, object[] RowLoadData, StreamReader ImportReader, ref String ImportedItem, ItemImportStatus ImportStatDisplay, ref int ImportReadCount)
        {
            while ((ImportedItem = ImportReader.ReadLine()) != null)
            {
                ImportReadCount++;
                ImportStatDisplay.pBarImport.Value = ImportReadCount;
                //	check if we got an interrupt to cancel
                Application.DoEvents();
                if (ImportStatDisplay.CancelFlag)
                {
                    MidStreamCancel = true;
                    break;
                }

                // Process an input item with a note
                if (ImportedItem == "")
                { continue; }
                if ((ImportedItem.Length > 6) && (ImportedItem.Substring(0, 6) == "Note: "))
                {
                    SaveNoteFromImport(ImportedItem);
                    continue;
                }

                // Just a regular Item... go ahead and add it
                AddNewItemToMemDS(ImportedItem, false);
                RowLoadData[0] = ImportedItem.ToString();
                RowLoadData[1] = false;
                RowLoadData[2] = nextItemkey[0] - 1;
                ImportedItems.LoadDataRow(RowLoadData, false);
            }
        }

        private void SaveNoteFromImport(String ImportedItem)
        {
            string NewNoteValue = ImportedItem.Substring(6);
            OleDbCommand NotesCmdBuilder = new OleDbCommand("", this.oleorGentaDbConx);
            string CurrentKey = Convert.ToString(nextItemkey[0] - 1);
            string AddOrChgNoteCmd = "INSERT INTO Notes(ItemKey, NoteText) VALUES ("
                + CurrentKey + ", '" + NewNoteValue.Replace("'", "''") + "')";
            NotesCmdBuilder.CommandText = AddOrChgNoteCmd;
            try
            {
                oleorGentaDbConx.Open();
                NotesCmdBuilder.ExecuteNonQuery();
                //	Set the check box
                DataRow[] dBitemForThisNote = this.ItemsMemImage.Tables[0].Select("ItemKey = " + CurrentKey);
                if (dBitemForThisNote.Length > 0)
                {
                    DataRow dBitemRetrieved = dBitemForThisNote[0];
                    dBitemRetrieved["HasNote"] = true;
                }
            }
            catch { }
            finally
            { oleorGentaDbConx.Close(); }

            NotesCmdBuilder.Dispose();
        }

        private void AssignImports(TreeNode CheckNode)
        {
            if (CheckNode.Text == "-")
            { return; }
            string likeCriteria = "'*" + CheckNode.Text + "*'";
            DataRow[] dBitemRetrievedSet = ImportedItems.Select("TextItem LIKE " + likeCriteria);
            if (dBitemRetrievedSet.Length > 0)
            {
                foreach (DataRow dBitemRetrieved in dBitemRetrievedSet)
                {
                    TreeNode newAddCatNode = BuildaNewTreeNode(dBitemRetrieved);
                    CheckNode.Nodes.Add(newAddCatNode);
                    dBitemRetrieved["HadMatch"] = true;
                }
            }
            foreach (TreeNode CheckChild in CheckNode.Nodes)
            {// Call myself recursively for subcategories
                AssignImports(CheckChild);
            }
        }
        #endregion

        #region Web Import Routines
        private void menuItemImportWeb_Click(object sender, System.EventArgs e)
        {
            if (!SoftwareIsRegistered)
            {
                MessageBox.Show(this, orGentaResources.RegistrationRequired);
                return;
            }
            WebImportForm WebImport = new WebImportForm();
            if (WebImport.ShowDialog(this) == DialogResult.OK)
            {
                MaxSpiderDepth = Convert.ToInt32(WebImport.txtHowManyLinkLevels.Text);
                string txtWebStartingPoint = WebImport.txtWebStartingPoint.Text;
                txtWebAlphaStart = WebImport.txtAlphaStart.Text;
                int WaitForWeb = Convert.ToInt32(WebImport.txtTimeout.Text);
                WebImpUseTitle = WebImport.rbTitle.Checked;
                WebImpUseMeta = WebImport.rBMetaTag.Checked;
                WebImpLinkExternal = WebImport.rBLeaveExternal.Checked;
                this.Cursor = Cursors.WaitCursor;
                if (!ShowDone)
                    { menuShowDone_Click(new object(), new System.EventArgs()); }

                SitesVisited.Clear();
                string ErrorBack = WebImportDriver(txtWebStartingPoint, WaitForWeb);
                this.Cursor = Cursors.Default;
                Application.DoEvents();
                if (ErrorBack != "")
                    { MessageBox.Show(this, ErrorBack); }
                else
                    { MessageBox.Show(this, orGentaResources.ImportCompleted); }
                ShowImportStatus.Close();
                ShowImportStatus.Dispose();
            }
            WebImport.Dispose();
            DelayedBuildGrid = true;
            this.ViewFormTimer.Enabled = true;
        }

        private string WebImportDriver(string txtWebStartingPoint, int WaitForWeb)
        {
            ShowImportStatus = new ImportStatus();
            ShowImportStatus.Show();
            UpdateImportStatus("Validating starting page . . .");
            HttpWebRequest myWebRequest;

            // See if we can connect to the internet
            try
            {
                Uri StartingUri = new Uri(txtWebStartingPoint);
                myWebRequest = (HttpWebRequest)WebRequest.Create(StartingUri);
            }
            catch (System.Exception e)
            {
                string ConnectError = "Failed Validation:";
                ConnectError += (char)10 + e.Message;
                return ConnectError;
            }

            myWebRequest.Timeout = WaitForWeb * 1000;
            UpdateImportStatus("Trying to connect to " + txtWebStartingPoint);
            try
            {
                HttpWebResponse WebDataBack = (HttpWebResponse)myWebRequest.GetResponse();
                UpdateImportStatus("Connected . . . ");
                ProcessWebPage(WebDataBack, 0);
            }
            catch (System.Net.WebException WebErr)
            {
                string ConnectError = "Internet Read Error:";
                ConnectError += (char)10 + WebErr.Message;
                return ConnectError;
            }
            return "";
        }

        private void ProcessWebPage(HttpWebResponse WebPageData, int SpiderLevel)
        {
            if (ShowImportStatus.WebImportSkip)
            {
                if (SpiderLevel == 1)
                { ShowImportStatus.WebImportSkip = false; }
                else
                { return; }
            }
            UpdateImportStatus("Importing data from " + WebPageData.ResponseUri.ToString());
            importedWebItem = "";
            importedWebNote = "";
            spacingRequests = 0;
            WebImpFoundTitle = false;
            WebImpFoundBody = false;
            WebImpInAcomment = false;
            ArrayList FollowUpLinks = new ArrayList();

            // Pipe the stream to a higher level stream reader with the required encoding format
            Stream receiveStream = WebPageData.GetResponseStream();
            Encoding UTFencoder = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader readStream = new StreamReader(receiveStream, UTFencoder);

            try
            {
                while (true)
                {
                    System.Threading.Thread.Sleep(10);
                    Application.DoEvents();
                    while (ShowImportStatus.WebImportPaused)
                    {
                        if (ShowImportStatus.WebImportCancel)
                        { return; }
                        System.Threading.Thread.Sleep(100);
                        Application.DoEvents();
                    }
                    if (ShowImportStatus.WebImportCancel)
                    { return; }

                    // Read and process one line at a time
                    ReadTheRemoteStream(FollowUpLinks, readStream);
                }
            }
            catch (Exception ex)
            { Console.WriteLine(ex); }

            readStream.Close();
            WebPageData.Close();

            if (importedWebItem != "")
            {
                int CurrentKey = nextItemkey[0];
                AddNewItemToMemDS(importedWebItem, false);
                TreeNode newAddCatNode = new TreeNode("-");
                newAddCatNode.Tag = "i-" + CurrentKey.ToString();
                FindNodeInTV(UnassignedNode, "", false, "").Nodes.Add(newAddCatNode);

                //	Now save the note and set the checkbox
                PersistNoteToDb(CurrentKey);
            }

            // Loop through the links
            if (SpiderLevel < MaxSpiderDepth)
            {
                FollowUpLinks.Sort();
                int HowManyLinks = FollowUpLinks.Count;
                string previousVisitedLink = "";
                for (int ThisLink = 0; ThisLink < HowManyLinks; ThisLink++)
                {
                    string LinkToFollow = FollowUpLinks[ThisLink].ToString();
                    //  honor top-level request for beginning location
                    if ((SpiderLevel == 0) && (LinkToFollow.CompareTo(txtWebAlphaStart) < 0))
                    { continue; }
                    //	make sure we didn't already grab this guy
                    if (LinkToFollow == previousVisitedLink)
                    { continue; }
                    else
                    { previousVisitedLink = LinkToFollow; }
                    bool exitLoop = ProcessOneLink(WebPageData, LinkToFollow, SpiderLevel);
                    if (exitLoop)
                    { return; }
                    if ((ShowImportStatus.WebImportSkip) && (SpiderLevel > 0))
                    { return; }
                }
            }
        }

        private void PersistNoteToDb(int CurrentKey)
        {
            OleDbCommand NotesCmdBuilder = new OleDbCommand("", this.oleorGentaDbConx);
            string AddOrChgNoteCmd = "INSERT INTO Notes(ItemKey, NoteText) VALUES ("
                + CurrentKey + ", '" + importedWebNote.Replace("'", "''") + "')";
            NotesCmdBuilder.CommandText = AddOrChgNoteCmd;
            try
            {
                oleorGentaDbConx.Open();
                NotesCmdBuilder.ExecuteNonQuery();
                DataRow[] dBitemRetrievedSet = this.ItemsMemImage.Tables[0].Select("ItemKey = " + CurrentKey);
                if (dBitemRetrievedSet.Length > 0)
                {
                    DataRow dBitemRetrieved = dBitemRetrievedSet[0];
                    dBitemRetrieved["HasNote"] = true;
                }
            }
            catch { }
            finally
            { oleorGentaDbConx.Close(); }

            NotesCmdBuilder.Dispose();
        }

        private void ReadTheRemoteStream(ArrayList FollowUpLinks, StreamReader readStream)
        {
            string RawWebData = readStream.ReadLine();
            ArrayList WebDataBack = ProcessHTMLdata(RawWebData);
            foreach (Hashtable ElementReturned in WebDataBack)
            {
                if (ElementReturned["Link"].ToString() != "")
                {
                    string LinkCandidate = ElementReturned["Link"].ToString();
                    string[] LinkCandidateParts = LinkCandidate.Split(new Char[] { '#' });
                    FollowUpLinks.Add(LinkCandidateParts[0].ToString());
                }
                ProcessOneElement(ElementReturned);
            }
        }

        private bool ProcessOneLink(HttpWebResponse WebPageData, string LinkToFollow, int SpiderLevel)
        {
            bool AbsoluteLink = false;
            bool exitLoop = true;
            bool goToNextLink = false;
            if (LinkToFollow.Length > 3)
            {
                if (LinkToFollow.Substring(0, 4).ToLower() == "http")
                { AbsoluteLink = true; }
            }
            if (!AbsoluteLink)
            {
                // change to absolute link if required
                string RelativePathStart = WebPageData.ResponseUri.GetLeftPart(System.UriPartial.Path);
                if (LinkToFollow.Substring(0, 1) == "/")
                {
                    for (int StartPathChar = 8; StartPathChar < RelativePathStart.Length; StartPathChar++)
                    {
                        if (RelativePathStart.Substring(StartPathChar, 1) == "/")
                        {
                            LinkToFollow = RelativePathStart.Substring(0, StartPathChar) + LinkToFollow;
                            break;
                        }
                    }
                }
                else
                {
                    if (RelativePathStart.Substring(RelativePathStart.Length - 1, 1) != "/")
                    {
                        for (int StartPathChar = RelativePathStart.Length - 1; StartPathChar > 8; StartPathChar--)
                        {
                            if (RelativePathStart.Substring(StartPathChar, 1) == "/")
                            {
                                RelativePathStart = RelativePathStart.Substring(0, StartPathChar + 1);
                                break;
                            }
                        }
                    }
                    LinkToFollow = RelativePathStart + LinkToFollow;
                }
            }

            // double check sites already visted to avoid duplicate
            int AlreadyVisited = SitesVisited.IndexOf(LinkToFollow, 0);
            if (AlreadyVisited >= 0)
            { return goToNextLink; }
            else
            { SitesVisited.Add(LinkToFollow); }

            while (ShowImportStatus.WebImportPaused)
            {
                if (ShowImportStatus.WebImportCancel)
                { return exitLoop; }
                System.Threading.Thread.Sleep(100);
                Application.DoEvents();
            }
            if (ShowImportStatus.WebImportCancel)
            { return exitLoop; }

            UpdateImportStatus("Trying to connect to " + LinkToFollow);
            try
            {
                HttpWebRequest myChildRequest;
                Uri ChildUri = new Uri(LinkToFollow);
                myChildRequest = (HttpWebRequest)WebRequest.Create(ChildUri);
                HttpWebResponse ChildPage = (HttpWebResponse)myChildRequest.GetResponse();
                UpdateImportStatus("Connected . . . ");
                ProcessWebPage(ChildPage, SpiderLevel + 1);
            }
            catch
            { UpdateImportStatus("Failed . . . "); }
            return goToNextLink;
        }

        private void ProcessOneElement(Hashtable ElementReturned)
        {
            if (ElementReturned["Type"].ToString() == "Title")
            { importedWebItem = ElementReturned["Data"].ToString(); }
            if (ElementReturned["Spacing"].ToString() != "0")
            {
                int ThisSpaceRequst = Convert.ToInt32(ElementReturned["Spacing"].ToString());
                switch (spacingRequests)
                {
                    case 0:
                        if (ThisSpaceRequst == 1)
                        { importedWebNote += "\r\n"; }
                        else
                        { importedWebNote += "\r\n\r\n"; }
                        break;
                    case 1:
                        importedWebNote += "\r\n";
                        break;
                }
                spacingRequests += ThisSpaceRequst;
            }

            // the Note data was returned
            if (ElementReturned["Type"].ToString() == "Contents")
            {
                if ((spacingRequests > 0) || (importedWebNote == ""))
                {
                    importedWebNote += ElementReturned["Data"].ToString();
                    spacingRequests = 0;
                }
                else
                { importedWebNote += " " + ElementReturned["Data"].ToString(); }
            }
        }

        private ArrayList ProcessHTMLdata(string RawWebData)
        {
            string[] WebDataIn = RawWebData.Split(new Char[] { '<' });
            int HowManyElements = WebDataIn.Length;
            ArrayList ParsedData = new ArrayList();
            Hashtable WorkingHash = new Hashtable();
            WorkingHash.Add("Type", "");
            WorkingHash.Add("Data", "");
            WorkingHash.Add("Link", "");
            WorkingHash.Add("Spacing", 0);

            for (int ThisElement = 0; ThisElement < HowManyElements; ThisElement++)
            {
                string ElementToCheck = WebDataIn[ThisElement].ToString();
                int WheresTheHTMLesc;
                string ElementLeftSide;
                string ElementRightSide;
                string HTMLesc;
                for (int j = 0; j < webEscapes.Length; j++)
                {
                    HTMLesc = webEscapes[j];
                    WheresTheHTMLesc = ElementToCheck.IndexOf(HTMLesc, 0);
                    while (WheresTheHTMLesc >= 0)
                    {
                        ElementLeftSide = ElementToCheck.Substring(0, WheresTheHTMLesc);
                        ElementRightSide = "";
                        if (ElementToCheck.Length > WheresTheHTMLesc + HTMLesc.Length)
                        { ElementRightSide = ElementToCheck.Substring(WheresTheHTMLesc + HTMLesc.Length); }
                        ElementToCheck = ElementLeftSide + webEscEQLs[j] + ElementRightSide;
                        WheresTheHTMLesc = ElementToCheck.IndexOf(HTMLesc, 0);
                    }
                }
                if (ElementToCheck.Trim() == "")
                { continue; }
                if (ElementToCheck.Length > 2)
                {
                    if (ElementToCheck.Substring(0, 3).ToLower() == "!--")
                    {
                        WebImpInAcomment = true;
                        continue;
                    }
                }

                // drop style sheets and embedded scripts
                if (ElementToCheck.Length > 5)
                {
                    if (ElementToCheck.Substring(0, 6).ToLower() == "style ")
                    {
                        WebImpInAcomment = true;
                        continue;
                    }
                }
                if (ElementToCheck.Length > 6)
                {
                    if (ElementToCheck.Substring(0, 7).ToLower() == "script ")
                    {
                        WebImpInAcomment = true;
                        continue;
                    }
                }
                if (WebImpInAcomment)
                {
                    if ((ElementToCheck.IndexOf(">", 0) >= 0) && (RawWebData.Substring(0, 1) == "<"))
                    { WebImpInAcomment = false; }
                    else
                    { continue; }
                }

                if (ElementToCheck.Length > 3)
                {
                    if (ElementToCheck.Substring(0, 4).ToLower() == "body")
                    { WebImpFoundBody = true; }
                }

                //	check for any special spacing requests
                if (ElementToCheck.Length > 2)
                {
                    if (ElementToCheck.Substring(0, 3).ToLower() == "br>")
                    { WorkingHash["Spacing"] = 1; }
                    if (ElementToCheck.Substring(0, 3).ToLower() == "tr>")
                    { WorkingHash["Spacing"] = 2; }
                }
                if (ElementToCheck.Length > 1)
                {
                    if (ElementToCheck.Substring(0, 2).ToLower() == "p>")
                    { WorkingHash["Spacing"] = 2; }
                    if (ElementToCheck.Substring(0, 2).ToLower() == "p ")
                    { WorkingHash["Spacing"] = 2; }
                }

                // does this tag start with a link?
                if (ElementToCheck.Length > 5)
                {
                    if (ElementToCheck.Substring(0, 6).ToLower() == "a href")
                    {
                        int GrabIt1 = ElementToCheck.ToLower().IndexOf("=", 0);
                        int GrabIt2 = ElementToCheck.ToLower().IndexOf("\"", GrabIt1);
                        int GrabIt3 = ElementToCheck.ToLower().IndexOf("\"", GrabIt2 + 1);
                        try
                        { WorkingHash["Link"] = ElementToCheck.Substring(GrabIt2 + 1, GrabIt3 - GrabIt2 - 1).Trim(); }
                        catch { }
                    }
                }

                GetTitleText(ParsedData, WorkingHash, ElementToCheck);

                // Process regular contents (note part) of imported page
                int PlaceToStart = ElementToCheck.IndexOf(">", 0) + 1;
                string CapturedText = "";
                try
                { CapturedText = ElementToCheck.Substring(PlaceToStart).Trim(); }
                catch { }
                if (CapturedText != "")
                {
                    WorkingHash["Data"] = CapturedText;
                    if ((WebImpFoundBody) && !(WebImpFoundTitle))
                    {
                        WorkingHash["Type"] = "Title";
                        WebImpFoundTitle = true;
                    }
                    else
                    { WorkingHash["Type"] = "Contents"; }
                }

                ParsedData.Add(WorkingHash.Clone());
                WorkingHash["Type"] = "";
                WorkingHash["Data"] = "";
                WorkingHash["Link"] = "";
                WorkingHash["Spacing"] = 0;
            }
            return ParsedData;
        }

        private void GetTitleText(ArrayList ParsedData, Hashtable WorkingHash, string ElementToCheck)
        {
            //	Locate the title for the Item text
            if ((WebImpUseTitle || WebImpUseMeta) && !(WebImpFoundTitle))
            {
                if (ElementToCheck.Length > 6)
                {
                    if (ElementToCheck.Substring(0, 6).ToLower() == "title>")
                    {
                        WorkingHash["Type"] = "Title";
                        WorkingHash["Data"] = ElementToCheck.Substring(6).Trim();
                        WebImpFoundTitle = true;
                        ParsedData.Add(WorkingHash.Clone());
                    }
                }
            }

            if ((WebImpUseMeta) && (ElementToCheck.Length > 5))
            {
                if (ElementToCheck.Substring(0, 4).ToLower() == "meta")
                {
                    if (ElementToCheck.ToLower().IndexOf("description", 0) >= 0)
                    {
                        int GrabIt1 = ElementToCheck.ToLower().IndexOf("content", 0);
                        int GrabIt2 = ElementToCheck.ToLower().IndexOf("\"", GrabIt1);
                        int GrabIt3 = ElementToCheck.ToLower().IndexOf("\"", GrabIt2 + 1);
                        try
                        {
                            WorkingHash["Type"] = "Title";
                            WorkingHash["Data"] = ElementToCheck.Substring(GrabIt2 + 1, GrabIt3 - GrabIt2 - 1).Trim();
                            WebImpFoundTitle = true;
                        }
                        catch { }
                    }
                }
            }

        }

        private void UpdateImportStatus(string UpdateMsg)
        {
            string statusText = ShowImportStatus.tbStatusDisplay.Text;
            if (statusText != "")
            { statusText += "\r\n" + UpdateMsg; }
            else
            { statusText = UpdateMsg; }
            ShowImportStatus.tbStatusDisplay.Text = statusText;
            ShowImportStatus.tbStatusDisplay.SelectionStart = statusText.Length;
            Application.DoEvents();
        }
        #endregion

        #region Export Routines
        private void menuExportCSV_Click(object sender, System.EventArgs e)
        {
            if (!SoftwareIsRegistered)
            {
                MessageBox.Show(this, orGentaResources.RegistrationRequired);
                return;
            }
            this.ExportItemsDialog.Filter = orGentaResources.CommaSep;
            this.ExportItemsDialog.DefaultExt = "csv";
            if (this.ExportItemsDialog.ShowDialog(this) == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                Application.DoEvents();
                string ExportFileName = this.ExportItemsDialog.FileName;
                StreamWriter ExportFile = new StreamWriter(ExportFileName);
                ExportFile.Write("\"" + "Category Path" + "\",");

                // Headers
                for (int j = 0; j < this.dataGridTableStyle1.GridColumnStyles.Count; j++)
                { ExportFile.Write("\"" + this.dataGridTableStyle1.GridColumnStyles[j].HeaderText.ToString() + "\","); }
                ExportFile.WriteLine("");

                // And now the grid data
                string CategoryPath = "";
                for (int i = 0; i < RowXrefArray.Count; i++)
                {
                    for (int j = 0; j < this.dataGridTableStyle1.GridColumnStyles.Count; j++)
                    {
                        if (RowXrefArray[i].ToString().IndexOf("-") != RowXrefArray[i].ToString().Length - 1)
                        { break; }
                        else if (j == 0)
                        {
                            CategoryPath = RowXrefArray[i].ToString();
                            ExportFile.Write("\"" + CategoryPath.Substring(0, CategoryPath.Length - 2) + "\",");
                        }

                        //	Special formatting for date fields
                        if (this.dataGridTableStyle1.GridColumnStyles[j].MappingName.IndexOf("Date") >= 0)
                        {
                            try
                            { ExportFile.Write("\"" + Convert.ToDateTime(this.ItemGrid[i, j]).ToShortDateString() + "\","); }
                            catch
                            { ExportFile.Write("\"" + "\","); }
                        }
                        else
                        { ExportFile.Write("\"" + this.ItemGrid[i, j].ToString() + "\","); }
                        if (j == this.dataGridTableStyle1.GridColumnStyles.Count - 1)
                        { ExportFile.WriteLine(""); }
                    }
                }
                ExportFile.Flush();
                ExportFile.Close();
                ExportFile.Dispose();
                Cursor = Cursors.Default;
            }
        }

        private void menuExportTabbed_Click(object sender, System.EventArgs e)
        {
            if (!SoftwareIsRegistered)
            {
                MessageBox.Show(this, orGentaResources.RegistrationRequired);
                return;
            }
            FieldSelector GetFields = BuildFieldSelector();
            if (GetFields.ShowDialog(this) == DialogResult.OK)
            {
                this.ExportItemsDialog.Filter = orGentaResources.TabbedText;
                this.ExportItemsDialog.DefaultExt = "txt";
                if (this.ExportItemsDialog.ShowDialog(this) == DialogResult.OK)
                {
                    this.Cursor = Cursors.WaitCursor;
                    Application.DoEvents();
                    string ExportFileName = this.ExportItemsDialog.FileName;
                    StreamWriter ExportFile = new StreamWriter(ExportFileName);

                    // Write column headers and the datagrid
                    foreach (object itemChecked in GetFields.chkBoxFields.CheckedItems)
                    { ExportFile.Write(itemChecked.ToString() + (char)9); }
                    ExportFile.WriteLine("");
                    for (int i = 0; i < RowXrefArray.Count; i++)
                    { ExportThisRow(i, GetFields, ExportFile); }
                    ExportFile.Flush();
                    ExportFile.Close();
                    ExportFile.Dispose();
                    this.Cursor = Cursors.Default;
                }
            }
            GetFields.Dispose();
        }

        private void ExportThisRow(int i, FieldSelector GetFields, StreamWriter ExportFile)
        {
            string CatToWrite = "";
            bool WritingCategories = false;
            foreach (object itemChecked in GetFields.chkBoxFields.CheckedItems)
                {ExportAfield(i, itemChecked, WritingCategories, CatToWrite, ExportFile);}
            if (CatToWrite == "Add Category")
                { }
            else if ((this.ItemGrid[i, 2].ToString() == NewItemClickMsg) && (!WritingCategories))
                { }
            else if ((this.ItemGrid[i, 2].ToString() == "") && (!WritingCategories))
                { }
            else
                { ExportFile.WriteLine(""); }
        }

        private void ExportAfield(int i, object itemChecked, bool WritingCategories, string CatToWrite, StreamWriter ExportFile)
        {
            if (itemChecked.ToString() == "Category")
            {
                WritingCategories = true;
                CatToWrite = GrabCatfromXrefA(RowXrefArray[i].ToString());
                if (CatToWrite == "Add Category")
                    { return; }
                ExportFile.Write(CatToWrite + (char)9);
            }
            if ((this.ItemGrid[i, 2].ToString() == NewItemClickMsg) && (!WritingCategories))
                { return; }
            if ((this.ItemGrid[i, 2].ToString() == "") && (!WritingCategories))
                { return; }

            //	Loop to find the column that matches the selected field to export
            for (int j = 0; j < this.dataGridTableStyle1.GridColumnStyles.Count; j++)
            {
                if (this.dataGridTableStyle1.GridColumnStyles[j].HeaderText == itemChecked.ToString())
                {
                    if (this.dataGridTableStyle1.GridColumnStyles[j].MappingName.IndexOf("Date") >= 0)
                    {
                        try
                            { ExportFile.Write(Convert.ToDateTime(this.ItemGrid[i, j]).ToShortDateString() + (char)9); }
                        catch
                            { ExportFile.Write((char)9); }
                    }
                    else
                    {
                        if (this.ItemGrid[i, j].ToString() == NewItemClickMsg)
                            { ExportFile.Write((char)9); }
                        else
                            { ExportFile.Write(this.ItemGrid[i, j].ToString() + (char)9); }
                    }
                    return;
                }
            }
        }

        private string GrabCatfromXrefA(string inboundXrefValue)
        {
            if (inboundXrefValue.IndexOf("-") == inboundXrefValue.Length - 1)
            { return ""; }
            string[] CategoryList = inboundXrefValue.Split(new Char[] { '\\' });
            int CatListLen = CategoryList.Length;
            return CategoryList[CatListLen - 1].ToString();
        }
        #endregion

    }
}