using System;
using System.Collections.Generic;
using System.Data;
using GebatCAD.Classes;

namespace GebatEN.Classes
{
    public class EBFresco : AEBConcession
    {
        #region//Atributes

        private VIEW vfresco;

        #endregion

        #region//Private Methods

        private void initADL()
        {
            adl = new ADL(defaultConnString, "fresco", "Concession");
            vfresco = new VIEW(defaultConnString, "frescodata", "Id");
        }

        #endregion

        #region//Protected Methods

        protected override void insert()
        {
            adl.ExecuteNonQuery("INSERT INTO fresco (Concession) VALUES (@Concession)", (int)this.id[0]);
        }

        protected override void delete()
        {
            adl.ExecuteNonQuery("DELETE FROM fresco WHERE Concession = @Concession", (int)this.id[0]);
        }

        #endregion

        #region//Internal Methods



        #endregion

        #region//Public Methods

        /// <summary>
        /// Constructor con parámetros.
        /// </summary>
        /// <param name="beginDate">Fecha de inicio de la concesión.</param>
        /// <param name="finishDate">Fecha final de la concesión.</param>
        /// <param name="notes">Notas sobre la concesión.</param>
        public EBFresco(DateTime beginDate, DateTime finishDate, string notes)
            : base(beginDate, finishDate, notes)
        {
            initADL();
        }

        /// <summary>
        /// Constructor por defecto.
        /// </summary>
        public EBFresco()
            : base()
        {
            initADL();
        }

        /// <summary>
        /// Busca en la base de datos la concesión de tipo fresco mediante identificador.
        /// </summary>
        /// <param name="id">Identificador por el que se buscará la concesión.</param>
        /// <returns>Concesión en formato AEB.</returns>
        public override AEB Read(List<object> id)
        {
            EBFresco ret;
            DataRow row = vfresco.Select("SELECT * FROM frescodata WHERE Id = @Id", (int)id[0]).Rows[0];
            if (row != null)
            {
                ret = new EBFresco();
                ret.FromRow(row);
            }
            else
            {
                ret = null;
            }
            return ret;
        }

        /// <summary>
        /// Obtiene todas las concesiones de tipo Fresco.
        /// </summary>
        /// <returns></returns>
        public override List<AEB> ReadAll()
        {
            List<AEB> ret = new List<AEB>();
            DataTable table = vfresco.SelectAll();
            foreach (DataRow rows in table.Rows)
            {
                EBFresco newfresco = new EBFresco();
                newfresco.FromRow(rows);
                ret.Add((EBFresco)newfresco);
            }
            return ret;
        }

        /// <summary>
        /// Guarda la concesión en la base de datos. Si es nueva la inserta, si ha sido modificada la modifica en la base de datos.
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
            }
        }

        /// <summary>
        /// Borra la concesión actual.
        /// </summary>
        public override void Delete()
        {
            this.delete();
            base.delete();
        }
        #endregion

        #region//Static Methods

        /// <summary>
        /// Comprueba si el identificador pertenece a una concesión fresco.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool IsFresco(int id)
        {
            VIEW vfresco = new VIEW(defaultConnString, "fresco", "Concession");
            DataTable table = vfresco.Select("SELECT * FROM fresco WHERE Concession = @Concession", id);
            if (table.Rows.Count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
