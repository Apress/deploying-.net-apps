namespace Codus.GUI.Extensions
{
	using System;
	using System.Collections;
	using System.ComponentModel;
	using System.Windows.Forms;
	using Adapdev.CodeGen;

	/// <summary>
	/// Summary description for GUIConfigurator.
	/// </summary>
	public class GUIConfigurator : UserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private Container components = null;

		private AbstractSchemaConfig config = null;

		public GUIConfigurator()
		{
			// This call is required by the Windows.Forms Form Designer.
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

		public virtual Hashtable GetCustomProperties()
		{
			return new Hashtable();
		}

		public virtual void SetAbstractSchemaConfig(AbstractSchemaConfig config)
		{
			this.config = config;
		}

		public virtual void PreRunCustomCode(AbstractConfig config)
		{
			
		}

		public virtual void PostRunCustomCode(AbstractConfig config)
		{
			
		}

		public virtual string PostMessage
		{
			get{return String.Empty;}
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// GUIConfigurator
			// 
			this.Name = "GUIConfigurator";
			this.Size = new System.Drawing.Size(376, 320);

		}

		#endregion
	}
}