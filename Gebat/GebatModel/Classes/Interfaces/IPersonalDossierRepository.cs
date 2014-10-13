using System.Collections.Generic;

namespace GebatModel
{
    public interface IPersonalDossierRepository
    {
        /// <summary>
        /// Gets all the PersonalDossiers from the database.
        /// </summary>
        /// <returns>List of persona dossiers</returns>
        List<PersonalDossier> GetAllDossiers();

        /// <summary>
        /// Adds a personal dossier into the database.
        /// </summary>
        /// <param name="dossier">PersonalDossier to add to the database.</param>
        void AddDossier(PersonalDossier dossier);

        /// <summary>
        /// Updates a personal dossier in the database.
        /// </summary>
        /// <param name="dossier">PersonalDossier to update in the database.</param>
        void UpdateDossier(PersonalDossier dossier);

        /// <summary>
        /// Deletes a personal dossier in the database.
        /// </summary>
        /// <param name="dossier">PersonalDossier to delete in the database.</param>
        void DeleteDossier(PersonalDossier dossier);
    }
}
