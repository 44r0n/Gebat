using System;

namespace GebatCAD.Classes
{
    public class ADLPersonalDossier : AADL
    {
        public ADLPersonalDossier(string connectionString)
            :base(connectionString)
        {
            this.tablename = "PersonalDossier";
            this.idFormat.Add("Id");
        }
    }
}
