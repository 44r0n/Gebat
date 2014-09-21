using GebatModel;
namespace GebatModelTest
{
    public class EntryFoodStub
    {
        public void ClearEntries()
        {
            GebatDataBaseEntities context = new GebatDataBaseEntities();
            context.Database.ExecuteSqlCommand("DELETE FROM EntryFood");
        }
    }
}
