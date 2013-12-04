DROP TABLE IF EXISTS TBC;
DROP TABLE IF EXISTS Personas;
DROP TABLE IF EXISTS OutgoingFood;
DROP TABLE IF EXISTS EntryFood;
DROP TABLE IF EXISTS Food;
DROP TABLE IF EXISTS Type;

CREATE TABLE IF NOT EXISTS Personas 
(
  DNI CHAR(9) Primary Key,
  Nombre VARCHAR(15) NULL,
  Apellidos VARCHAR(45) NULL
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
  Unique (DNI, Ejecutoria),
  CONSTRAINT fk_TBC_Personas FOREIGN KEY (DNI) REFERENCES Personas (DNI) ON DELETE NO ACTION ON UPDATE NO ACTION
);

INSERT INTO Personas (DNI, Nombre, Apellidos) VALUES
(
	'12345678A',
	'Pepe',
	'Olivares'
);

INSERT INTO Personas (DNI, Nombre, Apellidos) VALUES
(
	'23456789B',
	'Ana',
	'Entrepinares'
);

INSERT INTO TBC (DNI, Ejecutoria, Juzgado, FInicio, FFin, NumJornadas,Lunes, Martes, Miercoles, Jueves, Viernes, Sabado, Domingo) VALUES
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
	FALSE
);

CREATE OR REPLACE VIEW TBCPeople as select TBC.Id, Personas.DNI, Nombre, Apellidos, Ejecutoria, Juzgado, FInicio, FFin, NumJornadas, Lunes, Martes, Miercoles, Jueves, Viernes,Sabado, Domingo from Personas inner join TBC on (Personas.DNI = TBC.DNI);