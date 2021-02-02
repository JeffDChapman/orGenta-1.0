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

// TODO: Reduce footprint of includes, if possible

// NOTICE: This software is Copyright (c) 2005, 2006 by Jeff D. Chapman
// All Rights Reserved

namespace orGenta
{
	public partial class ViewForm : System.Windows.Forms.Form
	{
		[STAThread]

		#region Entry Point
		static void Main()
		{
			if (File.Exists(Environment.CurrentDirectory + "\\StartDebug.txt"))
				{Debugging = true;}

			if (Debugging)
			{
				FileStream CaptureRedirFile = new FileStream(Environment.CurrentDirectory + 
					"\\CaptureLog.txt", FileMode.Create);
				RedirectedConsole = new StreamWriter(CaptureRedirFile);
				RedirectedConsole.AutoFlush = true;
				Console.SetOut(RedirectedConsole);
			}

			Console.WriteLine("dotNet Runtime Version: {0}", 
				Environment.Version.ToString());

			// Test if user is trying to start a second instance of the software
			bool ok;
			Mutex ownerMutex = new System.Threading.Mutex(true, "orGenta", out ok);

			if (! ok)
			{
				MessageBox.Show(orGentaResources.AlreadyRunning);
				return;}
			Application.Run(new ViewForm());
			GC.KeepAlive(ownerMutex);
		}
		#endregion

		#region Variable Initializations

		// various runtime status flags
		private static bool Debugging = false;
		private bool GridInitialized = false;
		private bool AddingNewCategory;
		private bool endRoutinesRun;
		private bool endRoutinesRunning;
		private bool CollapseWaterfall;
		private bool SoftwareIsRegistered;
		private bool MatchedOnPrevNode;
		private bool FoundMatchNode = false;
		private bool findReplaying = false;
		private bool dateInThePast = false;
		private bool RunningMinimal = false;
		private bool ShowDone;
		private bool RebuildingMirrorTrees;
		private bool GridRebuilding;
		private bool AlreadyAskedOnDiscard;
		private bool print1toAline;
		private bool AutoPopNote;
		private bool fastAbort;
		private bool DeleteCancelled;
		private bool WebImpUseTitle;
		private bool WebImpUseMeta;
		private bool WebImpLinkExternal;
		private bool WebImpFoundTitle;
		private bool WebImpFoundBody;
		private bool WebImpInAcomment;

		// flags used in signalling events to run off the bkground timer
		private bool PendingDeletions;
		private bool CategoryAssignPending;
		private bool SoftAssignPending;
		private bool checkForGridAccess;
		private bool StartupRoutines;
		private bool ReselectNode;
		private bool AddingNewCategoryAutoEd;
		private bool DelayedBuildGrid;
		private bool EditItemMatchingCategory;
		private bool DupeRelocate;

		// flags set to prevent infinite cascade recursions
		private bool collapsingFlag;
		private bool DisableGridScroll = false;
		private bool tvSelectionWaterfall;

		// environment related variables
		string ThisLanguage; // The Windows language setting: if Eng then extra features are available
		private Version softwareVersion;
		private string CatHierSaveFileName;
		private string DemoKey = "Software ID bd3b6fae-6f17-47aa-89d4-9c0cd83d52e7";
		private static string TrashCanNode;
		private static string UnassignedNode;
		private DateTime TodaysDate = DateTime.Today;
		private static StreamWriter RedirectedConsole;

		// interface related variables
		private static string NewItemClickMsg = orGentaResources.gridEntryPrompt;
		private int GridRowCounter;
		private string lastExecutedSearch = "";
		private string PreviousMatchedItem = "";
		private int SaveEditedCol;
		private int SaveEditedRow;
		private string DeleteOptionMode = "";
		private TreeView DoneTreeView = new TreeView();
		private TreeView FullTreeView = new TreeView();
		private int PrevVisibleTopGridRow;
		private int PrevLeftGridColumn;
		private TreeNode PrevVisibleTopTVnode;
		private int clickedColumn;
		private int clickedRow;
		private Color FrozenColor = Color.BlueViolet;
		private MinimalIntface GetTextLineForm = new MinimalIntface();
		private ImportStatus ShowImportStatus;
		private string txtWebAlphaStart;

		// other work-area variables
		StreamReader PrintWorkFile;
		private int[] nextItemkey = new int[1];
		private string[] SavedColWidths = new string[14];
		private string[] DefaultColWidths = new string[14];
		private string TextToSoftAssign;
		private TreeNode CategoryToScan;
		private string CategoryToScanText;
		private string prevLookForOccursAgain = "";
		private DataTable ImportedItems;
		private object[] ItemFields = new object[15];
		private char[] PunctuationArray = new char [] {' ', ',', '.', ':', '?', '!', '\''};
		private char[] DateParseArray = new char [] {' ', ',', '.', '?', '!'};
		private ArrayList DayOfWeekList = new ArrayList();
		private ArrayList RowXrefArray = new ArrayList();
		private ArrayList ItemIDs = new ArrayList();
		private ArrayList DeletedRows = new ArrayList();
		private ArrayList SoftAssignedNodes = new ArrayList();
		private ArrayList CategoryAssignedNodes = new ArrayList();
		private ArrayList DoneReplay = new ArrayList();
		private ArrayList NextOccurMatches = new ArrayList();
		private System.Threading.Thread SoftAssignThread;
		private System.Threading.Thread CategoryScanThread;
		private string importedWebItem;
		private string importedWebNote;
		private int spacingRequests;
		private int MaxSpiderDepth;
		private ArrayList SitesVisited = new ArrayList();
		private string[] webEscapes = new string [] {"&nbsp;", "&amp;", "&quot;", "&#151;", "&copy;", "&#169;", "&#149;", "&#187;"};
		private string[] webEscEQLs = new string [] {" ", "&", "\"", "--", "(c)", "(c)", "+", ">>"};

        #endregion

		#region Application Initialization

		public ViewForm()
		{
			// this.oleorGentaDbConx.ConnectionString = @"Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Registry Path=;Jet OLEDB:Database Locking Mode=0;Jet OLEDB:Database Password=;Data Source=""" + Environment.CurrentDirectory + @"\orGenta.mdb"";Password=;Jet OLEDB:Engine Type=4;Jet OLEDB:Global Bulk Transactions=1;Provider=""Microsoft.Jet.OLEDB.4.0"";Jet OLEDB:System database=;Jet OLEDB:SFP=False;Extended Properties=;Mode=Share Deny None;Jet OLEDB:New Database Password=;Jet OLEDB:Create System Database=False;Jet OLEDB:Don't Copy Locale on Compact=False;Jet OLEDB:Compact Without Replica Repair=False;User ID=Admin;Jet OLEDB:Encrypt Database=False";
			// the above line replaces the similar line in the Windows Form Designer section before compiling a production build
			// (but comment out the Windows Form Designer line because you will want it back when debugging)

			InitializeComponent();
			for (int i = 0; i < this.dataGridTableStyle1.GridColumnStyles.Count; i++)
				{DefaultColWidths[i] = this.dataGridTableStyle1.GridColumnStyles[i].Width.ToString();}
			restoreRegistrySettings();
			setGridColWidths();

            InitializeVariables();

            ValidateDatabase();

			// See if they're registered
			SoftwareIsRegistered = false;
			string softwareIDretrieved = GetSoftwareKey();
			if (!orGregistered(softwareIDretrieved, ""))
			{
				PleaseReg PlzRegisterBox = new PleaseReg();
				PlzRegisterBox.txtSoftwareID.Text = softwareIDretrieved;
				PlzRegisterBox.ShowDialog(this);
				PlzRegisterBox.Dispose();
			}
			else
				{SoftwareIsRegistered = true;}

			if (SoftwareIsRegistered)
				{this.menuRegistration.Enabled = false;}
			this.ViewFormTimer.Enabled = true;
		}

        private void InitializeVariables()
        {
            AddingNewCategory = false;
            AddingNewCategoryAutoEd = false;
            endRoutinesRun = false;
            ReselectNode = false;
            StartupRoutines = true;
            tvSelectionWaterfall = false;
            GridRebuilding = false;
            DelayedBuildGrid = false;
            SoftAssignPending = false;
            CategoryAssignPending = false;
            ShowDone = false;
            PendingDeletions = false;
            AlreadyAskedOnDiscard = false;
            this.menuShowDone.Checked = false;
            CollapseWaterfall = false;
            checkForGridAccess = false;
            ThisLanguage = Thread.CurrentThread.CurrentCulture.ThreeLetterISOLanguageName;
            CatHierSaveFileName = Environment.CurrentDirectory + "\\CategHier.stv";
            SaveEditedRow = -1;
            PrevVisibleTopGridRow = 0;
            PrevLeftGridColumn = 0;
            for (int i = 1; i < 8; i++)
                { DayOfWeekList.Add(TodaysDate.AddDays(i).DayOfWeek.ToString()); }
        }

		private void setGridColWidths()
		{
			if (SavedColWidths[0] == "")
				{SavedColWidths = DefaultColWidths;}
			for (int i = 0; i < this.dataGridTableStyle1.GridColumnStyles.Count; i++)
				{this.dataGridTableStyle1.GridColumnStyles[i].Width = 
				 Convert.ToInt16(SavedColWidths[i]);}
		}

		private void restoreRegistrySettings()
		{
			RegistryKey ThisUser = Registry.CurrentUser;
			try
			{
				RegistryKey ScreenLoc = ThisUser.OpenSubKey("Software\\orGenta\\ScreenLocation", true);
				this.Top = Convert.ToInt32(ScreenLoc.GetValue("Top", 1));
				this.Left = Convert.ToInt32(ScreenLoc.GetValue("Left", 1));
				RegistryKey ScreenSize = ThisUser.OpenSubKey("Software\\orGenta\\ScreenSize", true);
				this.Height = Convert.ToInt32(ScreenSize.GetValue("Height", 532)) - 19;
				if (this.Height < 100)
				    {this.Height = 100;}
				this.Width = Convert.ToInt32(ScreenSize.GetValue("Width", 728));
				if (this.Width < 100)
				    {this.Width = 100;}
				RegistryKey Splitter = ThisUser.OpenSubKey("Software\\orGenta\\Splitter");
				this.CategoryHierTV.Width = Convert.ToInt32(Splitter.GetValue("Location", 152));
				RegistryKey ColWidths = ThisUser.OpenSubKey("Software\\orGenta\\ColumnWidth", true);
				SavedColWidths = ColWidths.GetValue("Values", "").ToString().Split(new Char[] {','});
			}
			catch
			{
                RegistryKey ColWidths = SaveIfaceToRegistry(ThisUser);
				ColWidths.SetValue("Values", String.Join(",", DefaultColWidths));
				SavedColWidths = DefaultColWidths;
			}
			this.lblLoading.Left = this.CategoryHierTV.Width + 30;
		}

		private void ValidateDatabase()
		{
			fastAbort = false;
			softwareVersion = Assembly.GetExecutingAssembly().GetName().Version;
			Console.WriteLine("Software Version: {0}", softwareVersion.ToString());
			string orGdBName = Environment.CurrentDirectory + "\\orGenta.mdb";
			if (File.Exists(orGdBName))
			{
				string SoftwareKeyRetd = "";
				string GetItemCmd = "SELECT TextItem FROM Items WHERE Recurrence = 'syskey' AND TextItem LIKE 'Database Version%'";
				oleorGentaDbConx.Open();
                OleDbCommand KeyCmdBuilder = GetSoftwareKeyInfo(ref SoftwareKeyRetd, GetItemCmd);
				oleorGentaDbConx.Close();
				Version dBversion = new Version(SoftwareKeyRetd.Substring(17));

				if (softwareVersion.CompareTo(dBversion) > 0)
				{
                    MessageBox.Show(this, orGentaResources.dbVsnErr);
					string errVsnInfo = "orGenta Vsn " + softwareVersion.ToString() + 
						" -- Database Vsn " + dBversion.ToString();
					throw new Exception(errVsnInfo);
				}
			}
			else
			{
				// this code runs for the initial installation
				string seedFile = Environment.CurrentDirectory + "\\orGseed.mdb";
				File.Copy(seedFile, orGdBName);
				string treeSeedFile = Environment.CurrentDirectory + "\\CategSeed.stv";
				File.Copy(treeSeedFile, CatHierSaveFileName);
			}
		}
		#endregion

		#region Registration Verification

