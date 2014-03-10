using System;
using System.Data;
using System.Collections.Generic;
using GebatCAD.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CADUnitTestProject.ADLTests
{
    [TestClass]
    public class ADLEntryFoodTest : AADLTest
    {
        protected override DataTable tableFormat
        {
            get
            {
                DataTable expected = new DataTable();
                expected.Columns.Add("Id", typeof(int));
                expected.Columns.Add("FoodType", typeof(int));
                expected.Columns.Add("QuantityIn", typeof(int));
                expected.Columns.Add("DateTime", typeof(DateTime));
                return expected;
            }
        }

        private ADL entry;

        private void AssertRow(DataRow expected, DataRow actual)
        {
            Assert.AreEqual(expected["Id"], actual["Id"]);
            Assert.AreEqual(expected["FoodType"], actual["FoodType"]);
            Assert.AreEqual(expected["QuantityIn"], actual["QuantityIn"]);
            Assert.AreEqual(expected["DateTime"], actual["DateTime"]);
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
            entry = new ADL(connectionString, "entryfood", "Id");
        }

        [TestMethod]
        public void TestCount()
        {
            int expected = 4;
            int actual = entry.Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestLast()
        {
            DataRow actual = entry.Last();
            DataRow expected = tableFormat.NewRow();
            expected["Id"] = 4;
            expected["FoodType"] = 1;
            expected["QuantityIn"] = 3;
            expected["DateTime"] = "2012/11/23";
            AssertRow(expected, actual);
        }

        [TestMethod]
        public void SelectAll()
        {
            DataTable actual = entry.SelectAll();
            DataTable expected = tableFormat;
            DataRow row = expected.NewRow();
            row["Id"] = 1;
            row["FoodType"] = 1;
            row["QuantityIn"] = 1;
            row["DateTime"] = "2012/11/20";
            DataRow row2 = expected.NewRow();
            row2["Id"] = 2;
            row2["FoodType"] = 1;
            row2["QuantityIn"] = 2;
            row2["DateTime"] = "2012/11/21";
            DataRow row3 = expected.NewRow();
            row3["Id"] = 3;
            row3["FoodType"] = 4;
            row3["QuantityIn"] = 4;
            row3["DateTime"] = "2012/11/22";
            DataRow row4 = expected.NewRow();
            row4["Id"] = 4;
            row4["FoodType"] = 1;
            row4["QuantityIn"] = 3;
            row4["DateTime"] = "2012/11/23";
            expected.Rows.Add(row);
            expected.Rows.Add(row2);
            expected.Rows.Add(row3);
            expected.Rows.Add(row4);
            for (int i = 0; i < expected.Rows.Count; i++)
            {
                AssertRow(expected.Rows[i], actual.Rows[i]);
            }
        }

        [TestMethod]
        public void SelectWhere()
        {
            DataTable expected = tableFormat;
            DataRow row = expected.NewRow();
            row["Id"] = 1;
            row["FoodType"] = 1;
            row["QuantityIn"] = 1;
            row["DateTime"] = "2012/11/20";
            expected.Rows.Add(row);
            DataTable actual = entry.Select("Select * FROM entryfood Where DateTime = @DateTime","2012/11/20");

            for (int i = 0; i < expected.Rows.Count; i++)
            {
                AssertRow(expected.Rows[i], actual.Rows[i]);
            }
        }
    }
}
