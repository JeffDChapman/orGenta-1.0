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
        #region mSoft Windows Form variables
        // Microsoft generated form-variables
        private System.Windows.Forms.MainMenu menuMain;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuItem6;
        private System.Windows.Forms.MenuItem menuItemExit;
        private System.Windows.Forms.Timer ViewFormTimer;
        private System.Data.OleDb.OleDbConnection oleorGentaDbConx;
        private System.Data.OleDb.OleDbDataAdapter ItemsDataAdapter;
        private orGenta.ItemsDataSet iGridDataSource;
        private orGenta.ItemsDataSet ItemsMemImage;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn3;
        private System.Windows.Forms.DataGridBoolColumn dataGridTextBoxColumn4;
        private System.Windows.Forms.DataGridBoolColumn dataGridTextBoxColumn5;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn6;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn7;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn8;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn9;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn10;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn11;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn12;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn13;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn14;
        private System.Windows.Forms.MenuItem menuItemAbout;
        private System.Windows.Forms.MenuItem menuItemImportItems;
        private System.Windows.Forms.OpenFileDialog ImportItemsDialog;
        private System.Windows.Forms.MenuItem menuShowDone;
        private System.Windows.Forms.MenuItem menuItem10;
        private System.Windows.Forms.MenuItem menuItemFindItem;
        private System.Windows.Forms.MenuItem menuItemFindCategory;
        private System.Windows.Forms.MenuItem menuItemFindNote;
        private System.Windows.Forms.MenuItem menuItemFindNext;
        private System.Data.OleDb.OleDbCommand oleDbSelectCommand1;
        private System.Data.OleDb.OleDbCommand oleDbInsertCommand1;
        private System.Data.OleDb.OleDbCommand oleDbUpdateCommand1;
        private System.Data.OleDb.OleDbCommand oleDbDeleteCommand1;
        private System.Windows.Forms.MenuItem menuItem9;
        private System.Windows.Forms.MenuItem menuExpandAll;
        private System.Windows.Forms.MenuItem menuCollapseAll;
        private System.Windows.Forms.MenuItem menuRegistration;
        private System.Windows.Forms.MenuItem menuItem7;
        private System.Windows.Forms.MenuItem menuExportCSV;
        private System.Windows.Forms.MenuItem menuExportTabbed;
        private System.Windows.Forms.MenuItem menuPrint;
        private System.Windows.Forms.MenuItem menuItem11;
        private System.Windows.Forms.SaveFileDialog ExportItemsDialog;
        private System.Drawing.Printing.PrintDocument prtGridPrintDoc;
        private System.Windows.Forms.PrintPreviewDialog prtOrgentaPreviewDialog;
        private System.Windows.Forms.PageSetupDialog orGentaPageSetup;
        private System.Windows.Forms.MenuItem menuPrintSetup;
        private System.Windows.Forms.MenuItem menuCalendar;
        private System.Windows.Forms.MenuItem menuItem12;
        private System.Windows.Forms.MenuItem menuAssignTo;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem menuAdvPrint;
        private System.Windows.Forms.MenuItem menuExpandNode;
        private System.Windows.Forms.MenuItem menuCollapseNode;
        private System.Windows.Forms.MenuItem menuItem14;
        private System.Windows.Forms.MenuItem menuItemPrint;
        private System.Windows.Forms.MenuItem menuItem15;
        private System.Windows.Forms.MenuItem menuSortCats;
        private System.Windows.Forms.MenuItem menuSortItems;
        private System.Windows.Forms.Panel pnlForButtons;
        private System.Windows.Forms.ToolBar tBarQuickBtns;
        private System.Windows.Forms.Panel pnlNotTheButtons;
        private System.Windows.Forms.Panel pnlNoteFind;
        private System.Windows.Forms.TextBox txtNoteSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlItemFind;
        private System.Windows.Forms.TextBox txtItemSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlCatFind;
        private System.Windows.Forms.TextBox txtCatSearch;
        private System.Windows.Forms.Label lblFindCat;
        private System.Windows.Forms.Label catInvisLabel;
        private System.Windows.Forms.PictureBox picLockedBox;
        private System.Windows.Forms.DataGrid ItemGrid;
        private System.Windows.Forms.TreeView CategoryHierTV;
        private System.Windows.Forms.Splitter splGridSplitter;
        private System.Windows.Forms.ToolBarButton tBBprint;
        private System.Windows.Forms.ToolBarButton tBBblank1;
        private System.Windows.Forms.Label lblHorizSep;
        private System.Windows.Forms.MenuItem menuDropCategories;
        private System.Windows.Forms.MenuItem menuItem13;
        private System.Windows.Forms.MenuItem menuResetWidths;
        private System.Windows.Forms.MenuItem menuItem8;
        private System.Windows.Forms.MenuItem menuTrayed;
        private System.Windows.Forms.MenuItem menuItem17;
        private System.Windows.Forms.ToolBarButton tBBprintItem;
        private System.Windows.Forms.ToolBarButton tBBblank2;
        private System.Windows.Forms.ToolBarButton tBBtrayed;
        private System.Windows.Forms.ToolBarButton tBBblank3;
        private System.Windows.Forms.ToolBarButton tBBfindCategory;
        private System.Windows.Forms.ToolBarButton tBBfindItem;
        private System.Windows.Forms.ToolBarButton tBBfindNote;
        private System.Windows.Forms.ToolBarButton tBBfindNext;
        private System.Windows.Forms.ToolBarButton tBBblank4;
        private System.Windows.Forms.ToolBarButton tBBassign;
        private System.Windows.Forms.ToolBarButton tBBblank5;
        private System.Windows.Forms.ToolBarButton tBBshowDone;
        private System.Windows.Forms.ToolBarButton tBBblank6;
        private System.Windows.Forms.ToolBarButton tBBexpandNode;
        private System.Windows.Forms.ToolBarButton tBBcollapseNode;
        private System.Windows.Forms.ToolBarButton tBBblank7;
        private System.Windows.Forms.ToolBarButton tBBcalendar;
        private System.Windows.Forms.ToolBarButton tBBblank8;
        private System.Windows.Forms.ToolBarButton tBBsortCats;
        private System.Windows.Forms.ToolBarButton tBBsortItems;
        private System.Windows.Forms.ImageList iListTbarBtns;
        private System.Windows.Forms.NotifyIcon trayIconTrayed;
        private System.Windows.Forms.Label lblLoading;
        private System.Windows.Forms.MenuItem menuItem18;
        private System.Windows.Forms.MenuItem menuZoomItem;
        private System.Windows.Forms.MenuItem menuCleanup;
        private System.Windows.Forms.ContextMenu ctxMenuGrid;
        private System.Windows.Forms.MenuItem ctxZoom;
        private System.Windows.Forms.MenuItem ctxPrintItem;
        private System.Windows.Forms.MenuItem ctxAssign;
        private System.Windows.Forms.MenuItem menuItemOccursAgain;
        private System.Windows.Forms.ToolBarButton tBBFindDupe;
        private System.Windows.Forms.MenuItem ctxOccursAgain;
        private System.Windows.Forms.MenuItem menuItem16;
        private System.Windows.Forms.MenuItem ctxDelete;
        private System.Windows.Forms.MenuItem menuItem20;
        private System.Windows.Forms.MenuItem menuItem21;
        private System.Windows.Forms.MenuItem menuItemImportWeb;
        private System.Windows.Forms.ContextMenu ctxMenuTV;
        private System.Windows.Forms.MenuItem ctxExpandNode;
        private System.Windows.Forms.MenuItem ctxCollapseNode;
        private System.Windows.Forms.MenuItem ctxFreezeNode;
        private System.Windows.Forms.MenuItem menuExpandOnlyThis;
        private System.Windows.Forms.MenuItem ctxExapndOnlyThis;
        private System.Windows.Forms.ToolBarButton tBBExpandOnly;
        private System.Windows.Forms.MenuItem menuNotesToItems;
        private System.Windows.Forms.MenuItem menuColorReset;
        private System.ComponentModel.IContainer components;

        #endregion

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ViewForm));
            this.menuMain = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuPrint = new System.Windows.Forms.MenuItem();
            this.menuItemPrint = new System.Windows.Forms.MenuItem();
            this.menuAdvPrint = new System.Windows.Forms.MenuItem();
            this.menuPrintSetup = new System.Windows.Forms.MenuItem();
            this.menuItem11 = new System.Windows.Forms.MenuItem();
            this.menuTrayed = new System.Windows.Forms.MenuItem();
            this.menuItem17 = new System.Windows.Forms.MenuItem();
            this.menuItemExit = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItemFindCategory = new System.Windows.Forms.MenuItem();
            this.menuItemFindItem = new System.Windows.Forms.MenuItem();
            this.menuItemFindNote = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.menuItemFindNext = new System.Windows.Forms.MenuItem();
            this.menuItemOccursAgain = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuAssignTo = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuShowDone = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.menuZoomItem = new System.Windows.Forms.MenuItem();
            this.menuItem18 = new System.Windows.Forms.MenuItem();
            this.menuExpandNode = new System.Windows.Forms.MenuItem();
            this.menuCollapseNode = new System.Windows.Forms.MenuItem();
            this.menuExpandOnlyThis = new System.Windows.Forms.MenuItem();
            this.menuItem14 = new System.Windows.Forms.MenuItem();
            this.menuExpandAll = new System.Windows.Forms.MenuItem();
            this.menuCollapseAll = new System.Windows.Forms.MenuItem();
            this.menuItem12 = new System.Windows.Forms.MenuItem();
            this.menuResetWidths = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.menuCalendar = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItemImportItems = new System.Windows.Forms.MenuItem();
            this.menuItemImportWeb = new System.Windows.Forms.MenuItem();
            this.menuItem21 = new System.Windows.Forms.MenuItem();
            this.menuExportCSV = new System.Windows.Forms.MenuItem();
            this.menuExportTabbed = new System.Windows.Forms.MenuItem();
            this.menuItem15 = new System.Windows.Forms.MenuItem();
            this.menuSortCats = new System.Windows.Forms.MenuItem();
            this.menuSortItems = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuDropCategories = new System.Windows.Forms.MenuItem();
            this.menuNotesToItems = new System.Windows.Forms.MenuItem();
            this.menuCleanup = new System.Windows.Forms.MenuItem();
            this.menuColorReset = new System.Windows.Forms.MenuItem();
            this.menuItem13 = new System.Windows.Forms.MenuItem();
            this.menuRegistration = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItemAbout = new System.Windows.Forms.MenuItem();
            this.ViewFormTimer = new System.Windows.Forms.Timer(this.components);
            this.oleorGentaDbConx = new System.Data.OleDb.OleDbConnection();
            this.ItemsDataAdapter = new System.Data.OleDb.OleDbDataAdapter();
            this.oleDbDeleteCommand1 = new System.Data.OleDb.OleDbCommand();
            this.oleDbInsertCommand1 = new System.Data.OleDb.OleDbCommand();
            this.oleDbSelectCommand1 = new System.Data.OleDb.OleDbCommand();
            this.oleDbUpdateCommand1 = new System.Data.OleDb.OleDbCommand();
            this.ItemsMemImage = new orGenta.ItemsDataSet();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.ItemGrid = new System.Windows.Forms.DataGrid();
            this.ctxMenuGrid = new System.Windows.Forms.ContextMenu();
            this.ctxZoom = new System.Windows.Forms.MenuItem();
            this.ctxOccursAgain = new System.Windows.Forms.MenuItem();
            this.ctxAssign = new System.Windows.Forms.MenuItem();
            this.menuItem16 = new System.Windows.Forms.MenuItem();
            this.ctxPrintItem = new System.Windows.Forms.MenuItem();
            this.menuItem20 = new System.Windows.Forms.MenuItem();
            this.ctxDelete = new System.Windows.Forms.MenuItem();
            this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn4 = new System.Windows.Forms.DataGridBoolColumn();
            this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn3 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn5 = new System.Windows.Forms.DataGridBoolColumn();
            this.dataGridTextBoxColumn6 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn7 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn8 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn9 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn10 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn11 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn14 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn12 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn13 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.ImportItemsDialog = new System.Windows.Forms.OpenFileDialog();
            this.ExportItemsDialog = new System.Windows.Forms.SaveFileDialog();
            this.prtGridPrintDoc = new System.Drawing.Printing.PrintDocument();
            this.prtOrgentaPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.orGentaPageSetup = new System.Windows.Forms.PageSetupDialog();
            this.pnlForButtons = new System.Windows.Forms.Panel();
            this.tBarQuickBtns = new System.Windows.Forms.ToolBar();
            this.tBBblank1 = new System.Windows.Forms.ToolBarButton();
            this.tBBprint = new System.Windows.Forms.ToolBarButton();
            this.tBBprintItem = new System.Windows.Forms.ToolBarButton();
            this.tBBblank2 = new System.Windows.Forms.ToolBarButton();
            this.tBBtrayed = new System.Windows.Forms.ToolBarButton();
            this.tBBblank3 = new System.Windows.Forms.ToolBarButton();
            this.tBBfindCategory = new System.Windows.Forms.ToolBarButton();
            this.tBBfindItem = new System.Windows.Forms.ToolBarButton();
            this.tBBfindNote = new System.Windows.Forms.ToolBarButton();
            this.tBBfindNext = new System.Windows.Forms.ToolBarButton();
            this.tBBFindDupe = new System.Windows.Forms.ToolBarButton();
            this.tBBblank4 = new System.Windows.Forms.ToolBarButton();
            this.tBBassign = new System.Windows.Forms.ToolBarButton();
            this.tBBblank5 = new System.Windows.Forms.ToolBarButton();
            this.tBBshowDone = new System.Windows.Forms.ToolBarButton();
            this.tBBblank6 = new System.Windows.Forms.ToolBarButton();
            this.tBBexpandNode = new System.Windows.Forms.ToolBarButton();
            this.tBBExpandOnly = new System.Windows.Forms.ToolBarButton();
            this.tBBcollapseNode = new System.Windows.Forms.ToolBarButton();
            this.tBBblank7 = new System.Windows.Forms.ToolBarButton();
            this.tBBcalendar = new System.Windows.Forms.ToolBarButton();
            this.tBBblank8 = new System.Windows.Forms.ToolBarButton();
            this.tBBsortCats = new System.Windows.Forms.ToolBarButton();
            this.tBBsortItems = new System.Windows.Forms.ToolBarButton();
            this.iListTbarBtns = new System.Windows.Forms.ImageList(this.components);
            this.pnlNotTheButtons = new System.Windows.Forms.Panel();
            this.picLockedBox = new System.Windows.Forms.PictureBox();
            this.pnlItemFind = new System.Windows.Forms.Panel();
            this.txtItemSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlNoteFind = new System.Windows.Forms.Panel();
            this.txtNoteSearch = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblHorizSep = new System.Windows.Forms.Label();
            this.splGridSplitter = new System.Windows.Forms.Splitter();
            this.pnlCatFind = new System.Windows.Forms.Panel();
            this.txtCatSearch = new System.Windows.Forms.TextBox();
            this.lblFindCat = new System.Windows.Forms.Label();
            this.catInvisLabel = new System.Windows.Forms.Label();
            this.CategoryHierTV = new System.Windows.Forms.TreeView();
            this.ctxMenuTV = new System.Windows.Forms.ContextMenu();
            this.ctxExpandNode = new System.Windows.Forms.MenuItem();
            this.ctxCollapseNode = new System.Windows.Forms.MenuItem();
            this.ctxExapndOnlyThis = new System.Windows.Forms.MenuItem();
            this.ctxFreezeNode = new System.Windows.Forms.MenuItem();
            this.lblLoading = new System.Windows.Forms.Label();
            this.trayIconTrayed = new System.Windows.Forms.NotifyIcon(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ItemsMemImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemGrid)).BeginInit();
            this.pnlForButtons.SuspendLayout();
            this.pnlNotTheButtons.SuspendLayout();
            this.pnlItemFind.SuspendLayout();
            this.pnlNoteFind.SuspendLayout();
            this.pnlCatFind.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.menuItem1,
																					 this.menuItem2,
																					 this.menuItem3,
																					 this.menuItem4,
																					 this.menuItem6});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuPrint,
																					  this.menuItemPrint,
																					  this.menuAdvPrint,
																					  this.menuPrintSetup,
																					  this.menuItem11,
																					  this.menuTrayed,
																					  this.menuItem17,
																					  this.menuItemExit});
            this.menuItem1.Text = orGentaResources.mnuFile;
            // 
            // menuPrint
            // 
            this.menuPrint.Index = 0;
            this.menuPrint.Shortcut = System.Windows.Forms.Shortcut.CtrlP;
            this.menuPrint.Text = orGentaResources.mnuPagePrint;
            this.menuPrint.Click += new System.EventHandler(this.menuPrint_Click);
            // 
            // menuItemPrint
            // 
            this.menuItemPrint.Index = 1;
            this.menuItemPrint.Text = orGentaResources.mnuItemPrint;
            this.menuItemPrint.Click += new System.EventHandler(this.menuItemPrint_Click);
            // 
            // menuAdvPrint
            // 
            this.menuAdvPrint.Index = 2;
            this.menuAdvPrint.Text = orGentaResources.mnuAdvancedPrint;
            this.menuAdvPrint.Click += new System.EventHandler(this.menuAdvPrint_Click);
            // 
            // menuPrintSetup
            // 
            this.menuPrintSetup.Index = 3;
            this.menuPrintSetup.Text = orGentaResources.mnuPrinterSetup;
            this.menuPrintSetup.Click += new System.EventHandler(this.menuPrintSetup_Click);
            // 
            // menuItem11
            // 
            this.menuItem11.Index = 4;
            this.menuItem11.Text = "-";
            // 
            // menuTrayed
            // 
            this.menuTrayed.Index = 5;
            this.menuTrayed.Text = orGentaResources.mnuTrayed;
            this.menuTrayed.Click += new System.EventHandler(this.menuTrayed_Click);
            // 
            // menuItem17
            // 
            this.menuItem17.Index = 6;
            this.menuItem17.Text = "-";
            // 
            // menuItemExit
            // 
            this.menuItemExit.Index = 7;
            this.menuItemExit.Shortcut = System.Windows.Forms.Shortcut.CtrlQ;
            this.menuItemExit.Text = orGentaResources.mnuExit;
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 1;
            this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItemFindCategory,
																					  this.menuItemFindItem,
																					  this.menuItemFindNote,
																					  this.menuItem10,
																					  this.menuItemFindNext,
																					  this.menuItemOccursAgain,
																					  this.menuItem5,
																					  this.menuAssignTo});
            this.menuItem2.Text = orGentaResources.mnuEdit;
            // 
            // menuItemFindCategory
            // 
            this.menuItemFindCategory.Index = 0;
            this.menuItemFindCategory.Text = orGentaResources.mnuFindCategory;
            this.menuItemFindCategory.Click += new System.EventHandler(this.menuItemFindCategory_Click);
            // 
            // menuItemFindItem
            // 
            this.menuItemFindItem.Index = 1;
            this.menuItemFindItem.Shortcut = System.Windows.Forms.Shortcut.CtrlF;
            this.menuItemFindItem.Text = orGentaResources.mnuFindItem;
            this.menuItemFindItem.Click += new System.EventHandler(this.menuItemFindItem_Click);
            // 
            // menuItemFindNote
            // 
            this.menuItemFindNote.Index = 2;
            this.menuItemFindNote.Text = orGentaResources.mnuFindNote;
            this.menuItemFindNote.Click += new System.EventHandler(this.menuItemFindNote_Click);
            // 
            // menuItem10
            // 
            this.menuItem10.Index = 3;
            this.menuItem10.Text = "-";
            // 
            // menuItemFindNext
            // 
            this.menuItemFindNext.Index = 4;
            this.menuItemFindNext.Shortcut = System.Windows.Forms.Shortcut.F3;
            this.menuItemFindNext.Text = orGentaResources.mnuFindNext;
            this.menuItemFindNext.Click += new System.EventHandler(this.menuItemFindNext_Click);
            // 
            // menuItemOccursAgain
            // 
            this.menuItemOccursAgain.Index = 5;
            this.menuItemOccursAgain.Shortcut = System.Windows.Forms.Shortcut.F9;
            this.menuItemOccursAgain.Text = orGentaResources.mnuOccursAgain;
            this.menuItemOccursAgain.Click += new System.EventHandler(this.menuItemOccursAgain_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 6;
            this.menuItem5.Text = "-";
            // 
            // menuAssignTo
            // 
            this.menuAssignTo.Index = 7;
            this.menuAssignTo.Text = orGentaResources.mnuAssign;
            this.menuAssignTo.Click += new System.EventHandler(this.menuAssignTo_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 2;
            this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuShowDone,
																					  this.menuItem9,
																					  this.menuZoomItem,
																					  this.menuItem18,
																					  this.menuExpandNode,
																					  this.menuCollapseNode,
																					  this.menuExpandOnlyThis,
																					  this.menuItem14,
																					  this.menuExpandAll,
																					  this.menuCollapseAll,
																					  this.menuItem12,
																					  this.menuResetWidths,
																					  this.menuItem8,
																					  this.menuCalendar});
            this.menuItem3.Text = orGentaResources.mnuView;
            // 
            // menuShowDone
            // 
            this.menuShowDone.Checked = true;
            this.menuShowDone.Index = 0;
            this.menuShowDone.Text = orGentaResources.mnuShowDone;
            this.menuShowDone.Click += new System.EventHandler(this.menuShowDone_Click);
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 1;
            this.menuItem9.Text = "-";
            // 
            // menuZoomItem
            // 
            this.menuZoomItem.Index = 2;
            this.menuZoomItem.Shortcut = System.Windows.Forms.Shortcut.F8;
            this.menuZoomItem.Text = orGentaResources.mnuZoomItem;
            this.menuZoomItem.Click += new System.EventHandler(this.menuZoomItem_Click);
            // 
            // menuItem18
            // 
            this.menuItem18.Index = 3;
            this.menuItem18.Text = "-";
            // 
            // menuExpandNode
            // 
            this.menuExpandNode.Index = 4;
            this.menuExpandNode.Shortcut = System.Windows.Forms.Shortcut.CtrlE;
            this.menuExpandNode.Text = orGentaResources.mnuExpandNode;
            this.menuExpandNode.Click += new System.EventHandler(this.menuExpandNode_Click);
            // 
            // menuCollapseNode
            // 
            this.menuCollapseNode.Index = 5;
            this.menuCollapseNode.Shortcut = System.Windows.Forms.Shortcut.CtrlW;
            this.menuCollapseNode.Text = orGentaResources.mnuCollapseNode;
            this.menuCollapseNode.Click += new System.EventHandler(this.menuCollapseNode_Click);
            // 
            // menuExpandOnlyThis
            // 
            this.menuExpandOnlyThis.Index = 6;
            this.menuExpandOnlyThis.Text = orGentaResources.mnuExpandThis;
            this.menuExpandOnlyThis.Click += new System.EventHandler(this.menuExpandOnlyThis_Click);
            // 
            // menuItem14
            // 
            this.menuItem14.Index = 7;
            this.menuItem14.Text = "-";
            // 
            // menuExpandAll
            // 
            this.menuExpandAll.Index = 8;
            this.menuExpandAll.Text = orGentaResources.mnuExpandAll;
            this.menuExpandAll.Click += new System.EventHandler(this.menuExpandAll_Click);
            // 
            // menuCollapseAll
            // 
            this.menuCollapseAll.Index = 9;
            this.menuCollapseAll.Text = orGentaResources.mnuCollapseAll;
            this.menuCollapseAll.Click += new System.EventHandler(this.menuCollapseAll_Click);
            // 
            // menuItem12
            // 
            this.menuItem12.Index = 10;
            this.menuItem12.Text = "-";
            // 
            // menuResetWidths
            // 
            this.menuResetWidths.Index = 11;
            this.menuResetWidths.Text = orGentaResources.mnuResetWidths;
            this.menuResetWidths.Click += new System.EventHandler(this.menuResetWidths_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 12;
            this.menuItem8.Text = "-";
            // 
            // menuCalendar
            // 
            this.menuCalendar.Index = 13;
            this.menuCalendar.Text = orGentaResources.mnuCalendar;
            this.menuCalendar.Click += new System.EventHandler(this.menuCalendar_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 3;
            this.menuItem4.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItemImportItems,
																					  this.menuItemImportWeb,
																					  this.menuItem21,
																					  this.menuExportCSV,
																					  this.menuExportTabbed,
																					  this.menuItem15,
																					  this.menuSortCats,
																					  this.menuSortItems,
																					  this.menuItem7,
																					  this.menuDropCategories,
																					  this.menuNotesToItems,
																					  this.menuCleanup,
																					  this.menuColorReset,
																					  this.menuItem13,
																					  this.menuRegistration});
            this.menuItem4.Text = orGentaResources.mnuTools;
            // 
            // menuItemImportItems
            // 
            this.menuItemImportItems.Index = 0;
            this.menuItemImportItems.Text = orGentaResources.mnuImportItems;
            this.menuItemImportItems.Click += new System.EventHandler(this.menuItemImportItems_Click);
            // 
            // menuItemImportWeb
            // 
            this.menuItemImportWeb.Index = 1;
            this.menuItemImportWeb.Text = orGentaResources.mnuImportWeb;
            this.menuItemImportWeb.Click += new System.EventHandler(this.menuItemImportWeb_Click);
            // 
            // menuItem21
            // 
            this.menuItem21.Index = 2;
            this.menuItem21.Text = "-";
            // 
            // menuExportCSV
            // 
            this.menuExportCSV.Index = 3;
            this.menuExportCSV.Text = orGentaResources.mnuExportCSV;
            this.menuExportCSV.Click += new System.EventHandler(this.menuExportCSV_Click);
            // 
            // menuExportTabbed
            // 
            this.menuExportTabbed.Index = 4;
            this.menuExportTabbed.Text = orGentaResources.mnuExportTabbed;
            this.menuExportTabbed.Click += new System.EventHandler(this.menuExportTabbed_Click);
            // 
            // menuItem15
            // 
            this.menuItem15.Index = 5;
            this.menuItem15.Text = "-";
            // 
            // menuSortCats
            // 
            this.menuSortCats.Index = 6;
            this.menuSortCats.Text = orGentaResources.mnuSortCategories;
            this.menuSortCats.Click += new System.EventHandler(this.menuSortCats_Click);
            // 
            // menuSortItems
            // 
            this.menuSortItems.Index = 7;
            this.menuSortItems.Text = orGentaResources.mnuSortItems;
            this.menuSortItems.Click += new System.EventHandler(this.menuSortItems_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 8;
            this.menuItem7.Text = "-";
            // 
            // menuDropCategories
            // 
            this.menuDropCategories.Index = 9;
            this.menuDropCategories.Text = orGentaResources.mnuDropCats;
            this.menuDropCategories.Click += new System.EventHandler(this.menuDropCategories_Click);
            // 
            // menuNotesToItems
            // 
            this.menuNotesToItems.Index = 10;
            this.menuNotesToItems.Text = orGentaResources.mnuNotesToItems;
            this.menuNotesToItems.Click += new System.EventHandler(this.menuNotesToItems_Click);
            // 
            // menuCleanup
            // 
            this.menuCleanup.Index = 11;
            this.menuCleanup.Text = orGentaResources.mnuCleanup;
            this.menuCleanup.Click += new System.EventHandler(this.menuCleanup_Click);
            // 
            // menuColorReset
            // 
            this.menuColorReset.Index = 12;
            this.menuColorReset.Text = orGentaResources.mnuColorReset;
            this.menuColorReset.Click += new System.EventHandler(this.menuColorReset_Click);
            // 
            // menuItem13
            // 
            this.menuItem13.Index = 13;
            this.menuItem13.Text = "-";
            // 
            // menuRegistration
            // 
            this.menuRegistration.Index = 14;
            this.menuRegistration.Text = orGentaResources.Registration;
            this.menuRegistration.Click += new System.EventHandler(this.menuRegistration_Click);
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 4;
            this.menuItem6.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItemAbout});
            this.menuItem6.Text = orGentaResources.mnuHelp;
            // 
            // menuItemAbout
            // 
            this.menuItemAbout.Index = 0;
            this.menuItemAbout.Shortcut = System.Windows.Forms.Shortcut.F1;
            this.menuItemAbout.Text = orGentaResources.mnuAbout;
            this.menuItemAbout.Click += new System.EventHandler(this.menuItemAbout_Click);
            // 
            // ViewFormTimer
            // 
            this.ViewFormTimer.Interval = 25;
            this.ViewFormTimer.Tick += new System.EventHandler(this.ViewFormTimer_Tick);
            // 
            // oleorGentaDbConx
            // 
            //			this.oleorGentaDbConx.ConnectionString = @"Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Registry Path=;Jet OLEDB:Database Locking Mode=0;Jet OLEDB:Database Password=;Data Source=""C:\Documents and Settings\jChapman\My Documents\Visual Studio Projects\orGenta\bin\Debug\orGenta.mdb"";Password=;Jet OLEDB:Engine Type=4;Jet OLEDB:Global Bulk Transactions=1;Provider=""Microsoft.Jet.OLEDB.4.0"";Jet OLEDB:System database=;Jet OLEDB:SFP=False;Extended Properties=;Mode=Share Deny None;Jet OLEDB:New Database Password=;Jet OLEDB:Create System Database=False;Jet OLEDB:Don't Copy Locale on Compact=False;Jet OLEDB:Compact Without Replica Repair=False;User ID=Admin;Jet OLEDB:Encrypt Database=False";
            this.oleorGentaDbConx.ConnectionString = @"Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Registry Path=;Jet OLEDB:Database Locking Mode=0;Jet OLEDB:Database Password=;Data Source=""" + Environment.CurrentDirectory + @"\orGenta.mdb"";Password=;Jet OLEDB:Engine Type=4;Jet OLEDB:Global Bulk Transactions=1;Provider=""Microsoft.Jet.OLEDB.4.0"";Jet OLEDB:System database=;Jet OLEDB:SFP=False;Extended Properties=;Mode=Share Deny None;Jet OLEDB:New Database Password=;Jet OLEDB:Create System Database=False;Jet OLEDB:Don't Copy Locale on Compact=False;Jet OLEDB:Compact Without Replica Repair=False;User ID=Admin;Jet OLEDB:Encrypt Database=False";

            // 
            // ItemsDataAdapter
            // 
            this.ItemsDataAdapter.DeleteCommand = this.oleDbDeleteCommand1;
            this.ItemsDataAdapter.InsertCommand = this.oleDbInsertCommand1;
            this.ItemsDataAdapter.SelectCommand = this.oleDbSelectCommand1;
            this.ItemsDataAdapter.TableMappings.AddRange(new System.Data.Common.DataTableMapping[] {
																									   new System.Data.Common.DataTableMapping("Table", "Items", new System.Data.Common.DataColumnMapping[] {
																																																				new System.Data.Common.DataColumnMapping("ItemKey", "ItemKey"),
																																																				new System.Data.Common.DataColumnMapping("HasNote", "HasNote"),
																																																				new System.Data.Common.DataColumnMapping("TextItem", "TextItem"),
																																																				new System.Data.Common.DataColumnMapping("WhenDate", "WhenDate"),
																																																				new System.Data.Common.DataColumnMapping("Done", "Done"),
																																																				new System.Data.Common.DataColumnMapping("DoneDate", "DoneDate"),
																																																				new System.Data.Common.DataColumnMapping("Priority", "Priority"),
																																																				new System.Data.Common.DataColumnMapping("WorkPhone", "WorkPhone"),
																																																				new System.Data.Common.DataColumnMapping("CellPhone", "CellPhone"),
																																																				new System.Data.Common.DataColumnMapping("HomePhone", "HomePhone"),
																																																				new System.Data.Common.DataColumnMapping("eMail", "eMail"),
																																																				new System.Data.Common.DataColumnMapping("Address", "Address"),
																																																				new System.Data.Common.DataColumnMapping("EnteredDate", "EnteredDate"),
																																																				new System.Data.Common.DataColumnMapping("Link", "Link"),
																																																				new System.Data.Common.DataColumnMapping("Recurrence", "Recurrence")})});
            this.ItemsDataAdapter.UpdateCommand = this.oleDbUpdateCommand1;
            // 
            // oleDbDeleteCommand1
            // 
            this.oleDbDeleteCommand1.CommandText = @"DELETE FROM Items WHERE (ItemKey = 
				?) AND (Address = ? OR ? IS NULL AND Address IS NULL) AND (CellPhone = ?
				OR ? IS NULL AND CellPhone IS NULL) AND (Done = ?) AND (DoneDate = ? OR ? IS
				NULL AND DoneDate IS NULL) AND (EnteredDate = ? OR ? IS NULL AND EnteredDate
				IS NULL) AND (HasNote = ?) AND (HomePhone = ? OR ? IS NULL AND HomePhone
				IS NULL) AND (Link = ? OR ? IS NULL AND Link IS NULL) AND (Priority = ? OR 
				? IS NULL AND Priority IS NULL) AND (Recurrence = ? OR ? IS NULL AND Recurrence 
				IS NULL) AND (TextItem = ?) AND (WhenDate = ? OR ? IS NULL AND WhenDate 
				IS NULL) AND (WorkPhone = ? OR ? IS NULL AND WorkPhone IS NULL) AND (eMail =
				? OR ? IS NULL AND eMail IS NULL)";
            this.oleDbDeleteCommand1.Connection = this.oleorGentaDbConx;
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_ItemKey", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ItemKey", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_Address", System.Data.OleDb.OleDbType.VarWChar, 250, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Address", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_Address1", System.Data.OleDb.OleDbType.VarWChar, 250, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Address", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CellPhone", System.Data.OleDb.OleDbType.VarWChar, 25, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CellPhone", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CellPhone1", System.Data.OleDb.OleDbType.VarWChar, 25, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CellPhone", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_Done", System.Data.OleDb.OleDbType.Boolean, 2, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Done", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_DoneDate", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "DoneDate", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_DoneDate1", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "DoneDate", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EnteredDate", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EnteredDate", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EnteredDate1", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EnteredDate", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_HasNote", System.Data.OleDb.OleDbType.Boolean, 2, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "HasNote", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_HomePhone", System.Data.OleDb.OleDbType.VarWChar, 25, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "HomePhone", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_HomePhone1", System.Data.OleDb.OleDbType.VarWChar, 25, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "HomePhone", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_Link", System.Data.OleDb.OleDbType.VarWChar, 250, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Link", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_Link1", System.Data.OleDb.OleDbType.VarWChar, 250, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Link", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_Priority", System.Data.OleDb.OleDbType.SmallInt, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Priority", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_Priority1", System.Data.OleDb.OleDbType.SmallInt, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Priority", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_Recurrence", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Recurrence", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_Recurrence1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Recurrence", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TextItem", System.Data.OleDb.OleDbType.VarWChar, 255, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "TextItem", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_WhenDate", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "WhenDate", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_WhenDate1", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "WhenDate", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_WorkPhone", System.Data.OleDb.OleDbType.VarWChar, 25, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "WorkPhone", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_WorkPhone1", System.Data.OleDb.OleDbType.VarWChar, 25, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "WorkPhone", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_eMail", System.Data.OleDb.OleDbType.VarWChar, 75, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "eMail", System.Data.DataRowVersion.Original, null));
            this.oleDbDeleteCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_eMail1", System.Data.OleDb.OleDbType.VarWChar, 75, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "eMail", System.Data.DataRowVersion.Original, null));
            // 
            // oleDbInsertCommand1
            // 
            this.oleDbInsertCommand1.CommandText = "INSERT INTO Items(HasNote, TextItem, WhenDate, Done, DoneDate, Priority, WorkPhon" +
                "e, CellPhone, HomePhone, eMail, Address, EnteredDate, Link, Recurrence) VALUES (" +
                "?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
            this.oleDbInsertCommand1.Connection = this.oleorGentaDbConx;
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("HasNote", System.Data.OleDb.OleDbType.Boolean, 2, "HasNote"));
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TextItem", System.Data.OleDb.OleDbType.VarWChar, 255, "TextItem"));
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("WhenDate", System.Data.OleDb.OleDbType.DBDate, 0, "WhenDate"));
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Done", System.Data.OleDb.OleDbType.Boolean, 2, "Done"));
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("DoneDate", System.Data.OleDb.OleDbType.DBDate, 0, "DoneDate"));
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Priority", System.Data.OleDb.OleDbType.SmallInt, 0, "Priority"));
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("WorkPhone", System.Data.OleDb.OleDbType.VarWChar, 25, "WorkPhone"));
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CellPhone", System.Data.OleDb.OleDbType.VarWChar, 25, "CellPhone"));
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("HomePhone", System.Data.OleDb.OleDbType.VarWChar, 25, "HomePhone"));
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("eMail", System.Data.OleDb.OleDbType.VarWChar, 75, "eMail"));
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Address", System.Data.OleDb.OleDbType.VarWChar, 250, "Address"));
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("EnteredDate", System.Data.OleDb.OleDbType.DBDate, 0, "EnteredDate"));
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Link", System.Data.OleDb.OleDbType.VarWChar, 250, "Link"));
            this.oleDbInsertCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Recurrence", System.Data.OleDb.OleDbType.VarWChar, 50, "Recurrence"));
            // 
            // oleDbSelectCommand1
            // 
            this.oleDbSelectCommand1.CommandText = "SELECT ItemKey, HasNote, TextItem, WhenDate, Done, DoneDate, Priority, WorkPhone," +
                " CellPhone, HomePhone, eMail, Address, EnteredDate, Link, Recurrence FROM Items " +
                "WHERE (Recurrence <> \'syskey\') OR (Recurrence IS NULL)";
            this.oleDbSelectCommand1.Connection = this.oleorGentaDbConx;
            // 
            // oleDbUpdateCommand1
            // 
            this.oleDbUpdateCommand1.CommandText = @"UPDATE Items SET HasNote = ?, 
				TextItem = ?, WhenDate = ?, Done = ?, DoneDate = ?, Priority = ?, WorkPhone 
				= ?, CellPhone = ?, HomePhone = ?, eMail = ?, Address = ?, EnteredDate
				= ?, Link = ?, Recurrence = ? WHERE (ItemKey = ?) AND (Address = ? OR
				? IS NULL AND Address IS NULL) AND (CellPhone = ? OR ? IS NULL AND CellPhone IS NULL) 
				AND (Done = ?) AND (DoneDate = ? OR ? IS NULL AND DoneDate IS NULL) AND 
				(EnteredDate = ? OR ? IS NULL AND EnteredDate IS NULL) AND (HasNote = ?)
				AND (HomePhone = ? OR ? IS NULL AND HomePhone IS NULL) AND (Link = ? OR ? IS
				NULL AND Link IS NULL) AND (Priority = ? OR ? IS NULL AND Priority IS NULL) 
				AND (Recurrence = ? OR ? IS NULL AND Recurrence IS NULL) AND (TextItem =
				?) AND (WhenDate = ? OR ? IS NULL AND WhenDate IS NULL) AND (WorkPhone = ? 
				OR ? IS NULL AND WorkPhone IS NULL) AND (eMail = ? OR ? IS NULL AND eMail IS 
				NULL)";
            this.oleDbUpdateCommand1.Connection = this.oleorGentaDbConx;
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("HasNote", System.Data.OleDb.OleDbType.Boolean, 2, "HasNote"));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("TextItem", System.Data.OleDb.OleDbType.VarWChar, 255, "TextItem"));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("WhenDate", System.Data.OleDb.OleDbType.DBDate, 0, "WhenDate"));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Done", System.Data.OleDb.OleDbType.Boolean, 2, "Done"));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("DoneDate", System.Data.OleDb.OleDbType.DBDate, 0, "DoneDate"));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Priority", System.Data.OleDb.OleDbType.SmallInt, 0, "Priority"));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("WorkPhone", System.Data.OleDb.OleDbType.VarWChar, 25, "WorkPhone"));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("CellPhone", System.Data.OleDb.OleDbType.VarWChar, 25, "CellPhone"));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("HomePhone", System.Data.OleDb.OleDbType.VarWChar, 25, "HomePhone"));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("eMail", System.Data.OleDb.OleDbType.VarWChar, 75, "eMail"));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Address", System.Data.OleDb.OleDbType.VarWChar, 250, "Address"));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("EnteredDate", System.Data.OleDb.OleDbType.DBDate, 0, "EnteredDate"));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Link", System.Data.OleDb.OleDbType.VarWChar, 250, "Link"));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Recurrence", System.Data.OleDb.OleDbType.VarWChar, 50, "Recurrence"));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_ItemKey", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "ItemKey", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_Address", System.Data.OleDb.OleDbType.VarWChar, 250, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Address", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_Address1", System.Data.OleDb.OleDbType.VarWChar, 250, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Address", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CellPhone", System.Data.OleDb.OleDbType.VarWChar, 25, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CellPhone", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_CellPhone1", System.Data.OleDb.OleDbType.VarWChar, 25, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "CellPhone", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_Done", System.Data.OleDb.OleDbType.Boolean, 2, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Done", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_DoneDate", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "DoneDate", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_DoneDate1", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "DoneDate", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EnteredDate", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EnteredDate", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_EnteredDate1", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "EnteredDate", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_HasNote", System.Data.OleDb.OleDbType.Boolean, 2, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "HasNote", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_HomePhone", System.Data.OleDb.OleDbType.VarWChar, 25, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "HomePhone", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_HomePhone1", System.Data.OleDb.OleDbType.VarWChar, 25, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "HomePhone", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_Link", System.Data.OleDb.OleDbType.VarWChar, 250, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Link", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_Link1", System.Data.OleDb.OleDbType.VarWChar, 250, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Link", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_Priority", System.Data.OleDb.OleDbType.SmallInt, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Priority", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_Priority1", System.Data.OleDb.OleDbType.SmallInt, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Priority", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_Recurrence", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Recurrence", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_Recurrence1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "Recurrence", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_TextItem", System.Data.OleDb.OleDbType.VarWChar, 255, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "TextItem", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_WhenDate", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "WhenDate", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_WhenDate1", System.Data.OleDb.OleDbType.DBDate, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "WhenDate", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_WorkPhone", System.Data.OleDb.OleDbType.VarWChar, 25, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "WorkPhone", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_WorkPhone1", System.Data.OleDb.OleDbType.VarWChar, 25, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "WorkPhone", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_eMail", System.Data.OleDb.OleDbType.VarWChar, 75, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "eMail", System.Data.DataRowVersion.Original, null));
            this.oleDbUpdateCommand1.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_eMail1", System.Data.OleDb.OleDbType.VarWChar, 75, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "eMail", System.Data.DataRowVersion.Original, null));
            // 
            // ItemsMemImage
            // 
            this.ItemsMemImage.DataSetName = "ItemsDataSet";
            this.ItemsMemImage.Locale = new System.Globalization.CultureInfo("en-US");
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.DataGrid = this.ItemGrid;
            this.dataGridTableStyle1.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
																												  this.dataGridTextBoxColumn1,
																												  this.dataGridTextBoxColumn4,
																												  this.dataGridTextBoxColumn2,
																												  this.dataGridTextBoxColumn3,
																												  this.dataGridTextBoxColumn5,
																												  this.dataGridTextBoxColumn6,
																												  this.dataGridTextBoxColumn7,
																												  this.dataGridTextBoxColumn8,
																												  this.dataGridTextBoxColumn9,
																												  this.dataGridTextBoxColumn10,
																												  this.dataGridTextBoxColumn11,
																												  this.dataGridTextBoxColumn14,
																												  this.dataGridTextBoxColumn12,
																												  this.dataGridTextBoxColumn13});
            this.dataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridTableStyle1.MappingName = "Items";
            this.dataGridTableStyle1.PreferredRowHeight = 18;
            // 
            // ItemGrid
            // 
            this.ItemGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ItemGrid.CaptionVisible = false;
            this.ItemGrid.ContextMenu = this.ctxMenuGrid;
            this.ItemGrid.DataMember = "Items";
            this.ItemGrid.DataSource = this.ItemsMemImage;
            this.ItemGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ItemGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.ItemGrid.Location = new System.Drawing.Point(152, 32);
            this.ItemGrid.Name = "ItemGrid";
            this.ItemGrid.PreferredRowHeight = 18;
            this.ItemGrid.Size = new System.Drawing.Size(568, 453);
            this.ItemGrid.TabIndex = 14;
            this.ItemGrid.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
																								 this.dataGridTableStyle1});
            this.ItemGrid.Visible = false;
            this.ItemGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ItemGrid_KeyDown);
            this.ItemGrid.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ItemGrid_KeyUp);
            this.ItemGrid.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ItemGrid_MouseUp);
            this.ItemGrid.CurrentCellChanged += new System.EventHandler(this.ItemGrid_CurrCellChanged);
            this.ItemGrid.Scroll += new System.EventHandler(this.ItemGrid_Scroll);
            // 
            // ctxMenuGrid
            // 
            this.ctxMenuGrid.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						this.ctxZoom,
																						this.ctxOccursAgain,
																						this.ctxAssign,
																						this.menuItem16,
																						this.ctxPrintItem,
																						this.menuItem20,
																						this.ctxDelete});
            // 
            // ctxZoom
            // 
            this.ctxZoom.Index = 0;
            this.ctxZoom.Text = orGentaResources.mnuZoomItem;
            this.ctxZoom.Click += new System.EventHandler(this.menuZoomItem_Click);
            // 
            // ctxOccursAgain
            // 
            this.ctxOccursAgain.Index = 1;
            this.ctxOccursAgain.Text = orGentaResources.mnuOccursAgain;
            this.ctxOccursAgain.Click += new System.EventHandler(this.menuItemOccursAgain_Click);
            // 
            // ctxAssign
            // 
            this.ctxAssign.Index = 2;
            this.ctxAssign.Text = orGentaResources.mnuAssign;
            this.ctxAssign.Click += new System.EventHandler(this.menuAssignTo_Click);
            // 
            // menuItem16
            // 
            this.menuItem16.Index = 3;
            this.menuItem16.Text = "-";
            // 
            // ctxPrintItem
            // 
            this.ctxPrintItem.Index = 4;
            this.ctxPrintItem.Text = orGentaResources.mnuPrintItem;
            this.ctxPrintItem.Click += new System.EventHandler(this.menuItemPrint_Click);
            // 
            // menuItem20
            // 
            this.menuItem20.Index = 5;
            this.menuItem20.Text = "-";
            // 
            // ctxDelete
            // 
            this.ctxDelete.Index = 6;
            this.ctxDelete.Text = orGentaResources.mnuDelete;
            this.ctxDelete.Click += new System.EventHandler(this.ctxDelete_Click);
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.HeaderText = orGentaResources.hdrKey;
            this.dataGridTextBoxColumn1.MappingName = "ItemKey";
            this.dataGridTextBoxColumn1.ReadOnly = true;
            this.dataGridTextBoxColumn1.Width = 30;
            // 
            // dataGridTextBoxColumn4
            // 
            this.dataGridTextBoxColumn4.AllowNull = false;
            this.dataGridTextBoxColumn4.FalseValue = false;
            this.dataGridTextBoxColumn4.HeaderText = orGentaResources.hdrNote;
            this.dataGridTextBoxColumn4.MappingName = "HasNote";
            this.dataGridTextBoxColumn4.NullValue = ((object)(resources.GetObject("dataGridTextBoxColumn4.NullValue")));
            this.dataGridTextBoxColumn4.TrueValue = true;
            this.dataGridTextBoxColumn4.Width = 35;
            // 
            // dataGridTextBoxColumn2
            // 
            this.dataGridTextBoxColumn2.Format = "";
            this.dataGridTextBoxColumn2.FormatInfo = null;
            this.dataGridTextBoxColumn2.HeaderText = orGentaResources.hdrItem;
            this.dataGridTextBoxColumn2.MappingName = "TextItem";
            this.dataGridTextBoxColumn2.Width = 275;
            // 
            // dataGridTextBoxColumn3
            // 
            this.dataGridTextBoxColumn3.Format = "";
            this.dataGridTextBoxColumn3.FormatInfo = null;
            this.dataGridTextBoxColumn3.HeaderText = orGentaResources.hdrWhen;
            this.dataGridTextBoxColumn3.MappingName = "WhenDate";
            this.dataGridTextBoxColumn3.NullText = "";
            this.dataGridTextBoxColumn3.Width = 75;
            // 
            // dataGridTextBoxColumn5
            // 
            this.dataGridTextBoxColumn5.AllowNull = false;
            this.dataGridTextBoxColumn5.FalseValue = false;
            this.dataGridTextBoxColumn5.HeaderText = orGentaResources.hdrDone;
            this.dataGridTextBoxColumn5.MappingName = "Done";
            this.dataGridTextBoxColumn5.NullValue = ((object)(resources.GetObject("dataGridTextBoxColumn5.NullValue")));
            this.dataGridTextBoxColumn5.TrueValue = true;
            this.dataGridTextBoxColumn5.Width = 35;
            // 
            // dataGridTextBoxColumn6
            // 
            this.dataGridTextBoxColumn6.Format = "";
            this.dataGridTextBoxColumn6.FormatInfo = null;
            this.dataGridTextBoxColumn6.HeaderText = orGentaResources.hdrFinished;
            this.dataGridTextBoxColumn6.MappingName = "DoneDate";
            this.dataGridTextBoxColumn6.NullText = "";
            this.dataGridTextBoxColumn6.Width = 75;
            // 
            // dataGridTextBoxColumn7
            // 
            this.dataGridTextBoxColumn7.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataGridTextBoxColumn7.Format = "";
            this.dataGridTextBoxColumn7.FormatInfo = null;
            this.dataGridTextBoxColumn7.HeaderText = orGentaResources.hdrPriority;
            this.dataGridTextBoxColumn7.MappingName = "Priority";
            this.dataGridTextBoxColumn7.NullText = "";
            this.dataGridTextBoxColumn7.Width = 35;
            // 
            // dataGridTextBoxColumn8
            // 
            this.dataGridTextBoxColumn8.Format = "";
            this.dataGridTextBoxColumn8.FormatInfo = null;
            this.dataGridTextBoxColumn8.HeaderText = orGentaResources.hdrWorkPhone;
            this.dataGridTextBoxColumn8.MappingName = "WorkPhone";
            this.dataGridTextBoxColumn8.NullText = "";
            this.dataGridTextBoxColumn8.Width = 75;
            // 
            // dataGridTextBoxColumn9
            // 
            this.dataGridTextBoxColumn9.Format = "";
            this.dataGridTextBoxColumn9.FormatInfo = null;
            this.dataGridTextBoxColumn9.HeaderText = orGentaResources.hdrCellPhone;
            this.dataGridTextBoxColumn9.MappingName = "CellPhone";
            this.dataGridTextBoxColumn9.NullText = "";
            this.dataGridTextBoxColumn9.Width = 75;
            // 
            // dataGridTextBoxColumn10
            // 
            this.dataGridTextBoxColumn10.Format = "";
            this.dataGridTextBoxColumn10.FormatInfo = null;
            this.dataGridTextBoxColumn10.HeaderText = orGentaResources.hdrHomePhone;
            this.dataGridTextBoxColumn10.MappingName = "HomePhone";
            this.dataGridTextBoxColumn10.NullText = "";
            this.dataGridTextBoxColumn10.Width = 75;
            // 
            // dataGridTextBoxColumn11
            // 
            this.dataGridTextBoxColumn11.Format = "";
            this.dataGridTextBoxColumn11.FormatInfo = null;
            this.dataGridTextBoxColumn11.HeaderText = orGentaResources.hdrEmail;
            this.dataGridTextBoxColumn11.MappingName = "eMail";
            this.dataGridTextBoxColumn11.NullText = "";
            this.dataGridTextBoxColumn11.Width = 250;
            // 
            // dataGridTextBoxColumn14
            // 
            this.dataGridTextBoxColumn14.Format = "";
            this.dataGridTextBoxColumn14.FormatInfo = null;
            this.dataGridTextBoxColumn14.HeaderText = orGentaResources.hdrWww;
            this.dataGridTextBoxColumn14.MappingName = "Link";
            this.dataGridTextBoxColumn14.NullText = "";
            this.dataGridTextBoxColumn14.Width = 250;
            // 
            // dataGridTextBoxColumn12
            // 
            this.dataGridTextBoxColumn12.Format = "";
            this.dataGridTextBoxColumn12.FormatInfo = null;
            this.dataGridTextBoxColumn12.HeaderText = orGentaResources.hdrAddress;
            this.dataGridTextBoxColumn12.MappingName = "Address";
            this.dataGridTextBoxColumn12.NullText = "";
            this.dataGridTextBoxColumn12.Width = 250;
            // 
            // dataGridTextBoxColumn13
            // 
            this.dataGridTextBoxColumn13.Format = "";
            this.dataGridTextBoxColumn13.FormatInfo = null;
            this.dataGridTextBoxColumn13.HeaderText = orGentaResources.hdrEntered;
            this.dataGridTextBoxColumn13.MappingName = "EnteredDate";
            this.dataGridTextBoxColumn13.NullText = "";
            this.dataGridTextBoxColumn13.Width = 75;
            // 
            // ImportItemsDialog
            // 
            this.ImportItemsDialog.DefaultExt = "txt";
            this.ImportItemsDialog.Filter = orGentaResources.TextAndAllFiles;
            this.ImportItemsDialog.InitialDirectory = "c:\\";
            this.ImportItemsDialog.Title = orGentaResources.ImportItemPrompt;
            // 
            // ExportItemsDialog
            // 
            this.ExportItemsDialog.DefaultExt = "csv";
            this.ExportItemsDialog.Filter = orGentaResources.CommaSep;
            this.ExportItemsDialog.InitialDirectory = "c:\\";
            this.ExportItemsDialog.Title = orGentaResources.SelectExportFile;
            // 
            // prtGridPrintDoc
            // 
            this.prtGridPrintDoc.DocumentName = "orGenta Tasks";
            this.prtGridPrintDoc.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.prtGridPrintDoc_BeginPrint);
            this.prtGridPrintDoc.EndPrint += new System.Drawing.Printing.PrintEventHandler(this.prtGridPrintDoc_EndPrint);
            this.prtGridPrintDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.prtGridPrintDoc_PrintPage);
            // 
            // prtOrgentaPreviewDialog
            // 
            this.prtOrgentaPreviewDialog.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.prtOrgentaPreviewDialog.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.prtOrgentaPreviewDialog.ClientSize = new System.Drawing.Size(400, 300);
            this.prtOrgentaPreviewDialog.Document = this.prtGridPrintDoc;
            this.prtOrgentaPreviewDialog.Enabled = true;
            this.prtOrgentaPreviewDialog.Icon = ((System.Drawing.Icon)(resources.GetObject("prtOrgentaPreviewDialog.Icon")));
            this.prtOrgentaPreviewDialog.Location = new System.Drawing.Point(146, 17);
            this.prtOrgentaPreviewDialog.MinimumSize = new System.Drawing.Size(375, 250);
            this.prtOrgentaPreviewDialog.Name = "prtOrgentaPreviewDialog";
            this.prtOrgentaPreviewDialog.TransparencyKey = System.Drawing.Color.Empty;
            this.prtOrgentaPreviewDialog.Visible = false;
            // 
            // orGentaPageSetup
            // 
            this.orGentaPageSetup.Document = this.prtGridPrintDoc;
            // 
            // pnlForButtons
            // 
            this.pnlForButtons.Controls.Add(this.tBarQuickBtns);
            this.pnlForButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlForButtons.Location = new System.Drawing.Point(0, 0);
            this.pnlForButtons.Name = "pnlForButtons";
            this.pnlForButtons.Size = new System.Drawing.Size(720, 28);
            this.pnlForButtons.TabIndex = 9;
            // 
            // tBarQuickBtns
            // 
            this.tBarQuickBtns.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.tBarQuickBtns.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																							 this.tBBblank1,
																							 this.tBBprint,
																							 this.tBBprintItem,
																							 this.tBBblank2,
																							 this.tBBtrayed,
																							 this.tBBblank3,
																							 this.tBBfindCategory,
																							 this.tBBfindItem,
																							 this.tBBfindNote,
																							 this.tBBfindNext,
																							 this.tBBFindDupe,
																							 this.tBBblank4,
																							 this.tBBassign,
																							 this.tBBblank5,
																							 this.tBBshowDone,
																							 this.tBBblank6,
																							 this.tBBexpandNode,
																							 this.tBBExpandOnly,
																							 this.tBBcollapseNode,
																							 this.tBBblank7,
																							 this.tBBcalendar,
																							 this.tBBblank8,
																							 this.tBBsortCats,
																							 this.tBBsortItems});
            this.tBarQuickBtns.ButtonSize = new System.Drawing.Size(18, 18);
            this.tBarQuickBtns.DropDownArrows = true;
            this.tBarQuickBtns.ImageList = this.iListTbarBtns;
            this.tBarQuickBtns.Location = new System.Drawing.Point(0, 0);
            this.tBarQuickBtns.Name = "tBarQuickBtns";
            this.tBarQuickBtns.ShowToolTips = true;
            this.tBarQuickBtns.Size = new System.Drawing.Size(720, 28);
            this.tBarQuickBtns.TabIndex = 0;
            this.tBarQuickBtns.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.tBarQuickBtns_ButtonClick);
            // 
            // tBBblank1
            // 
            this.tBBblank1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tBBprint
            // 
            this.tBBprint.ImageIndex = 0;
            this.tBBprint.ToolTipText = orGentaResources.mnuPagePrint;
            // 
            // tBBprintItem
            // 
            this.tBBprintItem.ImageIndex = 1;
            this.tBBprintItem.ToolTipText = orGentaResources.mnuItemPrint;
            // 
            // tBBblank2
            // 
            this.tBBblank2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tBBtrayed
            // 
            this.tBBtrayed.ImageIndex = 2;
            this.tBBtrayed.ToolTipText = orGentaResources.mnuTrayed;
            // 
            // tBBblank3
            // 
            this.tBBblank3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tBBfindCategory
            // 
            this.tBBfindCategory.ImageIndex = 3;
            this.tBBfindCategory.ToolTipText = orGentaResources.mnuFindCategory;
            // 
            // tBBfindItem
            // 
            this.tBBfindItem.ImageIndex = 4;
            this.tBBfindItem.ToolTipText = orGentaResources.mnuFindItem;
            // 
            // tBBfindNote
            // 
            this.tBBfindNote.ImageIndex = 5;
            this.tBBfindNote.ToolTipText = orGentaResources.mnuFindNote;
            // 
            // tBBfindNext
            // 
            this.tBBfindNext.ImageIndex = 6;
            this.tBBfindNext.ToolTipText = orGentaResources.mnuFindNext;
            // 
            // tBBFindDupe
            // 
            this.tBBFindDupe.ImageIndex = 16;
            this.tBBFindDupe.ToolTipText = orGentaResources.mnuOccursAgain;
            // 
            // tBBblank4
            // 
            this.tBBblank4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tBBassign
            // 
            this.tBBassign.ImageIndex = 7;
            this.tBBassign.ToolTipText = orGentaResources.mnuAssign;
            // 
            // tBBblank5
            // 
            this.tBBblank5.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tBBshowDone
            // 
            this.tBBshowDone.ImageIndex = 8;
            this.tBBshowDone.ToolTipText = orGentaResources.mnuShowDone;
            // 
            // tBBblank6
            // 
            this.tBBblank6.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tBBexpandNode
            // 
            this.tBBexpandNode.ImageIndex = 10;
            this.tBBexpandNode.ToolTipText = orGentaResources.mnuExpandNode;
            // 
            // tBBExpandOnly
            // 
            this.tBBExpandOnly.ImageIndex = 11;
            this.tBBExpandOnly.ToolTipText = orGentaResources.mnuExpandThis;
            // 
            // tBBcollapseNode
            // 
            this.tBBcollapseNode.ImageIndex = 12;
            this.tBBcollapseNode.ToolTipText = orGentaResources.mnuCollapseNode;
            // 
            // tBBblank7
            // 
            this.tBBblank7.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tBBcalendar
            // 
            this.tBBcalendar.ImageIndex = 13;
            this.tBBcalendar.ToolTipText = orGentaResources.mnuCalendar;
            // 
            // tBBblank8
            // 
            this.tBBblank8.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tBBsortCats
            // 
            this.tBBsortCats.ImageIndex = 14;
            this.tBBsortCats.ToolTipText = orGentaResources.mnuSortCategories;
            // 
            // tBBsortItems
            // 
            this.tBBsortItems.ImageIndex = 15;
            this.tBBsortItems.ToolTipText = orGentaResources.mnuSortItems;
            // 
            // iListTbarBtns
            // 
            this.iListTbarBtns.ImageSize = new System.Drawing.Size(16, 16);
            this.iListTbarBtns.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iListTbarBtns.ImageStream")));
            this.iListTbarBtns.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // pnlNotTheButtons
            // 
            this.pnlNotTheButtons.Controls.Add(this.picLockedBox);
            this.pnlNotTheButtons.Controls.Add(this.pnlItemFind);
            this.pnlNotTheButtons.Controls.Add(this.pnlNoteFind);
            this.pnlNotTheButtons.Controls.Add(this.lblHorizSep);
            this.pnlNotTheButtons.Controls.Add(this.splGridSplitter);
            this.pnlNotTheButtons.Controls.Add(this.pnlCatFind);
            this.pnlNotTheButtons.Controls.Add(this.catInvisLabel);
            this.pnlNotTheButtons.Controls.Add(this.ItemGrid);
            this.pnlNotTheButtons.Controls.Add(this.CategoryHierTV);
            this.pnlNotTheButtons.Controls.Add(this.lblLoading);
            this.pnlNotTheButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlNotTheButtons.DockPadding.Top = 32;
            this.pnlNotTheButtons.Location = new System.Drawing.Point(0, 0);
            this.pnlNotTheButtons.Name = "pnlNotTheButtons";
            this.pnlNotTheButtons.Size = new System.Drawing.Size(720, 485);
            this.pnlNotTheButtons.TabIndex = 10;
            // 
            // picLockedBox
            // 
            this.picLockedBox.Enabled = false;
            this.picLockedBox.Image = ((System.Drawing.Image)(resources.GetObject("picLockedBox.Image")));
            this.picLockedBox.Location = new System.Drawing.Point(328, 36);
            this.picLockedBox.Name = "picLockedBox";
            this.picLockedBox.Size = new System.Drawing.Size(12, 12);
            this.picLockedBox.TabIndex = 13;
            this.picLockedBox.TabStop = false;
            this.picLockedBox.Visible = false;
            // 
            // pnlItemFind
            // 
            this.pnlItemFind.Controls.Add(this.txtItemSearch);
            this.pnlItemFind.Controls.Add(this.label1);
            this.pnlItemFind.Location = new System.Drawing.Point(325, 56);
            this.pnlItemFind.Name = "pnlItemFind";
            this.pnlItemFind.Size = new System.Drawing.Size(143, 51);
            this.pnlItemFind.TabIndex = 11;
            this.pnlItemFind.Visible = false;
            // 
            // txtItemSearch
            // 
            this.txtItemSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtItemSearch.Location = new System.Drawing.Point(6, 24);
            this.txtItemSearch.Name = "txtItemSearch";
            this.txtItemSearch.Size = new System.Drawing.Size(128, 20);
            this.txtItemSearch.TabIndex = 1;
            this.txtItemSearch.Text = "";
            this.txtItemSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtItemSearch_KeyPress);
            this.txtItemSearch.Validating += new System.ComponentModel.CancelEventHandler(this.txtItemSearch_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = orGentaResources.SearchPrompt;
            // 
            // pnlNoteFind
            // 
            this.pnlNoteFind.Controls.Add(this.txtNoteSearch);
            this.pnlNoteFind.Controls.Add(this.label2);
            this.pnlNoteFind.Location = new System.Drawing.Point(165, 56);
            this.pnlNoteFind.Name = "pnlNoteFind";
            this.pnlNoteFind.Size = new System.Drawing.Size(143, 51);
            this.pnlNoteFind.TabIndex = 12;
            this.pnlNoteFind.Visible = false;
            // 
            // txtNoteSearch
            // 
            this.txtNoteSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNoteSearch.Location = new System.Drawing.Point(6, 24);
            this.txtNoteSearch.Name = "txtNoteSearch";
            this.txtNoteSearch.Size = new System.Drawing.Size(128, 20);
            this.txtNoteSearch.TabIndex = 1;
            this.txtNoteSearch.Text = "";
            this.txtNoteSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNoteSearch_KeyPress);
            this.txtNoteSearch.Validating += new System.ComponentModel.CancelEventHandler(this.txtNoteSearch_Validating);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = orGentaResources.SearchPrompt;
            // 
            // lblHorizSep
            // 
            this.lblHorizSep.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHorizSep.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblHorizSep.Location = new System.Drawing.Point(0, 27);
            this.lblHorizSep.Name = "lblHorizSep";
            this.lblHorizSep.Size = new System.Drawing.Size(900, 2);
            this.lblHorizSep.TabIndex = 17;
            // 
            // splGridSplitter
            // 
            this.splGridSplitter.Location = new System.Drawing.Point(152, 32);
            this.splGridSplitter.Name = "splGridSplitter";
            this.splGridSplitter.Size = new System.Drawing.Size(1, 453);
            this.splGridSplitter.TabIndex = 16;
            this.splGridSplitter.TabStop = false;
            // 
            // pnlCatFind
            // 
            this.pnlCatFind.Controls.Add(this.txtCatSearch);
            this.pnlCatFind.Controls.Add(this.lblFindCat);
            this.pnlCatFind.Location = new System.Drawing.Point(5, 56);
            this.pnlCatFind.Name = "pnlCatFind";
            this.pnlCatFind.Size = new System.Drawing.Size(143, 51);
            this.pnlCatFind.TabIndex = 10;
            this.pnlCatFind.Visible = false;
            // 
            // txtCatSearch
            // 
            this.txtCatSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCatSearch.Location = new System.Drawing.Point(6, 24);
            this.txtCatSearch.Name = "txtCatSearch";
            this.txtCatSearch.Size = new System.Drawing.Size(128, 20);
            this.txtCatSearch.TabIndex = 1;
            this.txtCatSearch.Text = "";
            this.txtCatSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCatSearch_KeyPress);
            this.txtCatSearch.Validating += new System.ComponentModel.CancelEventHandler(this.txtCatSearch_Validating);
            // 
            // lblFindCat
            // 
            this.lblFindCat.Location = new System.Drawing.Point(8, 6);
            this.lblFindCat.Name = "lblFindCat";
            this.lblFindCat.Size = new System.Drawing.Size(120, 16);
            this.lblFindCat.TabIndex = 0;
            this.lblFindCat.Text = orGentaResources.SearchPrompt;
            // 
            // catInvisLabel
            // 
            this.catInvisLabel.Location = new System.Drawing.Point(2, 34);
            this.catInvisLabel.Name = "catInvisLabel";
            this.catInvisLabel.Size = new System.Drawing.Size(143, 15);
            this.catInvisLabel.TabIndex = 9;
            this.catInvisLabel.Visible = false;
            // 
            // CategoryHierTV
            // 
            this.CategoryHierTV.AllowDrop = true;
            this.CategoryHierTV.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CategoryHierTV.ContextMenu = this.ctxMenuTV;
            this.CategoryHierTV.Dock = System.Windows.Forms.DockStyle.Left;
            this.CategoryHierTV.FullRowSelect = true;
            this.CategoryHierTV.HideSelection = false;
            this.CategoryHierTV.ImageIndex = -1;
            this.CategoryHierTV.Indent = 15;
            this.CategoryHierTV.ItemHeight = 18;
            this.CategoryHierTV.LabelEdit = true;
            this.CategoryHierTV.Location = new System.Drawing.Point(0, 32);
            this.CategoryHierTV.Name = "CategoryHierTV";
            this.CategoryHierTV.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
																					   new System.Windows.Forms.TreeNode("Main", new System.Windows.Forms.TreeNode[] {
																																										 new System.Windows.Forms.TreeNode("Unassigned"),
																																										 new System.Windows.Forms.TreeNode("Add Category"),
																																										 new System.Windows.Forms.TreeNode("TrashCan")})});
            this.CategoryHierTV.SelectedImageIndex = -1;
            this.CategoryHierTV.ShowLines = false;
            this.CategoryHierTV.Size = new System.Drawing.Size(152, 453);
            this.CategoryHierTV.TabIndex = 15;
            this.CategoryHierTV.Resize += new System.EventHandler(this.CategoryHierTV_Resize);
            this.CategoryHierTV.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.CategoryHierTV_AfterExpand);
            this.CategoryHierTV.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.CategoryHierTV_AfterCollapse);
            this.CategoryHierTV.DragOver += new System.Windows.Forms.DragEventHandler(this.CategoryHierTV_DragOver);
            this.CategoryHierTV.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CategoryHierTV_KeyUp);
            this.CategoryHierTV.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.CategoryHierTV_AfterSelect);
            this.CategoryHierTV.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.CategoryHierTV_AfterLabelEdit);
            this.CategoryHierTV.DragEnter += new System.Windows.Forms.DragEventHandler(this.CategoryHierTV_DragEnter);
            this.CategoryHierTV.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.CategoryHierTV_ItemDrag);
            this.CategoryHierTV.DragDrop += new System.Windows.Forms.DragEventHandler(this.CategoryHierTV_DragDrop);
            // 
            // ctxMenuTV
            // 
            this.ctxMenuTV.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.ctxExpandNode,
																					  this.ctxCollapseNode,
																					  this.ctxExapndOnlyThis,
																					  this.ctxFreezeNode});
            this.ctxMenuTV.Popup += new System.EventHandler(this.ctxMenuTV_Popup);
            // 
            // ctxExpandNode
            // 
            this.ctxExpandNode.Index = 0;
            this.ctxExpandNode.Text = orGentaResources.mnuExpandNode;
            this.ctxExpandNode.Click += new System.EventHandler(this.menuExpandNode_Click);
            // 
            // ctxCollapseNode
            // 
            this.ctxCollapseNode.Index = 1;
            this.ctxCollapseNode.Text = orGentaResources.mnuCollapseNode;
            this.ctxCollapseNode.Click += new System.EventHandler(this.menuCollapseNode_Click);
            // 
            // ctxExapndOnlyThis
            // 
            this.ctxExapndOnlyThis.Index = 2;
            this.ctxExapndOnlyThis.Text = orGentaResources.mnuExpandThis;
            this.ctxExapndOnlyThis.Click += new System.EventHandler(this.menuExpandOnlyThis_Click);
            // 
            // ctxFreezeNode
            // 
            this.ctxFreezeNode.Index = 3;
            this.ctxFreezeNode.Text = orGentaResources.mnuFreezeClosed;
            this.ctxFreezeNode.Click += new System.EventHandler(this.ctxFreezeNode_Click);
            // 
            // lblLoading
            // 
            this.lblLoading.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.lblLoading.Location = new System.Drawing.Point(176, 48);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(112, 16);
            this.lblLoading.TabIndex = 18;
            this.lblLoading.Text = orGentaResources.Loading;
            // 
            // trayIconTrayed
            // 
            this.trayIconTrayed.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIconTrayed.Icon")));
            this.trayIconTrayed.Text = "";
            this.trayIconTrayed.DoubleClick += new System.EventHandler(this.trayIconTrayed_DoubleClick);
            this.trayIconTrayed.Click += new System.EventHandler(this.trayIconTrayed_Click);
            // 
            // ViewForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(720, 485);
            this.Controls.Add(this.pnlForButtons);
            this.Controls.Add(this.pnlNotTheButtons);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.menuMain;
            this.MinimizeBox = false;
            this.Name = "ViewForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Closing += new System.ComponentModel.CancelEventHandler(this.ViewForm_Closing);
            ((System.ComponentModel.ISupportInitialize)(this.ItemsMemImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemGrid)).EndInit();
            this.pnlForButtons.ResumeLayout(false);
            this.pnlNotTheButtons.ResumeLayout(false);
            this.pnlItemFind.ResumeLayout(false);
            this.pnlNoteFind.ResumeLayout(false);
            this.pnlCatFind.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

    }
}