using GebatModel;

namespace GebatModelTest
{
    public class DateRestrictionStub
    {
        public void ClearDateRestriction()
        {
            GebatDataBaseEntities context = new GebatDataBaseEntities();
            context.Database.ExecuteSqlCommand("DELETE FROM DateRestriction");
        }
    }
}
