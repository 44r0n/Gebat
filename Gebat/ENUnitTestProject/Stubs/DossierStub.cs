using GebatEN.Classes;
using GebatCAD.Classes;
using System.Data;
using System.Collections.Generic;
using System;

namespace ENUnitTestProject.Stubs
{
    internal class DossierStub : EBPersonalDosier
    {
        private bool concessionsLoaded = false;

        protected override void loadConcessions()
        {
            if (!concessionsLoaded)
            {
                ADL aconcession = new ADL(defaultConnString, "concessions", "Id");
                DataTable table = aconcession.Select("SELECT * FROM concessions WHERE Dossier = @Dossier", (int)id[0]);
                foreach (DataRow rows in table.Rows)
                {
                    if (EBFresco.IsFresco((int)rows["Id"]))
                    {
                        List<object> ids = new List<object>();
                        ids.Add((int)rows["Id"]);
                        EBFresco nuevo = (EBFresco)new EBFresco().Read(ids);
                        concessions.Add(nuevo);
                    }

                    if (FegaStub.IsFega((int)rows["Id"]))
                    {
                        List<object> ids = new List<object>();
                        ids.Add((int)rows["Id"]);
                        FegaStub nuevo = (FegaStub)new FegaStub().Read(ids);
                        concessions.Add(nuevo);
                    }
                }
                concessionsLoaded = true;
            }
            
        }

        private void FromRow(DataRow row)
        {
            if (row != null)
            {
                this.id = new List<object>();
                this.id.Add((int)row["Id"]);
                this.observations = (string)row["Observations"];
                //this.loadFamiliars();
                this.saved = true;
            }
            else
            {
                throw new NullReferenceException("Cannot convert from row, the row is null");
            }
        }
        public override AEB Read(List<object> id)
        {
            DossierStub ret = new DossierStub();
            DataRow row = adl.Select("SELECT * FROM personaldossier WHERE Id = @Id", (int)id[0]).Rows[0];
            if (row != null)
            {
                ret.FromRow(row);
            }
            else
            {
                ret = null;
            }
            return ret;
        }
    }
}
