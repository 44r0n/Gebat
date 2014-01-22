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
            string expected = "Robo";
            ACAD food = new CADDelito(connectionString);
            List<object> ids = new List<object>();
            ids.Add((int)1);
            DataRow actual = food.Select(ids);
            Assert.AreEqual(actual["Name"].ToString(), expected);
        }

        [TestMethod]
        public void TestCount()
        {
            int expected = 1;
            ACAD food = new CADDelito(connectionString);
            int actual = food.Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Select()
        {
            ACAD food = new CADDelito(connectionString);
            List<object> ids = new List<object>();
            ids.Add(1);
            DataRow actual = food.Select(ids);
            DataTable table = tableFormat;
            DataRow expected = table.NewRow();
            expected["Id"] = 1;
            expected["Name"] = "Robo";

            Assert.AreEqual(expected["Id"], actual["Id"]);
            Assert.AreEqual(expected["Name"], actual["Name"]);
        }

        [TestMethod]
        public void Insert()
        {
            ACAD food = new CADDelito(connectionString);
            DataRow ins = food.GetVoidRow;
            ins["Name"] = "Cosas";
            DataRow expected = food.GetVoidRow;
            expected["Id"] = 2;
            expected["Name"] = "Cosas";
            DataRow actual = food.Insert(ins);
            Assert.AreEqual(expected["Id"], actual["Id"]);
            Assert.AreEqual(expected["Name"], actual["Name"]);
        }

        [TestMethod]
        public void Update()
        {
            ACAD food = new CADDelito(connectionString);
            DataRow mod = food.GetVoidRow;
            mod["Id"] = 1;
            mod["Name"] = "Cajas";
            food.Update(mod);
        }

        [TestMethod]
        public void Delete()
        {
            ACAD food = new CADDelito(connectionString);
            DataRow del = food.GetVoidRow;
            del["Id"] = 1;
            del["Name"] = "Robo";
            food.Delete(del);
        }
    }
}
