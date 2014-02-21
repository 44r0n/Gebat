﻿using System;
using System.Collections.Generic;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using GebatCAD.Classes;
using GebatEN.Enums;

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
        private ENDelito delito;

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

        private Paragraph titulo()
        {
            Paragraph ret = new Paragraph();
            ret.Alignment = Element.ALIGN_CENTER;
            ret.Font = FontFactory.GetFont(FontFactory.TIMES_BOLD, 16);
            ret.Add("COMUNICACIÓN DE CUMPLIMIENTO DE TRABAJO A FAVOR DE LA COMUNIDAD\n");
            return ret;
        }

        private Paragraph cabecera()
        {
            Paragraph ret = new Paragraph();
            ret.Alignment = Element.ALIGN_JUSTIFIED;
            ret.Font = FontFactory.GetFont(FontFactory.TIMES, 12);
            ret.Add("DE ENTIDAD: CONSIGNAS SOLIDARIAS\n\n");//TODO: obtener el nombre de consignas solidarias desde el archivo de configuración.
            ret.Add("AL SERIVICO SOCIAL PENITENCIARIO DE "+this.juzgado+"\n\n\n");
            return ret;
        }

        private Paragraph cuerpo()
        {
            Paragraph ret = new Paragraph();
            ret.Alignment = Element.ALIGN_JUSTIFIED;
            ret.Font = FontFactory.GetFont(FontFactory.TIMES, 12);
            ret.Add("Por la presente los comunicamos que "+this.Nombre + " " + this.Apellidos +" con DNI "+ this.DNI +" con respecto al cumplimiento de trabajo en beneficio a la comunidad, en Ejec. "+ this.ejecutoria +" del Juzgado J.P. "+ this.juzgado +" se ha producido la siguiente situación:\n\n");
            return ret;
        }

        private Paragraph cuerpoInicio()
        {
            Paragraph ret = cuerpo();
            ret.Add("                X Iniciación de cumplimiento Fecha: "+ this.finicio.ToShortDateString()+ "\n                Horario: ");
            if (this.horario[DayOfWeek.Monday])
            {
                ret.Add("Lunes ");
            }
            if (this.horario[DayOfWeek.Tuesday])
            {
                ret.Add("Martes ");
            }
            if (this.horario[DayOfWeek.Wednesday])
            {
                ret.Add("Miércoles ");
            }
            if (this.horario[DayOfWeek.Thursday])
            {
                ret.Add("Jueves ");
            }
            if (this.horario[DayOfWeek.Friday])
            {
                ret.Add("Viernes ");
            }
            if (this.horario[DayOfWeek.Saturday])
            {
                ret.Add("Sábado ");
            }
            if (this.horario[DayOfWeek.Sunday])
            {
                ret.Add("Domingo");
            }

            ret.Add("\n\n");
            return ret;
        }

        private Paragraph pie()
        {
            Paragraph ret = new Paragraph();
            ret.Alignment = Element.ALIGN_RIGHT;
            ret.Font = FontFactory.GetFont(FontFactory.TIMES, 12);
            ret.Add("En Elda a "+ this.ffin.ToShortDateString() + "                        \n");
            ret.Add("Por la entidad.                        \n\n\n\n");
            ret.Add("Firmado:                       ");
            return ret;
        }

        private Paragraph cuerpoFin()
        {
            Paragraph ret = cuerpo();
            ret.Add("               X Finalización de cumplimiento: " + this.ffin.ToShortDateString() + "\n                        Total de Jornadas cumplidas: "+this.numjornadas);
            ret.Add("\n\n");
            return ret;
        }

        private Paragraph tituloFirmas()
        {
            Paragraph ret = new Paragraph();
            ret.Alignment = Element.ALIGN_JUSTIFIED;
            ret.Font = FontFactory.GetFont(FontFactory.TIMES_BOLD, 16);
            ret.Add("REGISTRO DE PRESENTACIONES\n");
            return ret;
        }

        private Paragraph cuerpoFirmas()
        {
            Paragraph cuerpo = new Paragraph();
            cuerpo.Alignment = Element.ALIGN_JUSTIFIED;
            cuerpo.Font = FontFactory.GetFont(FontFactory.TIMES, 12);
            cuerpo.Add("Nombre y apellidos: " + this.Nombre + " " + this.Apellidos + "\nDNI: " + this.DNI + " Ejecutoria: " + this.ejecutoria + " Juzgado: "+ this.juzgado +"\n");
            return cuerpo;
        }

        private PdfPTable tablaIniciada()
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

        private PdfPTable tablaCompleta()
        {
            Font f = FontFactory.GetFont(FontFactory.TIMES, 12, Element.ALIGN_CENTER);
            PdfPTable table = tablaIniciada();
            DateTime inicio = this.finicio;
            PdfPCell cell = null;
            PdfPCell voidcell = new PdfPCell(new Phrase(new Chunk("\n\n\n\n", f)));
            int nj = 1;
            while (inicio < this.ffin)
            {
                if (horario[inicio.DayOfWeek])
                {
                    table.AddCell(voidcell);
                    cell = new PdfPCell(new Phrase(new Chunk("\n\n"+inicio.ToShortDateString()+"\n\n",f)));
                    table.AddCell(cell);
                    table.AddCell(voidcell);
                    table.AddCell(new PdfPCell(new Phrase(new Chunk(nj.ToString(), f))));
                    nj++;

                }
                inicio = inicio.AddDays(1);
            }


            return table;
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
                ret["Delito"] = this.delito.Id[0];
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
            if (row["Delito"] != DBNull.Value)
            {
                List<int> ids = new List<int>();
                ids.Add((int)row["Delito"]);
                delito = (ENDelito)new ENDelito().Read(ids);
            }
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

        /// <summary>
        /// Obtiene y establece el delito de TBC.
        /// </summary>
        public ENDelito Delito
        {
            get
            {
                return delito;
            }
            set
            {
                delito = value;
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
        public ENTBC(string DNI, string Ejecutoria, string Nombre, string Apellidos, DateTime FechaNac, sexo Genero ,string Juzgado, DateTime Finicio, DateTime Ffin, ENDelito delito)
            : base(DNI, Nombre, Apellidos, FechaNac, Genero)
        {
            cad = new ADLTBC(defaultConnString);
            this.ejecutoria = Ejecutoria;
            this.juzgado = Juzgado;
            this.finicio = Finicio;
            this.ffin = Ffin;
            this.initDictionary();
            this.delito = delito;
        }

        /// <summary>
        /// Constructor por defecto.
        /// </summary>
        public ENTBC()
            : base()
        {
            cad = new ADLTBC(defaultConnString);
            this.initDictionary();
        }

        /// <summary>
        /// Busca en la base de datos la persona TBC identificador.
        /// </summary>
        /// <param name="id">Identificador por el que se buscará la persona TBC</param>
        /// <returns>Persona TBC en formato AEN.</returns>
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
        /// Obtiene todos los tbc de la base de datos.
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
            ADLPeople per = new ADLPeople(defaultConnString);
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

        /// <summary>
        /// Crea un documento pdf en ruta que conetiene el inicio de sentencia.
        /// </summary>
        /// <param name="ruta">Ruta del archivo pdf a crear, se debe incluir la extensión pdf.</param>
        public void InicioSentenciaToPDF(string ruta)
        {
            Document document = new Document();
            PdfWriter.GetInstance(document, new FileStream(ruta, FileMode.OpenOrCreate));
            document.Open();
            Paragraph voidparagraph = new Paragraph();
            voidparagraph.Add("\n");
            document.Add(this.titulo());
            document.Add(voidparagraph);
            document.Add(this.cabecera());            
            document.Add(this.cuerpoInicio());
            document.Add(this.pie());
            document.Close();
        }

        /// <summary>
        /// Crea un documento pdf en la ruta que contiene el fin de sentencia.
        /// </summary>
        /// <param name="ruta">Ruta del archivo pdf a crear, se debe incluir la extensión pdf.</param>
        public void FinSentenciaToPDF(string ruta)
        {
            Document document = new Document();
            PdfWriter.GetInstance(document, new FileStream(ruta, FileMode.OpenOrCreate));
            document.Open();
            Paragraph voidparagraph = new Paragraph();
            voidparagraph.Add("\n");
            document.Add(this.titulo());
            document.Add(voidparagraph);
            document.Add(this.cabecera());
            document.Add(this.cuerpoFin());
            document.Add(this.pie());
            document.Close();
        }

        /// <summary>
        /// Crea un documento pdf en la ruta que contiene la hoja de firmas.
        /// </summary>
        /// <param name="ruta">Ruta del archivo pdf a crear, se debe incluir la extensión pdf.</param>
        public void FirmasToPDF(string ruta)
        {
            Document document = new Document();
            PdfWriter.GetInstance(document, new FileStream(ruta, FileMode.OpenOrCreate));
            document.Open();
            Paragraph voidparagraph = new Paragraph();
            voidparagraph.Add("\n");
            document.Add(this.tituloFirmas());
            document.Add(voidparagraph);
            document.Add(this.cuerpoFirmas());
            document.Add(voidparagraph);
            document.Add(this.tablaCompleta());
            document.Close();
        }

        #endregion
    }
}
