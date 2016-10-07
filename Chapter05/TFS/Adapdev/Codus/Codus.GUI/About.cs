#region Copyright / License Information

/*
Author: Sean McCormack

================================================
Copyright
================================================
Copyright (c) 2004 Adapdev Technologies, LLC
Portions Copyright (c) 2000 - 2004 Microsoft Corporation
Portions Copyright (c) 2002-2003, James W. Newkirk, Michael C. Two, Alexei A. Vorontsov, Charlie Poole, Philip A. Craig
Portions Copyright (c) 2002-2004 The Genghis Group (http://www.genghisgroup.com/).

================================================
License
================================================
Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

================================================
Change History
================================================
	III	MM/DD/YYYY	Change

*/

#endregion

namespace Adapdev.Codus.GUI
{
	using System;
	using System.ComponentModel;
	using System.Windows.Forms;
	using Genghis.Windows.Forms;

	/// <summary>
	/// Summary description for About.
	/// </summary>
	public class About : Form
	{
		private Label label1;
		private HtmlLinkLabel htmlLinkLabel1;
		private HtmlLinkLabel htmlLinkLabel2;
		private HtmlLinkLabel htmlLinkLabel3;
		private Label label3;
		private Label label4;
		private Label label6;
		private Label label7;
		private HtmlLinkLabel htmlLinkLabel4;
		private Label label2;
		private Genghis.Windows.Forms.HtmlLinkLabel htmlLinkLabel5;
		private System.Windows.Forms.GroupBox groupBox1;
		private Genghis.Windows.Forms.HtmlLinkLabel htmlLinkLabel6;
		private Genghis.Windows.Forms.HtmlLinkLabel htmlLinkLabel7;
		private Genghis.Windows.Forms.HtmlLinkLabel htmlLinkLabel8;
		private Genghis.Windows.Forms.HtmlLinkLabel htmlLinkLabel9;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		public About()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			this.Text = MainForm.GetVersion();
			this.label3.Text = MainForm.GetVersion().Replace("Adapdev ", "");

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(About));
			this.label1 = new System.Windows.Forms.Label();
			this.htmlLinkLabel1 = new Genghis.Windows.Forms.HtmlLinkLabel();
			this.htmlLinkLabel2 = new Genghis.Windows.Forms.HtmlLinkLabel();
			this.htmlLinkLabel3 = new Genghis.Windows.Forms.HtmlLinkLabel();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.htmlLinkLabel4 = new Genghis.Windows.Forms.HtmlLinkLabel();
			this.label2 = new System.Windows.Forms.Label();
			this.htmlLinkLabel5 = new Genghis.Windows.Forms.HtmlLinkLabel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.htmlLinkLabel6 = new Genghis.Windows.Forms.HtmlLinkLabel();
			this.htmlLinkLabel7 = new Genghis.Windows.Forms.HtmlLinkLabel();
			this.htmlLinkLabel8 = new Genghis.Windows.Forms.HtmlLinkLabel();
			this.htmlLinkLabel9 = new Genghis.Windows.Forms.HtmlLinkLabel();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.White;
			this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(320, 56);
			this.label1.TabIndex = 0;
			// 
			// htmlLinkLabel1
			// 
			this.htmlLinkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.htmlLinkLabel1.Location = new System.Drawing.Point(8, 232);
			this.htmlLinkLabel1.Name = "htmlLinkLabel1";
			this.htmlLinkLabel1.Size = new System.Drawing.Size(328, 24);
			this.htmlLinkLabel1.TabIndex = 2;
			this.htmlLinkLabel1.Text = "Copyright (c) 2004  Adapdev Technologies, LLC.";
			// 
			// htmlLinkLabel2
			// 
			this.htmlLinkLabel2.BackColor = System.Drawing.Color.White;
			this.htmlLinkLabel2.Location = new System.Drawing.Point(8, 136);
			this.htmlLinkLabel2.Name = "htmlLinkLabel2";
			this.htmlLinkLabel2.Size = new System.Drawing.Size(224, 16);
			this.htmlLinkLabel2.TabIndex = 3;
			this.htmlLinkLabel2.Text = "Website: <a href=\"http://www.adapdev.com\">http://www.adapdev.com</a>";
			// 
			// htmlLinkLabel3
			// 
			this.htmlLinkLabel3.BackColor = System.Drawing.Color.White;
			this.htmlLinkLabel3.Location = new System.Drawing.Point(8, 184);
			this.htmlLinkLabel3.Name = "htmlLinkLabel3";
			this.htmlLinkLabel3.Size = new System.Drawing.Size(248, 16);
			this.htmlLinkLabel3.TabIndex = 4;
			this.htmlLinkLabel3.Text = "Support Email: <a href=\"mailto:codus-support@adapdev.com\">codus-support@adapdev.c" +
				"om</a>";
			// 
			// label3
			// 
			this.label3.BackColor = System.Drawing.Color.White;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(160, 64);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(136, 23);
			this.label3.TabIndex = 6;
			this.label3.Text = "Codus v1.0";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label4.Location = new System.Drawing.Point(8, 256);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(328, 23);
			this.label4.TabIndex = 7;
			this.label4.Text = "Portions Copyright (c) 2004 NVelocity (http://nvelocity.sourceforge.net/)";
			this.label4.Click += new System.EventHandler(this.label4_Click);
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label6.Location = new System.Drawing.Point(8, 280);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(328, 32);
			this.label6.TabIndex = 9;
			this.label6.Text = "Portions Copyright (c) 2002-2004 The Genghis Group (http://www.genghisgroup.com/)" +
				"";
			this.label6.Click += new System.EventHandler(this.label6_Click);
			// 
			// label7
			// 
			this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.label7.BackColor = System.Drawing.Color.White;
			this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label7.Location = new System.Drawing.Point(-8, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(504, 224);
			this.label7.TabIndex = 10;
			this.label7.Click += new System.EventHandler(this.label7_Click);
			// 
			// htmlLinkLabel4
			// 
			this.htmlLinkLabel4.BackColor = System.Drawing.Color.White;
			this.htmlLinkLabel4.Location = new System.Drawing.Point(8, 112);
			this.htmlLinkLabel4.Name = "htmlLinkLabel4";
			this.htmlLinkLabel4.Size = new System.Drawing.Size(304, 16);
			this.htmlLinkLabel4.TabIndex = 12;
			this.htmlLinkLabel4.Text = "Author: <a href=\"http://www.adapdev.com/blogs/smccormack\">Sean McCormack</a>  (<a" +
				" href=\"mailto:smccormack@adapdev.com\">smccormack@adapdev.com</a>)";
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.Color.White;
			this.label2.Image = ((System.Drawing.Image)(resources.GetObject("label2.Image")));
			this.label2.Location = new System.Drawing.Point(96, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 56);
			this.label2.TabIndex = 5;
			// 
			// htmlLinkLabel5
			// 
			this.htmlLinkLabel5.BackColor = System.Drawing.Color.White;
			this.htmlLinkLabel5.Location = new System.Drawing.Point(8, 160);
			this.htmlLinkLabel5.Name = "htmlLinkLabel5";
			this.htmlLinkLabel5.Size = new System.Drawing.Size(248, 16);
			this.htmlLinkLabel5.TabIndex = 13;
			this.htmlLinkLabel5.Text = "Forums: <a href=\"http://www.adapdev.com/forums\">http://www.adapdev.com/forums</a>" +
				"";
			// 
			// groupBox1
			// 
			this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.groupBox1.Controls.Add(this.htmlLinkLabel8);
			this.groupBox1.Controls.Add(this.htmlLinkLabel6);
			this.groupBox1.Controls.Add(this.htmlLinkLabel9);
			this.groupBox1.Controls.Add(this.htmlLinkLabel7);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.groupBox1.Location = new System.Drawing.Point(320, 112);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(144, 104);
			this.groupBox1.TabIndex = 16;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Contributors";
			// 
			// htmlLinkLabel6
			// 
			this.htmlLinkLabel6.Location = new System.Drawing.Point(8, 56);
			this.htmlLinkLabel6.Name = "htmlLinkLabel6";
			this.htmlLinkLabel6.Size = new System.Drawing.Size(128, 23);
			this.htmlLinkLabel6.TabIndex = 0;
			this.htmlLinkLabel6.Text = "Ben Reichelt";
			// 
			// htmlLinkLabel7
			// 
			this.htmlLinkLabel7.Location = new System.Drawing.Point(8, 24);
			this.htmlLinkLabel7.Name = "htmlLinkLabel7";
			this.htmlLinkLabel7.Size = new System.Drawing.Size(128, 23);
			this.htmlLinkLabel7.TabIndex = 1;
			this.htmlLinkLabel7.Text = "Michael Brown";
			// 
			// htmlLinkLabel8
			// 
			this.htmlLinkLabel8.Location = new System.Drawing.Point(8, 72);
			this.htmlLinkLabel8.Name = "htmlLinkLabel8";
			this.htmlLinkLabel8.Size = new System.Drawing.Size(128, 23);
			this.htmlLinkLabel8.TabIndex = 2;
			this.htmlLinkLabel8.Text = "Levi Rosol";
			// 
			// htmlLinkLabel9
			// 
			this.htmlLinkLabel9.Location = new System.Drawing.Point(8, 40);
			this.htmlLinkLabel9.Name = "htmlLinkLabel9";
			this.htmlLinkLabel9.Size = new System.Drawing.Size(128, 23);
			this.htmlLinkLabel9.TabIndex = 3;
			this.htmlLinkLabel9.Text = "Trevor Leybourne";
			// 
			// About
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(480, 318);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.htmlLinkLabel5);
			this.Controls.Add(this.htmlLinkLabel4);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.htmlLinkLabel3);
			this.Controls.Add(this.htmlLinkLabel2);
			this.Controls.Add(this.htmlLinkLabel1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label7);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "About";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private void label2_Click(object sender, EventArgs e)
		{
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
		}

		private void label7_Click(object sender, EventArgs e)
		{
		}

		private void label4_Click(object sender, EventArgs e)
		{
		}

		private void label6_Click(object sender, EventArgs e)
		{
		}

		private void label5_Click(object sender, EventArgs e)
		{
		}
	}
}