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
            AADL.Password = "root";
        }

        [TestMethod]
        public void Read()
        {
            List<int> id = new List<int>();
            id.Add(2);
            List<string> telfs = new List<string>();
            EBFamiliar fam = (EBFamiliar)(new EBFamiliar().Read(id));
            Assert.AreEqual("91071949E", fam.DNI);
            Assert.AreEqual("Jose", fam.Name);
            Assert.AreEqual("Logroño", fam.Surname);
            Assert.AreEqual(41, fam.Age);
            Assert.AreEqual(MyGender.Male, fam.Gender);
        }

        [TestMethod]
        public void Save()
        {
            EBFamiliar nuevo = new EBFamiliar("42919826D", "Paco", "Mendoza", new DateTime(1978, 04, 18), MyGender.Male);
            nuevo.Save();
        }
    }
}
