using GUIConfigurator = Codus.GUI.Extensions.GUIConfigurator;
using IGUIConfigurable = Codus.GUI.Extensions.IGUIConfigurable;
using System;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Adapdev.CodeGen;
using Adapdev.Data.Schema;
using Adapdev.Reflection;
using EnvDTE;
using Microsoft.VisualBasic;
using Microsoft.Win32;
using NVelocity;
using VSLangProj;
using Constants = EnvDTE.Constants;
using Thread = System.Threading.Thread;

namespace Adapdev.Codus.GUI
{
	using Adapdev.IO;


	/// <summary>
	/// Summary description for SchemaConfigImpl.
	/// </summary>
	internal class SchemaConfigImpl : AbstractSchemaConfig, IGUIConfigurable
	{
		private TemplateProjects.TemplateProjectRow tpr = null;
		private TemplateProjects.SolutionRow sr = null;
		private GUIConfigurator gui = null;
		private static string VS2003_DIRECTORY = SchemaConfigImpl.GetVSNETLocation();
		private static string VS2003_CSHARPCONSOLE = Path.Combine(VS2003_DIRECTORY,ConfigurationSettings.AppSettings["VS2003_CSHARPCONSOLE"]);
		private static string VS2003_CSHARPDLL = Path.Combine(VS2003_DIRECTORY,ConfigurationSettings.AppSettings["VS2003_CSHARPDLL"]);
		private static string VS2003_CSHARPEXE = Path.Combine(VS2003_DIRECTORY,ConfigurationSettings.AppSettings["VS2003_CSHARPEXE"]);
		private static string VS2003_CSHARPWEBSERVICE = Path.Combine(VS2003_DIRECTORY,ConfigurationSettings.AppSettings["VS2003_CSHARPWEBSERVICE"]);
		private static string VS2003_CSHARPWEBSITE = Path.Combine(VS2003_DIRECTORY,ConfigurationSettings.AppSettings["VS2003_CSHARPWEBSITE"]);

		private static string VS2003_VBCONSOLE = Path.Combine(VS2003_DIRECTORY,ConfigurationSettings.AppSettings["VS2003_VBCONSOLE"]);
		private static string VS2003_VBDLL = Path.Combine(VS2003_DIRECTORY,ConfigurationSettings.AppSettings["VS2003_VBDLL"]);
		private static string VS2003_VBEXE = Path.Combine(VS2003_DIRECTORY,ConfigurationSettings.AppSettings["VS2003_VBEXE"]);
		private static string VS2003_VBWEBSERVICE = Path.Combine(VS2003_DIRECTORY,ConfigurationSettings.AppSettings["VS2003_VBWEBSERVICE"]);
		private static string VS2003_VBWEBSITE = Path.Combine(VS2003_DIRECTORY,ConfigurationSettings.AppSettings["VS2003_VBWEBSITE"]);

		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public SchemaConfigImpl(string connectionString, string nameSpace, DatabaseSchema databaseSchema, TemplateProjects.TemplateProjectRow tpr, TemplateProjects.SolutionDataTable solutionDataTable) : base(connectionString, nameSpace, databaseSchema)
		{
			this.tpr = tpr;
			if(solutionDataTable != null) this.sr = this.GetSolutionRow(solutionDataTable, tpr);
			this.gui = this.LoadGUI();
		}

		internal static string GetVSNETLocation()
		{
			object o = null;
			string key = ConfigurationSettings.AppSettings["VS2003_DIRECTORY_RKEY"];
			string value = ConfigurationSettings.AppSettings["VS2003_DIRECTORY_RVALUE"];

			try
			{
				o = Registry.LocalMachine.OpenSubKey(key).GetValue(value);
			}
			catch (Exception){}
			if(o == null)return String.Empty;
			return o.ToString();
		}

		private TemplateProjects.SolutionRow GetSolutionRow(TemplateProjects.SolutionDataTable dataTable, TemplateProjects.TemplateProjectRow tpr)
		{
			foreach (TemplateProjects.SolutionRow solutionRow in dataTable.Rows)
			{
				if(solutionRow.name == tpr.solution)
					return solutionRow;
			}
			throw new Exception("Solution '" + tpr.solution + "' not found.");
		}

