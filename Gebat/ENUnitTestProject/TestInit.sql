DROP TABLE IF EXISTS Phones;
DROP TABLE IF EXISTS TBC;
DROP TABLE IF EXISTS Crimes;
DROP TABLE IF EXISTS Fresco;
DROP TABLE IF EXISTS Concessions;
DROP TABLE IF EXISTS Familiars;
DROP TABLE IF EXISTS People;
DROP TABLE IF EXISTS PersonalDossier;
DROP TABLE IF EXISTS OutgoingFood;
DROP TABLE IF EXISTS EntryFood;
DROP TABLE IF EXISTS Food;
DROP TABLE IF EXISTS Type;

CREATE TABLE IF NOT EXISTS Type
(
	Id int Primary Key AUTO_INCREMENT,
	Name varchar(10) not null
);

CREATE TABLE IF NOT EXISTS Food
(
	Id int Primary Key AUTO_INCREMENT,
	Name varchar(20) not null,
	QuantityType int,
	Quantity int DEFAULT 0,
	CONSTRAINT fk_Food_Type FOREIGN KEY (QuantityType) REFERENCES Type (Id) ON UPDATE SET NULL ON DELETE SET NULL
);

CREATE TABLE IF NOT EXISTS EntryFood
(
	Id int Primary Key AUTO_INCREMENT,
	FoodType int,
	QuantityIn int,
	DateTime date,
	CONSTRAINT fk_EntryFood_Food FOREIGN KEY (FoodType) REFERENCES Food (Id) ON UPDATE SET NULL ON DELETE SET NULL
);

CREATE TABLE IF NOT EXISTS OutgoingFood
(
	Id int Primary Key AUTO_INCREMENT,
	FoodType int,
	QuantityOut int,
	DateTime date,
	CONSTRAINT fk_OutgoingFood_Food FOREIGN KEY (FoodType) REFERENCES Food (Id) ON UPDATE SET NULL ON DELETE SET NULL
);


CREATE TRIGGER addfood AFTER INSERT ON EntryFood
FOR EACH ROW
BEGIN
UPDATE Food SET Quantity = Quantity + NEW.QuantityIn WHERE (Id = NEW.FoodType);
END;

CREATE TRIGGER subfood AFTER INSERT ON OutGoingFood
FOR EACH ROW
BEGIN
UPDATE Food SET Quantity = Quantity - NEW.QuantityOut WHERE (Id = NEW.FoodType);
END;

CREATE TRIGGER subfoodin BEFORE DELETE ON EntryFood
FOR EACH ROW
BEGIN
UPDATE Food SET Quantity = Quantity - OLD.QuantityIN WHERE Id = OLD.FoodType;
END;

CREATE TRIGGER addfoodout BEFORE DELETE ON OutGoingFood
FOR EACH ROW
BEGIN
UPDATE Food SET Quantity = Quantity + OLD.QuantityOut WHERE Id = OLD.FoodType;
END;

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

