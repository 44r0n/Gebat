﻿using System;
using System.Data;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using GebatCAD.Classes;
using GebatCAD.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CADUnitTestProject.CADTests
{
    [TestClass]
    public class CADOutgoingFoodTest : ACADTest
    {
        protected override DataTable tableFormat
        {
            get 
            {
                DataTable expected = new DataTable();
                expected.Columns.Add("Id", typeof(int));
                expected.Columns.Add("FoodType", typeof(int));
                expected.Columns.Add("QuantityOut", typeof(int));
                expected.Columns.Add("Fecha", typeof(DateTime));
                return expected;
            }
        }

        protected override string specificScript
        {
            get 
            {
                return "Scripts/OutgoingFoodTest.sql";
            }
        }

        private AADL outgoing;

        private void AssertRow(DataRow expected, DataRow actual)
        {
            Assert.AreEqual(expected["FoodType"], actual["FoodType"]);
            Assert.AreEqual(expected["QuantityOut"], actual["QuantityOut"]);
            Assert.AreEqual(expected["Fecha"], actual["Fecha"]);
        }

        [TestInitialize()]
        public void InitTest()
        {
            ResetConn();
            SetPasswd();
            InitBD(specificScript);
            outgoing = new CADOutgoingFood(connectionString);
        }

        [TestMethod]
        public void TestSelectOne()
        {
            DataRow expected = tableFormat.NewRow();
            expected["FoodType"] = 1;
            expected["QuantityOut"] = 1;
            expected["Fecha"] = "2012/11/24";
            List<object> ids = new List<object>();
            ids.Add((int)1);
            DataRow actual = outgoing.Select(ids);
            AssertRow(expected, actual);
        }

        [TestMethod]
        public void TestCount()
        {
            int expected = 3;
            int actual = outgoing.Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestLast()
        {
            DataRow actual = outgoing.Last();
            DataRow expected = tableFormat.NewRow();
            expected["Id"] = 3;
            expected["FoodType"] = 4;
            expected["QuantityOut"] = 2;
            expected["Fecha"] = "2012/11/25";
            AssertRow(expected, actual);
        }

        [TestMethod]
        public void SelectAll()
        {
            AADL outgoing = new CADOutgoingFood(connectionString);
            DataTable actual = outgoing.SelectAll();
            DataTable expected = tableFormat;
            DataRow row = expected.NewRow();
            row["Id"] = 1;
            row["FoodType"] = 1;
            row["QuantityOut"] = 1;
            row["Fecha"] = "2012/11/24";
            DataRow row2 = expected.NewRow();
            row2["Id"] = 2;
            row2["FoodType"] = 1;
            row2["QuantityOut"] = 1;
            row2["Fecha"] = "2012/11/24";
            DataRow row3 = expected.NewRow();
            row3["Id"] = 3;
            row3["FoodType"] = 4;
            row3["QuantityOut"] = 2;
            row3["Fecha"] = "2012/11/25";
            expected.Rows.Add(row);
            expected.Rows.Add(row2);
            expected.Rows.Add(row3);
            for (int i = 0; i < expected.Rows.Count; i++)
            {
                AssertRow(expected.Rows[i], actual.Rows[i]);
            }
        }

        [TestMethod]
        public void SelectWhere()
        {
            AADL outgoing = new CADOutgoingFood(connectionString);
            DataTable expected = tableFormat;
            DataRow row = expected.NewRow();
            row["Id"] = 3;
            row["FoodType"] = 4;
            row["QuantityOut"] = 2;
            row["Fecha"] = "2012/11/25";
            expected.Rows.Add(row);
            DataTable actual = outgoing.SelectWhere("Fecha = '2012/11/25'");

            for (int i = 0; i < expected.Rows.Count; i++)
            {
                AssertRow(expected.Rows[i], actual.Rows[i]);
            }
        }

        [TestMethod]
        public void Insert()
        {
            AADL outgoing = new CADOutgoingFood(connectionString);
            DataRow ins = outgoing.GetVoidRow;
            ins["FoodType"] = 4;
            ins["QuantityOut"] = 2;
            ins["Fecha"] = "2012/11/25";
            DataRow expected = tableFormat.NewRow();
            expected["Id"] = 4;
            expected["FoodType"] = 4;
            expected["QuantityOut"] = 2;
            expected["Fecha"] = "2012/11/25";
            DataRow actual = outgoing.Insert(ins);
            AssertRow(expected, actual);
        }

        [TestMethod]
        public void Update()
        {
            AADL outgoing = new CADOutgoingFood(connectionString);
            DataRow mod = tableFormat.NewRow();
            mod["Id"] = 2;
            mod["FoodType"] = 2;
            mod["QuantityOut"] = 3;
            mod["Fecha"] = "2011/02/02";
            outgoing.Update(mod);
        }

        [TestMethod]
        public void Delete()
        {
            AADL outgoing = new CADOutgoingFood(connectionString);
            DataRow del = tableFormat.NewRow();
            del["Id"] = 2;
            del["FoodType"] = 1;
            del["QuantityOut"] = 1;
            del["Fecha"] = "2012/11/24";
            outgoing.Delete(del);
        }
    }
}
