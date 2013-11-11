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
	public class ENFood : AEN
	{
		#region//Atributes

		private string name;
		private ENType type;
        private int quantity;
        

		#endregion

		#region//Private Methods

		private ENFood()
			: base()
		{
			cad = new CADFood("GebatDataConnectionString");
			name = "";
		}

        /// <summary>
        /// Carga la comida entrante del alimento.
        /// </summary>
        /// <returns>Cantidad entrante.</returns>
        private int LoadEntrada()
        {
            int entry = 0;
            VIEWEntrada entrada = new VIEWEntrada("GebatDataConnectionString");
            List<object> param = new List<object>();
            param.Add(this.id[0]);
            DataRow row = entrada.Select(param);
            if (row != null)
            {
                entry = Convert.ToInt32(row["Quantity"]);
            }
            return entry;
        }

        private int LoadSalida()
        {
            int salida = 0;
            VIEWSalida vsalida = new VIEWSalida("GebatDataConnectionString");
            List<object> param = new List<object>();
            param.Add(this.id[0]);
            DataRow row = vsalida.Select(param);
            if (row != null)
            {
                salida = Convert.ToInt32(row["Quantity"]);
            }
            return salida;
        }

        /// <summary>
        /// Carga la cantidad de comida que hay en la base de datos.
        /// </summary>
        private void LoadQuantity()
        {
            this.quantity = LoadEntrada() - LoadSalida();
        }

		#endregion

		#region//Protected Methods

        /// <summary>
        /// Obtiene el objeto actual en tipo DataRow de forma que corresponde en la base de datos.
        /// </summary>
		protected override DataRow ToRow
		{
			get 
			{
				DataRow ret = cad.GetVoidRow;
				if (this.id != null)
				{
					ret ["Id"] = (int)this.id [0];
				}
				ret["Name"] = this.name;
				
				if (type != null)
				{
					ret ["QuantityType"] = (int)type.Id[0];
				}
				return ret;
			}
		}

        /// <summary>
        /// Asigna al objeto actual los datos contenientes en el DataRow.
        /// </summary>
        /// <param name="row">Fila con los datos.</param>
		protected override void FromRow(DataRow row)
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
					type = (ENType)new ENType ("").Read (ids);
				}
				this.saved = true;
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
		public ENType MyType
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
		public ENFood(string name, ENType type = null)
			: base()
		{
			if (name == null)
			{
				throw new NullReferenceException("The name cannot be null");
			}
			cad = new CADFood("GebatDataConnectionString");
			this.name = name;
            this.quantity = 0;
			this.type = type;
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
				ret.FromRow(row);
                ret.LoadQuantity();
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
                nueva.LoadQuantity();
				ret.Add((ENFood)nueva);
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
            ENFoodIN fin = new ENFoodIN(date, quantity, (int)this.id[0]);
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
            ENFoodOut fout = new ENFoodOut(date, quantity, (int)this.id[0]);
            fout.Save();
            this.quantity -= quantity;
        }

		#endregion
	}
}


