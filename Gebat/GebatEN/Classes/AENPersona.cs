using System;
using System.Collections.Generic;
using System.Data;
using GebatCAD.Classes;
using GebatEN.Enums;

namespace GebatEN.Classes
{
    public abstract class AENPersona : AEN
    {

        #region//Atributes
        private CADPersonas personas;
        private string dni;
        private string apellidos;
        private string nombre;
        private DateTime fechanac;
        private sexo genero;

        #endregion

        #region//Private Methods

        /// <summary> Tabla de asignación. </summary>
        private const string Correspondencia = "TRWAGMYFPDXBNJZSQVHLCKE";

        /// <summary> Genera la letra correspondiente a un DNI. </summary>
        /// <param name="dni"> DNI a procesar. </param>
        /// <returns> Letra correspondiente al DNI. </returns>
        private string LetraNIF(string dni)
        {
            int n;

            if ((dni == null) || (dni.Length != 9) || (!int.TryParse(dni.Substring(0, 8), out n)))
            {
                throw new ArgumentException("El DNI debe contener 8 dígitos.");
            }

            return Correspondencia[n % 23].ToString();
        }

        /// <summary> Genera la letra correspondiente a un NIE. </summary>
        /// <param name="nie"> NIE a procesar. </param>
        /// <returns> Letra correspondiente al NIE. </returns>
        private string LetraNIE(string nie)
        {
            int n;
            if ((nie == null) || (nie.Length != 9) || ((char.ToUpper(nie[0]) != 'X') && (char.ToUpper(nie[0]) != 'Y') && (char.ToUpper(nie[0]) != 'Z')) || (!int.TryParse(nie.Substring(1, 7), out n)))
            {
                throw new ArgumentException("El NIE debe comenzar con la letra X, Y o Z seguida de 7 dígitos.");
            }

            switch (char.ToUpper(nie[0]))
            {
                case 'X':
                    return Correspondencia[n % 23].ToString();
                case 'Y':
                    return Correspondencia[(10000000 + n) % 23].ToString();
                case 'Z':
                    return Correspondencia[(20000000 + n) % 23].ToString();
                default:
                    return '\0'.ToString();
            }
        }

        /// <summary>
        /// Vefirica si el DNI introducido es correcto.
        /// </summary>
        /// <param name="dni">DNI a verificar.</param>
        /// <returns>True si el DNI es correcto, false en caso contrario.</returns>
        private bool verifyDNI(string dni)
        {
            if(LetraNIF(dni) == dni.Substring(dni.Length -1,1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Verifica si el NIE introducido es correcto.
        /// </summary>
        /// <param name="nie">NIE a verificar.</param>
        /// <returns>True si el NIE es correcto, false en caso contrario.</returns>
        private bool verifyNIE(string nie)
        {
            if (LetraNIE(nie) == nie.Substring(nie.Length - 1, 1))
            {
                return true;
            }
            else
            {
                return false;
            }
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
                if (personas == null)
                {
                    personas = new CADPersonas(defaultConnString);
                }
                DataRow ret = personas.GetVoidRow;
                if (this.id != null)
                {
                    ret["Id"] = (int)this.id[0];
                }
                ret["DNI"] = (string)this.dni;
                ret["Nombre"] = this.nombre;
                ret["Apellidos"] = this.apellidos;
                ret["FechaNac"] = this.fechanac;
                if (this.genero == sexo.Masculino)
                {
                    ret["Sexo"] = "M";
                }
                else
                {
                    ret["Sexo"] = "F";
                }
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
                DataRow perrow = personas.SelectWhere("DNI = '" + row["DNI"] + "'").Rows[0];
                this.id.Add(perrow["Id"]);
                this.dni = (string)perrow["DNI"];
                this.nombre = (string)perrow["Nombre"];
                this.apellidos = (string)perrow["Apellidos"];
                this.fechanac = (DateTime)perrow["FechaNac"];
                switch ((string)perrow["Sexo"])
                {
                    case "M":
                        this.genero = sexo.Masculino;
                        break;
                    case "F":
                        this.genero = sexo.Femenino;
                        break;
                    default:
                        throw new Exception("Genero desconocido");
                }
            }
            else
            {
                throw new NullReferenceException("Cannot convert from row, the row is null");
            }
        }

        #endregion

        #region//Getters & Setters

        /// <summary>
        /// Obtiene y establece el DNI.
        /// </summary>
        public string DNI
        {
            get
            {
                return this.dni;
            }
            set
            {
                string primero = value.Substring(0, 1);
                if (primero == "X" || primero == "Y" || primero == "Z")
                {
                    if (verifyNIE(value))
                    {
                        this.dni = value;
                    }
                    else
                    {
                        throw new ArgumentException("El formato NIE no es correcto");
                    }
                }
                else
                {
                    if (verifyDNI(value))
                    {
                        this.dni = value;
                    }
                    else
                    {
                        throw new ArgumentException("El formato DNI no es correcto");
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene y establece el nombre.
        /// </summary>
        public string Nombre
        {
            get
            {
                return this.nombre;
            }
            set
            {
                this.nombre = value;
            }
        }

        /// <summary>
        /// Obtiene y establece los apellidos.
        /// </summary>
        public string Apellidos
        {
            get
            {
                return this.apellidos;
            }
            set
            {
                this.apellidos = value;
            }
        }

        public int Edad
        {
            get
            {
                int edad = DateTime.Now.Year - fechanac.Year;
                DateTime nacimientoAhora = fechanac.AddYears(edad);
                if (DateTime.Now.CompareTo(nacimientoAhora) < 0)
                {
                    edad--;
                }

                return edad;
            }
        }

        public sexo Genero
        {
            get
            {
                return this.genero;
            }
            set
            {
                this.genero = value;
            }
        }

        #endregion

        #region//Public Methods

        /// <summary>
        /// Constructor con parámetros de Persona.
        /// </summary>
        /// <param name="DNI">DNI de la persona.</param>
        /// <param name="Nombre">Nombre de la persona.</param>
        /// <param name="Apellidos">Apellidos de la persona.</param>
        public AENPersona(string DNI, string Nombre, string Apellidos, DateTime FechaNac, sexo Genero)
            :base()
        {
            personas = new CADPersonas(defaultConnString);
            //this.id = new List<object>(); -> ni se te ocurra descomentar esta línea.
            this.DNI = DNI;
            this.nombre = Nombre;
            this.apellidos = Apellidos;
            this.fechanac = FechaNac;
            this.genero = Genero;
        }

        /// <summary>
        /// Constructor por defecto. No asigna ningún dato.
        /// </summary>
        public AENPersona()
        {
            personas = new CADPersonas(defaultConnString);
            this.id = new List<object>();
        }

        /// <summary>
        /// Carga los datos de una persona.
        /// </summary>
        /// <param name="dni">DNI por el que se buscará a la persona.</param>
        /// <returns>Lista de objetos AENPersona.</returns>
        public abstract List<AENPersona> ReadByDNI(string dni);

        #endregion
    }
}
