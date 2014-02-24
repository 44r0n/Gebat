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
            AADL.Password = "root";
        }

        [TestMethod]
        public void Read()
        {
            List<int> id = new List<int>();
            id.Add(2);
            EBPersonalDosier exp = (EBPersonalDosier)(new EBPersonalDosier().Read(id));
            EBFamiliar fam = exp.Familiars[0];
            Assert.AreEqual(500, exp.Income);
            Assert.AreEqual("otra", exp.Observations);
            Assert.AreEqual("29556003Z", fam.DNI);
            Assert.AreEqual(1, exp.Familiars.Count);
        }

        [TestMethod]
        public void AddFamiliar()
        {
            List<int> id = new List<int>();
            id.Add(2);
            EBPersonalDosier exp = (EBPersonalDosier)(new EBPersonalDosier().Read(id));
            EBFamiliar fam = new EBFamiliar("16229371L", "Lucia", "Quevedo", new DateTime(1983, 11, 07), MyGender.Female);
            fam.Save();
            exp.AddFamiliar(fam);
        }
    }
}
