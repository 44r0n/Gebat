using GebatModel;
namespace GebatModelTest
{
    public class OutgoingFoodStub
    {
        public void ClearOutgoing()
        {
            GebatDataBaseEntities context = new GebatDataBaseEntities();
            context.Database.ExecuteSqlCommand("DELETE FROM OutgoingFood");
        }
    }
}
