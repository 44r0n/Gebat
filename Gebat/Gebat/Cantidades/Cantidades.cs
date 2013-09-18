//
//  Cantidades.cs
//
//  Author:
//       Aarón Sánchez Navarro <aaron.sn.1988@gmail.com>
//
//  Copyright (c) 2013 GNU
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using Gtk;
using System.Collections.Generic;
using System.Collections;
using GebatEN.Classes;

namespace Gebat
{
	public partial class Cantidades : Gtk.Window
	{
		ListStore quantityStore;
		TreeViewColumn quantity;
		CellRendererText quantityNameCell;
		ENType typeSelected = null;
		TreeModelFilter filter;

		public Cantidades () : base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
			SetTreeColumns ();
			FillTree ();
		}

		private void SetTreeColumns()
		{
			quantity = new TreeViewColumn ();
			quantity.Title = "Nombre";
			quantityNameCell = new CellRendererText ();
			quantity.PackStart (quantityNameCell, true);

			quantityStore = new ListStore (typeof(ENType));

			treeviewquantities.AppendColumn (quantity);

			quantity.SetCellDataFunc (quantityNameCell, new TreeCellDataFunc (RenderTypeName));

			filter = new TreeModelFilter (quantityStore, null);
			filter.VisibleFunc = new TreeModelFilterVisibleFunc (FilterTree);
		}

		private void FillTree()
		{
			quantityStore.Clear ();
			List<AEN> tipos = new ENType ("").ReadAll ();
			foreach (AEN tipo in tipos)
			{
				ENType untipo = (ENType)tipo;
				quantityStore.AppendValues (untipo);
			}
			treeviewquantities.Model = quantityStore;
		}

		protected void InsQuant (object sender, EventArgs e)
		{
			ENType tipo = new ENType (entryType.Text);
			tipo.Save ();
			FillTree ();
			entryType.Text = "";
			entryType.IsFocus = true;
		}

		private void RenderTypeName(TreeViewColumn col, CellRenderer cell, TreeModel model, TreeIter iter)
		{
			ENType type = (ENType)model.GetValue (iter, 0);
			(cell as CellRendererText).Text = type.Name;
		}

		protected void treeTypeSelected (object sender, EventArgs e)
		{
			TreeSelection selection = (sender as TreeView).Selection;
			TreeModel model;
			TreeIter iter;
			if (selection.GetSelected (out model, out iter))
			{
				var value = model.GetValue (iter, 0);
				typeSelected = (ENType)value;
				buttonDelete.Sensitive = true;
			}
		}

		protected void deleteType (object sender, EventArgs e)
		{
			typeSelected.Delete ();
			typeSelected = null;
			buttonDelete.Sensitive = false;
			entrySearch.Text = "";
			FillTree ();
		}

		private bool FilterTree (TreeModel model, TreeIter iter)
		{
			ENType tipo = (ENType)model.GetValue (iter, 0);

			if (entrySearch.Text == "")
			{
				return true;
			}

			if (tipo.Name.IndexOf (entrySearch.Text) > -1)
			{
				return true;
			} 
			else
			{
				return false;
			}
		}

		protected void searchType (object sender, EventArgs e)
		{
			treeviewquantities.Model = filter;
			filter.Refilter ();
			//ShowTree ();
		}
	}
}

