using System;
using System.Collections.Generic;
using System.Data;
using GebatCAD.Classes;
using GebatEN.Enums;

namespace GebatEN.Classes
{
    public class EBFamiliar : AEBPerson
    {
        #region//Atributes

        private int idfam = 0;
        internal int dossier = 0;
        private int income = 0;
        private VIEW vfam;

        #endregion

        #region//Private Methods

        private void initADL()
        {
            adl = new ADL(defaultConnString, "familiars", "Id");
            vfam = new VIEW(defaultConnString, "familiardata", "Id");
        }

        #endregion

        #region//Protected Methods

        protected override void insert()
        {
            adl.ExecuteNonQuery("INSERT INTO familiars (DNI, Dossier, Income) VALUES (@DNI, @Dossier, @Income)", this.DNI, this.dossier, this.income);
            this.id.Add((int)adl.Last()["Id"]);
        }

        protected override void update()
        {
            adl.ExecuteNonQuery("UPDATE familiars SET DNI = @DNI, Dossier = @Dossier, Income = @Income WHERE Id = @Id", this.DNI, this.dossier, this.income, (int)this.id[0]);
        }

        protected override void delete()
        {
            adl.ExecuteNonQuery("DELETE FROM familiars WHERE Id = @Id", (int)this.id[0]);
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
                if (this.idfam != 0)
                {
                    ret["Id"] = this.idfam;
                }
                ret["DNI"] = this.DNI;
                if (dossier != 0)
                {
                    ret["Dossier"] = dossier;
                }
                ret["Income"] = this.income;
                return ret;
            }
        }
        
        /// <summary>
        /// Asigna al objeto actual los datos contenientes en el DataRow.
        /// </summary>
        /// <param name="row"></param>
        internal override void FromRow(DataRow row)
        {
            base.FromRow(row);
            this.idfam = (int)row["Id"];
            this.dossier = (int)row["Dossier"];
            this.income = (int)row["Income"];
            this.saved = true;
        }

        #endregion

        #region//Getters & Setters
    
        /// <summary>
        /// Obtiene y establece el salario medio del familiar.
        /// </summary>
        public int Income
        {
            get
            {
                return income;
            }
            set
            {
                this.income = value;
            }
        }

        #endregion

        #region//Public Methods

        /// <summary>
        /// Constructor de Familiar
        /// </summary>
        /// <param name="DNI">DNI de la persona.</param>
        /// <param name="Name">Nombre de la persona.</param>
        /// <param name="Surname">Apellidos de la persona.</param>
        /// <param name="BirthDate">Fecha de nacimiento de la persona.</param>
        /// <param name="Gender">Genero de la persona.</param>
        public EBFamiliar(string DNI, string Name, string Surname, DateTime BirthDate, MyGender Gender, int Dossier, int Income)
            : base(DNI, Name, Surname, BirthDate, Gender)
        {
            initADL();
            this.dossier = Dossier;
            this.income = Income;
        }

        /// <summary>
        /// Constructor por defecto.
        /// </summary>
        public EBFamiliar()
            :base()
        {
            initADL();
        }

        /// <summary>
        /// Busca en la base de datos el Familiar mediante identificador.
        /// </summary>
        /// <param name="id">Identificador por el que se buscará el familiar.</param>
        /// <returns>Familiar en formato AEN.</returns>
        public override AEB Read(List<object> id)
        {
            EBFamiliar ret = new EBFamiliar();
            DataRow row = vfam.Select("SELECT * FROM familiardata WHERE Id = @Id",(int)id[0]).Rows[0];
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
        /// Obtiene todos los familiares de la base de datos.
        /// </summary>
        /// <returns></returns>
        public override List<AEB> ReadAll()
        {
            List<AEB> ret = new List<AEB>();
            DataTable table = vfam.SelectAll();
            foreach (DataRow rows in table.Rows)
            {
                EBFamiliar newfamiliar = new EBFamiliar();
                newfamiliar.FromRow(rows);
                ret.Add((EBFamiliar)newfamiliar);
            }
            return ret;
        }

        /// <summary>
        /// Guarda el Familiar en la base de datos.Si es nuevo lo inserta, si ha sido modificado lo modifica en la base de datos.
        /// </summary>
        public override void Save()
        {
            if (!this.saved)
            {
                if (!alreadyInPerson())
                {
                    base.insert();
                }
                this.insert();
                this.saved = true;
            }
            else
            {
                base.update();
                this.update();
            }
        }

        /// <summary>
        /// Carga los datos de una persona.
        /// </summary>
        /// <param name="dni">DNI por el que se buscará a la persona.</param>
        /// <returns>Lista de objetos AENPersona.</returns>
        public override List<AEBPerson> ReadByDNI(string dni)
        {
            List<AEBPerson> ret = new List<AEBPerson>();
            DataTable table = adl.Select("SELECT * FROM familiardata WHERE DNI = @DNI", dni);
            foreach (DataRow row in table.Rows)
            {
                EBFamiliar newfamiliar = new EBFamiliar();
                newfamiliar.FromRow(row);
                ret.Add((AEBPerson)newfamiliar);
            }
            return ret;
        }

        #endregion
    }
}
