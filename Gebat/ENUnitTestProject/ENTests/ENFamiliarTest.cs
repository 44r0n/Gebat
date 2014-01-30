using System;
using GebatEN.Classes;
using GebatEN.Enums;
using GebatCAD.Classes;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ENUnitTestProject.ENTests
{
    [TestClass]
    public class ENFamiliarTest
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
            List<string> telfs = new List<string>();
            ENFamiliar fam = (ENFamiliar)(new ENFamiliar().Read(id));
            Assert.AreEqual("91071949E", fam.DNI);
            Assert.AreEqual("Jose", fam.Nombre);
            Assert.AreEqual("Logroño", fam.Apellidos);
            Assert.AreEqual(41, fam.Edad);
            Assert.AreEqual(sexo.Masculino, fam.Genero);
        }
    }
}
