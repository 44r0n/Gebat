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
    public class ADLFamiliarTest : AADLTest
    {
        protected override DataTable tableFormat
        {
            get 
            {
                DataTable expected = new DataTable();
                expected.Columns.Add("DNI", typeof(string));
                return expected;
            }
        }

        private AADL familiar;

        protected override string specificScript
        {
            get 
            {
                return "Scripts/DatosFamiliaresTest.sql";
            }
        }

        [TestInitialize()]
        public void InitTest()
        {
            ResetConn();
            SetPasswd();
            InitBD(specificScript);
            familiar = new ADLFamiliars(connectionString);
        }

        [TestMethod]
        public void TestLast()
        {
            DataRow actual = familiar.Last();
            DataRow expected = tableFormat.NewRow();
            expected["DNI"] = "29556003Z";
            Assert.AreEqual(expected["DNI"], actual["DNI"]);
        }
    }
}
