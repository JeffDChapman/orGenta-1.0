using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Net;

namespace orGenta
{
	/// <summary>
	/// Summary description for PleaseReg.
	/// </summary>
	public class PleaseReg : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button btnReg;
		private System.Windows.Forms.Button btnContinue;
		public System.Windows.Forms.TextBox txtSoftwareID;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public PleaseReg()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PleaseReg));
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.btnReg = new System.Windows.Forms.Button();
			this.btnContinue = new System.Windows.Forms.Button();
			this.txtSoftwareID = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.AcceptsReturn = true;
			this.textBox1.BackColor = System.Drawing.SystemColors.ControlLight;
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textBox1.Location = new System.Drawing.Point(16, 16);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(384, 176);
			this.textBox1.TabIndex = 1;
			this.textBox1.TabStop = false;
            this.textBox1.Text = orGentaResources.RegistrationNotice.Replace(";", Environment.NewLine);
			this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// btnReg
			// 
			this.btnReg.Location = new System.Drawing.Point(272, 200);
			this.btnReg.Name = "btnReg";
			this.btnReg.Size = new System.Drawing.Size(112, 23);
			this.btnReg.TabIndex = 2;
			this.btnReg.Text = orGentaResources.RegistrationMail;
			this.btnReg.Click += new System.EventHandler(this.btnReg_Click);
			// 
			// btnContinue
			// 
			this.btnContinue.Location = new System.Drawing.Point(24, 200);
			this.btnContinue.Name = "btnContinue";
			this.btnContinue.TabIndex = 3;
			this.btnContinue.Text = orGentaResources.Continue;
			this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
			// 
			// txtSoftwareID
			// 
			this.txtSoftwareID.AcceptsReturn = true;
			this.txtSoftwareID.BackColor = System.Drawing.SystemColors.ControlLight;
			this.txtSoftwareID.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtSoftwareID.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtSoftwareID.Location = new System.Drawing.Point(24, 240);
			this.txtSoftwareID.Multiline = true;
			this.txtSoftwareID.Name = "txtSoftwareID";
			this.txtSoftwareID.ReadOnly = true;
			this.txtSoftwareID.Size = new System.Drawing.Size(352, 24);
			this.txtSoftwareID.TabIndex = 4;
			this.txtSoftwareID.TabStop = false;
			this.txtSoftwareID.Text = "";
			this.txtSoftwareID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// PleaseReg
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.ControlLight;
			this.ClientSize = new System.Drawing.Size(410, 279);
			this.ControlBox = false;
			this.Controls.Add(this.txtSoftwareID);
			this.Controls.Add(this.btnContinue);
			this.Controls.Add(this.btnReg);
			this.Controls.Add(this.textBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "PleaseReg";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = orGentaResources.PleaseRegister;
			this.ResumeLayout(false);

		}
		#endregion

		private void btnContinue_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btnReg_Click(object sender, System.EventArgs e)
		{
			string idToSend = this.txtSoftwareID.Text.ToString().Substring(12);
			string RegMailMessage = "mailto:jeffdchapman@hotmail.com?Subject=orGenta%20Registration" +
				"&Body=Please%20send%20me%20the%20registration%20key%20for%20orGenta%20Software%20ID%20" + idToSend +".";
			System.Diagnostics.Process.Start(RegMailMessage);
		}
	}
}
