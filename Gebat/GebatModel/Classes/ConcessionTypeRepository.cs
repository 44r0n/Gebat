using System.Collections.Generic;
using System.Linq;

namespace GebatModel
{
    public class ConcessionTypeRepository : BaseRepository , IConcessionTypeRepository
    {
        /// <summary>
        /// Gets all the concession types from the database.
        /// </summary>
        /// <returns>List of concession types.</returns>
        public List<ConcessionType> GetAllConcessionTypes()
        {
            return this.GetAll<ConcessionType>().ToList();
        }

        /// <summary>
        /// Adds concession type into the database.
        /// </summary>
        /// <param name="type">ConcessionTypeto addto the database.</param>
        public void AddConcessionType(ConcessionType type)
        {
            this.Add(type);
        }

        /// <summary>
        /// Updates concession type from the database.
        /// </summary>
        /// <param name="type">ConcessionType to update from the database.</param>
        public void UpdateConcessionType(ConcessionType type)
        {
            this.Update(type);
        }

        /// <summary>
        /// Deletes concession type from the database.
        /// </summary>
        /// <param name="type">ConcessionType to delete from the database.</param>
        public void DeleteConcessionType(ConcessionType type)
        {
            this.Delete(type);
        }

        /// <summary>
        /// Searches the ConcessionTypein the database with the given name.
        /// </summary>
        /// <param name="name">Name of the concession type.</param>
        /// <returns>List of ConcessionType with the given name.</returns>
        public List<ConcessionType> SearchConcessionType(string name)
        {
            return this.GetAll<ConcessionType>().Where(concession => concession.Name.Contains(name)).ToList();
        }
    }
}
