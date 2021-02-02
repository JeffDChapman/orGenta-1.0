using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace orGenta
{
	/// <summary>
	/// Summary description for WebImport.
	/// </summary>
	public class WebImportForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		public System.Windows.Forms.TextBox txtWebStartingPoint;
		private System.Windows.Forms.Label label2;
		public System.Windows.Forms.TextBox txtHowManyLinkLevels;
		private System.Windows.Forms.Label label3;
		public System.Windows.Forms.RadioButton rbTitle;
		public System.Windows.Forms.RadioButton rBMetaTag;
		public System.Windows.Forms.RadioButton rBfirstLine;
		private System.Windows.Forms.Label label4;
		public System.Windows.Forms.RadioButton rBchangeInternal;
		public System.Windows.Forms.RadioButton rBLeaveExternal;
		private System.Windows.Forms.Button btnImport;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.GroupBox gBUseForItemText;
		private System.Windows.Forms.GroupBox gBChangeLinks;
		private System.Windows.Forms.Label label5;
		public System.Windows.Forms.TextBox txtTimeout;
		private System.Windows.Forms.Label label6;
		public System.Windows.Forms.TextBox txtAlphaStart;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public WebImportForm()
		{
			InitializeComponent();

		}

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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(WebImportForm));
			this.label1 = new System.Windows.Forms.Label();
			this.txtWebStartingPoint = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtHowManyLinkLevels = new System.Windows.Forms.TextBox();
			this.gBUseForItemText = new System.Windows.Forms.GroupBox();
			this.rBfirstLine = new System.Windows.Forms.RadioButton();
			this.rBMetaTag = new System.Windows.Forms.RadioButton();
			this.rbTitle = new System.Windows.Forms.RadioButton();
			this.label3 = new System.Windows.Forms.Label();
			this.gBChangeLinks = new System.Windows.Forms.GroupBox();
			this.rBLeaveExternal = new System.Windows.Forms.RadioButton();
			this.rBchangeInternal = new System.Windows.Forms.RadioButton();
			this.label4 = new System.Windows.Forms.Label();
			this.btnImport = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.txtTimeout = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.txtAlphaStart = new System.Windows.Forms.TextBox();
			this.gBUseForItemText.SuspendLayout();
			this.gBChangeLinks.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(0, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(149, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = orGentaResources.WebAddrPrompt;
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtWebStartingPoint
			// 
			this.txtWebStartingPoint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtWebStartingPoint.Location = new System.Drawing.Point(160, 8);
			this.txtWebStartingPoint.Name = "txtWebStartingPoint";
			this.txtWebStartingPoint.Size = new System.Drawing.Size(376, 20);
			this.txtWebStartingPoint.TabIndex = 1;
			this.txtWebStartingPoint.Text = "http://";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(56, 72);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(200, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = orGentaResources.LinkDepth;
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtHowManyLinkLevels
			// 
			this.txtHowManyLinkLevels.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtHowManyLinkLevels.Location = new System.Drawing.Point(264, 72);
			this.txtHowManyLinkLevels.Name = "txtHowManyLinkLevels";
			this.txtHowManyLinkLevels.Size = new System.Drawing.Size(32, 20);
			this.txtHowManyLinkLevels.TabIndex = 3;
			this.txtHowManyLinkLevels.Text = "0";
			this.txtHowManyLinkLevels.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// gBUseForItemText
			// 
			this.gBUseForItemText.Controls.Add(this.rBfirstLine);
			this.gBUseForItemText.Controls.Add(this.rBMetaTag);
			this.gBUseForItemText.Controls.Add(this.rbTitle);
			this.gBUseForItemText.Controls.Add(this.label3);
			this.gBUseForItemText.Location = new System.Drawing.Point(40, 96);
			this.gBUseForItemText.Name = "gBUseForItemText";
			this.gBUseForItemText.Size = new System.Drawing.Size(240, 128);
			this.gBUseForItemText.TabIndex = 4;
			this.gBUseForItemText.TabStop = false;
			// 
			// rBfirstLine
			// 
			this.rBfirstLine.Location = new System.Drawing.Point(32, 96);
			this.rBfirstLine.Name = "rBfirstLine";
			this.rBfirstLine.Size = new System.Drawing.Size(168, 16);
			this.rBfirstLine.TabIndex = 3;
			this.rBfirstLine.Text = orGentaResources.FirstLine;
			// 
			// rBMetaTag
			// 
			this.rBMetaTag.Checked = true;
			this.rBMetaTag.Location = new System.Drawing.Point(32, 72);
			this.rBMetaTag.Name = "rBMetaTag";
			this.rBMetaTag.Size = new System.Drawing.Size(192, 16);
			this.rBMetaTag.TabIndex = 2;
			this.rBMetaTag.TabStop = true;
            this.rBMetaTag.Text = orGentaResources.DescrMeta;
			// 
			// rbTitle
			// 
			this.rbTitle.Location = new System.Drawing.Point(32, 48);
			this.rbTitle.Name = "rbTitle";
			this.rbTitle.Size = new System.Drawing.Size(128, 16);
			this.rbTitle.TabIndex = 1;
			this.rbTitle.Text = "Web Page Titles";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 24);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(152, 16);
			this.label3.TabIndex = 0;
			this.label3.Text = orGentaResources.ImportLabel;
			// 
			// gBChangeLinks
			// 
			this.gBChangeLinks.Controls.Add(this.rBLeaveExternal);
			this.gBChangeLinks.Controls.Add(this.rBchangeInternal);
			this.gBChangeLinks.Controls.Add(this.label4);
			this.gBChangeLinks.Location = new System.Drawing.Point(304, 96);
			this.gBChangeLinks.Name = "gBChangeLinks";
			this.gBChangeLinks.Size = new System.Drawing.Size(208, 128);
			this.gBChangeLinks.TabIndex = 5;
			this.gBChangeLinks.TabStop = false;
			// 
			// rBLeaveExternal
			// 
			this.rBLeaveExternal.Location = new System.Drawing.Point(32, 84);
			this.rBLeaveExternal.Name = "rBLeaveExternal";
			this.rBLeaveExternal.Size = new System.Drawing.Size(144, 16);
			this.rBLeaveExternal.TabIndex = 2;
			this.rBLeaveExternal.Text = orGentaResources.LeaveLinks;
			// 
			// rBchangeInternal
			// 
			this.rBchangeInternal.Checked = true;
			this.rBchangeInternal.Location = new System.Drawing.Point(32, 52);
			this.rBchangeInternal.Name = "rBchangeInternal";
			this.rBchangeInternal.Size = new System.Drawing.Size(144, 16);
			this.rBchangeInternal.TabIndex = 1;
			this.rBchangeInternal.TabStop = true;
			this.rBchangeInternal.Text = orGentaResources.chgInternal;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 24);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(152, 16);
			this.label4.TabIndex = 0;
			this.label4.Text = orGentaResources.OnLinksLabel;
			// 
			// btnImport
			// 
			this.btnImport.BackColor = System.Drawing.SystemColors.ControlLight;
			this.btnImport.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnImport.Location = new System.Drawing.Point(336, 232);
			this.btnImport.Name = "btnImport";
			this.btnImport.Size = new System.Drawing.Size(70, 24);
			this.btnImport.TabIndex = 6;
			this.btnImport.Text = orGentaResources.Import;
			// 
			// btnCancel
			// 
			this.btnCancel.BackColor = System.Drawing.SystemColors.ControlLight;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(432, 232);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(70, 24);
			this.btnCancel.TabIndex = 7;
			this.btnCancel.Text = orGentaResources.Cancel;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(316, 72);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(88, 16);
			this.label5.TabIndex = 8;
			this.label5.Text = orGentaResources.Timeout;
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtTimeout
			// 
			this.txtTimeout.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtTimeout.Location = new System.Drawing.Point(408, 72);
			this.txtTimeout.Name = "txtTimeout";
			this.txtTimeout.Size = new System.Drawing.Size(32, 20);
			this.txtTimeout.TabIndex = 9;
			this.txtTimeout.Text = "20";
			this.txtTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(48, 42);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(280, 16);
			this.label6.TabIndex = 10;
			this.label6.Text = orGentaResources.scanAlpha;
			// 
			// txtAlphaStart
			// 
			this.txtAlphaStart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtAlphaStart.Location = new System.Drawing.Point(320, 40);
			this.txtAlphaStart.Name = "txtAlphaStart";
			this.txtAlphaStart.Size = new System.Drawing.Size(160, 20);
			this.txtAlphaStart.TabIndex = 11;
			this.txtAlphaStart.Text = "";
			// 
			// WebImportForm
			// 
			this.AcceptButton = this.btnImport;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(544, 264);
			this.Controls.Add(this.txtAlphaStart);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.txtTimeout);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnImport);
			this.Controls.Add(this.gBChangeLinks);
			this.Controls.Add(this.gBUseForItemText);
			this.Controls.Add(this.txtHowManyLinkLevels);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtWebStartingPoint);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "WebImportForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Web Import Specifications";
			this.gBUseForItemText.ResumeLayout(false);
			this.gBChangeLinks.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

	}
}
