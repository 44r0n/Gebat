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
                expected.Columns.Add("FechaNac", typeof(DateTime));
                expected.Columns.Add("Sexo", typeof(string));
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
                expected.Columns.Add("Delito", typeof(int));
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
            Assert.AreEqual(expected["Nombre"], actual["Nombre"]);
            Assert.AreEqual(expected["Apellidos"], actual["Apellidos"]);
            Assert.AreEqual(expected["FechaNac"], actual["FechaNac"]);
            Assert.AreEqual(expected["Sexo"], actual["Sexo"]);
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
            Assert.AreEqual(expected["Delito"], actual["Delito"]);
        }

        private DataRow testRow(DataRow voidRow)
        {
            voidRow["DNI"] = "12345678A";
            voidRow["Nombre"] = "Pepe";
            voidRow["Apellidos"] = "Olivares";
            voidRow["FechaNac"] = "1976/04/02";
            voidRow["Sexo"] = "M";
            voidRow["Ejecutoria"] = "23/2013";
            voidRow["Juzgado"] = "Alicante";
            voidRow["FInicio"] = "2012/11/24";
            voidRow["FFin"] = "2013/03/09";
            voidRow["NumJornadas"] = 180;
            voidRow["Lunes"] = true;
            voidRow["Martes"] = true;
            voidRow["Miercoles"] = true;
            voidRow["Jueves"] = true;
            voidRow["Viernes"] = true;
            voidRow["Sabado"] = false;
            voidRow["Domingo"] = false;
            voidRow["Delito"] = 1;
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
            DataTable actual = vtbc.SelectWhere("Juzgado = 'Alicante'");
            for (int i = 0; i < expected.Rows.Count; i++)
            {
                AssertRow(expected.Rows[i], actual.Rows[i]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidStartRecordException))]
        public void SelectWhereInvalidStart()
        {
            vtbc.SelectWhere("Juzgado = 'Alicante'", -8);
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
            vtbc.SelectWhere("Juzgado = 'Alicante'");
        }
    }
}
