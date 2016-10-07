using GUIConfigurator = Codus.GUI.Extensions.GUIConfigurator;
using IGUIConfigurable = Codus.GUI.Extensions.IGUIConfigurable;

namespace Adapdev.Codus.GUI
{
	using System;
	using System.Collections;
	using System.ComponentModel;
	using System.Data;
	using System.Diagnostics;
	using System.Drawing;
	using System.IO;
	using System.Reflection;
	using System.Threading;
	using System.Windows.Forms;
	using Adapdev.CodeGen;
	using Adapdev.Codus.GUI.Commands;
	using Adapdev.Data.Schema;
	using Adapdev.Reflection;
	using Adapdev.Windows.Commands;
	using Adapdev.Windows.Forms;
	using Adapdev.Windows.Forms.Progress;
	using log4net;

	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	/// 
	public class MainForm : Form
	{
		private TabControl tabControl;

		private DatabaseSchema databaseSchema = null;
		private TabPage tpTables;
		private TabPage tpSetup;
		private GroupBox gbDatabase;
		private Label label3;
		private TreeView tvTables;
		private Label label4;
		private ListView lvColumns;
		private ColumnHeader cName;
		private ColumnHeader cType;
		private ColumnHeader cLength;
		private ColumnHeader cPK;
		private ColumnHeader cFK;
		private ColumnHeader cAllowNulls;

