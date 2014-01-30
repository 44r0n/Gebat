DROP TABLE IF EXISTS Telefonos;
DROP TABLE IF EXISTS TBC;
DROP TABLE IF EXISTS Delitos;
DROP TABLE IF EXISTS Familiares;
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
  Apellidos VARCHAR(45) NULL,
  FechaNac DATE NULL,
  Sexo CHAR(1) NULL
);

CREATE TABLE IF NOT EXISTS Familiares
(
	Id int Primary Key AUTO_INCREMENT,
	DNI CHAR(9) NOT NULL,
	CONSTRAINT fk_Familiares_Personas FOREIGN KEY (DNI) REFERENCES Personas (DNI) ON DELETE CASCADE ON UPDATE CASCADE
);

INSERT INTO Personas (DNI, Nombre, Apellidos, FechaNac, Sexo) VALUES
(
	'53705134L',
	'María',
	'Marcial',
	'1982/01/01',
	'F'
);

INSERT INTO Personas (DNI, Nombre, Apellidos, FechaNac, Sexo) VALUES
(
	'91071949E',
	'Jose',
	'Logroño',
	'1972/12/06',
	'M'
);

INSERT INTO Personas (DNI, Nombre,Apellidos,FechaNac, Sexo) VALUES
(
	'29556003Z',
	'Jenny',
	'Pecco',
	'2000/02/14',
	'F'
);

INSERT INTO Familiares(DNI) VALUES
(
	'53705134L'
);

INSERT INTO Familiares(DNI) VALUES
(
	'91071949E'
);

INSERT INTO Familiares(DNI) VALUES
(
	'29556003Z'
);

CREATE OR REPLACE VIEW DatosFamiliares as select Familiares.Id, Personas.DNI, Nombre, Apellidos, FechaNac, Sexo FROM Personas inner join Familiares on (Personas.DNI = Familiares.DNI);