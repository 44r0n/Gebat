﻿using System;
using System.Data;
using System.Collections.Generic;
using GebatCAD.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CADUnitTestProject.ADLTests
{
    [TestClass]
    public class ADLPhonesTest : AADLTest
    {
        protected override DataTable tableFormat
        {
            get 
            {
                DataTable expected = new DataTable();
                expected.Columns.Add("Id", typeof(int));
                expected.Columns.Add("PhoneNumber", typeof(string));
                expected.Columns.Add("Owner", typeof(string));
                return expected;
            }
        }

        private ADL phone;

        private void AssertRows(DataRow expected, DataRow actual)
        {
            Assert.AreEqual(expected["Id"], actual["Id"]);
            Assert.AreEqual(expected["PhoneNumber"], actual["PhoneNumber"]);
            Assert.AreEqual(expected["Owner"], actual["Owner"]);
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
            voidRow["PhoneNumber"] = "123456789";
            voidRow["Owner"] = "12345678A";
            return voidRow;
        }

        [TestInitialize()]
        public void InitTest()
        {
            ResetConn();
            SetPasswd();
            InitBD(specificScript);
            phone = new ADL(connectionString, "phones", "Id");
        }

        [TestMethod]
        public void TestCount()
        {
            int expected = 2;
            int actual = phone.Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestLast()
        {
            DataRow actual = phone.Last();
            DataRow expected = tableFormat.NewRow();
            expected["Id"] = 2;
            expected["PhoneNumber"] = "234567890";
            expected["Owner"] = "12345678A";
            AssertRows(expected, actual);
        }

        [TestMethod]
        public void SelectAll()
        {
            DataTable actual = phone.SelectAll();
            DataTable expected = tableFormat;
            DataRow row = expected.NewRow();
            row["Id"] = 1;
            row["PhoneNumber"] = "123456789";
            row["Owner"] = "12345678A";
            DataRow row2 = expected.NewRow();
            row2["Id"] = 2;
            row2["PhoneNumber"] = "234567890";
            row2["Owner"] = "12345678A";
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
            DataTable actual = phone.Select("SELECT * FROM phones WHERE PhoneNumber = @Number","123456789");
            for (int i = 0; i < expected.Rows.Count; i++)
            {
                AssertRows(expected.Rows[i], actual.Rows[i]);
            }
        }
    }
}