		private bool columnsBuilt = false;
		private string currentTemplate = String.Empty;
		private Hashtable databases = new Hashtable();
		private TabPage tpTemplates;
		private TreeView tvTemplates;
		private Label label7;
		private ReflectionCache reflectionCache = ReflectionCache.GetInstance();
		private Label label8;
		private Label label9;
		private Label label10;
		private Label lblAuthor;
		private Label lblVersion;
		private TextBox tbDescription;
		private Label lblPackage;
		private Label label12;
		private GroupBox gbGeneral;
		private Label label6;
		private Label label5;
		private TextBox tbOutput;
		private TextBox tbNamespace;
		private Button btnGenerate;
		private FolderBrowserDialog fbdOutputLocation;
		private Button btnFind;
		private GroupBox gbTemplateUC;
		private ColumnHeader cAutoIncrement;
		private MainMenu mainMenu1;
		private MenuItem miHelp;
		private MenuItem miAbout;
		private TreeView tvDatabases;
		private Button btnAddDatabase;
		private Label lblCurrentConnection;
		private ContextMenu cmDatabases;
		private MenuItem miDeleteDatabase;
		private bool isLoaded = false;
		private MenuItem miTestConnection;
		private MenuItem miLoadTables;
		private SmoothProgressBar pbGenerate;
		private Label lblFileCount;
		private int processed = 0;
		private Button btnLoadTables;
		private ToolTip ttLoadTables;
		private IContainer components;
		private MenuItem miWebsite;
		private MenuItem miSupport;
		private static readonly string website = "http://www.adapdev.com/codus/";
		private MenuItem menuItem1;
		private ImageList imageList1;
		private Label lblOutput;
		private PropertyGrid schemaPropertyGrid;
		private ColumnHeader cAlias;
		private Label lblSchemaStatus;
		private DatabaseWizard databaseWizard;
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public MainForm()
		{
			// Required for Windows Form Designer support
			InitializeComponent();

			// Load previously added templates
			this.LoadCodeTemplates();

			this.tbNamespace.Text = "Test";
			this.tbOutput.Text = @"c:\output";
			this.Text = MainForm.GetVersion();

			this.btnAddDatabase.Enabled = false;
			this.btnLoadTables.Enabled = false;

			this.databaseWizard.ConnectionControl.TextChanged += new EventHandler(this.tbConnectionName_TextChanged);
			this.databaseWizard.TypesTreeView.AfterSelect += new TreeViewEventHandler(this.typesTreeView_AfterSelect);
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

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main(string[] args) {
			Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
			try {
				Application.Run(new MainForm());
			}
			catch (Exception ex) {
				log.Error(ex.Message, ex);
				new ShowErrorMessageCommand(null, ex, true).Execute();
			}
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tpSetup = new System.Windows.Forms.TabPage();
			this.tvDatabases = new System.Windows.Forms.TreeView();
			this.cmDatabases = new System.Windows.Forms.ContextMenu();
			this.miDeleteDatabase = new System.Windows.Forms.MenuItem();
			this.miLoadTables = new System.Windows.Forms.MenuItem();
			this.miTestConnection = new System.Windows.Forms.MenuItem();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.gbGeneral = new System.Windows.Forms.GroupBox();
			this.btnFind = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.tbOutput = new System.Windows.Forms.TextBox();
			this.tbNamespace = new System.Windows.Forms.TextBox();
			this.gbDatabase = new System.Windows.Forms.GroupBox();
			this.btnLoadTables = new System.Windows.Forms.Button();
			this.btnAddDatabase = new System.Windows.Forms.Button();
			this.databaseWizard = new Adapdev.Windows.Forms.DatabaseWizard();
			this.tpTemplates = new System.Windows.Forms.TabPage();
			this.lblOutput = new System.Windows.Forms.Label();
			this.lblFileCount = new System.Windows.Forms.Label();
			this.pbGenerate = new Adapdev.Windows.Forms.SmoothProgressBar();
			this.gbTemplateUC = new System.Windows.Forms.GroupBox();
			this.btnGenerate = new System.Windows.Forms.Button();
			this.lblPackage = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.tbDescription = new System.Windows.Forms.TextBox();
			this.lblVersion = new System.Windows.Forms.Label();
			this.lblAuthor = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.tvTemplates = new System.Windows.Forms.TreeView();
			this.tpTables = new System.Windows.Forms.TabPage();
			this.lblSchemaStatus = new System.Windows.Forms.Label();
			this.schemaPropertyGrid = new System.Windows.Forms.PropertyGrid();
			this.lvColumns = new System.Windows.Forms.ListView();
			this.cName = new System.Windows.Forms.ColumnHeader();
			this.cAlias = new System.Windows.Forms.ColumnHeader();
			this.cPK = new System.Windows.Forms.ColumnHeader();
			this.cFK = new System.Windows.Forms.ColumnHeader();
			this.cAllowNulls = new System.Windows.Forms.ColumnHeader();
			this.cAutoIncrement = new System.Windows.Forms.ColumnHeader();
			this.cType = new System.Windows.Forms.ColumnHeader();
			this.cLength = new System.Windows.Forms.ColumnHeader();
			this.label4 = new System.Windows.Forms.Label();
			this.tvTables = new System.Windows.Forms.TreeView();
			this.label3 = new System.Windows.Forms.Label();
			this.fbdOutputLocation = new System.Windows.Forms.FolderBrowserDialog();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.miHelp = new System.Windows.Forms.MenuItem();
			this.miWebsite = new System.Windows.Forms.MenuItem();
			this.miSupport = new System.Windows.Forms.MenuItem();
			this.miAbout = new System.Windows.Forms.MenuItem();
			this.lblCurrentConnection = new System.Windows.Forms.Label();
			this.ttLoadTables = new System.Windows.Forms.ToolTip(this.components);
			this.tabControl.SuspendLayout();
			this.tpSetup.SuspendLayout();
			this.gbGeneral.SuspendLayout();
			this.gbDatabase.SuspendLayout();
			this.tpTemplates.SuspendLayout();
			this.tpTables.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tpSetup);
			this.tabControl.Controls.Add(this.tpTables);
			this.tabControl.Controls.Add(this.tpTemplates);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(0, 0);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(736, 529);
			this.tabControl.TabIndex = 40;
			this.tabControl.TabIndexChanged += new System.EventHandler(this.tabControl_TabIndexChanged);
			this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
			// 
			// tpSetup
			// 
			this.tpSetup.Controls.Add(this.tvDatabases);
			this.tpSetup.Controls.Add(this.gbGeneral);
			this.tpSetup.Controls.Add(this.gbDatabase);
			this.tpSetup.Location = new System.Drawing.Point(4, 22);
			this.tpSetup.Name = "tpSetup";
			this.tpSetup.Size = new System.Drawing.Size(728, 503);
			this.tpSetup.TabIndex = 1;
			this.tpSetup.Text = "Setup";
			// 
			// tvDatabases
			// 
			this.tvDatabases.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left)));
			this.tvDatabases.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tvDatabases.ContextMenu = this.cmDatabases;
			this.tvDatabases.HotTracking = true;
			this.tvDatabases.ImageList = this.imageList1;
			this.tvDatabases.Location = new System.Drawing.Point(0, 8);
			this.tvDatabases.Name = "tvDatabases";
			this.tvDatabases.ShowLines = false;
			this.tvDatabases.ShowRootLines = false;
			this.tvDatabases.Size = new System.Drawing.Size(168, 488);
			this.tvDatabases.Sorted = true;
			this.tvDatabases.TabIndex = 1;
			this.tvDatabases.DoubleClick += new System.EventHandler(this.tvDatabases_DoubleClick);
			this.tvDatabases.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvDatabases_AfterSelect);
			// 
			// cmDatabases
			// 
			this.cmDatabases.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						this.miDeleteDatabase,
																						this.miLoadTables,
																						this.miTestConnection});
			// 
			// miDeleteDatabase
			// 
			this.miDeleteDatabase.Index = 0;
			this.miDeleteDatabase.Text = "Delete";
			this.miDeleteDatabase.Click += new System.EventHandler(this.miDeleteDatabase_Click);
			// 
			// miLoadTables
			// 
			this.miLoadTables.Index = 1;
			this.miLoadTables.Text = "Load Tables";
			this.miLoadTables.Click += new System.EventHandler(this.miLoadTables_Click);
			// 
			// miTestConnection
			// 
			this.miTestConnection.Index = 2;
			this.miTestConnection.Text = "Test Connection";
			this.miTestConnection.Click += new System.EventHandler(this.miTestConnection_Click);
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// gbGeneral
			// 
			this.gbGeneral.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.gbGeneral.Controls.Add(this.btnFind);
			this.gbGeneral.Controls.Add(this.label6);
			this.gbGeneral.Controls.Add(this.label5);
			this.gbGeneral.Controls.Add(this.tbOutput);
			this.gbGeneral.Controls.Add(this.tbNamespace);
			this.gbGeneral.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.gbGeneral.Location = new System.Drawing.Point(176, 352);
			this.gbGeneral.Name = "gbGeneral";
			this.gbGeneral.Size = new System.Drawing.Size(546, 144);
			this.gbGeneral.TabIndex = 40;
			this.gbGeneral.TabStop = false;
			this.gbGeneral.Text = "Project Settings";
			// 
			// btnFind
			// 
			this.btnFind.Location = new System.Drawing.Point(472, 56);
			this.btnFind.Name = "btnFind";
			this.btnFind.Size = new System.Drawing.Size(24, 23);
			this.btnFind.TabIndex = 22;
			this.btnFind.Text = "...";
			this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 56);
			this.label6.Name = "label6";
			this.label6.TabIndex = 40;
			this.label6.Text = "Output Location:";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 24);
			this.label5.Name = "label5";
			this.label5.TabIndex = 40;
			this.label5.Text = "Root Namespace:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tbOutput
			// 
			this.tbOutput.Location = new System.Drawing.Point(112, 56);
			this.tbOutput.Name = "tbOutput";
			this.tbOutput.Size = new System.Drawing.Size(352, 20);
			this.tbOutput.TabIndex = 21;
			this.tbOutput.Text = "";
			// 
			// tbNamespace
			// 
			this.tbNamespace.Location = new System.Drawing.Point(112, 24);
			this.tbNamespace.Name = "tbNamespace";
			this.tbNamespace.Size = new System.Drawing.Size(352, 20);
			this.tbNamespace.TabIndex = 20;
			this.tbNamespace.Text = "";
			// 
			// gbDatabase
			// 
			this.gbDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.gbDatabase.Controls.Add(this.btnLoadTables);
			this.gbDatabase.Controls.Add(this.btnAddDatabase);
			this.gbDatabase.Controls.Add(this.databaseWizard);
			this.gbDatabase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.gbDatabase.Location = new System.Drawing.Point(176, 8);
			this.gbDatabase.Name = "gbDatabase";
			this.gbDatabase.Size = new System.Drawing.Size(546, 336);
			this.gbDatabase.TabIndex = 0;
			this.gbDatabase.TabStop = false;
			this.gbDatabase.Text = "Database Settings";
			// 
			// btnLoadTables
			// 
			this.btnLoadTables.Location = new System.Drawing.Point(360, 304);
			this.btnLoadTables.Name = "btnLoadTables";
			this.btnLoadTables.Size = new System.Drawing.Size(80, 23);
			this.btnLoadTables.TabIndex = 15;
			this.btnLoadTables.Text = "Load Tables";
			this.ttLoadTables.SetToolTip(this.btnLoadTables, "Loads the table information for the database");
			this.btnLoadTables.Click += new System.EventHandler(this.btnLoadTables_Click);
			// 
			// btnAddDatabase
			// 
			this.btnAddDatabase.Location = new System.Drawing.Point(288, 304);
			this.btnAddDatabase.Name = "btnAddDatabase";
			this.btnAddDatabase.Size = new System.Drawing.Size(64, 23);
			this.btnAddDatabase.TabIndex = 4;
			this.btnAddDatabase.Text = "Save";
			this.ttLoadTables.SetToolTip(this.btnAddDatabase, "Saves the connection information");
			this.btnAddDatabase.Click += new System.EventHandler(this.btnAddDatabase_Click);
			// 
			// databaseWizard
			// 
			this.databaseWizard.ConnectionName = "";
			this.databaseWizard.DbLocation = "";
			this.databaseWizard.DbName = "";
			this.databaseWizard.Filter = "";
			this.databaseWizard.Location = new System.Drawing.Point(8, 16);
			this.databaseWizard.Name = "databaseWizard";
			this.databaseWizard.Password = "";
			this.databaseWizard.Size = new System.Drawing.Size(536, 312);
			this.databaseWizard.TabIndex = 2;
			this.databaseWizard.Username = "";
			// 
			// tpTemplates
			// 
			this.tpTemplates.Controls.Add(this.lblOutput);
			this.tpTemplates.Controls.Add(this.lblFileCount);
			this.tpTemplates.Controls.Add(this.pbGenerate);
			this.tpTemplates.Controls.Add(this.gbTemplateUC);
			this.tpTemplates.Controls.Add(this.btnGenerate);
			this.tpTemplates.Controls.Add(this.lblPackage);
			this.tpTemplates.Controls.Add(this.label12);
			this.tpTemplates.Controls.Add(this.tbDescription);
			this.tpTemplates.Controls.Add(this.lblVersion);
			this.tpTemplates.Controls.Add(this.lblAuthor);
			this.tpTemplates.Controls.Add(this.label10);
			this.tpTemplates.Controls.Add(this.label9);
			this.tpTemplates.Controls.Add(this.label8);
			this.tpTemplates.Controls.Add(this.label7);
			this.tpTemplates.Controls.Add(this.tvTemplates);
			this.tpTemplates.Location = new System.Drawing.Point(4, 22);
			this.tpTemplates.Name = "tpTemplates";
			this.tpTemplates.Size = new System.Drawing.Size(728, 503);
			this.tpTemplates.TabIndex = 3;
			this.tpTemplates.Text = "Generate";
			// 
			// lblOutput
			// 
			this.lblOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblOutput.Location = new System.Drawing.Point(8, 477);
			this.lblOutput.Name = "lblOutput";
			this.lblOutput.Size = new System.Drawing.Size(442, 16);
			this.lblOutput.TabIndex = 17;
			this.lblOutput.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblFileCount
			// 
			this.lblFileCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lblFileCount.Location = new System.Drawing.Point(458, 461);
			this.lblFileCount.Name = "lblFileCount";
			this.lblFileCount.Size = new System.Drawing.Size(184, 35);
			this.lblFileCount.TabIndex = 16;
			// 
			// pbGenerate
			// 
			this.pbGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.pbGenerate.Location = new System.Drawing.Point(8, 461);
			this.pbGenerate.Maximum = 100;
			this.pbGenerate.Minimum = 0;
			this.pbGenerate.Name = "pbGenerate";
			this.pbGenerate.ProgressBarColor = System.Drawing.Color.Blue;
			this.pbGenerate.Size = new System.Drawing.Size(442, 16);
			this.pbGenerate.TabIndex = 15;
			this.pbGenerate.Value = 0;
			// 
			// gbTemplateUC
			// 
			this.gbTemplateUC.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.gbTemplateUC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.gbTemplateUC.Location = new System.Drawing.Point(8, 240);
			this.gbTemplateUC.Name = "gbTemplateUC";
			this.gbTemplateUC.Size = new System.Drawing.Size(706, 213);
			this.gbTemplateUC.TabIndex = 14;
			this.gbTemplateUC.TabStop = false;
			this.gbTemplateUC.Text = "Options";
			// 
			// btnGenerate
			// 
			this.btnGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnGenerate.BackColor = System.Drawing.Color.DarkOrange;
			this.btnGenerate.Location = new System.Drawing.Point(650, 461);
			this.btnGenerate.Name = "btnGenerate";
			this.btnGenerate.Size = new System.Drawing.Size(64, 23);
			this.btnGenerate.TabIndex = 11;
			this.btnGenerate.Text = "Generate";
			this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
			// 
			// lblPackage
			// 
			this.lblPackage.Location = new System.Drawing.Point(408, 24);
			this.lblPackage.Name = "lblPackage";
			this.lblPackage.Size = new System.Drawing.Size(304, 16);
			this.lblPackage.TabIndex = 10;
			this.lblPackage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label12
			// 
			this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label12.Location = new System.Drawing.Point(304, 24);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(100, 16);
			this.label12.TabIndex = 9;
			this.label12.Text = "Package: ";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tbDescription
			// 
			this.tbDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tbDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbDescription.Location = new System.Drawing.Point(304, 144);
			this.tbDescription.Multiline = true;
			this.tbDescription.Name = "tbDescription";
			this.tbDescription.Size = new System.Drawing.Size(410, 88);
			this.tbDescription.TabIndex = 8;
			this.tbDescription.Text = "";
			// 
			// lblVersion
			// 
			this.lblVersion.Location = new System.Drawing.Point(408, 88);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(304, 16);
			this.lblVersion.TabIndex = 7;
			this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblAuthor
			// 
			this.lblAuthor.Location = new System.Drawing.Point(408, 56);
			this.lblAuthor.Name = "lblAuthor";
			this.lblAuthor.Size = new System.Drawing.Size(304, 16);
			this.lblAuthor.TabIndex = 6;
			this.lblAuthor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label10
			// 
			this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label10.Location = new System.Drawing.Point(304, 120);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(100, 16);
			this.label10.TabIndex = 5;
			this.label10.Text = "Description: ";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label9
			// 
			this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label9.Location = new System.Drawing.Point(304, 88);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(100, 16);
			this.label9.TabIndex = 4;
			this.label9.Text = "Version: ";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label8
			// 
			this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label8.Location = new System.Drawing.Point(304, 56);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(100, 16);
			this.label8.TabIndex = 3;
			this.label8.Text = "Author: ";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label7
			// 
			this.label7.BackColor = System.Drawing.SystemColors.ControlLight;
			this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label7.Location = new System.Drawing.Point(0, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(280, 23);
			this.label7.TabIndex = 2;
			this.label7.Text = "Available Templates";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tvTemplates
			// 
			this.tvTemplates.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tvTemplates.CheckBoxes = true;
			this.tvTemplates.ImageIndex = -1;
			this.tvTemplates.Location = new System.Drawing.Point(0, 24);
			this.tvTemplates.Name = "tvTemplates";
			this.tvTemplates.SelectedImageIndex = -1;
			this.tvTemplates.Size = new System.Drawing.Size(280, 208);
			this.tvTemplates.TabIndex = 0;
			this.tvTemplates.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvTemplates_AfterCheck);
			this.tvTemplates.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvTemplates_AfterSelect);
			// 
			// tpTables
			// 
			this.tpTables.Controls.Add(this.lblSchemaStatus);
			this.tpTables.Controls.Add(this.schemaPropertyGrid);
			this.tpTables.Controls.Add(this.lvColumns);
			this.tpTables.Controls.Add(this.label4);
			this.tpTables.Controls.Add(this.tvTables);
			this.tpTables.Controls.Add(this.label3);
			this.tpTables.Location = new System.Drawing.Point(4, 22);
			this.tpTables.Name = "tpTables";
			this.tpTables.Size = new System.Drawing.Size(728, 503);
			this.tpTables.TabIndex = 0;
			this.tpTables.Text = "Tables";
			this.tpTables.Click += new System.EventHandler(this.tpTables_Click);
			this.tpTables.Enter += new System.EventHandler(this.tpTables_Enter);
			// 
			// lblSchemaStatus
			// 
			this.lblSchemaStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblSchemaStatus.ForeColor = System.Drawing.Color.Red;
			this.lblSchemaStatus.Location = new System.Drawing.Point(232, 484);
			this.lblSchemaStatus.Name = "lblSchemaStatus";
			this.lblSchemaStatus.Size = new System.Drawing.Size(490, 16);
			this.lblSchemaStatus.TabIndex = 7;
			this.lblSchemaStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// schemaPropertyGrid
			// 
			this.schemaPropertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.schemaPropertyGrid.CommandsVisibleIfAvailable = true;
			this.schemaPropertyGrid.HelpVisible = false;
			this.schemaPropertyGrid.LargeButtons = false;
			this.schemaPropertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.schemaPropertyGrid.Location = new System.Drawing.Point(232, 280);
			this.schemaPropertyGrid.Name = "schemaPropertyGrid";
			this.schemaPropertyGrid.Size = new System.Drawing.Size(490, 200);
			this.schemaPropertyGrid.TabIndex = 6;
			this.schemaPropertyGrid.Text = "propertyGrid1";
			this.schemaPropertyGrid.ToolbarVisible = false;
			this.schemaPropertyGrid.ViewBackColor = System.Drawing.SystemColors.Window;
			this.schemaPropertyGrid.ViewForeColor = System.Drawing.SystemColors.WindowText;
			this.schemaPropertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.schemaPropertyGrid_PropertyValueChanged);
			// 
			// lvColumns
			// 
			this.lvColumns.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lvColumns.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lvColumns.CheckBoxes = true;
			this.lvColumns.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.cName,
																						this.cAlias,
																						this.cPK,
																						this.cFK,
																						this.cAllowNulls,
																						this.cAutoIncrement,
																						this.cType,
																						this.cLength});
			this.lvColumns.FullRowSelect = true;
			this.lvColumns.Location = new System.Drawing.Point(232, 24);
			this.lvColumns.Name = "lvColumns";
			this.lvColumns.Size = new System.Drawing.Size(490, 248);
			this.lvColumns.TabIndex = 5;
			this.lvColumns.View = System.Windows.Forms.View.Details;
			this.lvColumns.Click += new System.EventHandler(this.lvColumns_Click);
			this.lvColumns.ItemActivate += new System.EventHandler(this.lvColumns_ItemActivate);
			this.lvColumns.SelectedIndexChanged += new System.EventHandler(this.lvColumns_SelectedIndexChanged);
			this.lvColumns.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvColumns_ItemCheck);
			// 
			// cName
			// 
			this.cName.Text = "Name";
			this.cName.Width = 134;
			// 
			// cAlias
			// 
			this.cAlias.Text = "Alias";
			this.cAlias.Width = 137;
			// 
			// cPK
			// 
			this.cPK.Text = "PK";
			this.cPK.Width = 29;
			// 
			// cFK
			// 
			this.cFK.Text = "FK";
			this.cFK.Width = 28;
			// 
			// cAllowNulls
			// 
			this.cAllowNulls.Text = "AllowNulls";
			// 
			// cAutoIncrement
			// 
			this.cAutoIncrement.Text = "AutoIncrement";
			this.cAutoIncrement.Width = 38;
			// 
			// cType
			// 
			this.cType.Text = "Type";
			this.cType.Width = 104;
			// 
			// cLength
			// 
			this.cLength.Text = "Length";
			this.cLength.Width = 79;
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.label4.BackColor = System.Drawing.SystemColors.ControlLight;
			this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label4.Location = new System.Drawing.Point(232, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(490, 23);
			this.label4.TabIndex = 4;
			this.label4.Text = "Column List";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tvTables
			// 
			this.tvTables.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left)));
			this.tvTables.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tvTables.CheckBoxes = true;
			this.tvTables.ImageIndex = 1;
			this.tvTables.ImageList = this.imageList1;
			this.tvTables.Location = new System.Drawing.Point(0, 24);
			this.tvTables.Name = "tvTables";
			this.tvTables.Size = new System.Drawing.Size(224, 456);
			this.tvTables.TabIndex = 2;
			this.tvTables.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvTables_AfterCheck);
			this.tvTables.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvTables_AfterSelect);
			// 
			// label3
			// 
			this.label3.BackColor = System.Drawing.SystemColors.ControlLight;
			this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(0, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(224, 23);
			this.label3.TabIndex = 1;
			this.label3.Text = "Table List";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1,
																					  this.miHelp,
																					  this.miAbout});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "Documentation";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// miHelp
			// 
			this.miHelp.Index = 1;
			this.miHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																				   this.miWebsite,
																				   this.miSupport});
			this.miHelp.Text = "Help";
			this.miHelp.Click += new System.EventHandler(this.miHelp_Click);
			// 
			// miWebsite
			// 
			this.miWebsite.Index = 0;
			this.miWebsite.Text = "Product Website";
			this.miWebsite.Click += new System.EventHandler(this.miWebsite_Click);
			// 
			// miSupport
			// 
			this.miSupport.Index = 1;
			this.miSupport.Text = "Support";
			this.miSupport.Click += new System.EventHandler(this.miSupport_Click);
			// 
			// miAbout
			// 
			this.miAbout.Index = 2;
			this.miAbout.Text = "About";
			this.miAbout.Click += new System.EventHandler(this.miAbout_Click);
			// 
			// lblCurrentConnection
			// 
			this.lblCurrentConnection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblCurrentConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblCurrentConnection.Location = new System.Drawing.Point(240, 4);
			this.lblCurrentConnection.Name = "lblCurrentConnection";
			this.lblCurrentConnection.Size = new System.Drawing.Size(482, 16);
			this.lblCurrentConnection.TabIndex = 1;
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(736, 529);
			this.Controls.Add(this.lblCurrentConnection);
			this.Controls.Add(this.tabControl);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Menu = this.mainMenu1;
			this.Name = "MainForm";
			this.Text = "Adapdev Codus";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.tabControl.ResumeLayout(false);
			this.tpSetup.ResumeLayout(false);
			this.gbGeneral.ResumeLayout(false);
			this.gbDatabase.ResumeLayout(false);
			this.tpTemplates.ResumeLayout(false);
			this.tpTables.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		#region Form Control Event Handlers
		private void tvTables_AfterCheck(object sender, TreeViewEventArgs e)
		{
			object o = e.Node.Tag;

			if (o != null && o is TableSchema) //grab it if it's a TableSchema
			{
				if (e.Node.Checked) // change color to black
				{
					((TableSchema) o).IsActive = true;
					e.Node.ForeColor = Color.Black;
				}
				else // change color to gray
				{
					((TableSchema) o).IsActive = false;
					e.Node.ForeColor = Color.LightGray;
				}
			}
			else // if it's not a TableSchema, then it's a parent node
			{
				if (e.Node.Checked) // check all subnodes
				{
					foreach (TreeNode n in e.Node.Nodes)
					{
						n.Checked = true;
					}
				}
				else // uncheck all subnodes
				{
					foreach (TreeNode n in e.Node.Nodes)
					{
						n.Checked = false;
					}
				}
			}
		}

		private void tvTables_AfterSelect(object sender, TreeViewEventArgs e)
		{
			object o = e.Node.Tag;
			if (o != null && o is TableSchema) { // if it's a TableSchema, display all the columns
				this.FillColumns(((TableSchema) o).SortedColumns);
				this.schemaPropertyGrid.SelectedObject = o as TableSchema;
				this.lvColumns.HeaderStyle = ColumnHeaderStyle.Clickable;
			} else {
				// TGL: Set the Selected Object to Null to clear the section if a root node selected
				this.schemaPropertyGrid.SelectedObject = null;
				this.lvColumns.Items.Clear();
				this.lvColumns.HeaderStyle = ColumnHeaderStyle.Nonclickable;
			}
			lblSchemaStatus.Text = "";
		}

		private void lvColumns_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			if (columnsBuilt)
			{
				ListViewItem o = this.lvColumns.Items[e.Index];
				this.schemaPropertyGrid.SelectedObject = o.Tag;

				if (e.NewValue == CheckState.Checked)
				{
					((ColumnSchema) o.Tag).IsActive = true;
					o.ForeColor = Color.Black;
				}
				else
				{
					((ColumnSchema) o.Tag).IsActive = false;
					o.ForeColor = Color.LightGray;
				}	
			}
		}
		private void btnGenerate_Click(object sender, EventArgs e) {
			Thread t = new Thread(new ThreadStart(GenerateCode));
			t.ApartmentState = ApartmentState.STA;
			t.Start();
		}

		private void tvTemplates_AfterSelect(object sender, TreeViewEventArgs e) {
			AbstractConfig ac = (AbstractConfig) e.Node.Tag;
			this.lblPackage.Text = ac.PackageName;
			this.lblAuthor.Text = ac.Author;
			this.lblVersion.Text = ac.Version;
			this.tbDescription.Text = ac.PackageDescription;
			this.currentTemplate = ac.PackageName;

			ShowIGUIConfigurable(ac);
		}

		private void ShowIGUIConfigurable(AbstractConfig ac)
		{
			this.gbTemplateUC.Controls.Clear();
			if (ac is IGUIConfigurable) 
			{
				this.gbTemplateUC.Text = "Options for " + ac.PackageName;
				this.gbTemplateUC.Controls.Add((ac as IGUIConfigurable).GetGUIConfigurator());
				this.gbTemplateUC.Controls[0].Dock = DockStyle.Fill;
			}
			else
			{
				this.gbTemplateUC.Text = "Options";	
			}
		}

		private void btnFind_Click(object sender, EventArgs e) {
			this.fbdOutputLocation.ShowNewFolderButton = true;
			this.fbdOutputLocation.Description = "Locate the root folder where all project output will be placed.";

			DialogResult result = this.fbdOutputLocation.ShowDialog();

			if (result == DialogResult.OK) {
				this.tbOutput.Text = this.fbdOutputLocation.SelectedPath;
			}
		}

		private void btnAddDatabase_Click(object sender, EventArgs e) {
			this.SaveSettingsToTree();
			this.SaveSettings();
		}

		private void tvDatabases_AfterSelect(object sender, TreeViewEventArgs e) {
			this.isLoaded = false;
			this.LoadDatabaseSetting(e.Node.Tag as ProjectSetting);
			this.lblCurrentConnection.Text = "Current Connection: " + (e.Node.Tag as DatabaseSetting).ConnectionName;
		}

		private void MainForm_Closing(object sender, CancelEventArgs e) {
			this.SaveSettings();
		}
		private void MainForm_Load(object sender, EventArgs e) {
			// If the settings file isn't present in the user's
			// local application data folder, then copy it from
			// the install folder
			if(!File.Exists(Constants.SettingsFile))
			{
				File.Copy(Constants.LocalSettingsFile, Constants.SettingsFile);
			}

			// Load the settings file
			LoadSettingsCommand load = new LoadSettingsCommand(this.tvDatabases);
			load.Execute();
			this.databases = load.Databases;
		}

		private void miDeleteDatabase_Click(object sender, EventArgs e) {
			this.tvDatabases.BeginUpdate();
			this.tvDatabases.SelectedNode.Remove();
			this.tvDatabases.Refresh();
			this.tvDatabases.EndUpdate();
		}

		private void tvDatabases_DoubleClick(object sender, EventArgs e) {
			this.LoadDatabaseSetting(this.tvDatabases.SelectedNode.Tag as ProjectSetting);
			this.LoadTables();
		}

		private void tabControl_TabIndexChanged(object sender, EventArgs e) {
		}

		private void tpTables_Enter(object sender, EventArgs e) {
		}

		private void tpTables_Click(object sender, EventArgs e) {
		}

		private void tabControl_SelectedIndexChanged(object sender, EventArgs e) {
			if (!this.isLoaded) {
				if ((this.tabControl.SelectedIndex == 1) ||
					this.tabControl.SelectedIndex == 2) {
					this.LoadTables();
				}
			}

			if (this.tabControl.SelectedIndex == 2) {
				if (this.tvTemplates.Nodes.Count > 0)
					this.tvTemplates.SelectedNode = this.tvTemplates.Nodes[0];
			}
		}

		private void miLoadTables_Click(object sender, EventArgs e) {
			this.LoadTables();
		}

		private void tbConnectionName_TextChanged(object sender, EventArgs e) {
			this.btnLoadTables.Enabled = (
				!(this.databaseWizard.ConnectionName == string.Empty) && 
				!(this.databaseWizard.GetConnectionString() == string.Empty));
		}

		private void miTestConnection_Click(object sender, EventArgs e) {
			this.databaseWizard.TestConnection();
		}

		private void cg_ICodeDomTemplateProcessed(object sender, ICodeDomTemplateEventArgs e) {
			this.pbGenerate.Value += 1;
			this.lblOutput.Text = "Processing " + e.ICodeDomTemplate.FileName + Environment.NewLine;
			this.processed++;
		}

		private void cg_ICodeTemplateProcessed(object sender, ICodeTemplateEventArgs e) {
			this.pbGenerate.Value += 1;
			this.lblOutput.Text = "Processing " + e.ICodeTemplate.FileName + Environment.NewLine;
			this.processed++;
		}

		private void miAbout_Click(object sender, EventArgs e) {
			About about = new About();
			about.ShowDialog();
		}

		private void btnLoadTables_Click(object sender, EventArgs e) {
			if (databaseWizard.TestConnections()) {
				this.SaveSettingsToTree();
				this.SaveSettings();
				this.LoadTables();
			}
		}

		public static string GetVersion() {
			Version version = Assembly.GetExecutingAssembly().GetName().Version;
			return String.Format("Adapdev Codus v{0}.{1}.{2}", version.Major, version.Minor, version.Build);
		}

		private void miHelp_Click(object sender, EventArgs e) {
			Process.Start("http://www.adapdev.com/codus/v1.0/");
		}

		private void miWebsite_Click(object sender, EventArgs e) {
			Process.Start(MainForm.website);
		}

		private void miSupport_Click(object sender, EventArgs e) {
			About about = new About();
			about.ShowDialog();
		}

		private void menuItem1_Click(object sender, EventArgs e) {
			AssemblyName name = Assembly.GetExecutingAssembly().GetName();
			Process.Start(MainForm.website + String.Format("{0}.{1}.{2}", name.Version.Major, name.Version.Minor, name.Version.Build));
		}

		private void AbstractConfig_Processing(string info) {
			this.lblOutput.Text = info;
		}

		private void lvColumns_Click(object sender, EventArgs e) {
			if (this.lvColumns.SelectedItems.Count > 0)
				this.schemaPropertyGrid.SelectedObject = this.lvColumns.SelectedItems[0].Tag;
		}

		private void lvColumns_ItemActivate(object sender, EventArgs e) {
			this.schemaPropertyGrid.SelectedObject = this.lvColumns.SelectedItems[0].Tag;		
		}


		private void schemaPropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e) {
			if(this.schemaPropertyGrid.SelectedObject is TableSchema) {
				this.FillTableList();
			}
			else if(this.schemaPropertyGrid.SelectedObject is ColumnSchema) {
				this.FillColumns((this.tvTables.SelectedNode.Tag as TableSchema).SortedColumns);
			}
			this.lblSchemaStatus.Text = e.ChangedItem.Label + " updated to " + e.ChangedItem.Value;
		}

		private void lvColumns_SelectedIndexChanged(object sender, EventArgs e) {
			if(this.lvColumns.SelectedItems.Count > 0) {
				this.schemaPropertyGrid.SelectedObject = this.lvColumns.SelectedItems[0].Tag;
			}
			lblSchemaStatus.Text = "";
		}

		private void typesTreeView_AfterSelect(object sender, TreeViewEventArgs e) {
			this.btnAddDatabase.Enabled = (e.Node.Parent != null);
			this.btnLoadTables.Enabled = ((e.Node.Parent != null) && (this.databaseWizard.ConnectionName != string.Empty));
		}
		#endregion

		#region Table Loading Routines
		// Grabs all tables for the specified database
		// and adds them to the tables treenode
		public void FillTableList()
		{
			new FillTablesCommand(this, this.databaseSchema, this.tvTables).Execute();
		}

		/// <summary>
		/// Determines the selected settings from a node and calls a load routine to 
		/// load the tables for the settings. 
		/// 
		/// TODO: This routine loads the selected node, or if none is selected it 
		/// loads the tree view item 0. If the user has made changes to the prompts 
		/// but has not saved, then this will overwrite those changes by loading 
		/// the data at position 0. 
		/// </summary>
		private void LoadTables()
		{
			// If the selected Node is null, then select the node
			if (this.tvDatabases.SelectedNode == null) {
				if (this.tvDatabases.Nodes.Count > 0) {
					this.tvDatabases.SelectedNode = this.tvDatabases.Nodes[0];
				}
			}
			if (this.tvDatabases.SelectedNode != null) {
				DatabaseSetting databaseSetting = this.tvDatabases.SelectedNode.Tag as DatabaseSetting;
				this.isLoaded = LoadSchemaForSettings(databaseSetting);
			}
		}

		/// <summary>
		/// Loads the Table details for a given Selected Database 
		/// </summary>
		private bool LoadSchemaForSettings(DatabaseSetting databaseSetting)
		{
			try
			{
				// Launch a Progress Window to display progress while loading 
				// the table details, and ensure this information is threaded. 
				ProgressWindow progress = new ProgressWindow();
				log.Debug(databaseSetting);
				LoadTablesDetails td = new LoadTablesDetails(databaseSetting, progress);
				ThreadPool.QueueUserWorkItem( new WaitCallback( LoadTablesThreaded ), td );
				progress.ShowDialog();

				// After the thread has completed, we need to check that all was loaded correctly. 
				if (this.databaseSchema != null) {
					this.databaseSchema.ConnectionString = databaseSetting.ConnectionString;
					this.FillTableList();

					this.databaseSchema.DatabaseProviderType = databaseSetting.DbProviderType; 
					this.databaseSchema.DatabaseType		 = databaseSetting.DbType;

					if (this.databaseSchema.Name == String.Empty || this.databaseSchema.Name == "Unknown") {
						this.databaseSchema.Name = databaseSetting.DatabaseName;
					}
					return true;
				} else {
					MessageBox.Show("Unable to load tables!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);					
					return false;
				}
			}
			catch (Exception ex)
			{
				log.Debug("Unable to load tables!", ex);
				MessageBox.Show(ex.Message, "Unable to load tables!", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
		}

		public void FillColumns(SortedList sl) {
			new FillColumnsCommand(this, sl, this.lvColumns).Execute();
			columnsBuilt = true;
		}

		/// <summary>
		/// This method is invoked as a Threaded Queued object. 
		/// </summary>
		/// <param name="status"></param>
		private void LoadTablesThreaded( object status ) {

			LoadTablesDetails td = status as LoadTablesDetails;
			try {
				td.ProgressCallBack.Begin();
				this.databaseSchema = SchemaBuilder.CreateDatabaseSchema(td.DatabaseSetting.OleDbConnectionString, td.DatabaseSetting.DbType, td.DatabaseSetting.DbProviderType, td.DatabaseSetting.Filter, td.ProgressCallBack);
			}
			catch( ThreadAbortException ) {
				// We want to exit gracefully here (if we're lucky)
			}
			catch( ThreadInterruptedException ) {
				// And here, if we can
			}
			finally {
				td.ProgressCallBack.End();
			}
		}
		#endregion
        
		#region Code Generation
		private void GenerateCode()
		{
			Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
			this.processed = 0;
			this.pbGenerate.Value = 0;
			this.lblFileCount.Text = String.Empty;

			if (!(this.databaseSchema == null))
			{
				this.btnGenerate.Enabled = false;
				this.lblOutput.Text = "";
				string output = "";

				CodeGenerator cg = new CodeGenerator(".");
				cg.ICodeDomTemplateProcessed += new ICodeDomTemplateProcessedEventHandler(cg_ICodeDomTemplateProcessed);
				cg.ICodeTemplateProcessed += new ICodeTemplateProcessedEventHandler(cg_ICodeTemplateProcessed);

				// Determine output location
				if (this.tbOutput.Text.Length > 0)
				{
					cg.OutputDir = this.tbOutput.Text;
				}

				output = cg.OutputDir;

				foreach (TreeNode tn in this.tvTemplates.Nodes)
				{
					if (tn.Checked)
					{
						try
						{
							this.pbGenerate.Value = 0;

							AbstractConfig ac = (AbstractConfig) tn.Tag;
							ac.Processing += new ProcessCustomCodeHandler(AbstractConfig_Processing);
							ac.NameSpace = this.tbNamespace.Text;

							// If using a GUI for configuration, then get custom properties
							if (ac is IGUIConfigurable)
							{
								ac.CustomProperties = (ac as IGUIConfigurable).GetGUIConfigurator().GetCustomProperties();
							}

							// Create output directory path
							cg.OutputDir = Path.Combine(output, this.databaseWizard.ConnectionName);
							cg.OutputDir = Path.Combine(cg.OutputDir, ac.PackageName);
							ac.OutputDirectory = cg.OutputDir;

							this.lblOutput.Text += ("Processing " + ac.PackageName) + Environment.NewLine;
							this.lblFileCount.Text = "Processing " + ac.PackageName;

							// If it's a database project, then get the database info
							if (ac is AbstractSchemaConfig)
							{
								((AbstractSchemaConfig) ac).ConnectionString = this.databaseWizard.GetConnectionString();
								((AbstractSchemaConfig) ac).DatabaseSchema = this.databaseSchema;
							}

							// Get templates from assembly
							ArrayList codedom = ac.GetCodeDomTemplates();
							ArrayList code = ac.GetCodeTemplates();
							int count = codedom.Count + code.Count;

							if (count > 0)
							{
								this.pbGenerate.Maximum = count;
								this.pbGenerate.Minimum = 0;
							}

							// Process CodeDom templates
							if (codedom.Count > 0)
							{
								this.lblOutput.Text += "Processing CodeDom templates..." + Environment.NewLine;
								cg.ProcessICodeDomTemplatesToFiles(codedom);
							}

							// Process Code templates
							if (code.Count > 0)
							{
								this.lblOutput.Text += "Processing Code templates..." + Environment.NewLine;
								cg.ProcessICodeTemplatesToFiles(code);
							}

							// Process any custom code for the GUI
							if (ac is IGUIConfigurable)
							{
								(ac as IGUIConfigurable).GetGUIConfigurator().PreRunCustomCode(ac);
							}

							// Process any custom code
							ac.ProcessCustomCode();

							// Process any custom code for the GUI
							if (ac is IGUIConfigurable)
							{
								IGUIConfigurable gui = ac as IGUIConfigurable;
								GUIConfigurator guic = gui.GetGUIConfigurator();
								guic.PostRunCustomCode(ac);

								// Show any final message
								if(guic.PostMessage.Length > 0) MessageBox.Show(guic.PostMessage, "Attention", MessageBoxButtons.OK );
							}
						}
						catch (Exception e)
						{
							log.Error(e.Message, e);
							MessageBox.Show(e.Message + " : " + e.StackTrace);
						}

						Thread.Sleep(2000);
					}
				}
				this.lblOutput.Text = "DONE" + Environment.NewLine;
				this.lblFileCount.Text = String.Format("DONE. Processed {0} files.", this.processed);
				this.btnGenerate.Enabled = true;
			}
			else
			{
				this.ShowLoadTablesMessage();
			}
		}

		private void LoadCodeTemplates()
		{
			TemplateProjects tp = new TemplateProjects();
			tp.ReadXml(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TemplateProjects.xml"), XmlReadMode.ReadSchema);

			foreach (TemplateProjects.TemplateProjectRow tpr in tp.TemplateProject)
			{
				try
				{
					SchemaConfigImpl ac = new SchemaConfigImpl(String.Empty, String.Empty, null, tpr, tp.Solution);
					this.CreateAbstractConfigNode(ac, false);
				}
				catch (Exception ex)
				{
					log.Error(ex.Message, ex);
				}
			}

			foreach (TemplateProjects.CompiledTemplateRow ctr in tp.CompiledTemplate)
			{
				Assembly a = reflectionCache.GetAssembly(ctr.assembly);
				Type t = a.GetType(ctr._class);

				try
				{
					string display = String.Empty;
					if (t.IsSubclassOf(typeof (AbstractSchemaConfig)))
					{
						AbstractSchemaConfig ac = (AbstractSchemaConfig) Activator.CreateInstance(t, new object[] {String.Empty, String.Empty, null});
						this.CreateAbstractConfigNode(ac, false);
					}
					else if (t.IsSubclassOf(typeof (AbstractConfig)))
					{
						AbstractConfig ac = (AbstractConfig) Activator.CreateInstance(t, new object[] {String.Empty});
						this.CreateAbstractConfigNode(ac, false);
					}
				}
				catch (Exception e)
				{
					log.Error(e.Message, e);
				}
			}
		}

		private void CreateAbstractConfigNode(AbstractConfig ac, bool active)
		{
			TreeNode tn = new TreeNode(ac.PackageName);
			tn.Checked = active;
			tn.Tag = ac;
			this.tvTemplates.Nodes.Add(tn);
		}
		#endregion

		#region Settings Load & Save Routines
		private void ShowLoadTablesMessage()
		{
			MessageBox.Show("Please load the database tables first.");
		}

		public void LoadDatabaseSetting(ProjectSetting ds)
		{
			this.databaseWizard.SetDatabaseSetting(ds);
			this.tbNamespace.Text = ds.Namespace;
			this.tbOutput.Text = ds.OutputLocation;
		}

		private void SaveSettingsToTree() {
			ProjectSetting ds = new ProjectSetting(this.databaseWizard.GetDatabaseSetting());
			ds.Namespace = this.tbNamespace.Text;
			ds.OutputLocation = this.tbOutput.Text;

			if (!this.databases.ContainsKey(this.databaseWizard.ConnectionName)) {
				TreeNode node = new TreeNode(ds.ConnectionName);
				node.Tag = ds;

				this.tvDatabases.Nodes.Add(node);
				this.tvDatabases.SelectedNode = node;
				this.databases.Add(ds.ConnectionName, node);
			}
			else {
				TreeNode tn = this.databases[this.databaseWizard.ConnectionName] as TreeNode;
				tn.Tag = ds;
				this.databases[this.databaseWizard.ConnectionName] = tn;
			}
		}

		public void SaveSettings()
		{
			new SaveSettingsCommand(this.tvDatabases).Execute();
		}
		#endregion

		private void tvTemplates_AfterCheck(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			AbstractConfig ac = (AbstractConfig) e.Node.Tag;
			this.ShowIGUIConfigurable(ac);		
		}
	}
}