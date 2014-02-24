CREATE TABLE IF NOT EXISTS People 
(
  Id int Primary Key AUTO_INCREMENT,
  DNI CHAR(9) Unique,
  Name VARCHAR(15) NULL,
  Surname VARCHAR(45) NULL,
  BirthDate DATE NULL,
  Gender CHAR(1) NULL
);

INSERT INTO People (DNI, Name, Surname, BirthDate, Gender) VALUES
(
	'12345678A',
	'Pepe',
	'Olivares',
	'1976/04/02',
	'M'
);

INSERT INTO People (DNI, Name, Surname, BirthDate, Gender) VALUES
(
	'23456789B',
	'Ana',
	'Entrepinares',
	'1988/07/11',
	'F'
);