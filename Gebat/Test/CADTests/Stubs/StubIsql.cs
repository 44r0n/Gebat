//
//  StubIsql.cs
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
using SqlManager;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace Test
{
	public class StubIsql : ISql
	{
		private string fooconn;

		public StubIsql ()
		{

		}

		public string Conn
		{
			set
			{
				fooconn = value;
			}
		}

		public DbConnection Connection (string sqlConnectionString)
		{
			DbConnection ret = new MySqlConnection (fooconn);
			return ret;
		}

		public DbCommand Command (string sqlQuery, DbConnection connection)
		{
			DbCommand ret = new MySqlCommand (sqlQuery, (MySqlConnection)connection);
			return ret;
		}

		public DbDataAdapter Adapter (string sqlQuery, DbConnection connection)
		{
			DbDataAdapter ret = new MySqlDataAdapter (sqlQuery, (MySqlConnection)connection);
			return ret;
		}

		public DbCommandBuilder Builder (DbDataAdapter adapter)
		{
			DbCommandBuilder ret = new MySqlCommandBuilder ((MySqlDataAdapter) adapter);
			return ret;
		}
	}
}

