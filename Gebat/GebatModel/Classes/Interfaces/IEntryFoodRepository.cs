using System.Collections.Generic;
using System;

namespace GebatModel
{
    internal interface IEntryFoodRepository
    {
        /// <summary>
        /// Gets all the entries of food form the database of a given food.
        /// </summary>        
        /// <param name="food">Food of the entry.</param>
        /// <returns>List of entries.</returns>
        List<EntryFood> GetAllEntries(Food food);

        /// <summary>
        /// Adds an entry into the database.
        /// </summary>
        /// <param name="entry">Entry to add to the database.</param>
        void AddEntry(EntryFood entry);

        /// <summary>
        /// Updates an from the database.
        /// </summary>
        /// <param name="entry">Entry to update from the database.</param>
        void UpdateEntry(EntryFood entry);

        /// <summary>
        /// Deletes an Entry from the database.
        /// </summary>
        /// <param name="entry">Entry to delete from the database.</param>
        void DeleteEntry(EntryFood entry);

        /// <summary>
        /// Searches the Entry in the database with the given date.
        /// </summary>
        /// <param name="food">Food of the entry.</param>
        /// <param name="date">Date of the Entry.</param>
        /// <returns>List of Entries with the given date.</returns>
        List<EntryFood> SearchEntry(Food food, DateTime date);
    }
}
