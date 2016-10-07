using System;

namespace Adapdev.Codus.GUI
{
	/// <summary>
	/// Summary description for Constants.
	/// </summary>
	public class Constants
	{
		public static readonly string SettingsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
		public static readonly string SettingsFile = System.IO.Path.Combine(SettingsPath,"codus.settings");
		public static readonly string LocalSettingsFile = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"codus.settings");
	}
}
