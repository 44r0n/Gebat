using System.Collections.Generic;
using System.Linq;

namespace GebatModel
{
    public class FamiliarRespository : BaseRepository , IFamiliarRespository
    {
        /// <summary>
        /// Gets all the familiars from the database.
        /// </summary>
        /// <returns>List of familiars.</returns>
        public List<Familiar> GetAllFamiliars()
        {
            return this.GetAll<Familiar>().ToList();
        }

        /// <summary>
        /// Updates a familiar in the database.
        /// </summary>
        /// <param name="familiar">Familiar to update in the database.</param>
        public void UpdateFamiliar(Familiar familiar)
        {
            this.Update(familiar);
        }

        /// <summary>
        /// Deletes a familiar from the database.
        /// </summary>
        /// <param name="familiar">Familiar to delete from the database.</param>
        public void DeleteFamiliar(Familiar familiar)
        {
            this.Delete(familiar);
        }

        /// <summary>
        /// Gets a familiar with the given DNI.
        /// </summary>
        /// <param name="DNI">DNI of the familiar.</param>
        /// <returns>Familiar with the given DNI or null.</returns>
        public List<Familiar> GetFamiliar(string DNI)
        {
            return this.GetAll<Familiar>().Where(familiar => familiar.DNI.Contains(DNI)).ToList();
        }
    }
}
