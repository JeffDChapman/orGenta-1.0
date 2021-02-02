using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace orGenta
{
	public class MinimalIntface : System.Windows.Forms.Form
	{
		public System.Windows.Forms.TextBox txtDataEntered;
		private System.Windows.Forms.Button btnEnter;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.Button btnRestore;
		private System.ComponentModel.Container components = null;

		public MinimalIntface()
		{
			InitializeComponent();

			this.txtDataEntered.SelectAll();

			// Create the ToolTip and associate with the Restore button.
            using (ToolTip tTipRestore = new ToolTip())
            {
                tTipRestore.AutoPopDelay = 5000;
                tTipRestore.InitialDelay = 1000;
                tTipRestore.ReshowDelay = 500;
                tTipRestore.ShowAlways = true;
                tTipRestore.SetToolTip(this.btnRestore, orGentaResources.Restore);
            }
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MinimalIntface));
			this.txtDataEntered = new System.Windows.Forms.TextBox();
			this.btnEnter = new System.Windows.Forms.Button();
			this.btnExit = new System.Windows.Forms.Button();
			this.btnRestore = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtDataEntered
			// 
			this.txtDataEntered.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtDataEntered.Dock = System.Windows.Forms.DockStyle.Left;
			this.txtDataEntered.Location = new System.Drawing.Point(0, 0);
			this.txtDataEntered.Name = "txtDataEntered";
			this.txtDataEntered.Size = new System.Drawing.Size(584, 20);
			this.txtDataEntered.TabIndex = 0;
			this.txtDataEntered.Text = orGentaResources.EntryPrompt;
			// 
			// btnEnter
			// 
			this.btnEnter.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnEnter.Location = new System.Drawing.Point(8, 0);
			this.btnEnter.Name = "btnEnter";
			this.btnEnter.Size = new System.Drawing.Size(40, 16);
			this.btnEnter.TabIndex = 1;
			this.btnEnter.Text = orGentaResources.Enter;
			// 
			// btnExit
			// 
			this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnExit.Location = new System.Drawing.Point(104, 0);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(40, 16);
			this.btnExit.TabIndex = 3;
			this.btnExit.Text = orGentaResources.Exit;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// btnRestore
			// 
			this.btnRestore.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnRestore.Image = ((System.Drawing.Image)(resources.GetObject("btnRestore.Image")));
			this.btnRestore.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnRestore.Location = new System.Drawing.Point(584, 0);
			this.btnRestore.Name = "btnRestore";
			this.btnRestore.Size = new System.Drawing.Size(24, 19);
			this.btnRestore.TabIndex = 4;
			this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
			// 
			// MinimalIntface
			// 
			this.AcceptButton = this.btnEnter;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnExit;
			this.ClientSize = new System.Drawing.Size(608, 19);
			this.ControlBox = false;
			this.Controls.Add(this.btnRestore);
			this.Controls.Add(this.txtDataEntered);
			this.Controls.Add(this.btnExit);
			this.Controls.Add(this.btnEnter);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "MinimalIntface";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.ResumeLayout(false);

		}
		#endregion

		private void btnExit_Click(object sender, System.EventArgs e)
		{
			this.Visible = false;
		}

		private void btnRestore_Click(object sender, System.EventArgs e)
		{
			this.txtDataEntered.Focus();
			this.Visible = false;
			this.txtDataEntered.Text = "";
		}
	}
}
