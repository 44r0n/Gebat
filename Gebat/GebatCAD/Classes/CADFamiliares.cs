using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GebatCAD.Classes
{
    public class CADFamiliares : ACAD
    {
        public CADFamiliares(string connStringName)
            :base(connStringName)
        {
            this.tablename = "Familiares";
            this.idFormat.Add("Id");
        }
    }
}
