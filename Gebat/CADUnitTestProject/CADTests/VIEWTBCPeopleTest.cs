using System;
using System.IO;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using SqlManager;
using GebatCAD.Classes;
using GebatCAD.Exceptions;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CADUnitTestProject.CADTests
{
    [TestClass]
    public class VIEWTBCPeopleTest : ACADTest
    {
        protected override DataTable tableFormat
        {
            get 
            {
                DataTable expected = new DataTable();
                expected.Columns.Add("DNI", typeof(string));
                expected.Columns.Add("Nombre", typeof(string));
                expected.Columns.Add("Apellidos", typeof(string));
                expected.Columns.Add("Ejecutoria", typeof(string));
                expected.Columns.Add("Juzgado", typeof(string));
                expected.Columns.Add("FInicio", typeof(DateTime));
                expected.Columns.Add("FFin", typeof(DateTime));
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

        [TestInitialize()]
        public void InitTest()
        {
            ResetConn();
            SetPasswd();
            InitBD(specificScript);
        }

        [TestMethod]
        public void TestCount()
        {
            int expected = 1;
            AVIEW vtbc = new VIEWTBCPeople(connectionString);
            int actual = vtbc.Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void TestCountFail()
        {
            setFailConn();
            AVIEW vtbc = new VIEWTBCPeople(connectionString);
            vtbc.Count();
        }

        [TestMethod]
        public void SelectAll()
        {
            AVIEW vtbc = new VIEWTBCPeople(connectionString);
            DataTable actual = vtbc.SelectAll();
            DataTable expected = tableFormat;
            DataRow row = expected.NewRow();
            row["DNI"] = "12345678A";
            row["Nombre"] = "Pepe";
            row["Apellidos"] = "Olivares";
            row["Ejecutoria"] = "23/2013";
            row["Juzgado"] = "Alicante";
            row["FInicio"] = "2012/11/24";
            row["FFin"] = "2013/03/09";
            for (int i = 0; i < expected.Rows.Count; i++)
            {
                Assert.AreEqual(expected.Rows[i]["DNI"], actual.Rows[i]["DNI"]);
                Assert.AreEqual(expected.Rows[i]["Nombre"],actual.Rows[i]["Nombre"]);
                Assert.AreEqual(expected.Rows[i]["Apelldidos"],actual.Rows[i]["Apellidos"]);
                Assert.AreEqual(expected.Rows[i]["Ejecutoria"], actual.Rows[i]["Ejecutoria"]);
                Assert.AreEqual(expected.Rows[i]["Juzgado"], actual.Rows[i]["Juzgado"]);
                Assert.AreEqual(expected.Rows[i]["FInicio"], actual.Rows[i]["FInicio"]);
                Assert.AreEqual(expected.Rows[i]["FFin"], actual.Rows[i]["FFin"]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void SelectAllFailConn()
        {
            setFailConn();
            AVIEW vtbc = new VIEWTBCPeople(connectionString);
            vtbc.SelectAll();
        }

        [TestMethod]
        public void SelectWhere()
        {
            AVIEW vtbc = new VIEWTBCPeople(connectionString);
            DataTable expected = tableFormat;
            DataRow row = expected.NewRow();
            row["DNI"] = "12345678A";
            row["Nombre"] = "Pepe";
            row["Apellidos"] = "Olivares";
            row["Ejecutoria"] = "23/2013";
            row["Juzgado"] = "Alicante";
            row["FInicio"] = "2012/11/24";
            row["FFin"] = "2013/03/09";
            expected.Rows.Add(row);
            DataTable actual = vtbc.SelectWhere("Juzgado = 'Alicante'");
            for (int i = 0; i < expected.Rows.Count; i++)
            {
                Assert.AreEqual(expected.Rows[i]["DNI"], actual.Rows[i]["DNI"]);
                Assert.AreEqual(expected.Rows[i]["Nombre"], actual.Rows[i]["Nombre"]);
                Assert.AreEqual(expected.Rows[i]["Apellidos"], actual.Rows[i]["Apellidos"]);
                Assert.AreEqual(expected.Rows[i]["Ejecutoria"], actual.Rows[i]["Ejecutoria"]);
                Assert.AreEqual(expected.Rows[i]["Juzgado"], actual.Rows[i]["Juzgado"]);
                Assert.AreEqual(expected.Rows[i]["FInicio"], actual.Rows[i]["FInicio"]);
                Assert.AreEqual(expected.Rows[i]["FFin"], actual.Rows[i]["FFin"]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidStartRecordException))]
        public void SelectWhereInvalidStart()
        {
            AVIEW vtbc = new VIEWTBCPeople(connectionString);
            vtbc.SelectWhere("Juzgado = 'Alicante'", -8);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void SelectWhereInvalidStatement()
        {
            AVIEW vtbc = new VIEWTBCPeople(connectionString);
            vtbc.SelectWhere("``´´");
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void SelectWhereFailConn()
        {
            setFailConn();
            AVIEW vtbc = new VIEWTBCPeople(connectionString);
            vtbc.SelectWhere("Juzgado = 'Alicante'");
        }
    }
}
