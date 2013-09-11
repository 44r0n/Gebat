//
//  ENType.cs
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
using GebatCAD.Classes;
using System.Data;
using System.Collections.Generic;

namespace GebatEN.Classes
{
	public class ENType : AEN
	{
		private string name;

		private ENType ()
			:base()
		{
			cad = new CADType ();
			name = "";
		}

		protected override DataRow ToRow
		{
			get
			{
				DataRow ret = cad.GetVoidRow;
				ret ["Id"] = this.id [0];
				ret ["Name"] = this.name;
				return ret;
			}
		}

		protected override void FromRow (DataRow row)
		{
			if (row != null)
			{
				this.id[0] = (int)row["Id"];
				this.name = (string)row["Name"];
			}
			else
			{
				throw new NullReferenceException("Cannot convert from row, the row is null");
			}
		}

		/// <summary>
		/// Obtiene y establece el nombre del tipo.
		/// </summary>
		/// <value>Nombre del tipo.</value>
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				name = value;
			}
		}

		/// <summary>
		/// Inicializa el tipo con el nombre.
		/// </summary>
		/// <param name="name">Nombre del tipo.</param>
		public ENType(string name)
		{
			if (name == null)
			{
				throw new NullReferenceException("The name cannot be null");
			}
			cad = new CADType ();
			this.name = name;
		}

		public override AEN Read (System.Collections.Generic.List<int> id)
		{
			ENType ret = new ENType();
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

		public override List<AEN> ReadAll()
		{
			List<AEN> ret = new List<AEN>();
			DataTable tabla = cad.SelectAll();
			foreach (DataRow rows in tabla.Rows)
			{
				ENType nueva = new ENType();
				nueva.FromRow(rows);
				ret.Add((ENType)nueva);
			}
			return ret;
		}
	}
}

