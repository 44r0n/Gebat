using System;
using GebatEN.Classes;
using GebatCAD.Classes;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ENUnitTestProject.ENTests
{
    [TestClass]
    public class ENTelefonoTest
    {
        [ClassInitialize()]
        public static void setpasswd(TestContext context)
        {
            ACAD.Password = "root";
        }

        [TestMethod]
        public void Read()
        {
            List<int> id = new List<int>();
            id.Add(2);
            ENTelefono telefono = (ENTelefono)new ENTelefono().Read(id);
            Assert.AreEqual("234567890", telefono.Numero);
            Assert.AreEqual("12345678A", telefono.Dueño.DNI);
        }
    }
}
