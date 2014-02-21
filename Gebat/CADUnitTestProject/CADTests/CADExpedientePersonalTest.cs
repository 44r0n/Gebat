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
    public class CADExpedientePersonalTest : ACADTest
    {
        protected override DataTable tableFormat
        {
            get 
            {
                DataTable expected = new DataTable();
                expected.Columns.Add("Ingresos", typeof(int));
                expected.Columns.Add("Observaciones", typeof(string));
                return expected;
            }
        }

        private AADL Expediente;

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
            Expediente = new ADLPersonalDosier(connectionString);
        }

        [TestMethod]
        public void TestLast()
        {
            DataRow actual = Expediente.Last();
            DataRow expected = tableFormat.NewRow();
            expected["Ingresos"] = 500;
            Assert.AreEqual(expected["Ingresos"], actual["Ingresos"]);
        }
    }
}
