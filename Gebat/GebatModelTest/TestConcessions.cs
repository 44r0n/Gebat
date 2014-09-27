using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GebatModel;

namespace GebatModelTest
{
    [TestClass]
    public class TestConcessions
    {
        private IConcessionRepository repoconcesion;

        private Concession defaultConcession
        {
            get
            {
                ConcessionType type = new ConcessionType();
                type.Name = "testconcession";
                Concession concession = new Concession();
                concession.BeginDate = new DateTime(2014,9,24);
                concession.FinishDate = concession.BeginDate.AddMonths(3);
                concession.Observations = "Observaciones";
                concession.Type = type;
                return concession;
            }
        }

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
            Concession concession = this.defaultConcession;
            repoconcesion.AddConcession(concession);
            Assert.AreEqual(1, repoconcesion.GetAllConcessions().Count);
            Assert.AreEqual("Observaciones", repoconcesion.GetAllConcessions()[0].Observations);
            Assert.AreEqual("testconcession",repoconcesion.GetAllConcessions()[0].Type.Name);
        }

        [TestMethod]
        public void UpdateConcession()
        {
            Concession concession = this.defaultConcession;
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
            Concession concession = this.defaultConcession;
            repoconcesion.AddConcession(concession);
            concession = repoconcesion.GetAllConcessions()[0];
            repoconcesion.DeleteConcession(concession);
            Assert.AreEqual(0, repoconcesion.GetAllConcessions().Count);
        }
    }
}
