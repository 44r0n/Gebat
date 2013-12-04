using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GebatCAD.Classes
{
    public class VIEWTBCPeople : AVIEW
    {
        public VIEWTBCPeople(string connStringName)
            : base(connStringName)
        {
            this.tablename = "TBCPeople";
            /*this.idFormat.Add("DNI");
            this.idFormat.Add("Ejecutoria");*/
        }
    }
}
