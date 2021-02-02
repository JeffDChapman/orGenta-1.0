using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace orGenta
{

	public class ItemImportStatus : System.Windows.Forms.Form
	{
		public System.Windows.Forms.Button btnCancel;
		public System.Windows.Forms.ProgressBar pBarImport;
		public System.Windows.Forms.Label lblStatusMsg;
		public bool CancelFlag = false;

		private System.ComponentModel.Container components = null;

		public ItemImportStatus()
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
			this.btnCancel = new System.Windows.Forms.Button();
			this.pBarImport = new System.Windows.Forms.ProgressBar();
			this.lblStatusMsg = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(112, 56);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(72, 24);
			this.btnCancel.TabIndex = 0;
			this.btnCancel.Text = orGentaResources.Cancel;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// pBarImport
			// 
			this.pBarImport.Dock = System.Windows.Forms.DockStyle.Top;
			this.pBarImport.Location = new System.Drawing.Point(0, 0);
			this.pBarImport.Name = "pBarImport";
			this.pBarImport.Size = new System.Drawing.Size(292, 16);
			this.pBarImport.TabIndex = 1;
			// 
			// lblStatusMsg
			// 
			this.lblStatusMsg.Location = new System.Drawing.Point(8, 28);
			this.lblStatusMsg.Name = "lblStatusMsg";
			this.lblStatusMsg.Size = new System.Drawing.Size(272, 16);
			this.lblStatusMsg.TabIndex = 2;
			this.lblStatusMsg.Text = "... Status Msgs Go Here ...";
			this.lblStatusMsg.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// ItemImportStatus
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 85);
			this.Controls.Add(this.lblStatusMsg);
			this.Controls.Add(this.pBarImport);
			this.Controls.Add(this.btnCancel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "ItemImportStatus";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Import Status";
			this.ResumeLayout(false);

		}
		#endregion

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			CancelFlag = true;
			this.Cursor = Cursors.WaitCursor;
		}
	}
}
