using Microsoft.VisualStudio.TestTools.UnitTesting;
using GebatModel;
using System.Collections.Generic;

namespace GebatModelTest
{
    [TestClass]
    public class TestFamiliar
    {
        private IFamiliarRespository repofamiliar;
        private IPersonalDossierRepository repodossier;

        [TestInitialize]
        public void Init()
        {
            repofamiliar = new FamiliarRespository();
            repodossier = new PersonalDossierRepository();
        }

        [TestCleanup]
        public void Clean()
        {
            DossierStub dstub = new DossierStub();
            FamiliarRepoStub stub = new FamiliarRepoStub();
            stub.ClearFamiliars();
            dstub.ClearDossiers();            
        }

        [TestMethod]
        public void AddFamiliar()
        {
            PersonalDossier dossier = new PersonalDossier();
            dossier.Observations = "Unas observaciones";
            Familiar familiar = new Familiar();
            familiar.BirthDate = new System.DateTime(1976, 10, 21);
            familiar.DNI = "4345632F";
            familiar.Gender = Gender.Man;
            familiar.Income = 300;
            familiar.Name = "Manolo";
            familiar.Surname = "García";
            dossier.Familiar.Add(familiar);
            repodossier.AddDossier(dossier);
            List<Familiar> familiars = repofamiliar.GetAllFamiliars();
            Assert.AreEqual(1, familiars.Count);
            Assert.AreEqual("Manolo", familiars[0].Name);
        }

        [TestMethod]
        public void TotalIncome()
        {
            PersonalDossier dossier = new PersonalDossier();
            dossier.Observations = "Obser";
            Familiar familiar = new Familiar();
            familiar.BirthDate = new System.DateTime(1978, 10, 21);
            familiar.DNI = "123412T";
            familiar.Gender = Gender.Man;
            familiar.Income = 200;
            familiar.Name = "Pepe";
            familiar.Surname = "Ussar";
            dossier.Familiar.Add(familiar);
            Familiar familiar2 = new Familiar();
            familiar2.BirthDate = new System.DateTime(1981, 8, 3);
            familiar2.DNI = "3123123c";
            familiar2.Gender = Gender.Woman;
            familiar2.Income = 134;
            familiar2.Name = "Tere";
            familiar2.Surname = "Basa";
            dossier.Familiar.Add(familiar2);
            repodossier.AddDossier(dossier);
            Assert.AreEqual(334, dossier.TotalIncome);
        }
    }
}
