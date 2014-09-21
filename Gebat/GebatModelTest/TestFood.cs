using Microsoft.VisualStudio.TestTools.UnitTesting;
using GebatModel;
using System.Collections.Generic;
using System;

namespace GebatModelTest
{
    [TestClass]
    public class TestFood
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

        [TestCleanup]
        public void Clean()
        {
            EntryFoodStub stubentry = new EntryFoodStub();
            stubentry.ClearEntries();
            OutgoingFoodStub stubout = new OutgoingFoodStub();
            stubout.ClearOutgoing();
            TypeRepoStub stubtype = new TypeRepoStub();
            stubtype.ClearType();
            FoodRepoStub stub = new FoodRepoStub();
            stub.ClearFood();
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

        [TestMethod]
        public void TestAddFood()
        {
            List<Food> foodlist = foodrepo.GetAllFood();
            Assert.AreEqual(1, foodlist.Count);
            Assert.AreEqual("Arroz", foodlist[0].Name);
        }

        [TestMethod]
        public void TestUpdateFood()
        {
            List<Food> foodlist = foodrepo.GetAllFood();
            Food f1 = foodlist[0];
            f1.Name = "Galletas";
            f1.Type = typerepo.SearchType("Paquete")[0];
            foodrepo.UpdateFood(f1);
            List<Food> list = foodrepo.GetAllFood();
            Assert.AreEqual("Galletas", list[0].Name);
        }

        [TestMethod]
        public void DeleteFood()
        {
            List<Food> foodlist = foodrepo.GetAllFood();
            foodrepo.DeleteFood(foodlist[0]);
            foodlist = foodrepo.GetAllFood();
            Assert.AreEqual(0, foodlist.Count);
        }

        [TestMethod]
        public void SearchAddRemoveQuantity()
        {
            Food food = foodrepo.SearchFood("Arro")[0];
            food.AddQuantity(3);
            food.RemoveQuantity(1);
            foodrepo.UpdateFood(food);
            food = foodrepo.SearchFood("Arro")[0];
            Assert.AreEqual(2, food.Quantity);
        }

        [TestMethod]
        public void EntryOutgoingFoodWithDate()
        {
            Food food = foodrepo.SearchFood("Arroz")[0];
            food.AddQuantity(3, new DateTime(2014, 8, 12));
            food.AddQuantity(2, new DateTime(2014, 9, 2));
            foodrepo.UpdateFood(food);
            Assert.AreEqual(5, food.Quantity);
            food = foodrepo.SearchFood("Arroz")[0];
            EntryFood[] entry = new EntryFood[food.EntryFood.Count];
            food.EntryFood.CopyTo(entry, 0);
            Assert.AreEqual(3, entry[0].Quantity);
            Assert.AreEqual(8, entry[0].Date.Month);
            food.RemoveQuantity(4, new DateTime(2014, 9, 5));
            foodrepo.UpdateFood(food);
            food = foodrepo.SearchFood("Arroz")[0];
            Assert.AreEqual(1, food.Quantity);
            OutgoingFood[] outgoing = new OutgoingFood[food.OutgoingFood.Count];
            food.OutgoingFood.CopyTo(outgoing,0);
            Assert.AreEqual(4, outgoing[0].Quantity);
            Assert.AreEqual(5, outgoing[0].Date.Day);
        }

        [TestMethod]
        public void EntryOutgoingFoodWithNoDate()
        {
            Food.SetToday(new DateTime(2014, 9, 21));
            Food food = foodrepo.SearchFood("Arroz")[0];
            food.AddQuantity(3);
            food.AddQuantity(2);
            foodrepo.UpdateFood(food);
            Assert.AreEqual(5, food.Quantity);
            food = foodrepo.SearchFood("Arroz")[0];
            EntryFood[] entry = new EntryFood[2];
            food.EntryFood.CopyTo(entry, 0);
            Assert.AreEqual(3, entry[0].Quantity);
            Assert.AreEqual(9, entry[0].Date.Month);
            food.RemoveQuantity(4);
            foodrepo.UpdateFood(food);
            food = foodrepo.SearchFood("Arroz")[0];
            Assert.AreEqual(1, food.Quantity);
            OutgoingFood[] outgoing = new OutgoingFood[food.OutgoingFood.Count];
            food.OutgoingFood.CopyTo(outgoing, 0);
            Assert.AreEqual(4, outgoing[0].Quantity);
            Assert.AreEqual(21, outgoing[0].Date.Day);
        }
    }
}
