using System;
using GebatEN.Classes;
using GebatEN.Enums;
using GebatCAD.Classes;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ENUnitTestProject.ENTests
{
    [TestClass]
    public class ENExpedienteTest
    {
        [ClassInitialize()]
        public static void setpasswd(TestContext context)
        {
            AADL.Password = "root";
        }

        [TestMethod]
        public void Read()
        {
            List<int> id = new List<int>();
            id.Add(2);
            ENExpedientePersonal exp = (ENExpedientePersonal)(new ENExpedientePersonal().Read(id));
            ENFamiliar fam = exp.Familiares[0];
            Assert.AreEqual(500, exp.Ingresos);
            Assert.AreEqual("otra", exp.Observaciones);
            Assert.AreEqual("29556003Z", fam.DNI);
            Assert.AreEqual(1, exp.Familiares.Count);
        }

        [TestMethod]
        public void AddFamiliar()
        {
            List<int> id = new List<int>();
            id.Add(2);
            ENExpedientePersonal exp = (ENExpedientePersonal)(new ENExpedientePersonal().Read(id));
            ENFamiliar fam = new ENFamiliar("16229371L", "Lucia", "Quevedo", new DateTime(1983, 11, 07), sexo.Femenino);
            fam.Save();
            exp.AddFamiliar(fam);
        }
    }
}
