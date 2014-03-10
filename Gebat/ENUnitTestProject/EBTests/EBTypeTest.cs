using System;
using GebatEN.Classes;
using GebatCAD.Classes;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ENUnitTestProject.ENTests
{
    [TestClass]
    public class EBTypeTest
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
            id.Add(2);
            EBType type = (EBType)(new EBType("").Read(id));
            Assert.AreEqual("Litros", type.Name);
            Assert.AreEqual(2, type.Id[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ReadNullId()
        {
            EBType type = (EBType)(new EBType("").Read(null));
        }

        [TestMethod]
        public void ReadAll_Insert_Delete()
        {
            List<AEB> general = new EBType("").ReadAll();
            List<EBType> expected = new List<EBType>();
            EBType p = new EBType("Kg");
            expected.Add(p);
            EBType s = new EBType("Litros");
            expected.Add(s);
            EBType t = new EBType("Paquetes");
            expected.Add(t);
            for(int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Name, ((EBType)general[i]).Name);
            }
            EBType ins = new EBType("De prueba");
            ins.Save();
            general = new EBType("").ReadAll();
            expected.Add(ins);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Name, ((EBType)general[i]).Name);
            }

            ins.Delete();
            expected.RemoveAt(3);

            general = new EBType("").ReadAll();
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Name, ((EBType)general[i]).Name);
            }
        }
    }
}
