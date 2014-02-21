using System;

namespace GebatCAD.Classes
{
    public class ADLPersonalDosier : AADL
    {
        public ADLPersonalDosier(string connectionString)
            :base(connectionString)
        {
            this.tablename = "PersonalDosier";
            this.idFormat.Add("Id");
        }
    }
}
