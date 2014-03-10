using System.Data;
using GebatCAD.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CADUnitTestProject.ADLTests
{
    [TestClass]
    public class ADLFamiliarTest : AADLTest
    {
        protected override DataTable tableFormat
        {
            get 
            {
                DataTable expected = new DataTable();
                expected.Columns.Add("DNI", typeof(string));
                return expected;
            }
        }

        private ADL familiar;

        protected override string specificScript
        {
            get 
            {
                return "Scripts/DatosFamiliaresTest.sql";
            }
        }

        [TestInitialize()]
        public void InitTest()
        {
            ResetConn();
            SetPasswd();
            InitBD(specificScript);
            familiar = new ADL(connectionString, "familiars", "Id");
        }

        [TestMethod]
        public void TestLast()
        {
            DataRow actual = familiar.Last();
            DataRow expected = tableFormat.NewRow();
            expected["DNI"] = "29556003Z";
            Assert.AreEqual(expected["DNI"], actual["DNI"]);
        }
    }
}
