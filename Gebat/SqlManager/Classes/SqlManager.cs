//
//  SqlManager.cs
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
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace SqlManager
{
	public class SqlManager : ISql
	{
		private string provider;

        private void providerNotSupported()
        {
            throw new NotSupportedException("The provider " + provider + " is not supported in this library.");
        }

		public SqlManager(string sqlprovider)
		{
			provider = sqlprovider;
		}

		public DbConnection Connection(string sqlConnectionString)
		{
			DbConnection conn = null;
			switch (provider) 
			{
				case "System.Data.SqlClient":
					conn = new SqlConnection (sqlConnectionString);
                    break;
				case "MySql.Data.MySqlClient":
					conn = new MySqlConnection (sqlConnectionString);
                    break;
				default:
                    providerNotSupported();
                    break;
			}
            return conn;
		}

		public DbCommand Command (string sqlQuery, DbConnection connection)
		{
			DbCommand command = null;
			switch (provider) 
			{
				case "System.Data.SqlClient":
					command = new SqlCommand (sqlQuery, (SqlConnection)connection);
                    break;
				case "MySql.Data.MySqlClient":
					command = new MySqlCommand (sqlQuery, (MySqlConnection)connection);
                    break;
				default:
                    providerNotSupported();
                    break;
			}
            return command;
		}

		public DbDataAdapter Adapter (string sqlQuery, DbConnection connection)
		{
            DbDataAdapter adapter = null;
			switch (provider) 
			{
				case "System.Data.SqlClient":
                    adapter = new SqlDataAdapter(sqlQuery, (SqlConnection)connection);
                    break;
				case "MySql.Data.MySqlClient":
                    adapter = new MySqlDataAdapter(sqlQuery, (MySqlConnection)connection);
                    break;
				default:
                    providerNotSupported();
                    break;
			}
            return adapter;
		}

        public DbDataAdapter Adapter(DbCommand command)
        {
            DbDataAdapter adapter = null;
            switch (provider)
            {
                case "System.Data.SqlClient":
                    adapter = new SqlDataAdapter((SqlCommand)command);
                    break;
                case "MySql.Data.MySqlClient":
                    adapter = new MySqlDataAdapter((MySqlCommand)command);
                    break;
                default:
                    providerNotSupported();
                    break;
            }
            return adapter;
        }

        public DbParameter Parameter(string name, object param)
        {
            DbParameter ret = null;
            switch (provider)
            {
                case "System.Data.SqlClient":
                    ret = new SqlParameter(name, param);
                    break;
                case "MySql.Data.MySqlClient":
                    ret = new MySqlParameter(name, param);
                    break;
                default:
                    providerNotSupported();
                    break;
            }
            return ret;
        }

		public DbCommandBuilder Builder (DbDataAdapter adapter)
		{
            DbCommandBuilder builder = null;
			switch (provider) 
			{
				case "System.Data.SqlClient":
					builder = new SqlCommandBuilder ((SqlDataAdapter)adapter);
                    break;
				case "MySql.Data.MySqlClient":
                    builder = new MySqlCommandBuilder((MySqlDataAdapter)adapter);
                    break;
				default:
                    providerNotSupported();
                    break;
			}
            return builder;
		}
	}
}


