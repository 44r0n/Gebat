using System;
using System.Collections.Generic;
using System.Data;
using GebatCAD.Classes;
using GebatEN.Enums;

namespace GebatEN.Classes
{
    public class EBFega : AEBConcession
    {

        #region//Atributes

        private FegaStates state;
        protected VIEW vfega;

        #endregion

        #region//Private Methods

        private void initADL()
        {
            adl = new ADL(defaultConnString, "fega", "Concession");
            vfega = new VIEW(defaultConnString, "fegadata", "Id");
        }

        #endregion

        #region//Protected Methods

        protected override void insert()
        {
            adl.ExecuteNonQuery("INSERT INTO fega (Concession, State) VALUES (@Concession, @State)", (int)this.id[0], state.ToString());
        }

        protected override void update()
        {
            adl.ExecuteNonQuery("UPDATE fega SET State = @State", state.ToString());
        }

        protected override void delete()
        {
            adl.ExecuteNonQuery("DELETE FROM fega WHERE Concession = @Concession", this.id[0]);
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
                ret["Concession"] = (int)this.id[0];
                return ret;
            }
        }

        /// <summary>
        /// Asigna al objeto actual los datos contenientes en el DataRow 
        /// </summary>
        /// <param name="row">Fila con los datos.</param>
        internal override void FromRow(DataRow row)
        {
            base.FromRow(row);
            List<object> ids = new List<object>();
            state = (FegaStates)Enum.Parse(typeof(FegaStates), (string)row["State"]);
        }

        #endregion

        #region//Getter & Setters

        /// <summary>
        /// Obteiene y establece el estado de la concesión.
        /// </summary>
        public FegaStates State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }

        /// <summary>
        /// Obtiene y establece la fecha de la concesión.
        /// </summary>
        public override DateTime FinishDate
        {
            get
            {
                if (!checkdate)
                {
                    return base.FinishDate;
                }
                else
                {
                    if (DateTime.Today >= finishDate)
                    {
                        return base.FinishDate.AddMonths(6);
                    }
                    else
                    {
                        return base.FinishDate;
                    }
                }
            }
            set
            {
                base.FinishDate = value;
            }
        }

        #endregion

        #region//Public Methods

        /// <summary>
        /// Constructor por defecto.
        /// </summary>
        public EBFega()
            : base()
        {
            initADL();
            state = FegaStates.Awaiting;
        }

        /// <summary>
        /// Constructor con parámetros.
        /// </summary>
        /// <param name="beginDate">Fecha de inicio de la concesión.</param>
        /// <param name="finishDate">Fecha final de la concesión.</param>
        /// <param name="notes">Notas sobre la concesión.</param>
        /// <param name="state">Estado de la concesión.</param>
        public EBFega(DateTime beginDate, DateTime finishDate, string notes, FegaStates state)
            : base(beginDate, finishDate, notes)
        {
            initADL();
            this.state = state;
        }

        /// <summary>
        /// Busca en la base de datos la concesión de tipo fega mediante el identificador. 
        /// </summary>
        /// <param name="id">Identificador por el que se buscará la concesión.</param>
        /// <returns>Concesión en formato AEB.</returns>
        public override AEB Read(List<object> id)
        {
            EBFega ret;
            DataRow row = vfega.Select("SELECT * FROM fegadata WHERE Id = @Id", (int)id[0]).Rows[0];
            if (row != null)
            {
                ret = new EBFega();
                ret.FromRow(row);
            }
            else
            {
                ret = null;
            }
            return ret;
        }

        /// <summary>
        /// Obtiene todas las concesiones de tipo fega.
        /// </summary>
        /// <returns></returns>
        public override List<AEB> ReadAll()
        {
            List<AEB> ret = new List<AEB>();
            DataTable table = vfega.SelectAll();
            foreach (DataRow rows in table.Rows)
            {
                EBFega newfega = new EBFega();
                newfega.FromRow(rows);
                ret.Add((EBFega)newfega);
            }
            return ret;
        }

        /// <summary>
        /// Guarfa la concesión en la base de datos. Si es nueva la inserta, si ha sido modificada la modifica en la base de datos.
        /// </summary>
        public override void Save()
        {
            if (!this.saved)
            {
                base.insert();
                this.insert();
                this.saved = true;
            }
            else
            {
                base.update();
                this.update();
            }
        }

        public override void Delete()
        {
            this.delete();
            base.Delete();
        }

        #endregion

        #region//Static Methods

        public static bool IsFega(int id)
        {
            VIEW vfega = new VIEW(defaultConnString, "fega", "Concession");
            DataTable table = vfega.Select("SELECT * FROM fega WHERE Concession = @Concession", id);
            return table.Rows.Count == 1;
        }

        #endregion
    }
}
