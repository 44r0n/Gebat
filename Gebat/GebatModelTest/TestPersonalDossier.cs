using Microsoft.VisualStudio.TestTools.UnitTesting;
using GebatModel;
using System;
using System.Collections.Generic;

namespace GebatModelTest
{
    [TestClass]
    public class TestPersonalDossier
    {
        private IPersonalDossierRepository repodossier;
        private IConcessionTypeRepository repotype;

        private Familiar StandardFamiliar
        {
            get
            {
                Familiar familiar = new Familiar();
                familiar.BirthDate = new System.DateTime(1976, 10, 21);
                familiar.DNI = "4345632F";
                familiar.Gender = Gender.Man;
                familiar.Income = 300;
                familiar.Name = "Manolo";
                familiar.Surname = "García";
                return familiar;
            }
        }

        private ConcessionType getFega()
        {
            return repotype.SearchConcessionType("Fega")[0];
        }

        private Concession StandardConcession
        {
            get
            {
                Concession concession = new Concession();
                concession.Observations = "Observaciones de prueba";
                concession.BeginDate = new System.DateTime(2014, 2, 2);
                concession.FinishDate = new System.DateTime(2014, 5, 2);
                ConcessionType type = new ConcessionType();
                type.Name = "uno";
                DateRestriction restriction = new DateRestriction();
                restriction.Interval = 0;
                type.DateRestriction = restriction;
                concession.Type = type;
                return concession;
            }
        }

        [TestInitialize]
        public void Init()
        {
            repodossier = new PersonalDossierRepository();
            repotype = new ConcessionTypeRepository();
            ConcessionType type = new ConcessionType();
            type.Name = "Fega";
            DateRestriction restriction = new DateRestriction();
            restriction.Interval = 3;
            type.DateRestriction = restriction;
            repotype.AddConcessionType(type);
        }

        [TestCleanup]
        public void Clean()
        {
            ConcessionRepoStub crstub = new ConcessionRepoStub();
            DossierStub dstub = new DossierStub();
            FamiliarRepoStub stub = new FamiliarRepoStub();
            crstub.ClearConcession();
            stub.ClearFamiliars();
            dstub.ClearDossiers();
        }

        [TestMethod]
        [ExpectedException(typeof(MinimumConcessionsException))]
        public void NewPersonalDossierOnlyFamiliar()
        {
            PersonalDossier dossier = new PersonalDossier();
            dossier.Observations = "Unas observaciones";
            dossier.Familiar.Add(this.StandardFamiliar);
            repodossier.AddDossier(dossier);
        }

        [TestMethod]
        [ExpectedException(typeof(MinimumFamiliarConcession))]
        public void NewPersonalDossierOnlyConcession()
        {
            Concession concession = new Concession();
            concession.Observations = "Observacioes de prueba";
            concession.BeginDate = new System.DateTime(2014, 2, 2);
            concession.FinishDate = new System.DateTime(2014, 5, 2);
            PersonalDossier dossier = new PersonalDossier();
            dossier.Observations = "Mas observaciones";
            dossier.Concessions.Add(concession);
            repodossier.AddDossier(dossier);
        }

        [TestMethod]
        public void NewPersonalDossier()
        {
            
            PersonalDossier dossier = new PersonalDossier();
            dossier.Observations = "Más observaciones";
            dossier.Concessions.Add(this.StandardConcession);
            dossier.Familiar.Add(StandardFamiliar);
            repodossier.AddDossier(dossier);
            PersonalDossier totest = repodossier.GetAllDossiers()[0];
            Assert.AreEqual("Más observaciones", totest.Observations);
            Familiar[] familiars = new Familiar[totest.Familiar.Count];
            totest.Familiar.CopyTo(familiars, 0);
            Assert.AreEqual("Manolo", familiars[0].Name);
            Concession[] concesions = new Concession[totest.Concessions.Count];
            totest.Concessions.CopyTo(concesions, 0);
            Assert.AreEqual("Observaciones de prueba", concesions[0].Observations);
            Assert.AreEqual("uno", concesions[0].Type.Name);
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
            dossier.Familiar.Add(StandardFamiliar);
            dossier.Concessions.Add(StandardConcession);
            repodossier.AddDossier(dossier);
            Assert.AreEqual(500, dossier.TotalIncome);
        }

