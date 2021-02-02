using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Data;
using System.Data.OleDb;

namespace orGenta
{
	
	public class AdvancedPrint : System.Windows.Forms.Form
	{

		#region Variable Initializations
		private System.Windows.Forms.Button btnFonts;
		private System.Windows.Forms.Button btnColumns;
		private System.Windows.Forms.Button btnPrint;
		private System.Windows.Forms.Button btnCancel;
		public System.Drawing.Printing.PrintDocument prtGridPrintDoc;
		private System.Windows.Forms.GroupBox gBoxZoom;
		private System.Windows.Forms.Button btnZoomPlus;
		private System.Windows.Forms.Button btnZoomMinus;
		private System.Windows.Forms.PrintPreviewControl prtPreviewWindow;
		private System.Windows.Forms.FontDialog fontChooser;
		private Font printFont;
		private System.Windows.Forms.PageSetupDialog orGentaPageSetup;
		private System.Windows.Forms.Button btnPrintSetup;
		private FieldSelector GetPrintFields;
		private int ItemRowCount;
		private static string NewItemClickMsg = "Click and Type new item here";
		private int dsLineCounter;
		private int DateWidth;
		private DataGrid PrintItems;
		private ArrayList SavedCategories;
		private ArrayList ItemIDs;
		private DataGridTableStyle TableStylesUsed;
		private OleDbConnection oleorGentaDbConx;
		private bool hasNote;
		private System.ComponentModel.Container components = null;

		#endregion

		#region Entry Point
		public AdvancedPrint(FieldSelector GetFields, DataGrid iGridDataSource, ArrayList RowXrefArray, DataGridTableStyle inpGridStyle, OleDbConnection orgDatabase, ArrayList inpItemIDs)
		{
			InitializeComponent();

			//	Assign values that were passed to local variables
			GetPrintFields = GetFields;
			PrintItems = iGridDataSource;
			ItemRowCount = RowXrefArray.Count;
			SavedCategories = RowXrefArray;
			TableStylesUsed = inpGridStyle;
			oleorGentaDbConx = orgDatabase;
			ItemIDs = inpItemIDs;

			//	Default font value to use
			printFont = new Font("Arial", 10);
		}

		#endregion

