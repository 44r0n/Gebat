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
    }
}
