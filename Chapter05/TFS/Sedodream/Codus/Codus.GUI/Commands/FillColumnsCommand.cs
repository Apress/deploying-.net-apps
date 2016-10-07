using System;
using Adapdev.Windows.Commands;

namespace Adapdev.Codus.GUI.Commands
{
	using System.Collections;
	using System.Drawing;
	using System.Windows.Forms;
	using Adapdev.Data.Schema;

	/// <summary>
	/// Summary description for FillColumnsCommand.
	/// </summary>
	public class FillColumnsCommand : AbstractGUICommand
	{
		private readonly ListView _columnsListView;
		private readonly SortedList _columns;

		public FillColumnsCommand(IWin32Window owner, SortedList columns, ListView columnsListView):base(owner)
		{
			this._columns = columns;
			this._columnsListView = columnsListView;
		}

		public override void Execute()
		{
			this._columnsListView.Items.Clear();

			foreach (ColumnSchema c in _columns.Values)
			{
				ListViewItem li1 = new ListViewItem(new string[] {c.Name, c.Alias, this.ConvertBool(c.IsPrimaryKey), this.ConvertBool(c.IsForeignKey), this.ConvertBool(c.AllowNulls), this.ConvertBool(c.IsAutoIncrement), c.DataType, c.Length.ToString()});
				li1.Tag = c;
				li1.Checked = c.IsActive;

				if (!c.IsActive) li1.ForeColor = Color.LightGray;

				this._columnsListView.Items.Add(li1);
			}
		}

		private string ConvertBool(bool b)
		{
			if (b) return "Y";
			else return "N";
		}
	}
}
