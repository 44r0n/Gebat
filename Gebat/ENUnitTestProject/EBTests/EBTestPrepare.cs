using System;
using System.IO;
using System.Data;
using System.Data.Common;
using SqlManager;
using System.Collections.Generic;
using GebatCAD.Classes;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ENUnitTestProject.ENTests
{
    [TestClass]
    public class EBTestPrepare
    {
        static protected string connectionString = "GebatDataConnectionString";

        static protected void InitBD(string fileScript)
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

        static protected void ResetConn()
        {
            FactorySql.ResetManager();
        }

        static protected void SetPasswd()
        {
            ADL.Password = "root";
        }

        [AssemblyInitialize()]
        public static void InitTests(TestContext context)
        {
            ResetConn();
            SetPasswd();
            InitBD("TestInit.sql");
        }
    }
}
