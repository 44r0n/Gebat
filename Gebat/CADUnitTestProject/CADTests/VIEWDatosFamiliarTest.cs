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
    public class VIEWDatosFamiliarTest : ACADTest
    {

        private AVIEW vfamiliar;

        protected override DataTable tableFormat
        {
            get 
            {
                DataTable expected = new DataTable();
                expected.Columns.Add("Id",typeof(int));
                expected.Columns.Add("DNI", typeof(string));
                expected.Columns.Add("Nombre", typeof(string));
                expected.Columns.Add("Apellidos", typeof(string));
                expected.Columns.Add("FechaNac", typeof(DateTime));
                expected.Columns.Add("Sexo", typeof(string));
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
            Assert.AreEqual(expected["Nombre"], actual["Nombre"]);
            Assert.AreEqual(expected["Apellidos"], actual["Apellidos"]);
            Assert.AreEqual(expected["FechaNac"], actual["FechaNac"]);
            Assert.AreEqual(expected["Sexo"], actual["Sexo"]);
        }

        private DataRow testRow(DataRow voidRow)
        {
            voidRow["DNI"] = "91071949E";
            voidRow["Nombre"] = "Jose";
            voidRow["Apellidos"] = "Logroño";
            voidRow["FechaNac"] = "1972/12/06";
            voidRow["Sexo"] = "M";
            return voidRow;
        }

        [TestInitialize()]
        public void InitTest()
        {
            ResetConn();
            SetPasswd();
            InitBD(specificScript);
            vfamiliar = new VIEWFamiliarData(connectionString);
        }

        [TestMethod]
        public void TestSelectOne()
        {
            DataRow expected = testRow(tableFormat.NewRow());
            List<object> ids = new List<object>();
            ids.Add(2);
            DataRow actual = vfamiliar.Select(ids);
            AssertRow(expected, actual);
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
