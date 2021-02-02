using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace orGenta
{
	/// <summary>
	/// Summary description for NoteDataEntry.
	/// </summary>
	public class NoteDataEntry : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnClear;
		public System.Windows.Forms.RichTextBox NoteText;
		private System.Windows.Forms.Button btnEnter;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public NoteDataEntry()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(NoteDataEntry));
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnClear = new System.Windows.Forms.Button();
			this.NoteText = new System.Windows.Forms.RichTextBox();
			this.btnEnter = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(360, 192);
			this.btnOK.Name = "btnOK";
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "OK";
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(272, 192);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = orGentaResources.Cancel;
			// 
			// btnClear
			// 
			this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnClear.Location = new System.Drawing.Point(16, 192);
			this.btnClear.Name = "btnClear";
			this.btnClear.TabIndex = 3;
			this.btnClear.Text = orGentaResources.Clear;
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// NoteText
			// 
			this.NoteText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.NoteText.Location = new System.Drawing.Point(8, 8);
			this.NoteText.Name = "NoteText";
			this.NoteText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.NoteText.Size = new System.Drawing.Size(432, 168);
			this.NoteText.TabIndex = 0;
			this.NoteText.Text = orGentaResources.NotePrompt;
			// 
			// btnEnter
			// 
			this.btnEnter.Location = new System.Drawing.Point(136, 192);
			this.btnEnter.Name = "btnEnter";
			this.btnEnter.Size = new System.Drawing.Size(64, 24);
			this.btnEnter.TabIndex = 4;
			this.btnEnter.Text = orGentaResources.Enter;
			this.btnEnter.Visible = false;
			this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
			// 
			// NoteDataEntry
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(448, 229);
			this.Controls.Add(this.btnEnter);
			this.Controls.Add(this.NoteText);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "NoteDataEntry";
			this.ShowInTaskbar = false;
			this.Text = orGentaResources.NoteTitle;
			this.ResumeLayout(false);

		}
		#endregion

		private void btnClear_Click(object sender, System.EventArgs e)
		{
			this.NoteText.Text = "";
		}

		private void btnEnter_Click(object sender, System.EventArgs e)
		{
			this.NoteText.Focus();
			SendKeys.Send("^{ENTER}");
		}

	}
}
