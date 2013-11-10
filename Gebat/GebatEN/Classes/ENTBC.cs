using System;
using System.Collections.Generic;
using System.Data;
using GebatCAD.Classes;

namespace GebatEN.Classes
{
    public class ENTBC : AENPersona
    {
        #region//Atributes

        //private string ejecutoria  -> this.id[1] hace referencia a la ejeutoria.
        private string juzgado;
        private DateTime finicio;
        private DateTime ffin;

        #endregion

        #region//Protected Methods

        /// <summary>
        /// Obtiene el objeto actual en tipo DataRow de forma que corresponde en la base de datos.
        /// </summary>
        protected override DataRow ToRow
        {
            get
            {
                DataRow ret = cad.GetVoidRow;
                ret["DNI"] = this.id[0];
                ret["Ejecutoria"] = this.id[1];
                ret["Juzgado"] = this.juzgado;
                ret["FInicio"] = this.finicio;
                ret["FFin"] = this.ffin;
                return ret;
            }
        }

        /// <summary>
        /// Asigna al objeto actual los datos contenientes en el DataRow.
        /// </summary>
        /// <param name="row">Fila con los datos.</param>
        protected override void FromRow(DataRow row)
        {
            base.FromRow(row);
            this.id.Add((object)row["Ejecutoria"]);
            this.finicio = (DateTime)row["FInicio"];
            this.ffin = (DateTime)row["FFin"];
        }

        #endregion

        #region//Getters & Setters

        /// <summary>
        /// Obtiene y establece la ejecutoria.
        /// </summary>
        public string Ejecutoria
        {
            get
            {
                return (string)this.id[1];
            }
            set
            {
                if (this.id.Count == 2)
                {
                    this.id[1] = value;
                }
                else
                {
                    this.id.Add(value);
                }
            }
        }

        /// <summary>
        /// Obtiene y establece el Juzgado.
        /// </summary>
        public string Juzgado
        {
            get
            {
                return this.juzgado;
            }
            set
            {
                this.juzgado = value;
            }
        }

        /// <summary>
        /// Obtiene y establece la fecha de inicio.
        /// </summary>
        public DateTime FInicio
        {
            get
            {
                return this.finicio;
            }
            set
            {
                this.finicio = value;
            }
        }

        /// <summary>
        /// Obtiene y establece la fecha de fin.
        /// </summary>
        public DateTime FFin
        {
            get
            {
                return this.ffin;
            }
            set
            {
                this.ffin = value;
            }
        }

        #endregion

        #region//Public Methods

        /// <summary>
        /// Constructor de TBC
        /// </summary>
        /// <param name="DNI">DNI de la persona.</param>
        /// <param name="Ejecutoria">Ejecutoria de TBC.</param>
        /// <param name="Nombre">Nombre de la persona.</param>
        /// <param name="Apellidos">Apellidos de la persona.</param>
        /// <param name="Juzgado">Juzagod de TBC.</param>
        /// <param name="Finicio">Fecha de inicio de cumplimiento.</param>
        /// <param name="Ffin">Fecha final de cumplimiento.</param>
        public ENTBC(string DNI, string Ejecutoria, string Nombre, string Apellidos, string Juzgado, DateTime Finicio, DateTime Ffin)
            : base(DNI, Nombre, Apellidos)
        {
            cad = new CADTBC("GebatDataConnectionString");
            this.id.Add(Ejecutoria);
            this.juzgado = Juzgado;
            this.finicio = Finicio;
            this.ffin = Ffin;
        }

        /// <summary>
        /// Constructor por defecto.
        /// </summary>
        public ENTBC()
            : base()
        {
            cad = new CADTBC("GebatDataConnectionString");
        }

        /// <summary>
        /// Busca en la base de datos la persona TBC por DNI y ejecutoria.
        /// </summary>
        /// <param name="id">Lista formada por [0] -> DNI, [1] -> Ejecutoria</param>
        /// <returns>Persona TBC en formato AEN:</returns>
        public override AEN Read(List<int> id)
        {
            VIEWTBCPeople tbcp = new VIEWTBCPeople("GebatDataConnectionString");
            ENTBC ret = new ENTBC();
            List<object> param = new List<object>();
            param.Add((object)this.id[0]);
            param.Add((object)this.id[1]);
            DataRow row = tbcp.Select(param);
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
        /// Obtiene todosl os tbc de la base de datos.
        /// </summary>
        /// <returns>Lista de TBC en formato AEN.</returns>
        public override List<AEN> ReadAll()
        {
            List<AEN> ret = new List<AEN>();
            VIEWTBCPeople tbcp = new VIEWTBCPeople("GebatDataConnectionString");
            DataTable tabla = tbcp.SelectAll();
            foreach (DataRow rows in tabla.Rows)
            {
                ENTBC nuevo = new ENTBC();
                nuevo.FromRow(rows);
                ret.Add((ENTBC)nuevo);
            }
            return ret;
        }

        /// <summary>
        /// Guarda el TBC en la base de datos. Si es nuevo lo inserta, si ha sido modificado lo modifica en la base de datos.
        /// </summary>
        public override void Save()
        {
            CADPersonas per = new CADPersonas("GebatDataConnectionString");
            if (!this.saved)
            {
                per.Insert(base.ToRow);
                cad.Insert(this.ToRow);
                this.saved = true;
            }
            else
            {
                per.Update(base.ToRow);
                cad.Update(base.ToRow);
            }
        }

        /// <summary>
        /// Elimina el TBC de la base de datos.
        /// </summary>
        public override void Delete()
        {
            CADPersonas per = new CADPersonas("GebatDataConnectionString");
            if (base.saved)
            {
                cad.Delete(this.ToRow);
                per.Delete(base.ToRow);
            }
        }

        #endregion
    }
}
