using GebatModel;

namespace GebatModelTest
{
    public class DossierStub
    {
        public void ClearDossiers()
        {
            GebatDataBaseEntities context = new GebatDataBaseEntities();
            context.Database.ExecuteSqlCommand("DELETE FROM PersonalDossier");
        }
    }
}
