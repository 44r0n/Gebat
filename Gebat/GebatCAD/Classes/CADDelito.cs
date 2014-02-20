using System;

namespace GebatCAD.Classes
{
    public class CADDelito : AADL
    {
        public CADDelito(string connStringName)
            : base(connStringName)
        {
            this.tablename = "Delitos";
            this.idFormat.Add("Id");
        }
    }
}
