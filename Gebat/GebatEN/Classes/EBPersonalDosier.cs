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

        private List<EBFamiliar> familiars = null;
        private int income;
        private string observations;

        #endregion

        #region//Private Methods

        /// <summary>
        /// Carga todos los familiares ligados a un expediente.
        /// </summary>
        private void loadFamiliars()
        {
            this.familiars = new List<EBFamiliar>();
            ADLFamiliars adlfam = new ADLFamiliars(defaultConnString);
            foreach (DataRow row in adlfam.SelectWhere("Expediente = " + (int)this.id[0]).Rows)
            {
                EBFamiliar fam = new EBFamiliar();
                fam.FromRow(row);
                this.familiars.Add(fam);
            }
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
                ret["Income"] = this.income;
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
                this.income = (int)row["Income"];
                this.observations = (string)row["Observations"];
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
            set
            {
                this.income = value;
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
                if (this.familiars == null)
                {
                    loadFamiliars();
                }
                return this.familiars;
            }
        }

        #endregion

        #region//Public Methods

        /// <summary>
        /// Constructor del expediente.
        /// </summary>
        /// <param name="Income">Ingresos del expediente.</param>
        /// <param name="Observations">Observaciones sobre el expediente.</param>
        public EBPersonalDosier(int Income, string Observations)
            :base()
        {
            adl = new ADLPersonalDosier(defaultConnString);
            this.income = Income;
            this.observations = Observations;
        }

        /// <summary>
        /// Constructor por defecto.
        /// </summary>
        public EBPersonalDosier()
            :base()
        {
            adl = new ADLPersonalDosier(defaultConnString);
        }

        /// <summary>
        /// Busca en la bse de datos el Expediente mediante identificador.
        /// </summary>
        /// <param name="id">Identificador por le que se buscará el expediente.</param>
        /// <returns>Expediente en formato AEN.</returns>
        public override AEB Read(List<int> id)
        {
            EBPersonalDosier ret = new EBPersonalDosier();
            List<object> param = new List<object>();
            param.Add(id[0]);
            DataRow row = adl.Select(param);
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
            familiar.Save();
            familiars.Add(familiar);
        }

        #endregion
    }
}