		private bool orGregistered (string InputSoftwareID, string SoftwareKey)
		{
			Console.WriteLine(InputSoftwareID);
			string SoftwareKeyRetd = "";
			if (SoftwareKey == "")
			{
				string GetItemCmd = "SELECT TextItem FROM Items WHERE Recurrence = 'syskey' AND TextItem LIKE 'Shareware Key%'";
                OleDbCommand KeyCmdBuilder = GetSoftwareKeyInfo(ref SoftwareKeyRetd, GetItemCmd);
				oleorGentaDbConx.Close();
			}
			else
				{SoftwareKeyRetd = SoftwareKey;}
			if (SoftwareKeyRetd == null)
				{return false;}

			// Validate key
			int SeedValue = 0;
			char[] IDarray = InputSoftwareID.ToCharArray();
			for (int i = 0; i < IDarray.Length; i++)
				{SeedValue += (int) IDarray[i];}
			Random RandomGen = new Random(SeedValue);
			string KeyGenerated = "Shareware Key " + RandomGen.Next(10000, 99999).ToString();
			if (KeyGenerated != SoftwareKeyRetd)
				{return false;}
			return true;
		}

		private string GetSoftwareKey()
		{
			string SoftwareKeyRetd = "";
			string GetItemCmd = "SELECT TextItem FROM Items WHERE Recurrence = 'syskey' AND TextItem LIKE 'Software ID%'";
            OleDbCommand KeyCmdBuilder = GetSoftwareKeyInfo(ref SoftwareKeyRetd, GetItemCmd);

			// Delete the software ID if it's a carryover from Demo version
			if ((SoftwareKeyRetd == DemoKey) && (softwareVersion.Major > 0))
			{
				string DelKeyCmd = "DELETE FROM Items WHERE Recurrence = 'syskey' AND TextItem LIKE 'Software ID%'";
				KeyCmdBuilder.CommandText = DelKeyCmd;
				try
				{
					KeyCmdBuilder.ExecuteNonQuery();
					string DelKey2Cmd = "DELETE FROM Items WHERE Recurrence = 'syskey' AND TextItem LIKE 'Shareware Key%'";
					KeyCmdBuilder.CommandText = DelKey2Cmd;
					KeyCmdBuilder.ExecuteNonQuery();
					Application.DoEvents();
				}
				catch {}
				SoftwareKeyRetd = null;
			}

			// Build a new software key if it doesn't exist
			if (SoftwareKeyRetd == null)
			{
				SoftwareKeyRetd = "Software ID " + Guid.NewGuid().ToString();
				string AddSysKey = "INSERT INTO Items(Recurrence, TextItem) VALUES ('syskey', '" + SoftwareKeyRetd + "')";
				KeyCmdBuilder.CommandText = AddSysKey;
				try
				{
					KeyCmdBuilder.ExecuteNonQuery();
					nextItemkey[0]++;
				}
				catch {}
			}
            KeyCmdBuilder.Dispose();
			oleorGentaDbConx.Close();
			return SoftwareKeyRetd;
		}

		#endregion

		#region Form Controls and Dataset Initialization

		private void DataInitialization()
		{
			this.Cursor = Cursors.WaitCursor;
			Application.DoEvents();
			if (File.Exists(CatHierSaveFileName))
			{
				this.CategoryHierTV.Nodes.Clear();
				loadTree(this.CategoryHierTV, CatHierSaveFileName);
			}

			// Create memory image of Items dB
			this.ItemsDataAdapter.Fill(this.ItemsMemImage);
			object nextItemkeyHold;
			string GetMaxItemCmd = "Select MAX(ItemKey) as MaxKey From Items";
			oleorGentaDbConx.Open();
            using (OleDbCommand KeyCmdBuilder = new OleDbCommand(GetMaxItemCmd, this.oleorGentaDbConx))
                {nextItemkeyHold = KeyCmdBuilder.ExecuteScalar();}
			oleorGentaDbConx.Close();
			nextItemkey[0] = Convert.ToInt16(nextItemkeyHold) + 1;

			RebuildMirrorTreeViews();
			this.CategoryHierTV.BeginUpdate();
			TreeNode tnDisposer = this.CategoryHierTV.Nodes[0];
            tnDisposer = ResetTopNode(tnDisposer);
			this.CategoryHierTV.SelectedNode = this.CategoryHierTV.GetNodeAt(1,1);
			PrevVisibleTopTVnode = this.CategoryHierTV.TopNode;

			// Build item grid
			iGridDataSource = (orGenta.ItemsDataSet) this.ItemsMemImage.Clone();
			iGridDataSource.Tables[0].ColumnChanging += new DataColumnChangeEventHandler(ItemGridData_ColumnChanging);
			iGridDataSource.Tables[0].RowDeleting += new DataRowChangeEventHandler(ItemGridData_RowDeleting);
			BuildGrid();
			this.catInvisLabel.Width = this.CategoryHierTV.Width - 21;
			this.ItemGrid.DataSource = this.iGridDataSource.Items;
			this.iGridDataSource.Items.DefaultView.ListChanged += new 
				System.ComponentModel.ListChangedEventHandler(this.ItemGrid_sortClick);
			this.pnlCatFind.Width = this.CategoryHierTV.Width - 21;
			this.txtCatSearch.Width = this.pnlCatFind.Width - 15;
			this.ItemGrid.Visible = true;
			this.Cursor = Cursors.Default;
		}

		private void RebuildMirrorTreeViews()
		{
			RebuildingMirrorTrees = true;
			FullTreeView.Nodes.Add((TreeNode) this.CategoryHierTV.Nodes[0].Clone());

			// Create copy of treeview that hides Done items
			DoneTreeView.Nodes.Add((TreeNode) this.CategoryHierTV.Nodes[0].Clone());
			DataRow[] dBitemRetrievedSet = this.ItemsMemImage.Tables[0].Select("Done = true");
			string dBitemNum = "";
			foreach (DataRow DoneRow in dBitemRetrievedSet)
			{
				dBitemNum = "i-" + DoneRow["ItemKey"].ToString();
				LookForItem(DoneTreeView.Nodes[0].Nodes, dBitemNum, true, false, false);
			}
			dBitemRetrievedSet = null;
			RebuildingMirrorTrees = false;
		}

		#endregion

		#region ItemGrid Text Changes and Category Synch

		private void ItemGridData_ColumnChanging(object sender, DataColumnChangeEventArgs e)
		{
			SaveEditedCol = this.ItemGrid.CurrentCell.ColumnNumber;
			SaveEditedRow = this.ItemGrid.CurrentCell.RowNumber;
			if ((e.Column.ToString() == "TextItem") && (e.Row["TextItem"].ToString() == NewItemClickMsg))
			{AddItemLeaf(e.ProposedValue.ToString(), Convert.ToInt16(e.Row["ItemKey"]), false);}
			else if (this.CategoryHierTV.SelectedNode.Text == "-")
			{
				string dBitemNum = this.CategoryHierTV.SelectedNode.Tag.ToString().Substring(2);
				DataRow[] dBitemRetrievedSet = this.ItemsMemImage.Tables[0].Select("ItemKey = " + dBitemNum);
				if (dBitemRetrievedSet.Length > 0)
				{
					DataRow dBitemRetrieved = dBitemRetrievedSet[0];
					if (e.ProposedValue == System.DBNull.Value)
						{dBitemRetrieved[e.Column.ToString()] = System.DBNull.Value;}
					else if ((e.ProposedValue.ToString() == "") && (e.Column.ToString() == "TextItem"))
						{e.Row.CancelEdit();
						e.Row.Delete();}
					else
						{dBitemRetrieved[e.Column.ToString()] = e.ProposedValue.ToString();}
				}
			}
		}

		private void AddItemLeaf(string NewTextValue, int GridRowNumber, bool recurAutoAdd)
		{
			int CurrentKey = nextItemkey[0];
			AddNewItemToMemDS(NewTextValue, recurAutoAdd);
			TreeNode newAddCatNode = new TreeNode("-");
			newAddCatNode.Tag = "i-" + CurrentKey.ToString();
			tvSelectionWaterfall = true;
			CategoryHierTV.SelectedNode = CategoryHierTV.TopNode;
			int TopGridRow = this.ItemGrid.HitTest(100,20).Row;
            GridRowNumber = WalkUpTheGrid(GridRowNumber, TopGridRow);
			int insertLoc = 0;

			if (recurAutoAdd)
			{
				insertLoc = CategoryHierTV.SelectedNode.Index;
				CategoryHierTV.SelectedNode.Parent.Nodes.Insert(insertLoc, newAddCatNode);
			}
			else
				{CategoryHierTV.SelectedNode.Nodes.Insert(0, newAddCatNode);}

			if (!ShowDone)
			{
				if (recurAutoAdd)
				{
					AddToDoneReplay("Insert " + insertLoc.ToString(), (TreeNode) 
					 newAddCatNode.Clone(), CategoryHierTV.SelectedNode.Parent.FullPath, 
					 CategoryHierTV.SelectedNode.Parent, null);}
				else
				{
					AddToDoneReplay("Insert", (TreeNode) newAddCatNode.Clone(), 
					 CategoryHierTV.SelectedNode.FullPath, CategoryHierTV.SelectedNode, null);}
			}
			Application.DoEvents();

			// Start the background soft assignment thread
			TextToSoftAssign = NewTextValue;
			SoftAssignThread = new System.Threading.Thread(new ThreadStart(SoftAssign));
			SoftAssignThread.Name = "SoftAssign";
			SoftAssignThread.Priority = System.Threading.ThreadPriority.BelowNormal;
			SoftAssignThread.IsBackground = false;
			Application.DoEvents();
			SoftAssignThread.Start();
			System.Threading.Thread.Sleep(20);

			CategoryHierTV.SelectedNode.Expand();
			DelayedBuildGrid = true;
			this.ViewFormTimer.Enabled = true;
		}

        private int WalkUpTheGrid(int GridRowNumber, int TopGridRow)
        {
            while (GridRowNumber > TopGridRow)
            {
                GridRowNumber--;
                CategoryHierTV.SelectedNode = CategoryHierTV.SelectedNode.NextVisibleNode;
            }
            tvSelectionWaterfall = false;
            return GridRowNumber;
        }

		private void AddNewItemToMemDS(string NewTextValue, bool recurAutoAdd)
		{
			ItemFields[0] = nextItemkey[0]; // Key
			ItemFields[1] = Convert.ToBoolean(false); // Note flag
			ItemFields[2] = NewTextValue; // Text
            NullFields4through11();
			ItemFields[12] = TodaysDate; // Entry Date
			ItemFields[13] = DBNull.Value; // Web link
			ItemFields[14] = CheckRecurrence(NewTextValue); // Recurs
			if (ItemFields[14] == DBNull.Value)   // When Date
				{ItemFields[3] = FigureOutWhenDate(NewTextValue);}
			else
			{
				ItemFields[3] = SpecializedWhenDate(ItemFields[14].ToString(), 
				 FigureOutWhenDate(NewTextValue), recurAutoAdd);}
			ItemsMemImage.Tables[0].Rows.Add(ItemFields);
			nextItemkey[0]++;
		}

		private void ItemGrid_CurrCellChanged(object sender, System.EventArgs e)
		{
			if (DisableGridScroll)
				{return;}
			if (PendingDeletions)
				{return;}
			if (GridRebuilding)
				{return;}
			if (ItemGrid.ReadOnly)
				{return;}
			if (this.ItemGrid.CurrentRowIndex < 0)
				{return;}

			int TopGridRow = this.ItemGrid.HitTest(100,20).Row;
			int GridRowNumber = this.ItemGrid.CurrentRowIndex + 1;
			tvSelectionWaterfall = true;

			// See if it's necessary to scroll the TreeView
			if (PrevVisibleTopTVnode != CategoryHierTV.TopNode)
			{
				CategoryHierTV.BeginUpdate();
				CategoryHierTV.SelectedNode = CategoryHierTV.Nodes[0];
				CategoryHierTV.SelectedNode.EnsureVisible();
				for (int i = 0; i < TopGridRow; i++)
				{CategoryHierTV.SelectedNode = CategoryHierTV.SelectedNode.NextVisibleNode;}
				PrevVisibleTopGridRow = 0;
				string Caller = "GridCellChanged";
				ItemGrid_Scroll(Caller, new System.EventArgs());
				CategoryHierTV.EndUpdate();
				PrevVisibleTopTVnode = CategoryHierTV.TopNode;
			}
			else
				{CategoryHierTV.SelectedNode = CategoryHierTV.TopNode;}

            GridRowNumber = WalkUpTheGrid(GridRowNumber, TopGridRow);
		}

