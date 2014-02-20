using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using GebatCAD.Exceptions;
using System.Data.Common;
using SqlManager;

namespace GebatCAD.Classes
{
    public abstract class AVIEW
    {
        static protected string password = string.Empty;
        protected string tablename;
        protected bool rowReturned;
        protected DataRow voidRow;
        private string sqlConnectionString;
        private string sqlprovider;
        protected ISql sql = null;
        protected List<string> idFormat;
        protected DbConnection conn = null;
        static protected DbConnection uniqueconn = null;
        static protected ISql ssql = null;
        private bool passEstablished;

        #region //Protected Methods

        /// <summary>
        /// Establece una conexión con la base de datos.
        /// </summary>
        protected void connect()
        {
            if (sql == null)
            {
                sql = FactorySql.Create(sqlprovider);
            }
            if (password != string.Empty && !passEstablished)
            {
                sqlConnectionString += password;
                passEstablished = true;
            }
            conn = sql.Connection(sqlConnectionString);
            conn.Open();
        }

        /// <summary>
        /// Desconecta de la base de datos.
        /// </summary>
        protected void disconnect()
        {
            if (conn != null)
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Busca una fila pasada por parámetro en la base de datos.
        /// </summary>
        /// <param name="row">Fila a buscar en la base de datos.</param>
        /// <returns></returns>
        protected string rowToQuery(DataRow row)
        {
            string query = "SELECT * FROM " + this.TableName + " WHERE ";
            for (int i = 0; i < idFormat.Count; i++)
            {
                query += idFormat[i] + " = '" + row[idFormat[i]] + "' ";
                if (i != idFormat.Count - 1)
                {
                    query += "AND ";
                }
            }
            return query;
        }

        /// <summary>
        /// Ejecuta ina sql Scalar.
        /// </summary>
        /// <param name="sql">Query a ejecutar.</param>
        /// <returns>Valor entero devuelto por el Scalar.</returns>
        protected int ExecuteScalar(string query)
        {
            DbCommand command;
            if (uniqueconn == null)
            {
                connect();
                command = sql.Command(query, conn);
            }
            else
            {
                command = ssql.Command(query, uniqueconn);
            }

            int ret = Convert.ToInt32(command.ExecuteScalar());
            if (uniqueconn == null)
            {
                disconnect();
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
                connect();

                adapter = sql.Adapter(query, conn);
            }
            else
            {
                adapter = ssql.Adapter(query, uniqueconn);
            }

            DataSet dSet = new DataSet();
            adapter.Fill(dSet, this.tablename);
            DataTable dTable = dSet.Tables[this.tablename];
            rowReturned = true;
            voidRow = dTable.NewRow();
            if (uniqueconn == null)
            {
                disconnect();
            }
            return dTable;
        }

        #endregion

        #region//Public Methods

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="connStringName">Nombre de la connectionString que está definida en el archivo de configuración de la aplicación.</param>
        public AVIEW(string connStringName)
        {
            sqlConnectionString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            sqlprovider = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
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

        /// <summary>
        /// Intenta establecer una única conexión a la base de datos.
        /// </summary>
        /// <param name="connStringName">Nombre de la conexión en el archibo App.conf</param>
        /// <returns>True si ha conseguido establecer una conexión con la base de datos, false en caso contrario.</returns>
        static public bool AttemptConnection(string connStringName)
        {
            try
            {
                string trysqlConnectionString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
                string trysqlprovider = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
                ISql tryisql = FactorySql.Create(trysqlprovider);

                if (password != string.Empty)
                {
                    trysqlConnectionString += password;
                }

                DbConnection tryconn = tryisql.Connection(trysqlConnectionString);
                tryconn.Open();
                tryconn.Close();

                return true;
            }
            catch (Exception ex)
            {
                //Log
                return false;
            }
        }

        /// <summary>
        /// Conecta a la base de datos mediante una única conexión.
        /// </summary>
        /// <param name="connStringName">Nombre de la connectionString que está definida en el archivo de configuración de la aplicación.</param>
        static public void Connect(string connStringName)
        {
            if (uniqueconn == null)
            {
                string sqlConnString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
                string sqlProvider = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
                ssql = FactorySql.Create(sqlProvider);

                if (password != string.Empty)
                {
                    sqlConnString += password;
                }

                uniqueconn = ssql.Connection(sqlConnString);
                uniqueconn.Open();
            }
        }

        /// <summary>
        /// Desconecta la conexión única.
        /// </summary>
        static public void Disconnect()
        {
            if (uniqueconn != null)
            {
                uniqueconn.Close();
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
        /// Devuelve una tabla con todos los registros de la misma.
        /// </summary>
        /// <param name="startRecord">Registro por el que se empezará a llenar la tabla.</param>
        /// <param name="maxRecord">Numero máximo de registros que se devolverá.</param>
        /// <returns>DataTable con los datos de la base de datos.</returns>
        public DataTable SelectAll()
        {
            string query = "SELECT * FROM " + tablename;
            DbDataAdapter adapter;
            if (uniqueconn == null)
            {
                connect();

                adapter = sql.Adapter(query, conn);
            }
            else
            {
                adapter = ssql.Adapter(query, uniqueconn);
            }
            DataTable datatable = new DataTable();
            DataSet dataset = new DataSet();

            adapter.Fill(dataset, tablename);
            datatable = dataset.Tables[tablename];


            voidRow = datatable.NewRow();
            rowReturned = true;
    
            if (uniqueconn == null)
            {
                disconnect();
            }    

            return datatable;
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
                
            if (uniqueconn == null)
            {
                disconnect();
            }
                
            if (dTable.Rows.Count == 1)
            {
                return dTable.Rows[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Devuelve una tabla con el primer registro, en caso de estar vacia devuelve null.
        /// </summary>
        public virtual DataTable First()
        {
            string query = "SELECT * FROM " + this.tablename + " limit 1";
            DataTable dTable = ExecuteQuery(query);
            rowReturned = true;
            voidRow = dTable.NewRow();

            if (uniqueconn == null)
            {
                disconnect();
            }

            if (dTable.Rows.Count == 1)
            {
                return dTable;
            }
            else
            {
                return null;
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
            if (startRecord < 0)
            {
                throw new InvalidStartRecordException("Start record cannot be negative");
            }

            string query = "SELECT * FROM " + this.TableName + " WHERE " + whereStatement;
            DbDataAdapter adapter;

            if (uniqueconn == null)
            {
                connect();
                adapter = sql.Adapter(query, conn);
            }
            else
            {
                adapter = ssql.Adapter(query, uniqueconn);
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

            if (uniqueconn == null)
            {
                disconnect();
            }

            return dt;
        }
        #endregion
    }
}
