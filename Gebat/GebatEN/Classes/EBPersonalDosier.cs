using System;
using System.Collections.Generic;
using System.Data;
using GebatCAD.Classes;
using GebatEN.Enums;

namespace GebatEN.Classes
{
    public class EBPersonalDosier : AEB
    {
        #region//Atributes

        private List<EBFamiliar> familiars;
        private bool familiarsLoaded = false;
        private int income = 0;
        private string observations;
        private List<AEBConcession> concessions;
        private bool concessionsLoaded = false;

        #endregion

        #region//Private Methods

        private void initADL()
        {
            adl = new ADL(defaultConnString, "personaldossier", "Id");
        }

        /// <summary>
        /// Carga todos los familiares ligados al expediente actual.
        /// </summary>
        private void loadFamiliars()
        {
            if (!familiarsLoaded)
            {
                foreach (DataRow row in adl.Select("SELECT * FROM familiars WHERE Dossier = @Dossier", (int)this.id[0]).Rows)
                {
                    EBFamiliar fam = new EBFamiliar();
                    fam.FromRow(row);
                    this.familiars.Add(fam);
                    this.income += fam.Income;
                }
                familiarsLoaded = true;
            }
        }

        /// <summary>
        /// Carga todas las concesiones otorgadas  al expediente actual.
        /// </summary>
        private void loadConcessions()
        {
            if (!concessionsLoaded)
            {
                ADL aconcession = new ADL(defaultConnString, "concessions", "Id");
                DataTable table = aconcession.SelectAll();
                foreach (DataRow rows in table.Rows)
                { 
                    if(EBFresco.IsFresco((int)rows["Id"]))
                    {
                        List<object> ids = new List<object>();
                        ids.Add((int)rows["Id"]);
                        EBFresco nuevo = (EBFresco) new EBFresco().Read(ids);
                        concessions.Add(nuevo);
                    }
                }
                concessionsLoaded = true;
            }
        }

        /// <summary>
        /// Comprueba si la concesión pasada por parámetro no solapa con una concesión previa.
        /// </summary>
        /// <param name="newConcession">Concesión a comprobar.</param>
        /// <returns></returns>
        private bool checkNewConcession(AEBConcession newConcession)
        {
            bool ret = true;
            foreach (AEBConcession concession in concessions)
            {
                if (newConcession.BeginDate < concession.FinishDate)
                {
                    ret = false;
                    break;
                }
            }
            return ret;
        }

        #endregion

        #region//Protected Methods

        protected override void insert()
        {
            adl.ExecuteNonQuery("INSERT INTO personaldossier (Observations) VALUES (@Observations)", this.observations);
            this.id.Add((int)adl.Last()["Id"]);
        }

        protected override void update()
        {
            adl.ExecuteNonQuery("UDPATE personaldossier SET Observations = @Observations WHERE Id = @Id", this.observations, (int)this.id[0]);
        }

        protected override void delete()
        {
            adl.ExecuteNonQuery("DELETE FROM personaldossier WHERE Id = @Id", (int)this.id[0]);
        }

        #endregion

        #region//Internal Methods

        /// <summary>
        /// Obtiene el objeto actual en el tipo DataRow de forma que corresponde en la base de datos.
        /// </summary>
        internal override DataRow ToRow
        {
            get 
            {
                DataRow ret = adl.GetVoidRow;
                if (this.id != null)
                {
                    ret["Id"] = (int)this.id[0];
                }
                ret["Observations"] = this.observations;
                return ret;
            }
        }

        /// <summary>
        /// Asigna al objeto actual los datos centenientes en el DataRow.
        /// </summary>
        /// <param name="row"></param>
        internal override void FromRow(DataRow row)
        {
            if (row != null)
            {
                this.id = new List<object>();
                this.id.Add((int)row["Id"]);
                this.observations = (string)row["Observations"];
                this.loadFamiliars();
                this.saved = true;
            }
            else
            {
                throw new NullReferenceException("Cannot convert from row, the row is null");
            }
        }

        #endregion

        #region//Getters & Setters

        /// <summary>
        /// Obtiene y establece los ingresos.
        /// </summary>
        public int Income
        {
            get
            {
                return this.income;
            }
        }

        /// <summary>
        /// Obtiene y establece las observaciones.
        /// </summary>
        public string Observations
        {
            get
            {
                return this.observations;
            }
            set
            {
                this.observations = value;
            }
        }

        /// <summary>
        /// Obtiene los familiares del expediente.
        /// </summary>
        public List<EBFamiliar> Familiars
        {
            get
            {
                loadFamiliars();
                return this.familiars;
            }
        }

        /// <summary>
        /// Obtiene las concesiones del expediente.
        /// </summary>
        public List<AEBConcession> Concessions
        {
            get
            {
                loadConcessions();
                return this.concessions;
            }
        }

        #endregion

        #region//Public Methods

        /// <summary>
        /// Constructor del expediente.
        /// </summary>
        /// <param name="Observations">Observaciones sobre el expediente.</param>
        public EBPersonalDosier(string Observations)
            :base()
        {
            initADL();
            this.observations = Observations;
            familiars = new List<EBFamiliar>();
            concessions = new List<AEBConcession>();
        }

        /// <summary>
        /// Constructor por defecto.
        /// </summary>
        public EBPersonalDosier()
            :base()
        {
            initADL();
            familiars = new List<EBFamiliar>();
            concessions = new List<AEBConcession>();
        }

        /// <summary>
        /// Busca en la bse de datos el Expediente mediante identificador.
        /// </summary>
        /// <param name="id">Identificador por le que se buscará el expediente.</param>
        /// <returns>Expediente en formato AEN.</returns>
        public override AEB Read(List<object> id)
        {
            EBPersonalDosier ret = new EBPersonalDosier();
            DataRow row = adl.Select("SELECT * FROM personaldossier WHERE Id = @Id",(int)id[0]).Rows[0];
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

        /// <summary>
        /// Obtiene todos los Expedientes en formate AEN.
        /// </summary>
        /// <returns>Lista de expedientes en formato AEN.</returns>
        public override List<AEB> ReadAll()
        {
            List<AEB> ret = new List<AEB>();
            DataTable table = adl.SelectAll();
            foreach (DataRow rows in table.Rows)
            {
                EBPersonalDosier newdossier = new EBPersonalDosier();
                newdossier.FromRow(rows);
                ret.Add((EBPersonalDosier)newdossier);
            }
            return ret;
        }

        /// <summary>
        /// Añade un familiar al expediente personal, guardando el familiar en la base de datos.
        /// </summary>
        /// <param name="familiar">Familiar a añadir.</param>
        public void AddFamiliar(EBFamiliar familiar)
        {
            loadFamiliars();
            familiar.dossier = (int)this.id[0];
            this.income += familiar.Income;
            familiar.Save();
            familiars.Add(familiar);
        }

        /// <summary>
        /// Añad una concesión nueva al expediente personal, guardando la concesión en la base de datos.
        /// </summary>
        /// <param name="concession">Concesión a añadir.</param>
        public virtual void AddConcession(AEBConcession concession)
        {
            loadConcessions();
            if (!checkNewConcession(concession))
            {
                throw new GebatEN.Exceptions.InvalidDateConcessionException("The begin date of the concession cannot be earlier than other concession");
            }
            concession.dossier = (int)this.id[0];
            concession.Save();
            concessions.Add(concession);
        }

        #endregion
    }
}
