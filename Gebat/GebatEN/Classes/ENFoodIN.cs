using System;
using GebatCAD.Classes;
using System.Data;
using System.Collections.Generic;

namespace GebatEN.Classes
{
    public class ENFoodIN :AEN
    {
        #region//Private atributes

        private int quantity;
        private DateTime fecha;
        private int type;
        private string nombre = null;

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
                if (this.id != null)
                {
                    ret["Id"] = this.id[0];
                }
                ret["QuantityIn"] = this.quantity;
                ret["FoodType"] = this.type;
                ret["Fecha"] = this.fecha.ToShortDateString();
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
                this.id.Add((int)row["Id"]);
                this.quantity = (int)row["QuantityIn"];
                this.fecha = (DateTime)row["Fecha"];
                if (row["FoodType"] != DBNull.Value)
                {
                    type = (int)row["FoodType"];
                }
            }
        }

        #endregion

        #region//Getters & Setters

        /// <summary>
        /// Obtiene la cantidad entrante.
        /// </summary>
        public int Quantity
        {
            get
            {
                return this.quantity;
            }
        }

        /// <summary>
        /// Obtiene la fecha en la que entró.
        /// </summary>
        public DateTime Fecha
        {
            get
            {
                return this.fecha;
            }
        }

        /// <summary>
        /// Obtiene el indice del tipo de comida al que pertenece.
        /// </summary>
        public int Type
        {
            get
            {
                return this.type;
            }
        }

        /// <summary>
        /// Obtiene el nombre del alimento.
        /// </summary>
        public string Nombre
        {
            get
            {
                if (this.nombre == null)
                {
                    CADFood food = new CADFood("GebatDataConnectionString");
                    List<object> param = new List<object>();
                    param.Add(this.type);
                    DataRow fila = food.Select(param);
                    this.nombre = (string)fila["Name"];
                }
                return this.nombre;
            }
        }

        #endregion

        #region//Public Methods

        /// <summary>
        /// Constructor de comida entrante.
        /// </summary>
        /// <param name="fecha">Fecha en la que entró la comida.</param>
        /// <param name="quantity">Cantidad que entra.</param>
        /// <param name="tipo">Identificador de la comida.</param>
        public ENFoodIN(DateTime fecha, int quantity,int tipo)
            : base()
        {
            cad = new CADEntryFood("GebatDataConnectionString");
            this.quantity = quantity;
            this.fecha = fecha;
            this.type = tipo;
        }

        /// <summary>
        /// Constructor por defecto.
        /// </summary>
        public ENFoodIN()
            : base()
        {
            cad = new CADEntryFood("GebatDataConnectionString");
        }

        /// <summary>
        /// Busca en la base de datos la entrada de alimentos por el id.
        /// </summary>
        /// <param name="id">Identificador por el que se buscará la entrada.</param>
        /// <returns>Entrada en formato AEN.</returns>
        public override AEN Read(List<int> id)
        {
            ENFoodIN ret = new ENFoodIN();
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
        /// Obtiene todas las entradas de comida de la base de datos.
        /// </summary>
        /// <returns>Lista de las entradas de comida en formato AEN.</returns>
        public override List<AEN> ReadAll()
        {
            List<AEN> ret = new List<AEN>();
            DataTable tabla = cad.SelectAll();
            foreach (DataRow rows in tabla.Rows)
            {
                ENFoodIN nueva = new ENFoodIN();
                nueva.FromRow(rows);
                ret.Add((ENFoodIN)nueva);
            }
            return ret;
        }

        #endregion
    }
}
