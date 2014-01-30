using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GebatCAD.Classes
{
    public class VIEWDatosFamiliares : AVIEW
    {
        public VIEWDatosFamiliares(string connStringName)
            : base(connStringName)
        {
            this.tablename = "DatosFamiliares";
            this.idFormat.Add("Id");
        }
    }
}
