using System.Collections.Generic;

namespace GebatModel
{
    public interface IFamiliarRespository
    {
        /// <summary>
        /// Gets all the familiars from the database.
        /// </summary>
        /// <returns>List of familiars.</returns>
        List<Familiar> GetAllFamiliars();

        /// <summary>
        /// Updates a familiar in the database.
        /// </summary>
        /// <param name="familiar">Familiar to update in the database.</param>
        void UpdateFamiliar(Familiar familiar);

        /// <summary>
        /// Deletes a familiar from the database.
        /// </summary>
        /// <param name="familiar">Familiar to delete from the database.</param>
        void DeleteFamiliar(Familiar familiar);

        /// <summary>
        /// Gets a familiar with the given DNI.
        /// </summary>
        /// <param name="DNI">DNI of the familiar.</param>
        /// <returns>List of familiars with the given DNI.</returns>
        List<Familiar> GetFamiliar(string DNI);
    }
}
