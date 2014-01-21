using System;

namespace GebatCAD.Classes
{
    public class CADDelito : ACAD
    {
        public CADDelito(string connStringName)
            : base(connStringName)
        {
            this.tablename = "Delitos";
            this.idFormat.Add("Id");
        }
    }
}
