using System;
using System.Data;
using System.Collections.Generic;
using GebatCAD.Classes;
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
                expected.Columns.Add("Id", typeof(int));
                expected.Columns.Add("DNI", typeof(string));
                expected.Columns.Add("Nombre", typeof(string));
                expected.Columns.Add("Apellidos", typeof(string));
                expected.Columns.Add("FechaNac", typeof(DateTime));
                expected.Columns.Add("Sexo", typeof(string));
                return expected;
            }
        }

        private AADL persona;

        private void AssertRows(DataRow expected, DataRow actual)
        {
            Assert.AreEqual(expected["DNI"], actual["DNI"]);
            Assert.AreEqual(expected["Nombre"], actual["Nombre"]);
            Assert.AreEqual(expected["Apellidos"], actual["Apellidos"]);
            Assert.AreEqual(expected["FechaNac"], actual["FechaNac"]);
            Assert.AreEqual(expected["Sexo"], actual["Sexo"]);
        }

        private DataRow testRow(DataRow voidRow)
        {
            voidRow["DNI"] = "12345678A";
            voidRow["Nombre"] = "Pepe";
            voidRow["Apellidos"] = "Olivares";
            voidRow["FechaNac"] = "1976/04/02";
            voidRow["Sexo"] = "M";
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
            persona = new CADPersonas(connectionString);
        }

        [TestMethod]
        public void TestSelectOne()
        {
            DataRow expected = testRow(tableFormat.NewRow());
            List<object> ids = new List<object>();
            ids.Add(1);
            DataRow actual = persona.Select(ids);
            AssertRows(expected, actual);
        }

        [TestMethod]
        public void TestCount()
        {
            int expected = 2;
            int actual = persona.Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestLast()
        {
            DataRow actual = persona.Last();
            DataRow expected = tableFormat.NewRow();
            expected["DNI"] = "23456789B";
            expected["Nombre"] = "Ana";
            expected["Apellidos"] = "Entrepinares";
            expected["FechaNac"] = "1988/07/11";
            expected["Sexo"] = "F";
            AssertRows(expected, actual);
        }

        [TestMethod]
        public void SelectAll()
        {
            DataTable actual = persona.SelectAll();
            DataTable expected = tableFormat;
            DataRow row = expected.NewRow();
            row["DNI"] = "12345678A";
            row["Nombre"] = "Pepe";
            row["Apellidos"] = "Olivares";
            row["FechaNac"] = "1976/04/02";
            row["Sexo"] = "M";
            DataRow row2 = expected.NewRow();
            row2["DNI"] = "23456789B";
            row2["Nombre"] = "Ana";
            row2["Apellidos"] = "Entrepinares";
            row2["FechaNac"] = "1988/07/11";
            row2["Sexo"] = "F";
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
            DataTable actual = persona.SelectWhere("Nombre = 'Pepe'");
            for (int i = 0; i < expected.Rows.Count; i++)
            {
                AssertRows(expected.Rows[i], actual.Rows[i]);
            }
        }

        [TestMethod]
        public void Insert()
        {
            DataRow ins = persona.GetVoidRow;
            ins["DNI"] = "34567890C";
            ins["Nombre"] = "Antonio";
            ins["Apellidos"] = "García";
            ins["FechaNAc"] = "1982/03/12";
            ins["Sexo"] = "M";
            DataRow expected = tableFormat.NewRow();
            expected["DNI"] = "34567890C";
            expected["Nombre"] = "Antonio";
            expected["Apellidos"] = "García";
            expected["FechaNAc"] = "1982/03/12";
            expected["Sexo"] = "M";
            DataRow actual = persona.Insert(ins);
            AssertRows(expected, actual);
        }

        [TestMethod]
        public void Update()
        {
            DataRow mod = tableFormat.NewRow();
            mod["Id"] = 2;
            mod["DNI"] = "23456789B";
            mod["Nombre"] = "María";
            mod["Apellidos"] = "Entrepinares";
            mod["FechaNac"] = "1954/02/02";
            mod["Sexo"] = "F";
            persona.Update(mod);
        }

        [TestMethod]
        public void Delete()
        {
            DataRow del = tableFormat.NewRow();
            del["Id"] = 1;
            del["DNI"] = "12345678A";
            del["Nombre"] = "Pepe";
            del["Apellidos"] = "Olivares";
            del["FechaNac"] = "1976/04/02";
            del["Sexo"] = "M";
            persona.Delete(del);
        }
    }
}