        [TestMethod]
        public void AddOnlyOne()
        {
            PersonalDossier dossier = new PersonalDossier();
            dossier.Familiar.Add(StandardFamiliar);
            Concession newConcession = new Concession();
            newConcession.Type = getFega();
            newConcession.BeginDate = new DateTime(2014, 5, 5);
            newConcession.FinishDate = new DateTime(2014, 8, 5);
            dossier.AddConcession(newConcession);
            repodossier.AddDossier(dossier);
            List<PersonalDossier> dossiers = repodossier.GetAllDossiers();
            Concession[] concessions = new Concession[dossiers[0].Concessions.Count];
            dossiers[0].Concessions.CopyTo(concessions, 0);
            Assert.AreEqual(newConcession.BeginDate, concessions[0].BeginDate);
        }

        [TestMethod]
        public void AddTwoFega()
        {
            PersonalDossier dossier = new PersonalDossier();
            dossier.Familiar.Add(StandardFamiliar);
            Concession first = new Concession();
            first.Type = getFega();
            first.BeginDate = new DateTime(2014, 2, 5);
            first.FinishDate = new DateTime(2014, 5, 5);
            dossier.AddConcession(first);
            repodossier.AddDossier(dossier);
            PersonalDossier loaded = repodossier.GetAllDossiers()[0];
            Concession second = new Concession();
            second.Type = getFega();
            second.BeginDate = new DateTime(2014, 5, 5);
            second.FinishDate = new DateTime(2014, 8, 5);
            loaded.AddConcession(second);
            repodossier.UpdateDossier(loaded);
            Assert.AreEqual(2, repodossier.GetAllDossiers()[0].Concessions.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ConcessionDateException))]
        public void AddThreeFegaFail()
        {
            PersonalDossier dossier = new PersonalDossier();
            dossier.Familiar.Add(StandardFamiliar);
            Concession first = new Concession();
            first.Type = getFega();
            first.BeginDate = new DateTime(2013, 11, 5);
            first.FinishDate = new DateTime(2014, 2, 5);
            dossier.AddConcession(first);
            repodossier.AddDossier(dossier);
            PersonalDossier loaded = repodossier.GetAllDossiers()[0];
            Concession second = new Concession();
            second.Type = getFega();
            second.BeginDate = new DateTime(2014, 2, 5);
            second.FinishDate = new DateTime(2014, 5, 5);
            dossier.AddConcession(second);
            Concession third = new Concession();
            third.Type = getFega();
            third.BeginDate = new DateTime(2014,5,5);
            third.FinishDate = new DateTime(2014, 8, 5);
            loaded.AddConcession(third);
        }

        [TestMethod]
        public void AddThreeFegaSuccess()
        {
            PersonalDossier dossier = new PersonalDossier();
            dossier.Familiar.Add(StandardFamiliar);
            Concession first = new Concession();
            first.Type = getFega();
            first.BeginDate = new DateTime(2013, 11, 5);
            first.FinishDate = new DateTime(2014, 2, 5);
            dossier.AddConcession(first);
            repodossier.AddDossier(dossier);
            PersonalDossier loaded = repodossier.GetAllDossiers()[0];
            Concession second = new Concession();
            second.Type = getFega();
            second.BeginDate = new DateTime(2014, 2, 5);
            second.FinishDate = new DateTime(2014, 5, 5);
            loaded.AddConcession(second);
            Concession third = new Concession();
            third.Type = getFega();
            third.BeginDate = new DateTime(2014, 8, 5);
            third.FinishDate = new DateTime(2014, 11, 5);
            loaded.AddConcession(third);
        }

        [TestMethod]
        public void AddTwoFegaAndOther()
        {
            PersonalDossier dossier = new PersonalDossier();
            dossier.Familiar.Add(StandardFamiliar);
            Concession first = new Concession();
            first.Type = getFega();
            first.BeginDate = new DateTime(2014, 2, 5);
            first.FinishDate = new DateTime(2014, 5, 5);
            dossier.AddConcession(first);
            repodossier.AddDossier(dossier);
            PersonalDossier loaded = repodossier.GetAllDossiers()[0];
            Concession second = new Concession();
            second.Type = getFega();
            second.BeginDate = new DateTime(2014, 5, 5);
            second.FinishDate = new DateTime(2014, 8, 5);
            dossier.AddConcession(second);
            Concession other = StandardConcession;
            loaded.AddConcession(other);
            repodossier.UpdateDossier(loaded);

        }
    }
}
