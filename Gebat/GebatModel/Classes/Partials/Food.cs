using System;

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

        /// <summary>
        /// Adds a quantity of food.
        /// </summary>
        /// <param name="quantity">Quantity of food to add.</param>
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

        /// <summary>
        /// Adds a quantity of food.
        /// </summary>
        /// <param name="quantity">Quantity of food to add.</param>
        /// <param name="date">Date of the adition of food.</param>
        public void AddQuantity(int quantity, DateTime date)
        {
            this.addQuantity(quantity, date);
        }

        /// <summary>
        /// Removes a quantity of food.
        /// </summary>
        /// <param name="quantity">Quantity of food to remove.</param>
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

        /// <summary>
        /// Removes a quantity of food.
        /// </summary>
        /// <param name="quantity">Quantity of fodd to remove.</param>
        /// <param name="date">Date of the output of food.</param>
        public void RemoveQuantity(int quantity, DateTime date)
        {
            this.removeQuantity(quantity, date);
        }
    }
}
