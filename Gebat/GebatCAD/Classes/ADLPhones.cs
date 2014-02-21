using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GebatCAD.Classes
{
    public class ADLPhones : AADL
    {
        public ADLPhones(string connectionString)
            : base(connectionString)
        {
            this.tablename = "Phones";
            this.idFormat.Add("Id");
        }
    }
}
