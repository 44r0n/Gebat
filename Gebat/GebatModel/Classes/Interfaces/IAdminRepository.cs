using System.Collections.Generic;

namespace GebatModel
{
    public interface IAdminRepository
    {
        
        List<Admin> GetAllAdmins();

        
        void SaveAdmin(Admin admin);

        
        void DeleteAdmin(Admin admin);

        
        Admin GetAdmin(string Username, string password);
    }
}
