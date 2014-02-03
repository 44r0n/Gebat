using System;
using System.Collections.Generic;
using System.Data;
using GebatCAD.Classes;
using GebatEN.Enums;

namespace GebatEN.Classes
{
    public class ENFamiliar : AENPersona
    {
        #region//Atributes

        private int idfam = 0;
        internal int expediente = 0;

        #endregion

        #region//Internal Methods

        /// <summary>
        /// Obtiene el objeto actual en tipo DataRow de forma que corresponde en la base de datos.
        /// </summary>
        internal override DataRow ToRow
        {
            get
            {
                DataRow ret = cad.GetVoidRow;
                if (this.idfam != 0)
                {
                    ret["Id"] = this.idfam;
                }
                ret["DNI"] = this.DNI;
                if (expediente != 0)
                {
                    ret["Expediente"] = expediente;
                }
                return ret;
            }
        }
        
        /// <summary>
        /// Asigna al objeto actual los datos contenientes en el DataRow.
        /// </summary>
        /// <param name="row"></param>
        internal override void FromRow(DataRow row)
        {
            base.FromRow(row);
            this.idfam = (int)row["Id"];
            this.saved = true;
        }

        #endregion

        #region//Public Methods

        /// <summary>
        /// Constructor de Familiar
        /// </summary>
        /// <param name="DNI">DNI de la persona.</param>
        /// <param name="Nombre">Nombre de la persona.</param>
        /// <param name="Apellidos">Apellidos de la persona.</param>
        /// <param name="FechaNac">Fecha de nacimiento de la persona.</param>
        /// <param name="Genero">Genero de la persona.</param>
        public ENFamiliar(string DNI, string Nombre, string Apellidos, DateTime FechaNac, sexo Genero)
            : base(DNI, Nombre, Apellidos, FechaNac, Genero)
        {
            cad = new CADFamiliares(defaultConnString);
        }

        /// <summary>
        /// Constructor por defecto.
        /// </summary>
        public ENFamiliar()
            :base()
        {
            cad = new CADPersonas(defaultConnString);
        }

        /// <summary>
        /// Busca en la base de datos el Familiar mediante identificador.
        /// </summary>
        /// <param name="id">Identificador por el que se buscará el familiar.</param>
        /// <returns>Familiar en formato AEN.</returns>
        public override AEN Read(List<int> id)
        {
            AVIEW vfam = new VIEWDatosFamiliares(defaultConnString);
            ENFamiliar ret = new ENFamiliar();
            List<object> param = new List<object>();
            param.Add(id[0]);
            DataRow row = vfam.Select(param);
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
        /// Obtiene todos los familiares de la base de datos.
        /// </summary>
        /// <returns></returns>
        public override List<AEN> ReadAll()
        {
            List<AEN> ret = new List<AEN>();
            VIEWDatosFamiliares tfam = new VIEWDatosFamiliares(defaultConnString);
            DataTable tabla = tfam.SelectAll();
            foreach (DataRow rows in tabla.Rows)
            {
                ENFamiliar nuevo = new ENFamiliar();
                nuevo.FromRow(rows);
                ret.Add((ENFamiliar)nuevo);
            }
            return ret;
        }

        /// <summary>
        /// Guarda el Familiar en la base de datos.Si es nuevo lo inserta, si ha sido modificado lo modifica en la base de datos.
        /// </summary>
        public override void Save()
        {
            CADPersonas per = new CADPersonas(defaultConnString);
            if (!this.saved)
            {
                if (!alreadyInPerson())
                {
                    per.Insert(base.ToRow);
                }
                this.FromRow(cad.Insert(this.ToRow));
                this.saved = true;
            }
            else
            {
                per.Update(base.ToRow);
                cad.Update(this.ToRow);
            }
        }

        /// <summary>
        /// Carga los datos de una persona.
        /// </summary>
        /// <param name="dni">DNI por el que se buscará a la persona.</param>
        /// <returns>Lista de objetos AENPersona.</returns>
        public override List<AENPersona> ReadByDNI(string dni)
        {
            List<AENPersona> ret = new List<AENPersona>();
            DataTable tabla = new VIEWDatosFamiliares(defaultConnString).SelectWhere("DNI = '" + dni + "'");
            foreach (DataRow fila in tabla.Rows)
            {
                ENFamiliar nuevo = new ENFamiliar();
                nuevo.FromRow(fila);
                ret.Add((AENPersona)nuevo);
            }
            return ret;
        }

        #endregion
    }
}
