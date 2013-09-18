//
//  FoodManager.cs
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
using GebatEN.Classes;
using System.Collections.Generic;

namespace Gebat
{
	public partial class FoodManager : Gtk.Window
	{
		ListStore quantityStore;
		TreeViewColumn nameFood;
		TreeViewColumn quantityFood;
		TreeViewColumn quantityType;
		CellRendererText nameCell;
		CellRendererText quantityCell;
		CellRendererText typeCell;
		ListStore foodStore;
		CellRendererText quantityNameCell;
		ENType seleccionado = null;
		ENFood foodselected = null;
		TreeModelFilter filter;

		public FoodManager () : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build ();
			initCombo ();
			setTreeColumns ();
		}

		private void initCombo()
		{
			quantityStore = new ListStore(typeof(ENType));
			quantityNameCell = new CellRendererText();
			comboboxType.PackStart(quantityNameCell,true);
			comboboxType.SetCellDataFunc(quantityNameCell, new CellLayoutDataFunc(RenderTypeName));
			comboboxType.AddAttribute (quantityNameCell, "text", 0);
			fillCombo ();
		}

		private void setTreeColumns()
		{
			nameFood = new TreeViewColumn();
			nameFood.Title = "Nombre";
			nameCell = new CellRendererText ();
			nameFood.PackStart (nameCell, true);

			quantityFood = new TreeViewColumn ();;
			quantityFood.Title = "Cantidad";
			quantityCell = new CellRendererText ();
			quantityFood.PackStart (quantityCell, true);

			quantityType = new TreeViewColumn ();
			quantityType.Title = "Tipo";
			typeCell = new CellRendererText ();
			quantityType.PackStart (typeCell, true);

			foodStore = new ListStore (typeof(ENFood));

			treeviewFood.AppendColumn (nameFood);
			treeviewFood.AppendColumn (quantityFood);
			treeviewFood.AppendColumn (quantityType);

			nameFood.SetCellDataFunc (nameCell, new TreeCellDataFunc (RenderNameFood));
			quantityFood.SetCellDataFunc (quantityCell, new TreeCellDataFunc (RenderQuantityFood));
			quantityType.SetCellDataFunc (typeCell, new TreeCellDataFunc (RenderTypeFood));

			treeviewFood.EnableTreeLines = true;

			filter = new TreeModelFilter (foodStore, null);
			filter.VisibleFunc = new TreeModelFilterVisibleFunc (FilterTree);

			fillTree ();
		}

		private bool FilterTree (TreeModel model, TreeIter iter)
		{
			ENFood tipo = model.GetValue (iter, 0) as ENFood;

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

		private void fillTree()
		{
			foodStore.Clear ();
			List<AEN> comidas = new ENFood ("").ReadAll ();
			foreach (AEN comida in comidas)
			{
				ENFood unacomida = (ENFood)comida;
				foodStore.AppendValues (unacomida);
			}
			treeviewFood.Model = foodStore;
		}

		private void RenderNameFood(TreeViewColumn col, CellRenderer cell, TreeModel model, TreeIter iter)
		{
			ENFood food = (ENFood)model.GetValue (iter, 0);
			(cell as CellRendererText).Text = food.Name;
		}

		private void RenderQuantityFood(TreeViewColumn col, CellRenderer cell, TreeModel model, TreeIter iter)
		{
			ENFood food = (ENFood)model.GetValue (iter, 0);
			(cell as CellRendererText).Text = food.Quantity.ToString();
		}

		private void RenderTypeFood(TreeViewColumn col, CellRenderer cell, TreeModel model, TreeIter iter)
		{
			ENFood food = (ENFood)model.GetValue (iter, 0);
			if (food.MyType == null)
			{
				(cell as CellRendererText).Text = "";
			}
			else
			{
				(cell as CellRendererText).Text = food.MyType.Name;
			}
		}

		private void fillCombo()
		{
			quantityStore.Clear ();

			List<AEN> tipos = new ENType ("").ReadAll ();
			foreach (AEN t in tipos)
			{
				ENType ins = (ENType)t;
				quantityStore.AppendValues (ins);
			}
			comboboxType.Model = quantityStore;
		}

		private void RenderTypeName(CellLayout layout, CellRenderer cell, TreeModel model, TreeIter iter)
		{
			ENType type = (ENType)model.GetValue (iter, 0);
			(cell as CellRendererText).Text = type.Name;
		}

		protected void insertFood (object sender, EventArgs e)
		{
			ENFood newfood = new ENFood (entryName.Text, (int)spinbuttonQuantity.Value, seleccionado);
			newfood.Save ();
			entryName.Text = "";
			spinbuttonQuantity.Value = 0;
			fillTree ();
		}

		protected void quantityChanged (object sender, EventArgs e)
		{
			ComboBox combo = sender as ComboBox;
			TreeIter iter;
			if(combo.GetActiveIter(out iter))
			{
				seleccionado = combo.Model.GetValue (iter, 0) as ENType;
			}
		}

		protected void treefoodchanged (object sender, EventArgs e)
		{
			TreeSelection selection = (sender as TreeView).Selection;
			TreeModel model;
			TreeIter iter;
			if (selection.GetSelected (out model, out iter))
			{
				var value = model.GetValue (iter, 0);
				foodselected = value as ENFood;
				buttonDelete.Sensitive = true;
			}
		}

		protected void deletefood (object sender, EventArgs e)
		{
			foodselected.Delete ();
			foodselected = null;
			buttonDelete.Sensitive = false;
			entrySearch.Text = "";
			fillTree ();
		}

		protected void searchFood (object sender, EventArgs e)
		{
			treeviewFood.Model = filter;
			filter.Refilter();
		}
	}
}

