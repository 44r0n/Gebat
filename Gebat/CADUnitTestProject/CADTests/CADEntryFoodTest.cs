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
    public class CADEntryFoodTest : ACADTest
    {
        protected override DataTable tableFormat
        {
            get
            {
                DataTable expected = new DataTable();
                expected.Columns.Add("Id", typeof(int));
                expected.Columns.Add("FoodType", typeof(int));
                expected.Columns.Add("QuantityIn", typeof(int));
                expected.Columns.Add("Fecha", typeof(DateTime));
                return expected;
            }
        }

        protected override string specificScript
        {
            get 
            {
                return "Scripts/EntryFoodTest.sql";
            }
        }

        [TestInitialize()]
        public void InitTest()
        {
            ResetConn();
            SetPasswd();
            InitBD(specificScript);
        }

        [TestMethod]
        public void TestSelectOne()
        {
            DataRow expected = tableFormat.NewRow();
            expected["FoodType"] = 1;
            expected["QuantityIn"] = 1;
            expected["Fecha"] = "2012/11/20";
            ACAD entry = new CADEntryFood(connectionString);
            List<object> ids = new List<object>();
            ids.Add((int)1);
            DataRow actual = entry.Select(ids);
            Assert.AreEqual(expected["FoodType"], actual["FoodType"]);
            Assert.AreEqual(expected["QuantityIn"], actual["QuantityIn"]);
            Assert.AreEqual(expected["Fecha"], actual["Fecha"]);
        }

        [TestMethod]
        public void TestCount()
        {
            int expected = 4;
            ACAD entry = new CADEntryFood(connectionString);
            int actual = entry.Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestLast()
        {
            ACAD entry = new CADEntryFood(connectionString);
            DataRow actual = entry.Last();
            DataRow expected = tableFormat.NewRow();
            expected["Id"] = 4;
            expected["FoodType"] = 1;
            expected["QuantityIn"] = 3;
            expected["Fecha"] = "2012/11/23";
            Assert.AreEqual(expected["Id"], actual["Id"]);
            Assert.AreEqual(expected["FoodType"], actual["FoodType"]);
            Assert.AreEqual(expected["QuantityIn"], actual["QuantityIn"]);
            Assert.AreEqual(expected["Fecha"], actual["Fecha"]);
        }

        [TestMethod]
        public void SelectAll()
        {
            ACAD entry = new CADEntryFood(connectionString);
            DataTable actual = entry.SelectAll();
            DataTable expected = tableFormat;
            DataRow row = expected.NewRow();
            row["Id"] = 1;
            row["FoodType"] = 1;
            row["QuantityIn"] = 1;
            row["Fecha"] = "2012/11/20";
            DataRow row2 = expected.NewRow();
            row2["Id"] = 2;
            row2["FoodType"] = 1;
            row2["QuantityIn"] = 2;
            row2["Fecha"] = "2012/11/21";
            DataRow row3 = expected.NewRow();
            row3["Id"] = 3;
            row3["FoodType"] = 4;
            row3["QuantityIn"] = 4;
            row3["Fecha"] = "2012/11/22";
            DataRow row4 = expected.NewRow();
            row4["Id"] = 4;
            row4["FoodType"] = 1;
            row4["QuantityIn"] = 3;
            row4["Fecha"] = "2012/11/23";
            expected.Rows.Add(row);
            expected.Rows.Add(row2);
            expected.Rows.Add(row3);
            expected.Rows.Add(row4);
            for (int i = 0; i < expected.Rows.Count; i++)
            {
                Assert.AreEqual(expected.Rows[i]["Id"], actual.Rows[i]["Id"]);
                Assert.AreEqual(expected.Rows[i]["FoodType"], actual.Rows[i]["FoodType"]);
                Assert.AreEqual(expected.Rows[i]["QuantityIn"], actual.Rows[i]["QuantityIn"]);
                Assert.AreEqual(expected.Rows[i]["Fecha"], actual.Rows[i]["Fecha"]);
            }
        }

        [TestMethod]
        public void SelectWhere()
        {
            ACAD entry = new CADEntryFood(connectionString);
            DataTable expected = tableFormat;
            DataRow row = expected.NewRow();
            row["Id"] = 1;
            row["FoodType"] = 1;
            row["QuantityIn"] = 1;
            row["Fecha"] = "2012/11/20";
            expected.Rows.Add(row);
            DataTable actual = entry.SelectWhere("Fecha = '2012/11/20'");

            for (int i = 0; i < expected.Rows.Count; i++)
            {
                Assert.AreEqual(expected.Rows[i]["Id"], actual.Rows[i]["Id"]);
                Assert.AreEqual(expected.Rows[i]["FoodType"], actual.Rows[i]["FoodType"]);
                Assert.AreEqual(expected.Rows[i]["QuantityIn"], actual.Rows[i]["QuantityIn"]);
                Assert.AreEqual(expected.Rows[i]["Fecha"], actual.Rows[i]["Fecha"]);
            }
        }

        [TestMethod]
        public void Insert()
        {
            ACAD entry = new CADEntryFood(connectionString);
            DataRow ins = entry.GetVoidRow;
            ins["FoodType"] = 2;
            ins["QuantityIn"] = 3;
            ins["Fecha"] = "2012/11/24";
            DataRow expected = tableFormat.NewRow();
            expected["Id"] = 5;
            expected["FoodType"] = 2;
            expected["QuantityIn"] = 3;
            expected["Fecha"] = "2012/11/24";
            DataRow actual = entry.Insert(ins);

            Assert.AreEqual(expected["Id"], actual["Id"]);
            Assert.AreEqual(expected["FoodType"], actual["FoodType"]);
            Assert.AreEqual(expected["QuantityIn"], actual["QuantityIn"]);
            Assert.AreEqual(expected["Fecha"], actual["Fecha"]);

        }

        [TestMethod]
        public void Update()
        {
            ACAD entry = new CADEntryFood(connectionString);
            DataRow mod = tableFormat.NewRow();
            mod["Id"] = 2;
            mod["FoodType"] = 2;
            mod["QuantityIn"] = 3;
            mod["Fecha"] = "2012/11/24";
            entry.Update(mod);
        }

        [TestMethod]
        public void Delete()
        {
            ACAD entry = new CADEntryFood(connectionString);
            DataRow del = tableFormat.NewRow();
            del["Id"] = 4;
            del["FoodType"] = 1;
            del["QuantityIn"] = 3;
            del["Fecha"] = "2012/11/23";
            entry.Delete(del);
        }
    }
}
