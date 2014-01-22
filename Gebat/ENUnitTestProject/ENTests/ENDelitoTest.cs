using System;
using GebatEN.Classes;
using GebatCAD.Classes;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ENUnitTestProject.ENTests
{
    [TestClass]
    public class ENDelitoTest
    {
        [ClassInitialize()]
        public static void setpasswd(TestContext context)
        {
            ACAD.Password = "root";
        }

        [TestMethod]
        public void Read()
        {
            List<int> id = new List<int>();
            id.Add(2);
            ENDelito delito = (ENDelito)new ENDelito().Read(id);
            Assert.AreEqual("Pelea", delito.Name);
            Assert.AreEqual(2, delito.Id[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ReadNullId()
        {
            ENDelito delito = (ENDelito)new ENDelito().Read(null);
        }

        [TestMethod]
        public void ReadAll_Insert_Delete()
        {
            List<AEN> general = new ENDelito().ReadAll();
            List<ENDelito> expected = new List<ENDelito>();
            ENDelito p = new ENDelito("Robo");
            expected.Add(p);
            ENDelito s = new ENDelito("Pelea");
            expected.Add(s);
            ENDelito t = new ENDelito("Otro");
            expected.Add(t);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Name, ((ENDelito)general[i]).Name);
            }
            ENDelito ins = new ENDelito("De prueba");
            ins.Save();
            general = new ENDelito().ReadAll();
            expected.Add(ins);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Name, ((ENDelito)general[i]).Name);
            }

            ins.Delete();
            expected.RemoveAt(3);

            general = new ENDelito().ReadAll();
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Name, ((ENDelito)general[i]).Name);
            }
        }
    }
}
