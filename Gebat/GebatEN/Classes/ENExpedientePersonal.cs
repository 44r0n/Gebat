using System;
using System.Collections.Generic;
using System.Data;
using GebatCAD.Classes;
using GebatEN.Enums;

namespace GebatEN.Classes
{
    public class ENExpedientePersonal : AEN
    {
        #region//Atributes

        private List<ENFamiliar> familiares = null;
        private int ingresos;
        private string observaciones;

        #endregion

        #region//Private Methods

        /// <summary>
        /// Carga todos los familiares ligados a un expediente.
        /// </summary>
        private void loadFamiliares()
        {
            this.familiares = new List<ENFamiliar>();
            ADLFamiliars cfam = new ADLFamiliars(defaultConnString);
            foreach (DataRow row in cfam.SelectWhere("Expediente = " + (int)this.id[0]).Rows)
            {
                ENFamiliar fam = new ENFamiliar();
                fam.FromRow(row);
                this.familiares.Add(fam);
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
                DataRow ret = cad.GetVoidRow;
                if (this.id != null)
                {
                    ret["Id"] = (int)this.id[0];
                }
                ret["Ingresos"] = this.ingresos;
                ret["Observaciones"] = this.observaciones;
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
                this.ingresos = (int)row["Ingresos"];
                this.observaciones = (string)row["Observaciones"];
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
        public int Ingresos
        {
            get
            {
                return this.ingresos;
            }
            set
            {
                this.ingresos = value;
            }
        }

        /// <summary>
        /// Obtiene y establece las observaciones.
        /// </summary>
        public string Observaciones
        {
            get
            {
                return this.observaciones;
            }
            set
            {
                this.observaciones = value;
            }
        }

        /// <summary>
        /// Obtiene los familiares del expediente.
        /// </summary>
        public List<ENFamiliar> Familiares
        {
            get
            {
                if (this.familiares == null)
                {
                    loadFamiliares();
                }
                return this.familiares;
            }
        }

        #endregion

        #region//Public Methods

        /// <summary>
        /// Constructor del expediente.
        /// </summary>
        /// <param name="Ingresos">Ingresos del expediente.</param>
        /// <param name="Observaciones">Observaciones sobre el expediente.</param>
        public ENExpedientePersonal(int Ingresos, string Observaciones)
            :base()
        {
            cad = new ADLPersonalDosier(defaultConnString);
            this.ingresos = Ingresos;
            this.observaciones = Observaciones;
        }

        /// <summary>
        /// Constructor por defecto.
        /// </summary>
        public ENExpedientePersonal()
            :base()
        {
            cad = new ADLPersonalDosier(defaultConnString);
        }

        /// <summary>
        /// Busca en la bse de datos el Expediente mediante identificador.
        /// </summary>
        /// <param name="id">Identificador por le que se buscará el expediente.</param>
        /// <returns>Expediente en formato AEN.</returns>
        public override AEN Read(List<int> id)
        {
            ENExpedientePersonal ret = new ENExpedientePersonal();
            List<object> param = new List<object>();
            param.Add(id[0]);
            DataRow row = cad.Select(param);
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
        public override List<AEN> ReadAll()
        {
            List<AEN> ret = new List<AEN>();
            DataTable tabla = cad.SelectAll();
            foreach (DataRow rows in tabla.Rows)
            {
                ENExpedientePersonal nuevo = new ENExpedientePersonal();
                nuevo.FromRow(rows);
                ret.Add((ENExpedientePersonal)nuevo);
            }
            return ret;
        }

        /// <summary>
        /// Añade un familiar al expediente personal, guardando el familiar en la base de datos.
        /// </summary>
        /// <param name="familiar">Familiar a añadir.</param>
        public void AddFamiliar(ENFamiliar familiar)
        {
            loadFamiliares();
            familiar.expediente = (int)this.id[0];
            familiar.Save();
            familiares.Add(familiar);
        }

        #endregion
    }
}
