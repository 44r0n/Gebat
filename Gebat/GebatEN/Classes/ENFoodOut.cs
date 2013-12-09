using System;
using GebatCAD.Classes;
using System.Data;
using System.Collections.Generic;

namespace GebatEN.Classes
{
    public class ENFoodOut: AEN
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

                ret["QuantityOut"] = this.quantity;
                ret["FoodType"] = this.type;
                ret["Fecha"] = this.fecha;
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
                this.quantity = (int)row["QuantityOut"];
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
        /// Obtiene la cantidad saliente.
        /// </summary>
        public int Quantity
        {
            get
            {
                return this.quantity;
            }
        }

        /// <summary>
        /// Obtiene la fecha en la que salió.
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

        public string Nombre
        {
            get
            {
                if (this.nombre == null)
                {
                    CADFood food = new CADFood(defaultConnString);
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
        /// Constructor de comida saliente.
        /// </summary>
        /// <param name="fecha">Fecha en la que salió la comida.</param>
        /// <param name="quantity">Cantidad que sale.</param>
        /// <param name="tipo">Identificador de la comida.</param>
        public ENFoodOut(DateTime fecha, int quantity, int tipo)
            :base()
        {
            cad = new CADOutgoingFood(defaultConnString);
            this.quantity = quantity;
            this.fecha = fecha;
            this.type = tipo;
        }

        /// <summary>
        /// Constructor por defecto.
        /// </summary>
        public ENFoodOut()
            : base()
        {
            cad = new CADOutgoingFood(defaultConnString);
        }

        /// <summary>
        /// Busca en la bse de datos la salida de alimentos por el identificador.
        /// </summary>
        /// <param name="id">Identificador por el que se buscará la salida de alimentos</param>
        /// <returns>Salida en formato AEN.</returns>
        public override AEN Read(List<int> id)
        {
            ENFoodOut ret = new ENFoodOut();
            List<object> param = new List<object>();
            param.Add((object)id[0]);
            DataRow row = cad.Select(param);
            if(row != null)
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
        /// Obtiene todas las salidas de comida de la base de datos.
        /// </summary>
        /// <returns>Lista de las salidas de comida en formato AEN.</returns>
        public override List<AEN> ReadAll()
        {
            List<AEN> ret = new List<AEN>();
            DataTable tabla = cad.SelectAll();
            foreach (DataRow rows in tabla.Rows)
            {
                ENFoodOut nueva = new ENFoodOut();
                nueva.FromRow(rows);
                ret.Add((ENFoodOut)nueva);
            }
            return ret;
        }

        #endregion
    }
}
