using System;
using GebatEN.Classes;
using GebatCAD.Classes;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ENUnitTestProject.ENTests
{
    [TestClass]
    public class ENFoodTest
    {
        [ClassInitialize()]
        public static void stepasswd(TestContext context)
        {
            ACAD.Password = "root";
        }

        [TestMethod]
        public void Read()
        {
            List<int> id = new List<int>();
            id.Add(4);
            ENFood comida = (ENFood)(new ENFood("").Read(id));
            Assert.AreEqual("Pomes", comida.Name);
            Assert.AreEqual("Kg", comida.MyType.Name);
            Assert.AreEqual(2, comida.Quantity);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ReadNullId()
        {
            new ENFood("").Read(null);
        }

        [TestMethod]
        public void ReadAll_Insert_Delete()
        {
            List<AEN> general = new ENFood("").ReadAll();
            List<ENFood> expected = new List<ENFood>();
            List<int> idtype = new List<int>();
            idtype.Add(1);
            ENFood p = new ENFood("Patates",(ENType)(new ENType("").Read(idtype)));
            expected.Add(p);
            ENFood s = new ENFood("Tomates",(ENType)(new ENType("").Read(idtype)));
            expected.Add(s);
            ENFood t = new ENFood("Pomes", (ENType)(new ENType("").Read(idtype)));
            expected.Add(t);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Name, ((ENFood)general[i]).Name);
                Assert.AreEqual(expected[i].MyType.Name, ((ENFood)general[i]).MyType.Name);
            }

            ENFood ins = new ENFood("Testing", (ENType)(new ENType("").Read(idtype)));
            ins.Save();
            general = new ENFood("").ReadAll();
            expected.Add(ins);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Name, ((ENFood)general[i]).Name);
                Assert.AreEqual(expected[i].MyType.Name, ((ENFood)general[i]).MyType.Name);
            }

            ins.Delete();
            expected.RemoveAt(3);

            general = new ENFood("").ReadAll();
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Name, ((ENFood)general[i]).Name);
                Assert.AreEqual(expected[i].MyType.Name, ((ENFood)general[i]).MyType.Name);
            }
        }

        [TestMethod]
        public void Control_Food()
        {
            List<int> idfood = new List<int>();
            idfood.Add(2);
            ENFood f = (ENFood)(new ENFood("").Read(idfood));
            f.Add(3,new DateTime(2012,11,09));
            f.Remove(1, new DateTime(2012, 11, 09));
            Assert.AreEqual(2, f.Quantity);

            List<int> foodin = new List<int>();
            foodin.Add(5);
            ENFoodIN fin = (ENFoodIN)(new ENFoodIN().Read(foodin));
            fin.Delete();
            List<int> foodout = new List<int>();
            foodout.Add(4);
            ENFoodOut fout = (ENFoodOut)(new ENFoodOut().Read(foodout));
            fout.Delete();
        }
    }
}
