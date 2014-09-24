using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GebatModel;

namespace GebatModelTest
{
    [TestClass]
    public class TestConcessions
    {
        private IConcessionRepository repoconcesion;

        [TestInitialize]
        public void Init()
        {
            repoconcesion = new ConcessionRepository();
        }

        [TestCleanup]
        public void Clean()
        {
            ConcessionRepoStub stub = new ConcessionRepoStub();
            stub.ClearConcession();
        }

        [TestMethod]
        public void AddConcession()
        {
            Concession concession = new Concession();
            concession.BeginDate = DateTime.Today;
            concession.FinishDate = DateTime.Today.AddMonths(3);
            concession.Observations = "Observaciones";
            repoconcesion.AddConcession(concession);
            Assert.AreEqual(1, repoconcesion.GetAllConcessions().Count);
            Assert.AreEqual("Observaciones", repoconcesion.GetAllConcessions()[0].Observations);
        }

        [TestMethod]
        public void UpdateConcession()
        {
            Concession concession = new Concession();
            concession.BeginDate = DateTime.Today;
            concession.FinishDate = DateTime.Today.AddMonths(3);
            concession.Observations = "Observaciones";
            repoconcesion.AddConcession(concession);
            concession = repoconcesion.GetAllConcessions()[0];
            concession.Observations = "Cambio de observaciones";
            repoconcesion.UpdateConcession(concession);
            concession = repoconcesion.GetAllConcessions()[0];
            Assert.AreEqual("Cambio de observaciones", concession.Observations);
        }

        [TestMethod]
        public void DeleteConcession()
        {
            Concession concession = new Concession();
            concession.BeginDate = DateTime.Today;
            concession.FinishDate = DateTime.Today.AddMonths(3);
            concession.Observations = "Observaciones";
            repoconcesion.AddConcession(concession);
            concession = repoconcesion.GetAllConcessions()[0];
            repoconcesion.DeleteConcession(concession);
            Assert.AreEqual(0, repoconcesion.GetAllConcessions().Count);
        }
    }
}
