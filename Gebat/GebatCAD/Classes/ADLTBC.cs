using System;

namespace GebatCAD.Classes
{
    public class ADLTBC : AADL
    {
        public ADLTBC(string connStringName)
            : base(connStringName)
        {
            this.tablename = "TBC";
            this.idFormat.Add("Id");
        }
    }
}
