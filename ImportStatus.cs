using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace orGenta
{
	public class ImportStatus : System.Windows.Forms.Form
	{
		public System.Windows.Forms.RichTextBox tbStatusDisplay;
		private System.Windows.Forms.Button btnPause;
		private System.Windows.Forms.Button btnCancel;
		public bool WebImportPaused = false;
		public bool WebImportCancel = false;
		public bool WebImportSkip = false;
		private System.Windows.Forms.Button btnSkip;
		private System.ComponentModel.Container components = null;

		public ImportStatus()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ImportStatus));
			this.tbStatusDisplay = new System.Windows.Forms.RichTextBox();
			this.btnPause = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnSkip = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// tbStatusDisplay
			// 
			this.tbStatusDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tbStatusDisplay.BackColor = System.Drawing.SystemColors.ControlLight;
			this.tbStatusDisplay.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tbStatusDisplay.Location = new System.Drawing.Point(8, 8);
			this.tbStatusDisplay.Name = "tbStatusDisplay";
			this.tbStatusDisplay.Size = new System.Drawing.Size(464, 168);
			this.tbStatusDisplay.TabIndex = 0;
			this.tbStatusDisplay.Text = "";
			// 
			// btnPause
			// 
			this.btnPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPause.Location = new System.Drawing.Point(287, 184);
			this.btnPause.Name = "btnPause";
			this.btnPause.Size = new System.Drawing.Size(80, 24);
			this.btnPause.TabIndex = 1;
			this.btnPause.Text = orGentaResources.Pause;
			this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(384, 184);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(80, 24);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = orGentaResources.Cancel;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnSkip
			// 
			this.btnSkip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSkip.Location = new System.Drawing.Point(190, 184);
			this.btnSkip.Name = "btnSkip";
			this.btnSkip.Size = new System.Drawing.Size(80, 24);
			this.btnSkip.TabIndex = 3;
			this.btnSkip.Text = orGentaResources.Skip;
			this.btnSkip.Click += new System.EventHandler(this.btnSkip_Click);
			// 
			// ImportStatus
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(480, 213);
			this.Controls.Add(this.btnSkip);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnPause);
			this.Controls.Add(this.tbStatusDisplay);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ImportStatus";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ImportStatus";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.ImportStatus_Closing);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnPause_Click(object sender, System.EventArgs e)
		{
			if (this.btnPause.Text == orGentaResources.Pause)
			{
				WebImportPaused = true;
				this.btnPause.Text = orGentaResources.Resume;
			}
			else
			{
				WebImportPaused = false;
				this.btnPause.Text = orGentaResources.Pause;
			}
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			WebImportCancel = true;
		}

		private void ImportStatus_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			WebImportCancel = true;
			e.Cancel = true;
		}

		private void btnSkip_Click(object sender, System.EventArgs e)
		{
			WebImportSkip = true;		
		}
	}
}
