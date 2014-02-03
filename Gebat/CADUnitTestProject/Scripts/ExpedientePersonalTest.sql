CREATE TABLE IF NOT EXISTS ExpedientesPersonales
(
	Id int PRIMARY KEY AUTO_INCREMENT,
	Ingresos INT,
	Observaciones varchar(255)
);

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
	Expediente int,
	CONSTRAINT fk_Familiares_Personas FOREIGN KEY (DNI) REFERENCES Personas (DNI) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT fk_Familiares_Expediente FOREIGN KEY (Expediente) REFERENCES ExpedientesPersonales (Id) ON DELETE SET NULL ON UPDATE CASCADE
);

INSERT INTO ExpedientesPersonales(Ingresos, Observaciones) VALUES
(
	1000,
	"Una observación"
);

INSERT INTO ExpedientesPersonales(Ingresos, Observaciones) VALUES
(
	500,
	"otra"
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

INSERT INTO Familiares(DNI, Expediente) VALUES
(
	'53705134L',
	1
);

INSERT INTO Familiares(DNI, Expediente) VALUES
(
	'91071949E',
	1
);

INSERT INTO Familiares(DNI, Expediente) VALUES
(
	'29556003Z',
	2
);



CREATE OR REPLACE VIEW DatosFamiliares as select Familiares.Id, Personas.DNI, Nombre, Apellidos, FechaNac, Sexo FROM Personas inner join Familiares on (Personas.DNI = Familiares.DNI);