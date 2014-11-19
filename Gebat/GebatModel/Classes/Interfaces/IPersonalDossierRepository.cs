using System.Collections.Generic;

namespace GebatModel
{
    public interface IPersonalDossierRepository
    {
        
        List<PersonalDossier> GetAllDossiers();

        
        void SaveDossier(PersonalDossier dossier);

        
        void UpdateDossier(PersonalDossier dossier);

        
        void DeleteDossier(PersonalDossier dossier);
    }
}
