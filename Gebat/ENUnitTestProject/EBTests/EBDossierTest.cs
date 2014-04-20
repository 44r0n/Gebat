using System;
using GebatEN.Classes;
using GebatEN.Exceptions;
using GebatEN.Enums;
using GebatCAD.Classes;
using System.Collections.Generic;
using ENUnitTestProject.Stubs;
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
            Assert.AreEqual(9, (int)actual[2].Id[0]);
        }

        [TestMethod]
        public void AddNullFinishDateConcession()
        {
            EBFresco nuevo = new EBFresco();
            nuevo.BeginDate = new DateTime(2015, 1, 23);
            nuevo.Notes = "test null finishDate";
            List<object> id = new List<object>();
            id.Add(1);
            EBPersonalDosier exp = (EBPersonalDosier)(new EBPersonalDosier().Read(id));
            exp.AddConcession(nuevo);
        }

        [TestMethod]
        public void AddFegaInDate()
        {
            DateTime hoy = new DateTime(2012,11,10);
            FegaStub.SetFecha(hoy);
            FegaStub add = new FegaStub(new DateTime(2012,11,11),new DateTime(2013,2,11),"pruebafecha",FegaStates.Awaiting);
            List<object> id = new List<object>();
            id.Add(3);
            DossierStub exp = (DossierStub) new DossierStub().Read(id);
            exp.AddConcession(add);
            Assert.AreEqual("pruebafecha", ((EBFega)exp.Concessions[2]).Notes);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDateConcessionException))]
        public void AddFegaOutDate()
        {
            DateTime hoy = new DateTime(2012, 11, 10);
            FegaStub.SetFecha(hoy);
            FegaStub add = new FegaStub(new DateTime(2012, 11, 11), new DateTime(2013, 2, 11), "pruebafecha", FegaStates.Awaiting);
            List<object> id = new List<object>();
            id.Add(4);
            DossierStub exp = (DossierStub)new DossierStub().Read(id);
            exp.AddConcession(add);
        }
    }
}
