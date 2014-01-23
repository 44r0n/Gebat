using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GebatCAD.Classes
{
    public class CADTelefonos : ACAD
    {
        public CADTelefonos(string connectionString)
            : base(connectionString)
        {
            this.tablename = "Telefonos";
            this.idFormat.Add("Id");
        }
    }
}
