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
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;

namespace SqlManager
{
	public class SqlManager : ISql
	{
		private string provider;

		public SqlManager(string sqlprovider)
		{
			provider = sqlprovider;
		}

		public DbConnection Connection(string sqlConnectionString)
		{
			DbConnection conn;
			switch (provider) 
			{
				case "System.Data.SqlClient":
					conn = new SqlConnection (sqlConnectionString);
					return conn;
					case "MySql.Data.MySqlClient":
					conn = new MySqlConnection (sqlConnectionString);
					return conn;
					default:
					throw new NotSupportedException ("The provider "+provider+" is not supported in this library.");
			}
		}

		public DbCommand Command (string sqlQuery, DbConnection connection)
		{
			DbCommand command;
			switch (provider) 
			{
				case "System.Data.SqlClient":
					command = new SqlCommand (sqlQuery, (SqlConnection)connection);
					return command;
					case "MySql.Data.MySqlClient":
					command = new MySqlCommand (sqlQuery, (MySqlConnection)connection);
					return command;
					default:
					throw new NotSupportedException ("The provider "+provider+" is not supported in this library.");
			}
		}

		public DbDataAdapter Adapter (string sqlQuery, DbConnection connection)
		{
			switch (provider) 
			{
				case "System.Data.SqlClient":
					SqlDataAdapter sqladapter = new SqlDataAdapter (sqlQuery, (SqlConnection) connection);
					return sqladapter;
					case "MySql.Data.MySqlClient":
					MySqlDataAdapter mysqladapter = new MySqlDataAdapter (sqlQuery, (MySqlConnection) connection);
					return mysqladapter;
					default:
					throw new NotSupportedException ("The provider "+provider+" is not supported in this library.");
			}
		}

		public DbCommandBuilder Builder (DbDataAdapter adapter)
		{
			switch (provider) 
			{
				case "System.Data.SqlClient":
					return new SqlCommandBuilder ((SqlDataAdapter)adapter);
					case "MySql.Data.MySqlClient":
					return new MySqlCommandBuilder ((MySqlDataAdapter)adapter);
					default:
					throw new NotSupportedException ("The provider "+provider+" is not supported in this library.");
			}
		}
	}
}


