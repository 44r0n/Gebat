using System.Collections.Generic;
using GebatModel;

namespace GebatModelTest
{
    class AdminRepoStub : AdminRepository
    {
        public void ClearAdmin()
        {
            GebatDataBaseEntities context = new GebatDataBaseEntities();
            context.Database.ExecuteSqlCommand("DELETE FROM Admin");
        }

    }
}
