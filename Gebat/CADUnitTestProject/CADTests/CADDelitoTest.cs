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
    public class CADDelitoTest : ACADTest
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
                return "Scripts/DelitosTest.sql";
            }
        }

        private AADL delito;

        private void AssertRow(DataRow expected, DataRow actual)
        {
            Assert.AreEqual(expected["Id"], actual["Id"]);
            Assert.AreEqual(expected["Name"], actual["Name"]);
        }

        [TestInitialize()]
        public void InnitTest()
        {
            ResetConn();
            SetPasswd();
            InitBD(specificScript);
            delito = new ADLCrime(connectionString);
        }

        [TestMethod]
        public void TestSelectOne()
        {
            string expected = "Robo";
            List<object> ids = new List<object>();
            ids.Add((int)1);
            DataRow actual = delito.Select(ids);
            Assert.AreEqual(actual["Name"].ToString(), expected);
        }

        [TestMethod]
        public void TestCount()
        {
            int expected = 1;
            AADL food = new ADLCrime(connectionString);
            int actual = food.Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Select()
        {
            List<object> ids = new List<object>();
            ids.Add(1);
            DataRow actual = delito.Select(ids);
            DataTable table = tableFormat;
            DataRow expected = table.NewRow();
            expected["Id"] = 1;
            expected["Name"] = "Robo";
            AssertRow(expected, actual);
        }

        [TestMethod]
        public void Insert()
        {
            DataRow ins = delito.GetVoidRow;
            ins["Name"] = "Cosas";
            DataRow expected = delito.GetVoidRow;
            expected["Id"] = 2;
            expected["Name"] = "Cosas";
            DataRow actual = delito.Insert(ins);
            AssertRow(expected, actual);
        }

        [TestMethod]
        public void Update()
        {
            AADL food = new ADLCrime(connectionString);
            DataRow mod = food.GetVoidRow;
            mod["Id"] = 1;
            mod["Name"] = "Cajas";
            food.Update(mod);
        }

        [TestMethod]
        public void Delete()
        {
            AADL food = new ADLCrime(connectionString);
            DataRow del = food.GetVoidRow;
            del["Id"] = 1;
            del["Name"] = "Robo";
            food.Delete(del);
        }
    }
}
