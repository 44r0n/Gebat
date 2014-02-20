using System;

namespace GebatCAD.Classes
{
    public class CADPersonas : AADL
    {
        public CADPersonas(string connStringName)
            : base(connStringName)
        {
            this.tablename = "Personas";
            this.idFormat.Add("Id");
        }
    }
}
