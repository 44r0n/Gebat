using Microsoft.VisualStudio.TestTools.UnitTesting;
using GebatModel;
using System.Collections.Generic;
using System;

namespace GebatModelTest
{
    [TestClass]
    public class TestEntryFood
    {
        private IFoodRepository foodrepo;

        private ITypeRepository typerepo;

        private void AddFood(string name, string typename)
        {
            Food food = new Food();
            food.Name = name;
            food.Type = typerepo.SearchType(typename)[0];
            foodrepo.AddFood(food);
        }

        [TestInitialize]
        public void Init()
        {
            foodrepo = new FoodRepository();
            typerepo = new TypeRepository();
            GebatModel.Type type = new GebatModel.Type();
            type.Name = "Kilos";
            typerepo.AddType(type);
            GebatModel.Type type2 = new GebatModel.Type();
            type2.Name = "Paquetes";
            typerepo.AddType(type2);
            AddFood("Arroz", "Kilos");
        }

        [TestCleanup]
        public void Clean()
        {
            EntryFoodStub stubentry = new EntryFoodStub();
            stubentry.ClearEntries();
            TypeRepoStub typestub = new TypeRepoStub();
            typestub.ClearType();
            FoodRepoStub stub = new FoodRepoStub();
            stub.ClearFood();
        }

        [TestMethod]
        public void EntryFoodWithDate()
        {
            Food food = foodrepo.SearchFood("Arroz")[0];
            food.AddQuantity(3, new DateTime(2014, 8, 12));
            food.AddQuantity(2, new DateTime(2014,9,2));
            foodrepo.UpdateFood(food);
            Assert.AreEqual(5, food.Quantity);
            food = foodrepo.SearchFood("Arroz")[0];
            EntryFood[] entry = new EntryFood[2];
            food.EntryFood.CopyTo(entry,0);
            Assert.AreEqual(3, entry[0].Quantity);
            Assert.AreEqual(8, entry[0].Date.Month);
        }

        [TestMethod]
        public void EntryFoodWithNoDate()
        {
            Food food = foodrepo.SearchFood("Arroz")[0];
            food.AddQuantity(3);
            food.AddQuantity(2);
            foodrepo.UpdateFood(food);
            Assert.AreEqual(5, food.Quantity);
        }
    }
}
