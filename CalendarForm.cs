using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace orGenta
{
	public class CalendarForm : System.Windows.Forms.Form
	{
		#region Variable Initializations
		private System.Windows.Forms.MonthCalendar calMonthCalendar;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Panel pnlCalBorder;
		private System.Windows.Forms.TextBox tbSeparator1;
		private System.Windows.Forms.TextBox tbSeparator2;
		private int SelectedGridRow;
		private int[] MaxRecordsCopy;
		private System.Data.DataView DatedItemsView;
		private orGenta.ItemsDataSet DatedItemsCopy;
		private System.Windows.Forms.DataGrid dgridTaskList;
		private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;
		private System.Windows.Forms.Button btnShowDone;
		#endregion
		private System.Windows.Forms.Timer tmrCalendarStart;
		private System.ComponentModel.IContainer components;

		#region Windows Form Designer generated code
	
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(CalendarForm));
			this.calMonthCalendar = new System.Windows.Forms.MonthCalendar();
			this.btnClose = new System.Windows.Forms.Button();
			this.pnlCalBorder = new System.Windows.Forms.Panel();
			this.tbSeparator1 = new System.Windows.Forms.TextBox();
			this.tbSeparator2 = new System.Windows.Forms.TextBox();
			this.dgridTaskList = new System.Windows.Forms.DataGrid();
			this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
			this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.btnShowDone = new System.Windows.Forms.Button();
			this.tmrCalendarStart = new System.Windows.Forms.Timer(this.components);
			((System.ComponentModel.ISupportInitialize)(this.dgridTaskList)).BeginInit();
			this.SuspendLayout();
			// 
			// calMonthCalendar
			// 
			this.calMonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 2);
			this.calMonthCalendar.Font = new System.Drawing.Font("Eras Light ITC", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.calMonthCalendar.Location = new System.Drawing.Point(8, 8);
			this.calMonthCalendar.Name = "calMonthCalendar";
			this.calMonthCalendar.ScrollChange = 1;
			this.calMonthCalendar.ShowToday = false;
			this.calMonthCalendar.TabIndex = 0;
			this.calMonthCalendar.TitleBackColor = System.Drawing.SystemColors.Menu;
			this.calMonthCalendar.TitleForeColor = System.Drawing.SystemColors.MenuText;
			this.calMonthCalendar.TrailingForeColor = System.Drawing.SystemColors.ControlLight;
			this.calMonthCalendar.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.calMonthCalendar_DateSelected);
			this.calMonthCalendar.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.calMonthCalendar_DateChanged);
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(600, 416);
			this.btnClose.Name = "btnClose";
			this.btnClose.TabIndex = 2;
			this.btnClose.Text = orGentaResources.Close;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// pnlCalBorder
			// 
			this.pnlCalBorder.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnlCalBorder.Location = new System.Drawing.Point(4, 3);
			this.pnlCalBorder.Name = "pnlCalBorder";
			this.pnlCalBorder.Size = new System.Drawing.Size(241, 400);
			this.pnlCalBorder.TabIndex = 3;
			// 
			// tbSeparator1
			// 
			this.tbSeparator1.AutoSize = false;
			this.tbSeparator1.BackColor = System.Drawing.SystemColors.Highlight;
			this.tbSeparator1.Enabled = false;
			this.tbSeparator1.Location = new System.Drawing.Point(8, 45);
			this.tbSeparator1.Name = "tbSeparator1";
			this.tbSeparator1.Size = new System.Drawing.Size(232, 2);
			this.tbSeparator1.TabIndex = 4;
			this.tbSeparator1.Text = "";
			// 
			// tbSeparator2
			// 
			this.tbSeparator2.AutoSize = false;
			this.tbSeparator2.BackColor = System.Drawing.SystemColors.Highlight;
			this.tbSeparator2.Enabled = false;
			this.tbSeparator2.Location = new System.Drawing.Point(8, 232);
			this.tbSeparator2.Name = "tbSeparator2";
			this.tbSeparator2.Size = new System.Drawing.Size(232, 2);
			this.tbSeparator2.TabIndex = 5;
			this.tbSeparator2.Text = "";
			// 
			// dgridTaskList
			// 
			this.dgridTaskList.CaptionVisible = false;
			this.dgridTaskList.ColumnHeadersVisible = false;
			this.dgridTaskList.DataMember = "";
			this.dgridTaskList.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dgridTaskList.Location = new System.Drawing.Point(250, 3);
			this.dgridTaskList.Name = "dgridTaskList";
			this.dgridTaskList.Size = new System.Drawing.Size(445, 407);
			this.dgridTaskList.TabIndex = 6;
			this.dgridTaskList.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
																									  this.dataGridTableStyle1});
			// 
			// dataGridTableStyle1
			// 
			this.dataGridTableStyle1.DataGrid = this.dgridTaskList;
			this.dataGridTableStyle1.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
																												  this.dataGridTextBoxColumn1,
																												  this.dataGridTextBoxColumn2});
			this.dataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGridTableStyle1.MappingName = "Items";
			// 
			// dataGridTextBoxColumn1
			// 
			this.dataGridTextBoxColumn1.Format = "";
			this.dataGridTextBoxColumn1.FormatInfo = null;
			this.dataGridTextBoxColumn1.MappingName = "WhenDate";
			this.dataGridTextBoxColumn1.ReadOnly = true;
			this.dataGridTextBoxColumn1.Width = 75;
			// 
			// dataGridTextBoxColumn2
			// 
			this.dataGridTextBoxColumn2.Format = "";
			this.dataGridTextBoxColumn2.FormatInfo = null;
			this.dataGridTextBoxColumn2.MappingName = "TextItem";
			this.dataGridTextBoxColumn2.Width = 315;
			// 
			// btnShowDone
			// 
			this.btnShowDone.Location = new System.Drawing.Point(432, 416);
			this.btnShowDone.Name = "btnShowDone";
			this.btnShowDone.TabIndex = 7;
			this.btnShowDone.Text = orGentaResources.ShowDone;
			this.btnShowDone.Click += new System.EventHandler(this.btnShowDone_Click);
			// 
			// tmrCalendarStart
			// 
			this.tmrCalendarStart.Tick += new System.EventHandler(this.tmrCalendarStart_Tick);
			// 
			// CalendarForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(700, 447);
			this.Controls.Add(this.btnShowDone);
			this.Controls.Add(this.tbSeparator2);
			this.Controls.Add(this.tbSeparator1);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.calMonthCalendar);
			this.Controls.Add(this.pnlCalBorder);
			this.Controls.Add(this.dgridTaskList);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "CalendarForm";
			this.Text = orGentaResources.CalendarTitle;
			((System.ComponentModel.ISupportInitialize)(this.dgridTaskList)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		#region Calendar and itemGrid synchronization
		public CalendarForm(ref ItemsDataSet DatedItems, ref int[] MaxRecords)
		{
			InitializeComponent();

			//	MaxRecords was the last item num for future use in adding items
			MaxRecordsCopy = MaxRecords;

			//	Copy of the items dB memory image
			DatedItemsCopy = DatedItems;

			//	set other parameters for the source dataset
			this.DatedItemsCopy.DataSetName = "ItemsDataSet";
			this.DatedItemsCopy.Locale = new System.Globalization.CultureInfo("en-US");

			//	Create a filtered view of the dataset
			this.DatedItemsView = new System.Data.DataView();
			this.DatedItemsView.Table = this.DatedItemsCopy.Tables[0];
			this.DatedItemsView.AllowDelete = false;
			this.DatedItemsView.AllowNew = false;
			this.DatedItemsView.RowFilter = "WhenDate IS NOT NULL AND Done = False";
			this.DatedItemsView.Sort = "WhenDate";

			//	Set source of task list
			this.dgridTaskList.DataSource = this.DatedItemsView;
			sizeTheForm(this.DatedItemsView.Count);

			//	Select next item to happen chronologically
			LocateCurrentItem();
		}

		private void sizeTheForm(int HowManyRows)
		{
			if (HowManyRows < 24)
			{
				this.dgridTaskList.Width = 445 - 16;
				this.Width = 706 - 16;
			}
			else
			{
				this.Width = 706;
				this.dgridTaskList.Width = 445;
			}
			this.Refresh();
		}
		
		private void LocateCurrentItem()
		{
			//	Get the date selected on the Calendar control
			DateTime SelectedCalDate = this.calMonthCalendar.SelectionStart;

			//	Loop until you equal or exceed the selected date
			for (int i = 0; i < DatedItemsView.Count; i++)
			{
				if (Convert.ToDateTime(this.dgridTaskList[i,0]) >=  SelectedCalDate)
				{
					try
					{
						//	Deselect any previous selected row
						this.dgridTaskList.UnSelect(SelectedGridRow);
					}
					catch
					{
					}
					
					//	Select this row
					SelectedGridRow = i;
					this.tmrCalendarStart.Enabled = true;
					break;
				}
			}
		}

		private void btnShowDone_Click(object sender, System.EventArgs e)
		{
			if (this.btnShowDone.Text == orGentaResources.ShowDone)
			{
				//	Change view filter to show done items
				DateTime calShowsfirst  = this.calMonthCalendar.GetDisplayRange(true).Start;
				string RowFilterCmd = "(WhenDate IS NOT NULL) AND ((Done = False) OR ";
				RowFilterCmd = RowFilterCmd + "((Done = True) AND (DoneDate >= '" + calShowsfirst.ToShortDateString() + "')))";
				this.DatedItemsView.RowFilter = RowFilterCmd;
				this.btnShowDone.Text = orGentaResources.HideDone;
			}
			else
			{
				//	Change view filter to hide done items
				this.DatedItemsView.RowFilter = "WhenDate IS NOT NULL AND Done = False";
				this.btnShowDone.Text = orGentaResources.ShowDone;
			}
			this.dgridTaskList.Refresh();
			sizeTheForm(this.DatedItemsView.Count);
			LocateCurrentItem();
		}

		private void calMonthCalendar_DateSelected(object sender, System.Windows.Forms.DateRangeEventArgs e)
		{
			//	When user click on new date on Calendar, change selected grid item
			LocateCurrentItem();
		}

		private void calMonthCalendar_DateChanged(object sender, System.Windows.Forms.DateRangeEventArgs e)
		{
			if (this.btnShowDone.Text == "Hide Done")
			{
				//	Change viewport of grid items corresponding to Calendar dates
				DateTime calShowsfirst  = this.calMonthCalendar.GetDisplayRange(true).Start;
				string RowFilterCmd = "(WhenDate IS NOT NULL) AND ((Done = False) OR ";
				RowFilterCmd = RowFilterCmd + "((Done = True) AND (DoneDate >= '" + calShowsfirst.ToShortDateString() + "')))";
				this.DatedItemsView.RowFilter = RowFilterCmd;
				this.dgridTaskList.Refresh();
				LocateCurrentItem();
			}
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void tmrCalendarStart_Tick(object sender, System.EventArgs e)
		{
			this.dgridTaskList.CurrentCell = new DataGridCell(SelectedGridRow,0);
			this.dgridTaskList.Select(SelectedGridRow);
			this.tmrCalendarStart.Enabled = false;
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
