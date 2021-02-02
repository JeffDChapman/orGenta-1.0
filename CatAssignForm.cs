using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace orGenta
{
	public class CatAssignForm : System.Windows.Forms.Form
	{
		public System.Windows.Forms.CheckedListBox chkListAssignedCats;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TextBox txtFindCat;
		private System.Windows.Forms.Button btnFind;
		private System.ComponentModel.Container components = null;

		public CatAssignForm()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(CatAssignForm));
			this.chkListAssignedCats = new System.Windows.Forms.CheckedListBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.txtFindCat = new System.Windows.Forms.TextBox();
			this.btnFind = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// chkListAssignedCats
			// 
			this.chkListAssignedCats.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.chkListAssignedCats.CheckOnClick = true;
			this.chkListAssignedCats.Location = new System.Drawing.Point(8, 8);
			this.chkListAssignedCats.Name = "chkListAssignedCats";
			this.chkListAssignedCats.Size = new System.Drawing.Size(216, 349);
			this.chkListAssignedCats.TabIndex = 0;
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(144, 392);
			this.btnOK.Name = "btnOK";
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "OK";
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(64, 392);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = orGentaResources.Cancel;
			// 
			// txtFindCat
			// 
			this.txtFindCat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.txtFindCat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtFindCat.Location = new System.Drawing.Point(8, 365);
			this.txtFindCat.Name = "txtFindCat";
			this.txtFindCat.Size = new System.Drawing.Size(112, 20);
			this.txtFindCat.TabIndex = 0;
			this.txtFindCat.Text = "";
			this.txtFindCat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFindCat_KeyPress);
			// 
			// btnFind
			// 
			this.btnFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnFind.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnFind.Location = new System.Drawing.Point(128, 365);
			this.btnFind.Name = "btnFind";
			this.btnFind.Size = new System.Drawing.Size(32, 20);
			this.btnFind.TabIndex = 4;
			this.btnFind.Text = "-->";
			this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
			// 
			// CatAssignForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(232, 421);
			this.Controls.Add(this.btnFind);
			this.Controls.Add(this.txtFindCat);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.chkListAssignedCats);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "CatAssignForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = orGentaResources.CategoriesTitle;
			this.ResumeLayout(false);

		}
		#endregion

		private void btnFind_Click(object sender, System.EventArgs e)
		{
			if (this.txtFindCat.Text == "")
			{
				return;
			}
			
			//	find the corresponding category
			int HowManyCats = chkListAssignedCats.Items.Count;
			int CurSelCat = chkListAssignedCats.SelectedIndex;
			string loopCatText = "";

			for (int i = CurSelCat + 1; i < HowManyCats; i++)
			{
				loopCatText = chkListAssignedCats.Items[i].ToString().ToLower();
				if (loopCatText.IndexOf(txtFindCat.Text.ToLower()) >= 0)
				{
					chkListAssignedCats.SelectedIndex = i;
					break;
				}
			}
		}

		private void txtFindCat_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			//	on enter key, do the btnFind click
			if (e.KeyChar == (char)13)
			{
				e.Handled=true;
				btnFind_Click(this.txtFindCat, null);
			}
		}
	}
}
