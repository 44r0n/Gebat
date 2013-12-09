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
                expected.Columns.Add("Id", typeof(int));
                expected.Columns.Add("DNI", typeof(string));
                expected.Columns.Add("Nombre", typeof(string));
                expected.Columns.Add("Apellidos", typeof(string));
                expected.Columns.Add("Ejecutoria", typeof(string));
                expected.Columns.Add("Juzgado", typeof(string));
                expected.Columns.Add("FInicio", typeof(DateTime));
                expected.Columns.Add("FFin", typeof(DateTime));
                expected.Columns.Add("NumJornadas", typeof(int));
                expected.Columns.Add("Lunes", typeof(bool));
                expected.Columns.Add("Martes", typeof(bool));
                expected.Columns.Add("Miercoles", typeof(bool));
                expected.Columns.Add("Jueves", typeof(bool));
                expected.Columns.Add("Viernes", typeof(bool));
                expected.Columns.Add("Sabado", typeof(bool));
                expected.Columns.Add("Domingo", typeof(bool));
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
        public void TestSelectOne()
        {
            DataRow expected = tableFormat.NewRow();
            expected["DNI"] = "12345678A";
            expected["Nombre"] = "Pepe";
            expected["Apellidos"] = "Olivares";
            expected["Ejecutoria"] = "23/2013";
            expected["Juzgado"] = "Alicante";
            expected["FInicio"] = "2012/11/24";
            expected["FFin"] = "2013/03/09";
            expected["NumJornadas"] = 180;
            expected["Lunes"] = true;
            expected["Martes"] = true;
            expected["Miercoles"] = true;
            expected["Jueves"] = true;
            expected["Viernes"] = true;
            expected["Sabado"] = false;
            expected["Domingo"] = false;

            AVIEW vtbc = new VIEWTBCPeople(connectionString);
            List<object> ids = new List<object>();
            ids.Add(1);
            DataRow actual = vtbc.Select(ids);
            Assert.AreEqual(expected["DNI"], actual["DNI"]);
            Assert.AreEqual(expected["Nombre"], actual["Nombre"]);
            Assert.AreEqual(expected["Apellidos"], actual["Apellidos"]);
            Assert.AreEqual(expected["Ejecutoria"], actual["Ejecutoria"]);
            Assert.AreEqual(expected["Juzgado"], actual["Juzgado"]);
            Assert.AreEqual(expected["FInicio"], actual["FInicio"]);
            Assert.AreEqual(expected["FFin"], actual["FFin"]);
            Assert.AreEqual(expected["NumJornadas"], actual["NumJornadas"]);
            Assert.AreEqual(expected["Lunes"], actual["Lunes"]);
            Assert.AreEqual(expected["Martes"], actual["Martes"]);
            Assert.AreEqual(expected["Miercoles"], actual["Miercoles"]);
            Assert.AreEqual(expected["Jueves"], actual["Jueves"]);
            Assert.AreEqual(expected["Viernes"], actual["Viernes"]);
            Assert.AreEqual(expected["Sabado"], actual["Sabado"]);
            Assert.AreEqual(expected["Domingo"], actual["Domingo"]);
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
        [ExpectedException(typeof(MySqlException))]
        public void TestLastConnFail()
        {
            setFailConn();
            AVIEW vtbc = new VIEWTBCPeople(connectionString);
            vtbc.Last();
        }

        [TestMethod]
        public void TestLast()
        {
            AVIEW vtbc = new VIEWTBCPeople(connectionString);
            DataRow actual = vtbc.Last();
            DataRow expected = tableFormat.NewRow();
            expected["DNI"] = "12345678A";
            expected["Nombre"] = "Pepe";
            expected["Apellidos"] = "Olivares";
            expected["Ejecutoria"] = "23/2013";
            expected["Juzgado"] = "Alicante";
            expected["FInicio"] = "2012/11/24";
            expected["FFin"] = "2013/03/09";
            expected["NumJornadas"] = 180;
            expected["Lunes"] = true;
            expected["Martes"] = true;
            expected["Miercoles"] = true;
            expected["Jueves"] = true;
            expected["Viernes"] = true;
            expected["Sabado"] = false;
            expected["Domingo"] = false;
            Assert.AreEqual(expected["DNI"], actual["DNI"]);
            Assert.AreEqual(expected["Nombre"], actual["Nombre"]);
            Assert.AreEqual(expected["Apellidos"], actual["Apellidos"]);
            Assert.AreEqual(expected["Ejecutoria"], actual["Ejecutoria"]);
            Assert.AreEqual(expected["Juzgado"], actual["Juzgado"]);
            Assert.AreEqual(expected["FInicio"], actual["FInicio"]);
            Assert.AreEqual(expected["FFin"], actual["FFin"]);
            Assert.AreEqual(expected["NumJornadas"], actual["NumJornadas"]);
            Assert.AreEqual(expected["Lunes"], actual["Lunes"]);
            Assert.AreEqual(expected["Martes"], actual["Martes"]);
            Assert.AreEqual(expected["Miercoles"], actual["Miercoles"]);
            Assert.AreEqual(expected["Jueves"], actual["Jueves"]);
            Assert.AreEqual(expected["Viernes"], actual["Viernes"]);
            Assert.AreEqual(expected["Sabado"], actual["Sabado"]);
            Assert.AreEqual(expected["Domingo"], actual["Domingo"]);
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
            row["Lunes"] = true;
            row["Martes"] = true;
            row["Miercoles"] = true;
            row["Jueves"] = true;
            row["Viernes"] = true;
            row["Sabado"] = true;
            row["Domingo"] = true;
            for (int i = 0; i < expected.Rows.Count; i++)
            {
                Assert.AreEqual(expected.Rows[i]["DNI"], actual.Rows[i]["DNI"]);
                Assert.AreEqual(expected.Rows[i]["Nombre"],actual.Rows[i]["Nombre"]);
                Assert.AreEqual(expected.Rows[i]["Apelldidos"],actual.Rows[i]["Apellidos"]);
                Assert.AreEqual(expected.Rows[i]["Ejecutoria"], actual.Rows[i]["Ejecutoria"]);
                Assert.AreEqual(expected.Rows[i]["Juzgado"], actual.Rows[i]["Juzgado"]);
                Assert.AreEqual(expected.Rows[i]["FInicio"], actual.Rows[i]["FInicio"]);
                Assert.AreEqual(expected.Rows[i]["FFin"], actual.Rows[i]["FFin"]);
                Assert.AreEqual(expected.Rows[i]["Lunes"], actual.Rows[i]["Lunes"]);
                Assert.AreEqual(expected.Rows[i]["Martes"], actual.Rows[i]["Martes"]);
                Assert.AreEqual(expected.Rows[i]["Miercoles"], actual.Rows[i]["Miercoles"]);
                Assert.AreEqual(expected.Rows[i]["Jueves"], actual.Rows[i]["Jueves"]);
                Assert.AreEqual(expected.Rows[i]["Viernes"], actual.Rows[i]["Viernes"]);
                Assert.AreEqual(expected.Rows[i]["Sabado"], actual.Rows[i]["Sabado"]);
                Assert.AreEqual(expected.Rows[i]["Domingo"], actual.Rows[i]["Domingo"]);
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
            row["Lunes"] = true;
            row["Martes"] = true;
            row["Miercoles"] = true;
            row["Jueves"] = true;
            row["Viernes"] = true;
            row["Sabado"] = false;
            row["Domingo"] = false;
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
                Assert.AreEqual(expected.Rows[i]["Lunes"], actual.Rows[i]["Lunes"]);
                Assert.AreEqual(expected.Rows[i]["Martes"], actual.Rows[i]["Martes"]);
                Assert.AreEqual(expected.Rows[i]["Miercoles"], actual.Rows[i]["Miercoles"]);
                Assert.AreEqual(expected.Rows[i]["Jueves"], actual.Rows[i]["Jueves"]);
                Assert.AreEqual(expected.Rows[i]["Viernes"], actual.Rows[i]["Viernes"]);
                Assert.AreEqual(expected.Rows[i]["Sabado"], actual.Rows[i]["Sabado"]);
                Assert.AreEqual(expected.Rows[i]["Domingo"], actual.Rows[i]["Domingo"]);
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
