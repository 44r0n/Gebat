using System;
using System.Data;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using GebatCAD.Classes;
using GebatCAD.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CADUnitTestProject.ADLTests
{
    [TestClass]
    public class ADLTypeTest : AADLTest
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

        private void AssertRows(DataRow expected, DataRow actual)
        {
            Assert.AreEqual(expected["Id"], actual["Id"]);
            Assert.AreEqual(expected["Name"], actual["Name"]);
        }

        private AADL type;

        [TestInitialize()]
        public void InnitTest()
        {
            ResetConn();
            SetPasswd();    
            InitBD(specificScript);
            type = new ADLType(connectionString);
        }

        [TestMethod]
        public void TestSelectOne()
        {
            string expected = "Kg";
            List<object> ids = new List<object>();
            ids.Add((int)1);
            DataRow actual = type.Select(ids);
            Assert.AreEqual(expected, actual["Name"].ToString());
        }

        [TestMethod]
        public void TestCount()
        {
            int expected = 3;
            int actual = type.Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void TestCountConnFail()
        {
            setFailConn();
            type.Count();
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void TestLastConnFail()
        {
            setFailConn();
            type.Last();
        }

        [TestMethod]
        public void TestLast()
        {

            DataRow actual = type.Last();
            DataRow expected = tableFormat.NewRow();
            expected["Id"] = 4;
            expected["Name"] = "Paquetes";
            AssertRows(expected, actual);
        }

        [TestMethod]
        public void SelectAll()
        {
            DataTable actual = type.SelectAll();
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
                AssertRows(expected.Rows[i], actual.Rows[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void SelectAllFailConn()
        {
            setFailConn();
            type.SelectAll();
        }

        [TestMethod]
        public void Select()
        {
            List<object> ids = new List<object>();
            ids.Add(1);
            DataRow actual = type.Select(ids);
            DataTable table = tableFormat;
            DataRow expected = table.NewRow();
            expected["Id"] = 1;
            expected["Name"] = "Kg";
            AssertRows(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void SelectVoidList()
        {
            type.Select(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNumberIdException))]
        public void SelectInvalidNumberId()
        {
            List<object> ids = new List<object>();
            ids.Add("hola");
            ids.Add(3);
            type.Select(ids);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void SelectConnFail()
        {
            setFailConn();
            List<object> ids = new List<object>();
            ids.Add(2);
            type.Select(ids);

        }

        [TestMethod]
        public void SelectWhere()
        {
            DataTable expected = tableFormat;
            DataRow row = expected.NewRow();
            row["Id"] = 1;
            row["Name"] = "Kg";
            expected.Rows.Add(row);
            DataTable actual = type.SelectWhere("Name = 'Kg'");

            for (int i = 0; i < expected.Rows.Count; i++)
            {
                AssertRows(expected.Rows[i], actual.Rows[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidStartRecordException))]
        public void SelectWhereInvalidStart()
        {
            type.SelectWhere("Name = 'Kg'", -3);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void SelectWhereInvalidStatement()
        {
            type.SelectWhere("Name = ; ");
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void SelectWhereFailConn()
        {
            setFailConn();
            type.SelectWhere("Name = 'Kg'");
        }

        [TestMethod]
        public void Insert()
        {
            DataRow ins = type.GetVoidRow;
            ins["Name"] = "Cajas";
            DataRow expected = type.GetVoidRow;
            expected["Id"] = 5;
            expected["Name"] = "Cajas";
            DataRow actual = type.Insert(ins);
            AssertRows(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void InsertNullRow()
        {
            DataRow ins = null;
            type.Insert(ins);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void InsertFailCOnn()
        {
            setFailConn();
            DataRow ins = type.GetVoidRow;
            ins["Name"] = "Cajas";
            type.Insert(ins);
        }

        [TestMethod]
        public void Update()
        {
            DataRow mod = type.GetVoidRow;
            mod["Id"] = 1;
            mod["Name"] = "Cajas";
            type.Update(mod);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void UpdateNullRow()
        {
            DataRow mod = null;
            type.Update(mod);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void UpdateFailConn()
        {
            setFailConn();
            DataRow ins = type.GetVoidRow;
            ins["Id"] = 1;
            ins["Name"] = "Cajas";
            type.Update(ins);
        }

        [TestMethod]
        public void Delete()
        {
            DataRow del = type.GetVoidRow;
            del["Id"] = 1;
            del["Name"] = "Kg";
            type.Delete(del);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void DeleteNullRow()
        {
            type.Delete(null);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void DeleteWrongRow()
        {
            DataRow del = type.GetVoidRow;
            del["Name"] = new MySqlConnection();
            type.Delete(del);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void DeleteFailConn()
        {
            setFailConn();
            DataRow del = type.GetVoidRow;
            del["Id"] = 1;
            del["Name"] = "cajas";
            type.Delete(del);
        }
    }
}
