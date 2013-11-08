using System;

namespace GebatCAD.Classes
{
    public class VIEWEntrada : ACAD //TODO: crear AVIEW y substituir.
    {
        public VIEWEntrada(string connStringName)
            : base(connStringName)
        {
            this.tablename = "Entrada";
            this.idFormat.Add("FoodType");
        }
    }
}
