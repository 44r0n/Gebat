using System;

namespace GebatCAD.Classes
{
    public class CADOutgoingFood : ACAD
    {
        public CADOutgoingFood(string connStringName)
            : base(connStringName)
        {
            this.tablename = "OutgoingFood";
            this.idFormat.Add("Id");
        }
        
    }
}
