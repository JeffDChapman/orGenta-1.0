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
        private RegistryKey SaveIfaceToRegistry(RegistryKey ThisUser)
        {
            RegistryKey ScreenLoc = ThisUser.CreateSubKey("Software\\orGenta\\ScreenLocation");
            ScreenLoc.SetValue("Top", this.Top);
            ScreenLoc.SetValue("Left", this.Left);
            RegistryKey ScreenSize = ThisUser.CreateSubKey("Software\\orGenta\\ScreenSize");
            ScreenSize.SetValue("Height", this.Height);
            ScreenSize.SetValue("Width", this.Width);
            RegistryKey Splitter = ThisUser.CreateSubKey("Software\\orGenta\\Splitter");
            Splitter.SetValue("Location", this.CategoryHierTV.Width);
            RegistryKey ColWidths = ThisUser.CreateSubKey("Software\\orGenta\\ColumnWidth");
            return ColWidths;
        }
        
        private OleDbCommand GetSoftwareKeyInfo(ref string SoftwareKeyRetd, string GetItemCmd)
        {
           // oleorGentaDbConx.Open();
            OleDbCommand KeyCmdBuilder = new OleDbCommand(GetItemCmd,
                this.oleorGentaDbConx);
            try
                { SoftwareKeyRetd = (string)KeyCmdBuilder.ExecuteScalar(); }
            catch { }
            return KeyCmdBuilder;
        }
        
        private TreeNode ResetTopNode(TreeNode tnDisposer)
        {
            this.CategoryHierTV.Nodes[0].Remove();
            tnDisposer = null;
            this.CategoryHierTV.Nodes.Add((TreeNode)DoneTreeView.Nodes[0].Clone());
            this.CategoryHierTV.EndUpdate();
            this.CategoryHierTV.CollapseAll();
            this.catInvisLabel.Visible = false;
            this.CategoryHierTV.Nodes[0].Expand();
            return tnDisposer;
        }

        private void NullFields4through11()
        {
            ItemFields[4] = Convert.ToBoolean(false); // Done Flag
            ItemFields[5] = DBNull.Value; // Done Date
            ItemFields[6] = DBNull.Value; // Priority
            ItemFields[7] = DBNull.Value; // Work Ph
            ItemFields[8] = DBNull.Value; // Cell Ph
            ItemFields[9] = DBNull.Value; // Home Ph
            ItemFields[10] = DBNull.Value; // eMail
            ItemFields[11] = DBNull.Value; // Address
        }

        private TreeNode AddAnewNode(object SoftAssignment)
        {
            Hashtable HoldHashTable = SoftAssignment as Hashtable;
            TreeNode ParentNode = (TreeNode)HoldHashTable["ParentNode"];
            TreeNode NewNode = (TreeNode)HoldHashTable["NewNode"];
            ParentNode.Nodes.Insert(0, NewNode);
            if (!ShowDone)
                { AddToDoneReplay("Insert", (TreeNode)NewNode.Clone(), ParentNode.FullPath, ParentNode, null); }

            // Remove item from Unassigned category if it was there
            LookForItem(FindNodeInTV(UnassignedNode, "", false, "").Nodes, NewNode.Tag.ToString(), true, false, false);
            return NewNode;
        }
        
        private void SetNoteCheckBox(string ActiveItem)
        {
            DataRow[] dBitemRetrievedSet = this.ItemsMemImage.Tables[0].Select("ItemKey = " + ActiveItem);
            if (dBitemRetrievedSet.Length > 0)
            {
                DataRow dBitemRetrieved = dBitemRetrievedSet[0];
                dBitemRetrieved["HasNote"] = true;
                int GridRowLoc = this.ItemGrid.CurrentCell.RowNumber;
                int BackingRow = Convert.ToInt32(this.ItemGrid[GridRowLoc, 0]) - 1;
                this.ItemGrid[BackingRow, 1] = true;
            }
        }
        
        private TreeNode RebuildTreeFromDone(TreeNode tnDisposer)
        {
            FullTreeView.Nodes[0].Remove();
            tnDisposer = null;
            tnDisposer = DoneTreeView.Nodes[0];
            DoneTreeView.Nodes[0].Remove();
            tnDisposer = null;
            RebuildMirrorTreeViews();
            return tnDisposer;
        }
        
        private void HandleEnterAndEscape(System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
            if (e.KeyChar == (char)27)
            {
                e.Handled = true;
                this.txtNoteSearch.Text = "";
                SendKeys.Send("{TAB}");
            }
        }
        
        private FieldSelector BuildFieldSelector()
        {
            FieldSelector GetFields = new FieldSelector();
            GetFields.chkBoxFields.Items.Add("Category");
            for (int j = 0; j < this.dataGridTableStyle1.GridColumnStyles.Count; j++)
                { GetFields.chkBoxFields.Items.Add(this.dataGridTableStyle1.GridColumnStyles[j].HeaderText); }
            return GetFields;
        }
        
        private static TreeNode BuildaNewTreeNode(DataRow dBitemRetrieved)
        {
            string WorkingText = dBitemRetrieved["TextItem"].ToString();
            int KeyToMatch = Convert.ToInt16(dBitemRetrieved["ItemKey"]);
            TreeNode newAddCatNode = new TreeNode("-");
            newAddCatNode.Tag = "i-" + KeyToMatch.ToString();
            return newAddCatNode;
        }



    }
}