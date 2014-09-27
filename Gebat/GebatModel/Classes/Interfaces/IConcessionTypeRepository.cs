using System.Collections.Generic;

namespace GebatModel
{
    public interface IConcessionTypeRepository
    {
        /// <summary>
        /// Gets all the concession types from the database.
        /// </summary>
        /// <returns>List of concession types.</returns>
        List<ConcessionType> GetAllConcessionTypes();

        /// <summary>
        /// Adds concession type into the database.
        /// </summary>
        /// <param name="type">ConcessionTypeto addto the database.</param>
        void AddConcessionType(ConcessionType type);

        /// <summary>
        /// Updates concession type from the database.
        /// </summary>
        /// <param name="type">ConcessionType to update from the database.</param>
        void UpdateConcessionType(ConcessionType type);

        /// <summary>
        /// Deletes concession type from the database.
        /// </summary>
        /// <param name="type">ConcessionType to delete from the database.</param>
        void DeleteConcessionType(ConcessionType type);

        /// <summary>
        /// Searches the ConcessionTypein the database with the given name.
        /// </summary>
        /// <param name="name">Name of the concession type.</param>
        /// <returns>List of ConcessionType with the given name.</returns>
        List<ConcessionType> SearchConcessionType(string name);
    }
}
