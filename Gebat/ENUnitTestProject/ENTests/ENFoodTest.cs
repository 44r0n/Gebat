using System;
using GebatEN.Classes;
using GebatCAD.Classes;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ENUnitTestProject.ENTests
{
    [TestClass]
    public class ENFoodTest
    {
        [ClassInitialize()]
        public static void stepasswd(TestContext context)
        {
            ACAD.Password = "root";
        }

        [TestMethod]
        public void Read()
        {
            List<int> id = new List<int>();
            id.Add(4);
            ENFood comida = (ENFood)(new ENFood("").Read(id));
            Assert.AreEqual("Pomes", comida.Name);
            Assert.AreEqual("Kg", comida.MyType.Name);
            Assert.AreEqual(2, comida.Quantity);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ReadNullId()
        {
            new ENFood("").Read(null);
        }

        [TestMethod]
        public void ReadAll_Insert_Delete()
        {
            List<AEN> general = new ENFood("").ReadAll();
            List<ENFood> expected = new List<ENFood>();
            
        }
    }
}
