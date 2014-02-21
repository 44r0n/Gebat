using System;

namespace GebatCAD.Classes
{
    public class ADLPeople : AADL
    {
        public ADLPeople(string connStringName)
            : base(connStringName)
        {
            this.tablename = "People";
            this.idFormat.Add("Id");
        }
    }
}
