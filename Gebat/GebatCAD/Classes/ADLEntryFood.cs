using System;

namespace GebatCAD.Classes
{
    public class ADLEntryFood : AADL
    {
        public ADLEntryFood(string connStringName)
            : base(connStringName)
        {
            this.tablename = "EntryFood";
            this.idFormat.Add("Id");
        }
    }
}
