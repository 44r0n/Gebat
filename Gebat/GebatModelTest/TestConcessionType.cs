using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GebatModel;

namespace GebatModelTest
{
    [TestClass]
    public class TestConcessionType
    {
        [TestMethod]
        public void SetAndCheckType()
        {
            IRestriction restriction = new DateRestriction(3);
            ConcessionType type = new ConcessionType();
            type.Name = "Testing";
            type.AddRestriction(restriction);
            Concession concession = new Concession();
            concession.BeginDate = new DateTime(2014, 5, 14);
            concession.FinishDate = new DateTime(2014, 8, 14);
            Assert.AreEqual(false, type.CheckAll(concession));
            Assert.AreEqual("Testing", type.Name);
        }
    }
}
