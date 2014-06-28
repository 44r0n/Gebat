using System;
using System.Collections.Generic;
using System.Data;
using GebatCAD.Classes;

namespace GebatEN.Classes
{
    public abstract class AEBConcession : AEB
    {
        #region//Atributes

        protected bool checkdate;
        internal int dossier = 0;
        protected DateTime beginDate;
        protected DateTime finishDate;
        protected string notes;
        private ADL concesion;

        #endregion

        #region//Private Methods

        private void initADL()
        {
            concesion = new ADL(defaultConnString, "concessions", "Id");
            beginDate = new DateTime();
            finishDate = new DateTime();
            checkdate = false;
        }

        #endregion

        #region//Protected Methods

        protected override void insert()
        {
            if (finishDate != new DateTime())
            {
                concesion.ExecuteNonQuery("INSERT INTO concessions (Dossier, BeginDate, FinishDate, Notes) VALUES (@Dossier, @BeginDate, @FinishDate, @Notes)", dossier, GetCipher.Encrypt(beginDate.ToShortDateString()), GetCipher.Encrypt(finishDate.ToShortDateString()), notes);
                this.id.Add((int)concesion.Last()["Id"]);
            }
            else
            {
                concesion.ExecuteNonQuery("INSERT INTO concessions (Dossier, BeginDate, Notes) VALUES (@Dossier, @BeginDate, @Notes)", dossier, GetCipher.Encrypt(beginDate.ToShortDateString()), notes);
                this.id.Add((int)concesion.Last()["Id"]);
            }
        }

        protected override void update()
        {
            concesion.ExecuteNonQuery("UPDATE concessions SET Dossier = @Dossier, BeginDate = @BeginDate, FinishDate = @FinishDate, Notes = @Notes WHERE Id = @Id", dossier, GetCipher.Encrypt(beginDate.ToShortDateString()), GetCipher.Encrypt(finishDate.ToShortDateString()), notes, (int)this.id[0]);
        }

        protected override void delete()
        {
            concesion.ExecuteNonQuery("DELETE FROM concessions WHERE Id = @Id",(int)this.id[0]);
        }

        #endregion

        #region//Internal Methods

        /// <summary>
        /// Obtiene el objeto actual en tipo DataRow de forma que corresponde en la base de datos.
        /// </summary>
        internal override DataRow ToRow
        {
            get 
            {
                DataRow ret = concesion.GetVoidRow;
                if (this.id != null)
                {
                    ret["Id"] = (int)this.id[0];
                }
                ret["BeginDate"] = GetCipher.Encrypt(this.beginDate.ToShortDateString());
                ret["FinishDate"] = GetCipher.Encrypt(this.finishDate.ToShortDateString());
                ret["Notes"] = this.notes;
                return ret;
            }
        }

        /// <summary>
        /// Asigna al objeto acutal los datos contenientes en el DataRow
        /// </summary>
        /// <param name="row">Fila con los datos.</param>
        internal override void FromRow(DataRow row)
        {
            if (row != null)
            {
                this.id = new List<object>();
                DataRow conrow = concesion.Select("SELECT * FROM concessions WHERE Id = @Id", (int)row["Id"]).Rows[0];
                this.id.Add(conrow["Id"]);
                this.dossier = (int)conrow["Dossier"];
                this.beginDate = Convert.ToDateTime(GetCipher.Decrypt((string)conrow["BeginDate"]));
                if (conrow["FinishDate"] != DBNull.Value)
                {
                    this.finishDate = Convert.ToDateTime(GetCipher.Decrypt((string)conrow["FinishDate"]));
                }
                this.notes = (string)conrow["Notes"];
            }
            else
            {
                throw new NullReferenceException("Cannot convert from row, the row is null");
            }
            this.saved = true;
        }

        /// <summary>
        /// Establece el modo de comprobación de fechas. Usado para comprobar el tiempo de sanción de salida de Fega.
        /// </summary>
        internal void CheckModeON()
        {
            checkdate = true;
        }

        /// <summary>
        /// Establece el modo de fecha normal.
        /// </summary>
        internal void CheckModeOff()
        {
            checkdate = false;
        }

        #endregion

        #region//Gettes & Setters

        /// <summary>
        /// Obtiene y establece la fecha inicial de la concesión.
        /// </summary>
        public DateTime BeginDate
        {
            get
            {
                return beginDate;
            }
            set
            {
                beginDate = value;
            }
        }

        /// <summary>
        /// Obtiene y establece la fecha final de la concesión.
        /// </summary>
        public virtual DateTime FinishDate
        {
            get
            {
                return finishDate;
            }
            set
            {
                finishDate = value;
            }
        }

        /// <summary>
        /// Obtiene y establece las notas de la concesión.
        /// </summary>
        public string Notes
        {
            get
            {
                return notes;
            }
            set
            {
                notes = value;
            }
        }

        #endregion

        #region//Public Methods

        /// <summary>
        /// Constructor con parámetros.
        /// </summary>
        /// <param name="beginDate">Fecha de inicio de la concesión.</param>
        /// <param name="finishDate">Fecha final de la concesión.</param>
        /// <param name="notes">Notas sobre la concesión.</param>
        public AEBConcession(DateTime beginDate, DateTime finishDate, string notes)
            : base()
        {
            initADL();
            this.beginDate = beginDate;
            this.finishDate = finishDate;
            this.notes = notes;
        }

        /// <summary>
        /// Constructor por defecto.
        /// </summary>
        public AEBConcession()
            :base()
        {
            initADL();
        }

        #endregion
    }
}
