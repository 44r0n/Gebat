using System;
using System.Data;
using System.Collections.Generic;
using GebatCAD.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CADUnitTestProject.ADLTests
{
    [TestClass]
    public class ADLTBCTest : AADLTest
    {
        protected override DataTable tableFormat
        {
            get 
            {
                DataTable expected = new DataTable();
                expected.Columns.Add("Id", typeof(int));
                expected.Columns.Add("DNI", typeof(string));
                expected.Columns.Add("Judgement", typeof(string));
                expected.Columns.Add("Court", typeof(string));
                expected.Columns.Add("BeginDate", typeof(DateTime));
                expected.Columns.Add("FinishDate", typeof(DateTime));
                expected.Columns.Add("NumJourney", typeof(int));
                expected.Columns.Add("Monday", typeof(bool));
                expected.Columns.Add("Tuesday", typeof(bool));
                expected.Columns.Add("Wednesday", typeof(bool));
                expected.Columns.Add("Thursday", typeof(bool));
                expected.Columns.Add("Friday", typeof(bool));
                expected.Columns.Add("Saturday", typeof(bool));
                expected.Columns.Add("Sunday", typeof(bool));
                expected.Columns.Add("Crime", typeof(int));
                return expected;
            }
        }

        private void AssertRow(DataRow expected, DataRow actual)
        {
            Assert.AreEqual(expected["DNI"], actual["DNI"]);
            Assert.AreEqual(expected["Judgement"], actual["Judgement"]);
            Assert.AreEqual(expected["Court"], actual["Court"]);
            Assert.AreEqual(expected["BeginDate"], actual["BeginDate"]);
            Assert.AreEqual(expected["FinishDate"], actual["FinishDate"]);
            Assert.AreEqual(expected["NumJourney"], actual["NumJourney"]);
            Assert.AreEqual(expected["Monday"], actual["Monday"]);
            Assert.AreEqual(expected["Tuesday"], actual["Tuesday"]);
            Assert.AreEqual(expected["Wednesday"], actual["Wednesday"]);
            Assert.AreEqual(expected["Thursday"], actual["Thursday"]);
            Assert.AreEqual(expected["Friday"], actual["Friday"]);
            Assert.AreEqual(expected["Saturday"], actual["Saturday"]);
            Assert.AreEqual(expected["Sunday"], actual["Sunday"]);
            Assert.AreEqual(expected["Crime"], actual["Crime"]);
        }

        private DataRow testRow(DataRow voidRow)
        {
            voidRow["DNI"] = "12345678A";
            voidRow["Judgement"] = "23/2013";
            voidRow["Court"] = "Alicante";
            voidRow["BeginDate"] = "2012/11/24";
            voidRow["FinishDate"] = "2013/03/09";
            voidRow["NumJourney"] = 180;
            voidRow["Monday"] = true;
            voidRow["Tuesday"] = true;
            voidRow["Wednesday"] = true;
            voidRow["Thursday"] = true;
            voidRow["Friday"] = true;
            voidRow["Saturday"] = false;
            voidRow["Sunday"] = false;
            voidRow["Crime"] = 1;
            return voidRow;
        }

        private ADL tbc;

        protected override string specificScript
        {
            get 
            {
                return "Scripts/TBCTest.sql";
            }
        }

        [TestInitialize()]
        public void InitTest()
        {
            ResetConn();
            SetPasswd();
            InitBD(specificScript);
            tbc = new ADL(connectionString, "tbc", "Id");
        }

        [TestMethod]
        public void TestCount()
        {
            int expected = 1;
            int actual = tbc.Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestLast()
        {
            DataRow actual = tbc.Last();
            DataRow expected = testRow(tableFormat.NewRow());
            AssertRow(expected, actual);
        }

        [TestMethod]
        public void SelectAll()
        {
            DataTable actual = tbc.SelectAll();
            DataTable expected = tableFormat;
            DataRow row = testRow(expected.NewRow());
            for (int i = 0; i < expected.Rows.Count; i++)
            {
                AssertRow(expected.Rows[i], actual.Rows[i]);
            }
        }

        [TestMethod]
        public void SelectWhere()
        {
            DataTable expected = tableFormat;
            DataRow row = testRow(expected.NewRow());
            DataTable actual = tbc.Select("SELECT * FROM tbc WHERE Court = @Court","Alicante");
            for (int i = 0; i < expected.Rows.Count; i++)
            {
                AssertRow(expected.Rows[i], actual.Rows[i]);
            }
        }
    }
}
