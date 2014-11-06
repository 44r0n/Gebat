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

        /// <summary>
        /// Gets all the admins of the database.
        /// </summary>
        /// <returns>A list of admins.</returns>
        public List<Admin> GetAllAdmins()
        {
            return this.GetAll<Admin>().ToList();
        }

        /// <summary>
        /// Adds an admin into the database.
        /// </summary>
        /// <param name="admin">Admin to add to the database.</param>
        public void AddAdmin(Admin admin)
        {
            admin.Password = Hasher.CreateHash(admin.Password);
            this.Add(admin);
        }

        /// <summary>
        /// Deletes an admin from the database.
        /// </summary>
        /// <param name="admin">Admin to delete from the databse.</param>
        public void DeleteAdmin(Admin admin)
        {
            checkLastAdmin();
            this.Delete(admin);
        }

        /// <summary>
        /// Gets an Admin with the given Username and Password if exists in the database.
        /// </summary>
        /// <param name="Username">Username of the Admin</param>
        /// <param name="password">Password of the Admin</param>
        /// <returns>Admin with the given Username and Password or null.</returns>
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
