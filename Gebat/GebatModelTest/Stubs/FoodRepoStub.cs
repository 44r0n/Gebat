using GebatModel;

namespace GebatModelTest
{
    public class FoodRepoStub : FoodRepository
    {
        public void ClearFood()
        {
            GebatDataBaseEntities context = new GebatDataBaseEntities();
            context.Database.ExecuteSqlCommand("DELETE FROM Food");
        }
    }
}
