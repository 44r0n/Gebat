using System;

namespace GebatCAD.Classes
{
    public class CADPersonas : ACAD
    {
        public CADPersonas(string connStringName)
            : base(connStringName)
        {
            this.tablename = "Personas";
            this.idFormat.Add("DNI");
        }
    }
}
