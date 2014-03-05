using System;
using SqlManager;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace CADUnitTestProject.ADLTests
{
    public class StubIsql : ISql
    {
        private string foocon;

        public StubIsql()
        {
        }

        public string Conn
        {
            set
            {
                foocon = value;
            }
        }

        public DbConnection Connection(string sqlConnectionString)
        {
            DbConnection ret = new MySqlConnection(foocon);
            return ret;
        }

        public DbCommand Command(string sqlQuery, DbConnection connection)
        {
            DbCommand ret = new MySqlCommand(sqlQuery, (MySqlConnection)connection);
            return ret;
        }

        public DbDataAdapter Adapter(string sqlQuery, DbConnection connection)
        {
            DbDataAdapter ret = new MySqlDataAdapter(sqlQuery, (MySqlConnection)connection);
            return ret;
        }

        public DbCommandBuilder Builder(DbDataAdapter adapter)
        {
            DbCommandBuilder ret = new MySqlCommandBuilder((MySqlDataAdapter)adapter);
            return ret;
        }
    }
}
