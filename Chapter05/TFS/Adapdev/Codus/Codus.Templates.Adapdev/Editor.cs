using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AbstractConfig = Adapdev.CodeGen.AbstractConfig;

namespace Codus.Templates.Adapdev
{
	using System.Diagnostics;
	using System.IO;

	public class Editor : Codus.GUI.Extensions.GUIConfigurator
	{
		private CheckBox cbGenerateSln;
		private CheckBox cbNant;
		private IContainer components = null;

		public static readonly string P_GENERATESLN = "generatesln";
		public static readonly string P_GENERATENANT = "generatenant";
		public static readonly string P_GENERATEZANEBUG = "generatezanebug";
		public static readonly string P_GENERATENUNIT = "generatenunit";
		public static readonly string P_GENERATEWEBSERVICE = "generatewebservice";
		public static readonly string P_SQLSPS = "sqltypesps";
		public static readonly string P_SQLTEXT = "sqltypetext";

		private CheckBox cbNUnit;
		private CheckBox cbWebServices;
		private System.Windows.Forms.CheckBox cbSps;
		private CheckBox cbZanebug;


		public Editor()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
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

		public override Hashtable GetCustomProperties()
		{
			Hashtable ht = new Hashtable();
			ht[Editor.P_GENERATENANT] = this.cbNant.Checked;
			ht[Editor.P_GENERATENUNIT] = this.cbNUnit.Checked;
			ht[Editor.P_GENERATESLN] = this.cbGenerateSln.Checked;
			ht[Editor.P_SQLSPS] = this.cbSps.Checked;
			ht[Editor.P_SQLTEXT] = !this.cbSps.Checked;
			ht[Editor.P_GENERATEWEBSERVICE] = this.cbWebServices.Checked;
			ht[Editor.P_GENERATEZANEBUG] = this.cbZanebug.Checked;
			ht[Editor.P_GENERATENANT] = this.cbNant.Checked;
			return ht;
		}

		#region Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.cbGenerateSln = new System.Windows.Forms.CheckBox();
			this.cbNant = new System.Windows.Forms.CheckBox();
			this.cbNUnit = new System.Windows.Forms.CheckBox();
			this.cbWebServices = new System.Windows.Forms.CheckBox();
			this.cbZanebug = new System.Windows.Forms.CheckBox();
			this.cbSps = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// cbGenerateSln
			// 
			this.cbGenerateSln.Location = new System.Drawing.Point(8, 8);
			this.cbGenerateSln.Name = "cbGenerateSln";
			this.cbGenerateSln.Size = new System.Drawing.Size(200, 24);
			this.cbGenerateSln.TabIndex = 0;
			this.cbGenerateSln.Text = "Create VS.NET 2003 Solution";
			this.cbGenerateSln.CheckedChanged += new System.EventHandler(this.cbGenerateSln_CheckedChanged);
			// 
			// cbNant
			// 
			this.cbNant.Checked = true;
			this.cbNant.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbNant.Location = new System.Drawing.Point(224, 8);
			this.cbNant.Name = "cbNant";
			this.cbNant.Size = new System.Drawing.Size(200, 24);
			this.cbNant.TabIndex = 2;
			this.cbNant.Text = "Create NAnt .build file";
			this.cbNant.CheckedChanged += new System.EventHandler(this.cbNant_CheckedChanged);
			// 
			// cbNUnit
			// 
			this.cbNUnit.Checked = true;
			this.cbNUnit.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbNUnit.Location = new System.Drawing.Point(8, 40);
			this.cbNUnit.Name = "cbNUnit";
			this.cbNUnit.Size = new System.Drawing.Size(200, 24);
			this.cbNUnit.TabIndex = 3;
			this.cbNUnit.Text = "Create NUnit Tests (v2.2.2)";
			// 
			// cbWebServices
			// 
			this.cbWebServices.Location = new System.Drawing.Point(224, 72);
			this.cbWebServices.Name = "cbWebServices";
			this.cbWebServices.Size = new System.Drawing.Size(200, 24);
			this.cbWebServices.TabIndex = 4;
			this.cbWebServices.Text = "Create WebServices";
			// 
			// cbZanebug
			// 
			this.cbZanebug.Location = new System.Drawing.Point(8, 72);
			this.cbZanebug.Name = "cbZanebug";
			this.cbZanebug.Size = new System.Drawing.Size(200, 24);
			this.cbZanebug.TabIndex = 6;
			this.cbZanebug.Text = "Create Zanebug Tests (v1.5.0)";
			this.cbZanebug.Visible = false;
			// 
			// cbSps
			// 
			this.cbSps.Location = new System.Drawing.Point(224, 40);
			this.cbSps.Name = "cbSps";
			this.cbSps.Size = new System.Drawing.Size(168, 24);
			this.cbSps.TabIndex = 11;
			this.cbSps.Text = "Create Stored Procedures";
			// 
			// Editor
			// 
			this.Controls.Add(this.cbSps);
			this.Controls.Add(this.cbZanebug);
			this.Controls.Add(this.cbWebServices);
			this.Controls.Add(this.cbNUnit);
			this.Controls.Add(this.cbNant);
			this.Controls.Add(this.cbGenerateSln);
			this.Name = "Editor";
			this.Size = new System.Drawing.Size(608, 200);
			this.ResumeLayout(false);

		}

		#endregion


		public override void PreRunCustomCode(AbstractConfig config)
		{
		}
		
		public override void PostRunCustomCode(AbstractConfig config)
		{
			if(this.cbNant.Checked)
			{
				string buildfile = Path.Combine(config.OutputDirectory,"runbuild.bat");
				config.OnProcessing("Running " + buildfile);
				Process.Start(buildfile);
			}
		}

		private void cbGenerateSln_CheckedChanged(object sender, System.EventArgs e)
		{
			//this.cbNant.Checked = !this.cbGenerateSln.Checked;		
		}

		private void cbNant_CheckedChanged(object sender, System.EventArgs e)
		{
			//this.cbGenerateSln.Checked = !this.cbNant.Checked;
		}

	}
}

