using System;
using System.Collections.Generic;
using System.Data;
using GebatCAD.Classes;

namespace GebatEN.Classes
{
    public abstract class AEBConcession : AEB
    {
        #region//Atributes

        internal int dossier = 0;
        private DateTime beginDate;
        private DateTime finishDate;
        private string notes;
        private ADL concesion;

        #endregion

        #region//Private Methods

        private void initADL()
        {
            concesion = new ADL(defaultConnString, "concessions", "Id");
        }

        #endregion

        #region//Protected Methods

        protected override void insert()
        {
            concesion.ExecuteNonQuery("INSERT INTO concessions (Dossier, BeginDate, FinishDate, Notes) VALUES (@Dossier, @BeginDate, @FinishDate, @Notes)", dossier, beginDate, finishDate, notes);
            this.id.Add((int)concesion.Last()["Id"]);
        }

        protected override void update()
        {
            concesion.ExecuteNonQuery("UPDATE concessions SET Dossier = @Dossier, BeginDate = @BeginDate, FinishDate = @FinishDate, Notes = @Notes WHERE Id = @Id", dossier, beginDate, finishDate, notes, (int)this.id[0]);
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
                ret["BeginDate"] = this.beginDate;
                ret["FinishDate"] = this.finishDate;
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
                this.beginDate = (DateTime)conrow["BeginDate"];
                this.finishDate = (DateTime)conrow["FinishDate"];
                this.notes = (string)conrow["Notes"];
            }
            else
            {
                throw new NullReferenceException("Cannot convert from row, the row is null");
            }
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
        public DateTime FinishDate
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
