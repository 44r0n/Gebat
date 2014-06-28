﻿using System;
using System.Collections.Generic;
using System.Data;
using GebatCAD.Classes;
using GebatEN.Enums;

namespace GebatEN.Classes
{
    public abstract class AEBPerson : AEB
    {

        #region//Atributes

        private ADL people;
        private ADL adlphones;
        private string dni;
        private string surname;
        private string name;
        private DateTime birthDate;
        private MyGender gender;
        private List<string> phones = null;       

        #endregion

        #region//Private Methods

        private void initADL()
        {
            people = new ADL(defaultConnString, "people", "Id");
            adlphones = new ADL(defaultConnString, "phones", "Id");
        }

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
            phones = new List<string>();
            DataTable telfs = adlphones.Select("SELECT * FROM phones WHERE Owner = @Owner", this.dni);
            foreach (DataRow row in telfs.Rows)
            {
                phones.Add((string)row["PhoneNumber"]);
            }
        }

        #endregion

        #region//Protected Methods

        protected override void insert()
        {
            
            if(this.gender == MyGender.Male)
            {
                adl.ExecuteNonQuery("INSERT INTO people (DNI, Name, Surname, BirthDate, Gender) VALUES (@DNI, @Name, @Surname, @BirthDate, @Gender)", GetCipher.Encrypt(this.dni), GetCipher.Encrypt(this.name), GetCipher.Encrypt(this.surname), GetCipher.Encrypt(this.birthDate.ToShortDateString()), GetCipher.Encrypt("M"));
            }
            else
            {
                adl.ExecuteNonQuery("INSERT INTO people (DNI, Name, Surname, BirthDate, Gender) VALUES (@DNI, @Name, @Surname, @BirthDate, @Gender)", GetCipher.Encrypt(this.dni), GetCipher.Encrypt(this.name), GetCipher.Encrypt(this.surname), GetCipher.Encrypt(this.birthDate.ToShortDateString()), GetCipher.Encrypt("F"));
            }
            this.id.Add((int) new ADL(defaultConnString,"people","Id").Last()["Id"]);
        }

        protected override void update()
        {
            if (this.gender == MyGender.Male)
            {
                adl.ExecuteNonQuery("UPDATE people SET DNI = @DNI, Name = @Name, Surname = @Surname, BirthDate = @BirthDate, Gender = @Gender WHERE Id = @Id", GetCipher.Encrypt(this.dni), GetCipher.Encrypt(this.name), GetCipher.Encrypt(this.surname), GetCipher.Encrypt(this.birthDate.ToShortDateString()), GetCipher.Encrypt("M"), this.id[0]);
            }
            else
            {
                adl.ExecuteNonQuery("UPDATE people SET DNI = @DNI, Name = @Name, Surname = @Surname, BirthDate = @BirthDate, Gender = @Gender WHERE Id = @Id", GetCipher.Encrypt(this.dni), GetCipher.Encrypt(this.name), GetCipher.Encrypt(this.surname), GetCipher.Encrypt(this.birthDate.ToShortDateString()), GetCipher.Encrypt("F"), this.id[0]);
            }
        }

        protected override void delete()
        {
            adl.ExecuteNonQuery("DELETE FROM people WHERE Id = @Id", this.id[0]);
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
                DataRow ret = people.GetVoidRow;
                if (this.id != null)
                {
                    ret["Id"] = this.id[0];
                }
                ret["DNI"] = GetCipher.Encrypt((string)this.dni);
                ret["Name"] = GetCipher.Encrypt(this.name);
                ret["Surname"] = GetCipher.Encrypt(this.surname);
                ret["BirthDate"] = GetCipher.Encrypt(this.birthDate.ToShortDateString());
                if (this.gender == MyGender.Male)
                {
                    ret["Gender"] = GetCipher.Encrypt("M");
                }
                else
                {
                    ret["Gender"] = GetCipher.Encrypt("F");
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
                DataRow perrow = people.Select("SELECT * FROM people WHERE DNI = @DNI", row["DNI"]).Rows[0];
                this.id.Add((int)perrow["Id"]);
                this.dni = GetCipher.Decrypt((string)perrow["DNI"]);
                this.name = GetCipher.Decrypt((string)perrow["Name"]);
                this.surname = GetCipher.Decrypt((string)perrow["Surname"]);
                this.birthDate = Convert.ToDateTime(GetCipher.Decrypt((string)perrow["BirthDate"]));
                switch (GetCipher.Decrypt((string)perrow["Gender"]))
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
            if (new ADL(defaultConnString,"people","Id").Select("SELECT * FROM people WHERE DNI = @DNI", GetCipher.Encrypt(this.DNI)).Rows.Count == 1)
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
            initADL();
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
            :base()
        {
            initADL();
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
            adlphones.ExecuteNonQuery("INSERT INTO phones (PhoneNumber, Owner) VALUES (@PhoneNumber, @Owner)", GetCipher.Encrypt(number), this.id[0]);
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
            DataTable del = adlphones.Select("SELECT * FROM phones WHERE PhoneNumber = @PhoneNumber AND Owner = @Owner", GetCipher.Encrypt(number),this.id[0]);
            if (del.Rows.Count == 1)
            {
                adlphones.ExecuteNonQuery("DELETE FROM phones WHERE Id = @Id",(int)del.Rows[0]["Id"]);
                phones.Remove(number);
            }
        }

        #endregion
    }
}
