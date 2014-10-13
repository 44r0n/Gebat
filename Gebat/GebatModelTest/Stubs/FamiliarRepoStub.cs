using GebatModel;

namespace GebatModelTest
{
    public class FamiliarRepoStub
    {
        public void ClearFamiliars()
        {
            GebatDataBaseEntities context = new GebatDataBaseEntities();
            context.Database.ExecuteSqlCommand("DELETE FROM Person_Familiar");
            context.Database.ExecuteSqlCommand("DELETE FROM Person");
        }
    }
}
