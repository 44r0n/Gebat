using System.Collections.Generic;
using System.Linq;

namespace GebatModel
{
    public class FoodRepository : BaseRepository , IFoodRepository
    {
        
        public List<Food> GetAllFood()
        {
            return this.GetAll<Food>().ToList();
        }

        
        public void SaveFood(Food food)
        {
            this.Save(food);
        }

        
        public void UpdateFood(Food food)
        {
            this.Update(food);
        }

        
        public void DeleteFood(Food food)
        {
            this.Delete(food);
        }

        
        public List<Food> SearchFood(string name)
        {
            return this.GetAll<Food>().Where(food => food.Name.Contains(name)).ToList();
        }
    }
}
