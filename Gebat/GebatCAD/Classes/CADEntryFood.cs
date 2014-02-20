using System;

namespace GebatCAD.Classes
{
    public class CADEntryFood : AADL
    {
        public CADEntryFood(string connStringName)
            : base(connStringName)
        {
            this.tablename = "EntryFood";
            this.idFormat.Add("Id");
        }
    }
}
