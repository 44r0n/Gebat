using System;
using GebatEN.Classes;
using GebatCAD.Classes;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ENUnitTestProject.EBTests
{
    [TestClass]
    public class ENFrescoTest
    {
        [ClassInitialize()]
        public static void setpasswod(TestContext context)
        {
            ADL.Password = "root";
        }

        [TestMethod]
        public void TestIsFresco()
        {
            Assert.IsTrue(EBFresco.IsFresco(1));
            Assert.IsTrue(EBFresco.IsFresco(2));
            Assert.IsFalse(EBFresco.IsFresco(3));
        }
    }
}
