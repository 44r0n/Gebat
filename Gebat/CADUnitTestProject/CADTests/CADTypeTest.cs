using System;
using System.Data;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using GebatCAD.Classes;
using GebatCAD.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CADUnitTestProject.CADTests
{
    [TestClass]
    public class CADTypeTest : ACADTest
    {

        protected override DataTable tableFormat
        {
            get 
            {
                DataTable expected = new DataTable();
                expected.Columns.Add("Id", typeof(int));
                expected.Columns.Add("Name", typeof(string));
                return expected;
            }
        }

        protected override string specificScript
        {
            get 
            {
                return "Scripts/TypeTest.sql";
            }
        }

        [TestInitialize()]
        public void InnitTest()
        {
            ResetConn();
            SetPasswd();    
            InitBD(specificScript);
        }

        [TestMethod]
        public void TestSelectOne()
        {
            string expected = "Kg";
            ACAD food = new CADType(connectionString);
            List<object> ids = new List<object>();
            ids.Add((int)1);
            DataRow actual = food.Select(ids);
            Assert.AreEqual(actual["Name"].ToString(), expected);
        }

        [TestMethod]
        public void TestCount()
        {
            int expected = 3;
            ACAD food = new CADType(connectionString);
            int actual = food.Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void TestCountConnFail()
        {
            setFailConn();
            ACAD food = new CADType(connectionString);

            food.Count();
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void TestLastConnFail()
        {
            setFailConn();
            ACAD food = new CADType(connectionString);

            food.Last();
        }

        [TestMethod]
        public void TestLast()
        {

            ACAD food = new CADType(connectionString);
            DataRow actual = food.Last();
            DataRow expected = tableFormat.NewRow();
            expected["Id"] = 4;
            expected["Name"] = "Paquetes";
            Assert.AreEqual(expected["Id"], actual["Id"]);
            Assert.AreEqual(expected["Name"], actual["Name"]);
        }

        [TestMethod]
        public void SelectAll()
        {
            ACAD food = new CADType(connectionString);
            DataTable actual = food.SelectAll();
            DataTable expected = this.tableFormat;
            DataRow row = expected.NewRow();
            row["Id"] = 1;
            row["Name"] = "Kg";
            expected.Rows.Add(row);
            DataRow row2 = expected.NewRow();
            row2["Id"] = 2;
            row2["Name"] = "Litros";
            expected.Rows.Add(row2);
            DataRow row3 = expected.NewRow();
            row3["Id"] = 4;
            row3["Name"] = "Paquetes";
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
            ACAD food = new CADType(connectionString);

            food.SelectAll();
        }

        [TestMethod]
        public void Select()
        {
            ACAD food = new CADType(connectionString);
            List<object> ids = new List<object>();
            ids.Add(1);
            DataRow actual = food.Select(ids);
            DataTable table = tableFormat;
            DataRow expected = table.NewRow();
            expected["Id"] = 1;
            expected["Name"] = "Kg";

            Assert.AreEqual(expected["Id"], actual["Id"]);
            Assert.AreEqual(expected["Name"], actual["Name"]);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void SelectVoidList()
        {
            ACAD food = new CADType(connectionString);
            food.Select(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNumberIdException))]
        public void SelectInvalidNumberId()
        {
            List<object> ids = new List<object>();
            ids.Add("hola");
            ids.Add(3);
            ACAD food = new CADType(connectionString);
            food.Select(ids);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void SelectConnFail()
        {
            setFailConn();

            ACAD food = new CADType(connectionString);
            List<object> ids = new List<object>();
            ids.Add(2);
            food.Select(ids);

        }

        [TestMethod]
        public void SelectWhere()
        {
            ACAD food = new CADType(connectionString);
            DataTable expected = tableFormat;
            DataRow row = expected.NewRow();
            row["Id"] = 1;
            row["Name"] = "Kg";
            expected.Rows.Add(row);
            DataTable actual = food.SelectWhere("Name = 'Kg'");

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
            ACAD food = new CADType(connectionString);
            food.SelectWhere("Name = 'Kg'", -3);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void SelectWhereInvalidStatement()
        {
            ACAD food = new CADType(connectionString);
            food.SelectWhere("Name = ; ");
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void SelectWhereFailConn()
        {
            setFailConn();

            ACAD food = new CADType(connectionString);
            food.SelectWhere("Name = 'Kg'");
        }

        [TestMethod]
        public void Insert()
        {
            ACAD food = new CADType(connectionString);
            DataRow ins = food.GetVoidRow;
            ins["Name"] = "Cajas";
            DataRow expected = food.GetVoidRow;
            expected["Id"] = 5;
            expected["Name"] = "Cajas";
            DataRow actual = food.Insert(ins);
            Assert.AreEqual(expected["Id"], actual["Id"]);
            Assert.AreEqual(expected["Name"], actual["Name"]);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void InsertNullRow()
        {
            ACAD food = new CADType(connectionString);
            DataRow ins = null;
            food.Insert(ins);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void InsertFailCOnn()
        {
            setFailConn();
            ACAD food = new CADType(connectionString);
            DataRow ins = food.GetVoidRow;
            ins["Name"] = "Cajas";
            food.Insert(ins);
        }

        [TestMethod]
        public void Update()
        {
            ACAD food = new CADType(connectionString);
            DataRow mod = food.GetVoidRow;
            mod["Id"] = 1;
            mod["Name"] = "Cajas";
            food.Update(mod);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void UpdateNullRow()
        {
            ACAD food = new CADType(connectionString);
            DataRow mod = null;
            food.Update(mod);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void UpdateFailConn()
        {
            setFailConn();
            ACAD food = new CADType(connectionString);
            DataRow ins = food.GetVoidRow;
            ins["Id"] = 1;
            ins["Name"] = "Cajas";
            food.Update(ins);
        }

        [TestMethod]
        public void Delete()
        {
            ACAD food = new CADType(connectionString);
            DataRow del = food.GetVoidRow;
            del["Id"] = 1;
            del["Name"] = "Kg";
            food.Delete(del);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void DeleteNullRow()
        {
            ACAD food = new CADType(connectionString);
            food.Delete(null);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void DeleteWrongRow()
        {
            ACAD food = new CADType(connectionString);
            DataRow del = food.GetVoidRow;
            del["Name"] = new MySqlConnection();
            food.Delete(del);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void DeleteFailConn()
        {
            setFailConn();
            ACAD food = new CADType(connectionString);
            DataRow del = food.GetVoidRow;
            del["Id"] = 1;
            del["Name"] = "cajas";
            food.Delete(del);
        }
    }
}
