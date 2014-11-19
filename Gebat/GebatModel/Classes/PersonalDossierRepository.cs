using System.Linq;
using System.Collections.Generic;

namespace GebatModel
{
    public class PersonalDossierRepository : BaseRepository , IPersonalDossierRepository
    {
        private const string minimumConcessionsExceptionMessage = "There must be at last one concession attached to a dossier.";

        private const string minimumFamiliarConcessionExceptionMessage = "There must be at last one familiar attached to a dossier.";

        #region//Private Methods

        private void checkDossier(PersonalDossier dossier)
        {
            if (dossier.Concessions.Count == 0)
            {
                throw new MinimumConcessionsException(minimumConcessionsExceptionMessage);
            }
            if (dossier.Familiars.Count == 0)
            {
                throw new MinimumFamiliarConcessionException(minimumFamiliarConcessionExceptionMessage);
            }
        }

        #endregion

        #region//Public Methods

        
        public List<PersonalDossier> GetAllDossiers()
        {
            return this.GetAll<PersonalDossier>().ToList();
        }

        
        public void SaveDossier(PersonalDossier dossier)
        {
            checkDossier(dossier);
            this.Save(dossier);
        }

        
        public void UpdateDossier(PersonalDossier dossier)
        {
            this.Update(dossier);
        }

        
        public void DeleteDossier(PersonalDossier dossier)
        {
            this.Delete(dossier);
        }

        #endregion
    }
}
