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
using System.Data;
using GebatCAD.Classes;

namespace GebatEN.Classes
{
	public class EBFood : AEB
	{
		#region//Atributes

		private string name;
		private EBType type;
        private int quantity;
        

		#endregion

		#region//Private Methods

		private EBFood()
			: base()
		{
			adl = new ADLFood(defaultConnString);
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
					ret ["Id"] = (int)this.id [0];
				}
				ret["Name"] = this.name;
				
				if (type != null)
				{
					ret ["QuantityType"] = (int)type.Id[0];
				}
                ret["Quantity"] = quantity;
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
				if (row ["QuantityType"] != DBNull.Value)
				{
					List<int> ids = new List<int> ();
					ids.Add ((int)row ["QuantityType"]);
					type = (EBType)new EBType ("").Read (ids);
				}
                this.quantity = (int)row["Quantity"];
				this.saved = true;
			}
			else
			{
				throw new NullReferenceException("Cannot convert from row, the row is null");
			}
		}

		#endregion

		#region//Getters & Setters

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
        /// Obtiene la cantidad de alimento.
        /// </summary>
        public int Quantity
        {
            get
            {
                return quantity;
            }
        }

        /// <summary>
        /// Obtiene y establece el tipo de cantidad del alimento.
        /// </summary>
		public EBType MyType
		{
			get
			{
				return type;
			}
			set
			{
				type = value;
			}
		}

		#endregion

		#region//Public Methods

		/// <summary>
		/// Constructor que inicializa el objeto.
		/// </summary>
		/// <param name="name">Nombre del alimento.</param>
		/// <param name="quantity">Cantidad del alimento.</param>
		public EBFood(string name, EBType type = null)
			: base()
		{
			if (name == null)
			{
				throw new NullReferenceException("The name cannot be null");
			}
			adl = new ADLFood(defaultConnString);
			this.name = name;
            this.quantity = 0;
			this.type = type;
		}

		/// <summary>
		/// Busca en la base de datos el alimento por el id.
		/// </summary>
		/// <param name="id">Identificador por el que se buscará el alimento.</param>
		/// <returns>Alimento en formato AEN.</returns>
		public override AEB Read(List<int> id)
		{
			EBFood ret = new EBFood();
			List<object> param = new List<object>();
			param.Add((object)id[0]);
			DataRow row = adl.Select(param);
			if (row != null)
			{
				ret.FromRow(row);
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
		public override List<AEB> ReadAll()
		{
			List<AEB> ret = new List<AEB>();
			DataTable table = adl.SelectAll();
			foreach (DataRow rows in table.Rows)
			{
				EBFood newfood = new EBFood();
				newfood.FromRow(rows);
				ret.Add((EBFood)newfood);
			}
			return ret;
		}

        /// <summary>
        /// Añade una entrada de comida en la base de datos.
        /// </summary>
        /// <param name="quantity">Cantidad de comida a insertar.</param>
        /// <param name="date">Fecha en la que se introduce.</param>
        public void Add(int quantity, DateTime date)
        {
            EBFoodIN fin = new EBFoodIN(date, quantity, (int)this.id[0]);
            fin.Save();
            this.quantity += quantity;
        }

        /// <summary>
        /// Añade una salida de comida en la base de datos.
        /// </summary>
        /// <param name="quantity">Cantidad de comida a salir.</param>
        /// <param name="date">Fecha en la que sale.</param>
        public void Remove(int quantity, DateTime date)
        {
            EBFoodOut fout = new EBFoodOut(date, quantity, (int)this.id[0]);
            fout.Save();
            this.quantity -= quantity;
        }

		#endregion
	}
}