		#region Windows Form Designer generated code

		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(AdvancedPrint));
			this.prtPreviewWindow = new System.Windows.Forms.PrintPreviewControl();
			this.prtGridPrintDoc = new System.Drawing.Printing.PrintDocument();
			this.btnFonts = new System.Windows.Forms.Button();
			this.btnColumns = new System.Windows.Forms.Button();
			this.btnPrint = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.gBoxZoom = new System.Windows.Forms.GroupBox();
			this.btnZoomMinus = new System.Windows.Forms.Button();
			this.btnZoomPlus = new System.Windows.Forms.Button();
			this.fontChooser = new System.Windows.Forms.FontDialog();
			this.orGentaPageSetup = new System.Windows.Forms.PageSetupDialog();
			this.btnPrintSetup = new System.Windows.Forms.Button();
			this.gBoxZoom.SuspendLayout();
			this.SuspendLayout();
			// 
			// prtPreviewWindow
			// 
			this.prtPreviewWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.prtPreviewWindow.AutoZoom = false;
			this.prtPreviewWindow.Document = this.prtGridPrintDoc;
			this.prtPreviewWindow.Location = new System.Drawing.Point(8, 8);
			this.prtPreviewWindow.Name = "prtPreviewWindow";
			this.prtPreviewWindow.Size = new System.Drawing.Size(496, 376);
			this.prtPreviewWindow.TabIndex = 0;
			this.prtPreviewWindow.Zoom = 0.75;
			// 
			// prtGridPrintDoc
			// 
			this.prtGridPrintDoc.DocumentName = "orGenta Tasks";
			this.prtGridPrintDoc.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.prtGridPrintDoc_BeginPrint);
			this.prtGridPrintDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.prtGridPrintDoc_PrintPage);
			// 
			// btnFonts
			// 
			this.btnFonts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFonts.Location = new System.Drawing.Point(520, 16);
			this.btnFonts.Name = "btnFonts";
			this.btnFonts.TabIndex = 1;
			this.btnFonts.Text = "Fonts ...";
			this.btnFonts.Click += new System.EventHandler(this.btnFonts_Click);
			// 
			// btnColumns
			// 
			this.btnColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnColumns.Location = new System.Drawing.Point(520, 48);
			this.btnColumns.Name = "btnColumns";
			this.btnColumns.TabIndex = 2;
			this.btnColumns.Text = "Columns ...";
			this.btnColumns.Click += new System.EventHandler(this.btnColumns_Click);
			// 
			// btnPrint
			// 
			this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPrint.Location = new System.Drawing.Point(520, 320);
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.TabIndex = 3;
            this.btnPrint.Text = orGentaResources.Print;
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(520, 352);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = orGentaResources.Cancel;
			// 
			// gBoxZoom
			// 
			this.gBoxZoom.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.gBoxZoom.Controls.Add(this.btnZoomMinus);
			this.gBoxZoom.Controls.Add(this.btnZoomPlus);
			this.gBoxZoom.Location = new System.Drawing.Point(516, 184);
			this.gBoxZoom.Name = "gBoxZoom";
			this.gBoxZoom.Size = new System.Drawing.Size(80, 64);
			this.gBoxZoom.TabIndex = 5;
			this.gBoxZoom.TabStop = false;
			this.gBoxZoom.Text = "Zoom";
			// 
			// btnZoomMinus
			// 
			this.btnZoomMinus.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnZoomMinus.Location = new System.Drawing.Point(44, 24);
			this.btnZoomMinus.Name = "btnZoomMinus";
			this.btnZoomMinus.Size = new System.Drawing.Size(24, 23);
			this.btnZoomMinus.TabIndex = 1;
			this.btnZoomMinus.Text = "-";
			this.btnZoomMinus.Click += new System.EventHandler(this.btnZoomMinus_Click);
			// 
			// btnZoomPlus
			// 
			this.btnZoomPlus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnZoomPlus.Location = new System.Drawing.Point(12, 24);
			this.btnZoomPlus.Name = "btnZoomPlus";
			this.btnZoomPlus.Size = new System.Drawing.Size(24, 23);
			this.btnZoomPlus.TabIndex = 0;
			this.btnZoomPlus.Text = "+";
			this.btnZoomPlus.Click += new System.EventHandler(this.btnZoomPlus_Click);
			// 
			// fontChooser
			// 
			this.fontChooser.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.fontChooser.ShowEffects = false;
			// 
			// orGentaPageSetup
			// 
			this.orGentaPageSetup.Document = this.prtGridPrintDoc;
			// 
			// btnPrintSetup
			// 
			this.btnPrintSetup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPrintSetup.Location = new System.Drawing.Point(520, 80);
			this.btnPrintSetup.Name = "btnPrintSetup";
			this.btnPrintSetup.TabIndex = 6;
			this.btnPrintSetup.Text = "Setup ...";
			this.btnPrintSetup.Click += new System.EventHandler(this.btnPrintSetup_Click);
			// 
			// AdvancedPrint
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(608, 389);
			this.Controls.Add(this.btnPrintSetup);
			this.Controls.Add(this.gBoxZoom);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnPrint);
			this.Controls.Add(this.btnColumns);
			this.Controls.Add(this.btnFonts);
			this.Controls.Add(this.prtPreviewWindow);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "AdvancedPrint";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = orGentaResources.PrintOptions;
			this.gBoxZoom.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region Printing
		private void btnPrint_Click(object sender, System.EventArgs e)
		{
			//	Initiate actual printing
			try
			{
				this.prtGridPrintDoc.Print();
			}
			catch {}
		}

		private void prtGridPrintDoc_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			dsLineCounter = 0;
		}

		private void prtGridPrintDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs ev)
		{
			//	set up initial variables for the page
			float linesPerPage = ev.MarginBounds.Height  / printFont.GetHeight(ev.Graphics);
			float yPos =  0;
			int countOfLinesPrinted = 0;

			//	initialize printing "format" parameter
			StringFormat StringFormatParms = new StringFormat();
			StringFormatParms.FormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.FitBlackBox;

			//	retrieve the current side-margin setttings
			float leftMargin = ev.MarginBounds.Left;
			float topMargin = ev.MarginBounds.Top;

			//	call the subroutines that determine the column widths
			int[] columnWidths = ComputeColWidths(ev.Graphics);
			int[] columnsAdjusted = AdjustColumns(columnWidths, ev.MarginBounds.Width);

			//	compute the starting horizontal location for each column
			int[] colStartLocs = new int[columnsAdjusted.Length];
			int PreviousStartLoc = 0;
			colStartLocs[0] = 0;
			for (int i = 1; i < columnsAdjusted.Length; i++)
			{
				colStartLocs[i] = PreviousStartLoc + columnsAdjusted[i - 1];
				PreviousStartLoc = colStartLocs[i];
			}

			//	initialize work variables for the page
			string PrintWorkLine = "";
			string[] TestCategArray;
			string RightMostCat;
			string PrevPrintCateg = "";
			object dbItemToConvert;

			while ((countOfLinesPrinted < linesPerPage) && (dsLineCounter < ItemRowCount)) 
			{
				//	initialize work variables for the present print line
				int MostLinesThisRow = 0;
				int linesReqdToPrint;
				int charsActuallyPrinted;
				int colCounter;

				//	one column at a time
				foreach(object checkCol in GetPrintFields.chkBoxFields.CheckedItems) 
				{
					if ((PrintItems[dsLineCounter,2].ToString() == "") || (PrintItems[dsLineCounter,2].ToString() == NewItemClickMsg))
					{
						//	bypass the blank and the "Click Here" lines
						break;
					}

					colCounter = GetPrintFields.chkBoxFields.Items.IndexOf(checkCol) - 1;

					if (colCounter == -1)			// category
					{
						//	get the last element in the category path
						TestCategArray = SavedCategories[dsLineCounter].ToString().Split(new char [] {'\\'});
						RightMostCat = TestCategArray[TestCategArray.Length - 2];
						if (PrevPrintCateg == RightMostCat)
						{
							//	supress printing of duplicates from previous line
							PrintWorkLine = "";
						}
						else
						{
							//	print the category name
							PrintWorkLine = RightMostCat;
							PrevPrintCateg = RightMostCat;
						}
					}

					else if (colCounter == 1)			// note gets printed after all items
					{
						hasNote = (bool) PrintItems[dsLineCounter,colCounter];
					}

					else
					{
						//	see if this is one of the Date-type fields
						if (TableStylesUsed.GridColumnStyles[colCounter].MappingName.IndexOf("Date") >= 0)
						{
							//	change display of field to DateTime
							dbItemToConvert = PrintItems[dsLineCounter,colCounter];
							if (dbItemToConvert != DBNull.Value)
							{
								PrintWorkLine = Convert.ToDateTime(dbItemToConvert).ToShortDateString();
							}
							else
							{
								PrintWorkLine = "";
							}
						}
						else
						{
							//	not a date field, just print it as it is
							PrintWorkLine = PrintItems[dsLineCounter,colCounter].ToString();
						}
					}

					//	Print everything except the Note
					if (colCounter != 1)
					{
						//	get our present starting vertical location
						yPos = topMargin + (countOfLinesPrinted * printFont.GetHeight(ev.Graphics));

						//	set the graphical bounds for this print column
						RectangleF OutputRectangle = new RectangleF(ev.MarginBounds.Left + colStartLocs[colCounter + 1], yPos, 
							columnsAdjusted[colCounter + 1], ev.MarginBounds.Height - (yPos - ev.MarginBounds.Top));

						//	do the actual printing
						ev.Graphics.DrawString (PrintWorkLine, printFont, Brushes.Black, OutputRectangle, StringFormatParms);

						//	save the amount of actual lines the printing required
						ev.Graphics.MeasureString(PrintWorkLine, printFont, OutputRectangle.Size, StringFormatParms, out charsActuallyPrinted, out linesReqdToPrint);

						//	if its the maxmimum sofar for this row, then save it
						if (linesReqdToPrint > MostLinesThisRow)
						{
							MostLinesThisRow = linesReqdToPrint;
						}
					}
				}

				//	Notes get printed following the item
				if (hasNote)
				{
					//	Read the note from the dB
					string NoteTextToShow = "";
					int thisItemKey = Convert.ToInt16(ItemIDs[dsLineCounter]);
					string GetItemCmd = "SELECT NoteText FROM Notes WHERE ItemKey = " + thisItemKey.ToString();
					oleorGentaDbConx.Open();
                    using (OleDbCommand NotesCmdBuilder = new OleDbCommand(GetItemCmd, this.oleorGentaDbConx))
                    {
                        try
                        {
                            NoteTextToShow = (string)NotesCmdBuilder.ExecuteScalar();
                            if (NoteTextToShow == null)
                            {
                                NoteTextToShow = "";
                            }
                        }
                        catch
                            { NoteTextToShow = ""; }
                        finally
                            { oleorGentaDbConx.Close(); }
                    }

					//	set current position and allowable area for printing
					yPos = topMargin + ((countOfLinesPrinted + MostLinesThisRow) * printFont.GetHeight(ev.Graphics));
					RectangleF NoteRectangle = new RectangleF(ev.MarginBounds.Left + 50, yPos, 
						ev.MarginBounds.Width, ev.MarginBounds.Height - (yPos - ev.MarginBounds.Top));

					//	print the note and see how large it was
					ev.Graphics.DrawString (NoteTextToShow, printFont, Brushes.Black, NoteRectangle, StringFormatParms);
					ev.Graphics.MeasureString(NoteTextToShow, printFont, NoteRectangle.Size, StringFormatParms, out charsActuallyPrinted, out linesReqdToPrint);
					MostLinesThisRow += linesReqdToPrint;
				}

				//	increment dataset counter and page-line counter
				dsLineCounter++;
				countOfLinesPrinted = countOfLinesPrinted + MostLinesThisRow;
			}

			//	set flagging to show if there's still more
			if (dsLineCounter < ItemRowCount) 
			{
				ev.HasMorePages = true;
			}
			else 
			{
				ev.HasMorePages = false;
			}
		}

		private int[] AdjustColumns(int[] columnWidths, int pageWidthAvail)
		{
			//	first count how many columns are available for shrinking
			int TotalWidth = 0;
			int targetColsToShrink = 0;
			for (int i = 0; i < columnWidths.Length; i++)
			{
				TotalWidth += columnWidths[i];
				if ((columnWidths[i] > DateWidth) && (i > 0))
				{
					targetColsToShrink++;
				}
			}

			//	do we need to shrink anyway?
			if (TotalWidth <= pageWidthAvail)
			{
				return columnWidths;
			}

			//	how much should we shrink each column
			int AmountToShrink = (int) ((TotalWidth - pageWidthAvail) / targetColsToShrink);

			//	go ahead and shrink 'em
			for (int i = 0; i < columnWidths.Length; i++)
			{
				if ((columnWidths[i] > DateWidth) && (i > 0))
				{
					columnWidths[i] = columnWidths[i] - AmountToShrink;
				}
			}
			return columnWidths;
		}
        
		private int[] ComputeColWidths(Graphics pageGraphics)
		{
			//   The array returned matches the layouts of the GetPrintFields
			int[] ComputedWidths = new int[GetPrintFields.chkBoxFields.Items.Count];

			//	initialize working variables
			int colCounter;
			string longestValue;

			//	use a standard long date value for date-formatted fields
			DateWidth = (int) (pageGraphics.MeasureString("12/33/2888", printFont).Width + 2);

			//	find the longest string for each column
			foreach(object checkCol in GetPrintFields.chkBoxFields.CheckedItems) 
			{
				colCounter = GetPrintFields.chkBoxFields.Items.IndexOf(checkCol) - 1;
				if (colCounter == -1)
				{
					//	find the longest category
					int CatCount = 0;
					int maxCatSize = 0;
					int TestCatSize = 0;
					string[] TestCategArray;
					string RightMostCat;
					while (CatCount < SavedCategories.Count)
					{
						//	get the rightmost subcategory for each node
						TestCategArray = SavedCategories[CatCount].ToString().Split(new char [] {'\\'});
						RightMostCat = TestCategArray[TestCategArray.Length - 1];

						//	we only care about it if the category is expanded
						if (RightMostCat == "-")
						{
							//	use the category name before the dash
							RightMostCat = TestCategArray[TestCategArray.Length - 2];
							TestCatSize = (int) (pageGraphics.MeasureString(RightMostCat, printFont).Width + 2);
							if (TestCatSize > maxCatSize)
							{
								maxCatSize = TestCatSize;
							}
						}
						CatCount++;
					}

					//	save the size of the largest category
					ComputedWidths[0] = maxCatSize;
				}
				else if (colCounter == 1)
				{
					//	notes don't have their own column, skip
					ComputedWidths[2] = 0;
				}
				else
				{
					//	all other columns here
					longestValue = "";
					if (TableStylesUsed.GridColumnStyles[colCounter].MappingName.IndexOf("Date") >= 0)
					{
						//	use the precomputed value for date-type fields
						ComputedWidths[colCounter + 1] = DateWidth;
					}
					else
					{
						int maxLenField = 0;
						int rowCounter = 0;

						//	loop through all values for this column
						while (rowCounter < ItemRowCount)
						{
							string testString = PrintItems[rowCounter,colCounter].ToString();
							if (testString.Length > maxLenField)
							{
								maxLenField = testString.Length;
								longestValue = testString;
							}
							rowCounter++;
						}

						//	set size for this column
						ComputedWidths[colCounter + 1] = (int) (pageGraphics.MeasureString(longestValue, printFont).Width * 1.05);
					}
				}
			}
			return ComputedWidths;
		}
		#endregion

		#region Preview and Print Option Settings

		private void btnFonts_Click(object sender, System.EventArgs e)
		{
			//	user is changing the font
			this.fontChooser.ShowDialog(this);
			printFont = this.fontChooser.Font;
			this.prtPreviewWindow.InvalidatePreview();
		}

		private void btnPrintSetup_Click(object sender, System.EventArgs e)
		{
			//	user is changing the printer setup
			this.orGentaPageSetup.ShowDialog(this);
			this.prtPreviewWindow.InvalidatePreview();
		}

		private void btnColumns_Click(object sender, System.EventArgs e)
		{
			//	user is going to select which columns to print
			if (GetPrintFields.ShowDialog(this) == DialogResult.OK)
			{
				this.prtPreviewWindow.InvalidatePreview();
			}
		}

		private void btnZoomPlus_Click(object sender, System.EventArgs e)
		{
			//	user is zooming in
			this.btnZoomMinus.Enabled = true;
			this.prtPreviewWindow.Zoom = this.prtPreviewWindow.Zoom + .25;
			if (this.prtPreviewWindow.Zoom == 2)
			{
				this.btnZoomPlus.Enabled = false;
			}
		}

		private void btnZoomMinus_Click(object sender, System.EventArgs e)
		{
			//	user is zooming out
			this.btnZoomPlus.Enabled = true;
			this.prtPreviewWindow.Zoom = this.prtPreviewWindow.Zoom - .25;
			if (this.prtPreviewWindow.Zoom == .25)
			{
				this.btnZoomMinus.Enabled = false;
			}
		}

		#endregion

		#region Form Disposing
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		#endregion

	}
}
