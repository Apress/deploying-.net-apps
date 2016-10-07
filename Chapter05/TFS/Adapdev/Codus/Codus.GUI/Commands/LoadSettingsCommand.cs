using System;

namespace Adapdev.Codus.GUI.Commands
{
	using System.Collections;
	using System.IO;
	using System.Windows.Forms;
	using Adapdev.Commands;
	using Adapdev.Serialization;
	using Adapdev.Windows.Commands;
	using Adapdev.Windows.Forms;

	/// <summary>
	/// Summary description for FillDatabaseSettingsCommand.
	/// </summary>
	public class LoadSettingsCommand : ICommand
	{
		private readonly Hashtable _databases = new Hashtable();
		private readonly TreeView _databasesTreeView;

		public LoadSettingsCommand(TreeView databasesTreeView)
		{
			this._databasesTreeView = databasesTreeView;
		}

		public void Execute()
		{
			if(File.Exists(Constants.SettingsFile))
			{
				try
				{
					Settings s = (Settings) Serializer.DeserializeFromBinary(typeof (Settings), Constants.SettingsFile);
					foreach (DatabaseSetting ds in s.DatabaseSettings)
					{
						try
						{
							TreeNode td = new TreeNode(ds.ConnectionName);
							td.Tag = ds;
							this._databasesTreeView.Nodes.Add(td);
							this._databases.Add(ds.ConnectionName, td);
						}
						catch (Exception){}
					}
				}
				catch (Exception)
				{
					File.Delete(Constants.SettingsFile);
				}
			}
		}

		public Hashtable Databases
		{
			get { return _databases; }
		}
	}
}
