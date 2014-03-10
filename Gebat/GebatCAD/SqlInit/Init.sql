DROP TABLE IF EXISTS Phones;
DROP TABLE IF EXISTS TBC;
DROP TABLE IF EXISTS Crimes;
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

DELIMITER |
CREATE TRIGGER addfood AFTER INSERT ON EntryFood
FOR EACH ROW 
BEGIN
	UPDATE Food SET Quantity = Quantity + NEW.QuantityIn WHERE (Id = NEW.FoodType);
END
|
DELIMITER ;

DELIMITER |
CREATE TRIGGER subfood AFTER INSERT ON OutGoingFood
FOR EACH ROW 
BEGIN
	UPDATE Food SET Quantity = Quantity - NEW.QuantityOut WHERE (Id = NEW.FoodType);
END
|
DELIMITER ;

DELIMITER |
CREATE TRIGGER subfoodin BEFORE DELETE ON EntryFood
FOR EACH ROW 
BEGIN
	UPDATE Food SET Quantity = Quantity - OLD.QuantityIN WHERE Id = OLD.FoodType;
END
|
DELIMITER ;

DELIMITER |
CREATE TRIGGER addfoodout BEFORE DELETE ON OutGoingFood
FOR EACH ROW 
BEGIN
	UPDATE Food SET Quantity = Quantity + OLD.QuantityOut WHERE Id = OLD.FoodType;
END
|
DELIMITER ;

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