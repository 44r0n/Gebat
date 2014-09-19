using System.Collections.Generic;

namespace GebatModel
{
    public interface IFoodRepository
    {
        /// <summary>
        /// Gets all the food form the database.
        /// </summary>
        /// <returns>List of food.</returns>
        List<Food> GetAllFood();

        /// <summary>
        /// Adds food into the database.
        /// </summary>
        /// <param name="food">Food to add to the database.</param>
        void AddFood(Food food);

        /// <summary>
        /// Updates food from the database.
        /// </summary>
        /// <param name="food">Food to update form the database.</param>
        void UpdateFood(Food food);

        /// <summary>
        /// Deletes Food form the database.
        /// </summary>
        /// <param name="food">Food to delete form the database.</param>
        void DeleteFood(Food food);

        /// <summary>
        /// Searchhes the Food in the database with the given name.
        /// </summary>
        /// <param name="name">Name of the food.</param>
        /// <returns>List of Food with the given name.</returns>
        List<Food> SearchFood(string name);
    }
}
