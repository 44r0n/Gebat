using Microsoft.VisualStudio.TestTools.UnitTesting;
using GebatModel;
using System.Collections.Generic;

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
            Type type = new Type();
            type.Name = "Kilos";
            typerepo.AddType(type);
            Type type2 = new Type();
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
            Assert.AreEqual(2, food.Quantity);
        }
    }
}
