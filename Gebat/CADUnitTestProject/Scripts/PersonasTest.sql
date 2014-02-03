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