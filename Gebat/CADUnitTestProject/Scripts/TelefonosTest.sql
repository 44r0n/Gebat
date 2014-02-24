CREATE TABLE IF NOT EXISTS People 
(
  Id int Primary Key AUTO_INCREMENT,
  DNI CHAR(9) Unique,
  Name VARCHAR(15) NULL,
  Surname VARCHAR(45) NULL,
  BirthDate DATE NULL,
  Gender CHAR(1) NULL
);

CREATE TABLE IF NOT EXISTS Crimes
(
	Id int Primary Key AUTO_INCREMENT,
	Name VARCHAR(45) NOT NULL
);

CREATE TABLE IF NOT EXISTS TBC 
(
  Id int PRIMARY KEY AUTO_INCREMENT,
  DNI CHAR(9) NOT NULL,
  Judgement VARCHAR(10) NOT NULL,
  Court VARCHAR(45) NULL,
  BeginDate DATE NULL,
  FinishDate DATE NULL,
  NumJourney INT,
  Monday BOOLEAN DEFAULT FALSE,
  Tuesday BOOLEAN DEFAULT FALSE,
  Wednesday BOOLEAN DEFAULT FALSE,
  Thursday BOOLEAN DEFAULT FALSE,
  Friday BOOLEAN DEFAULT FALSE,
  Saturday BOOLEAN DEFAULT FALSE,
  Sunday BOOLEAN DEFAULT FALSE,
  Crime int NOT NULL,
  Unique (DNI, Judgement),
  CONSTRAINT fk_TBC_Crimes FOREIGN KEY (Crime) REFERENCES Crimes (Id) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT fk_TBC_People FOREIGN KEY (DNI) REFERENCES People (DNI) ON DELETE NO ACTION ON UPDATE NO ACTION
  
);

CREATE TABLE IF NOT EXISTS Phones
(
	Id int PRIMARY KEY AUTO_INCREMENT,
	PhoneNumber CHAR(9) NOT NULL,
	Owner CHAR(9) NOT NULL,
	CONSTRAINT fk_Phones_People FOREIGN KEY (Owner) REFERENCES People(DNI) ON DELETE NO ACTION ON UPDATE NO ACTION
);

INSERT INTO Crimes (Name) VALUES
(
	'Robo'
);

INSERT INTO People (DNI, Name, Surname, BirthDate ,Gender) VALUES
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

INSERT INTO TBC (DNI, Judgement, Court, BeginDate, FinishDate, NumJourney,Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday, Crime) VALUES
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

INSERT INTO Phones(PhoneNumber, Owner) VALUES
(
	'123456789',
	'12345678A'
);

INSERT INTO Phones (PhoneNumber, Owner) VALUES
(
	'234567890',
	'12345678A'
);

CREATE OR REPLACE VIEW TBCPeople as select TBC.Id, People.DNI, Name, Surname, BirthDate, Gender ,Judgement, Court, BeginDate, FinishDate, NumJourney, Monday, Tuesday, Wednesday, Thursday, Friday,Saturday, Sunday, Crime from People inner join TBC on (People.DNI = TBC.DNI);