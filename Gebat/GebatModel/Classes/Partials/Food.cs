using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GebatModel
{
    public partial class Food
    {
        public void AddQuantity(int quantity)
        {
            this.Quantity += quantity;
        }

        public void RemoveQuantity(int quantity)
        {
            this.Quantity -= quantity;
        }
    }
}
