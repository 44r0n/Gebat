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
    public class VIEWFamiliarDataTest : AADLTest
    {

        private VIEW vfamiliar;

        protected override DataTable tableFormat
        {
            get 
            {
                DataTable expected = new DataTable();
                expected.Columns.Add("Id",typeof(int));
                expected.Columns.Add("DNI", typeof(string));
                expected.Columns.Add("Name", typeof(string));
                expected.Columns.Add("Surname", typeof(string));
                expected.Columns.Add("BirthDate", typeof(DateTime));
                expected.Columns.Add("Gender", typeof(string));
                return expected;
            }
        }

        protected override string specificScript
        {
            get 
            {
                return "Scripts/DatosFamiliaresTest.sql";
            }
        }

        private void AssertRow(DataRow expected, DataRow actual)
        {
            Assert.AreEqual(expected["DNI"], actual["DNI"]);
            Assert.AreEqual(expected["Name"], actual["Name"]);
            Assert.AreEqual(expected["Surname"], actual["Surname"]);
            Assert.AreEqual(expected["BirthDate"], actual["BirthDate"]);
            Assert.AreEqual(expected["Gender"], actual["Gender"]);
        }

        private DataRow testRow(DataRow voidRow)
        {
            voidRow["DNI"] = "91071949E";
            voidRow["Name"] = "Jose";
            voidRow["Surname"] = "Logroño";
            voidRow["BirthDate"] = "1972/12/06";
            voidRow["Gender"] = "M";
            return voidRow;
        }

        [TestInitialize()]
        public void InitTest()
        {
            ResetConn();
            SetPasswd();
            InitBD(specificScript);
            vfamiliar = new VIEW(connectionString, "familiardata", "Id");
        }

        [TestMethod]
        public void TestCount()
        {
            int expected = 3;
            int actual = vfamiliar.Count();
            Assert.AreEqual(expected, actual);
        }
    }
}
