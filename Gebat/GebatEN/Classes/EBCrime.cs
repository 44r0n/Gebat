using System;
using System.Collections.Generic;
using System.Data;
using GebatCAD.Classes;

namespace GebatEN.Classes
{
    public class EBCrime : AEB
    {
        #region//Atributes

        private string name;

        #endregion

        #region//Private Methods

        private void initADL()
        {
            adl = new ADL(defaultConnString, "crimes", "Id");
        }

        #endregion

        #region//Protected Methods

        protected override void insert()
        {
            adl.ExecuteNonQuery("INSERT INTO crimes (Name) VALUES (@Name)", this.name);
            this.id.Add((int)adl.Last()["Id"]);
        }

        protected override void update()
        {
            adl.ExecuteNonQuery("UPDATE crimes SET Name = @Name WHERE Id = @Id", this.name, (int)this.id[0]);
        }

        protected override void delete()
        {
            adl.ExecuteNonQuery("DELETE FROM crimes WHERE Id = @Id", (int)this.id[0]);
        }

        #endregion

        #region//Internal Methods

        /// <summary>
        /// Obtiene el objeto actual en tipo DataRow de forma que corresponde a la base de datos.
        /// </summary>
        internal override DataRow ToRow
        {
            get 
            {
                DataRow ret = adl.GetVoidRow;
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
        /// <param name="row">Fila a convertir</param>
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
        public EBCrime(string name)
            :base()
        {
            if (name == null)
            {
                throw new NullReferenceException("The name cannot be null");
            }
            this.name = name;
            initADL();
        }

        /// <summary>
        /// Constructor por defecto.
        /// </summary>
        public EBCrime()
            :base()
        {
            initADL();
        }

        /// <summary>
        /// Busca en la base de datos el delito por el id.
        /// </summary>
        /// <param name="id">Identificador por el que se buscará el delito</param>
        /// <returns>Delito en formato AEN.</returns>
        public override AEB Read(List<object> id)
        {
            EBCrime ret = new EBCrime();
            DataRow row = adl.Select("SELECT * FROM crimes WHERE Id = @Id",(int)id[0]).Rows[0];
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
        public override List<AEB> ReadAll()
        {
            List<AEB> ret = new List<AEB>();
            DataTable table = adl.SelectAll();
            foreach (DataRow rows in table.Rows)
            {
                EBCrime nuevo = new EBCrime();
                nuevo.FromRow(rows);
                ret.Add((EBCrime)nuevo);
            }
            return ret;
        }
        #endregion
    }
}
