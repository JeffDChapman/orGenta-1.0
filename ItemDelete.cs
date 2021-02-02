using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace orGenta
{
	/// <summary>
	/// Summary description for ItemDelete.
	/// </summary>
	public class ItemDelete : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		public System.Windows.Forms.RadioButton btnDeleteFromCat;
		public System.Windows.Forms.RadioButton btnDiscard;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ItemDelete()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ItemDelete));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnDiscard = new System.Windows.Forms.RadioButton();
			this.btnDeleteFromCat = new System.Windows.Forms.RadioButton();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnDiscard);
			this.groupBox1.Controls.Add(this.btnDeleteFromCat);
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(192, 96);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			// 
			// btnDiscard
			// 
			this.btnDiscard.Location = new System.Drawing.Point(16, 56);
			this.btnDiscard.Name = "btnDiscard";
			this.btnDiscard.Size = new System.Drawing.Size(167, 24);
			this.btnDiscard.TabIndex = 1;
			this.btnDiscard.Text = orGentaResources.Discard;
			// 
			// btnDeleteFromCat
			// 
			this.btnDeleteFromCat.Checked = true;
			this.btnDeleteFromCat.Location = new System.Drawing.Point(16, 24);
			this.btnDeleteFromCat.Name = "btnDeleteFromCat";
			this.btnDeleteFromCat.Size = new System.Drawing.Size(167, 24);
			this.btnDeleteFromCat.TabIndex = 0;
			this.btnDeleteFromCat.TabStop = true;
			this.btnDeleteFromCat.Text = orGentaResources.Remove;
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(112, 115);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 24);
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(24, 115);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 24);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = orGentaResources.Cancel;
			// 
			// ItemDelete
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(208, 149);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ItemDelete";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = orGentaResources.DisposeTitle;
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnOK_Click(object sender, System.EventArgs e)
		{
//			this.Close();
		}
	}
}
