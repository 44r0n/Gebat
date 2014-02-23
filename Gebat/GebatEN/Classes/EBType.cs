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
	public class EBType : AEB
    {
        #region//Atributes

        private string name;

        #endregion

        #region//Private Methods

        private EBType ()
			:base()
		{
			adl = new ADLType (defaultConnString);
			name = "";
		}

        #endregion

        #region//Internal Methods

        /// <summary>
        /// Obtiene el objeto actual en tipo DataRow de forma que corresponde en la base de datos.
        /// </summary>
        internal override DataRow ToRow
		{
			get
			{
				DataRow ret = adl.GetVoidRow;
				if (this.id != null)
				{
					ret ["Id"] = this.id [0];
				}
				ret ["Name"] = this.name;
				return ret;
			}
		}

        /// <summary>
        /// Asigna al objeto actual los datos contenientes en el DataRow.
        /// </summary>
        /// <param name="row">Fila con los datos.</param>
        internal override void FromRow(DataRow row)
		{
			if (row != null)
			{
				this.id = new List<object> ();
				this.id.Add((int)row["Id"]);
				this.name = (string)row["Name"];
				this.saved = true;
			}
			else
			{
				throw new NullReferenceException("Cannot convert from row, the row is null");
			}
		}

        #endregion

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
		public EBType(string name)
		{
			if (name == null)
			{
				throw new NullReferenceException("The name cannot be null");
			}
			adl = new ADLType (defaultConnString);
			this.name = name;
		}

        /// <summary>
        /// Busca en la base de datos el tipo de alimento por el id.
        /// </summary>
        /// <param name="id">Identificador por el que se buscará el tipo de alimento.</param>
        /// <returns>Tipo de alimento en formatoAEN</returns>
		public override AEB Read (List<int> id)
		{
			EBType ret = new EBType();
			List<object> param = new List<object>();
			param.Add((object)id[0]);
			DataRow row = adl.Select(param);
			if (row != null)
			{
				ret.FromRow(adl.Select(param));
			}
			else
			{
				ret = null;
			}
			return ret;
		}

        /// <summary>
        /// Obtiene todos los tipos de alimentos de la base de datos.
        /// </summary>
        /// <returns>Listo de los tipos de alimentos en formato AEN.</returns>
		public override List<AEB> ReadAll()
		{
			List<AEB> ret = new List<AEB>();
			DataTable table = adl.SelectAll();
			foreach (DataRow rows in table.Rows)
			{
				EBType newtype = new EBType();
				newtype.FromRow(rows);
				ret.Add((EBType)newtype);
			}
			return ret;
		}
	}
}
