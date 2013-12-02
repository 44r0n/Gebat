//
//  ACAD.cs
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
using System.Configuration;
using System.Data;
using GebatCAD.Exceptions;
using System.Data.Common;
using SqlManager;

namespace GebatCAD.Classes
{
	public abstract class ACAD : AVIEW
	{
		#region //Public Methods
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="connStringName">Nombre de la connectionString que está definida en el archivo de configuración de la aplicación.</param>
		public ACAD(string connStringName)
            :base(connStringName)
		{
			
			
		}

		/// <summary>
		/// Obtiene una fila vacía lista para rellenar.
		/// </summary>
		/// <returns>DataRow vacía.</returns>
		public virtual DataRow GetVoidRow
		{
			get
			{
				DataRow ret;
				if (!rowReturned)
				{
					First ();
				}
				ret = voidRow;
				return ret;
			}
		}

        /// <summary>
        /// Devuelve el último Id insertado en la base de datos. El campo en la base de datos debe llamarse Id.
        /// </summary>
        /// <returns>La última fila de la tabla.</returns>
        public virtual DataRow Last()
        {
            string sql = "SELECT * FROM " + this.tablename + " Order by ";
            if (this.idFormat.Count == 1)
            {
                sql += this.idFormat[0];
            }
            else
            {
                for (int i = 0; i < idFormat.Count; i++)
                {
                    if (i == idFormat.Count - 1)
                    {
                        sql += this.idFormat[i];
                    }
                    else
                    {
                        sql += this.idFormat[i] + ", ";
                    }
                }
            }
            sql += " desc limit 1";
            return ExecuteQuery(sql).Rows[0];
        }

        /// <summary>
        /// Devuelve un DataRow con el registro indicado en el id. El formato de este id dependerá del IdFormat. En caso de que no lo encuentre devuelve null.
        /// </summary>
        /// <param name="id">Lista con los identificadores de la tabla, solo uno en casod de campo simple.</param>
        /// <returns>Devuelve una fila de la base de datos.</returns>
        public virtual DataRow Select(List<object> id)
        {
            try
            {
                if (id.Count != this.idFormat.Count)
                {
                    throw new InvalidNumberIdException("Invalid number of id");
                }
                string query = "SELECT * FROM " + this.TableName + " WHERE ";
                for (int i = 0; i < id.Count; i++)
                {
                    query += this.idFormat[i] + " = " + id[i].ToString() + " ";
                    if (i != id.Count - 1)
                    {
                        query += "AND ";
                    }
                }

                //connect();
                DataTable dTable = ExecuteQuery(query);
                rowReturned = true;
                voidRow = dTable.NewRow();
                if (dTable.Rows.Count == 1)
                {
                    return dTable.Rows[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (uniqueconn == null)
                {
                    disconnect();
                }
            }
        }

		/// <summary>
		/// Inserta la fila pasada por parámetro en la base de datos. La tabla en la que se insertará dependará del campo TableName.
		/// </summary>
		/// <param name="newRow">Fila nueva que se insertará en la base de datos.</param>
		/// <returns>La fila insertada, con los id's inicializados de la base de datos.</returns>
		public virtual DataRow Insert(DataRow newRow)
		{
			try
			{
				if (newRow == null)
				{
					throw new NullReferenceException("Cannot insert a null row");
				}

				string query = "SELECT * FROM " + this.TableName;
				DbDataAdapter adapter;

				if(uniqueconn == null)
				{
					connect();
					adapter = sql.Adapter(query, conn);
				}
				else
				{
					adapter = ssql.Adapter(query,uniqueconn);
				}

				DataSet dSet = new DataSet();
				adapter.Fill(dSet, this.TableName);
				DataTable dTable = dSet.Tables[this.TableName];
				rowReturned = true;

				voidRow = dTable.NewRow();
				DataRow addRow = dTable.NewRow();
				addRow.ItemArray = newRow.ItemArray;
				dTable.Rows.Add(addRow);

                if (uniqueconn == null)
                {
                    sql.Builder(adapter);
                }
                else
                {
                    ssql.Builder(adapter);
                }
				adapter.Update(dSet, tablename);

				return this.Last();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (uniqueconn == null)
				{
					disconnect ();
				}
			}
		}

		/// <summary>
		/// Modifica en la base de datos la fila pasada por parámetro. La fila que se modifica viene indicada por el Identificador de la fila pasada por parámetro.
		/// </summary>
		/// <param name="newRow">Fila a modificar en la base de datos.</param>
		public virtual void Update(DataRow newRow)
		{
			try
			{
				if (newRow == null)
				{
					throw new NullReferenceException("Cannot update a null row");
				}

				string query = rowToQuery(newRow);
				DbDataAdapter adapter;

				if(uniqueconn == null)
				{
					connect();
					adapter = sql.Adapter(query, conn);
				}
				else
				{	
					adapter = ssql.Adapter(query,uniqueconn);
				}

				DataSet dSet = new DataSet();
				adapter.Fill(dSet, this.TableName);
				DataTable dTable = dSet.Tables[this.TableName];
				rowReturned = true;

				dTable.Rows[0].BeginEdit();
				dTable.Rows[0].ItemArray = newRow.ItemArray;
				dTable.Rows[0].EndEdit();

                if (uniqueconn == null)
                {
                    sql.Builder(adapter);
                }
                else
                {
                    ssql.Builder(adapter);
                }

				adapter.Update(dSet, tablename);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (uniqueconn == null)
				{
					disconnect ();
				}
			}
		}

		/// <summary>
		/// Elimina en la base de datos la fila pasada por parámetro.
		/// </summary>
		/// <param name="delRow">Fila a eliminar.</param>
		public virtual void Delete(DataRow delRow)
		{
			try
			{
				if (delRow == null)
				{
					throw new NullReferenceException("Cannot delete a null row");
				}

				string query = rowToQuery(delRow);
				DbDataAdapter adapter;

				if(uniqueconn == null)
				{
					connect();
					adapter = sql.Adapter(query, conn);
				}
				else
				{
					adapter = ssql.Adapter(query,uniqueconn);
				}

				DataSet dSet = new DataSet();
				adapter.Fill(dSet, this.TableName);
				DataTable dTable = dSet.Tables[this.TableName];
				rowReturned = true;

				dTable.Rows[0].Delete();

                if (uniqueconn == null)
                {
                    sql.Builder(adapter);
                }
                else
                {
                    ssql.Builder(adapter);
                }
				adapter.Update(dSet, tablename);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (uniqueconn == null)
				{
					disconnect ();
				}
			}
		}

		#endregion
	}
}


