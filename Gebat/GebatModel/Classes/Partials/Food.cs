using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GebatModel
{
    public partial class Food
    {
        IEntryFoodRepository entryfoodrepo = new EntryFoodRepository();

        //TODO: refactor here
        public void AddQuantity(int quantity)
        {
            this.Quantity += quantity;
            EntryFood entry = new EntryFood();
            entry.Quantity = quantity;
            entry.Date = DateTime.Now;
            entry.FoodIdFood = this.IdFood;
            entryfoodrepo.AddEntry(entry);
        }

        public void AddQuantity(int quantity, DateTime date)
        {
            this.Quantity += quantity;
            EntryFood entry = new EntryFood();
            entry.Quantity = quantity;
            entry.Date = date;
            entry.FoodIdFood = this.IdFood;
            this.EntryFood.Add(entry);
            entryfoodrepo.AddEntry(entry);
        }

        public void RemoveQuantity(int quantity)
        {
            this.Quantity -= quantity;
        }
    }
}
