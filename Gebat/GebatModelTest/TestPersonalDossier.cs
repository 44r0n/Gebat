using Microsoft.VisualStudio.TestTools.UnitTesting;
using GebatModel;
using System.Collections.Generic;

namespace GebatModelTest
{
    [TestClass]
    public class TestPersonalDossier
    {
        private IPersonalDossierRepository repodossier;

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
                concession.Type = type;
                return concession;
            }
        }

        [TestInitialize]
        public void Init()
        {
            repodossier = new PersonalDossierRepository();
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
    }
}
