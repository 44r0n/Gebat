using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using GebatCAD.Exceptions;
using System.Data.Common;

namespace GebatCAD.Classes
{
    public class VIEW
    {
        static protected string password = string.Empty;
        static protected DbConnection uniqueconn = null;
        static protected SqlManager ssql = null;
        protected string tablename;
        protected bool rowReturned;
        protected DataRow voidRow;
        protected string sqlConnectionString;
        protected string sqlprovider;
        protected SqlManager sql = null;
        protected List<string> idFormat;
        protected DbConnection conn = null;
        private bool passEstablished;
        protected const string pattern = "\\@[A-z]*[0-9]*"; 

        #region //Protected Methods

        /// <summary>
        /// Establece una conexión con la base de datos.
        /// </summary>
        protected void connect()
        {
            if (sql == null)
            {
                sql = this.Manager;
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
        /// Ejecuta ina sql Scalar.
        /// </summary>
        /// <param name="sql">Query a ejecutar.</param>
        /// <returns>Valor entero devuelto por el Scalar.</returns>
        protected int executeScalar(DbCommand command)
        {
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
        protected DataTable executeQuery(DbCommand query)
        {
            DbDataAdapter adapter;
            if (uniqueconn == null)
            {
                connect();

                adapter = sql.Adapter(query);
            }
            else
            {
                adapter = ssql.Adapter(query);
            }

            DataSet dSet = new DataSet();
            adapter.Fill(dSet, this.tablename);
            DataTable dTable = dSet.Tables[this.tablename];
            if (!rowReturned)
            {
                rowReturned = true;
                voidRow = dTable.NewRow();
            }
            if (uniqueconn == null)
            {
                disconnect();
            }
            return dTable;
        }
    
        /// <summary>
        /// Crea el comando a partir de una query y de los parámetros en forma de @param.
        /// </summary>
        /// <param name="query">Query a ejecutar.</param>
        /// <param name="values">Parámetros</param>
        /// <returns>Commando resultante creado.</returns>
        protected DbCommand createCommand(string query, params object[] values)
        {
            if (query == null)
            {
                throw new ArgumentNullException("The query cannot be null.");
            }
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

            MatchCollection col = Regex.Matches(query, pattern);
            if (uniqueconn == null)
            {
                for (int i = 0; i < col.Count; i++)
                {
                    command.Parameters.Add(sql.Parameter(col[i].ToString(), values[i]));
                }
            }
            else
            {
                for (int i = 0; i < col.Count; i++)
                {
                    command.Parameters.Add(ssql.Parameter(col[i].ToString(), values[i]));
                }
            }

            return command;
        }

        #endregion

        #region//Public Methods

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="connStringName">Nombre de la connectionString que está definida en el archivo de configuración de la aplicación.</param>
        public VIEW(string connStringName, string tableName, params string[] ids)
        {
            sqlConnectionString = ConfigurationManager.ConnectionStrings[connStringName].ConnectionString;
            sqlprovider = ConfigurationManager.ConnectionStrings[connStringName].ProviderName;
            rowReturned = false;
            idFormat = new List<string>();
            tablename = tableName;
            passEstablished = false;
            foreach (string newid in ids)
            {
                this.idFormat.Add(newid);
            }
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
                SqlManager tryisql = new SqlManager(trysqlprovider);

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
                ssql = new SqlManager(sqlProvider);

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
            string sqlCount = "SELECT COUNT(*) FROM " + this.tablename;
            DbCommand command = createCommand(sqlCount);
            return executeScalar(command);
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
        public virtual DataTable SelectAll()
        {
            string query = "SELECT * FROM " + tablename;
            DbCommand command = createCommand(query);
            DataTable datatable = executeQuery(command);
            return datatable;
        }


        /// <summary>
        /// Devuelve el último Id insertado en la base de datos. El campo en la base de datos debe llamarse Id.
        /// </summary>
        /// <returns>La última fila de la tabla.</returns>
        public virtual DataRow Last()
        {
            string sqlLast = "SELECT * FROM " + this.tablename + " Order by ";
            if (this.idFormat.Count == 1)
            {
                sqlLast += this.idFormat[0];
            }
            else
            {
                for (int i = 0; i < idFormat.Count; i++)
                {
                    if (i == idFormat.Count - 1)
                    {
                        sqlLast += this.idFormat[i];
                    }
                    else
                    {
                        sqlLast += this.idFormat[i] + ", ";
                    }
                }
            }
            sqlLast += " desc limit 1";
            DbCommand command = createCommand(sqlLast);
            return executeQuery(command).Rows[0];
        }

        /// <summary>
        /// Devuelve una tabla con el primer registro, en caso de estar vacia devuelve null.
        /// </summary>
        public virtual DataTable First()
        {
            string query = "SELECT * FROM " + this.tablename + " limit 1";
            DbCommand command = createCommand(query);            
            DataTable dTable = executeQuery(command);

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
        /// Ejecuta la select pasada por parámetro.
        /// </summary>
        /// <param name="query">Query a ejecutar con parámetros.</param>
        /// <param name="values">Valores de los parámetros.</param>
        /// <returns>DataTable resultante de ejecutar la query.</returns>
        public virtual DataTable Select(string query, params object[] values)
        {
            DbCommand command = createCommand(query,values);
            return executeQuery(command);
        }

        /// <summary>
        /// Obtiene el un Manager nuevo.
        /// </summary>
        public virtual SqlManager Manager
        {
            get
            {
                SqlManager ret = new SqlManager(sqlprovider);
                return ret;
            }
        }
        #endregion
    }
}
