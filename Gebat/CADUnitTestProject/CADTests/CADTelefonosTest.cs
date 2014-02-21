using System;
using System.Data;
using System.Collections.Generic;
using GebatCAD.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CADUnitTestProject.CADTests
{
    [TestClass]
    public class CADTelefonosTest : ACADTest
    {
        protected override DataTable tableFormat
        {
            get 
            {
                DataTable expected = new DataTable();
                expected.Columns.Add("Id", typeof(int));
                expected.Columns.Add("Numero", typeof(string));
                expected.Columns.Add("DNI", typeof(string));
                return expected;
            }
        }

        private AADL telefono;

        private void AssertRows(DataRow expected, DataRow actual)
        {
            Assert.AreEqual(expected["Id"], actual["Id"]);
            Assert.AreEqual(expected["Numero"], actual["Numero"]);
            Assert.AreEqual(expected["DNI"], actual["DNI"]);
        }

        protected override string specificScript
        {
            get 
            {
                return "Scripts/TelefonosTest.sql";
            }
        }

        private DataRow testRow(DataRow voidRow)
        {
            voidRow["Id"] = 1;
            voidRow["Numero"] = "123456789";
            voidRow["DNI"] = "12345678A";
            return voidRow;
        }

        [TestInitialize()]
        public void InitTest()
        {
            ResetConn();
            SetPasswd();
            InitBD(specificScript);
            telefono = new ADLPhones(connectionString);
        }

        [TestMethod]
        public void TestSelectOne()
        {
            DataRow expected = testRow(tableFormat.NewRow());
            List<object> ids = new List<object>();
            ids.Add(1);
            DataRow actual = telefono.Select(ids);
            AssertRows(expected, actual);
        }

        [TestMethod]
        public void TestCount()
        {
            int expected = 2;
            int actual = telefono.Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestLast()
        {
            DataRow actual = telefono.Last();
            DataRow expected = tableFormat.NewRow();
            expected["Id"] = 2;
            expected["Numero"] = "234567890";
            expected["DNI"] = "12345678A";
            AssertRows(expected, actual);
        }

        [TestMethod]
        public void SelectAll()
        {
            DataTable actual = telefono.SelectAll();
            DataTable expected = tableFormat;
            DataRow row = expected.NewRow();
            row["Id"] = 1;
            row["Numero"] = "123456789";
            row["DNI"] = "12345678A";
            DataRow row2 = expected.NewRow();
            row2["Id"] = 2;
            row2["Numero"] = "234567890";
            row2["DNI"] = "12345678A";
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
            DataTable actual = telefono.SelectWhere("Numero = '123456789'");
            for (int i = 0; i < expected.Rows.Count; i++)
            {
                AssertRows(expected.Rows[i], actual.Rows[i]);
            }
        }

        [TestMethod]
        public void Insert()
        {
            DataRow ins = telefono.GetVoidRow;
            ins["Numero"] = "987654321";
            ins["DNI"] = "12345678A";
            DataRow expected = tableFormat.NewRow();
            expected["Id"] = 3;
            expected["Numero"] = "987654321";
            expected["DNI"] = "12345678A";
            DataRow actual = telefono.Insert(ins);
            AssertRows(expected, actual);
        }

        [TestMethod]
        public void Update()
        {
            DataRow mod = tableFormat.NewRow();
            mod["Id"] = 2;
            mod["Numero"] = "978654321";
            mod["DNI"] = "12345678A";
            telefono.Update(mod);
        }

        [TestMethod]
        public void Delete()
        {
            DataRow del = tableFormat.NewRow();
            del["Id"] = 2;
            del["Numero"] = "234567890";
            del["DNI"] = "12345678A";
            telefono.Delete(del);
        }
    }
}
