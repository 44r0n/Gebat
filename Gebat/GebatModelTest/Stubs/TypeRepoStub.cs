using GebatModel;

namespace GebatModelTest
{
    public class TypeRepoStub : TypeRepository
    {
        public void ClearType()
        {
            GebatDataBaseEntities context = new GebatDataBaseEntities();
            context.Database.ExecuteSqlCommand("DELETE FROM Type");
        }
    }
}
