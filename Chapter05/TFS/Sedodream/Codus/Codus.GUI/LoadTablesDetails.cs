using System;
using Adapdev.Data;
using Adapdev.Windows.Forms;
using Adapdev.Windows.Forms.Progress;

namespace Adapdev.Codus.GUI
{
	/// <summary>
	/// Summary description for LoadTablesDetails.
	/// </summary>
	public class LoadTablesDetails
	{
		DatabaseSetting		_databaseSetting;
		IProgressCallback	_progressCallBack;
		
		public LoadTablesDetails(DatabaseSetting databaseSetting, IProgressCallback progressCallBack) { 
			_databaseSetting	= databaseSetting;
			_progressCallBack	= progressCallBack;
		}

		public DatabaseSetting DatabaseSetting {
			get { return _databaseSetting; }
			set { _databaseSetting = value; }
		}

		public Adapdev.IProgressCallback ProgressCallBack
		{
			get {return _progressCallBack;}
			set {_progressCallBack = value; }
		}
	}
}
