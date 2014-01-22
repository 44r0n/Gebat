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
    public class CADFoodTest : ACADTest
    {
        protected override DataTable tableFormat
        {
            get
            {
                DataTable expected = new DataTable();
                expected.Columns.Add("Id", typeof(int));
                expected.Columns.Add("Name", typeof(string));
                expected.Columns.Add("QuantityType", typeof(int));
                expected.Columns.Add("Quantity", typeof(int));
                return expected;
            }
        }

        protected override string specificScript
        {
            get 
            {
                return "Scripts/FoodTest.sql";
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
            string expected = "Patates";
            ACAD food = new CADFood(connectionString);
            List<object> ids = new List<object>();
            ids.Add((int)1);
            DataRow actual = food.Select(ids);
            Assert.AreEqual(actual["Name"].ToString(), expected);
        }

        [TestMethod]
        public void TestCount()
        {
            int expected = 3;
            ACAD food = new CADFood(connectionString);
            int actual = food.Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestLast()
        {
            ACAD food = new CADFood(connectionString);
            DataRow actual = food.Last();
            DataRow expected = food.GetVoidRow;
            expected["Id"] = 4;
            expected["Name"] = "Pomes";
            expected["QuantityType"] = 1;
            Assert.AreEqual(expected["Id"], actual["Id"]);
            Assert.AreEqual(expected["Name"], actual["Name"]);
            Assert.AreEqual(expected["QuantityType"], actual["QuantityType"]);
        }

        [TestMethod]
        public void SelectAll()
        {
            ACAD food = new CADFood(connectionString);
            DataTable actual = food.SelectAll();
            DataTable expected = this.tableFormat;
            DataRow row = expected.NewRow();
            row["Id"] = 1;
            row["Name"] = "Patates";
            row["QuantityType"] = 1;
            row["Quantity"] = 4;
            expected.Rows.Add(row);
            DataRow row2 = expected.NewRow();
            row2["Id"] = 2;
            row2["Name"] = "Tomates";
            row2["QuantityType"] = 1;
            row2["Quantity"] = 0;
            expected.Rows.Add(row2);
            DataRow row3 = expected.NewRow();
            row3["Id"] = 4;
            row3["Name"] = "Pomes";
            row3["QuantityType"] = 1;
            row3["Quantity"] = 2;
            expected.Rows.Add(row3);
            for (int i = 0; i < expected.Rows.Count; i++)
            {
                Assert.AreEqual(expected.Rows[i]["Id"], actual.Rows[i]["Id"]);
                Assert.AreEqual(expected.Rows[i]["Name"], actual.Rows[i]["Name"]);
                Assert.AreEqual(expected.Rows[i]["QuantityType"], actual.Rows[i]["QuantityType"]);
                Assert.AreEqual(expected.Rows[i]["Quantity"], actual.Rows[i]["Quantity"]);
            }
        }

        [TestMethod]
        public void Select()
        {
            ACAD food = new CADFood(connectionString);
            List<object> ids = new List<object>();
            ids.Add(1);
            DataRow actual = food.Select(ids);
            DataTable table = tableFormat;
            DataRow expected = table.NewRow();
            expected["Id"] = 1;
            expected["Name"] = "Patates";
            expected["QuantityType"] = 1;
            expected["Quantity"] = 4;

            Assert.AreEqual(expected["Id"], actual["Id"]);
            Assert.AreEqual(expected["Name"], actual["Name"]);
            Assert.AreEqual(expected["QuantityType"], actual["QuantityType"]);
            Assert.AreEqual(expected["Quantity"], actual["Quantity"]);
        }

        [TestMethod]
        public void SelectWhere()
        {
            ACAD food = new CADFood(connectionString);
            DataTable expected = tableFormat;
            DataRow row = expected.NewRow();
            row["Id"] = 1;
            row["Name"] = "Patates";
            row["QuantityType"] = 1;
            row["Quantity"] = 4;
            expected.Rows.Add(row);
            DataTable actual = food.SelectWhere("Name = 'Patates'");

            for (int i = 0; i < expected.Rows.Count; i++)
            {
                Assert.AreEqual(expected.Rows[i]["Id"], actual.Rows[i]["Id"]);
                Assert.AreEqual(expected.Rows[i]["Name"], actual.Rows[i]["Name"]);
                Assert.AreEqual(expected.Rows[i]["QuantityType"], actual.Rows[i]["QuantityType"]);
                Assert.AreEqual(expected.Rows[i]["Quantity"], actual.Rows[i]["Quantity"]);
            }
        }

        [TestMethod]
        public void Insert()
        {
            ACAD food = new CADFood(connectionString);
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
        public void Update()
        {
            ACAD food = new CADFood(connectionString);
            DataRow mod = food.GetVoidRow;
            mod["Id"] = 1;
            mod["Name"] = "Peres";
            food.Update(mod);
        }

        [TestMethod]
        public void Delete()
        {
            ACAD food = new CADFood(connectionString);
            DataRow del = food.GetVoidRow;
            del["Id"] = 1;
            del["Name"] = "Patates";
            food.Delete(del);
        }
    }
}