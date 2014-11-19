using System.Collections.Generic;
using System.Linq;
using Cripto.Util;

namespace GebatModel
{
    public class AdminRepository : BaseRepository , IAdminRepository
    {
        private const string adminExceptionMessage = "There must exist at last one admin.";

        #region//Private Methods
        
        private void checkLastAdmin()
        {
            if(GetAllAdmins().Count == 1)
            {
                throw new AdminException(adminExceptionMessage);
            }
        }

        #endregion

        #region//Public Methods

        
        public List<Admin> GetAllAdmins()
        {
            return this.GetAll<Admin>().ToList();
        }

        
        public void SaveAdmin(Admin admin)
        {
            admin.Password = Hasher.CreateHash(admin.Password);
            this.Save(admin);
        }

        
        public void DeleteAdmin(Admin admin)
        {
            checkLastAdmin();
            this.Delete(admin);
        }

       
        public Admin GetAdmin(string username, string password)
        {
            var admins = this.GetAll<Admin>().Where(admin => admin.Username == username).ToList();
            if(admins.Count() == 1)
            {
                if(Hasher.ValidatePassword(password,admins[0].Password))
                {
                    return admins[0];
                }
            }
            return null;
        }

        #endregion
    }
}