		private void ItemGrid_Scroll(object sender, System.EventArgs e)
		{
			if (DisableGridScroll)
				{return;}
			DisableGridScroll = true;
			int moveCounter = 0;
			int TopGridRow = this.ItemGrid.HitTest(100,20).Row;
			int LeftGridCol = this.ItemGrid.HitTest(50,20).Column;
			if (LeftGridCol != PrevLeftGridColumn)
			{
				PrevLeftGridColumn = LeftGridCol;
				DisableGridScroll = false;
				this.ItemGrid.Focus();
				return;
			}

			if ((TopGridRow == PrevVisibleTopGridRow) && (sender.ToString() != "GridCellChanged"))
				{DisableGridScroll = false;
				return;}
			int numRowsVisible = this.ItemGrid.VisibleRowCount;
			TreeNode WalkingNode = this.CategoryHierTV.Nodes[0];
			if (TopGridRow < PrevVisibleTopGridRow)
				{moveCounter = TopGridRow;}
			else
				{moveCounter = TopGridRow + numRowsVisible;}

			while (moveCounter > 0)
			{
				if (WalkingNode.NextVisibleNode == null)
					{}
				else
					{WalkingNode = WalkingNode.NextVisibleNode;}
				moveCounter--;
			}
			WalkingNode.EnsureVisible();
			AdjustCatInvisLabel();

			//  Handle bottom of grid extra line
			if (WalkingNode.NextVisibleNode == null)
			{
				this.ItemGrid.Focus();
				DataGridCell SaveCell = this.ItemGrid.CurrentCell;
				this.ItemGrid.CurrentCell = new DataGridCell(TopGridRow - 2, 2);
				this.ItemGrid.Refresh();
				Application.DoEvents();
				if (SaveCell.RowNumber > TopGridRow - 2)
				{this.ItemGrid.CurrentCell = SaveCell;}
				TopGridRow = TopGridRow - 2;
			}
			PrevVisibleTopGridRow = TopGridRow;
			DisableGridScroll = false;
		}

		private void AdjustCatInvisLabel()
		{
			TreeNode TopCatVis = this.CategoryHierTV.TopNode;
			if (TopCatVis.Text == "-")
			{
				this.catInvisLabel.Text = TopCatVis.Parent.Text;
				this.catInvisLabel.Visible = true;
			}
			else
				{this.catInvisLabel.Visible = false;}
		}

		private void ItemGrid_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (!e.Control)
				{return;}
			this.Cursor = Cursors.Hand;
		}

		private void ItemGrid_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
			{this.Cursor = Cursors.Default;}

		private void ItemGrid_sortClick (object sender, System.ComponentModel.ListChangedEventArgs e)
		{
			if (clickedRow == -1)
			{
				ItemGrid.ReadOnly = true;
				this.picLockedBox.Visible = true;
				if (clickedColumn == 0)
					{checkForGridAccess = true;
					this.ViewFormTimer.Enabled = true;}
			}
		}

		#endregion

		#region ItemGrid Deletions

		private void ItemGridData_RowDeleting(object sender, DataRowChangeEventArgs e )
		{
			this.CategoryHierTV.BeginUpdate();
			DeletedRows.Add(e.Row["ItemKey"]);
			PendingDeletions = true;
			this.ViewFormTimer.Enabled = true;
			this.CategoryHierTV.EndUpdate();
		}

		private void DelayedRowDelete(int RowNumber)
		{
			// Called by the timer routine to actually delete rows
			TreeNode NodeToUse = CategoryHierTV.Nodes[0];
			for (int i = 0; i < RowNumber; i++)
				{NodeToUse = NodeToUse.NextVisibleNode;}
			if (NodeToUse.Text != "-")
				{return;}

			if (!AlreadyAskedOnDiscard)
			{
				DeleteCancelled = false;
				ItemDelete GetDeleteInfo = new ItemDelete();
				AlreadyAskedOnDiscard = true;
				if (GetDeleteInfo.ShowDialog(this) == DialogResult.OK)
				{
					if (GetDeleteInfo.btnDeleteFromCat.Checked)
						{Console.WriteLine("Delete from Category selected");
						DeleteOptionMode = "Delete";}
					else
						{Console.WriteLine("Discard Completely selected");
						DeleteOptionMode = "Discard";}
				}
				else
					{DeleteCancelled = true;}
				GetDeleteInfo.Dispose();
			}

			if (DeleteCancelled)
				{return;}
			if (DeleteOptionMode == "Delete")
			{
				//	Routine to only remove from categories
				string ItemRemovedKey = NodeToUse.Tag.ToString();
				TreeNode SaveNodeInfo = (TreeNode) NodeToUse.Clone();
				if (!ShowDone)
					{AddToDoneReplay("Remove", null, NodeToUse.FullPath, NodeToUse, null);}
				NodeToUse.Remove();

				// if Item isn't assigned anywhere else, add it to the TrashCan category
				if (!LookForItem(this.CategoryHierTV.Nodes[0].Nodes, ItemRemovedKey, false, false, false))
				{
					FindNodeInTV(TrashCanNode, "", false, "").Nodes.Add(SaveNodeInfo);
					if (!ShowDone)
						{AddToDoneReplay("Add", (TreeNode) SaveNodeInfo.Clone(), TrashCanNode,
						 FindNodeInTV(TrashCanNode, "", false, ""), null);}
				}
			}
			else
			{
				// Completely discard items from database
                DiscardItem(NodeToUse);
			}
		}

        private void DiscardItem(TreeNode NodeToUse)
        {
            string dBitemNum = NodeToUse.Tag.ToString().Substring(2);
            LookForItem(this.CategoryHierTV.Nodes[0].Nodes, NodeToUse.Tag.ToString(),
                true, false, false);
            DataRow[] dBitemRetrievedSet = this.ItemsMemImage.Tables[0].Select("ItemKey = " + dBitemNum);
            if (dBitemRetrievedSet.Length > 0)
            {
                DataRow dBitemRetrieved = dBitemRetrievedSet[0];
                dBitemRetrieved.Delete();
            }

            // If item previously was max Item, and new dummy row
            if (Convert.ToInt16(dBitemNum) + 1 == nextItemkey[0])
            {
                oleorGentaDbConx.Open();
                string AddDummyRow = "INSERT INTO Items(Recurrence, TextItem) VALUES ('', 'Row Placeholder')";
                using (OleDbCommand KeyCmdBuilder = new OleDbCommand(AddDummyRow, this.oleorGentaDbConx))
                {
                    try
                    {
                        KeyCmdBuilder.ExecuteNonQuery();
                        nextItemkey[0]++;
                    }
                    catch { }
                }
                oleorGentaDbConx.Close();
            }
        }

		private bool LookForItem(TreeNodeCollection NodesToSearch, string ItemToFind, bool DiscardMatch, bool SelectWhenFound, bool NextOccurence)
		{
			bool FoundInChild = false;
			foreach (TreeNode ExamineNode in NodesToSearch)
			{if (ExamineNode.Text == "-")
				{if (ExamineNode.Tag.ToString() == ItemToFind)
					{if ((NextOccurence) && (NextOccurMatches.IndexOf(ExamineNode.FullPath) >= 0))
						{continue;}
					if (DiscardMatch)
						{if ((!ShowDone) && (!RebuildingMirrorTrees))
							{AddToDoneReplay("Remove", null, ExamineNode.FullPath, ExamineNode, null);}
						ExamineNode.Remove();
					}
					if (SelectWhenFound)
						{CategoryHierTV.SelectedNode = ExamineNode;}
					return true;}
				}
				else
				{if (ExamineNode.Text == "Add Category")
					{continue;}

				// Node corresponds to a category; call myself recursively
				if(LookForItem(ExamineNode.Nodes, ItemToFind, DiscardMatch, SelectWhenFound, NextOccurence))
				{
					FoundInChild = true;
					if (SelectWhenFound)
						{break;}}
				}
			}
			if (FoundInChild)
				{return true;}
			else
				{return false;}
		}

        #endregion

		#region Soft Assignment Routines
		private void SoftAssign()
		{
			string WorkingText = TextToSoftAssign.ToLower();
			int KeyToMatch = nextItemkey[0] - 1;

			// Sort the text string to avoid matching dupes
			string[] AssignWords = WorkingText.Split(PunctuationArray);
			ArrayList SortedWords = new ArrayList();
			SortedWords.AddRange(AssignWords);
			SortedWords.Sort();
			int numAssignWords = AssignWords.GetUpperBound(0);
			WorkingText = "";
			for (int i = 0; i <= numAssignWords; i++)
			{WorkingText += SortedWords[i].ToString() + " ";}
			SoftAssignDrill(WorkingText, this.CategoryHierTV.Nodes, KeyToMatch);
			SoftAssignPending = true;
		}

		private void SoftAssignDrill(string WorkingText, TreeNodeCollection NodesToSearch, int KeyToMatch)
		{
			string[] AssignWords = WorkingText.Split(PunctuationArray);
			int numAssignWords = AssignWords.GetUpperBound(0);
			foreach (TreeNode CategoryNode in NodesToSearch)
			{
				for (int i = 0; i <= numAssignWords; i++)
				{
					if ((i == 0) && (CategoryNode.Text == "-"))
						{break;}
					if ((i == 0) && (AlreadyAssigned(CategoryNode.Nodes, KeyToMatch)))
						//	Avoid "soft" assigning if already assigned "hard"
						{break;}
					if ((i > 0) && ((AssignWords[i]) == AssignWords[i - 1]))
						{}
					else
					{
						if (CategoryNode.Text.ToLower() == (AssignWords[i]))
						{
							Console.WriteLine("Soft assigning '{0}' to {1}", WorkingText, CategoryNode.Text);
							TreeNode newAddCatNode = new TreeNode("-");
							newAddCatNode.Tag = "i-" + KeyToMatch.ToString();

							// Add a hashtable to hold info to array used after the thread finishes
							Hashtable HoldHashTable = new Hashtable();
							HoldHashTable.Add("ParentNode", (object) CategoryNode);
							HoldHashTable.Add("NewNode", (object) newAddCatNode);
							SoftAssignedNodes.Add((object) HoldHashTable);
						}
					}
				}
				SoftAssignDrill(WorkingText, CategoryNode.Nodes, KeyToMatch);
			}
			AssignWords = null;
		}

		private bool AlreadyAssigned(TreeNodeCollection NodeToCheck, int KeyToMatch)
		{
			// See if item already exists in node collection
			foreach (TreeNode CheckNode in NodeToCheck)
			{
				try
				{
					string tagItemNum = CheckNode.Tag.ToString();
					if (tagItemNum == "i-" + KeyToMatch.ToString())
						{return true;}
				}
				catch {}
			}
			return false;
		}

		private void AddSoftAssignedNodes(ArrayList NodesToAssign)
		{
            foreach (object SoftAssignment in NodesToAssign)
			    {TreeNode NewNode = AddAnewNode(SoftAssignment);}
            NodesToAssign.Clear();
		}

		private void ScanForCategoryNodes()
		{
			string likeCriteria = "'*" + CategoryToScanText + "*'";
			DataRow[] dBitemRetrievedSet = this.ItemsMemImage.Tables[0].Select("TextItem LIKE " + likeCriteria);
			if (dBitemRetrievedSet.Length > 0)
			{
				foreach (DataRow dBitemRetrieved in dBitemRetrievedSet)
				{
                    TreeNode newAddCatNode = BuildaNewTreeNode(dBitemRetrieved);
					// Add a hashtable to hold info to array used after the thread finishes
					Hashtable HoldHashTable = new Hashtable();
					HoldHashTable.Add("ParentNode", (object) CategoryToScan);
					HoldHashTable.Add("NewNode", (object) newAddCatNode);
					CategoryAssignedNodes.Add((object) HoldHashTable);
				}
				CategoryAssignPending = true;
			}
		}
		#endregion

		#region Recurrence Processing
		private object CheckRecurrence(string InputItem)
		{
			if (ThisLanguage != "eng")
				{return DBNull.Value;}

			string[] AssignWords = InputItem.Split(PunctuationArray);
			int numAssignWords = AssignWords.GetUpperBound(0);
			string ThisWord = "";
			int everyLoc = -1;
			for (int i = 0; i <= numAssignWords; i++)
			{
				ThisWord = AssignWords[i];
				if (ThisWord.ToLower() == "every")
					{everyLoc = i;
					break;}
			}
			if (everyLoc == -1)
				{return DBNull.Value;}

			// See if the word after "every" is a recognized Day name
			try
			{
				if (DayOfWeekList.IndexOf(AssignWords[everyLoc + 1]) >= 0)
				{return "Every " + AssignWords[everyLoc + 1];}
			}
			catch {}

			// Maybe it's "every other" and a recognized Day name
			try
			{
				if ((AssignWords[everyLoc + 1].ToLower() == "other") &&
					(DayOfWeekList.IndexOf(AssignWords[everyLoc + 2]) >= 0))
					{return "Every other " + AssignWords[everyLoc + 2];}
			}
			catch {}

