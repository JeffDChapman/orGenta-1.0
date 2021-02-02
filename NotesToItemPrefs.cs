using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace orGenta
{
	/// <summary>
	/// Summary description for NotesToItemPrefs.
	/// </summary>
	public class NotesToItemPrefs : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		public System.Windows.Forms.RadioButton btn1perSentence;
		private System.Windows.Forms.GroupBox prefSentOrPara;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		public System.Windows.Forms.RadioButton btnOnePerPgh;
		private System.Windows.Forms.GroupBox prefFirstOrLastSent;
		public System.Windows.Forms.RadioButton btnUseFirst;
		public System.Windows.Forms.RadioButton btnUseLast;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public NotesToItemPrefs()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(NotesToItemPrefs));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.prefSentOrPara = new System.Windows.Forms.GroupBox();
			this.btnOnePerPgh = new System.Windows.Forms.RadioButton();
			this.btn1perSentence = new System.Windows.Forms.RadioButton();
			this.prefFirstOrLastSent = new System.Windows.Forms.GroupBox();
			this.btnUseLast = new System.Windows.Forms.RadioButton();
			this.btnUseFirst = new System.Windows.Forms.RadioButton();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.prefSentOrPara.SuspendLayout();
			this.prefFirstOrLastSent.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F);
			this.label1.Location = new System.Drawing.Point(8, 5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(304, 48);
			this.label1.TabIndex = 0;
            this.label1.Text = orGentaResources.ConvsnDescriptive;
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F);
			this.label2.Location = new System.Drawing.Point(8, 72);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(304, 26);
			this.label2.TabIndex = 1;
			this.label2.Text = orGentaResources.ConvChoicePrompt;
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// label3
			// 
			this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label3.Location = new System.Drawing.Point(4, 61);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(310, 2);
			this.label3.TabIndex = 2;
			// 
			// prefSentOrPara
			// 
			this.prefSentOrPara.Controls.Add(this.btnOnePerPgh);
			this.prefSentOrPara.Controls.Add(this.btn1perSentence);
			this.prefSentOrPara.Location = new System.Drawing.Point(8, 96);
			this.prefSentOrPara.Name = "prefSentOrPara";
			this.prefSentOrPara.Size = new System.Drawing.Size(304, 64);
			this.prefSentOrPara.TabIndex = 4;
			this.prefSentOrPara.TabStop = false;
			// 
			// btnOnePerPgh
			// 
			this.btnOnePerPgh.Location = new System.Drawing.Point(72, 40);
			this.btnOnePerPgh.Name = "btnOnePerPgh";
			this.btnOnePerPgh.Size = new System.Drawing.Size(168, 16);
			this.btnOnePerPgh.TabIndex = 5;
			this.btnOnePerPgh.Text = orGentaResources.PerParagraph;
			this.btnOnePerPgh.Click += new System.EventHandler(this.btnOnePerPgh_Click);
			// 
			// btn1perSentence
			// 
			this.btn1perSentence.Location = new System.Drawing.Point(72, 16);
			this.btn1perSentence.Name = "btn1perSentence";
			this.btn1perSentence.Size = new System.Drawing.Size(168, 16);
			this.btn1perSentence.TabIndex = 4;
			this.btn1perSentence.Text = orGentaResources.PerSentence;
			this.btn1perSentence.Click += new System.EventHandler(this.btn1perSentence_Click);
			// 
			// prefFirstOrLastSent
			// 
			this.prefFirstOrLastSent.Controls.Add(this.btnUseLast);
			this.prefFirstOrLastSent.Controls.Add(this.btnUseFirst);
			this.prefFirstOrLastSent.Enabled = false;
			this.prefFirstOrLastSent.Location = new System.Drawing.Point(8, 168);
			this.prefFirstOrLastSent.Name = "prefFirstOrLastSent";
			this.prefFirstOrLastSent.Size = new System.Drawing.Size(304, 72);
			this.prefFirstOrLastSent.TabIndex = 5;
			this.prefFirstOrLastSent.TabStop = false;
			// 
			// btnUseLast
			// 
			this.btnUseLast.Location = new System.Drawing.Point(20, 40);
			this.btnUseLast.Name = "btnUseLast";
			this.btnUseLast.Size = new System.Drawing.Size(264, 24);
			this.btnUseLast.TabIndex = 1;
			this.btnUseLast.Text = orGentaResources.UseLast;
			this.btnUseLast.Click += new System.EventHandler(this.btnUseLast_Click);
			// 
			// btnUseFirst
			// 
			this.btnUseFirst.Location = new System.Drawing.Point(20, 16);
			this.btnUseFirst.Name = "btnUseFirst";
			this.btnUseFirst.Size = new System.Drawing.Size(264, 24);
			this.btnUseFirst.TabIndex = 0;
			this.btnUseFirst.Text = orGentaResources.UseFirst;
			this.btnUseFirst.Click += new System.EventHandler(this.btnUseFirst_Click);
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Enabled = false;
			this.btnOK.Location = new System.Drawing.Point(240, 248);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(64, 24);
			this.btnOK.TabIndex = 6;
			this.btnOK.Text = "OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(168, 248);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(64, 24);
			this.btnCancel.TabIndex = 7;
			this.btnCancel.Text = orGentaResources.Cancel;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// NotesToItemPrefs
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(320, 277);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.prefFirstOrLastSent);
			this.Controls.Add(this.prefSentOrPara);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "NotesToItemPrefs";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = orGentaResources.N2Ititle;
			this.prefSentOrPara.ResumeLayout(false);
			this.prefFirstOrLastSent.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btnOnePerPgh_Click(object sender, System.EventArgs e)
		{
			if (this.btnOnePerPgh.Checked == true)
			{
				this.prefFirstOrLastSent.Enabled = true;
				if (this.btnUseFirst.Checked || this.btnUseLast.Checked)
				{
					this.btnOK.Enabled = true;
				}
				else
				{
					this.btnOK.Enabled = false;
				}
			}
		}

		private void btn1perSentence_Click(object sender, System.EventArgs e)
		{
			if (this.btn1perSentence.Checked == true)
			{
				this.prefFirstOrLastSent.Enabled = false;
				this.btnOK.Enabled = true;
			}
		
		}

		private void btnUseFirst_Click(object sender, System.EventArgs e)
		{
			this.btnOK.Enabled = true;
		}

		private void btnUseLast_Click(object sender, System.EventArgs e)
		{
			this.btnOK.Enabled = true;		
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			this.Cursor = Cursors.WaitCursor;
			Application.DoEvents();
			this.Close();
		}

	}
}
