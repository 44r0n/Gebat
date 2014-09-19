using Microsoft.VisualStudio.TestTools.UnitTesting;
using GebatModel;
using System.Collections.Generic;

namespace GebatModelTest
{
    [TestClass]
    public class TestType
    {
        private ITypeRepository repotype;

        private Type AddType(string name)
        {
            Type type = new Type();
            type.Name = "Kilos";
            repotype.AddType(type);
            return type;
        }

        [TestInitialize]
        public void Init()
        {
            repotype = new TypeRepository();
        }

        [TestCleanup]
        public void Clean()
        {
            TypeRepoStub stub = new TypeRepoStub();
            stub.ClearType();
        }

        [TestMethod]
        public void TestAddType()
        {
            AddType("Kilos");
            List<Type> types = repotype.GetAllTypes();
            Assert.AreEqual(1, types.Count);
            Assert.AreEqual("Kilos", types[0].Name);
        }

        [TestMethod]
        public void TestNoTypes()
        {
            List<Type> types = repotype.GetAllTypes();
            Assert.AreEqual(0, types.Count);
        }

        [TestMethod]
        public void SearchAndUpdateType()
        {
            AddType("Kilos");
            List<Type> list = repotype.SearchType("Kilo");
            Type update = list[0];
            update.Name = "Litros";
            repotype.UpdateType(update);
            List<Type> all = repotype.GetAllTypes();
            Assert.AreEqual(1, all.Count);
            Assert.AreEqual("Litros", all[0].Name);
        }

        [TestMethod]
        public void DeleteType()
        {
            AddType("Kilos");
            List<Type> types = repotype.GetAllTypes();
            repotype.DeleteType(types[0]);
            Assert.AreEqual(0, repotype.GetAllTypes().Count);
        }
    }
}
