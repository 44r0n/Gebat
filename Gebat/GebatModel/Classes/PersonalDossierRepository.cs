﻿using System.Linq;
using System.Collections.Generic;

namespace GebatModel
{
    public class PersonalDossierRepository : BaseRepository , IPersonalDossierRepository
    {
        /// <summary>
        /// Gets all the PersonalDossiers from the database.
        /// </summary>
        /// <returns>List of persona dossiers</returns>
        public List<PersonalDossier> GetAllDossiers()
        {
            return this.GetAll<PersonalDossier>().ToList();
        }

        /// <summary>
        /// Adds a personal dossier into the database.
        /// </summary>
        /// <param name="dossier">PersonalDossier to add to the database.</param>
        public void AddDossier(PersonalDossier dossier)
        {
            if(dossier.Concessions.Count == 0)
            {
                throw new MinimumConcessionsException("There must be at last one concession attached to a dossier.");
            }
            if(dossier.Familiar.Count == 0)
            {
                throw new MinimumFamiliarConcession("There must be at last one familiar attached to a dossier.");
            }
            this.Add(dossier);
        }

        /// <summary>
        /// Updates a personal dossier in the database.
        /// </summary>
        /// <param name="dossier">PersonalDossier to update in the database.</param>
        public void UpdateDossier(PersonalDossier dossier)
        {
            this.Update(dossier);
        }

        /// <summary>
        /// Deletes a personal dossier in the database.
        /// </summary>
        /// <param name="dossier">PersonalDossier to delete in the database.</param>
        public void DeleteDossier(PersonalDossier dossier)
        {
            this.Delete(dossier);
        }
    }
}
