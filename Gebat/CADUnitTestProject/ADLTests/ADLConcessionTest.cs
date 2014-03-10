﻿using System;
using System.Data;
using System.Collections.Generic;
using GebatCAD.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CADUnitTestProject.ADLTests
{
    [TestClass]
    public class ADLConcessionTest : AADLTest
    {
        protected override DataTable tableFormat
        {
            get 
            {
                DataTable expected = new DataTable();
                expected.Columns.Add("Id", typeof(int));
                expected.Columns.Add("Dossier", typeof(int));
                expected.Columns.Add("BeginDate", typeof(DateTime));
                expected.Columns.Add("FinishDate", typeof(DateTime));
                expected.Columns.Add("Notes",typeof(string));
                return expected;
            }
        }

        private ADL concession;

        protected override string specificScript
        {
            get 
            {
                return "Scripts/Concessions.sql";
            }
        }

        [TestInitialize()]
        public void InitTest()
        {
            ResetConn();
            SetPasswd();
            InitBD(specificScript);
            concession = new ADL(connectionString, "concessions", "Id");
        }

        [TestMethod]
        public void TestLast()
        {
            DataRow actual = concession.Last();
            DataRow expected = tableFormat.NewRow();
            expected["Dossier"] = 2;
            expected["Id"] = 3;
            Assert.AreEqual(expected["Dossier"], actual["Dossier"]);
            Assert.AreEqual(expected["Id"], actual["Id"]);
        }

        [TestMethod]
        public void Select()
        {   
            DataTable tabla = concession.Select("Select * From concessions Where Id = @Id", 1);
            Assert.AreEqual(1, tabla.Rows.Count);
        }
    }
}