		// A list of ICodeDomTemplates to process
		public override ArrayList GetCodeDomTemplates()
		{
			return new ArrayList();
		}

		// A list of ICodeTemplates to process
		public override ArrayList GetCodeTemplates()
		{
			ArrayList templates = new ArrayList();

			foreach (TableSchema ti in this.databaseSchema.Tables.Values)
			{
				// Process if the TableSchema is checked
				if(ti.IsActive)
				{
					foreach (TemplateProjects.TemplatesRow tsr in this.tpr.GetTemplatesRows())
					{
						foreach (TemplateProjects.TemplateRow tr in tsr.GetTemplateRows())
						{
							this.CheckTemplateRowValues(tr);

							if (tr.type.ToLower() == "nvelocitytable")
							{
								if (ValidateDependencies(tr))
								{
									NVelocityTableCodeTemplate n = new NVelocityTableCodeTemplate(this.databaseSchema, ti, tr.outputDirectory, tr.templateFile, this.nameSpace, this.GetTableFileName(tr.outputFileName, ti), tr.outputFileExtension, tr.className, Convert.ToBoolean(tr.overWrite));
									if(this.CustomProperties != null)
									{
										foreach(object o in this.CustomProperties.Keys)
										{
											// Put the custom properties into the NVelocity context
											n.Context.Put(o.ToString(), this.CustomProperties[o]);
											this.AddGeneralItems(n.Context);
										}
									}
									templates.Add(n);
								}
							}
						}
					}
				}
			}


			foreach (TemplateProjects.TemplatesRow tsr in this.tpr.GetTemplatesRows())
			{
				foreach (TemplateProjects.TemplateRow tr in tsr.GetTemplateRows())
				{
					this.CheckTemplateRowValues(tr);

					if (tr.type.ToLower() == "nvelocity")
					{
						NVelocityCodeTemplate n = new NVelocityCodeTemplate(tr.outputFileName, tr.outputFileExtension, Convert.ToBoolean(tr.overWrite), tr.templateFile, tr.outputDirectory, tr.className, this.nameSpace );
						this.AddGeneralItems(n.Context);
						templates.Add(n);
					}
				}
			}

			return templates;
		}

		// Set default values
		private void CheckTemplateRowValues(TemplateProjects.TemplateRow tr)
		{
			if(tr.IsclassNameNull()) tr.className = "";
			if(tr.IsoutputDirectoryNull()) tr.outputDirectory = @"C:\";
			if(tr.IsoverWriteNull()) tr.overWrite = "true";
			if(tr.IstypeNull()) tr.type = "nvelocitytable";
		}

		private bool ValidateDependencies(TemplateProjects.TemplateRow tr)
		{
			bool valid = true;

			foreach (TemplateProjects.depsRow deps in tr.GetdepsRows())
			{
				foreach (TemplateProjects.depRow dep in deps.GetdepRows())
				{
					if (this.CheckValue(dep))
					{
						valid = true;
					}
					else
					{
						valid = false;
						break;
					}
				}
			}
			return valid;
		}

		private bool CheckValue(TemplateProjects.depRow dr)
		{
			// Make sure data is present
			if(dr.IstypeNull()) dr.type = "eq";
			if(dr.IskeyNull()) return true;
			if(dr.IsvalueNull()) return true;

			// Make sure that value passed in is equal to key's value in xml file
			if (dr.type.ToLower() == "eq" && this.CustomProperties[dr.key] != null)
			{
				if (this.CustomProperties[dr.key].ToString().ToLower() == dr.value.ToLower())
					return true;
				else
					return false;
			}

			// Make sure that value passed in is not equal to key's value in xml file
			if (dr.type.ToLower() == "neq" && this.CustomProperties[dr.key] != null)
			{
				if (this.CustomProperties[dr.key].ToString().ToLower() != dr.value.ToLower())
					return true;
				else
					return false;
			}

			return false;
		}

		private string GetTableFileName(string fileName, TableSchema ts)
		{
			return fileName.Replace("${table}", ts.Alias);
		}

		// The name of the package.  Primarily used for IDE display
		public override string PackageName
		{
			get { return this.tpr.Name; }
		}

