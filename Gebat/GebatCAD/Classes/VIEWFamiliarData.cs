using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GebatCAD.Classes
{
    public class VIEWFamiliarData : AVIEW
    {
        public VIEWFamiliarData(string connStringName)
            : base(connStringName)
        {
            this.tablename = "FamiliarData";
            this.idFormat.Add("Id");
        }
    }
}
