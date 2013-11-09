using System;

namespace GebatCAD.Classes
{
    public class VIEWTotalFood : ACAD
    {
        public VIEWTotalFood(string connStringName)
            : base(connStringName)
        {
            this.tablename = "TotalFood";
            this.idFormat.Add("FoodType");
        }
    }
}
