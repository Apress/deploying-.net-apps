using System;
using Adapdev.Commands;
using Adapdev.Codus.GUI;

namespace Adapdev.Codus.GUI.Commands
{
	using System.IO;
	using System.Windows.Forms;
	using Adapdev.Serialization;
	using Adapdev.Windows.Forms;

	/// <summary>
	/// Summary description for SaveSettingsCommand.
	/// </summary>
	public class SaveSettingsCommand : ICommand
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		private TreeView _databases;

		public SaveSettingsCommand(TreeView databases)
		{
			this._databases = databases;
		}
		#region ICommand Members

		public void Execute()
		{
			try
			{
				Settings s = new Settings();
				foreach (TreeNode t in this._databases.Nodes)
				{
					s.AddDatabaseSetting(t.Tag as DatabaseSetting);
				}

				Serializer.SerializeToBinary(s, Constants.SettingsFile);
			}
			catch (Exception e)
			{
				log.Error(e.Message, e);
				MessageBox.Show(e.Message + ": " + e.StackTrace);
			}

		}

		#endregion
	}
}
