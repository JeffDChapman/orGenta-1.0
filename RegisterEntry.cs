using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace orGenta
{
	/// <summary>
	/// Summary description for RegisterEntry.
	/// </summary>
	public class RegisterEntry : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBox1;
		public System.Windows.Forms.TextBox txtKeyEntered;
		private System.Windows.Forms.Button btnContinue;
		private System.Windows.Forms.Button btnCancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public RegisterEntry()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(RegisterEntry));
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.txtKeyEntered = new System.Windows.Forms.TextBox();
			this.btnContinue = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.AcceptsReturn = true;
			this.textBox1.BackColor = System.Drawing.SystemColors.ControlLight;
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textBox1.Location = new System.Drawing.Point(8, 8);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(228, 112);
			this.textBox1.TabIndex = 2;
			this.textBox1.TabStop = false;
			this.textBox1.Text = orGentaResources.KeyPrompt.Replace(";", Environment.NewLine);
			this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// txtKeyEntered
			// 
			this.txtKeyEntered.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtKeyEntered.Location = new System.Drawing.Point(80, 136);
			this.txtKeyEntered.Name = "txtKeyEntered";
			this.txtKeyEntered.Size = new System.Drawing.Size(88, 20);
			this.txtKeyEntered.TabIndex = 3;
			this.txtKeyEntered.Text = "";
			this.txtKeyEntered.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// btnContinue
			// 
			this.btnContinue.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnContinue.Location = new System.Drawing.Point(128, 176);
			this.btnContinue.Name = "btnContinue";
			this.btnContinue.TabIndex = 4;
			this.btnContinue.Text = orGentaResources.Continue;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(32, 176);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = orGentaResources.Cancel;
			// 
			// RegisterEntry
			// 
			this.AcceptButton = this.btnContinue;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(248, 213);
			this.ControlBox = false;
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnContinue);
			this.Controls.Add(this.txtKeyEntered);
			this.Controls.Add(this.textBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "RegisterEntry";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = orGentaResources.Registration;
			this.ResumeLayout(false);

		}
		#endregion
	}
}
