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

        private ADL type;

        [TestInitialize()]
        public void InnitTest()
        {
            ResetConn();
            SetPasswd();    
            InitBD(specificScript);
            type = new ADL(connectionString, "type", "Id");
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
        [ExpectedException(typeof(ArgumentNullException))]
        public void SelectVoidList()
        {
            type.Select(null);
        }

        [TestMethod]
        public void SelectWhere()
        {
            DataTable expected = tableFormat;
            DataRow row = expected.NewRow();
            row["Id"] = 1;
            row["Name"] = "Kg";
            expected.Rows.Add(row);
            DataTable actual = type.Select("SELECT * FROM type WHERE Name = @Name","Kg");

            for (int i = 0; i < expected.Rows.Count; i++)
            {
                AssertRows(expected.Rows[i], actual.Rows[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void SelectWhereInvalidStatement()
        {
            type.Select("Name = ; ");
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void SelectWhereFailConn()
        {
            setFailConn();
            type.Select("SELECT * FROM type WHERE Name = @Name","Kg");
        }

        [TestMethod]
        public void Insert()
        {
           Assert.AreEqual(1,type.ExecuteNonQuery("INSERT INTO type (Name) VALUES (@Name)","Cajas"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InsertNullRow()
        {
            type.ExecuteNonQuery(null);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void InsertFailCOnn()
        {
            setFailConn();
            type.ExecuteNonQuery("INSERT INTO type (Name) VALUES (@Name)","Cajas");
        }

        [TestMethod]
        public void Update()
        {
            Assert.AreEqual(1,type.ExecuteNonQuery("UPDATE type SET Name = @Name WHERE Id = @Id","Cajones",2));
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void UpdateFailConn()
        {
            setFailConn();
            type.ExecuteNonQuery("UPDATE type SET Name = @Name WHERE Id = @Id","Cajones",2);
        }

        [TestMethod]
        public void Delete()
        {
            Assert.AreEqual(1,type.ExecuteNonQuery("DELETE FROM type WHERE Id = @Id",4));
        }
    }
}
