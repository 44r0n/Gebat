//
//  AEN.cs
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
//using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GebatCAD.Classes;
using System.Data;

namespace GebatEN.Classes
{
	public abstract class AEN
	{
		protected List<int> id;

		protected ACAD cad;

		#region//Protected Methods

		/// <summary>
		/// Obtiene el objeto actual en tipo DataRow de forma que corresponde en la base de datos.
		/// </summary>
		protected abstract DataRow ToRow
		{
			get;
		}

		/// <summary>
		/// Asigna al objeto actual los datos contenientes en el DataRow.
		/// </summary>
		/// <param name="row">Fila con los datos.</param>
		protected abstract void FromRow(DataRow row);

		#endregion

		#region//Public Methods

		/// <summary>
		/// Obtiene el identificador del objeto en la base de datos.
		/// </summary>
		public List<int> Id
		{
			get
			{
				return id;
			}
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		public AEN()
		{
			cad = null;
			id = new List<int>();
			id.Add(0);
		}

		/// <summary>
		/// Guarda el objeto actual en la base de datos. Si es nuevo lo inserta, si ha sido modificado lo modifica en la base de datos.
		/// </summary>
		public virtual void Save()
		{
			if (cad == null)
			{
				throw new NullReferenceException("The cad cannot be null");
			}
			if (id[0] == 0)
			{
				this.id[0] = cad.Insert(ToRow);
			}
			else
			{
				cad.Update(ToRow);
			}
		}

		/// <summary>
		/// Elimina el objeto actual en la base de datos.
		/// </summary>
		public virtual void Delete()
		{
			if (cad == null)
			{
				throw new NullReferenceException("Cannot delete, the cad object is null");
			}

			if (id[0] != 0)
			{
				cad.Delete(ToRow);
			}
		}

		/// <summary>
		/// Lee de la base de datos y filtra por el identificador que se le pasa por parámetro.
		/// </summary>
		/// <param name="id">Identificador a buscar.</param>
		/// <returns>El objeto de tipo ENGeneral.</returns>
		public abstract AEN Read(List<int> id);

		/// <summary>
		/// Devuelve todos los registros de la base de datos.
		/// </summary>
		/// <returns></returns>
		public abstract List<AEN> ReadAll();

		#endregion
	}
}

