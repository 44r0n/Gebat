using System.Collections.Generic;
using System.Linq;

namespace GebatModel
{
    public class TypeRepository : BaseRepository , ITypeRepository
    {
        /// <summary>
        /// Gets all the types from the database.
        /// </summary>
        /// <returns>List of Types.</returns>
        public List<Type> GetAllTypes()
        {
            return this.GetAll<Type>().ToList();
        }

        /// <summary>
        /// Adds a type into the database.
        /// </summary>
        /// <param name="type">Type to add to the database.</param>
        public void AddType(Type type)
        {
            this.Add(type);
        }

        /// <summary>
        /// Updates a type from the database.
        /// </summary>
        /// <param name="type">Type to update from the database.</param>
        public void UpdateType(Type type)
        {
            this.Update(type);
        }

        /// <summary>
        /// Deletes a Type from the database.
        /// </summary>
        /// <param name="type">Type to delete from the database.</param>
        public void DeleteType(Type type)
        {
            this.Delete(type);
        }

        /// <summary>
        /// Searches the Type in the database with the given name.
        /// </summary>
        /// <param name="name">Name of the type.</param>
        /// <returns>List of Types with the given name or null.</returns>
        public List<Type> SearchType(string name)
        {
            return this.GetAll<Type>().Where(type => type.Name.Contains(name)).ToList();
        }
    }
}
