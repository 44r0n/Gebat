using System.Collections.Generic;
using System.Linq;

namespace GebatModel
{
    public class ConcessionRepository : BaseRepository , IConcessionRepository
    {
        /// <summary>
        /// Gets all the concessions from the database.
        /// </summary>
        /// <returns>List of Concessions.</returns>
        public List<Concession> GetAllConcessions()
        {
            return this.GetAll<Concession>().ToList();
        }

        /// <summary>
        /// Updates a concession from the database.
        /// </summary>
        /// <param name="concession">Concession to update from the database.</param>
        public void UpdateConcession(Concession concession)
        {
            this.Update(concession);
        }

        /// <summary>
        /// Deletes a Concession from the database.
        /// </summary>
        /// <param name="concession">Concession to delete from the database.</param>
        public void DeleteConcession(Concession concession)
        {
            this.Delete(concession);
        }
    }
}
