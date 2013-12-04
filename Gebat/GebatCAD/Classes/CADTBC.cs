using System;

namespace GebatCAD.Classes
{
    public class CADTBC : ACAD
    {
        public CADTBC(string connStringName)
            : base(connStringName)
        {
            this.tablename = "TBC";
            this.idFormat.Add("Id");
        }
    }
}
