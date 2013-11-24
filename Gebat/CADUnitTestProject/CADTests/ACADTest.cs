using System;
using System.IO;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using SqlManager;
using GebatCAD.Classes;
using GebatCAD.Exceptions;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CADUnitTestProject.CADTests
{
    public abstract class ACADTest
    {
        protected abstract DataTable tableFormat
        {
            get;
        }

        protected abstract string specificScript
        {
            get;
        }

        protected string connectionString = "GebatDataConnectionString";

        protected ISql foocon
        {
            get
            {
                StubIsql  ret= new StubIsql();
                ret.Conn = "Server=tacas;Database=test;Uid=root;";
                return ret;
            }
        }

        protected void setFailConn()
        {
            ISql conn = foocon;
            FactorySql factory = new FactorySql();
            factory.SetManager(conn);
        }

        protected void InitBD(string fileScript)
        {
            string connString = ConfigurationManager.ConnectionStrings[connectionString].ConnectionString;
            connString += "Password=root";
            string provider = ConfigurationManager.ConnectionStrings[connectionString].ProviderName;
            ISql manager = FactorySql.Create(provider);
            FileInfo file = new FileInfo(fileScript);
            StreamReader lector = file.OpenText();
            string script = lector.ReadToEnd();
            lector.Close();
            DbConnection conn = manager.Connection(connString);
            conn.Open();
            DbCommand comando = manager.Command(script, conn);
            comando.ExecuteNonQuery();
            conn.Close();
        }

        protected void ResetConn()
        {
            FactorySql.ResetManager();
        }

        protected void SetPasswd()
        {
            ACAD.Password = "root";
        }
    }
}
