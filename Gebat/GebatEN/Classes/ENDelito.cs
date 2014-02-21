﻿using System;
using System.Collections.Generic;
using System.Data;
using GebatCAD.Classes;

namespace GebatEN.Classes
{
    public class ENDelito : AEN
    {
        #region//Atributes

        private string name;

        #endregion

        #region//Internal Methods

        /// <summary>
        /// Obtiene el objeto actual en tipo DataRow de forma que corresponde a la base de datos.
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
                ret["Name"] = this.name;
                return ret;
            }
        }

        /// <summary>
        /// Asigna al objeto actual los datos contenientes en el DataRow.
        /// </summary>
        /// <param name="row"></param>
        internal override void FromRow(DataRow row)
        {
            if (row != null)
            {
                this.id = new List<object>();
                this.id.Add((int)row["Id"]);
                this.name = (string)row["Name"];
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
        /// Obtiene y establece el nombre del delito.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                this.name = value;
            }
        }

        #endregion

        #region//Public Methods

        /// <summary>
        /// Constructor que inicializa el objeto.
        /// </summary>
        /// <param name="name">Nombre del delito.</param>
        public ENDelito(string name)
        {
            if (name == null)
            {
                throw new NullReferenceException("The name cannot be null");
            }
            this.name = name;
            cad = new ADLCrime(defaultConnString);
        }

        /// <summary>
        /// Constructor por defecto.
        /// </summary>
        public ENDelito()
        {
            cad = new ADLCrime(defaultConnString);
        }

        /// <summary>
        /// Busca en la base de datos el delito por el id.
        /// </summary>
        /// <param name="id">Identificador por el que se buscará el delito</param>
        /// <returns>Delito en formato AEN.</returns>
        public override AEN Read(List<int> id)
        {
            ENDelito ret = new ENDelito();
            List<object> param = new List<object>();
            param.Add((object)id[0]);
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
        /// Obtiene todos los delitos de la base de datos.
        /// </summary>
        /// <returns>Lista de delitos en formato AEN.</returns>
        public override List<AEN> ReadAll()
        {
            List<AEN> ret = new List<AEN>();
            DataTable tabla = cad.SelectAll();
            foreach (DataRow rows in tabla.Rows)
            {
                ENDelito nuevo = new ENDelito();
                nuevo.FromRow(rows);
                ret.Add((ENDelito)nuevo);
            }
            return ret;
        }
        #endregion
    }
}
