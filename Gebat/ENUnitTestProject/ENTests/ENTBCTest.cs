using System;
using GebatEN.Classes;
using GebatCAD.Classes;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ENUnitTestProject.ENTests
{
    [TestClass]
    public class ENTBCTest
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
            ENTBC tbc = (ENTBC)(new ENTBC().Read(id));
            Assert.AreEqual("01086932K", tbc.DNI);
            Assert.AreEqual("Ana", tbc.Nombre);
            Assert.AreEqual("Entrepinares", tbc.Apellidos);
            Assert.AreEqual("1/98", tbc.Ejecutoria);
            Assert.AreEqual("Juzgado Valencia", tbc.Juzgado);
            Assert.AreEqual("20/07/2013", tbc.FInicio.ToShortDateString());
            Assert.AreEqual("10/03/2014", tbc.FFin.ToShortDateString());
            Assert.AreEqual(true, tbc.Horario[DayOfWeek.Monday]);
            Assert.AreEqual(true, tbc.Horario[DayOfWeek.Tuesday]);
            Assert.AreEqual(true, tbc.Horario[DayOfWeek.Wednesday]);
            Assert.AreEqual(true, tbc.Horario[DayOfWeek.Thursday]);
            Assert.AreEqual(true, tbc.Horario[DayOfWeek.Friday]);
            Assert.AreEqual(false, tbc.Horario[DayOfWeek.Saturday]);
            Assert.AreEqual(false, tbc.Horario[DayOfWeek.Sunday]);
            Assert.AreEqual(250, tbc.NumJornadas);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ReadNullId()
        {
            ENTBC tbc = (ENTBC)(new ENTBC().Read(null));
        }

        [TestMethod]
        public void ReadAll_Insert_Delete()
        {
            List<AEN> general = new ENTBC().ReadAll();
            List<string> dnis = new List<string>();
            dnis.Add("54508005Y");
            dnis.Add("01086932K");
            dnis.Add("01086932K");

            for (int i = 0; i < dnis.Count; i++)
            {
                Assert.AreEqual(dnis[i], ((ENTBC)general[i]).DNI);
            }

            ENTBC ins = new ENTBC("52835460K", "02/2013", "Manolo", "Hansen", "Albacete", new DateTime(2013, 02, 15), new DateTime(2013, 08, 31));
            ins.Save();
            general = new ENTBC().ReadAll();
            dnis.Add(ins.DNI);
            for (int i = 0; i < dnis.Count; i++)
            {
                Assert.AreEqual(dnis[i], ((ENTBC)general[i]).DNI);
            }

            ins.Delete();
            dnis.RemoveAt(4);
            general = new ENTBC().ReadAll();
            for (int i = 0; i < dnis.Count; i++)
            {
                Assert.AreEqual(dnis[i], ((ENTBC)general[i]).DNI);
            }
        }
    }
}
