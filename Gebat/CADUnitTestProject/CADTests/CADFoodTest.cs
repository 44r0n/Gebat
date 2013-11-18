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
    [TestClass]
    public class CADFoodTest
    {
        private DataTable tableFormat
        {
            get
            {
                DataTable expected = new DataTable();
                expected.Columns.Add("Id", typeof(int));
                expected.Columns.Add("Name", typeof(string));
                return expected;
            }
        }

        private string scriptFileName = "FoodTest.sql";

        private string connectionString = "GebatDataConnectionString";

        private ISql fooCon
        {
            get
            {
                StubIsql ret = new StubIsql();
                ret.Conn = "Server=tacas;Database=test;Uid=root;";
                return ret;
            }
        }

        private void setFailConn()
        {
            ISql conn = fooCon;
            FactorySql factory = new FactorySql();
            factory.SetManager(conn);
        }

        private void InitBD()
        {
            string connString = ConfigurationManager.ConnectionStrings[connectionString].ConnectionString;
            connString += "Password=root";
            string provider = ConfigurationManager.ConnectionStrings[connectionString].ProviderName;
            ISql manager = FactorySql.Create(provider);
            FileInfo file = new FileInfo(scriptFileName);
            StreamReader lector = file.OpenText();
            string script = lector.ReadToEnd();
            lector.Close();
            DbConnection conn = manager.Connection(connString);
            conn.Open();
            DbCommand comando = manager.Command(script, conn);
            comando.ExecuteNonQuery();
            conn.Close();
        }

        private void ResetConn()
        {
            FactorySql.ResetManager();
        }

        public void SetPasswd()
        {
            ACAD.Password = "root";
        }

        [TestInitialize()]
        public void InnitTest()
        {
            ResetConn();
            SetPasswd();
            InitBD();
        }

        [TestMethod]
        public void TestSelectOne()
        {
            string expected = "Patates";
            ACAD food = new CADFood("GebatDataConnectionString");
            List<object> ids = new List<object>();
            ids.Add((int)1);
            DataRow actual = food.Select(ids);
            Assert.AreEqual(actual["Name"].ToString(), expected);
        }

        [TestMethod]
        public void TestCount()
        {
            int expected = 3;
            ACAD food = new CADFood("GebatDataConnectionString");
            int actual = food.Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void TestCountConnFail()
        {
            setFailConn();
            ACAD food = new CADFood("GebatDataConnectionString");

            food.Count();
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void TestLastConnFail()
        {
            setFailConn();
            ACAD food = new CADFood("GebatDataConnectionString");

            food.Last();
        }

        [TestMethod]
        public void TestLast()
        {
            ACAD food = new CADFood("GebatDataConnectionString");
            DataRow actual = food.Last();
            DataRow expected = food.GetVoidRow;
            expected["Id"] = 4;
            expected["Name"] = "Pomes";
            Assert.AreEqual(expected["Id"], actual["Id"]);
            Assert.AreEqual(expected["Name"], actual["Name"]);
        }

        [TestMethod]
        public void SelectAll()
        {
            ACAD food = new CADFood("GebatDataConnectionString");
            DataTable actual = food.SelectAll();
            DataTable expected = this.tableFormat;
            DataRow row = expected.NewRow();
            row["Id"] = 1;
            row["Name"] = "Patates";
            expected.Rows.Add(row);
            DataRow row2 = expected.NewRow();
            row2["Id"] = 2;
            row2["Name"] = "Tomates";
            expected.Rows.Add(row2);
            DataRow row3 = expected.NewRow();
            row3["Id"] = 4;
            row3["Name"] = "Pomes";
            expected.Rows.Add(row3);
            for (int i = 0; i < expected.Rows.Count; i++)
            {
                Assert.AreEqual(expected.Rows[i]["Id"], actual.Rows[i]["Id"]);
                Assert.AreEqual(expected.Rows[i]["Name"], actual.Rows[i]["Name"]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void SelectAllFailConn()
        {
            setFailConn();
            ACAD food = new CADFood("GebatDataConnectionString");

            food.SelectAll();
        }

        [TestMethod]
        public void Select()
        {
            ACAD food = new CADFood("GebatDataConnectionString");
            List<object> ids = new List<object>();
            ids.Add(1);
            DataRow actual = food.Select(ids);
            DataTable table = tableFormat;
            DataRow expected = table.NewRow();
            expected["Id"] = 1;
            expected["Name"] = "Patates";

            Assert.AreEqual(expected["Id"], actual["Id"]);
            Assert.AreEqual(expected["Name"], actual["Name"]);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void SelectVoidList()
        {
            ACAD food = new CADFood("GebatDataConnectionString");
            food.Select(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNumberIdException))]
        public void SelectInvalidNumberId()
        {
            List<object> ids = new List<object>();
            ids.Add("hola");
            ids.Add(3);
            ACAD food = new CADFood("GebatDataConnectionString");
            food.Select(ids);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void SelectConnFail()
        {
            setFailConn();

            ACAD food = new CADFood("GebatDataConnectionString");
            List<object> ids = new List<object>();
            ids.Add(2);
            food.Select(ids);

        }

        [TestMethod]
        public void SelectWhere()
        {
            ACAD food = new CADFood("GebatDataConnectionString");
            DataTable expected = tableFormat;
            DataRow row = expected.NewRow();
            row["Id"] = 1;
            row["Name"] = "Patates";
            expected.Rows.Add(row);
            DataTable actual = food.SelectWhere("Name = 'Patates'");

            for (int i = 0; i < expected.Rows.Count; i++)
            {
                Assert.AreEqual(expected.Rows[i]["Id"], actual.Rows[i]["Id"]);
                Assert.AreEqual(expected.Rows[i]["Name"], actual.Rows[i]["Name"]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidStartRecordException))]
        public void SelectWhereInvalidStart()
        {
            ACAD food = new CADFood("GebatDataConnectionString");
            food.SelectWhere("Name = 'Patates'", -3);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void SelectWjereInvalidStatement()
        {
            ACAD food = new CADFood("GebatDataConnectionString");
            food.SelectWhere("Name = ; ");
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void SelectWhereFailConn()
        {
            setFailConn();

            ACAD food = new CADFood("GebatDataConnectionString");
            food.SelectWhere("Name = 'Patates'");
        }

        [TestMethod]
        public void Insert()
        {
            ACAD food = new CADFood("GebatDataConnectionString");
            DataRow ins = food.GetVoidRow;
            ins["Name"] = "Peres";
            DataRow expected = food.GetVoidRow;
            expected["Id"] = 5;
            expected["Name"] = "Peres";
            DataRow actual = food.Insert(ins);
            Assert.AreEqual(expected["Id"], actual["Id"]);
            Assert.AreEqual(expected["Name"], actual["Name"]);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void InsertNullRow()
        {
            ACAD food = new CADFood("GebatDataConnectionString");
            DataRow ins = null;
            food.Insert(ins);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertWrongRow()
        {
            ACAD food = new CADFood("GebatDataConnectionString");
            DataRow ins = food.GetVoidRow;
            ins["Name"] = 4;
            ins["Quantity"] = "hola";
            food.Insert(ins);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void InsertFailConn()
        {
            setFailConn();
            ACAD food = new CADFood("GebatDataConnectionString");
            DataRow ins = food.GetVoidRow;
            ins["Name"] = "Peres";
            ins["Quantity"] = 4;
            food.Insert(ins);
        }

        [TestMethod]
        public void Update()
        {
            ACAD food = new CADFood("GebatDataConnectionString");
            DataRow mod = food.GetVoidRow;
            mod["Id"] = 1;
            mod["Name"] = "Peres";
            food.Update(mod);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void UpdateNullRow()
        {
            ACAD food = new CADFood("GebatDataConnectionString");
            DataRow mod = null;
            food.Update(mod);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateWrongRow()
        {
            ACAD food = new CADFood("GebatDataConnectionString");
            DataRow ins = food.GetVoidRow;
            ins["Id"] = 1;
            ins["Name"] = 4;
            ins["Quantity"] = "hola";
            food.Update(ins);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void UpdateFailConn()
        {
            setFailConn();
            ACAD food = new CADFood("GebatDataConnectionString");
            DataRow ins = food.GetVoidRow;
            ins["Id"] = 1;
            ins["Name"] = "Peres";
            ins["Quantity"] = 4;
            food.Update(ins);
        }

        [TestMethod]
        public void Delete()
        {
            ACAD food = new CADFood("GebatDataConnectionString");
            DataRow del = food.GetVoidRow;
            del["Id"] = 1;
            del["Name"] = "Patates";
            food.Delete(del);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void DeleteNullRow()
        {
            ACAD food = new CADFood("GebatDataConnectionString");
            food.Delete(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteWrongRow()
        {
            ACAD food = new CADFood("GebatDataConnectionString");
            DataRow del = food.GetVoidRow;
            del["Name"] = 4;
            del["Quantity"] = "hola";
            food.Delete(del);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void DeleteFailConn()
        {
            setFailConn();
            ACAD food = new CADFood("GebatDataConnectionString");
            DataRow del = food.GetVoidRow;
            del["Id"] = 1;
            del["Name"] = "Patates";
            del["Quantity"] = 2;
            food.Delete(del);
        }
    }
}