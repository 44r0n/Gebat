using System;
using GebatEN.Classes;
using GebatCAD.Classes;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ENUnitTestProject.ENTests
{
    [TestClass]
    public class ENTypeTest
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
            id.Add(2);
            ENType tipo = (ENType)(new ENType("").Read(id));
            Assert.AreEqual("Litros", tipo.Name);
            Assert.AreEqual(2, tipo.Id[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ReadNullId()
        {
            ENType tipo = (ENType)(new ENType("").Read(null));
        }

        [TestMethod]
        public void ReadAll_Insert_Delete()
        {
            List<AEN> general = new ENType("").ReadAll();
            List<ENType> expected = new List<ENType>();
            ENType p = new ENType("Kg");
            expected.Add(p);
            ENType s = new ENType("Litros");
            expected.Add(s);
            ENType t = new ENType("Paquetes");
            expected.Add(t);
            for(int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Name, ((ENType)general[i]).Name);
            }
            ENType ins = new ENType("De prueba");
            ins.Save();
            general = new ENType("").ReadAll();
            expected.Add(ins);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Name, ((ENType)general[i]).Name);
            }

            ins.Delete();
            expected.RemoveAt(3);

            general = new ENType("").ReadAll();
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Name, ((ENType)general[i]).Name);
            }
        }
    }
}
