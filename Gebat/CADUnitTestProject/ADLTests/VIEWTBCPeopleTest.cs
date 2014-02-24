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
    public class VIEWTBCPeopleTest : AADLTest
    {
        protected override DataTable tableFormat
        {
            get 
            {
                DataTable expected = new DataTable();
                expected.Columns.Add("Id", typeof(int));
                expected.Columns.Add("DNI", typeof(string));
                expected.Columns.Add("Name", typeof(string));
                expected.Columns.Add("Surname", typeof(string));
                expected.Columns.Add("BirthDate", typeof(DateTime));
                expected.Columns.Add("Gender", typeof(string));
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

        protected override string specificScript
        {
            get 
            {
                return "Scripts/TBCPeopleTest.sql";
            }
        }

        private AVIEW vtbc;

        private void AssertRow(DataRow expected, DataRow actual)
        {
            Assert.AreEqual(expected["DNI"], actual["DNI"]);
            Assert.AreEqual(expected["Name"], actual["Name"]);
            Assert.AreEqual(expected["Surname"], actual["Surname"]);
            Assert.AreEqual(expected["BirthDate"], actual["BirthDate"]);
            Assert.AreEqual(expected["Gender"], actual["Gender"]);
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
            voidRow["Name"] = "Pepe";
            voidRow["Surname"] = "Olivares";
            voidRow["BirthDate"] = "1976/04/02";
            voidRow["Gender"] = "M";
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

        [TestInitialize()]
        public void InitTest()
        {
            ResetConn();
            SetPasswd();
            InitBD(specificScript);
            vtbc = new VIEWTBCPeople(connectionString);
        }

        [TestMethod]
        public void TestSelectOne()
        {
            DataRow expected = testRow(tableFormat.NewRow());
            List<object> ids = new List<object>();
            ids.Add(1);
            DataRow actual = vtbc.Select(ids);
            AssertRow(expected, actual);
        }

        [TestMethod]
        public void TestCount()
        {
            int expected = 1;
            int actual = vtbc.Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void TestCountFail()
        {
            setFailConn();
            vtbc.Count();
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void TestLastConnFail()
        {
            setFailConn();
            vtbc.Last();
        }

        [TestMethod]
        public void TestLast()
        {
            DataRow actual = vtbc.Last();
            DataRow expected = testRow(tableFormat.NewRow());
            AssertRow(expected, actual);
        }

        [TestMethod]
        public void SelectAll()
        {
            DataTable actual = vtbc.SelectAll();
            DataTable expected = tableFormat;
            DataRow row = testRow(expected.NewRow());
            for (int i = 0; i < expected.Rows.Count; i++)
            {
                AssertRow(expected.Rows[i], actual.Rows[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void SelectAllFailConn()
        {
            setFailConn();
            vtbc.SelectAll();
        }

        [TestMethod]
        public void SelectWhere()
        {
            DataTable expected = tableFormat;
            DataRow row = testRow(expected.NewRow());
            expected.Rows.Add(row);
            DataTable actual = vtbc.SelectWhere("Court = 'Alicante'");
            for (int i = 0; i < expected.Rows.Count; i++)
            {
                AssertRow(expected.Rows[i], actual.Rows[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidStartRecordException))]
        public void SelectWhereInvalidStart()
        {
            vtbc.SelectWhere("Court = 'Alicante'", -8);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void SelectWhereInvalidStatement()
        {
            vtbc.SelectWhere("``´´");
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void SelectWhereFailConn()
        {
            setFailConn();
            vtbc.SelectWhere("Court = 'Alicante'");
        }
    }
}
