using System.Collections.Generic;
using System.Linq;
using System;

namespace GebatModel
{
    internal class EntryFoodRepository : BaseRepository , IEntryFoodRepository
    {
        /// <summary>
        /// Gets all the entries of food form the database of a given food.
        /// </summary>
        /// <returns>List of entries.</returns>
        public List<EntryFood> GetAllEntries(Food food)
        {
            return this.GetAll<EntryFood>().Where(t => t.FoodIdFood == food.IdFood).ToList();
        }

        /// <summary>
        /// Adds an entry into the database.
        /// </summary>
        /// <param name="entry">Entry to add to the database.</param>
        public void AddEntry(EntryFood entry)
        {
            this.Add(entry);
        }

        /// <summary>
        /// Updates an from the database.
        /// </summary>
        /// <param name="entry">Entry to update from the database.</param>
        public void UpdateEntry(EntryFood entry)
        {
            this.Update(entry);
        }

        /// <summary>
        /// Deletes an Entry from the database.
        /// </summary>
        /// <param name="entry">Entry to delete from the database.</param>
        public void DeleteEntry(EntryFood entry)
        {
            this.Delete(entry);
        }

        /// <summary>
        /// Searches the Entry in the database with the given date.
        /// </summary>
        /// <param name="date">Date of the Entry.</param>
        /// <returns>List of Entries with the given date.</returns>
        public List<EntryFood> SearchEntry(Food food, DateTime date)
        {
            return this.GetAll<EntryFood>().Where(t => t.FoodIdFood == food.IdFood && t.Date == date).ToList();
        }
    }
}
