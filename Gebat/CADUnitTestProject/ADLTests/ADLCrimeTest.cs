using System.Data;
using System.Collections.Generic;
using GebatCAD.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CADUnitTestProject.ADLTests
{
    [TestClass]
    public class ADLCrimeTest : AADLTest
    {

        protected override DataTable tableFormat
        {
            get
            {
                DataTable expected = new DataTable();
                expected.Columns.Add("Id", typeof(int));
                expected.Columns.Add("Name", typeof(string));
                return expected;
            }
        }

        protected override string specificScript
        {
            get
            {
                return "Scripts/DelitosTest.sql";
            }
        }

        private ADL crime;

        private void AssertRow(DataRow expected, DataRow actual)
        {
            Assert.AreEqual(expected["Id"], actual["Id"]);
            Assert.AreEqual(expected["Name"], actual["Name"]);
        }

        [TestInitialize()]
        public void InnitTest()
        {
            ResetConn();
            SetPasswd();
            InitBD(specificScript);
            crime = new ADL(connectionString, "crimes", "Id");
        }

        [TestMethod]
        public void Select()
        {
            string expected = "Robo";
            DataTable tabla = crime.Select("Select * FROM crimes WHERE Id = @Id",1);
            DataRow actual = tabla.Rows[0];
            Assert.AreEqual(actual["Name"].ToString(), expected);
        }

        [TestMethod]
        public void TestCount()
        {
            int expected = 2;
            int actual = crime.Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ExectuteNonQuery()
        {
            int rows = crime.ExecuteNonQuery("INSERT INTO crimes (Name) VALUES (@Name)", "Cosas");
            Assert.AreEqual(1, rows);
        }

        [TestMethod]
        public void Update()
        {
            Assert.AreEqual(1,crime.ExecuteNonQuery("UPDATE crimes SET Name = @Name WHERE Id = @Id","Cajas",2));
        }

        [TestMethod]
        public void Delete()
        {
            Assert.AreEqual(1,crime.ExecuteNonQuery("DELETE FROM crimes WHERE Id = @Id",2));
        }
    }
}