			// How about "the first Saturday of every month"
			try
			{
				if ((AssignWords[everyLoc + 1].ToLower() == "month") &&
					(DayOfWeekList.IndexOf(AssignWords[everyLoc - 2]) >= 0))
					{return AssignWords[everyLoc - 3] + " " + AssignWords[everyLoc - 2] + " of every month";}
			}
			catch {}

			// Maybe "the first Saturday of every other month"
			try
			{
				if ((AssignWords[everyLoc + 1].ToLower() == "other") &&
					(AssignWords[everyLoc + 2].ToLower() == "month") && 
					(DayOfWeekList.IndexOf(AssignWords[everyLoc - 2]) >= 0))
					{return AssignWords[everyLoc - 3] + " " + AssignWords[everyLoc - 2] + " of every other month";}
			}
			catch {}

			// It could be the "5th of every month"
			try
			{
				if ((AssignWords[everyLoc + 1].ToLower() == "month") &&
					(AssignWords[everyLoc - 1].ToLower() == "of"))
					{return AssignWords[everyLoc - 2] + " of every month";}
			}
			catch {}
			return DBNull.Value;
		}

		private object SpecializedWhenDate(string recurrenceText, object ComputedWhenDate, bool recurAutoAdd)
		{
			// Returns the next (upcoming) date of a recurring item date
			try
			{
				int numOfWordsInRecur = recurrenceText.Split().GetUpperBound(0);
				switch (numOfWordsInRecur)
				{
					case 1: // every Saturday
						return ComputedWhenDate;
					case 2: // every other Saturday
						if (recurAutoAdd)
							{return Convert.ToDateTime(ComputedWhenDate).AddDays(7);}
						else
							{return ComputedWhenDate;}
					case 3: // 5th of every month
						DateTime CompareDate = Convert.ToDateTime(CheckForTwoWordDate("Jan " + recurrenceText.Substring(0, recurrenceText.IndexOf(" "))));
						while (CompareDate < TodaysDate)
							{CompareDate = CompareDate.AddMonths(1);}
						return CompareDate;
					case 4: // third Saturday of every month
						return DBNull.Value;
					case 5: // first Saturday of every other month
						return DBNull.Value;
				}
			}
			catch {}
			return DBNull.Value;
		}
		#endregion

		#region When Date Computations
		private object FigureOutWhenDate(string inpItemText)
		{
			dateInThePast = false;
			object ComputedDate = CheckForExplicitDate(inpItemText);
			if (ComputedDate == DBNull.Value)
				{ComputedDate = CheckForTwoWordDate(inpItemText);}
			if (ComputedDate == DBNull.Value)
				{ComputedDate = CheckForDayOfWeek(inpItemText);}
			if (ComputedDate == DBNull.Value)
				{ComputedDate = CheckForMonthName(inpItemText);}

			// Add a year if the computed date was in the past
			try
			{
				if ((Convert.ToDateTime(ComputedDate) < TodaysDate) && (!dateInThePast))
					{object HoldTheDate = ComputedDate;
					ComputedDate = Convert.ToDateTime(HoldTheDate).AddYears(1);}
			}
			catch {}
			return ComputedDate;
		}

		private object CheckForMonthName(string inpItemText)
		{
			string[] AssignWords = inpItemText.Split(DateParseArray);
			int numAssignWords = AssignWords.GetUpperBound(0);
			string ThisWord = "";
			for (int i = 0; i <= numAssignWords; i++)
			{
				ThisWord = AssignWords[i];
				if (IsAmonthName(ThisWord))
					{return Convert.ToDateTime(CheckForTwoWordDate(ThisWord + " 1"));}
			}
			return DBNull.Value;
		}

		private object CheckForDayOfWeek(string inpItemText)
		{
			// Note: the DayOfWeekList is in the user's native language,
			// however at present we only handle qualifiers in English
			string[] AssignWords = inpItemText.Split(DateParseArray);
			int numAssignWords = AssignWords.GetUpperBound(0);
			string ThisWord = "";
			string PrevWord = "";
			dateInThePast = false;
			string OrdinalList = "first second third fourth last";
			for (int i = 0; i <= numAssignWords; i++)
			{
				ThisWord = AssignWords[i];
				if ((ThisLanguage == "eng") && (i > 0))
					{PrevWord = AssignWords[i - 1].ToLower();}

				// Monday, Tuesday, et cetera
				if (DayOfWeekList.IndexOf(ThisWord) >= 0)
				{
					if (PrevWord == "next")
						{return TodaysDate.AddDays(DayOfWeekList.IndexOf(ThisWord) + 8);}
					else if ((OrdinalList.IndexOf(PrevWord) >= 0) && (numAssignWords > i + 1) && (IsAmonthName(AssignWords[i + 2])))
						{return ComputedOrdinalDOM(PrevWord, ThisWord, AssignWords[i + 2]);}
					else if (PrevWord == "last")
					{
						dateInThePast = true;
						return TodaysDate.AddDays(DayOfWeekList.IndexOf(ThisWord) - 6);
					}
					else
						{return TodaysDate.AddDays(DayOfWeekList.IndexOf(ThisWord) + 1);}
				}

				// Today, yesterday, and tomorow
				else if ((ThisLanguage == "eng") && (ThisWord.ToLower() == "tomorrow"))
				{
					if (PrevWord == "after")
						{return TodaysDate.AddDays(2);}
					else
						{return TodaysDate.AddDays(1);}
				}
				else if ((ThisLanguage == "eng") && (ThisWord.ToLower() == "yesterday"))
				{
					dateInThePast = true;
					if (PrevWord == "before")
						{return TodaysDate.AddDays(-2);}
					else
						{return TodaysDate.AddDays(-1);}
				}
				else if ((ThisLanguage == "eng") && (ThisWord.ToLower() == "today"))
					{return TodaysDate;}
			}
			return DBNull.Value;
		}

		private DateTime ComputedOrdinalDOM(string OrdinalSeq, string MatchDayofWeek, string MonthName)
		{
			// Conversion for dates entered as "first Tuesday of July"
			int counterOfDayMatches = 0;
			DateTime StartingDate = Convert.ToDateTime(CheckForTwoWordDate(MonthName + "1"));
			if (StartingDate < TodaysDate)
				{StartingDate = StartingDate.AddYears(1);}
			int TargetedMonthNum = StartingDate.Month;
			StartingDate = StartingDate.AddDays(-1);
			DateTime LastMatchedDOW = StartingDate;
			//	Loop through the month accumulating matching day names
			while (true)
			{
				StartingDate = StartingDate.AddDays(1);
				if (StartingDate.Month != TargetedMonthNum)
					{return LastMatchedDOW;}
				if (StartingDate.DayOfWeek.ToString().ToLower() == MatchDayofWeek.ToLower())
				{
					counterOfDayMatches++;
					LastMatchedDOW = StartingDate;
					if ((counterOfDayMatches == 1) && (OrdinalSeq == "first"))
						{return LastMatchedDOW;}
					else if ((counterOfDayMatches == 2) && (OrdinalSeq == "second"))
						{return LastMatchedDOW;}
					else if ((counterOfDayMatches == 3) && (OrdinalSeq == "third"))
						{return LastMatchedDOW;}
					else if ((counterOfDayMatches == 4) && (OrdinalSeq == "fourth"))
						{return LastMatchedDOW;}
				}
			}
		}

		private bool IsAmonthName(string TrialMonthName)
		{
			if (CheckForTwoWordDate(TrialMonthName + " 1") != DBNull.Value)
				{return true;}
			else
				{return false;}
		}

		private object CheckForExplicitDate(string inpItemText)
		{
			string[] AssignWords = inpItemText.Split(DateParseArray);
			int numAssignWords = AssignWords.GetUpperBound(0);
			string ThisWord = "";
			for (int i = 0; i <= numAssignWords; i++)
			{
				ThisWord = AssignWords[i];
				if (ThisWord.IndexOf(":") < 0)
				{
					try
						{return Convert.ToDateTime(ThisWord);}
					catch {}
				}
			}
			return DBNull.Value;
		}

		private object CheckForTwoWordDate(string inpItemText)
		{
			string[] AssignWords = inpItemText.Split(DateParseArray);
			int numAssignWords = AssignWords.GetUpperBound(0);
			string ThisWord = "";
			string SecondWordPiece = "";
			string suffixOnNum = "";
			//	Note: suffixes are just for English
			string suffixList = "st rd th";
			int numCheck = 0;
			for (int i = 0; i <= numAssignWords - 1; i++)
			{
				SecondWordPiece = AssignWords[i + 1];
				if (ThisLanguage == "eng")
				{
					try
					{
						numCheck = Convert.ToInt16(SecondWordPiece.Substring(0,1));
						if (numCheck > 0)
						{
							suffixOnNum = SecondWordPiece.Substring(SecondWordPiece.Length - 2, 2);
							if (suffixList.IndexOf(suffixOnNum) >= 0)
							{
								string SaveWord = SecondWordPiece;
								SecondWordPiece = SaveWord.Substring(0, SecondWordPiece.Length - 2);
							}
						}
					}
					catch {}
				}
				ThisWord = AssignWords[i] + " " + SecondWordPiece;

				try
					{return Convert.ToDateTime(ThisWord);}
				catch {}
			}
			return DBNull.Value;
		}
		#endregion

		#region Grid Building
		private void BuildGrid()
		{
			if (collapsingFlag)
				{return;}
			int PrevNodeCount = RowXrefArray.Count;
			if (PrevNodeCount > 100)
				{this.Cursor = Cursors.WaitCursor;}
			GridRebuilding = true;
			clickedRow = -9;
			iGridDataSource.Clear();

			DataRowCollection ItemRows = this.iGridDataSource.Tables[0].Rows;
			GridRowCounter = 0;
			RowXrefArray.Clear();
			ItemIDs.Clear();
			foreach (TreeNode CategoryNode in this.CategoryHierTV.Nodes[0].Nodes)
				{AddCatRowToGrid(CategoryNode, ItemRows);}
			if (!this.CategoryHierTV.Nodes[0].IsVisible)
				{SyncTheGrid();}
			GridRebuilding = false;
			GridInitialized = true;
			this.Cursor = Cursors.Default;
		}

		private void SyncTheGrid()
		{
			DisableGridScroll = true;
			int RowToStartAt = RowXrefArray.IndexOf(CategoryHierTV.TopNode.FullPath) + 1;
			if (CategoryHierTV.TopNode.Text == "-")
				{RowToStartAt += CategoryHierTV.TopNode.Index;}
			this.ItemGrid.CurrentCell = new DataGridCell(RowXrefArray.Count, 2);
			this.ItemGrid.CurrentCell = new DataGridCell(RowToStartAt, 2);
			PrevVisibleTopGridRow = this.ItemGrid.HitTest(100,20).Row;
			DisableGridScroll = false;
		}

		private void AddCatRowToGrid(TreeNode NodeWithData, DataRowCollection RowStorage)
		{
			GridRowCounter++;
			InitializeItemFields(ItemFields);
			RowXrefArray.Add(NodeWithData.FullPath);
			DataRow dBitemRetrieved = this.ItemsMemImage.Tables[0].Rows[0];
			//	Leftmost element is always just the row number
			ItemFields[0] = Convert.ToInt32(GridRowCounter);

			// ArrayList ItemIDs is used for calls to advanced printing
			if (NodeWithData.Text == "Add Category")
				{ItemFields[2] = "";
				ItemIDs.Add("");}
			else if (NodeWithData.Text == "-")
			{
				//	Item number is stored in the Tag
				string dBitemNum = NodeWithData.Tag.ToString().Substring(2);
				ItemIDs.Add(dBitemNum);
				DataRow[] dBitemRetrievedSet = this.ItemsMemImage.Tables[0].Select("ItemKey = " + dBitemNum);
				if (dBitemRetrievedSet.Length == 0)
					{ItemFields[2] = "(dB lookup failed)";}
				else
				{
					dBitemRetrieved = dBitemRetrievedSet[0];
					ItemFields[2] = dBitemRetrieved["TextItem"];
					if (ItemFields[2].ToString() == "")
						{ItemFields[2] = "(dB lookup failed)";}
					SetGridRowColumns(ItemFields, dBitemRetrieved);
				}
			}
			else if (NodeWithData.Text == "TrashCan")
				{ItemFields[2] = "";
				ItemIDs.Add("");}
			else
				{ItemFields[2] = NewItemClickMsg;
				ItemIDs.Add("");}
			RowStorage.Add(ItemFields);

			if (NodeWithData.IsExpanded)
				{foreach (TreeNode ChildNode in NodeWithData.Nodes)
					{AddCatRowToGrid(ChildNode, RowStorage);}
			 }
		}

