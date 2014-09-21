using System.Collections.Generic;

namespace GebatModel
{
    public interface ITypeRepository
    {
        /// <summary>
        /// Gets all the types from the database.
        /// </summary>
        /// <returns>List of Types.</returns>
        List<Type> GetAllTypes();

        /// <summary>
        /// Adds a type into the database.
        /// </summary>
        /// <param name="type">Type to add to the database.</param>
        void AddType(Type type);

        /// <summary>
        /// Updates a type from the database.
        /// </summary>
        /// <param name="type">Type to update from the database.</param>
        void UpdateType(Type type);

        /// <summary>
        /// Deletes a Type from the database.
        /// </summary>
        /// <param name="type">Type to delete from the database.</param>
        void DeleteType(Type type);

        /// <summary>
        /// Searches the Type in the database with the given name.
        /// </summary>
        /// <param name="name">Name of the type.</param>
        /// <returns>List of Types with the given name.</returns>
        List<Type> SearchType(string name);
    }
}
