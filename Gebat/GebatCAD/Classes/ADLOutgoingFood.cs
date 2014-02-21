using System;

namespace GebatCAD.Classes
{
    public class ADLOutgoingFood : AADL
    {
        public ADLOutgoingFood(string connStringName)
            : base(connStringName)
        {
            this.tablename = "OutgoingFood";
            this.idFormat.Add("Id");
        }
        
    }
}
