DROP TABLE IF EXISTS Telefonos;
DROP TABLE IF EXISTS TBC;
DROP TABLE IF EXISTS Delitos;
DROP TABLE IF EXISTS Personas;
DROP TABLE IF EXISTS Almacen;
DROP TABLE IF EXISTS OutgoingFood;
DROP TABLE IF EXISTS EntryFood;
DROP TABLE IF EXISTS Food;
DROP TABLE IF EXISTS Type;


CREATE TABLE IF NOT EXISTS Personas 
(
  Id int Primary Key AUTO_INCREMENT,
  DNI CHAR(9) Unique,
  Nombre VARCHAR(15) NULL,
  Apellidos VARCHAR(45) NULL,
  FechaNac DATE NULL,
  Sexo CHAR(1) NULL
);

CREATE TABLE IF NOT EXISTS Delitos
(
	Id int Primary Key AUTO_INCREMENT,
	Name VARCHAR(45) NOT NULL
);

CREATE TABLE IF NOT EXISTS TBC 
(
  Id int PRIMARY KEY AUTO_INCREMENT,
  DNI CHAR(9) NOT NULL,
  Ejecutoria VARCHAR(10) NOT NULL,
  Juzgado VARCHAR(45) NULL,
  FInicio DATE NULL,
  FFin DATE NULL,
  NumJornadas INT,
  Lunes BOOLEAN DEFAULT FALSE,
  Martes BOOLEAN DEFAULT FALSE,
  Miercoles BOOLEAN DEFAULT FALSE,
  Jueves BOOLEAN DEFAULT FALSE,
  Viernes BOOLEAN DEFAULT FALSE,
  Sabado BOOLEAN DEFAULT FALSE,
  Domingo BOOLEAN DEFAULT FALSE,
  Delito int NOT NULL,
  Unique (DNI, Ejecutoria),
  CONSTRAINT fk_TBC_Delitos FOREIGN KEY (Delito) REFERENCES Delitos (Id) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT fk_TBC_Personas FOREIGN KEY (DNI) REFERENCES Personas (DNI) ON DELETE NO ACTION ON UPDATE NO ACTION
  
);

CREATE TABLE IF NOT EXISTS Telefonos
(
	Id int PRIMARY KEY AUTO_INCREMENT,
	Numero CHAR(9) NOT NULL,
	DNI CHAR(9) NOT NULL,
	CONSTRAINT fk_Telefonos_Personas FOREIGN KEY (DNI) REFERENCES Personas(DNI) ON DELETE NO ACTION ON UPDATE NO ACTION
);

INSERT INTO Delitos (Name) VALUES
(
	'Robo'
);

INSERT INTO Personas (DNI, Nombre, Apellidos, FechaNac ,Sexo) VALUES
(
	'12345678A',
	'Pepe',
	'Olivares',
	'1976/04/02',
	'M'
);

INSERT INTO Personas (DNI, Nombre, Apellidos, FechaNac, Sexo) VALUES
(
	'23456789B',
	'Ana',
	'Entrepinares',
	'1988/07/11',
	'F'
);

INSERT INTO TBC (DNI, Ejecutoria, Juzgado, FInicio, FFin, NumJornadas,Lunes, Martes, Miercoles, Jueves, Viernes, Sabado, Domingo, Delito) VALUES
(
	'12345678A',
	'23/2013',
	'Alicante',
	'2012/11/24',
	'2013/03/09',
	180,
	TRUE,
	TRUE,
	TRUE,
	TRUE,
	TRUE,
	FALSE,
	FALSE,
	1
);

INSERT INTO Telefonos(Numero, DNI) VALUES
(
	'123456789',
	'12345678A'
);

INSERT INTO Telefonos (Numero, DNI) VALUES
(
	'234567890',
	'12345678A'
);

CREATE OR REPLACE VIEW TBCPeople as select TBC.Id, Personas.DNI, Nombre, Apellidos, FechaNac, Sexo ,Ejecutoria, Juzgado, FInicio, FFin, NumJornadas, Lunes, Martes, Miercoles, Jueves, Viernes,Sabado, Domingo, Delito from Personas inner join TBC on (Personas.DNI = TBC.DNI);