using System;
using GebatCAD.Classes;
using System.Data;
using System.Collections.Generic;

namespace GebatEN.Classes
{
    public class EBFoodOut: AEB
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
            adl = new ADL(defaultConnString, "outgoingfood", "Id");
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

                ret["QuantityOut"] = this.quantity;
                ret["FoodType"] = this.type;
                ret["DateTime"] = this.date;
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
                this.quantity = (int)row["QuantityOut"];
                this.date = (DateTime)row["DateTime"];
                if (row["FoodType"] != DBNull.Value)
                {
                    type = (int)row["FoodType"];
                }
            }
        }

        #endregion

        #region//Protected Methods

        protected override void insert()
        {
            adl.ExecuteNonQuery("INSERT INTO outgoingfood (FoodType, QuantityOut, DateTime) VALUES (@Type, @Quantity, @Date)", this.type, this.quantity, this.date);
            this.id.Add(adl.Last());
        }

        protected override void update()
        {
            adl.ExecuteNonQuery("UPDATE outgoingfood SET FoodType = @Type, QuantityOut = @Quantity, DateTime = @Date WHERE Id = @Id",this.type,this.quantity,this.date,(int)this.id[0]);
        }

        protected override void delete()
        {
            adl.ExecuteNonQuery("DELETE FROM outgoingfoos WHERE Id = @Id", (int)this.id[0]);
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
                    DataRow fila = adl.Select("SELECT Name FROM food WHERE Id = @Id",this.type).Rows[0];
                    this.name = (string)fila["Name"];
                }
                return this.name;
            }
        }

        #endregion

        #region//Public Methods

        /// <summary>
        /// Constructor de comida saliente.
        /// </summary>
        /// <param name="Date">Fecha en la que salió la comida.</param>
        /// <param name="Quantity">Cantidad que sale.</param>
        /// <param name="Type">Identificador de la comida.</param>
        public EBFoodOut(DateTime Date, int Quantity, int Type)
            :base()
        {
            initADL();
            this.quantity = Quantity;
            this.date = Date;
            this.type = Type;
        }

        /// <summary>
        /// Constructor por defecto.
        /// </summary>
        public EBFoodOut()
            : base()
        {
            initADL();
        }

        /// <summary>
        /// Busca en la bse de datos la salida de alimentos por el identificador.
        /// </summary>
        /// <param name="id">Identificador por el que se buscará la salida de alimentos</param>
        /// <returns>Salida en formato AEN.</returns>
        public override AEB Read(List<object>id)
        {
            EBFoodOut ret = new EBFoodOut();
            DataRow row = adl.Select("SELECT * FROM outgoingfood WHERE Id = @Id",(int)id[0]).Rows[0];
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
        public override List<AEB> ReadAll()
        {
            List<AEB> ret = new List<AEB>();
            DataTable table = adl.SelectAll();
            foreach (DataRow rows in table.Rows)
            {
                EBFoodOut newfoodout = new EBFoodOut();
                newfoodout.FromRow(rows);
                ret.Add((EBFoodOut)newfoodout);
            }
            return ret;
        }

        #endregion
    }
}