		// The package description.
		public override string PackageDescription
		{
			get { return this.tpr.Description; }
		}

		// The author of the template library
		public override string Author
		{
			get { return this.tpr.Author; }
		}

		// The version of the template library
		public override string Version
		{
			get { return this.tpr.Version; }
		}

		// Copyright info
		public override string Copyright
		{
			get { return this.tpr.Copyright; }
		}

		public override void ProcessCustomCode()
		{
			if (this.sr != null
				&& !this.tpr.IssolutionNull()
				&& Directory.Exists(SchemaConfigImpl.VS2003_DIRECTORY) 
				&& !(this.CustomProperties["generatesln"] == null) 
				&& this.CustomProperties["generatesln"].ToString().ToLower() == "true")
			{

				// Create the solution directory [OutputDirectory]\sln
				this.OnProcessing("Creating VS.NET 2003 Solution");
				string solutiondir = Path.Combine(this.OutputDirectory, "sln");
				Directory.CreateDirectory(solutiondir);

				// Insantiate the VS.NET environment
				DTE dte;
				dte = (DTE) Interaction.CreateObject("VisualStudio.DTE.7.1", "");
				dte.SuppressUI = true;

				// Create the message filter to handle threading errors in VS.NET
				MessageFilter.Register();

				try
				{
					dte.MainWindow.Activate();
					dte.Windows.Item(EnvDTE.Constants.vsWindowKindSolutionExplorer).Activate();

					// Create the Codus output window
					OutputWindowPane owp = CreateOutputWindowPane(dte);
					owp.OutputString("Starting Codus VS.NET integration..." + Environment.NewLine);

					// Create the VS.NET solution
					Solution solution = dte.Solution;
					solution.Create(solutiondir, this.sr.name);
					owp.OutputString("Creating solution...please wait." + Environment.NewLine);

					foreach (TemplateProjects.ProjectsRow psr in this.sr.GetProjectsRows())
					{
						Hashtable projects = new Hashtable();
						int i = 1;

						// First need to creat all projects, since we'll need to retrieve
						// them in random order later
						foreach (TemplateProjects.ProjectRow projectRow in psr.GetProjectRows())
						{
							// Don't create a project unless it's used by one of the templates
							if(this.ProjectIsReferenced(projectRow))
							{
								owp.OutputString("Creating project: " + projectRow.name + Environment.NewLine);
								this.OnProcessing("Creating project: " + projectRow.name);
								Project p = null;
								if(this.GetVSTemplate(projectRow.type) == SchemaConfigImpl.VS2003_CSHARPWEBSERVICE
									|| this.GetVSTemplate(projectRow.type) == SchemaConfigImpl.VS2003_CSHARPWEBSITE)
								{
									// Add website or webservice template
									p = solution.AddFromTemplate(this.GetVSTemplate(projectRow.type), "http://localhost/" + projectRow.name, projectRow.name, false);
								}
								else
								{
									// Add regular template
									p = solution.AddFromTemplate(this.GetVSTemplate(projectRow.type), Path.Combine(solutiondir, projectRow.name), projectRow.name, false);
								}
								Project sp = solution.Projects.Item(i);

								projects[projectRow.name] = sp;
								i++;
							}
						}					

						// Now that the projects are created, go and add
						// references and files
						owp.OutputString(Environment.NewLine);
						foreach (TemplateProjects.ProjectRow projectRow in psr.GetProjectRows())
						{
							// Don't deal with it unless a template uses it
							if(this.ProjectIsReferenced(projectRow))
							{
								Project p = projects[projectRow.name] as Project;
								owp.OutputString("Updating project: " + projectRow.name + Environment.NewLine);
								this.OnProcessing("Adding references");

								// Create the project
								VSProject vsp = p.Object as VSProject;
								foreach(TemplateProjects._refRow refRow in projectRow.GetrefRows())
								{
									if(!refRow.IsassemblyNull())
									{
										// Add assembly references
										owp.OutputString("-- Adding dll reference: " + this.GetSharedPath(refRow.assembly) + Environment.NewLine);
										vsp.References.Add(this.GetSharedPath(refRow.assembly));
									}

									if(!refRow.IsprojectNull())
									{
										// Add project references
										owp.OutputString("-- Adding project reference: " + this.GetSharedPath(refRow.project) + Environment.NewLine);
										vsp.References.AddProject(projects[refRow.project] as Project);
									}
								}

								Hashtable folders = new Hashtable();
								foreach (TemplateProjects.TemplatesRow templatesRow in this.tpr.GetTemplatesRows())
								{
									foreach (TemplateProjects.TemplateRow templateRow in templatesRow.GetTemplateRows())
									{
										if(!templateRow.IsprojectNull()
											&& templateRow.project.ToLower() == projectRow.name.ToLower()
											&& Directory.Exists(this.GetOutputPath(templateRow.outputDirectory)) 
											&& this.ValidateDependencies(templateRow))
										{

											if(!folders.Contains(this.GetOutputPath(templateRow.outputDirectory)))
											{
												// Add individual files
												owp.OutputString("-- Adding files: " + this.GetOutputPath(templateRow.outputDirectory) + Environment.NewLine);
												this.OnProcessing("Adding content from " + templateRow.outputDirectory);
												vsp.Project.ProjectItems.AddFromDirectory(this.GetOutputPath(templateRow.outputDirectory));
											}
											folders[this.GetOutputPath(templateRow.outputDirectory)] = null;
										}
									}

								}

								i++;
							}
						}

						// Build the solution
						owp.OutputString("Preparing to build...please wait." + Environment.NewLine);
						this.OnProcessing("Building solution");
						dte.Solution.SolutionBuild.Build(true);
						this.OnProcessing("Solution built");
					}

					this.OnProcessing("Saving solution");
					// Save all open documents
					dte.Documents.SaveAll();

					// Save each individual project
					foreach(Project p in dte.Solution.Projects)
					{
						p.Save(p.FileName);
					}

					// Save the solution
					string solutionname = Path.Combine(solutiondir,"daoframework.sln");
					solution.SaveAs(solutionname);
					owp.OutputString("DONE!" + Environment.NewLine);

					// Close.  You have to do this, because otherwise
					// an unmanaged reference to VS.NET stays around, which
					// prevents you from deleting or regenerating files in 
					// the output directory
					dte.Quit();

					// Relaunch so the user can see the final project
					System.Diagnostics.Process.Start(solutionname);
				}
				catch(Exception e)
				{
					log.Error(e.Message, e);
					dte.Quit();
					throw e;
				}
				finally
				{
					MessageFilter.Revoke();
				}
			}
		}

