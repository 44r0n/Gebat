using System;
using GebatCAD.Classes;
using System.Data;
using System.Collections.Generic;

namespace GebatEN.Classes
{
    public class EBFoodIN :AEB
    {
        #region//Private atributes

        private int quantity;
        private DateTime date;
        private int type;
        private string name = null;

        #endregion

        #region//Private Methods

        private void initADL()
        {
            adl = new ADL(defaultConnString, "entryfood", "Id");
        }

        #endregion

        #region//Protected Methods

        protected override void insert()
        {
            adl.ExecuteNonQuery("INSERT INTO entryfood (FoodType, QuantityIn, DateTime) VALUES (@FoodType, @QuantityIn, @DateTime)", this.type, this.quantity, this.date);
            this.id.Add((int)adl.Last()["Id"]);
        }

        protected override void update()
        {
            adl.ExecuteNonQuery("UPDATE entryfood SET FoodType = @FoodType, QuantityIn = @QuantityIn, DateTime = @DateTime WHERE Id = @Id", this.type, this.quantity, this.date, (int)this.id[0]);
        }

        protected override void delete()
        {
            adl.ExecuteNonQuery("DELETE FROM entryfood WHERE Id = @Id", (int)this.id[0]);
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
                DataRow ret = adl.GetVoidRow;
                if (this.id != null)
                {
                    ret["Id"] = this.id[0];
                }
                ret["QuantityIn"] = this.quantity;
                ret["FoodType"] = this.type;
                ret["DateTime"] = this.date.ToShortDateString();
                return ret;
            }
        }

        /// <summary>
        /// Asigna al objeto actual los datos contenientes en el DataRow.
        /// </summary>
        /// <param name="row">Fila con los datos.</param>
        internal override void FromRow(DataRow row)
        {
            if (row != null)
            {
                this.id = new List<object>();
                this.id.Add((int)row["Id"]);
                this.quantity = (int)row["QuantityIn"];
                this.date = (DateTime)row["DateTime"];
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
        public DateTime Date
        {
            get
            {
                return this.date;
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
        public string Name
        {
            get
            {
                if (this.name == null)
                {
                    DataRow row = adl.Select("SELECT Name FROM food WHERE Id = @Id",this.type).Rows[0];
                    this.name = (string)row["Name"];
                }
                return this.name;
            }
        }

        #endregion

        #region//Public Methods

        /// <summary>
        /// Constructor de comida entrante.
        /// </summary>
        /// <param name="Date">Fecha en la que entró la comida.</param>
        /// <param name="Quantity">Cantidad que entra.</param>
        /// <param name="Type">Identificador de la comida.</param>
        public EBFoodIN(DateTime Date, int Quantity,int Type)
            : base()
        {
            initADL();
            this.quantity = Quantity;
            this.date = Date;
            this.type = Type;
        }

        /// <summary>
        /// Constructor por defecto.
        /// </summary>
        public EBFoodIN()
            : base()
        {
            initADL();
        }

        /// <summary>
        /// Busca en la base de datos la entrada de alimentos por el id.
        /// </summary>
        /// <param name="id">Identificador por el que se buscará la entrada.</param>
        /// <returns>Entrada en formato AEN.</returns>
        public override AEB Read(List<object> id)
        {
            EBFoodIN ret = new EBFoodIN();
            DataRow row = adl.Select("SELECT * FROM entryfood WHERE Id = @Id", (int)id[0]).Rows[0];
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
        public override List<AEB> ReadAll()
        {
            List<AEB> ret = new List<AEB>();
            DataTable table = adl.SelectAll();
            foreach (DataRow rows in table.Rows)
            {
                EBFoodIN newfoodin = new EBFoodIN();
                newfoodin.FromRow(rows);
                ret.Add((EBFoodIN)newfoodin);
            }
            return ret;
        }

        #endregion
    }
}