		private void InitializeItemFields(object[] ItemFields)
		{
			ItemFields[1] = Convert.ToBoolean(false);
			ItemFields[2] = "";
			ItemFields[3] = DBNull.Value;
            NullFields4through11();
			ItemFields[12] = DBNull.Value;
			ItemFields[13] = DBNull.Value;
			ItemFields[14] = DBNull.Value;
		}

		private void SetGridRowColumns(object[] ItemFields, DataRow dBitemRetrieved)
		{
			try
				{ItemFields[3] = Convert.ToDateTime(dBitemRetrieved["WhenDate"]);}
			catch
				{ItemFields[3] = DBNull.Value;}
			try
				{ItemFields[1] = Convert.ToBoolean(dBitemRetrieved["HasNote"]);}
			catch
				{ItemFields[1] = Convert.ToBoolean(false);}
			try
				{ItemFields[4] = Convert.ToBoolean(dBitemRetrieved["Done"]);}
			catch
				{ItemFields[4] = Convert.ToBoolean(false);}
			try
				{ItemFields[5] = Convert.ToDateTime(dBitemRetrieved["DoneDate"]);}
			catch
				{ItemFields[5] = DBNull.Value;}
			try
				{ItemFields[6] = Convert.ToInt16(dBitemRetrieved["Priority"]);}
			catch
				{ItemFields[6] = DBNull.Value;}
			try
				{ItemFields[7] = dBitemRetrieved["WorkPhone"].ToString();}
			catch
				{ItemFields[7] = DBNull.Value;}
			try
				{ItemFields[8] = dBitemRetrieved["CellPhone"].ToString();}
			catch
				{ItemFields[8] = DBNull.Value;}
			try
				{ItemFields[9] = dBitemRetrieved["HomePhone"].ToString();}
			catch
				{ItemFields[9] = DBNull.Value;}
			try
				{ItemFields[10] = dBitemRetrieved["eMail"].ToString();}
			catch
				{ItemFields[10] = DBNull.Value;}
			try
				{ItemFields[11] = dBitemRetrieved["Address"].ToString();}
			catch
				{ItemFields[11] = DBNull.Value;}
			try
				{ItemFields[12] = Convert.ToDateTime(dBitemRetrieved["EnteredDate"]);}
			catch
				{ItemFields[12] = DBNull.Value;}
			try
				{ItemFields[13] = dBitemRetrieved["Link"].ToString();}
			catch
				{ItemFields[13] = DBNull.Value;}
			try
				{ItemFields[14] = dBitemRetrieved["Recurrence"].ToString();}
			catch
				{ItemFields[14] = DBNull.Value;}
		}
		#endregion

