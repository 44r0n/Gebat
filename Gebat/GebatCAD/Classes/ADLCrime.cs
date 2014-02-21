using System;

namespace GebatCAD.Classes
{
    public class ADLCrime : AADL
    {
        public ADLCrime(string connStringName)
            : base(connStringName)
        {
            this.tablename = "Crimes";
            this.idFormat.Add("Id");
        }
    }
}
