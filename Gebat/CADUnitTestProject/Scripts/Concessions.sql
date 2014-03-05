CREATE TABLE IF NOT EXISTS People
(
  Id int Primary Key AUTO_INCREMENT,
  DNI CHAR(9) Unique,
  Name VARCHAR(15) NULL,
  Surname VARCHAR(45) NULL,
  BirthDate DATE NULL,
  Gender CHAR(1) NULL
);

CREATE TABLE IF NOT EXISTS PersonalDossier
(
	Id int PRIMARY KEY AUTO_INCREMENT,
	Observations varchar(255)
);

CREATE TABLE IF NOT EXISTS Familiars
(
	Id int Primary Key AUTO_INCREMENT,
	DNI CHAR(9) Unique,
	Dossier int,
	Income INT,
	CONSTRAINT fk_Familiars_People FOREIGN KEY (DNI) REFERENCES People (DNI) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT fk_Familiars_Dossier FOREIGN KEY (Dossier) REFERENCES PersonalDossier (Id) ON DELETE SET NULL ON UPDATE CASCADE
);

CREATE TABLE IF NOT EXISTS Concessions
(
	Id int Primary Key AUTO_INCREMENT,
	Dossier INT,
	BeginDate DATE NULL,
	FinishDate DATE NULL,
	Notes VARCHAR(150),
	CONSTRAINT fk_Concessions_Dossier FOREIGN KEY (Dossier) REFERENCES PersonalDossier (Id) ON DELETE SET NULL ON UPDATE CASCADE
);

INSERT INTO PersonalDossier(Observations) VALUES
(
	"Una observación"
);

INSERT INTO PersonalDossier(Observations) VALUES
(
	"otra"
);

INSERT INTO People (DNI, Name, Surname, BirthDate, Gender) VALUES
(
	'53705134L',
	'María',
	'Marcial',
	'1982/01/01',
	'F'
);

INSERT INTO People (DNI, Name, Surname, BirthDate, Gender) VALUES
(
	'91071949E',
	'Jose',
	'Logroño',
	'1972/12/06',
	'M'
);

INSERT INTO People (DNI, Name,Surname,BirthDate, Gender) VALUES
(
	'29556003Z',
	'Jenny',
	'Pecco',
	'2000/02/14',
	'F'
);

INSERT INTO Familiars(DNI, Dossier, Income) VALUES
(
	'53705134L',
	1,
	750
);

INSERT INTO Familiars(DNI, Dossier, Income) VALUES
(
	'91071949E',
	1,
	250
);

INSERT INTO Familiars(DNI, Dossier, Income) VALUES
(
	'29556003Z',
	2,
	500
);

INSERT INTO Concessions (Dossier, BeginDate, FinishDate, Notes) VALUES
(
	1,
	'2014/02/24',
	'2014/08/24',
	'Algo'
);

INSERT INTO Concessions (Dossier, BeginDate, FinishDate, Notes) VALUES
(
	1,
	'2014/08/25',
	'2014/10/22',
	'Otro'
);

INSERT INTO Concessions (Dossier, BeginDate, FinishDate, Notes) VALUES
(
	2,
	'2014/01/02',
	'2014/07/02',
	'Tercero'
);