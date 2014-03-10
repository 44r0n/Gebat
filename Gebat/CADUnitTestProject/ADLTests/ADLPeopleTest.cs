using System;
using System.Data;
using System.Collections.Generic;
using GebatCAD.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CADUnitTestProject.ADLTests
{
    [TestClass]
    public class ADLPeopleTest : AADLTest
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
                return expected;
            }
        }

        private ADL person;

        private void AssertRows(DataRow expected, DataRow actual)
        {
            Assert.AreEqual(expected["DNI"], actual["DNI"]);
            Assert.AreEqual(expected["Name"], actual["Name"]);
            Assert.AreEqual(expected["Surname"], actual["Surname"]);
            Assert.AreEqual(expected["BirthDate"], actual["BirthDate"]);
            Assert.AreEqual(expected["Gender"], actual["Gender"]);
        }

        private DataRow testRow(DataRow voidRow)
        {
            voidRow["DNI"] = "12345678A";
            voidRow["Name"] = "Pepe";
            voidRow["Surname"] = "Olivares";
            voidRow["BirthDate"] = "1976/04/02";
            voidRow["Gender"] = "M";
            return voidRow;
        }

        protected override string specificScript
        {
            get 
            {
                return "Scripts/PersonasTest.sql";
            }
        }

        [TestInitialize()]
        public void InitTest()
        {
            ResetConn();
            SetPasswd();
            InitBD(specificScript);
            person = new ADL(connectionString, "people", "Id");
        }

        [TestMethod]
        public void TestCount()
        {
            int expected = 2;
            int actual = person.Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestLast()
        {
            DataRow actual = person.Last();
            DataRow expected = tableFormat.NewRow();
            expected["DNI"] = "23456789B";
            expected["Name"] = "Ana";
            expected["Surname"] = "Entrepinares";
            expected["BirthDate"] = "1988/07/11";
            expected["Gender"] = "F";
            AssertRows(expected, actual);
        }

        [TestMethod]
        public void SelectAll()
        {
            DataTable actual = person.SelectAll();
            DataTable expected = tableFormat;
            DataRow row = expected.NewRow();
            row["DNI"] = "12345678A";
            row["Name"] = "Pepe";
            row["Surname"] = "Olivares";
            row["BirthDate"] = "1976/04/02";
            row["Gender"] = "M";
            DataRow row2 = expected.NewRow();
            row2["DNI"] = "23456789B";
            row2["Name"] = "Ana";
            row2["Surname"] = "Entrepinares";
            row2["BirthDate"] = "1988/07/11";
            row2["Gender"] = "F";
            expected.Rows.Add(row);
            expected.Rows.Add(row2);
            for (int i = 0; i < expected.Rows.Count; i++)
            {
                AssertRows(expected.Rows[i], actual.Rows[i]);
            }
        }

        [TestMethod]
        public void SelectWhere()
        {
            DataTable expected = tableFormat;
            DataRow row = testRow(expected.NewRow());
            DataTable actual = person.Select("SELECT * FROM people WHERE Name = @Name","Pepe");
            for (int i = 0; i < expected.Rows.Count; i++)
            {
                AssertRows(expected.Rows[i], actual.Rows[i]);
            }
        }
    }
}
