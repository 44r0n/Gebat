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

        private AADL tbc;

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
            tbc = new ADLTBC(connectionString);
        }

        [TestMethod]
        public void TestSelectOne()
        {
            DataRow expected = testRow(tableFormat.NewRow());
            List<object> ids = new List<object>();
            ids.Add(1);
            DataRow actual = tbc.Select(ids);
            AssertRow(expected, actual);
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
            AADL tbc = new ADLTBC(connectionString);
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
            AADL tbc = new ADLTBC(connectionString);
            DataTable expected = tableFormat;
            DataRow row = testRow(expected.NewRow());
            DataTable actual = tbc.SelectWhere("Court = 'Alicante'");
            for (int i = 0; i < expected.Rows.Count; i++)
            {
                AssertRow(expected.Rows[i], actual.Rows[i]);
            }
        }

        [TestMethod]
        public void Insert()
        {
            AADL tbc = new ADLTBC(connectionString);
            DataRow ins = tbc.GetVoidRow;
            ins["DNI"] = "23456789B";
            ins["Judgement"] = "45/12";
            ins["Court"] = "Court de Albacete";
            ins["BeginDate"] = "2013/02/12";
            ins["FinishDate"] = "2014/04/27";
            ins["NumJourney"] = 60;
            ins["Monday"] = false;
            ins["Tuesday"] = true;
            ins["Wednesday"] = false;
            ins["Thursday"] = true;
            ins["Friday"] = false;
            ins["Saturday"] = true;
            ins["Sunday"] = false;
            ins["Crime"] = 1;
            DataRow expected = tableFormat.NewRow();
            expected["DNI"] = "23456789B";
            expected["Judgement"] = "45/12";
            expected["Court"] = "Court de Albacete";
            expected["BeginDate"] = "2013/02/12";
            expected["FinishDate"] = "2014/04/27";
            expected["NumJourney"] = 60;
            expected["Monday"] = false;
            expected["Tuesday"] = true;
            expected["Wednesday"] = false;
            expected["Thursday"] = true;
            expected["Friday"] = false;
            expected["Saturday"] = true;
            expected["Sunday"] = false;
            expected["Crime"] = 1;
            DataRow actual = tbc.Insert(ins);

            AssertRow(expected, actual);
        }

        [TestMethod]
        public void Update()
        {
            AADL tbc = new ADLTBC(connectionString);
            DataRow mod = tableFormat.NewRow();
            mod["Id"] = 1;
            mod["DNI"] = "12345678A";
            mod["Judgement"] = "23/2013";
            mod["Court"] = "Murcia";
            mod["BeginDate"] = "2013/09/13";
            mod["FinishDate"] = "2014/02/17";
            mod["NumJourney"] = 90;
            mod["Monday"] = false;
            mod["Tuesday"] = true;
            mod["Wednesday"] = false;
            mod["Thursday"] = true;
            mod["Friday"] = false;
            mod["Saturday"] = true;
            mod["Sunday"] = false;
            mod["Crime"] = 1;
            tbc.Update(mod);
        }

        [TestMethod]
        public void Delete()
        {
            AADL tbc = new ADLTBC(connectionString);
            DataRow del = tableFormat.NewRow();
            del["Id"] = 1;
            del["DNI"] = "12345678A";
            del["Judgement"] = "23/2013";
            del["Court"] = "Alicante";
            del["BeginDate"] = "2012/11/24";
            del["FinishDate"] = "2013/03/09";
            del["NumJourney"] = 180;
            del["Monday"] = true;
            del["Tuesday"] = true;
            del["Wednesday"] = true;
            del["Thursday"] = true;
            del["Friday"] = true;
            del["Saturday"] = false;
            del["Sunday"] = false;
            del["Crime"] = 1;
            tbc.Delete(del);
        }
    }
}
