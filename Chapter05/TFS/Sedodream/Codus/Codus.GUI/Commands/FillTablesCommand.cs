using System;
using Adapdev.Windows.Commands;

namespace Adapdev.Codus.GUI.Commands
{
	using System.Windows.Forms;
	using Adapdev.Data.Schema;

	/// <summary>
	/// Summary description for FillTablesCommand.
	/// </summary>
	public class FillTablesCommand : AbstractGUICommand
	{
		private readonly TreeView _tablesTreeView;
		private readonly DatabaseSchema _databaseSchema;

		public FillTablesCommand(IWin32Window owner, DatabaseSchema databaseSchema, TreeView tablesTreeView):base(owner)
		{
			this._databaseSchema = databaseSchema;
			this._tablesTreeView = tablesTreeView;
		}

		public override void Execute()
		{
			// Clear the current contents
			this._tablesTreeView.Nodes.Clear();

			if (_databaseSchema != null)
			{
				// Add the Tables node
				TreeNode tt = new TreeNode("Tables",2,2);
				tt.Checked = true;

				// Add the Views node
				TreeNode tv = new TreeNode("Views",3,3);
				tv.Checked = true;

				this._tablesTreeView.BeginUpdate();

				foreach (TableSchema table in _databaseSchema.SortedTables.Values)
				{
					if (table.TableType == TableType.TABLE)
					{
						TreeNode tn = new TreeNode(table.Name,1,1);
						tn.Tag = table;
						tn.Checked = true;
						tt.Nodes.Add(tn);
					}
					else if (table.TableType == TableType.VIEW)
					{
						TreeNode tn = new TreeNode(table.Name,1,1);
						tn.Tag = table;
						tn.Checked = true;
						tv.Nodes.Add(tn);
					}
				}
				this._tablesTreeView.Nodes.AddRange(new TreeNode[] {tt, tv});
				this._tablesTreeView.EndUpdate();
				this._tablesTreeView.CollapseAll();
				tt.Expand();
			}

		}
	}
}
