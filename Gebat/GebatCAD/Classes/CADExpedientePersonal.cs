using System;

namespace GebatCAD.Classes
{
    public class CADExpedientePersonal : AADL
    {
        public CADExpedientePersonal(string connectionString)
            :base(connectionString)
        {
            this.tablename = "ExpedientesPersonales";
            this.idFormat.Add("Id");
        }
    }
}
