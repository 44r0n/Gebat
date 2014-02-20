using System.IO;
using System.Data;
using System.Data.Common;
using SqlManager;
using GebatCAD.Classes;
using System.Configuration;

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

        private string connString;
        DbConnection conn;
        ISql manager;

        protected void InitBD(string fileScript)
        {
            connString = ConfigurationManager.ConnectionStrings[connectionString].ConnectionString;
            connString += "Password=root";
            string provider = ConfigurationManager.ConnectionStrings[connectionString].ProviderName;
            manager = FactorySql.Create(provider);
            //FileInfo file = new FileInfo(fileScript);
            //StreamReader lector = file.OpenText();
            //string script = lector.ReadToEnd();
            //lector.Close();
            conn = manager.Connection(connString);
            conn.Open();
            ExecScript("Scripts/CleanDB.sql");
            ExecScript(fileScript);
            //DbCommand comando = manager.Command(script, conn);
            //comando.ExecuteNonQuery();
            conn.Close();
        }

        private void ExecScript(string fileScript)
        {
            FileInfo file = new FileInfo(fileScript);
            StreamReader lector = file.OpenText();
            string sciript = lector.ReadToEnd();
            lector.Close();
            DbCommand comando = manager.Command(sciript, conn);
            comando.ExecuteNonQuery();
        }

        private void CleanDB()
        {

        }

        protected void ResetConn()
        {
            FactorySql.ResetManager();
        }

        protected void SetPasswd()
        {
            AADL.Password = "root";
        }
    }
}
