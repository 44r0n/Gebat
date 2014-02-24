using System;
using System.Collections.Generic;
using System.Data;
using GebatCAD.Classes;
using GebatEN.Enums;

namespace GebatEN.Classes
{
    public abstract class AEBPerson : AEB
    {

        #region//Atributes

        private ADLPeople people;
        private string dni;
        private string surname;
        private string name;
        private DateTime birthDate;
        private MyGender gender;
        private List<string> phones = null;

        #endregion

        #region//Private Methods

        /// <summary> Tabla de asignación. </summary>
        private const string connection = "TRWAGMYFPDXBNJZSQVHLCKE";

        /// <summary> Genera la letra correspondiente a un DNI. </summary>
        /// <param name="dni"> DNI a procesar. </param>
        /// <returns> Letra correspondiente al DNI. </returns>
        private string NIFLetter(string dni)
        {
            int n;

            if ((dni == null) || (dni.Length != 9) || (!int.TryParse(dni.Substring(0, 8), out n)))
            {
                throw new ArgumentException("El DNI debe contener 8 dígitos.");
            }

            return connection[n % 23].ToString();
        }

        /// <summary> Genera la letra correspondiente a un NIE. </summary>
        /// <param name="nie"> NIE a procesar. </param>
        /// <returns> Letra correspondiente al NIE. </returns>
        private string NIELetter(string nie)
        {
            int n;
            if ((nie == null) || (nie.Length != 9) || ((char.ToUpper(nie[0]) != 'X') && (char.ToUpper(nie[0]) != 'Y') && (char.ToUpper(nie[0]) != 'Z')) || (!int.TryParse(nie.Substring(1, 7), out n)))
            {
                throw new ArgumentException("El NIE debe comenzar con la letra X, Y o Z seguida de 7 dígitos.");
            }

            switch (char.ToUpper(nie[0]))
            {
                case 'X':
                    return connection[n % 23].ToString();
                case 'Y':
                    return connection[(10000000 + n) % 23].ToString();
                case 'Z':
                    return connection[(20000000 + n) % 23].ToString();
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
            if(NIFLetter(dni) == dni.Substring(dni.Length -1,1))
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
            if (NIELetter(nie) == nie.Substring(nie.Length - 1, 1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Carga los telefonos de la persona actual.
        /// </summary>
        private void loadPhones()
        {
            ADLPhones adlphones = new ADLPhones(defaultConnString);
            phones = new List<string>();
            DataTable telfs = adlphones.SelectWhere("Owner = '" + this.dni + "'");
            foreach (DataRow row in telfs.Rows)
            {
                phones.Add((string)row["PhoneNumber"]);
            }
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
                if (people == null)
                {
                    people = new ADLPeople(defaultConnString);
                }
                DataRow ret = people.GetVoidRow;
                if (this.id != null)
                {
                    ret["Id"] = (int)this.id[0];
                }
                ret["DNI"] = (string)this.dni;
                ret["Name"] = this.name;
                ret["Surname"] = this.surname;
                ret["BirthDate"] = this.birthDate;
                if (this.gender == MyGender.Male)
                {
                    ret["Gender"] = "M";
                }
                else
                {
                    ret["Gender"] = "F";
                }
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
                DataRow perrow = people.SelectWhere("DNI = '" + row["DNI"] + "'").Rows[0];
                this.id.Add(perrow["Id"]);
                this.dni = (string)perrow["DNI"];
                this.name = (string)perrow["Name"];
                this.surname = (string)perrow["Surname"];
                this.birthDate = (DateTime)perrow["BirthDate"];
                switch ((string)perrow["Gender"])
                {
                    case "M":
                        this.gender = MyGender.Male;
                        break;
                    case "F":
                        this.gender = MyGender.Female;
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

        /// <summary>
        /// Comprueba si la instancia actual está guardada en la tabla personas.
        /// </summary>
        /// <returns>True si está guardada en la tabla, false en caso contrario.</returns>
        protected bool alreadyInPerson()
        {
            if (new ADLPeople(defaultConnString).SelectWhere("DNI = '" + this.DNI + "'").Rows.Count == 1)
            {
                return true;
            }
            return false;
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
                string first = value.Substring(0, 1);
                if (first == "X" || first == "Y" || first == "Z")
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
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        /// <summary>
        /// Obtiene y establece los apellidos.
        /// </summary>
        public string Surname
        {
            get
            {
                return this.surname;
            }
            set
            {
                this.surname = value;
            }
        }

        /// <summary>
        /// Obtiene la edad de la persona.
        /// </summary>
        public int Age
        {
            get
            {
                int age = DateTime.Now.Year - birthDate.Year;
                DateTime nowBirth = birthDate.AddYears(age);
                if (DateTime.Now.CompareTo(nowBirth) < 0)
                {
                    age--;
                }

                return age;
            }
        }

        /// <summary>
        /// Obtiene y establece el sexo de la persona.
        /// </summary>
        public MyGender Gender
        {
            get
            {
                return this.gender;
            }
            set
            {
                this.gender = value;
            }
        }

        /// <summary>
        /// Obtiene los telefonos de la persona.
        /// </summary>
        public List<string> Phones
        {
            get
            {
                if (this.phones == null)
                {
                    this.loadPhones();
                }
                return phones;
            }
        }

        #endregion

        #region//Public Methods

        /// <summary>
        /// Constructor con parámetros de Persona.
        /// </summary>
        /// <param name="DNI">DNI de la persona.</param>
        /// <param name="Name">Nombre de la persona.</param>
        /// <param name="Surname">Apellidos de la persona.</param>
        /// <param name="BirthDate">Fehca de nacimiento de la persona.</param>
        /// <param name="Gender">Genero de la persona.</param>
        public AEBPerson(string DNI, string Name, string Surname, DateTime BirthDate, MyGender Gender)
            :base()
        {
            people = new ADLPeople(defaultConnString);
            //this.id = new List<object>(); -> ni se te ocurra descomentar esta línea.
            this.DNI = DNI;
            this.name = Name;
            this.surname = Surname;
            this.birthDate = BirthDate;
            this.gender = Gender;
        }

        /// <summary>
        /// Constructor por defecto. No asigna ningún dato.
        /// </summary>
        public AEBPerson()
        {
            people = new ADLPeople(defaultConnString);
            this.id = new List<object>();
        }

        /// <summary>
        /// Carga los datos de una persona.
        /// </summary>
        /// <param name="dni">DNI por el que se buscará a la persona.</param>
        /// <returns>Lista de objetos AENPersona.</returns>
        public abstract List<AEBPerson> ReadByDNI(string dni);

        /// <summary>
        /// Añade un telefono de contacto.
        /// </summary>
        /// <param name="number">Nuevo numero de telefono.</param>
        public void AddPhone(string number)
        {
            ADLPhones adlphones = new ADLPhones(defaultConnString);
            DataRow newrow = adlphones.GetVoidRow;
            newrow["PhoneNumber"] = number;
            newrow["Owner"] = this.dni;
            adlphones.Insert(newrow);
            if (phones == null)
            {
                phones = new List<string>();
            }
            phones.Add(number);
        }

        /// <summary>
        /// Elimina el teléfono de contacto.
        /// </summary>
        /// <param name="number">Número de teléfono a eliminar.</param>
        public void DelPhone(string number)
        {
            ADLPhones adlphone = new ADLPhones(defaultConnString);
            DataTable del = adlphone.SelectWhere("PhoneNumber = " + number + " AND Owner = '" + this.dni+"'");
            if (del.Rows.Count == 1)
            {
                adlphone.Delete(del.Rows[0]);
                phones.Remove(number);
            }
        }

        #endregion
    }
}
