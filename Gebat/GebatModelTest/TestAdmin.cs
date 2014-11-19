using Microsoft.VisualStudio.TestTools.UnitTesting;
using GebatModel;
using System.Collections.Generic;

namespace GebatModelTest
{
    [TestClass]
    public class TestAdmin
    {
        private IAdminRepository adminrepo;

        private Admin AddAdmin(string name, string password)
        {
            Admin administrador = new Admin();
            administrador.Username = name;
            administrador.Password = password;
            adminrepo.SaveAdmin(administrador);
            return administrador;
        }

        [TestInitialize]
        public void Init()
        {
            adminrepo = new AdminRepository();
        }

        [TestCleanup]
        public void Clean()
        {
            AdminRepoStub stub = new AdminRepoStub();
            stub.ClearAdmin();
        }

        [TestMethod]
        public void TestAddAdmin()
        {
            AddAdmin("Maria", "contra");
            List<Admin> admins = adminrepo.GetAllAdmins();
            Assert.AreEqual(1, admins.Count);
            Assert.AreEqual("Maria", admins[0].Username);
        }

        [TestMethod]
        public void TestNoAdmins()
        {
            List<Admin> admins = adminrepo.GetAllAdmins();
            Assert.AreEqual(0, admins.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(GebatModel.AdminException))]
        public void DeleteOnlyAdmin()
        {
            Admin administrador = AddAdmin("Maria", "contra");
            adminrepo.DeleteAdmin(administrador);
        }

        [TestMethod]
        public void DeleteAdmin()
        {
            AddAdmin("Maria", "contra");
            AddAdmin("Aaron", "otracontra");
            List<Admin> admins = adminrepo.GetAllAdmins();
            adminrepo.DeleteAdmin(admins[0]);
            admins = adminrepo.GetAllAdmins();
            Assert.AreEqual(1, admins.Count);
        }
    }
}