		private OutputWindowPane CreateOutputWindowPane(DTE dte)
		{
			Window outputwindow = dte.Windows.Item(EnvDTE.Constants.vsWindowKindOutput);
			OutputWindow ow = outputwindow.Object as OutputWindow;
			OutputWindowPane owp = ow.OutputWindowPanes.Add("Codus Progress");
			outputwindow.Activate();
			outputwindow.AutoHides = false;

			return owp;
		}

		// Find out if the specified project is referenced by other projects
		private bool ProjectIsReferenced(TemplateProjects.ProjectRow projectRow)
		{
			foreach (TemplateProjects.TemplatesRow templatesRow in this.tpr.GetTemplatesRows())
			{
				foreach (TemplateProjects.TemplateRow templateRow in templatesRow.GetTemplateRows())
				{
					if(!templateRow.IsprojectNull())
						if(this.ValidateDependencies(templateRow) && projectRow.name.ToLower() == templateRow.project.ToLower()) return true;
				}
			}

			return false;
		}

		private string GetSharedPath(string path)
		{
			return path.Replace("${shared}",this.shareddir);
		}

		private string GetOutputPath(string path)
		{
			
			return Path.Combine(this.outputdir, path);
		}

		// Gets the location to the specified VS.NET template type
		private string GetVSTemplate(string type)
		{
			switch(type.ToLower())
			{
				case "csharpconsole":
					return SchemaConfigImpl.VS2003_CSHARPCONSOLE;
				case "csharpdll":
					return SchemaConfigImpl.VS2003_CSHARPDLL;
				case "csharpexe":
					return SchemaConfigImpl.VS2003_CSHARPEXE;
				case "csharpwebservice":
					return SchemaConfigImpl.VS2003_CSHARPWEBSERVICE;
				case "csharpwebsite":
					return SchemaConfigImpl.VS2003_CSHARPWEBSITE;
				case "vbconsole":
					return SchemaConfigImpl.VS2003_VBCONSOLE;
				case "vbdll":
					return SchemaConfigImpl.VS2003_VBDLL;
				case "vbexe":
					return SchemaConfigImpl.VS2003_VBEXE;
				case "vbwebservice":
					return SchemaConfigImpl.VS2003_VBWEBSERVICE;
				case "vbwebsite":
					return SchemaConfigImpl.VS2003_VBWEBSITE;

				default:
					throw new ApplicationException("Output type '" + type + "' is incorrect.");
			}
		}

