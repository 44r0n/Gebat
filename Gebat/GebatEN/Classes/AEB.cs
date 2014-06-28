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
using Cripto.Util;

namespace GebatEN.Classes
{
	public abstract class AEB
	{
        private static Cipher cipher = null;
		protected bool saved;
		protected List<object> id = null;
		protected ADL adl;

        #region//Private Methods

        private void nullADL()
        {
            throw new NullReferenceException("The adl cannot be null");
        }

        #endregion

        #region//Protected Methods

        /// <summary>
        /// Obtiene un cifrador.
        /// </summary>
        protected static Cipher GetCipher
        {
            get
            {
                if (cipher == null)
                {
                    cipher = new Cipher("contra1", "salt1", "contras2", "salte2");
                }
                return cipher;
            }
        }

        /// <summary>
        /// Inserta el objeto actual en la base de datos.
        /// </summary>
        protected abstract void insert();

        /// <summary>
        /// Modifica el objeto actual en la base de datos.
        /// </summary>
        protected abstract void update();

        /// <summary>
        /// Elimina el objeto acutal en la base de datos.
        /// </summary>
        protected abstract void delete();

        /// <summary>
        /// Obtiene el nombre de cadena de conexión por defecto.
        /// </summary>
        protected static string defaultConnString
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

        #region//Getter&Setters

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

        #endregion

        #region//Public Methods

        /// <summary>
		/// Constructor.
		/// </summary>
		public AEB()
		{
            this.id = new List<object>();
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
                this.insert();
				this.saved = true;
			}
			else
			{
                this.update();
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
                this.delete();
				saved = false;
			}
		}

		/// <summary>
		/// Lee de la base de datos y filtra por el identificador que se le pasa por parámetro.
		/// </summary>
		/// <param name="id">Identificador a buscar.</param>
		/// <returns>El objeto de tipo ENGeneral.</returns>
		public abstract AEB Read(List<object> id);

		/// <summary>
		/// Devuelve todos los registros de la base de datos.
		/// </summary>
		/// <returns></returns>
		public abstract List<AEB> ReadAll();

		#endregion
	}
}

