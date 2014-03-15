using System;
using GebatEN.Classes;
using GebatEN.Enums;
using GebatCAD.Classes;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ENUnitTestProject.ENTests
{
    [TestClass]
    public class EBDossier
    {
        [ClassInitialize()]
        public static void setpasswd(TestContext context)
        {
            ADL.Password = "root";
        }

        [TestMethod]
        public void Read()
        {
            List<object> id = new List<object>();
            id.Add(2);
            EBPersonalDosier exp = (EBPersonalDosier)(new EBPersonalDosier().Read(id));
            EBFamiliar fam = exp.Familiars[0];
            Assert.AreEqual("otra", exp.Observations);
            Assert.AreEqual("29556003Z", fam.DNI);
        }

        [TestMethod]
        public void AddFamiliar()
        {
            List<object> id = new List<object>();
            id.Add(2);
            EBPersonalDosier exp = (EBPersonalDosier)(new EBPersonalDosier().Read(id));
            EBFamiliar fam = new EBFamiliar("16229371L", "Lucia", "Quevedo", new DateTime(1983, 11, 07), MyGender.Female,1,400);
            fam.Save();
            exp.AddFamiliar(fam);
        }
        [TestMethod]
        public void AddConcession()
        {
            EBFresco nuevo = new EBFresco(new DateTime(2014, 10, 23), new DateTime(2015,1,17), "una nota");
            List<object> id = new List<object>();
            id.Add(1);
            EBPersonalDosier exp = (EBPersonalDosier)(new EBPersonalDosier().Read(id));
            exp.AddConcession(nuevo);
            List<AEBConcession> actual = exp.Concessions;
            Assert.AreEqual(3, actual.Count);
            Assert.AreEqual(1, (int)actual[0].Id[0]);
            Assert.AreEqual(2, (int)actual[1].Id[0]);
            Assert.AreEqual(4, (int)actual[2].Id[0]);
        }
    }
}