		#region Category Editing and Change Events
		private void CategoryHierTV_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			if ((tvSelectionWaterfall) | (!GridInitialized))
				{return;}
			if (e.Node.Text == "Add Category")
			{
				AddingNewCategory = true;
				AddingNewCategoryAutoEd = true;
				this.ViewFormTimer.Enabled = true;
			}
			else
				{AddingNewCategory = false;}
			SyncTheGrid();
			AdjustCatInvisLabel();
			this.CategoryHierTV.Focus();
		}

		private void CategoryHierTV_AfterExpand(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			if (!StartupRoutines)
				{DelayedBuildGrid = true;
				this.ViewFormTimer.Enabled = true;}
		}

		private void CategoryHierTV_AfterCollapse(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			if (!CollapseWaterfall)
				{BuildGrid();}
		}

		private void CategoryHierTV_AfterLabelEdit(object sender, System.Windows.Forms.NodeLabelEditEventArgs e)
		{
			if ((!ShowDone) && (e.Label != null))
				{AddToDoneReplay("Label", null, e.Node.FullPath, e.Node, e.Label);}
			if (AddingNewCategory)
			{
				AddingNewCategory = false;
				if (e.Label == null)
					{e.CancelEdit = true;}
				else
				{
					e.Node.ForeColor = Color.FromName("WindowText");
					//	Create a new replacement "Add Category" node
					TreeNode newAddCatNode = new TreeNode(orGentaResources.AddCategory);
					newAddCatNode.ForeColor = Color.LightSlateGray;
					CategoryHierTV.SelectedNode.Parent.Nodes.Add(newAddCatNode);
					if (!ShowDone)
					{
						AddToDoneReplay("Add", (TreeNode) newAddCatNode.Clone(), 
							CategoryHierTV.SelectedNode.Parent.FullPath, 
							CategoryHierTV.SelectedNode.Parent, null);}
					CategoryHierTV.SelectedNode.Nodes.Add((TreeNode) newAddCatNode.Clone());
					if (!ShowDone)
					{
						AddToDoneReplay("Add", (TreeNode) newAddCatNode.Clone(), 
							CategoryHierTV.SelectedNode.Parent.FullPath + "\\" + e.Label, CategoryHierTV.SelectedNode, null);}

					// Start up the soft-assign for the newly added Category
					CategoryToScan = e.Node;
					CategoryToScanText = e.Label;
					CategoryScanThread = new System.Threading.Thread(new ThreadStart(ScanForCategoryNodes));
					CategoryScanThread.Name = "CategoryScan";
					CategoryScanThread.Priority = System.Threading.ThreadPriority.BelowNormal;
					CategoryScanThread.IsBackground = false;
					Application.DoEvents();
					CategoryScanThread.Start();
					System.Threading.Thread.Sleep(20);
					ReselectNode = true;
					this.ViewFormTimer.Enabled = true;
				}
			}
		}

		private void CategoryHierTV_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Home)
				{this.catInvisLabel.Visible = false;}
			if (e.KeyCode != Keys.Delete)
				{return;}
			// Eh, no. We don't allow these two nodes to delete
			if (this.CategoryHierTV.SelectedNode.Text == "TrashCan")
				{return;}
			if (this.CategoryHierTV.SelectedNode.Text == "Unassigned")
				{return;}

			// If deleting a leaf, send to itemGrid deletion
			if (this.CategoryHierTV.SelectedNode.Text == "-")
			{
				string ActiveItem = this.CategoryHierTV.SelectedNode.Tag.ToString().Substring(2);
				int tvLoc = ItemIDs.IndexOf(ActiveItem);
				DeletedRows.Add(tvLoc + 1);
				PendingDeletions = true;
				this.ViewFormTimer.Enabled = true;
				return;
			}

			// confirm their intent
			try
			{
				string DeleteMessage = orGentaResources.DeleteCat + this.CategoryHierTV.SelectedNode.Text + "\" ?";
				string ConfirmCaption = orGentaResources.Confirming;
				MessageBoxButtons ConfirmButtons = MessageBoxButtons.OKCancel;
				DialogResult OKtoDeleteCat;
				OKtoDeleteCat = MessageBox.Show(this, DeleteMessage, ConfirmCaption, ConfirmButtons, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
				if(OKtoDeleteCat == DialogResult.Cancel)
					{return;}
			}
			catch
				{return;}

			// Deleting nodes
			Console.WriteLine("Deleting node {0} . . .", this.CategoryHierTV.SelectedNode.Text);
			this.Cursor = Cursors.WaitCursor;
			TreeNode SaveNodeInfo = (TreeNode) this.CategoryHierTV.SelectedNode.Clone();
			if (!ShowDone)
				{AddToDoneReplay("Remove", null, CategoryHierTV.SelectedNode.FullPath, CategoryHierTV.SelectedNode, null);}
			this.CategoryHierTV.SelectedNode.Remove();
			// check if any of the child items should be placed in the trash
			foreach (TreeNode CheckNode in SaveNodeInfo.Nodes)
				{CheckForTrashRescue(CheckNode);}
			this.Cursor = Cursors.Default;
			DelayedBuildGrid = true;
			this.ViewFormTimer.Enabled = true;
		}

		private void CheckForTrashRescue (TreeNode CheckNode)
		{
			// Happens when a parent category of an Item was deleted
			if (CheckNode.Text == "-")
			{
				string ItemRemovedKey = CheckNode.Tag.ToString();
				if (!LookForItem(this.CategoryHierTV.Nodes[0].Nodes, ItemRemovedKey, false, false, false))
				{
					FindNodeInTV(TrashCanNode, "", false, "").Nodes.Add(CheckNode);
					if (!ShowDone)
					{
						AddToDoneReplay("Add", (TreeNode) CheckNode.Clone(), TrashCanNode,
							FindNodeInTV(TrashCanNode, "", false, ""), null);}
				}
			}
			else
			{
				//	child of deleted Category is a sub-category
				foreach (TreeNode CheckKids in CheckNode.Nodes)
					{CheckForTrashRescue(CheckKids);}
			}
		}

		private void CategoryHierTV_Resize(object sender, System.EventArgs e)
		{
			this.catInvisLabel.Width = this.CategoryHierTV.Width - 21;
			this.pnlCatFind.Width = this.CategoryHierTV.Width - 21;
			this.txtCatSearch.Width = this.pnlCatFind.Width - 15;
		}
		#endregion

		#region Category Drag and Drop
		private void CategoryHierTV_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e)
			{CategoryHierTV.DoDragDrop(e.Item, DragDropEffects.Move);}

		private void CategoryHierTV_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
			{e.Effect = DragDropEffects.Move;}

		private void CategoryHierTV_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			// TODO: what if they drag the TrashCan or Unassigend? Re-establish special nodes?
			if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", true))
			{
				Point dropTarget = CategoryHierTV.PointToClient(new Point(e.X, e.Y));
				TreeNode DestinationNode = CategoryHierTV.GetNodeAt(dropTarget);
				TreeNode SourceNode = (TreeNode) e.Data.GetData("System.Windows.Forms.TreeNode");
				if (DestinationNode.Text == "-")
				{
					//	Dropping onto an item, place the source after it
					int PlaceToInsert = DestinationNode.Parent.Nodes.IndexOf(DestinationNode) + 1;
					DestinationNode.Parent.Nodes.Insert(PlaceToInsert, (TreeNode) SourceNode.Clone());
					if (!ShowDone)
					{
						AddToDoneReplay("Insert " + PlaceToInsert.ToString(), (TreeNode)
							SourceNode.Clone(), DestinationNode.Parent.FullPath, DestinationNode.Parent, null);}
				}
				else
				{
					//	Dropping onto a category, just add to end of category
					DestinationNode.Nodes.Add((TreeNode) SourceNode.Clone());
					if (!ShowDone)
						{AddToDoneReplay("Add", (TreeNode) SourceNode.Clone(), DestinationNode.FullPath, DestinationNode, null);}
				}

				// Remove original location from mirror and active treeviews
				if (!ShowDone)
					{AddToDoneReplay("Remove", null, SourceNode.FullPath, SourceNode, null);}
				SourceNode.Remove();
				DelayedBuildGrid = true;
				this.ViewFormTimer.Enabled = true;
			}
		}

		private void CategoryHierTV_DragOver(object sender, System.Windows.Forms.DragEventArgs de)
		{
			Point clientPoint = CategoryHierTV.PointToClient(new Point(de.X, de.Y));
			TreeNode OverNode = CategoryHierTV.GetNodeAt(clientPoint);
			tvSelectionWaterfall = true;
			CategoryHierTV.SelectedNode = OverNode;
			Application.DoEvents();
			tvSelectionWaterfall = false;
		}
		#endregion

		#region Adding, Changing, and Removing Notes

		private void ItemGrid_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			clickedColumn = this.ItemGrid.HitTest(e.X, e.Y).Column;
			clickedRow = this.ItemGrid.HitTest(e.X, e.Y).Row;
			if ((clickedColumn == 4) && (clickedRow != -1))
				{UpdateDoneFields();}
			if ((clickedColumn == 10) && (clickedRow != -1) && (this.Cursor == Cursors.Hand))
			{
				this.Cursor = Cursors.Default;
				string SendToAddress = this.ItemGrid[clickedRow, clickedColumn].ToString();
				string RegMailMessage = "mailto:" + SendToAddress;
				System.Diagnostics.Process.Start(RegMailMessage);
			}

			// They clicked on a Web address
			if ((clickedColumn == 11) && (clickedRow != -1) && (this.Cursor == Cursors.Hand))
			{
				this.Cursor = Cursors.Default;
				string WebGotoAddress = this.ItemGrid[clickedRow, clickedColumn].ToString();
				if (WebGotoAddress.IndexOf("://") < 0)
					{WebGotoAddress = "http://" + WebGotoAddress;}
				try
					{System.Diagnostics.Process.Start(WebGotoAddress);}
				catch {}
			}

			// They clicked the Note checkbox
			if ((clickedColumn == 1) && (clickedRow != -1))
			{
				string ActiveItem = "";
				try
					{ActiveItem = this.CategoryHierTV.SelectedNode.Tag.ToString().Substring(2);}
				catch
					{return;}
				BuildAndShowNote(ActiveItem);
			}
		}

		private void BuildAndShowNote(string ActiveItem)
		{
			string NoteTextToShow = "";
			string EmptyNoteText = orGentaResources.NotePrompt;
			string GetItemCmd = "SELECT NoteText FROM Notes WHERE ItemKey = " + ActiveItem;
			oleorGentaDbConx.Open();
			OleDbCommand NotesCmdBuilder = new OleDbCommand(GetItemCmd, this.oleorGentaDbConx);
			try
			{
				NoteTextToShow = (string) NotesCmdBuilder.ExecuteScalar();
				if (NoteTextToShow == null)
					{NoteTextToShow = EmptyNoteText;}
			}
			catch 
				{NoteTextToShow = EmptyNoteText;}
			finally
				{oleorGentaDbConx.Close();}

			NoteDataEntry GetNoteInfo = new NoteDataEntry();
			GetNoteInfo.NoteText.Text = NoteTextToShow;
			if (NoteTextToShow != EmptyNoteText)
				{GetNoteInfo.NoteText.SelectionStart = NoteTextToShow.Length;}
			else
				{GetNoteInfo.NoteText.SelectionStart = 0;
				GetNoteInfo.NoteText.SelectionLength = NoteTextToShow.Length;}

			if (GetNoteInfo.ShowDialog(this) == DialogResult.OK)
				{SaveTheNote(GetNoteInfo.NoteText.Text, NotesCmdBuilder, ActiveItem, NoteTextToShow);}
			else
			{
				// Client clicked the Cancel on Note Entry dialog
				DataRow[] dBitemRetrievedSet = this.ItemsMemImage.Tables[0].Select("ItemKey = " + ActiveItem);
				DataRow dBitemRetrieved = dBitemRetrievedSet[0];
				if (NoteTextToShow != EmptyNoteText)
					{dBitemRetrieved["HasNote"] = true;}
				else
					{dBitemRetrieved["HasNote"] = false;}
				int GridRowLoc = this.ItemGrid.CurrentCell.RowNumber;
				int BackingRow = Convert.ToInt32(this.ItemGrid[GridRowLoc, 0]) - 1;
				this.ItemGrid[BackingRow, 1] = dBitemRetrieved["HasNote"];
			}

			NotesCmdBuilder.Dispose();
			GetNoteInfo.Dispose();
		}

		private void SaveTheNote(string noteText, OleDbCommand NotesCmdBuilder, string ActiveItem, string NoteTextToShow)
		{
			string NoteReturned = noteText;
			string EmptyNoteText = "Enter Note text here . . .";
			if (NoteReturned == "")
			{
				string DelNoteCmd = "DELETE FROM Notes WHERE ItemKey = " + ActiveItem;
				NotesCmdBuilder.CommandText = DelNoteCmd;
				try
				{
					oleorGentaDbConx.Open();
					NotesCmdBuilder.ExecuteNonQuery();
					DataRow[] dBitemRetrievedSet = this.ItemsMemImage.Tables[0].Select("ItemKey = " + ActiveItem);
					if (dBitemRetrievedSet.Length > 0)
					{
						DataRow dBitemRetrieved = dBitemRetrievedSet[0];
						dBitemRetrieved["HasNote"] = false;
						int GridRowLoc = this.ItemGrid.CurrentCell.RowNumber;
						int BackingRow = Convert.ToInt32(this.ItemGrid[GridRowLoc, 0]) - 1;
						this.ItemGrid[BackingRow, 1] = false;
					}
				}
				catch {}
				finally
					{oleorGentaDbConx.Close();}
			}
			else if (NoteReturned != NoteTextToShow)
			{
				string AddOrChgNoteCmd = "";
				if (NoteTextToShow == EmptyNoteText)
					{AddOrChgNoteCmd = "INSERT INTO Notes(ItemKey, NoteText) VALUES ("
						+ ActiveItem + ", '" + NoteReturned.Replace("'","''") + "')";}
				else
					{AddOrChgNoteCmd = "UPDATE Notes SET NoteText = '" + 
						NoteReturned.Replace("'","''") + "' WHERE ItemKey = " + ActiveItem;}

				// Update the database
				NotesCmdBuilder.CommandText = AddOrChgNoteCmd;
				try
				{
					oleorGentaDbConx.Open();
					NotesCmdBuilder.ExecuteNonQuery();
                    SetNoteCheckBox(ActiveItem);
				}
				catch {}
				finally
					{oleorGentaDbConx.Close();}
			}
			else if (NoteReturned != EmptyNoteText)
			{
				// Note wasn't changed but still has stuff in it
                SetNoteCheckBox(ActiveItem);
			}
		}

		#endregion

		#region Item Marked as Done
		private void UpdateDoneFields()
		{
			if (this.CategoryHierTV.SelectedNode.Text == "Main")
				{return;}
			string dBitemNum = this.CategoryHierTV.SelectedNode.Tag.ToString().Substring(2);
			DataRow[] dBitemRetrievedSet = this.ItemsMemImage.Tables[0].Select("ItemKey = " + dBitemNum);
			if (dBitemRetrievedSet.Length > 0)
			{
				DataRow dBitemRetrieved = dBitemRetrievedSet[0];
				if (Convert.ToBoolean(dBitemRetrieved["Done"]) == false)
					{dBitemRetrieved["Done"] = true;
					dBitemRetrieved["DoneDate"] = TodaysDate;}
				else
					{dBitemRetrieved["Done"] = false;
					dBitemRetrieved["DoneDate"] = DBNull.Value;}
				int GridRowLoc = this.ItemGrid.CurrentCell.RowNumber;
				if (dBitemRetrieved["Recurrence"] != DBNull.Value)
				{
					this.ItemGrid.CurrentCell = new DataGridCell(GridRowLoc, 2);
					AddItemLeaf(dBitemRetrieved["TextItem"].ToString(), 
						Convert.ToInt16(this.ItemGrid[GridRowLoc, 0]), true);
					return;
				}
				int BackingRow = Convert.ToInt32(this.ItemGrid[GridRowLoc, 0]) - 1;
				this.ItemGrid[BackingRow, 4] = dBitemRetrieved["Done"];
				this.ItemGrid[BackingRow, 5] = dBitemRetrieved["DoneDate"];
			}
		}
		#endregion

		#region Minimal Interface Processing
		private void trayIconTrayed_DoubleClick(object sender, System.EventArgs e)
		{
			GetTextLineForm.Visible = false;
			if (this.WindowState == FormWindowState.Minimized)
				{this.WindowState = FormWindowState.Normal;}
			// Activate the main form.
			this.trayIconTrayed.Visible = false;
			this.Visible = true;
			this.Activate();
			RunningMinimal = false;
			if (sender.ToString().IndexOf("NotifyIcon") >= 0)
				{Cursor.Position = new Point(this.Left + 600, this.Top + 600);}
			else
				{Cursor.Position = new Point(this.Left + 300, this.Top + 300);}
		}

		private void trayIconTrayed_Click(object sender, System.EventArgs e)
		{
			// Ignore single click if form is already shown
			if (GetTextLineForm.Visible == true)
			{
				GetTextLineForm.txtDataEntered.SelectAll();
				GetTextLineForm.Activate();
				GetTextLineForm.txtDataEntered.Focus();
				return;
			}
			Screen [] thisPCscreens = Screen.AllScreens;
			Rectangle ScreenSize = new Rectangle();
			ScreenSize = thisPCscreens[0].WorkingArea;

			// Locate the form at the bottom right of the screen
			GetTextLineForm.Left = ScreenSize.Width - GetTextLineForm.Width;
			GetTextLineForm.Top = ScreenSize.Height - GetTextLineForm.Height;
            string startingPrompt = orGentaResources.TrayedPrompt;
			GetTextLineForm.txtDataEntered.Text = startingPrompt;
			GetTextLineForm.txtDataEntered.SelectAll();
			GetTextLineForm.txtDataEntered.Focus();
			if (GetTextLineForm.ShowDialog(this) == DialogResult.OK)
			{
				//	If user entered only 1 word, then it's a search
				if (GetTextLineForm.txtDataEntered.Text.IndexOf(" ",1) == -1)
				{
					// Search categories first
					this.txtCatSearch.Text = GetTextLineForm.txtDataEntered.Text;
					txtCatSearch_Validating(this.txtCatSearch, null);
					if (this.CategoryHierTV.SelectedNode.Text != "Main")
					{
						trayIconTrayed_DoubleClick(GetTextLineForm.txtDataEntered, null);
						Application.DoEvents();
						this.CategoryHierTV.Focus();
						return;
					}

					// Next search items
					this.txtItemSearch.Text = GetTextLineForm.txtDataEntered.Text;
					txtItemSearch_Validating(this.txtCatSearch, null);
					if (this.txtItemSearch.Text != "")
					{
						trayIconTrayed_DoubleClick(GetTextLineForm.txtDataEntered, null);
						Application.DoEvents();
						return;
					}

					// Lastly search notes
					this.txtNoteSearch.Text = GetTextLineForm.txtDataEntered.Text;
					txtNoteSearch_Validating(this.txtCatSearch, null);
					if (this.txtNoteSearch.Text != "")
					{
						trayIconTrayed_DoubleClick(GetTextLineForm.txtDataEntered, null);
						Application.DoEvents();
						return;
					}
					MessageBox.Show(orGentaResources.NotFound);
				}
				else
				{
					if (GetTextLineForm.txtDataEntered.Text == startingPrompt)
						{return;}

					// New item entered: add to Unassigned
					TreeNode UnassignedParent = FindNodeInTV(UnassignedNode, "", false, "");
					CategoryHierTV.SelectedNode = UnassignedParent;
					CategoryHierTV.SelectedNode.EnsureVisible();
					int UnassignedLoc = RowXrefArray.IndexOf(UnassignedNode) + 1;
					AddItemLeaf(GetTextLineForm.txtDataEntered.Text, UnassignedLoc, false);
				}
			}
			if (GetTextLineForm.txtDataEntered.Text == "")
			{
				//	user pressed the restore button
				trayIconTrayed_DoubleClick(GetTextLineForm.txtDataEntered, null);
				Application.DoEvents();
				return;
			}
		}
		#endregion

		#region Treeview Save Code
		private static int saveTree(TreeView TreeToSave, string SaveFileName)
		{
			ArrayList SaveArrayList = new ArrayList();
			foreach (TreeNode CategoryNode in TreeToSave.Nodes)
				{saveNode(CategoryNode, SaveArrayList);}
			if (File.Exists(SaveFileName))
				{File.Delete(SaveFileName);}
			// TODO: change to rename file to .old, delete .old if save succeeds

			FileStream SaveFile = File.Create(SaveFileName);
			BinaryFormatter SaveSerializer = new BinaryFormatter();
			try
				{SaveSerializer.Serialize(SaveFile, SaveArrayList);}
			catch (System.Runtime.Serialization.SerializationException e)
				{MessageBox.Show("TreeView save serialization failed: {0}", e.Message);
				return -1;}

			SaveFile.Close();
            SaveFile.Dispose();
			SaveArrayList = null;
			return 0;
		}

		private static void saveNode(TreeNode NodeToSave, ArrayList SaveArrayList)
		{
			Hashtable HoldHashTable = new Hashtable();
			HoldHashTable.Add("Tag", NodeToSave.Tag);
			HoldHashTable.Add("Text", (object)NodeToSave.Text);
			HoldHashTable.Add("FullPath", (object)NodeToSave.FullPath);
			HoldHashTable.Add("SelectedImageIndex", (object)NodeToSave.SelectedImageIndex);
			HoldHashTable.Add("ImageIndex", (object)NodeToSave.ImageIndex);
			HoldHashTable.Add("TextColor", (object)NodeToSave.ForeColor);

			SaveArrayList.Add((object)HoldHashTable);
			foreach (TreeNode ChildNode in NodeToSave.Nodes)
				{saveNode(ChildNode, SaveArrayList);}
		}
		#endregion

		#region Coordination between Mirror treeviews
		private void AddToDoneReplay(string TreeVaction, TreeNode ActionSource, 
			string ActionTarget, TreeNode TargetNode, string NewLabel)
		{
			Hashtable HoldHashTable = new Hashtable();
			HoldHashTable.Add("Action", TreeVaction);
			HoldHashTable.Add("Source", ActionSource);
			HoldHashTable.Add("Target", ActionTarget);
			HoldHashTable.Add("TargetNode", TargetNode);
			HoldHashTable.Add("Label", NewLabel);
			DoneReplay.Add((object)HoldHashTable);
		}

		private void ReplayDoneTVupdates()
		{
			string ReplayAction = "";
			TreeNode ReplaySource = new TreeNode();
			string ReplayTarget = "";
			TreeNode ReplayTargetNode = new TreeNode();
			string ReplayLabel = "";
			Hashtable HoldHashTable = new Hashtable();

			this.Cursor = Cursors.WaitCursor;
			this.CategoryHierTV.BeginUpdate();
			foreach (object ThisReplay in DoneReplay)
			{
				ReplayLabel = "";
				HoldHashTable = (Hashtable) ThisReplay;
				ReplayAction = HoldHashTable["Action"].ToString();
				ReplaySource = (TreeNode) HoldHashTable["Source"];
				ReplayTarget = HoldHashTable["Target"].ToString();
				ReplayTargetNode = (TreeNode) HoldHashTable["TargetNode"];
				try
					{ReplayLabel = HoldHashTable["Label"].ToString();}
				catch {}
				DonePlayback(ReplayAction, ReplaySource, ReplayTarget, ReplayTargetNode, ReplayLabel);
			}

			this.Cursor = Cursors.Default;
			this.CategoryHierTV.EndUpdate();
			DoneReplay.Clear();
			this.CategoryHierTV.Refresh();
		}

		private void DonePlayback(string ReplayAction, TreeNode ReplaySource, string ReplayTarget, TreeNode ReplayTargetNode, string ReplayLabel)
		{
			string nodeTag = "";
			if (ReplayTargetNode.Text == "-")
				{nodeTag = ReplayTargetNode.Tag.ToString();}
			TreeNode NodeTarget = FindNodeInTV(ReplayTarget, nodeTag, false, "");
			if (!FoundMatchNode)
			{
				Console.WriteLine("Couldn't find Node {0} {1}", nodeTag, ReplayTarget);
				Console.WriteLine(" during {0} of {1}", ReplayAction, ReplaySource.Text);
				return;
			}

			switch (ReplayAction)
			{
				case "Label":
					NodeTarget.Text = ReplayLabel;
					NodeTarget.ForeColor = Color.FromName("WindowText");
					break;
				case "Color":
					NodeTarget.ForeColor = ReplaySource.ForeColor;
					break;
				case "Add":
					NodeTarget.Nodes.Add(ReplaySource);
					break;
				case "Insert":
					NodeTarget.Nodes.Insert(0, ReplaySource);
					break;
				case "Remove":
					NodeTarget.Remove();
					break;
				default:
					if (ReplayAction.Substring(0,6) == "Insert")
						{int InsertWhere = Convert.ToInt16(ReplayAction.Substring(7));
						NodeTarget.Nodes.Insert(InsertWhere, ReplaySource);}
					else
						{Console.WriteLine("Unrecognized Replay command: {0}, at {1}",
						 ReplayAction, 
						 ReplayTarget);}
					break;
			}
		}

		private TreeNode FindNodeInTV (string PathToFind, string TagToMatch, bool PartialMatch, string PreviousFoundNode)
		{
			FoundMatchNode = false;
			MatchedOnPrevNode = false;
			TreeNode NodeThatMatches = this.CategoryHierTV.Nodes[0];
			foreach (TreeNode MatchNode in this.CategoryHierTV.Nodes)
				{NodeThatMatches = Match1Node (MatchNode, PathToFind, TagToMatch, PartialMatch, PreviousFoundNode);
				if (FoundMatchNode)
					{return NodeThatMatches;}
			}
			return NodeThatMatches;
		}

		private TreeNode Match1Node (TreeNode MatchNode, string PathToFind, string TagToMatch, bool PartialMatch, string PreviousFoundNode)
		{
			if ((MatchNode.FullPath == PathToFind) | ((PartialMatch) && 
				(MatchNode.Text.ToLower().IndexOf(PathToFind.ToLower()) >= 0)))
			{
				if (MatchNode.Text != "-")
					{FoundMatchNode = true;}
				else if (MatchNode.Tag.ToString() == TagToMatch)
					{FoundMatchNode = true;}

				if (FoundMatchNode)
				{
					//	Sometimes this is a "find next" condition ...
					if (PreviousFoundNode == "")
						{return MatchNode;}
					else if (PreviousFoundNode == MatchNode.FullPath)
						{MatchedOnPrevNode = true;
						FoundMatchNode = false;}
					else if (MatchedOnPrevNode)
						{return MatchNode;}
					else
						//	still in process of getting up to the last match
						{FoundMatchNode = false;}
				}
			}

			// Setting to top node indicates no category match yet
			TreeNode NodeThatMatches = this.CategoryHierTV.Nodes[0];

			foreach (TreeNode ChildNode in MatchNode.Nodes)
				{NodeThatMatches = Match1Node (ChildNode, PathToFind, TagToMatch, PartialMatch, PreviousFoundNode);
				if (FoundMatchNode)
					{return NodeThatMatches;}
			}
			return NodeThatMatches;
		}
		#endregion

		#region Code for Loading a saved TreeView
		private static int loadTree(TreeView TreeToLoad, string LoadFileName)
		{
			if (File.Exists(LoadFileName))
			{
                object DeserializedData = null;
                using (Stream LoadFile = File.Open(LoadFileName, FileMode.Open))
                {
                    BinaryFormatter LoadSerializer = new BinaryFormatter();
                    try
                        { DeserializedData = LoadSerializer.Deserialize(LoadFile); }
                    catch (System.Runtime.Serialization.SerializationException e)
                    {
                        MessageBox.Show("De-Serialization failed : {0}", e.Message);
                        return -1;
                    }
                    LoadFile.Close();
                }
				ArrayList LoadArrayList = DeserializedData as ArrayList;
				foreach (object LoadItem in LoadArrayList)
				{
					Hashtable HoldHashTable = LoadItem as Hashtable;
					TreeNode LoadTreeNode = new TreeNode(HoldHashTable["Text"].ToString());
					LoadTreeNode.Tag = HoldHashTable["Tag"];
					LoadTreeNode.ImageIndex = Convert.ToInt32(HoldHashTable["SelectedImageIndex"].ToString());
					LoadTreeNode.SelectedImageIndex = Convert.ToInt32(HoldHashTable["SelectedImageIndex"].ToString());
					LoadTreeNode.ForeColor = (Color) HoldHashTable["TextColor"];
					string TargetLoadPath = HoldHashTable["FullPath"].ToString();
					string[] LoadPathParts = TargetLoadPath.Split(TreeToLoad.PathSeparator.ToCharArray());

					if (LoadPathParts.Length > 1)
					{
						TreeNode parentNode = null;
						TreeNodeCollection EntireTree = TreeToLoad.Nodes;

						//	Find his parent and add the unpacked node
						searchNode(LoadPathParts, ref parentNode, EntireTree);
						if (parentNode != null)
						{
							int lastAdded = parentNode.Nodes.Add(LoadTreeNode);
							if (LoadTreeNode.Text == "TrashCan")
								{TrashCanNode = parentNode.Nodes[lastAdded].FullPath;}
							if (LoadTreeNode.Text == "Unassigned")
								{UnassignedNode = parentNode.Nodes[lastAdded].FullPath;}
						}
					}
					else
					{
						//	no explicit category path, add at the end
						int lastAdded = TreeToLoad.Nodes.Add(LoadTreeNode);
						if (LoadTreeNode.Text == "TrashCan")
							{TrashCanNode = TreeToLoad.Nodes[lastAdded].FullPath;}
						if (LoadTreeNode.Text == "Unassigned")
							{UnassignedNode = TreeToLoad.Nodes[lastAdded].FullPath;}
					}
				}
				return 0;
			}
			else return -2;
		}

		private static void searchNode(string[] LoadPathParts, ref TreeNode parentNode, TreeNodeCollection EntireTree)
		{
			foreach (TreeNode ExaminingNode in EntireTree)
			{
				if (ExaminingNode.Text.Equals(LoadPathParts[LoadPathParts.Length - 2].ToString()))
					{parentNode = ExaminingNode;
					return;}
				else searchNode(LoadPathParts, ref parentNode, ExaminingNode.Nodes);}
		}
		#endregion

		#region Print Routines
		private void menuAdvPrint_Click(object sender, System.EventArgs e)
		{
			if (!SoftwareIsRegistered)
				{MessageBox.Show(this, orGentaResources.RegistrationRequired);
				return;}
            FieldSelector GetFields = BuildFieldSelector(); 
            GetFields.chkBoxFields.SetItemCheckState(3, CheckState.Checked);

			AdvancedPrint AdvPrintDialog = new AdvancedPrint(GetFields, this.ItemGrid, 
				RowXrefArray, this.dataGridTableStyle1, oleorGentaDbConx, ItemIDs);

			//	Reference and copy the Advanced Print form printer settings
			System.Drawing.Printing.PrinterSettings ccPrt = AdvPrintDialog.prtGridPrintDoc.PrinterSettings;
			ccPrt.Collate = this.prtGridPrintDoc.PrinterSettings.Collate;
			ccPrt.Copies = this.prtGridPrintDoc.PrinterSettings.Copies;
			ccPrt.Duplex = this.prtGridPrintDoc.PrinterSettings.Duplex;
			ccPrt.PrinterName = this.prtGridPrintDoc.PrinterSettings.PrinterName;

			System.Drawing.Printing.PageSettings ccDfpg = AdvPrintDialog.prtGridPrintDoc.DefaultPageSettings;
			ccDfpg.Color = this.prtGridPrintDoc.DefaultPageSettings.Color;
			ccDfpg.Landscape = this.prtGridPrintDoc.DefaultPageSettings.Landscape;
			ccDfpg.Margins = this.prtGridPrintDoc.DefaultPageSettings.Margins;
			ccDfpg.PaperSize = this.prtGridPrintDoc.DefaultPageSettings.PaperSize;
			ccDfpg.PaperSource = this.prtGridPrintDoc.DefaultPageSettings.PaperSource;
			ccDfpg.PrinterResolution = this.prtGridPrintDoc.DefaultPageSettings.PrinterResolution;

			AdvPrintDialog.ShowDialog(this);
			GetFields.Dispose();
			AdvPrintDialog.Dispose();
		}

		private void menuPrint_Click(object sender, System.EventArgs e)
		{
			StreamWriter streamToPrint = new StreamWriter(Environment.CurrentDirectory + "\\PrintWrk.tmp");
			DataRowCollection ItemRows = this.iGridDataSource.Tables[0].Rows;
			for (int i = 0; i < RowXrefArray.Count; i++)
			{
				if (RowXrefArray[i].ToString().IndexOf("-") == RowXrefArray[i].ToString().Length - 1)
				{
					object WhenDateForPrt = ItemRows[i].ItemArray[3];
					string WhenDateFormatted = "";
					try
						{WhenDateFormatted = Convert.ToDateTime(WhenDateForPrt).ToShortDateString();}
					catch {}
					streamToPrint.WriteLine(WhenDateFormatted);
					streamToPrint.WriteLine(ItemRows[i].ItemArray[2].ToString());
				}
			}

			streamToPrint.Flush();
			streamToPrint.Close();
            streamToPrint.Dispose();
			print1toAline = false;
			try
				{this.prtOrgentaPreviewDialog.ShowDialog(this);}
			catch 
				{PrintWorkFile.Close();}
		}

		private void prtGridPrintDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs ev)
		{
			float linesPerPage = 0;
			float yPos = 0;
			int count = 0;
			float leftMargin = ev.MarginBounds.Left;
			float topMargin = ev.MarginBounds.Top;
			Font printFont = new Font("Arial", 10);
			string PrintWorkLine = "";

			linesPerPage = ev.MarginBounds.Height / printFont.GetHeight(ev.Graphics);
			StringFormat StringFormatParms = new StringFormat();
			StringFormatParms.FormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.FitBlackBox;
			while ((count < linesPerPage) && ((PrintWorkLine=PrintWorkFile.ReadLine()) != null))
			{
				yPos = topMargin + (count * printFont.GetHeight(ev.Graphics));
				if (print1toAline)
				{
					//	Create the bounding print area rectangle
					RectangleF OutputRectangle = new RectangleF(leftMargin, yPos,
						ev.MarginBounds.Width, ev.MarginBounds.Height - (yPos - 
						ev.MarginBounds.Top));
					ev.Graphics.DrawString (PrintWorkLine, printFont, Brushes.Black, OutputRectangle, StringFormatParms);
				}
				else
				{
					ev.Graphics.DrawString (PrintWorkLine, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
					PrintWorkLine=PrintWorkFile.ReadLine();
					ev.Graphics.DrawString (PrintWorkLine, printFont, Brushes.Black, leftMargin + 100, yPos, new StringFormat());
				}
				count++;
			}

			if (PrintWorkLine != null)
				{ev.HasMorePages = true;}
			else
				{ev.HasMorePages = false;}
		}

		private void menuItemPrint_Click(object sender, System.EventArgs e)
		{
			if (this.CategoryHierTV.SelectedNode.Text != "-")
				{return;}
			StreamWriter streamToPrint = new StreamWriter(Environment.CurrentDirectory + "\\PrintWrk.tmp");
			streamToPrint.WriteLine(this.ItemGrid[this.ItemGrid.CurrentCell.RowNumber,2]);
			streamToPrint.WriteLine("");
			string NoteTextToShow = "";
			string GetItemCmd = "SELECT NoteText FROM Notes WHERE ItemKey = "
				+ this.CategoryHierTV.SelectedNode.Tag.ToString().Substring(2);
			oleorGentaDbConx.Open();
            using (OleDbCommand NotesCmdBuilder = new OleDbCommand(GetItemCmd, this.oleorGentaDbConx))
            {
                try
                {
                    NoteTextToShow = (string)NotesCmdBuilder.ExecuteScalar();
                    if (NoteTextToShow == null)
                        { NoteTextToShow = ""; }
                }
                catch
                    { NoteTextToShow = ""; }
                finally
                    { oleorGentaDbConx.Close(); }
            }

			streamToPrint.WriteLine(NoteTextToShow);
			streamToPrint.Flush();
			streamToPrint.Close();
            streamToPrint.Dispose();
			print1toAline = true;
			try
				{this.prtOrgentaPreviewDialog.ShowDialog(this);}
			catch 
				{PrintWorkFile.Close();}
		}

		private void menuPrintSetup_Click(object sender, System.EventArgs e)
			{this.orGentaPageSetup.ShowDialog(this);}

		private void prtGridPrintDoc_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
			{PrintWorkFile = new StreamReader(Environment.CurrentDirectory + "\\PrintWrk.tmp");}

		private void prtGridPrintDoc_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
			{PrintWorkFile.Close();}
		#endregion

		#region Sort Routines
		private void menuSortCats_Click(object sender, System.EventArgs e)
		{
			if (CategoryHierTV.SelectedNode == null)
				{return;}
			TreeNode sortFather = this.CategoryHierTV.SelectedNode;
			if (sortFather.Text == "-")
				{sortFather = this.CategoryHierTV.SelectedNode.Parent;}
			ArrayList nodesToSort = new ArrayList(sortFather.Nodes.Count);
			foreach (TreeNode childNode in sortFather.Nodes)
			{
				nodesToSort.Add(childNode);
				if (!ShowDone)
					{AddToDoneReplay("Remove", null, childNode.FullPath, childNode, null);}
			}
			sortFather.Nodes.Clear();

			IComparer nodeComparer = new nodeSortCompareClass();
			nodesToSort.Sort(nodeComparer);
			this.CategoryHierTV.BeginUpdate();
			foreach (TreeNode childNode in nodesToSort)
			{
				sortFather.Nodes.Add(childNode);
				if (!ShowDone)
					{AddToDoneReplay("Add", (TreeNode) childNode.Clone(), sortFather.FullPath, sortFather, null);}
			}
			nodeComparer = null;
			this.CategoryHierTV.EndUpdate();
			BuildGrid();
		}

		public class nodeSortCompareClass : IComparer
		{
			int IComparer.Compare( Object x, Object y )
			{
				// Place Add Category node at the bottom, Unassigned node at the top
				int CompareBack = 0;
				TreeNode FirstNode = (TreeNode) x;
				TreeNode SecondNode = (TreeNode) y;
				if (SecondNode.Text == FirstNode.Text)
					{CompareBack = 0;}
				else if (SecondNode.Text == "Add Category")
					{CompareBack = -1;}
				else if (FirstNode.Text == "Add Category")
					{CompareBack = 1;}
				else if (SecondNode.Text == "Unassigned")
					{CompareBack = 1;}
				else if (FirstNode.Text == "Unassigned")
					{CompareBack = -1;}
				else
					{CompareBack = FirstNode.Text.CompareTo(SecondNode.Text);}
				return CompareBack;
			}
		}

		private void menuSortItems_Click(object sender, System.EventArgs e)
		{
			if (CategoryHierTV.SelectedNode == null)
				{return;}
			TreeNode sortFather = this.CategoryHierTV.SelectedNode;
			if (sortFather.Text == "-")
				{sortFather = this.CategoryHierTV.SelectedNode.Parent;}

			ArrayList nodesToSort = new ArrayList();
			foreach (TreeNode childNode in sortFather.Nodes)
			{
				if (childNode.Text == "-")
				{
					Hashtable HoldHashTable = new Hashtable();
					HoldHashTable.Add("TreeNode", (object) childNode);
					int RowToStartAt = RowXrefArray.IndexOf(childNode.FullPath) + 1;
					RowToStartAt += childNode.Index - 1;
					string GridValueAtNode = this.ItemGrid[RowToStartAt,2].ToString();
					HoldHashTable.Add("GridText", (object) GridValueAtNode);
					nodesToSort.Add(HoldHashTable);
					if (!ShowDone)
						{AddToDoneReplay("Remove", null, childNode.FullPath, childNode, null);}
				}
			}

			IComparer nodeComparer = new nodeValueSortCompareClass();
			nodesToSort.Sort(nodeComparer);
			this.CategoryHierTV.BeginUpdate();
			foreach (object SortDataBack in nodesToSort)
			{
				Hashtable HoldHashTable = SortDataBack as Hashtable;
				TreeNode childNode = (TreeNode) HoldHashTable["TreeNode"];
				sortFather.Nodes.Remove(childNode);
				sortFather.Nodes.Insert(0, childNode);
				if (!ShowDone)
					{AddToDoneReplay("Insert", (TreeNode) childNode.Clone(), 
					 sortFather.FullPath, sortFather, null);}
			}
			nodeComparer = null;
			this.CategoryHierTV.EndUpdate();
			BuildGrid();
		}

		public class nodeValueSortCompareClass : IComparer
		{
			int IComparer.Compare( Object x, Object y )
			{
				int CompareBack = 0;
				Hashtable FirstNodeData = (Hashtable) x;
				Hashtable SecondNodeData = (Hashtable) y;
				string FirstNodeGridVal = FirstNodeData["GridText"].ToString();
				string SecondNodeGridVal = SecondNodeData["GridText"].ToString();
				CompareBack = FirstNodeGridVal.CompareTo(SecondNodeGridVal) * -1;
				return CompareBack;
			}
		}
		#endregion

		#region Background Events

		private void ViewFormTimer_Tick(object sender, System.EventArgs e)
		{
			this.ViewFormTimer.Enabled = false;

			if (PendingDeletions)
			{
                HandlePendingDeletions();
			}

			if (CategoryAssignPending)
			{
                AddSoftAssignedNodes(CategoryAssignedNodes);
				CategoryAssignedNodes.Clear();
				CategoryAssignPending = false;
			}

			if (SoftAssignPending)
			{
                AddSoftAssignedNodes(SoftAssignedNodes);
				SoftAssignedNodes.Clear();
				SoftAssignPending = false;
			}

			if (checkForGridAccess)
			{
				int topValue = Convert.ToInt16(ItemGrid[0,0]);
				if (topValue == 1)
					{ItemGrid.ReadOnly = false;
					this.picLockedBox.Visible = false;}
				checkForGridAccess = false;
			}

			if (StartupRoutines)
			{
				this.Text = "orGenta " + softwareVersion.Major.ToString() + "." + softwareVersion.Minor.ToString();
				DataInitialization();
				this.MinimizeBox = true;
				StartupRoutines = false;
			}

			if (ReselectNode)
			{
				ReselectNode = false;
				SendKeys.Send("{ENTER}");
				BuildGrid();
			}

			if (AddingNewCategoryAutoEd)
				{AddingNewCategoryAutoEd = false;
				this.CategoryHierTV.SelectedNode.BeginEdit();}

			if ((DelayedBuildGrid) || (DupeRelocate))
				{DelayedBuildGrid = false;
				BuildGrid();}

			if (DupeRelocate)
			{
                RelocateOnDupe();
			}

			if (SaveEditedRow >= 0)
			{
				this.ItemGrid.Focus();
				this.ItemGrid.CurrentCell = new DataGridCell(SaveEditedRow, SaveEditedCol);
				DataGridColumnStyle dgCol;
				dgCol = this.ItemGrid.TableStyles[0].GridColumnStyles[SaveEditedCol];
				this.ItemGrid.BeginEdit(dgCol, SaveEditedRow);
				SaveEditedRow = -1;
			}

			if (EditItemMatchingCategory)
			{
				EditItemMatchingCategory = false;
				this.ItemGrid.Focus();
				if (CategoryHierTV.SelectedNode == null)
					{return;}
                EditMatchingCat();
			}
		}

        private void EditMatchingCat()
        {
            int RowToStartAt = RowXrefArray.IndexOf(CategoryHierTV.SelectedNode.FullPath) + 1;
            RowToStartAt += CategoryHierTV.SelectedNode.Index - 1;
            this.ItemGrid.CurrentCell = new DataGridCell(RowToStartAt, 2);
            if (AutoPopNote)
            {
                AutoPopNote = false;
                if (CategoryHierTV.SelectedNode.Text == "-")
                { BuildAndShowNote(CategoryHierTV.SelectedNode.Tag.ToString().Substring(2)); }
            }
            else
            {
                DataGridColumnStyle dgCol;
                dgCol = this.ItemGrid.TableStyles[0].GridColumnStyles[2];
                this.ItemGrid.BeginEdit(dgCol, RowToStartAt);
            }
        }

        private void RelocateOnDupe()
        {
            this.CategoryHierTV.SelectedNode.EnsureVisible();
            Application.DoEvents();
            TreeNode WalkingNode = this.CategoryHierTV.TopNode;
            int TopGridRow = this.ItemGrid.HitTest(100, 20).Row;
            for (int i = 0; i < this.CategoryHierTV.VisibleCount; i++)
            {
                if (WalkingNode == this.CategoryHierTV.SelectedNode)
                {
                    SaveEditedCol = 2;
                    SaveEditedRow = TopGridRow + i - 1;
                    break;
                }
                WalkingNode = WalkingNode.NextVisibleNode;
            }
            DupeRelocate = false;
        }

        private void HandlePendingDeletions()
        {
            this.ItemsDataAdapter.Update(this.ItemsMemImage);
            AlreadyAskedOnDiscard = false;
            SaveEditedRow = this.ItemGrid.HitTest(100, 20).Row;
            SaveEditedCol = 2;
            this.CategoryHierTV.BeginUpdate();
            for (int i = DeletedRows.Count - 1; i >= 0; i--)
            {
                DelayedRowDelete(Convert.ToInt16(DeletedRows[i]));
                if (DeleteCancelled)
                { break; }
            }
            this.CategoryHierTV.EndUpdate();
            this.CategoryHierTV.Refresh();
            Application.DoEvents();
            DeletedRows.Clear();
            PendingDeletions = false;
            DelayedBuildGrid = true;
        }

		#endregion

		#region EOJ procedures
		private void ViewForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (Debugging)
				{RedirectedConsole.Close();}
			if (fastAbort)
				{return;}
			if (!endRoutinesRun)
				{endRoutines();}
		}

		private void endRoutines()
		{
			// TODO: wait on any background threads to finish
			this.Cursor = Cursors.WaitCursor;
			Application.DoEvents();
			endRoutinesRunning = true;

			this.ItemsDataAdapter.Update(this.ItemsMemImage);
			if (SoftAssignPending)
                { AddSoftAssignedNodes(SoftAssignedNodes); }
			if (CategoryAssignPending)
                { AddSoftAssignedNodes(CategoryAssignedNodes); }

			for (int i = 0; i < this.dataGridTableStyle1.GridColumnStyles.Count; i++)
			{
				int oneColWidth = this.dataGridTableStyle1.GridColumnStyles[i].Width;
				SavedColWidths[i] = oneColWidth.ToString();
			}
			if (!ShowDone)
				{menuShowDone_Click(new object(), new System.EventArgs());}

			// Serialze-save the treeView
			saveTree(this.CategoryHierTV, CatHierSaveFileName);
			RegistryKey ThisUser = Registry.CurrentUser;
			try
			{
                RegistryKey ColWidths = SaveIfaceToRegistry(ThisUser);
				ColWidths.SetValue("Values", String.Join(",", SavedColWidths));
			}
			catch {}
			endRoutinesRun = true;
			endRoutinesRunning = false;
			this.Cursor = Cursors.Default;
		}

		protected override void Dispose( bool disposing )
		{
			GetTextLineForm.Close();
			if( disposing )
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		#endregion
	}
}
