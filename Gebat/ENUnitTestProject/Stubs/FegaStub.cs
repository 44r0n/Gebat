using GebatEN.Classes;
using System;
using GebatEN.Enums;
using GebatCAD.Classes;
using System.Data;
using System.Collections.Generic;
using Cripto.Util;

namespace ENUnitTestProject.Stubs
{
    internal class FegaStub : EBFega
    {
        private int dossier = 0;

        private static DateTime fecha;

        private void FromRow(DataRow row)
        {
            this.id = new List<object>();
            DataRow conrow = adl.Select("SELECT * FROM concessions WHERE Id = @Id", (int)row["Id"]).Rows[0];
            this.id.Add(conrow["Id"]);
            this.dossier = (int)conrow["Dossier"];
            this.beginDate = Convert.ToDateTime(GetCipher.Decrypt((string)conrow["BeginDate"]));
            if (conrow["FinishDate"] != DBNull.Value)
            {
                this.finishDate = Convert.ToDateTime(GetCipher.Decrypt((string)conrow["FinishDate"]));
            }
            this.notes = (string)conrow["Notes"];
        }

        public override DateTime FinishDate
        {
            get
            {
                if (fecha.Year == 1)
                {
                    return finishDate;
                }
                else
                {
                    if (!checkdate)
                    {
                        return finishDate;
                    }
                    else
                    {
                        if (fecha >= finishDate)
                        {
                            return finishDate.AddMonths(6);
                        }
                        else
                        {
                            return finishDate;
                        }
                    }
                }
            }
            set
            {
                finishDate = value;
            }
        }

        public static void SetFecha(DateTime f)
        {
            fecha = f;
        }

        public FegaStub(DateTime beginDate, DateTime finishDate, string notes, FegaStates state)
            : base(beginDate, finishDate, notes, state)
        {

        }

        public FegaStub()
            : base()
        {

        }

        public override AEB Read(List<object> id)
        {
            FegaStub ret;
            DataRow row = vfega.Select("SELECT * FROM fegadata WHERE Id = @Id", (int)id[0]).Rows[0];
            if (row != null)
            {
                ret = new FegaStub();
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
