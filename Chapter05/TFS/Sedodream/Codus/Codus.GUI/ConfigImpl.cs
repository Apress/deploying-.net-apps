namespace Adapdev.Codus.GUI
{
	using System.Collections;
	using Adapdev.CodeGen;

	/// <summary>
	/// Summary description for ConfigImpl.
	/// </summary>
	internal class ConfigImpl : AbstractConfig
	{
		private TemplateProjects.TemplateProjectRow tpr = null;

		public ConfigImpl(string nameSpace, TemplateProjects.TemplateProjectRow tr) : base(nameSpace)
		{
			this.tpr = tpr;
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

			foreach (TemplateProjects.TemplatesRow tsr in this.tpr.GetTemplatesRows())
			{
				foreach (TemplateProjects.TemplateRow tr in tsr.GetTemplateRows())
				{
					NVelocityCodeTemplate n = new NVelocityCodeTemplate(tr.outputFileName, tr.outputFileExtension, tr.templateFile, tr.outputDirectory);
					templates.Add(n);
				}
			}
			return templates;
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
	}
}