CREATE TABLE IF NOT EXISTS Fresco
(
	Concession Int Unique,
	CONSTRAINT fk_Fresco_Concessions FOREIGN KEY (Concession) REFERENCES Concessions (Id) ON DELETE SET NULL ON UPDATE CASCADE
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
  CONSTRAINT fk_TBC_People FOREIGN KEY (DNI) REFERENCES People (DNI) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE IF NOT EXISTS Phones
(
	Id int PRIMARY KEY AUTO_INCREMENT,
	PhoneNumber CHAR(9) NOT NULL,
	Owner CHAR(9) NOT NULL,
	CONSTRAINT fk_Phones_People FOREIGN KEY (Owner) REFERENCES People(DNI) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE OR REPLACE VIEW TBCPeople as select TBC.Id, People.DNI, Name, Surname, BirthDate, Gender ,Judgement, Court, BeginDate, FinishDate, NumJourney, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday, Crime from People inner join TBC on (People.DNI = TBC.DNI);

CREATE OR REPLACE VIEW FamiliarData as select Familiars.Id, People.DNI, Name, Surname, BirthDate, Gender, Dossier, Income FROM People inner join Familiars on (People.DNI = Familiars.DNI);

CREATE OR REPLACE VIEW FrescoData as select Concessions.Id, Dossier, BeginDate, FinishDate, Notes FROM Concessions inner join Fresco on (Concessions.Id = Fresco.Concession);

INSERT INTO Type (Name) VALUES
(
	'Kg'
);

INSERT INTO Type (Name) VALUES
(
    'Litros'
);

INSERT INTO Type (Name) VALUES
(
    'Borrar'
);

INSERT INTO Type (Name) VALUES
(
    'Paquetes'
);

DELETE FROM Type WHERE Id = 3;

INSERT INTO Food (Name,QuantityType) VALUES
(
    'Patates',
    1
);

INSERT INTO Food (Name,QuantityType) VALUES
(
    'Tomates',
    1
);

INSERT INTO Food (Name) VALUES
(
    'Pa borrar'
);

INSERT INTO Food (Name,QuantityType) VALUES
(
    'Pomes',
    1
);

DELETE FROM Food WHERE Id = 3;

INSERT INTO EntryFood(FoodType,QuantityIn, DateTime) VALUES
(
	1,
	1,
	'2012/11/20'
);

INSERT INTO EntryFood(FoodType,QuantityIn, DateTime) VALUES
(
	1,
	2,
	'2012/11/21'
);

INSERT INTO EntryFood(FoodType,QuantityIn,DateTime) VALUES
(
	4,
	4,
	'2012/11/22'
);

INSERT INTO EntryFood(FoodType, QuantityIn, DateTime) VALUES
(
	1,
	3,
	'2012/11/23'
);

INSERT INTO OutgoingFood(FoodType,QuantityOut, DateTime) VALUES
(
	1,
	1,
	'2012/11/24'
);

INSERT INTO OutgoingFood(FoodType,QuantityOut,DateTime) VALUES
(
	1,
	1,
	'2012/11/24'
);

INSERT INTO OutgoingFood(FoodType,QuantityOut, DateTime) VALUES
(
	4,
	2,
	'2012/11/25'
);

INSERT INTO Crimes (Name) VALUES
(
	"Robo"
);

INSERT INTO Crimes (Name) VALUES
(
	"Pelea"
);

INSERT INTO Crimes (Name) VALUES
(
	"Otro"
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
	'54508005Y',
	'Pepe',
	'Olivares',
	'1976/04/02',
	'M'
);

INSERT INTO People (DNI, Name, Surname, BirthDate, Gender) VALUES
(
	'01086932K',
	'Ana',
	'Entrepinares',
	'1988/07/11',
	'F'
);

INSERT INTO People (DNI, Name, Surname, BirthDate, Gender) VALUES
(
	'12218401L',
	'Isabel',
	'Gonzalez',
	'1978/11/17',
	'F'
);

INSERT INTO People (DNI, Name, Surname, BirthDate, Gender) VALUES
(
	'93909231R',
	'Yerai',
	'Gallardo',
	'1993/12/21',
	'M'
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

INSERT INTO TBC (DNI, Judgement, Court, BeginDate, FinishDate, NumJourney,Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday,Crime) VALUES
(
	'54508005Y',
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

INSERT INTO TBC (DNI, Judgement, Court, BeginDate, FinishDate, NumJourney,Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday,Crime) VALUES
(
	'01086932K',
	'1/98',
	'Juzgado Valencia',
	'2013/07/20',
	'2014/03/10',
	250,
	TRUE,
	TRUE,
	TRUE,
	TRUE,
	TRUE,
	FALSE,
	FALSE,
	2
);

INSERT INTO TBC (DNI, Judgement, Court, BeginDate, FinishDate, NumJourney,Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday,Crime) VALUES
(
	'01086932K',
	'93/2012',
	'Court Valencia',
	'2014/05/20',
	'2014/10/10',
	250,
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
	'01086932K'
);

INSERT INTO Phones (PhoneNumber, Owner) VALUES
(
	'234567890',
	'01086932K'
);

INSERT INTO Familiars(DNI, Dossier,Income) VALUES
(
	'53705134L',
	1,
	500
);

INSERT INTO Familiars(DNI, Dossier, Income) VALUES
(
	'91071949E',
	1,
	500
);

INSERT INTO Familiars(DNI, Dossier,Income) VALUES
(
	'29556003Z',
	2,
	400
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

INSERT INTO Concessions (Dossier, BeginDate, Notes) VALUES
(
	2,
	'2014/07/03',
	'NullFinishDateConcession'
);

INSERT INTO Fresco (Concession) VALUES
(
	1
);

INSERT INTO Fresco (Concession) VALUES
(
	2
);