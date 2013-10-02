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
	public abstract class ACAD
	{
		static protected string password = string.Empty;
		protected string tablename;
		private bool rowReturned;
		private DataRow voidRow;
		private string sqlConnectionString;
		private string sqlprovider;
		private ISql sql = null;
		protected List<string> idFormat;
		private DbConnection conn = null;
		static private DbConnection uniqueconn = null;
		private bool passEstablished;


		#region //Private Methods
		private void connect()
		{
			if (sql == null) 
			{
				sql = FactorySql.Create (sqlprovider);
			}
			if (password != string.Empty && !passEstablished) 
			{
				sqlConnectionString += password;
				passEstablished = true;
			}
			conn = sql.Connection (sqlConnectionString);
			conn.Open();
		}

		private void disconnect()
		{
			if (conn != null)
			{
				conn.Close ();
			}
		}

		private string rowToQuery(DataRow row)
		{
			string query = "SELECT * FROM " + this.TableName + " WHERE ";
			for (int i = 0; i < idFormat.Count; i++)
			{
				query += idFormat[i] + " = " + row[idFormat[i]] + " ";
				if (i != idFormat.Count - 1)
				{
					query += "AND ";
				}
			}
			return query;
		}
		#endregion

		#region//Protected Methods
		/// <summary>
		/// Ejecuta ina sql Scalar.
		/// </summary>
		/// <param name="sql">Query a ejecutar.</param>
		/// <returns>Valor entero devuelto por el Scalar.</returns>
		protected int ExecuteScalar(string query)
		{
			if (uniqueconn == null)
			{
				connect ();
			}
			DbCommand command = sql.Command (query, conn);
			int ret = Convert.ToInt32(command.ExecuteScalar());
			if (uniqueconn == null)
			{
				disconnect ();
			}
			return ret;
		}

		/// <summary>
		/// Ejecuta la query pasada por parámetro y devuelve el resultado en un DataTable.
		/// </summary>
		/// <param name="query">Query a ejecutar.</param>
		/// <returns>DataTable con los resultados.</returns>
		protected DataTable ExecuteQuery(string query)
		{
			DbDataAdapter adapter;
			if (uniqueconn == null)
			{
				connect ();

				adapter = sql.Adapter (query, conn);
			} 
			else
			{
				adapter = sql.Adapter (query, uniqueconn);
			}

			DataSet dSet = new DataSet();
			adapter.Fill(dSet, this.tablename);
			DataTable dTable = dSet.Tables[this.tablename];
			rowReturned = true;
			voidRow = dTable.NewRow();
			if (uniqueconn == null)
			{
				disconnect ();
			}
			return dTable;
		}

		#endregion

		#region //Public Methods
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="connStringName">Nombre de la connectionString que está definida en el archivo de configuración de la aplicación.</param>
		public ACAD(string connStringName)
		{
			//sqlConnectionString = ConfigurationManager.ConnectionStrings["GebatDataConnectionString"].ConnectionString;
			//sqlprovider = ConfigurationManager.ConnectionStrings ["GebatDataConnectionString"].ProviderName;
			sqlConnectionString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
			sqlprovider = ConfigurationManager.ConnectionStrings [connStringName].ProviderName;
			rowReturned = false;
			idFormat = new List<string>();
			tablename = null;
			passEstablished = false;
		}

		/// <summary>
		/// Establece la contraseña de la base de datos.
		/// </summary>
		/// <value>Contraseña.</value>
		static public string Password
		{
			set 
			{
				password = "Password=" + value;
			}
		}

		static public bool AttemptConnection(string connStringName)
		{
			try
			{
				string trysqlConnectionString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
				string trysqlprovider = ConfigurationManager.ConnectionStrings [connStringName].ProviderName;
				ISql tryisql = FactorySql.Create(trysqlprovider);

				if (password != string.Empty) 
				{
					trysqlConnectionString += password;
				}

				DbConnection tryconn = tryisql.Connection (trysqlConnectionString);
				tryconn.Open();
				tryconn.Close();

				return true;
			}
			catch(Exception ex)
			{
				return false;
			}
		}

		/// <summary>
		/// Conecta a la base de datos mediante una única conexión.
		/// </summary>
		/// <param name="connStringName">Nombre de la connectionString que está definida en el archivo de configuración de la aplicación.</param>
		static public void Connect(string connStringName)
		{
			if (uniqueconn != null)
			{
				string sqlConnString = ConfigurationManager.ConnectionStrings [connStringName].ConnectionString;
				string sqlProvider = ConfigurationManager.ConnectionStrings [connStringName].ProviderName;
				ISql consql = FactorySql.Create (sqlProvider);

				if (password != string.Empty)
				{
					sqlConnString += password;
				}

				uniqueconn = consql.Connection (sqlConnString);
				uniqueconn.Open ();
			}
		}

		/// <summary>
		/// Desconecta la conexión única.
		/// </summary>
		static public void Disconnect()
		{
			if (uniqueconn != null)
			{
				uniqueconn.Close ();
				uniqueconn = null;
			}
		}

		/// <summary>
		/// Cuenta el numero de registros en la tabla.
		/// </summary>
		/// <returns>Número de registros en la tabla.</returns>
		public virtual int Count()
		{
			string sql = "SELECT COUNT(*) FROM " + this.tablename;
			return ExecuteScalar(sql);
		}

		/// <summary>
		/// Devuelve el último Id insertado en la base de datos. El campo en la base de datos debe llamarse Id.
		/// </summary>
		/// <returns></returns>
		public virtual DataRow Last()
		{
			string sql = "SELECT * FROM " + this.tablename + " Order by ";
			if (this.idFormat.Count == 1)
			{
				sql += this.idFormat [0];
			} 
			else
			{
				for (int i = 0; i < idFormat.Count; i++)
				{
					if (i == idFormat.Count - 1)
					{
						sql += this.idFormat [i];
					} 
					else
					{
						sql += this.idFormat [i] + ", ";
					}
				}
			}
			sql += " desc limit 1";
			return ExecuteQuery(sql).Rows[0];
		}

		/// <summary>
		/// Obtiene el nombre de la tabla en la base de datos.
		/// </summary>
		public string TableName
		{
			get
			{
				if (tablename == null)
				{
					throw new NullReferenceException("Tablaname is null");
				}
				else
				{
					return tablename;
				}
			}
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
		/// Devuelve una tabla con todos los registros de la misma.
		/// </summary>
		/// <param name="startRecord">Registro por el que se empezará a llenar la tabla.</param>
		/// <param name="maxRecord">Numero máximo de registros que se devolverá.</param>
		/// <returns>DataTable con los datos de la base de datos.</returns>
		public DataTable SelectAll()
		{
			try
			{
				string query = "SELECT * FROM " + tablename;
				DbDataAdapter adapter;
				if (uniqueconn == null)
				{
					connect ();

					adapter = sql.Adapter (query, conn);
				} 
				else
				{
					adapter = sql.Adapter (query, uniqueconn);
				}
				DataTable datatable = new DataTable();
				DataSet dataset = new DataSet();

				adapter.Fill(dataset, tablename);
				datatable = dataset.Tables[tablename];
			

				voidRow = datatable.NewRow();
				rowReturned = true;

				return datatable;
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
					disconnect ();
				}
			}
		}

		/// <summary>
		/// Devuelve una tabla con el primer registro, en caso de estar vacia devuelve null.
		/// </summary>
		public virtual DataTable First()
		{
			try
			{
				string query = "SELECT * FROM "+this.tablename+" limit 1";
				DataTable dTable = ExecuteQuery(query);
				rowReturned = true;
				voidRow = dTable.NewRow();

				if (dTable.Rows.Count == 1)
				{
					return dTable;
				}
				else
				{
					return null;
				}
			}
			catch(Exception ex)
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
		/// Devuelve un DataTable con una ejecucion de la base de datos con la clausula where pasada por parámetro.
		/// </summary>
		/// <param name="whereStatement">Clausula where, deberá estar bien formada.</param>
		/// <param name="startRecord">Registro por el que se empezará a llenar la tabla.</param>
		/// <param name="maxRecord">Numero máximo de registros que se devolverá.</param>
		/// <returns>DaTatable con los datos de la base de datos.</returns>
		public virtual DataTable SelectWhere(string whereStatement, int startRecord = 0, int maxRecords = -1)
		{
			try
			{
				if (startRecord < 0)
				{
					throw new InvalidStartRecordException("Start record cannot be negative");
				}

				string query = "SELECT * FROM " + this.TableName + " WHERE " + whereStatement;
				DbDataAdapter adapter;

				if(uniqueconn == null)
				{
				connect();
				adapter = sql.Adapter(query, conn);
				}
				else
				{
					adapter = sql.Adapter(query,uniqueconn);
				}

				DataTable dt = new DataTable();
				DataSet ds = new DataSet();

				if (maxRecords < 0)
				{
					adapter.Fill(ds, this.TableName);
					dt = ds.Tables[this.TableName];
				}
				else
				{
					adapter.Fill(startRecord, maxRecords, dt);
				}

				rowReturned = true;
				voidRow = dt.NewRow();

				return dt;
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
					adapter = sql.Adapter(query,uniqueconn);
				}

				DataSet dSet = new DataSet();
				adapter.Fill(dSet, this.TableName);
				DataTable dTable = dSet.Tables[this.TableName];
				rowReturned = true;

				voidRow = dTable.NewRow();
				DataRow addRow = dTable.NewRow();
				addRow.ItemArray = newRow.ItemArray;
				dTable.Rows.Add(addRow);

				sql.Builder(adapter);
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
					adapter = sql.Adapter(query,uniqueconn);
				}

				DataSet dSet = new DataSet();
				adapter.Fill(dSet, this.TableName);
				DataTable dTable = dSet.Tables[this.TableName];
				rowReturned = true;

				dTable.Rows[0].BeginEdit();
				dTable.Rows[0].ItemArray = newRow.ItemArray;
				dTable.Rows[0].EndEdit();

				sql.Builder(adapter);

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
				if (delRow == null || delRow["Id"] == null)
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
					adapter = sql.Adapter(query,uniqueconn);
				}

				DataSet dSet = new DataSet();
				adapter.Fill(dSet, this.TableName);
				DataTable dTable = dSet.Tables[this.TableName];
				rowReturned = true;

				dTable.Rows[0].Delete();

				sql.Builder(adapter);
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


