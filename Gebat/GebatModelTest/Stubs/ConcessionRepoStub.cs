using GebatModel;

namespace GebatModelTest
{
    public class ConcessionRepoStub : ConcessionRepository
    {
        public void ClearConcession()
        {
            GebatDataBaseEntities context = new GebatDataBaseEntities();
            context.Database.ExecuteSqlCommand("DELETE FROM Concession");
        }
    }
}
