using System.IO;
using System.Data;
using System.Data.Common;
using SqlManager;
using GebatCAD.Classes;
using System.Configuration;

namespace CADUnitTestProject.ADLTests
{
    public abstract class AADLTest
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
            conn = manager.Connection(connString);
            conn.Open();
            ExecScript("Scripts/CleanDB.sql");
            ExecScript(fileScript);
            conn.Close();
        }

        private void ExecScript(string fileScript)
        {
            FileInfo file = new FileInfo(fileScript);
            StreamReader reader = file.OpenText();
            string sciript = reader.ReadToEnd();
            reader.Close();
            DbCommand command = manager.Command(sciript, conn);
            command.ExecuteNonQuery();
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
