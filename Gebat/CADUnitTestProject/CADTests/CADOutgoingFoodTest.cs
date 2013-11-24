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
                return "OutgoingFoodTest.sql";
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
            expected["FoodType"] = 1;
            expected["QuantityOut"] = 1;
            expected["Fecha"] = "2012/11/24";
            ACAD outgoing = new CADOutgoingFood(connectionString);
            List<object> ids = new List<object>();
            ids.Add((int)1);
            DataRow actual = outgoing.Select(ids);
            Assert.AreEqual(expected["FoodType"], actual["FoodType"]);
            Assert.AreEqual(expected["QuantityOut"], actual["QuantityOut"]);
            Assert.AreEqual(expected["Fecha"], actual["Fecha"]);
        }

        [TestMethod]
        public void TestCount()
        {
            int expected = 3;
            ACAD outgoing = new CADOutgoingFood(connectionString);
            int actual = outgoing.Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void TestCountConnFail()
        {
            setFailConn();
            ACAD outgoing = new CADOutgoingFood(connectionString);
            outgoing.Count();
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void TestLastConnFail()
        {
            setFailConn();
            ACAD outgoing = new CADOutgoingFood(connectionString);
            outgoing.Last();
        }

        [TestMethod]
        public void TestLast()
        {
            ACAD outgoing = new CADOutgoingFood(connectionString);
            DataRow actual = outgoing.Last();
            DataRow expected = tableFormat.NewRow();
            expected["Id"] = 3;
            expected["FoodType"] = 4;
            expected["QuantityOut"] = 2;
            expected["Fecha"] = "2012/11/25";
            Assert.AreEqual(expected["Id"], actual["Id"]);
            Assert.AreEqual(expected["FoodType"], actual["FoodType"]);
            Assert.AreEqual(expected["QuantityOut"], actual["QuantityOut"]);
            Assert.AreEqual(expected["Fecha"], actual["Fecha"]);
        }

        [TestMethod]
        public void SelectAll()
        {
            ACAD outgoing = new CADOutgoingFood(connectionString);
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
                Assert.AreEqual(expected.Rows[i]["Id"], actual.Rows[i]["Id"]);
                Assert.AreEqual(expected.Rows[i]["FoodType"], actual.Rows[i]["FoodType"]);
                Assert.AreEqual(expected.Rows[i]["QuantityOut"], actual.Rows[i]["QuantityOut"]);
                Assert.AreEqual(expected.Rows[i]["Fecha"], actual.Rows[i]["Fecha"]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void SelectAllFailConn()
        {
            setFailConn();
            ACAD outgoing = new CADOutgoingFood(connectionString);
            outgoing.SelectAll();
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void SelectVoidList()
        {
            ACAD outgoing = new CADOutgoingFood(connectionString);
            outgoing.Select(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNumberIdException))]
        public void SelectInvalidNumberIrd()
        {
            List<object> ids = new List<object>();
            ids.Add("hola");
            ids.Add(45);
            ACAD outgoing = new CADOutgoingFood(connectionString);
            outgoing.Select(ids);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void SelectConnFail()
        {
            setFailConn();
            ACAD outgoing = new CADOutgoingFood(connectionString);
            List<object> ids = new List<object>();
            ids.Add(2);
            outgoing.Select(ids);
        }

        [TestMethod]
        public void SelectWhere()
        {
            ACAD outgoing = new CADOutgoingFood(connectionString);
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
                Assert.AreEqual(expected.Rows[i]["Id"], actual.Rows[i]["Id"]);
                Assert.AreEqual(expected.Rows[i]["FoodType"], actual.Rows[i]["FoodType"]);
                Assert.AreEqual(expected.Rows[i]["QuantityOut"], actual.Rows[i]["QuantityOut"]);
                Assert.AreEqual(expected.Rows[i]["Fecha"], actual.Rows[i]["Fecha"]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidStartRecordException))]
        public void SelectWhereInvalidStart()
        {
            ACAD outgoing = new CADOutgoingFood(connectionString);
            outgoing.SelectWhere("Fecha = '2012/11/25'", -2);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void SelectWhereInvalidStatement()
        {
            ACAD outgoing = new CADOutgoingFood(connectionString);
            outgoing.SelectWhere("Fecha = ;");
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void SelectWhereFailConn()
        {
            setFailConn();
            ACAD outgoing = new CADOutgoingFood(connectionString);
            outgoing.SelectWhere("Fecha = '2012/11/25'");
        }

        [TestMethod]
        public void Insert()
        {
            ACAD outgoing = new CADOutgoingFood(connectionString);
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

            Assert.AreEqual(expected["Id"], actual["Id"]);
            Assert.AreEqual(expected["FoodType"], actual["FoodType"]);
            Assert.AreEqual(expected["QuantityOut"], actual["QuantityOut"]);
            Assert.AreEqual(expected["Fecha"], actual["Fecha"]);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void InsertNullRow()
        {
            ACAD outgoing = new CADOutgoingFood(connectionString);
            outgoing.Insert(null);
        }

        [TestMethod]
        public void Update()
        {
            ACAD outgoing = new CADOutgoingFood(connectionString);
            DataRow mod = tableFormat.NewRow();
            mod["Id"] = 2;
            mod["FoodType"] = 2;
            mod["QuantityOut"] = 3;
            mod["Fecha"] = "2011/02/02";
            outgoing.Update(mod);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void UpdateNullRow()
        {
            ACAD outgoing = new CADOutgoingFood(connectionString);
            outgoing.Update(null);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void UpdateFailConn()
        {
            setFailConn();
            ACAD outgoing = new CADOutgoingFood(connectionString);
            DataRow mod = tableFormat.NewRow();
            mod["Id"] = 2;
            mod["FoodType"] = 2;
            mod["QuantityOut"] = 2;
            mod["Fecha"] = "2012/11/24";
            outgoing.Update(mod);
        }

        [TestMethod]
        public void Delete()
        {
            ACAD outgoing = new CADOutgoingFood(connectionString);
            DataRow del = tableFormat.NewRow();
            del["Id"] = 2;
            del["FoodType"] = 1;
            del["QuantityOut"] = 1;
            del["Fecha"] = "2012/11/24";
            outgoing.Delete(del);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void DeleteNullRow()
        {
            ACAD outgoing = new CADOutgoingFood(connectionString);
            outgoing.Delete(null);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void DeleteWrongRow()
        {
            ACAD outgoing = new CADOutgoingFood(connectionString);
            DataRow del = tableFormat.NewRow();
            del["QuantityOut"] = 9;
            outgoing.Delete(del);
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlException))]
        public void DeleteFailConn()
        {
            setFailConn();
            ACAD outgoing = new CADOutgoingFood(connectionString);
            DataRow del = tableFormat.NewRow();
            del["Id"] = 2;
            del["FoodType"] = 1;
            del["QuantityOut"] = 1;
            del["Fecha"] = "2012/11/24";
            outgoing.Delete(del);

        }
    }
}
