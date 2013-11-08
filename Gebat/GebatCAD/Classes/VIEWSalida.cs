using System;

namespace GebatCAD.Classes
{
    public class VIEWSalida : ACAD
    {
        public VIEWSalida(string connStringName)
            : base(connStringName)
        {
            this.tablename = "Salida";
            this.idFormat.Add("FoodType");
        }
    }
}
