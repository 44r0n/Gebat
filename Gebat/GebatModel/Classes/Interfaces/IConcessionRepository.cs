using System.Collections.Generic;

namespace GebatModel
{
    public interface IConcessionRepository
    {
        /// <summary>
        /// Gets all the concessions from the database.
        /// </summary>
        /// <returns>List of Concessions.</returns>
        List<Concession> GetAllConcessions();

        /// <summary>
        /// Adds a concession into the database.
        /// </summary>
        /// <param name="concession">COncession to add to the database.</param>
        void AddConcession(Concession concession);

        /// <summary>
        /// Updates a concession from the database.
        /// </summary>
        /// <param name="concession">Concession to update from the database.</param>
        void UpdateConcession(Concession concession);

        /// <summary>
        /// Deletes a Concession from the database.
        /// </summary>
        /// <param name="concession">Concession to delete from the database.</param>
        void DeleteConcession(Concession concession);
    }
}
