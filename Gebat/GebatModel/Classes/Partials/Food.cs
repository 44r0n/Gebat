using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GebatModel
{
    public partial class Food
    {
        //TODO: asegura que es así.
        private static DateTime today = DateTime.MinValue;

        private void addQuantity(int quantity, DateTime date)
        {
            this.Quantity += quantity;
            EntryFood entry = new EntryFood();
            entry.Quantity = quantity;
            entry.Date = date;
            entry.FoodIdFood = this.IdFood;
            this.EntryFood.Add(entry);
        }

        private void removeQuantity(int quantity, DateTime date)
        {
            this.Quantity -= quantity;
            OutgoingFood outgoing = new OutgoingFood();
            outgoing.Quantity = quantity;
            outgoing.Date = date;
            outgoing.FoodIdFood = this.IdFood;
            this.OutgoingFood.Add(outgoing);
        }

        public static void SetToday(DateTime date)
        {
            today = date;
        }

        public void AddQuantity(int quantity)
        {
            //TODO: refactor this.
            if(today == DateTime.MinValue)
            {
                this.addQuantity(quantity, DateTime.Now);
            }
            else
            {
                this.addQuantity(quantity, today);
            }
        }

        public void AddQuantity(int quantity, DateTime date)
        {
            this.addQuantity(quantity, date);
        }

        public void RemoveQuantity(int quantity)
        {
            if(today == DateTime.MinValue)
            {
                this.removeQuantity(quantity, DateTime.Now);
            }
            else
            {
                this.removeQuantity(quantity, today);
            }
        }

        public void RemoveQuantity(int quantity, DateTime date)
        {
            this.removeQuantity(quantity, date);
        }
    }
}
