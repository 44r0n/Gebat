//
//  ENFood.cs
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using GebatCAD.Classes;

namespace GebatEN.Classes
{
	public class ENFood : AEN
	{
		#region//Atributes

		private string name;
		private int quantity;

		#endregion

		#region//Private Methods

		private ENFood()
			: base()
		{
			cad = new CADFood();
			name = "";
			quantity = 0;
		}

		#endregion

		#region//Protected Methods

		protected override DataRow ToRow
		{
			get 
			{
				DataRow ret = cad.GetVoidRow;
				ret["Id"] = this.id[0];
				ret["Name"] = this.name;
				ret["Quantity"] = this.quantity;
				return ret;
			}
		}

		protected override void FromRow(DataRow row)
		{
			if (row != null)
			{
				this.id[0] = (int)row["Id"];
				this.name = (string)row["Name"];
				this.quantity = (int)row["Quantity"];
			}
			else
			{
				throw new NullReferenceException("Cannot convert from row, the row is null");
			}
		}

		#endregion

		#region//Geters & Setters

		/// <summary>
		/// Obtiene el nombre del alimento.
		/// </summary>
		public string Name
		{
			get
			{
				return name;
			}
		}

		/// <summary>
		/// Obtiene y establece la cantidad del alimento.
		/// </summary>
		public int Quantity
		{
			get
			{
				return quantity;
			}
			set
			{
				if (value >= 0)
				{
					quantity = value;
				}
			}
		}

		#endregion

		#region//Public Methods

		/// <summary>
		/// Constructor que inicializa el objeto.
		/// </summary>
		/// <param name="name">Nombre del alimento.</param>
		/// <param name="quantity">Cantidad del alimento.</param>
		public ENFood(string name, int quantity = 0)
			: base()
		{
			if (name == null)
			{
				throw new NullReferenceException("The name cannot be null");
			}
			cad = new CADFood();
			this.name = name;
			this.quantity = quantity;
		}

		/// <summary>
		/// Busca en la base de datos el alimento por el id.
		/// </summary>
		/// <param name="id">Identificador por el que se buscará el alimento.</param>
		/// <returns>Alimento en formato AEN.</returns>
		public override AEN Read(List<int> id)
		{
			ENFood ret = new ENFood();
			List<object> param = new List<object>();
			param.Add((object)id[0]);
			DataRow row = cad.Select(param);
			if (row != null)
			{
				ret.FromRow(cad.Select(param));
			}
			else
			{
				ret = null;
			}
			return ret;
		}

		/// <summary>
		/// Obtiene todos los alimentos de la base de datos.
		/// </summary>
		/// <returns>Lista de alimentos en formato AEN.</returns>
		public override List<AEN> ReadAll()
		{
			List<AEN> ret = new List<AEN>();
			DataTable tabla = cad.SelectAll();
			foreach (DataRow rows in tabla.Rows)
			{
				ENFood nueva = new ENFood();
				nueva.FromRow(rows);
				ret.Add((ENFood)nueva);
			}
			return ret;
		}

		#endregion
	}
}


