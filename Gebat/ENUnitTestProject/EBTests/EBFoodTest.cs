using System;
using GebatEN.Classes;
using GebatCAD.Classes;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ENUnitTestProject.ENTests
{
    [TestClass]
    public class EBFoodTest
    {
        [ClassInitialize()]
        public static void stepasswd(TestContext context)
        {
            ADL.Password = "root";
        }

        [TestMethod]
        public void Read()
        {
            List<object> id = new List<object>();
            id.Add(4);
            EBFood food = (EBFood)(new EBFood("").Read(id));
            Assert.AreEqual("Pomes", food.Name);
            Assert.AreEqual("Kg", food.MyType.Name);
            Assert.AreEqual(2, food.Quantity);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ReadNullId()
        {
            new EBFood("").Read(null);
        }

        [TestMethod]
        public void ReadAll_Insert_Delete()
        {
            List<AEB> general = new EBFood("").ReadAll();
            List<EBFood> expected = new List<EBFood>();
            List<object> idtype = new List<object>();
            idtype.Add(1);
            EBFood p = new EBFood("Patates",(EBType)(new EBType("").Read(idtype)));
            expected.Add(p);
            EBFood s = new EBFood("Tomates",(EBType)(new EBType("").Read(idtype)));
            expected.Add(s);
            EBFood t = new EBFood("Pomes", (EBType)(new EBType("").Read(idtype)));
            expected.Add(t);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Name, ((EBFood)general[i]).Name);
                Assert.AreEqual(expected[i].MyType.Name, ((EBFood)general[i]).MyType.Name);
            }

            EBFood ins = new EBFood("Testing", (EBType)(new EBType("").Read(idtype)));
            ins.Save();
            general = new EBFood("").ReadAll();
            expected.Add(ins);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Name, ((EBFood)general[i]).Name);
                Assert.AreEqual(expected[i].MyType.Name, ((EBFood)general[i]).MyType.Name);
            }

            ins.Delete();
            expected.RemoveAt(3);

            general = new EBFood("").ReadAll();
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Name, ((EBFood)general[i]).Name);
                Assert.AreEqual(expected[i].MyType.Name, ((EBFood)general[i]).MyType.Name);
            }
        }

        [TestMethod]
        public void Control_Food()
        {
            List<object> idfood = new List<object>();
            idfood.Add(2);
            EBFood f = (EBFood)(new EBFood("").Read(idfood));
            f.Add(3,new DateTime(2012,11,09));
            f.Remove(1, new DateTime(2012, 11, 09));
            Assert.AreEqual(2, f.Quantity);

            List<object> foodin = new List<object>();
            foodin.Add(5);
            EBFoodIN fin = (EBFoodIN)(new EBFoodIN().Read(foodin));
            fin.Delete();
            List<object> foodout = new List<object>();
            foodout.Add(4);
            EBFoodOut fout = (EBFoodOut)(new EBFoodOut().Read(foodout));
            fout.Delete();
        }
    }
}
