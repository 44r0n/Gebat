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
    public class ADLOutgoingFoodTest : AADLTest
    {
        protected override DataTable tableFormat
        {
            get 
            {
                DataTable expected = new DataTable();
                expected.Columns.Add("Id", typeof(int));
                expected.Columns.Add("FoodType", typeof(int));
                expected.Columns.Add("QuantityOut", typeof(int));
                expected.Columns.Add("DateTime", typeof(DateTime));
                return expected;
            }
        }

        protected override string specificScript
        {
            get 
            {
                return "Scripts/OutgoingFoodTest.sql";
            }
        }

        private ADL outgoing;

        private void AssertRow(DataRow expected, DataRow actual)
        {
            Assert.AreEqual(expected["FoodType"], actual["FoodType"]);
            Assert.AreEqual(expected["QuantityOut"], actual["QuantityOut"]);
            Assert.AreEqual(expected["DateTime"], actual["DateTime"]);
        }

        [TestInitialize()]
        public void InitTest()
        {
            ResetConn();
            SetPasswd();
            InitBD(specificScript);
            outgoing = new ADL(connectionString, "outgoingfood", "Id");
        }

        [TestMethod]
        public void TestCount()
        {
            int expected = 3;
            int actual = outgoing.Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestLast()
        {
            DataRow actual = outgoing.Last();
            DataRow expected = tableFormat.NewRow();
            expected["Id"] = 3;
            expected["FoodType"] = 4;
            expected["QuantityOut"] = 2;
            expected["DateTime"] = "2012/11/25";
            AssertRow(expected, actual);
        }

        [TestMethod]
        public void SelectAll()
        {
            DataTable actual = outgoing.SelectAll();
            DataTable expected = tableFormat;
            DataRow row = expected.NewRow();
            row["Id"] = 1;
            row["FoodType"] = 1;
            row["QuantityOut"] = 1;
            row["DateTime"] = "2012/11/24";
            DataRow row2 = expected.NewRow();
            row2["Id"] = 2;
            row2["FoodType"] = 1;
            row2["QuantityOut"] = 1;
            row2["DateTime"] = "2012/11/24";
            DataRow row3 = expected.NewRow();
            row3["Id"] = 3;
            row3["FoodType"] = 4;
            row3["QuantityOut"] = 2;
            row3["DateTime"] = "2012/11/25";
            expected.Rows.Add(row);
            expected.Rows.Add(row2);
            expected.Rows.Add(row3);
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
            row["Id"] = 3;
            row["FoodType"] = 4;
            row["QuantityOut"] = 2;
            row["DateTime"] = "2012/11/25";
            expected.Rows.Add(row);
            DataTable actual = outgoing.Select("Select * FROM outgoingfood WHERE DateTime = @DateTime","2012/11/25");

            for (int i = 0; i < expected.Rows.Count; i++)
            {
                AssertRow(expected.Rows[i], actual.Rows[i]);
            }
        }
    }
}
