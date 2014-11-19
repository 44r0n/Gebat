using System.Collections.Generic;

namespace GebatModel
{
    public interface IFoodRepository
    {
        
        List<Food> GetAllFood();

        
        void SaveFood(Food food);

        
        void UpdateFood(Food food);

        
        void DeleteFood(Food food);

        
        List<Food> SearchFood(string name);
    }
}
