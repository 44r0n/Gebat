using System;
using System.Collections.Generic;
using System.Data;
using GebatCAD.Classes;

namespace GebatEN.Classes
{
    abstract class AENPersona : AEN
    {
        #region//Atributes
        private CADPersonas personas;
       //private string dni -> this.id[0] hace referencia al DNI;
        private string apellidos;
        private string nombre;

        #endregion

        #region//Protected Methods

        /// <summary>
        /// Obtiene el objeto actual en tipo DataRow de forma que corresponde en la base de datos.
        /// </summary>
        protected override DataRow ToRow
        {
            get 
            {
                DataRow ret = personas.GetVoidRow;
                ret["DNI"] = (string)this.id[0];
                ret["Nombre"] = this.nombre;
                ret["Apellidos"] = this.apellidos;
                return ret;
            }
        }

        /// <summary>
        /// Asigna al objeto actual los datos contenientes en el DataRow.
        /// </summary>
        /// <param name="row">Fila con los datos.</param>
        protected override void FromRow(DataRow row)
        {
            if (row != null)
            {
                this.id = new List<object>();
                this.id.Add((object)row["DNI"]);
                this.nombre = (string)row["Nombre"];
                this.apellidos = (string)row["Apellidos"];
            }
            else
            {
                throw new NullReferenceException("Cannot convert from row, the row is null");
            }
        }

        #endregion

        #region//Getters & Setters

        /// <summary>
        /// Obtiene y establece el DNI.
        /// </summary>
        public string DNI
        {
            get
            {
                return (string)this.id[0];
            }
            set
            {
                this.id[0] = value;
            }
        }

        /// <summary>
        /// Obtiene y establece el nombre.
        /// </summary>
        public string Nombre
        {
            get
            {
                return this.nombre;
            }
            set
            {
                this.nombre = value;
            }
        }

        /// <summary>
        /// Obtiene y establece los apellidos.
        /// </summary>
        public string Apellidos
        {
            get
            {
                return this.apellidos;
            }
            set
            {
                this.apellidos = value;
            }
        }

        #endregion

        public AENPersona(string DNI, string Nombre, string Apellidos)
            :base()
        {
            personas = new CADPersonas("GebatDataConnectionString");
            this.id = new List<object>();
            this.DNI = DNI;
            this.nombre = Nombre;
            this.apellidos = Apellidos;
        }
    }
}
