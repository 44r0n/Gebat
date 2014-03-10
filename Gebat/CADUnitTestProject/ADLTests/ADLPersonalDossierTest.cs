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
    public class ADLPersonalDossierTest : AADLTest
    {
        protected override DataTable tableFormat
        {
            get 
            {
                DataTable expected = new DataTable();
                expected.Columns.Add("Observations", typeof(string));
                return expected;
            }
        }

        private ADL dossier;

        protected override string specificScript
        {
            get 
            {
                return "Scripts/ExpedientePersonalTest.sql";
            }
        }

        [TestInitialize()]
        public void InitTest()
        {
            ResetConn();
            SetPasswd();
            InitBD(specificScript);
            dossier = new ADL(connectionString, "personaldossier", "Id");
        }

        [TestMethod]
        public void TestLast()
        {
            DataRow actual = dossier.Last();
            DataRow expected = tableFormat.NewRow();
            expected["Observations"] = "otra";
            Assert.AreEqual(expected["Observations"], actual["Observations"]);
        }
    }
}
