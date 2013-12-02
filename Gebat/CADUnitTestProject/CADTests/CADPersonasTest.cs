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
    public class CADPersonasTest : ACADTest
    {
        protected override DataTable tableFormat
        {
            get 
            {
                DataTable expected = new DataTable();
                expected.Columns.Add("DNI", typeof(string));
                expected.Columns.Add("Nombre", typeof(string));
                expected.Columns.Add("Apellidos", typeof(string));
                return expected;
            }
        }

        protected override string specificScript
        {
            get 
            {
                return "PersonasTest.sql";
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
            ACAD persona = new CADPersonas(connectionString);
            List<object> ids = new List<object>();
            ids.Add(1);
            DataRow actual = persona.Select(ids);
            Assert.AreEqual(expected["DNI"], actual["DNI"]);
            Assert.AreEqual(expected["Nombre"], actual["Nombre"]);
            Assert.AreEqual(expected["Apellidos"], actual["Apellidos"]);
        }

        [TestMethod]
        public void TestCount()
        {
            int expected = 2;
            ACAD personas = new CADPersonas(connectionString);
            int actual = personas.Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void TestCountConnFail()
        {
            setFailConn();
            ACAD persona = new CADPersonas(connectionString);
            persona.Count();
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void TestLastConnFail()
        {
            setFailConn();
            ACAD persona = new CADPersonas(connectionString);
            persona.Last();
        }

        [TestMethod]
        public void TestLast()
        {
            ACAD persona = new CADPersonas(connectionString);
            DataRow actual = persona.Last();
            DataRow expected = tableFormat.NewRow();
            expected["DNI"] = "23456789B";
            expected["Nombre"] = "Ana";
            expected["Apellidos"] = "Entrepinares";
            Assert.AreEqual(expected["DNI"], actual["DNI"]);
            Assert.AreEqual(expected["Nombre"], actual["Nombre"]);
            Assert.AreEqual(expected["Apellidos"], actual["Apellidos"]);
        }

        [TestMethod]
        public void SelectAll()
        {
            ACAD persona = new CADPersonas(connectionString);
            DataTable actual = persona.SelectAll();
            DataTable expected = tableFormat;
            DataRow row = expected.NewRow();
            row["DNI"] = "12345678A";
            row["Nombre"] = "Pepe";
            row["Apellidos"] = "Olivares";
            DataRow row2 = expected.NewRow();
            row2["DNI"] = "23456789B";
            row2["Nombre"] = "Ana";
            row2["Apellidos"] = "Entrepinares";
            expected.Rows.Add(row);
            expected.Rows.Add(row2);
            for (int i = 0; i < expected.Rows.Count; i++)
            {
                Assert.AreEqual(expected.Rows[i]["DNI"], actual.Rows[i]["DNI"]);
                Assert.AreEqual(expected.Rows[i]["Nombre"], actual.Rows[i]["Nombre"]);
                Assert.AreEqual(expected.Rows[i]["Apellidos"], actual.Rows[i]["Apellidos"]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void SelectAllFailConn()
        {
            setFailConn();
            ACAD persona = new CADPersonas(connectionString);
            persona.SelectAll();
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void SelectVoidList()
        {
            ACAD persona = new CADPersonas(connectionString);
            persona.Select(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNumberIdException))]
        public void SelectInvalidNumberId()
        {
            List<object> ids = new List<object>();
            ids.Add("hola");
            ids.Add(7);
            ACAD personas = new CADPersonas(connectionString);
            personas.Select(ids);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void SelectConnFail()
        {
            setFailConn();
            ACAD persona = new CADPersonas(connectionString);
            List<object> ids = new List<object>();
            ids.Add("12345678A");
            persona.Select(ids);
        }

        [TestMethod]
        public void SelectWhere()
        {
            ACAD personas = new CADPersonas(connectionString);
            DataTable expected = tableFormat;
            DataRow row = expected.NewRow();
            row["DNI"] = "12345678A";
            row["Nombre"] = "Pepe";
            row["Apellidos"] = "Olivares";
            expected.Rows.Add(row);
            DataTable actual = personas.SelectWhere("Nombre = 'Pepe'");
            for (int i = 0; i < expected.Rows.Count; i++)
            {
                Assert.AreEqual(expected.Rows[i]["DNI"], actual.Rows[i]["DNI"]);
                Assert.AreEqual(expected.Rows[i]["Nombre"], actual.Rows[i]["Nombre"]);
                Assert.AreEqual(expected.Rows[i]["Apellidos"], actual.Rows[i]["Apellidos"]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidStartRecordException))]
        public void SelectWhereInvalidStart()
        {
            ACAD persona = new CADPersonas(connectionString);
            persona.SelectWhere("Nombre = 'Pepe'", -9);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void SelectWhereInvalidStatement()
        {
            ACAD persona = new CADPersonas(connectionString);
            persona.SelectWhere(";");
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void SelectWhereFailConn()
        {
            setFailConn();
            ACAD persona = new CADPersonas(connectionString);
            persona.SelectWhere("Nombre = 'Pepe'");
        }

        [TestMethod]
        public void Insert()
        {
            ACAD persona = new CADPersonas(connectionString);
            DataRow ins = persona.GetVoidRow;
            ins["DNI"] = "34567890C";
            ins["Nombre"] = "Antonio";
            ins["Apellidos"] = "García";
            DataRow expected = tableFormat.NewRow();
            expected["DNI"] = "34567890C";
            expected["Nombre"] = "Antonio";
            expected["Apellidos"] = "García";
            DataRow actual = persona.Insert(ins);

            Assert.AreEqual(expected["DNI"], actual["DNI"]);
            Assert.AreEqual(expected["Nombre"], actual["Nombre"]);
            Assert.AreEqual(expected["Apellidos"], actual["Apellidos"]);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void InsertNullRow()
        {
            ACAD persona = new CADPersonas(connectionString);
            persona.Insert(null);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void InsertFailConn()
        {
            setFailConn();
            ACAD persona = new CADPersonas(connectionString);
            DataRow ins = persona.GetVoidRow;
            ins["DNI"] = "34567890C";
            ins["Nombre"] = "Antonio";
            ins["Apellidos"] = "García";
            persona.Insert(ins);
        }

        [TestMethod]
        public void Update()
        {
            ACAD persona = new CADPersonas(connectionString);
            DataRow mod = tableFormat.NewRow();
            mod["DNI"] = "23456789B";
            mod["Nombre"] = "María";
            mod["Apellidos"] = "Entrepinares";
            persona.Update(mod);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void UpdateNullRow()
        {
            ACAD persona = new CADPersonas(connectionString);
            persona.Update(null);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void UpdateFailConn()
        {
            setFailConn();
            ACAD persona = new CADPersonas(connectionString);
            DataRow mod = tableFormat.NewRow();
            mod["DNI"] = "123456789A";
            mod["Nombre"] = "Manolo";
            mod["Apellidos"] = "Solo";
            persona.Update(mod);
        }

        [TestMethod]
        public void Delete()
        {
            ACAD persona = new CADPersonas(connectionString);
            DataRow del = tableFormat.NewRow();
            del["DNI"] = "12345678A";
            del["Nombre"] = "Pepe";
            del["Apellidos"] = "Olivares";
            persona.Delete(del);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void DeleteNullRow()
        {
            ACAD persona = new CADPersonas(connectionString);
            persona.Delete(null);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void DeleteWrongRow()
        {
            ACAD persona = new CADPersonas(connectionString);
            DataRow del = tableFormat.NewRow();
            del["Nombre"] = 2345;
            persona.Delete(del);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void DeleteFailConn()
        {
            setFailConn();
            ACAD persona = new CADPersonas(connectionString);
            DataRow del = tableFormat.NewRow();
            del["DNI"] = "123456789A";
            del["Nombre"] = "Pepe";
            del["Apellidos"] = "Olivares";
            persona.Delete(del);
        }
    }
}
