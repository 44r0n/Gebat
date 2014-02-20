using System;

namespace GebatCAD.Classes
{
    public class CADTBC : AADL
    {
        public CADTBC(string connStringName)
            : base(connStringName)
        {
            this.tablename = "TBC";
            this.idFormat.Add("Id");
        }
    }
}
