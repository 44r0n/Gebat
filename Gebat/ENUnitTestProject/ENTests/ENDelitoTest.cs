﻿using System;
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
            AADL.Password = "root";
        }

        [TestMethod]
        public void Read()
        {
            List<int> id = new List<int>();
            id.Add(2);
            EBCrime delito = (EBCrime)new EBCrime().Read(id);
            Assert.AreEqual("Pelea", delito.Name);
            Assert.AreEqual(2, delito.Id[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ReadNullId()
        {
            EBCrime delito = (EBCrime)new EBCrime().Read(null);
        }

        [TestMethod]
        public void ReadAll_Insert_Delete()
        {
            List<AEB> general = new EBCrime().ReadAll();
            List<EBCrime> expected = new List<EBCrime>();
            EBCrime p = new EBCrime("Robo");
            expected.Add(p);
            EBCrime s = new EBCrime("Pelea");
            expected.Add(s);
            EBCrime t = new EBCrime("Otro");
            expected.Add(t);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Name, ((EBCrime)general[i]).Name);
            }
            EBCrime ins = new EBCrime("De prueba");
            ins.Save();
            general = new EBCrime().ReadAll();
            expected.Add(ins);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Name, ((EBCrime)general[i]).Name);
            }

            ins.Delete();
            expected.RemoveAt(3);

            general = new EBCrime().ReadAll();
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Name, ((EBCrime)general[i]).Name);
            }
        }
    }
}
