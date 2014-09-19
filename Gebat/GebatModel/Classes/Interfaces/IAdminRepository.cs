using System.Collections.Generic;

namespace GebatModel
{
    public interface IAdminRepository
    {
        /// <summary>
        /// Gets all the admins from the database.
        /// </summary>
        /// <returns>List of Admins.</returns>
        List<Admin> GetAllAdmins();

        /// <summary>
        /// Adds an admin into the database.
        /// </summary>
        /// <param name="admin">Admin to add to the database.</param>
        void AddAdmin(Admin admin);

        /// <summary>
        /// Deletes an admin from the database.
        /// </summary>
        /// <param name="admin">Admin to delete from the databse.</param>
        void DeleteAdmin(Admin admin);

        /// <summary>
        /// Gets an Admin with the given Username and Password if exists in the database.
        /// </summary>
        /// <param name="Username">Username of the Admin</param>
        /// <param name="password">Password of the Admin</param>
        /// <returns>Admin with the given Username and Password or null.</returns>
        Admin GetAdmin(string Username, string password);
    }
}
