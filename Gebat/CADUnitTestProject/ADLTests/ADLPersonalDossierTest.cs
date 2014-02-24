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
    public class ADLPersonalDossierTest : AADLTest
    {
        protected override DataTable tableFormat
        {
            get 
            {
                DataTable expected = new DataTable();
                expected.Columns.Add("Income", typeof(int));
                expected.Columns.Add("Observations", typeof(string));
                return expected;
            }
        }

        private AADL dossier;

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
            dossier = new ADLPersonalDossier(connectionString);
        }

        [TestMethod]
        public void TestLast()
        {
            DataRow actual = dossier.Last();
            DataRow expected = tableFormat.NewRow();
            expected["Income"] = 500;
            Assert.AreEqual(expected["Income"], actual["Income"]);
        }
    }
}
