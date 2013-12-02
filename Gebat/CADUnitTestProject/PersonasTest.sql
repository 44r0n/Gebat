DROP TABLE IF EXISTS TBC;
DROP TABLE IF EXISTS Personas;
DROP TABLE IF EXISTS OutgoingFood;
DROP TABLE IF EXISTS EntryFood;
DROP TABLE IF EXISTS Food;
DROP TABLE IF EXISTS Type;

CREATE TABLE IF NOT EXISTS Personas 
(
  Id int Primary Key AUTO_INCREMENT,
  DNI CHAR(9) Unique,
  Nombre VARCHAR(15) NULL,
  Apellidos VARCHAR(45) NULL
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