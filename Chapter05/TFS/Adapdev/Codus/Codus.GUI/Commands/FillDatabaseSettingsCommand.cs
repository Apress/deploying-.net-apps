using System;

namespace Adapdev.Codus.GUI.Commands
{
	using System.Collections;
	using System.IO;
	using System.Windows.Forms;
	using Adapdev.Serialization;
	using Adapdev.Windows.Commands;
	using Adapdev.Windows.Forms;

	/// <summary>
	/// Summary description for FillDatabaseSettingsCommand.
	/// </summary>
	public class FillDatabaseSettingsCommand : AbstractGUICommand
	{
		private readonly string _fileName;
		private readonly Hashtable _databases;
		private readonly TreeView _databasesTreeView;

		public FillDatabaseSettingsCommand(IWin32Window owner, Hashtable databases, TreeView databasesTreeView, string fileName):base(owner)
		{
			this._databasesTreeView = databasesTreeView;
			this._databases = databases;
			this._fileName = fileName;
		}

		public override void Execute()
		{
			Settings s = (Settings) Serializer.DeserializeFromBinary(typeof (Settings), _fileName);
			foreach (DatabaseSetting ds in s.DatabaseSettings)
			{
				TreeNode td = new TreeNode(ds.ConnectionName);
				td.Tag = ds;
				this._databasesTreeView.Nodes.Add(td);
				this._databases.Add(ds.ConnectionName, td);
			}
		}
	}
}
