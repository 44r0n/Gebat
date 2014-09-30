using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GebatModel;

namespace GebatModelTest
{
    [TestClass]
    public class TestConcessionType
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
        public void SetAndCheckType()
        {
            DateRestriction.SetDate(new DateTime(2014, 8, 14));
            DateRestriction restriction = new DateRestriction(3);
            ConcessionType type = new ConcessionType();
            type.Name = "Testing";
            type.DateRestriction = restriction;
            Concession concession = new Concession();
            concession.BeginDate = new DateTime(2014, 5, 14);
            concession.FinishDate = new DateTime(2014, 8, 14);
            Assert.AreEqual(false, type.CheckAll(concession));
            Assert.AreEqual("Testing", type.Name);
        }

        [TestMethod]
        public void CheckDateFalse()
        {
            DateRestriction.SetDate(new DateTime(2014, 8, 14));
            DateRestriction restriction = new DateRestriction(3);
            ConcessionType type = new ConcessionType();
            type.Name = "Fega";
            type.DateRestriction = restriction;
            Concession concession = new Concession();
            concession.BeginDate = new DateTime(2014, 5, 12);
            concession.FinishDate = new DateTime(2014, 8, 12);
            Assert.AreEqual(false, type.CheckAll(concession));
        }

        [TestMethod]
        public void CheckDateTrue()
        {
            DateRestriction.SetDate(new DateTime(2014, 8, 14));
            DateRestriction restriction = new DateRestriction(3);
            ConcessionType type = new ConcessionType();
            type.Name = "Fega";
            type.DateRestriction = restriction;
            Concession concession = new Concession();
            concession.BeginDate = new DateTime(2014,2,12);
            concession.FinishDate = new DateTime(2014, 5, 12);
            Assert.AreEqual(true, type.CheckAll(concession));
        }
    }
}
