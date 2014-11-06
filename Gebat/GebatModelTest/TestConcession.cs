using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GebatModel;

namespace GebatModelTest
{
    [TestClass]
    public class TestConcession
    {
        private IConcessionRepository repoconcession;

        [TestInitialize]
        public void Init()
        {
            repoconcession = new ConcessionRepository();
            Concession.SetDate(new DateTime(2014, 8, 14));
        }

        [TestCleanup]
        public void Clean()
        {
            ConcessionRepoStub stub = new ConcessionRepoStub();
            stub.ClearConcession();
        }

        private ConcessionType getFega()
        {
            ConcessionType type = new ConcessionType();
            type.Name = "Fega";
            DateRestriction restriction = new DateRestriction();
            restriction.Interval = 3;
            restriction.Concatenable = true;
            type.DateRestriction = restriction;
            return type;
        }

        [TestMethod]
        public void CheckDatesSimpleFalse()
        {
            Concession concession = new Concession();
            concession.Type = getFega();
            concession.BeginDate = new DateTime(2014, 5, 14);
            concession.FinishDate = new DateTime(2014, 8, 14);
            Assert.AreEqual("Fega", concession.Type.Name);
            Assert.AreEqual(false, concession.IsValid());
        }

        [TestMethod]
        public void CheckDatesTrue()
        {
            Concession concession = new Concession();
            concession.Type = getFega();
            concession.BeginDate = new DateTime(2014, 2, 12);
            concession.FinishDate = new DateTime(2014, 5, 12);
            Assert.AreEqual(true, concession.IsValid());
        }
    }
}
