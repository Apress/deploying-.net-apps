using System;
using Adapdev.Windows.Forms;

namespace Adapdev.Codus.GUI
{
	/// <summary>
	/// Summary description for ProjectSetting.
	/// </summary>
	/// 
	[Serializable]
	public class ProjectSetting : DatabaseSetting
	{
		public ProjectSetting(DatabaseSetting ds)
		{
			this.ConnectionName = ds.ConnectionName;
			this.ConnectionType = ds.ConnectionType;
			this.DatabaseLocation = ds.DatabaseLocation;
			this.DatabaseName = ds.DatabaseName;
			this.Password = ds.Password;
			this.UserName = ds.UserName;
			this.Filter = ds.Filter;
			this.DbProviderType = ds.DbProviderType;
			this.DbType = ds.DbType;
			this.ConnectionRef = ds.ConnectionRef;
			this.ProviderRef = ds.ProviderRef;
			this.ConnectionString = ds.ConnectionString;
			this.OleDbConnectionString = ds.OleDbConnectionString;
		}

		public string Namespace;
		public string @OutputLocation;
	}
}
