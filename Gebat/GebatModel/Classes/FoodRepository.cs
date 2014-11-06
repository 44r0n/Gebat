using System.Collections.Generic;
using System.Linq;

namespace GebatModel
{
    public class FoodRepository : BaseRepository , IFoodRepository
    {
        /// <summary>
        /// Gets all the food form the database.
        /// </summary>
        /// <returns>List of food.</returns>
        public List<Food> GetAllFood()
        {
            return this.GetAll<Food>().ToList();
        }

        /// <summary>
        /// Adds food into the database.
        /// </summary>
        /// <param name="food">Food to add to the database.</param>
        public void AddFood(Food food)
        {
            this.Add(food);
        }

        /// <summary>
        /// Updates food from the database.
        /// </summary>
        /// <param name="food">Food to update form the database.</param>
        public void UpdateFood(Food food)
        {
            this.Update(food);
        }

        /// <summary>
        /// Deletes Food form the database.
        /// </summary>
        /// <param name="food">Food to delete form the database.</param>
        public void DeleteFood(Food food)
        {
            this.Delete(food);
        }

        /// <summary>
        /// Searchhes the Food in the database with the given name.
        /// </summary>
        /// <param name="name">Name of the food.</param>
        /// <returns>List of Food with the given name.</returns>
        public List<Food> SearchFood(string name)
        {
            return this.GetAll<Food>().Where(food => food.Name.Contains(name)).ToList();
        }
    }
}
