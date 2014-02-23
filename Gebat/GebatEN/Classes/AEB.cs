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
using GebatCAD.Classes;
using System.Data;

namespace GebatEN.Classes
{
	public abstract class AEB
	{
		protected bool saved;
		protected List<object> id = null;
		protected AADL adl;

        private void nullADL()
        {
            throw new NullReferenceException("The adl cannot be null");
        }

		#region//Protected Methods

        /// <summary>
        /// Obtiene el nombre de cadena de conexión por defecto.
        /// </summary>
        protected string defaultConnString
        {
            get
            {
                return ("GebatDataConnectionString");
            }
        }

        #endregion  

        #region//Internal Methods

        /// <summary>
		/// Obtiene el objeto actual en tipo DataRow de forma que corresponde en la base de datos.
		/// </summary>
		internal abstract DataRow ToRow
		{
			get;
		}

		/// <summary>
		/// Asigna al objeto actual los datos contenientes en el DataRow.
		/// </summary>
		/// <param name="row">Fila con los datos.</param>
		internal abstract void FromRow(DataRow row);

		#endregion

		#region//Public Methods

		/// <summary>
		/// Obtiene el identificador del objeto en la base de datos. Null si no está en la base de datos.
		/// </summary>
		public List<object> Id
		{
			get
			{
				return id;
			}
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		public AEB()
		{
			adl = null;
			saved = false;
		}

		/// <summary>
		/// Guarda el objeto actual en la base de datos. Si es nuevo lo inserta, si ha sido modificado lo modifica en la base de datos.
		/// </summary>
		public virtual void Save()
		{
			if (adl == null)
			{
                nullADL();
			}
			if (!saved)
			{
				this.FromRow(adl.Insert(ToRow));
				this.saved = true;
			}
			else
			{
				adl.Update(ToRow);
			}
		}

		/// <summary>
		/// Elimina el objeto actual en la base de datos.
		/// </summary>
		public virtual void Delete()
		{
			if (adl == null)
			{
                nullADL();
			}

			if (saved)
			{
				adl.Delete(ToRow);
				saved = false;
			}
		}

		/// <summary>
		/// Lee de la base de datos y filtra por el identificador que se le pasa por parámetro.
		/// </summary>
		/// <param name="id">Identificador a buscar.</param>
		/// <returns>El objeto de tipo ENGeneral.</returns>
		public abstract AEB Read(List<int> id);

		/// <summary>
		/// Devuelve todos los registros de la base de datos.
		/// </summary>
		/// <returns></returns>
		public abstract List<AEB> ReadAll();

		#endregion
	}
}
