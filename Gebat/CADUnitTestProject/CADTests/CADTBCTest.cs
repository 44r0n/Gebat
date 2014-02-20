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
    public class CADTBCTest : ACADTest
    {
        protected override DataTable tableFormat
        {
            get 
            {
                DataTable expected = new DataTable();
                expected.Columns.Add("Id", typeof(int));
                expected.Columns.Add("DNI", typeof(string));
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

        private void AssertRow(DataRow expected, DataRow actual)
        {
            Assert.AreEqual(expected["DNI"], actual["DNI"]);
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

        private AADL tbc;

        protected override string specificScript
        {
            get 
            {
                return "Scripts/TBCTest.sql";
            }
        }

        [TestInitialize()]
        public void InitTest()
        {
            ResetConn();
            SetPasswd();
            InitBD(specificScript);
            tbc = new CADTBC(connectionString);
        }

        [TestMethod]
        public void TestSelectOne()
        {
            DataRow expected = testRow(tableFormat.NewRow());
            List<object> ids = new List<object>();
            ids.Add(1);
            DataRow actual = tbc.Select(ids);
            AssertRow(expected, actual);
        }

        [TestMethod]
        public void TestCount()
        {
            int expected = 1;
            int actual = tbc.Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestLast()
        {
            DataRow actual = tbc.Last();
            DataRow expected = testRow(tableFormat.NewRow());
            AssertRow(expected, actual);
        }

        [TestMethod]
        public void SelectAll()
        {
            AADL tbc = new CADTBC(connectionString);
            DataTable actual = tbc.SelectAll();
            DataTable expected = tableFormat;
            DataRow row = testRow(expected.NewRow());
            for (int i = 0; i < expected.Rows.Count; i++)
            {
                AssertRow(expected.Rows[i], actual.Rows[i]);
            }
        }

        [TestMethod]
        public void SelectWhere()
        {
            AADL tbc = new CADTBC(connectionString);
            DataTable expected = tableFormat;
            DataRow row = testRow(expected.NewRow());
            DataTable actual = tbc.SelectWhere("Juzgado = 'Alicante'");
            for (int i = 0; i < expected.Rows.Count; i++)
            {
                AssertRow(expected.Rows[i], actual.Rows[i]);
            }
        }

        [TestMethod]
        public void Insert()
        {
            AADL tbc = new CADTBC(connectionString);
            DataRow ins = tbc.GetVoidRow;
            ins["DNI"] = "23456789B";
            ins["Ejecutoria"] = "45/12";
            ins["Juzgado"] = "Juzgado de Albacete";
            ins["FInicio"] = "2013/02/12";
            ins["FFin"] = "2014/04/27";
            ins["NumJornadas"] = 60;
            ins["Lunes"] = false;
            ins["Martes"] = true;
            ins["Miercoles"] = false;
            ins["Jueves"] = true;
            ins["Viernes"] = false;
            ins["Sabado"] = true;
            ins["Domingo"] = false;
            ins["Delito"] = 1;
            DataRow expected = tableFormat.NewRow();
            expected["DNI"] = "23456789B";
            expected["Ejecutoria"] = "45/12";
            expected["Juzgado"] = "Juzgado de Albacete";
            expected["FInicio"] = "2013/02/12";
            expected["FFin"] = "2014/04/27";
            expected["NumJornadas"] = 60;
            expected["Lunes"] = false;
            expected["Martes"] = true;
            expected["Miercoles"] = false;
            expected["Jueves"] = true;
            expected["Viernes"] = false;
            expected["Sabado"] = true;
            expected["Domingo"] = false;
            expected["Delito"] = 1;
            DataRow actual = tbc.Insert(ins);

            AssertRow(expected, actual);
        }

        [TestMethod]
        public void Update()
        {
            AADL tbc = new CADTBC(connectionString);
            DataRow mod = tableFormat.NewRow();
            mod["Id"] = 1;
            mod["DNI"] = "12345678A";
            mod["Ejecutoria"] = "23/2013";
            mod["Juzgado"] = "Murcia";
            mod["FInicio"] = "2013/09/13";
            mod["FFin"] = "2014/02/17";
            mod["NumJornadas"] = 90;
            mod["Lunes"] = false;
            mod["Martes"] = true;
            mod["Miercoles"] = false;
            mod["Jueves"] = true;
            mod["Viernes"] = false;
            mod["Sabado"] = true;
            mod["Domingo"] = false;
            mod["Delito"] = 1;
            tbc.Update(mod);
        }

        [TestMethod]
        public void Delete()
        {
            AADL tbc = new CADTBC(connectionString);
            DataRow del = tableFormat.NewRow();
            del["Id"] = 1;
            del["DNI"] = "12345678A";
            del["Ejecutoria"] = "23/2013";
            del["Juzgado"] = "Alicante";
            del["FInicio"] = "2012/11/24";
            del["FFin"] = "2013/03/09";
            del["NumJornadas"] = 180;
            del["Lunes"] = true;
            del["Martes"] = true;
            del["Miercoles"] = true;
            del["Jueves"] = true;
            del["Viernes"] = true;
            del["Sabado"] = false;
            del["Domingo"] = false;
            del["Delito"] = 1;
            tbc.Delete(del);
        }
    }
}
