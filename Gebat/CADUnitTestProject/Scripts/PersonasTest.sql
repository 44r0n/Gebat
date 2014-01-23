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

INSERT INTO Personas (DNI, Nombre, Apellidos, FechaNac, Sexo) VALUES
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