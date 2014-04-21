using System;
using GebatEN.Classes;
using GebatEN.Enums;
using GebatCAD.Classes;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ENUnitTestProject.ENTests
{
    [TestClass]
    public class EBTBCTest
    {
        [ClassInitialize()]
        public static void setpasswd(TestContext context)
        {
            ADL.Password = "root";
        }

        [TestMethod]
        public void Read()
        {
            List<object> id = new List<object>();
            id.Add(2);
            List<string> phones = new List<string>();
            phones.Add("123456789");
            phones.Add("234567890");
            EBTBC tbc = (EBTBC)(new EBTBC().Read(id));
            Assert.AreEqual("01086932K", tbc.DNI);
            Assert.AreEqual("Ana", tbc.Name);
            Assert.AreEqual("Entrepinares", tbc.Surname);
            Assert.AreEqual(25, tbc.Age);
            Assert.AreEqual(MyGender.Female, tbc.Gender);
            Assert.AreEqual("1/98", tbc.Judgement);
            Assert.AreEqual("Juzgado Valencia", tbc.Court);
            Assert.AreEqual("20/07/2013", tbc.BeginDate.ToShortDateString());
            Assert.AreEqual("10/03/2014", tbc.FinishDate.ToShortDateString());
            Assert.AreEqual(true, tbc.Timetable[DayOfWeek.Monday]);
            Assert.AreEqual(true, tbc.Timetable[DayOfWeek.Tuesday]);
            Assert.AreEqual(true, tbc.Timetable[DayOfWeek.Wednesday]);
            Assert.AreEqual(true, tbc.Timetable[DayOfWeek.Thursday]);
            Assert.AreEqual(true, tbc.Timetable[DayOfWeek.Friday]);
            Assert.AreEqual(false, tbc.Timetable[DayOfWeek.Saturday]);
            Assert.AreEqual(false, tbc.Timetable[DayOfWeek.Sunday]);
            Assert.AreEqual(250, tbc.NumJourney);
            Assert.AreEqual("Pelea", tbc.Crime.Name);
            Assert.AreEqual(phones[0], tbc.Phones[0]);
            Assert.AreEqual(phones[1], tbc.Phones[1]);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ReadNullId()
        {
            EBTBC tbc = (EBTBC)(new EBTBC().Read(null));
        }

        [TestMethod]
        public void ReadAll_Insert_Delete()
        {
            List<AEB> general = new EBTBC().ReadAll();
            List<string> dnis = new List<string>();
            dnis.Add("54508005Y");
            dnis.Add("01086932K");
            dnis.Add("01086932K");

            for (int i = 0; i < dnis.Count; i++)
            {
                Assert.AreEqual(dnis[i], ((EBTBC)general[i]).DNI);
            }
            List<object> crimes = new List<object>();
            crimes.Add(1);
            EBTBC ins = new EBTBC("52835460K", "02/2013", "Manolo", "Hansen", new DateTime(1968, 04, 30), MyGender.Male, "Albacete", new DateTime(2013, 02, 15), new DateTime(2013, 08, 31), new TimeSpan(9,0,0),new TimeSpan(15,30,0), (EBCrime)new EBCrime().Read(crimes));
            ins.Save();
            general = new EBTBC().ReadAll();
            dnis.Add(ins.DNI);
            for (int i = 0; i < dnis.Count; i++)
            {
                Assert.AreEqual(dnis[i], ((EBTBC)general[i]).DNI);
            }

            ins.AddPhone("132456789");

            Assert.AreEqual("132456789", ins.Phones[0]);

            ins.Delete();

            dnis.RemoveAt(3);
            general = new EBTBC().ReadAll();
            for (int i = 0; i < dnis.Count; i++)
            {
                Assert.AreEqual(dnis[i], ((EBTBC)general[i]).DNI);
            }
        }
    }
}
