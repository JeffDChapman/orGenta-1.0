using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Resources;
using System.Configuration;

namespace orGenta
{
	/// <summary>
	/// Summary description for About.
	/// </summary>
	public class About : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button btnOK;
		public System.Windows.Forms.TextBox txtBuildNumber;
		public System.Windows.Forms.TextBox txtVersionNumber;
		private System.Windows.Forms.Label label1;
		public System.Windows.Forms.TextBox txtDotNetVsn;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.LinkLabel linkLabel1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public About()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(About));
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.txtBuildNumber = new System.Windows.Forms.TextBox();
			this.txtVersionNumber = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtDotNetVsn = new System.Windows.Forms.TextBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.AcceptsReturn = true;
			this.textBox1.BackColor = System.Drawing.SystemColors.ControlLight;
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textBox1.Location = new System.Drawing.Point(56, 16);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(232, 72);
			this.textBox1.TabIndex = 0;
			this.textBox1.TabStop = false;
            this.textBox1.Text = orGentaResources.CopyrightNotice.Replace(";", Environment.NewLine);
			this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(112, 168);
			this.btnOK.Name = "btnOK";
			this.btnOK.TabIndex = 2;
			this.btnOK.Text = "OK";
			// 
			// txtBuildNumber
			// 
			this.txtBuildNumber.AcceptsReturn = true;
			this.txtBuildNumber.BackColor = System.Drawing.SystemColors.ControlLight;
			this.txtBuildNumber.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtBuildNumber.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtBuildNumber.Location = new System.Drawing.Point(198, 144);
			this.txtBuildNumber.Name = "txtBuildNumber";
			this.txtBuildNumber.ReadOnly = true;
			this.txtBuildNumber.Size = new System.Drawing.Size(72, 16);
			this.txtBuildNumber.TabIndex = 3;
			this.txtBuildNumber.TabStop = false;
			this.txtBuildNumber.Text = "";
			this.txtBuildNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txtVersionNumber
			// 
			this.txtVersionNumber.AcceptsReturn = true;
			this.txtVersionNumber.BackColor = System.Drawing.SystemColors.ControlLight;
			this.txtVersionNumber.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtVersionNumber.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtVersionNumber.Location = new System.Drawing.Point(22, 128);
			this.txtVersionNumber.Name = "txtVersionNumber";
			this.txtVersionNumber.ReadOnly = true;
			this.txtVersionNumber.Size = new System.Drawing.Size(248, 16);
			this.txtVersionNumber.TabIndex = 4;
			this.txtVersionNumber.TabStop = false;
			this.txtVersionNumber.Text = "";
			this.txtVersionNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label1
			// 
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label1.Location = new System.Drawing.Point(8, 93);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(272, 3);
			this.label1.TabIndex = 5;
			// 
			// txtDotNetVsn
			// 
			this.txtDotNetVsn.AcceptsReturn = true;
			this.txtDotNetVsn.BackColor = System.Drawing.SystemColors.ControlLight;
			this.txtDotNetVsn.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtDotNetVsn.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtDotNetVsn.Location = new System.Drawing.Point(22, 144);
			this.txtDotNetVsn.Name = "txtDotNetVsn";
			this.txtDotNetVsn.ReadOnly = true;
			this.txtDotNetVsn.Size = new System.Drawing.Size(178, 16);
			this.txtDotNetVsn.TabIndex = 6;
			this.txtDotNetVsn.TabStop = false;
			this.txtDotNetVsn.Text = "";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(12, 29);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(32, 32);
			this.pictureBox1.TabIndex = 7;
			this.pictureBox1.TabStop = false;
			// 
			// label2
			// 
			this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label2.Location = new System.Drawing.Point(9, 114);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(272, 3);
			this.label2.TabIndex = 8;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(91, 97);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 16);
			this.label3.TabIndex = 9;
			this.label3.Text = orGentaResources.IconCredit;
			// 
			// linkLabel1
			// 
			this.linkLabel1.Location = new System.Drawing.Point(139, 97);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(64, 16);
			this.linkLabel1.TabIndex = 10;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "Techlogica";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// About
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.ControlLight;
			this.ClientSize = new System.Drawing.Size(290, 199);
			this.ControlBox = false;
			this.Controls.Add(this.linkLabel1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.txtDotNetVsn);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtVersionNumber);
			this.Controls.Add(this.txtBuildNumber);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.textBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "About";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = orGentaResources.About;
			this.ResumeLayout(false);

		}
		#endregion

		private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("www.techlogica.us");
		}

	}
}
