CREATE TABLE IF NOT EXISTS PersonalDossier
(
	Id int PRIMARY KEY AUTO_INCREMENT,
	Income INT,
	Observations varchar(255)
);

CREATE TABLE IF NOT EXISTS People 
(
  Id int Primary Key AUTO_INCREMENT,
  DNI CHAR(9) Unique,
  Name VARCHAR(15) NULL,
  Surname VARCHAR(45) NULL,
  BirthDate DATE NULL,
  Gendre CHAR(1) NULL
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

INSERT INTO PersonalDossier(Income, Observations) VALUES
(
	1000,
	"Una observación"
);

INSERT INTO PersonalDossier(Income, Observations) VALUES
(
	500,
	"otra"
);

INSERT INTO People (DNI, Name, Surname, BirthDate, Gendre) VALUES
(
	'53705134L',
	'María',
	'Marcial',
	'1982/01/01',
	'F'
);

INSERT INTO People (DNI, Name, Surname, BirthDate, Gendre) VALUES
(
	'91071949E',
	'Jose',
	'Logroño',
	'1972/12/06',
	'M'
);

INSERT INTO People (DNI, Name,Surname,BirthDate, Gendre) VALUES
(
	'29556003Z',
	'Jenny',
	'Pecco',
	'2000/02/14',
	'F'
);

INSERT INTO Familiars(DNI, Dossier) VALUES
(
	'53705134L',
	1
);

INSERT INTO Familiars(DNI, Dossier) VALUES
(
	'91071949E',
	1
);

INSERT INTO Familiars(DNI, Dossier) VALUES
(
	'29556003Z',
	2
);



CREATE OR REPLACE VIEW FamiliarData as select Familiars.Id, People.DNI, Name, Surname, BirthDate, Gendre FROM People inner join Familiars on (People.DNI = Familiars.DNI);