		#region IGUIConfigurable Members

		public GUIConfigurator GetGUIConfigurator()
		{
			return this.gui;
		}

		private GUIConfigurator LoadGUI()
		{
			if (this.tpr.GetEditorRows()[0] != null && this.tpr.GetEditorRows()[0].assembly.Length > 0)
			{
				Assembly a = ReflectionCache.GetInstance().GetAssembly(this.tpr.GetEditorRows()[0].assembly);
				Type t = a.GetType(this.tpr.GetEditorRows()[0]._class);

				if (t != null)
				{
					return Activator.CreateInstance(t) as GUIConfigurator;
				}
			}
			return null;
		}

		public void AddGeneralItems(VelocityContext context)
		{
			// Add output directory location to NVelocity context
			context.Put("outputdir", this.OutputDirectory);

			// Add Codus directory location to NVelocity context
			context.Put("codusdir", AppDomain.CurrentDomain.BaseDirectory);
		}

		#endregion
	}

	class MessageFilter : IOleMessageFilter
	{
		//
		// Public API

		public static void Register()
		{
			IOleMessageFilter newfilter = new MessageFilter(); 

			IOleMessageFilter oldfilter = null; 
			CoRegisterMessageFilter(newfilter, out oldfilter);
		}

		public static void Revoke()
		{
			IOleMessageFilter oldfilter = null; 
			CoRegisterMessageFilter(null, out oldfilter);
		}

		//
		// IOleMessageFilter impl
    
		int IOleMessageFilter.HandleInComingCall(int dwCallType, System.IntPtr hTaskCaller, int dwTickCount, System.IntPtr lpInterfaceInfo) 
		{
			System.Diagnostics.Debug.WriteLine("IOleMessageFilter::HandleInComingCall");

			return 0; //SERVERCALL_ISHANDLED
		}

		int IOleMessageFilter.RetryRejectedCall(System.IntPtr hTaskCallee, int dwTickCount, int dwRejectType)
		{
			System.Diagnostics.Debug.WriteLine("IOleMessageFilter::RetryRejectedCall");

			if (dwRejectType == 2 ) //SERVERCALL_RETRYLATER
			{
				System.Diagnostics.Debug.WriteLine("Retry call later");
				return 99; //retry immediately if return >=0 & <100
			}
			return -1; //cancel call
		}

		int IOleMessageFilter.MessagePending(System.IntPtr hTaskCallee, int dwTickCount, int dwPendingType)
		{
			System.Diagnostics.Debug.WriteLine("IOleMessageFilter::MessagePending");

			return 2; //PENDINGMSG_WAITDEFPROCESS 
		}

		//
		// Implementation

		[DllImport("Ole32.dll")]
		private static extern int CoRegisterMessageFilter(IOleMessageFilter newfilter, out IOleMessageFilter oldfilter);
	}

	[ComImport(), Guid("00000016-0000-0000-C000-000000000046"),    
	InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
	interface IOleMessageFilter // deliberately renamed to avoid confusion w/ System.Windows.Forms.IMessageFilter
	{
		[PreserveSig]
		int HandleInComingCall( 
			int dwCallType, 
			IntPtr hTaskCaller, 
			int dwTickCount, 
			IntPtr lpInterfaceInfo);

		[PreserveSig]
		int RetryRejectedCall( 
			IntPtr hTaskCallee, 
			int dwTickCount,
			int dwRejectType);

		[PreserveSig]
		int MessagePending( 
			IntPtr hTaskCallee, 
			int dwTickCount,
			int dwPendingType);
	}
}