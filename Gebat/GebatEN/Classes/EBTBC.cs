using System;
using System.Collections.Generic;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using GebatCAD.Classes;
using GebatEN.Enums;

namespace GebatEN.Classes
{
    public class EBTBC : AEBPerson
    {
        #region//Atributes

        private int idtbc = 0; 
        private string judgement;
        private string court;
        private DateTime begindate;
        private DateTime finishdate;
        private int numjourney;
        private Dictionary<DayOfWeek, bool> timetable;
        private EBCrime crime;
        private VIEW tbcp;

        #endregion

        #region//Private Methods

        private void initADL()
        {
            adl = new ADL(defaultConnString, "tbc", "Id");
            tbcp = new VIEW(defaultConnString, "tbcpeople", "Id");
        }

        private void initDictionary()
        {
            this.timetable = new Dictionary<DayOfWeek, bool>();
            this.timetable[DayOfWeek.Monday] = true;
            this.timetable[DayOfWeek.Tuesday] = true;
            this.timetable[DayOfWeek.Wednesday] = true;
            this.timetable[DayOfWeek.Thursday] = true;
            this.timetable[DayOfWeek.Friday] = true;
            this.timetable[DayOfWeek.Saturday] = false;
            this.timetable[DayOfWeek.Sunday] = false;
        }

        private Paragraph title()
        {
            Paragraph ret = new Paragraph();
            ret.Alignment = Element.ALIGN_CENTER;
            ret.Font = FontFactory.GetFont(FontFactory.TIMES_BOLD, 16);
            ret.Add("COMUNICACIÓN DE CUMPLIMIENTO DE TRABAJO A FAVOR DE LA COMUNIDAD\n");
            return ret;
        }

        private Paragraph head()
        {
            Paragraph ret = new Paragraph();
            ret.Alignment = Element.ALIGN_JUSTIFIED;
            ret.Font = FontFactory.GetFont(FontFactory.TIMES, 12);
            ret.Add("DE ENTIDAD: CONSIGNAS SOLIDARIAS\n\n");//TODO: obtener el nombre de consignas solidarias desde el archivo de configuración.
            ret.Add("AL SERIVICO SOCIAL PENITENCIARIO DE "+this.court+"\n\n\n");
            return ret;
        }

        private Paragraph body()
        {
            Paragraph ret = new Paragraph();
            ret.Alignment = Element.ALIGN_JUSTIFIED;
            ret.Font = FontFactory.GetFont(FontFactory.TIMES, 12);
            ret.Add("Por la presente los comunicamos que "+this.Name + " " + this.Surname +" con DNI "+ this.DNI +" con respecto al cumplimiento de trabajo en beneficio a la comunidad, en Ejec. "+ this.judgement +" del Juzgado J.P. "+ this.court +" se ha producido la siguiente situación:\n\n");
            return ret;
        }

        private Paragraph beginBody()
        {
            Paragraph ret = body();
            ret.Add("                X Iniciación de cumplimiento Fecha: "+ this.begindate.ToShortDateString()+ "\n                Horario: ");
            if (this.timetable[DayOfWeek.Monday])
            {
                ret.Add("Lunes ");
            }
            if (this.timetable[DayOfWeek.Tuesday])
            {
                ret.Add("Martes ");
            }
            if (this.timetable[DayOfWeek.Wednesday])
            {
                ret.Add("Miércoles ");
            }
            if (this.timetable[DayOfWeek.Thursday])
            {
                ret.Add("Jueves ");
            }
            if (this.timetable[DayOfWeek.Friday])
            {
                ret.Add("Viernes ");
            }
            if (this.timetable[DayOfWeek.Saturday])
            {
                ret.Add("Sábado ");
            }
            if (this.timetable[DayOfWeek.Sunday])
            {
                ret.Add("Domingo");
            }

            ret.Add("\n\n");
            return ret;
        }

        private Paragraph foot()
        {
            Paragraph ret = new Paragraph();
            ret.Alignment = Element.ALIGN_RIGHT;
            ret.Font = FontFactory.GetFont(FontFactory.TIMES, 12);
            ret.Add("En Elda a "+ this.finishdate.ToShortDateString() + "                        \n");
            ret.Add("Por la entidad.                        \n\n\n\n");
            ret.Add("Firmado:                       ");
            return ret;
        }

        private Paragraph endBody()
        {
            Paragraph ret = body();
            ret.Add("               X Finalización de cumplimiento: " + this.finishdate.ToShortDateString() + "\n                        Total de Jornadas cumplidas: "+this.numjourney);
            ret.Add("\n\n");
            return ret;
        }

