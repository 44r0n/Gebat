using System;
using System.Collections.Generic;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using GebatCAD.Classes;

namespace GebatEN.Classes
{
    public class ENTBC : AENPersona
    {
        #region//Atributes

        private int idtbc = 0; 
        private string ejecutoria;
        private string juzgado;
        private DateTime finicio;
        private DateTime ffin;
        private int numjornadas;
        private Dictionary<DayOfWeek, bool> horario;

        #endregion

        #region//Private Methods

        private void initDictionary()
        {
            this.horario = new Dictionary<DayOfWeek, bool>();
            this.horario[DayOfWeek.Monday] = true;
            this.horario[DayOfWeek.Tuesday] = true;
            this.horario[DayOfWeek.Wednesday] = true;
            this.horario[DayOfWeek.Thursday] = true;
            this.horario[DayOfWeek.Friday] = true;
            this.horario[DayOfWeek.Saturday] = false;
            this.horario[DayOfWeek.Sunday] = false;
        }

        private bool alreadyInPerson()
        {
            if (new CADPersonas(defaultConnString).SelectWhere("DNI = '" + this.DNI + "'").Rows.Count == 1)
            {
                return true;
            }
            return false;
        }

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
                if (this.idtbc != 0)
                {
                    ret["Id"] = this.idtbc;
                }
                ret["DNI"] = this.DNI;
                ret["Ejecutoria"] = this.ejecutoria;
                ret["Juzgado"] = this.juzgado;
                ret["FInicio"] = this.finicio;
                ret["FFin"] = this.ffin;
                ret["NumJornadas"] = this.numjornadas;
                ret["Lunes"] = this.horario[DayOfWeek.Monday];
                ret["Martes"] = this.horario[DayOfWeek.Tuesday];
                ret["Miercoles"] = this.horario[DayOfWeek.Wednesday];
                ret["Jueves"] = this.horario[DayOfWeek.Thursday];
                ret["Viernes"] = this.horario[DayOfWeek.Friday];
                ret["Sabado"] = this.horario[DayOfWeek.Saturday];
                ret["Domingo"] = this.horario[DayOfWeek.Sunday];
                return ret;
            }
        }

        /// <summary>
        /// Asigna al objeto actual los datos contenientes en el DataRow.
        /// </summary>
        /// <param name="row">Fila con los datos.</param>
        protected override void FromRow(DataRow row)
        {
            base.FromRow(row);
            this.ejecutoria = (string)row["Ejecutoria"];
            this.idtbc = (int)row["Id"];
            this.juzgado = (string)row["Juzgado"];
            this.finicio = (DateTime)row["FInicio"];
            this.ffin = (DateTime)row["FFin"];
            this.numjornadas = (int)row["NumJornadas"];
            this.horario[DayOfWeek.Monday] = (bool)row["Lunes"];
            this.horario[DayOfWeek.Tuesday] = (bool)row["Martes"];
            this.horario[DayOfWeek.Wednesday] = (bool)row["Miercoles"];
            this.horario[DayOfWeek.Thursday] = (bool)row["Jueves"];
            this.horario[DayOfWeek.Friday] = (bool)row["Viernes"];
            this.horario[DayOfWeek.Saturday] = (bool)row["Sabado"];
            this.horario[DayOfWeek.Sunday] = (bool)row["Domingo"];
            this.saved = true;
        }

        #endregion

        #region//Getters & Setters

        /// <summary>
        /// Obtiene y establece la ejecutoria.
        /// </summary>
        public string Ejecutoria//TODO: comprobar que el formato de la ejecutoria a la hora de asignar.
        {
            get
            {
                return this.ejecutoria;
            }
            set
            {
                this.ejecutoria = value;
            }
        }

        /// <summary>
        /// Obtiene y establece el Juzgado.
        /// </summary>
        public string Juzgado
        {
            get
            {
                return this.juzgado;
            }
            set
            {
                this.juzgado = value;
            }
        }

        /// <summary>
        /// Obtiene y establece la fecha de inicio.
        /// </summary>
        public DateTime FInicio
        {
            get
            {
                return this.finicio;
            }
            set
            {
                this.finicio = value;
            }
        }

        /// <summary>
        /// Obtiene y establece la fecha de fin.
        /// </summary>
        public DateTime FFin
        {
            get
            {
                return this.ffin;
            }
            set
            {
                this.ffin = value;
            }
        }

        /// <summary>
        /// Obtiene y establece el número de jornadas que debe realizar.
        /// </summary>
        public int NumJornadas
        {
            get
            {
                return this.numjornadas;
            }
            set
            {
                this.numjornadas = value;
            }
        }

        /// <summary>
        /// Obtiene y establece el horario del TBC.
        /// </summary>
        public Dictionary<DayOfWeek, bool> Horario
        {
            get
            {
                return this.horario;
            }
            set
            {
                this.horario = value;
            }
        }

        #endregion

        #region//Public Methods

        /// <summary>
        /// Constructor de TBC
        /// </summary>
        /// <param name="DNI">DNI de la persona.</param>
        /// <param name="Ejecutoria">Ejecutoria de TBC.</param>
        /// <param name="Nombre">Nombre de la persona.</param>
        /// <param name="Apellidos">Apellidos de la persona.</param>
        /// <param name="Juzgado">Juzagod de TBC.</param>
        /// <param name="Finicio">Fecha de inicio de cumplimiento.</param>
        /// <param name="Ffin">Fecha final de cumplimiento.</param>
        public ENTBC(string DNI, string Ejecutoria, string Nombre, string Apellidos, string Juzgado, DateTime Finicio, DateTime Ffin)
            : base(DNI, Nombre, Apellidos)
        {
            cad = new CADTBC(defaultConnString);
            this.ejecutoria = Ejecutoria;
            this.juzgado = Juzgado;
            this.finicio = Finicio;
            this.ffin = Ffin;
            this.initDictionary();
        }

        /// <summary>
        /// Constructor por defecto.
        /// </summary>
        public ENTBC()
            : base()
        {
            cad = new CADTBC(defaultConnString);
            this.initDictionary();
        }

        /// <summary>
        /// Busca en la base de datos la persona TBC identificador.
        /// </summary>
        /// <param name="id">Identificador por el que se buscará la persona TBC</param>
        /// <returns>Persona TBC en formato AEN:</returns>
        public override AEN Read(List<int> id)
        {
            AVIEW tbcp = new VIEWTBCPeople(defaultConnString);
            ENTBC ret = new ENTBC();
            List<object> param = new List<object>();
            param.Add((object)id[0]);
            DataRow row = tbcp.Select(param);
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
        /// Obtiene todosl os tbc de la base de datos.
        /// </summary>
        /// <returns>Lista de TBC en formato AEN.</returns>
        public override List<AEN> ReadAll()
        {
            List<AEN> ret = new List<AEN>();
            VIEWTBCPeople tbcp = new VIEWTBCPeople(defaultConnString);
            DataTable tabla = tbcp.SelectAll();
            foreach (DataRow rows in tabla.Rows)
            {
                ENTBC nuevo = new ENTBC();
                nuevo.FromRow(rows);
                ret.Add((ENTBC)nuevo);
            }
            return ret;
        }

        /// <summary>
        /// Guarda el TBC en la base de datos. Si es nuevo lo inserta, si ha sido modificado lo modifica en la base de datos.
        /// </summary>
        public override void Save()
        {
            CADPersonas per = new CADPersonas(defaultConnString);
            if (!this.saved)
            {
                if (!alreadyInPerson())
                {
                    per.Insert(base.ToRow);
                }
                this.FromRow(cad.Insert(this.ToRow));
                this.saved = true;
            }
            else
            {
                per.Update(base.ToRow);
                cad.Update(this.ToRow);
            }
        }

        /// <summary>
        /// Elimina el TBC de la base de datos.
        /// </summary>
        public override void Delete()
        {
            CADPersonas per = new CADPersonas(defaultConnString);
            if (this.saved)
            {
                cad.Delete(this.ToRow);
                per.Delete(base.ToRow);
            }
        }

        /// <summary>
        /// Carga los datos de una persona.
        /// </summary>
        /// <param name="dni">DNI por el que se buscará a la persona.</param>
        /// <returns>Lista de objetos AENPersona.</returns>
        public override List<AENPersona> ReadByDNI(string dni)
        {
            List<AENPersona> ret = new List<AENPersona>();
            DataTable tabla = new VIEWTBCPeople(defaultConnString).SelectWhere("DNI = '" + dni + "'");
            foreach (DataRow fila in tabla.Rows)
            {
                ENTBC nuevo = new ENTBC();
                nuevo.FromRow(fila);
                ret.Add((AENPersona)nuevo);
            }
            return ret;
        }

        public void InicioSentenciaToPDF(string ruta)
        {
            Document document = new Document();
            PdfWriter.GetInstance(document, new FileStream(ruta, FileMode.OpenOrCreate));
            document.Open();
            Paragraph titulo = new Paragraph();
            titulo.Alignment = Element.ALIGN_CENTER;
            titulo.Font = FontFactory.GetFont(FontFactory.TIMES_BOLD, 16);
            Paragraph voidparagraph = new Paragraph();
            voidparagraph.Add("\n");
            titulo.Add("COMUNICACIÓN DE CUMPLIMIENTO DE TRABAJO A FAVOR DE LA COMUNIDAD\n");
            document.Add(titulo);
            document.Add(voidparagraph);
            Paragraph cabecera = new Paragraph();
            cabecera.Alignment = Element.ALIGN_JUSTIFIED;
            cabecera.Font = FontFactory.GetFont(FontFactory.TIMES, 12);
            cabecera.Add("DE ENTIDAD: [CONSIGNAS SOLIDARIAS]\n\n");
            cabecera.Add("AL SERIVICO SOCIAL PENITENCIARIO DE [JUZGADO]\n\n\n");
            document.Add(cabecera);
            Paragraph cuerpo = new Paragraph();
            cuerpo.Alignment = Element.ALIGN_JUSTIFIED;
            cuerpo.Font = FontFactory.GetFont(FontFactory.TIMES, 12);
            cuerpo.Add("Por la presente los comunicamos que [TBC.Nombre + TBC.Apellidos] con DNI [TBC.DNI] con respecto al cumplimiento de trabajo en beneficio a la comunidad, en Ejec. [TBC.Ejecutoria] del Juzgado J.P. [TBC.Juzgado] se ha producido la siguiente situación:\n\n");
            cuerpo.Add("                [X si es de inicio]Iniciación de cumplimiento Fecha: [TBC.Finicio]\n                Horario: –------------");
            cuerpo.Add("\n\n");
            document.Add(cuerpo);
            Paragraph pie = new Paragraph();
            pie.Alignment = Element.ALIGN_RIGHT;
            pie.Font = FontFactory.GetFont(FontFactory.TIMES, 12);
            pie.Add("En Elda a [TBC.Fin]                        \n");
            pie.Add("Por la entidad.                        \n\n\n\n");
            pie.Add("Firmado:                       ");
            document.Add(pie);
            document.Close();
        }

        #endregion
    }
}
