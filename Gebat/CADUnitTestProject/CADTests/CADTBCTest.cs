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
    public class CADTBCTest : ACADTest
    {
        protected override DataTable tableFormat
        {
            get 
            {
                DataTable expected = new DataTable();
                expected.Columns.Add("DNI", typeof(string));
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
                return "Scripts/TBCTest.sql";
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
            expected["Ejecutoria"] = "23/2013";
            expected["Juzgado"] = "Alicante";
            expected["FInicio"] = "2012/11/24";
            expected["FFin"] = "2013/03/09";
            ACAD tbc = new CADTBC(connectionString);
            List<object> ids = new List<object>();
            ids.Add(1);
            DataRow actual = tbc.Select(ids);
            Assert.AreEqual(expected["DNI"], actual["DNI"]);
            Assert.AreEqual(expected["Ejecutoria"], actual["Ejecutoria"]);
            Assert.AreEqual(expected["Juzgado"], actual["Juzgado"]);
            Assert.AreEqual(expected["FInicio"], actual["FInicio"]);
            Assert.AreEqual(expected["FFin"], actual["FFin"]);
        }

        [TestMethod]
        public void TestCount()
        {
            int expected = 1;
            ACAD tbc = new CADTBC(connectionString);
            int actual = tbc.Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void TestCountFail()
        {
            setFailConn();
            ACAD tbc = new CADTBC(connectionString);
            tbc.Count();
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void TestLastConnFail()
        {
            setFailConn();
            ACAD tbc = new CADTBC(connectionString);
            tbc.Last();
        }

        [TestMethod]
        public void TestLast()
        {
            ACAD tbc = new CADTBC(connectionString);
            DataRow actual = tbc.Last();
            DataRow expected = tableFormat.NewRow();
            expected["DNI"] = "12345678A";
            expected["Ejecutoria"] = "23/2013";
            expected["Juzgado"] = "Alicante";
            expected["FInicio"] = "2012/11/24";
            expected["FFin"] = "2013/03/09";
            Assert.AreEqual(expected["DNI"], actual["DNI"]);
            Assert.AreEqual(expected["Ejecutoria"], actual["Ejecutoria"]);
            Assert.AreEqual(expected["Juzgado"], actual["Juzgado"]);
            Assert.AreEqual(expected["FInicio"], actual["FInicio"]);
            Assert.AreEqual(expected["FFin"], actual["FFin"]);
        }

        [TestMethod]
        public void SelectAll()
        {
            ACAD tbc = new CADTBC(connectionString);
            DataTable actual = tbc.SelectAll();
            DataTable expected = tableFormat;
            DataRow row = expected.NewRow();
            row["DNI"] = "12345678A";
            row["Ejecutoria"] = "23/2013";
            row["Juzgado"] = "Alicante";
            row["FInicio"] = "2012/11/24";
            row["FFin"] = "2013/03/09";
            expected.Rows.Add(row);
            for (int i = 0; i < expected.Rows.Count; i++)
            {
                Assert.AreEqual(expected.Rows[i]["DNI"], actual.Rows[i]["DNI"]);
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
            ACAD tbc = new CADTBC(connectionString);
            tbc.SelectAll();
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void SelectViodList()
        {
            ACAD tbc = new CADTBC(connectionString);
            tbc.Select(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNumberIdException))]
        public void SelectInvalidNumberId()
        {
            List<object> ids = new List<object>();
            ids.Add("taca");
            ACAD tbc = new CADTBC(connectionString);
            tbc.Select(ids);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void SelectConnFail()
        {
            setFailConn();
            ACAD tbc = new CADTBC(connectionString);
            List<object> ids = new List<object>();
            ids.Add("12345678A");
            ids.Add("23/2013");
            tbc.Select(ids);
        }

        [TestMethod]
        public void SelectWhere()
        {
            ACAD tbc = new CADTBC(connectionString);
            DataTable expected = tableFormat;
            DataRow row = expected.NewRow();
            row["DNI"] = "12345678A";
            row["Ejecutoria"] = "23/2013";
            row["Juzgado"] = "Alicante";
            row["FInicio"] = "2012/11/24";
            row["FFin"] = "2013/03/09";
            expected.Rows.Add(row);
            DataTable actual = tbc.SelectWhere("Juzgado = 'Alicante'");
            for (int i = 0; i < expected.Rows.Count; i++)
            {
                Assert.AreEqual(expected.Rows[i]["DNI"], actual.Rows[i]["DNI"]);
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
            ACAD tbc = new CADTBC(connectionString);
            tbc.SelectWhere("Juzgado = 'Alicante'", -8);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void SelectWHereInvalidStatement()
        {
            ACAD tbc = new CADTBC(connectionString);
            tbc.SelectWhere("Juz'[");
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void SelectWhereFailConn()
        {
            setFailConn();
            ACAD tbc = new CADTBC(connectionString);
            tbc.SelectWhere("Juzgado = 'Alicante'");
        }

        [TestMethod]
        public void Insert()
        {
            ACAD tbc = new CADTBC(connectionString);
            DataRow ins = tbc.GetVoidRow;
            ins["DNI"] = "23456789B";
            ins["Ejecutoria"] = "45/12";
            ins["Juzgado"] = "Juzgado de Albacete";
            ins["FInicio"] = "2013/02/12";
            ins["FFin"] = "2014/04/27";
            DataRow expected = tableFormat.NewRow();
            expected["DNI"] = "23456789B";
            expected["Ejecutoria"] = "45/12";
            expected["Juzgado"] = "Juzgado de Albacete";
            expected["FInicio"] = "2013/02/12";
            expected["FFin"] = "2014/04/27";
            DataRow actual = tbc.Insert(ins);

            Assert.AreEqual(expected["DNI"], actual["DNI"]);
            Assert.AreEqual(expected["Ejecutoria"], actual["Ejecutoria"]);
            Assert.AreEqual(expected["Juzgado"], actual["Juzgado"]);
            Assert.AreEqual(expected["FInicio"], actual["FInicio"]);
            Assert.AreEqual(expected["FFin"], actual["FFin"]);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void InsertFailConn()
        {
            setFailConn();
            ACAD tbc = new CADTBC(connectionString);
            DataRow ins = tbc.GetVoidRow;
            ins["DNI"] = "23456789B";
            ins["Ejecutoria"] = "45/12";
            ins["Juzgado"] = "Juzgado de Albacete";
            ins["FInicio"] = "2013/02/12";
            ins["FFin"] = "2014/04/27";
            tbc.Insert(ins);
        }

        [TestMethod]
        public void Update()
        {
            ACAD tbc = new CADTBC(connectionString);
            DataRow mod = tableFormat.NewRow();
            mod["DNI"] = "12345678A";
            mod["Ejecutoria"] = "23/2013";
            mod["Juzgado"] = "Murcia";
            mod["FInicio"] = "2013/09/13";
            mod["FFin"] = "2014/02/17";
            tbc.Update(mod);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void UpdateNullRow()
        {
            ACAD tbc = new CADTBC(connectionString);
            tbc.Update(null);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void UpdateFailConn()
        {
            setFailConn();
            ACAD tbc = new CADTBC(connectionString);
            DataRow mod = tableFormat.NewRow();
            mod["DNI"] = "123456789A";
            mod["Ejecutoria"] = "23/2013";
            mod["Juzgado"] = "Murcia";
            mod["FInicio"] = "2013/09/13";
            mod["FFin"] = "2014/02/17";
            tbc.Update(mod);
        }

        [TestMethod]
        public void Delete()
        {
            ACAD tbc = new CADTBC(connectionString);
            DataRow del = tableFormat.NewRow();
            del["DNI"] = "12345678A";
            del["Ejecutoria"] = "23/2013";
            del["Juzgado"] = "Alicante";
            del["FInicio"] = "2012/11/24";
            del["FFin"] = "2013/03/09";
            tbc.Delete(del);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void DeleteNullRow()
        {
            ACAD tbc = new CADTBC(connectionString);
            tbc.Delete(null);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void DeleteWronRow()
        {
            ACAD tbc = new CADTBC(connectionString);
            DataRow del = tableFormat.NewRow();
            del["DNI"] = 365365;
            tbc.Delete(del);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void DeleteFailConn()
        {
            setFailConn();
            ACAD tbc = new CADTBC(connectionString);
            DataRow del = tableFormat.NewRow();
            del["DNI"] = "12345678A";
            del["Ejecutoria"] = "23/2013";
            del["Juzgado"] = "Alicante";
            del["FInicio"] = "2012/11/24";
            del["FFin"] = "2013/03/09";
            tbc.Delete(del);            
        }
    }
}