        private Paragraph signatureTitle()
        {
            Paragraph ret = new Paragraph();
            ret.Alignment = Element.ALIGN_JUSTIFIED;
            ret.Font = FontFactory.GetFont(FontFactory.TIMES_BOLD, 16);
            ret.Add("REGISTRO DE PRESENTACIONES\n");
            return ret;
        }

        private Paragraph singarutreBody()
        {
            Paragraph body = new Paragraph();
            body.Alignment = Element.ALIGN_JUSTIFIED;
            body.Font = FontFactory.GetFont(FontFactory.TIMES, 12);
            body.Add("Nombre y apellidos: " + this.Name + " " + this.Surname + "\nDNI: " + this.DNI + " Ejecutoria: " + this.judgement + " Juzgado: "+ this.court +"\n");
            return body;
        }

        private PdfPTable beginTable()
        {
            Font f = FontFactory.GetFont(FontFactory.TIMES, 12, Element.ALIGN_CENTER);

            PdfPTable table = new PdfPTable(4);
            PdfPCell cell = new PdfPCell(new Phrase(new Chunk("Firma del iteresado", f)));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(new Chunk("Fecha control", f)));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(new Chunk("Firma responable actividad", f)));
            table.AddCell(cell);
            cell = new PdfPCell(new Phrase(new Chunk("Jornadas", f)));
            table.AddCell(cell);
            return table;
        }

        private PdfPTable comleteTable()
        {
            Font f = FontFactory.GetFont(FontFactory.TIMES, 12, Element.ALIGN_CENTER);
            PdfPTable table = beginTable();
            DateTime begin = this.begindate;
            PdfPCell cell = null;
            PdfPCell voidcell = new PdfPCell(new Phrase(new Chunk("\n\n\n\n", f)));
            int nj = 1;
            while (begin < this.finishdate)
            {
                if (timetable[begin.DayOfWeek])
                {
                    table.AddCell(voidcell);
                    cell = new PdfPCell(new Phrase(new Chunk("\n\n"+begin.ToShortDateString()+"\n\n",f)));
                    table.AddCell(cell);
                    table.AddCell(voidcell);
                    table.AddCell(new PdfPCell(new Phrase(new Chunk(nj.ToString(), f))));
                    nj++;

                }
                begin = begin.AddDays(1);
            }


            return table;
        }

        #endregion

        #region//Protected Methods

        protected override void insert()
        {
            adl.ExecuteNonQuery("INSERT INTO tbc (DNI, Judgement, Court, BeginDate, FinishDate, NumJourney ,Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday, Crime) VALUES (@DNI, @Judgement, @Court, @BeginDate, @FinishDate, @NumJourney, @Monday, @Tuesday, @Wednesday, @Thursday, @Friday, @Saturday, @Sunday, @Crime)", this.DNI, this.judgement, this.court, this.begindate, this.finishdate, this.numjourney, this.timetable[DayOfWeek.Monday], this.timetable[DayOfWeek.Tuesday], this.timetable[DayOfWeek.Wednesday], this.timetable[DayOfWeek.Thursday], this.timetable[DayOfWeek.Friday], this.timetable[DayOfWeek.Saturday], this.timetable[DayOfWeek.Sunday], (int)this.crime.Id[0]);
            this.id.Add((int)adl.Last()["Id"]);
        }

        protected override void update()
        {
            adl.ExecuteNonQuery("UPDATE tbc SET DNI = @DNI, Judgement = @Judgement, Court = @Court, BeginDate = @BeginDate, FinishDate = @FinishDate, NumJourney = @NumJourney, Monday = @Monday, Tuesday = @Tuesday, Wednesday = @Wednesday, Thursday = @Thursday, Friday = @Friday, Saturday = @Saturday, Sunday = @Sunday, Crime = @Crime WHERE Id = @Id", this.DNI, this.judgement, this.court, this.begindate, this.finishdate, this.numjourney, this.timetable[DayOfWeek.Monday], this.timetable[DayOfWeek.Tuesday], this.timetable[DayOfWeek.Wednesday], this.timetable[DayOfWeek.Thursday], this.timetable[DayOfWeek.Friday], this.timetable[DayOfWeek.Saturday], this.timetable[DayOfWeek.Sunday], (int)this.crime.Id[0],(int)this.id[0]);
        }

        protected override void delete()
        {
            adl.ExecuteNonQuery("DELETE FROM tbc WHERE Id = @Id", (int)this.id[0]);
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
                if (this.idtbc != 0)
                {
                    ret["Id"] = this.idtbc;
                }
                ret["DNI"] = this.DNI;
                ret["Judgement"] = this.judgement;
                ret["Court"] = this.court;
                ret["BeginDate"] = this.begindate;
                ret["FinishDate"] = this.finishdate;
                ret["NumJourney"] = this.numjourney;
                ret["Monday"] = this.timetable[DayOfWeek.Monday];
                ret["Tuesday"] = this.timetable[DayOfWeek.Tuesday];
                ret["Wednesday"] = this.timetable[DayOfWeek.Wednesday];
                ret["Thursday"] = this.timetable[DayOfWeek.Thursday];
                ret["Friday"] = this.timetable[DayOfWeek.Friday];
                ret["Saturday"] = this.timetable[DayOfWeek.Saturday];
                ret["Sunday"] = this.timetable[DayOfWeek.Sunday];
                ret["Crime"] = this.crime.Id[0];
                return ret;
            }
        }

        /// <summary>
        /// Asigna al objeto actual los datos contenientes en el DataRow.
        /// </summary>
        /// <param name="row">Fila con los datos.</param>
        internal override void FromRow(DataRow row)
        {
            base.FromRow(row);
            this.judgement = (string)row["Judgement"];
            this.idtbc = (int)row["Id"];
            this.court = (string)row["Court"];
            this.begindate = (DateTime)row["BeginDate"];
            this.finishdate = (DateTime)row["FinishDate"];
            this.numjourney = (int)row["NumJourney"];
            this.timetable[DayOfWeek.Monday] = (bool)row["Monday"];
            this.timetable[DayOfWeek.Tuesday] = (bool)row["Tuesday"];
            this.timetable[DayOfWeek.Wednesday] = (bool)row["Wednesday"];
            this.timetable[DayOfWeek.Thursday] = (bool)row["Thursday"];
            this.timetable[DayOfWeek.Friday] = (bool)row["Friday"];
            this.timetable[DayOfWeek.Saturday] = (bool)row["Saturday"];
            this.timetable[DayOfWeek.Sunday] = (bool)row["Sunday"];
            if (row["Crime"] != DBNull.Value)
            {
                List<object> ids = new List<object>();
                ids.Add((int)row["Crime"]);
                crime = (EBCrime)new EBCrime().Read(ids);
            }
            this.saved = true;
        }

        #endregion

        #region//Getters & Setters

        /// <summary>
        /// Obtiene y establece la ejecutoria.
        /// </summary>
        public string Judgement//TODO: comprobar que el formato de la ejecutoria a la hora de asignar.
        {
            get
            {
                return this.judgement;
            }
            set
            {
                this.judgement = value;
            }
        }

        /// <summary>
        /// Obtiene y establece el Juzgado.
        /// </summary>
        public string Court
        {
            get
            {
                return this.court;
            }
            set
            {
                this.court = value;
            }
        }

        /// <summary>
        /// Obtiene y establece la fecha de inicio.
        /// </summary>
        public DateTime BeginDate
        {
            get
            {
                return this.begindate;
            }
            set
            {
                this.begindate = value;
            }
        }

        /// <summary>
        /// Obtiene y establece la fecha de fin.
        /// </summary>
        public DateTime FinishDate
        {
            get
            {
                return this.finishdate;
            }
            set
            {
                this.finishdate = value;
            }
        }

        /// <summary>
        /// Obtiene y establece el número de jornadas que debe realizar.
        /// </summary>
        public int NumJourney
        {
            get
            {
                return this.numjourney;
            }
            set
            {
                this.numjourney = value;
            }
        }

        /// <summary>
        /// Obtiene y establece el horario del TBC.
        /// </summary>
        public Dictionary<DayOfWeek, bool> Timetable
        {
            get
            {
                return this.timetable;
            }
            set
            {
                this.timetable = value;
            }
        }

        /// <summary>
        /// Obtiene y establece el delito de TBC.
        /// </summary>
        public EBCrime Crime
        {
            get
            {
                return crime;
            }
            set
            {
                crime = value;
            }
        }

        #endregion

        #region//Public Methods

        /// <summary>
        /// Constructor de TBC
        /// </summary>
        /// <param name="DNI">DNI de la persona.</param>
        /// <param name="Court">Ejecutoria de TBC.</param>
        /// <param name="Name">Nombre de la persona.</param>
        /// <param name="Surname">Apellidos de la persona.</param>
        /// <param name="Court">Juzagod de TBC.</param>
        /// <param name="BeginDate">Fecha de inicio de cumplimiento.</param>
        /// <param name="FinishDate">Fecha final de cumplimiento.</param>
        public EBTBC(string DNI, string Judgement, string Name, string Surname, DateTime BirthDate, MyGender Gender ,string Court, DateTime BeginDate, DateTime FinishDate, EBCrime Crime)
            : base(DNI, Name, Surname, BirthDate, Gender)
        {
            initADL();
            this.judgement = Judgement;
            this.court = Court;
            this.begindate = BeginDate;
            this.finishdate = FinishDate;
            this.initDictionary();
            this.crime = Crime;
        }

        /// <summary>
        /// Constructor por defecto.
        /// </summary>
        public EBTBC()
            : base()
        {
            initADL();
            this.initDictionary();
        }

        /// <summary>
        /// Busca en la base de datos la persona TBC identificador.
        /// </summary>
        /// <param name="id">Identificador por el que se buscará la persona TBC</param>
        /// <returns>Persona TBC en formato AEN.</returns>
        public override AEB Read(List<object> id)
        {
            EBTBC ret = new EBTBC();
            DataRow row = tbcp.Select("SELECT * FROM tbcpeople WHERE ID = @Id",(int)id[0]).Rows[0];
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
        /// Obtiene todos los tbc de la base de datos.
        /// </summary>
        /// <returns>Lista de TBC en formato AEN.</returns>
        public override List<AEB> ReadAll()
        {
            List<AEB> ret = new List<AEB>();
            DataTable table = tbcp.SelectAll();
            foreach (DataRow rows in table.Rows)
            {
                EBTBC newtbc = new EBTBC();
                newtbc.FromRow(rows);
                ret.Add((EBTBC)newtbc);
            }
            return ret;
        }

        /// <summary>
        /// Guarda el TBC en la base de datos. Si es nuevo lo inserta, si ha sido modificado lo modifica en la base de datos.
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
            DataTable table = new VIEW(defaultConnString,"tbcpeople","Id").Select("SELECT * FROM tbcpeople WHERE DNI = @DNI",dni);
            foreach (DataRow row in table.Rows)
            {
                EBTBC newtbc = new EBTBC();
                newtbc.FromRow(row);
                ret.Add((AEBPerson)newtbc);
            }
            return ret;
        }

        /// <summary>
        /// Crea un documento pdf en ruta que conetiene el inicio de sentencia.
        /// </summary>
        /// <param name="file">Ruta del archivo pdf a crear, se debe incluir la extensión pdf.</param>
        public void BeginSentenceToPDF(string file)
        {
            Document document = new Document();
            PdfWriter.GetInstance(document, new FileStream(file, FileMode.OpenOrCreate));
            document.Open();
            Paragraph voidparagraph = new Paragraph();
            voidparagraph.Add("\n");
            document.Add(this.title());
            document.Add(voidparagraph);
            document.Add(this.head());            
            document.Add(this.beginBody());
            document.Add(this.foot());
            document.Close();
        }

        /// <summary>
        /// Crea un documento pdf en la ruta que contiene el fin de sentencia.
        /// </summary>
        /// <param name="file">Ruta del archivo pdf a crear, se debe incluir la extensión pdf.</param>
        public void FinishSentenceToPDF(string file)
        {
            Document document = new Document();
            PdfWriter.GetInstance(document, new FileStream(file, FileMode.OpenOrCreate));
            document.Open();
            Paragraph voidparagraph = new Paragraph();
            voidparagraph.Add("\n");
            document.Add(this.title());
            document.Add(voidparagraph);
            document.Add(this.head());
            document.Add(this.endBody());
            document.Add(this.foot());
            document.Close();
        }

        /// <summary>
        /// Crea un documento pdf en la ruta que contiene la hoja de firmas.
        /// </summary>
        /// <param name="file">Ruta del archivo pdf a crear, se debe incluir la extensión pdf.</param>
        public void SignaturesToPDF(string file)
        {
            Document document = new Document();
            PdfWriter.GetInstance(document, new FileStream(file, FileMode.OpenOrCreate));
            document.Open();
            Paragraph voidparagraph = new Paragraph();
            voidparagraph.Add("\n");
            document.Add(this.signatureTitle());
            document.Add(voidparagraph);
            document.Add(this.singarutreBody());
            document.Add(voidparagraph);
            document.Add(this.comleteTable());
            document.Close();
        }

        #endregion
    }
}
