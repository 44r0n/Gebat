DROP TABLE IF EXISTS Phones;
DROP TABLE IF EXISTS TBC;
DROP TABLE IF EXISTS Crimes;
DROP TABLE IF EXISTS Fega;
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
	Fega tinyint not null,
	CONSTRAINT fk_Food_Type FOREIGN KEY (QuantityType) REFERENCES Type (Id) ON UPDATE SET NULL ON DELETE SET NULL
);

CREATE INDEX fegafood ON Food (Fega);

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
  DNI VARCHAR(255) Unique,
  Name VARCHAR(255) NULL,
  Surname VARCHAR(255) NULL,
  BirthDate VARCHAR(255) NULL,
  Gender VARCHAR(255) NULL
);

CREATE TABLE IF NOT EXISTS PersonalDossier
(
	Id int PRIMARY KEY AUTO_INCREMENT,
	Observations varchar(255)
);

CREATE TABLE IF NOT EXISTS Familiars
(
	Id int Primary Key AUTO_INCREMENT,
	Id_Person int Unique,
	Dossier int,
	Income varchar(255),
	CONSTRAINT fk_Familiars_People FOREIGN KEY (Id_Person) REFERENCES People (Id) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT fk_Familiars_Dossier FOREIGN KEY (Dossier) REFERENCES PersonalDossier (Id) ON DELETE SET NULL ON UPDATE CASCADE
);

CREATE TABLE IF NOT EXISTS Concessions
(
	Id int Primary Key AUTO_INCREMENT,
	Dossier INT,
	BeginDate varchar(255) NULL,
	FinishDate varchar(255) NULL,
	Notes VARCHAR(150),
	CONSTRAINT fk_Concessions_Dossier FOREIGN KEY (Dossier) REFERENCES PersonalDossier (Id) ON DELETE SET NULL ON UPDATE CASCADE
);

CREATE TABLE IF NOT EXISTS Fresco
(
	Concession Int Unique,
	CONSTRAINT fk_Fresco_Concessions FOREIGN KEY (Concession) REFERENCES Concessions (Id) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE IF NOT EXISTS Fega
(
	Concession Int Unique,
	State varchar(10),
	CONSTRAINT fk_Fega_Concessions FOREIGN KEY (Concession) REFERENCES Concessions (Id) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE IF NOT EXISTS Crimes
(
	Id int Primary Key AUTO_INCREMENT,
	Name VARCHAR(45) NOT NULL
);

CREATE TABLE IF NOT EXISTS TBC 
(
  Id int PRIMARY KEY AUTO_INCREMENT,
  Id_Person int NOT NULL,
  Judgement VARCHAR(255) NOT NULL,
  Court VARCHAR(255) NULL,
  BeginDate varchar(255) NULL,
  FinishDate varchar(255) NULL,
  NumJourney INT,
  Monday BOOLEAN DEFAULT FALSE,
  Tuesday BOOLEAN DEFAULT FALSE,
  Wednesday BOOLEAN DEFAULT FALSE,
  Thursday BOOLEAN DEFAULT FALSE,
  Friday BOOLEAN DEFAULT FALSE,
  Saturday BOOLEAN DEFAULT FALSE,
  Sunday BOOLEAN DEFAULT FALSE,
  BeginHour TIME,
  FinishHour TIME,
  Crime int NOT NULL,
  Unique (Id_Person, Judgement),
  CONSTRAINT fk_TBC_Crimes FOREIGN KEY (Crime) REFERENCES Crimes (Id) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT fk_TBC_People FOREIGN KEY (Id_Person) REFERENCES People (Id) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE IF NOT EXISTS Phones
(
	Id int PRIMARY KEY AUTO_INCREMENT,
	PhoneNumber varchar(255) NOT NULL,
	Owner int NOT NULL,
	CONSTRAINT fk_Phones_People FOREIGN KEY (Owner) REFERENCES People(Id) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE OR REPLACE VIEW TBCPeople as select TBC.Id, People.DNI, Name, Surname, BirthDate, Gender ,Judgement, Court, BeginDate, FinishDate, NumJourney, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday, BeginHour, FinishHour,Crime from People inner join TBC on (People.Id = TBC.Id_Person);

CREATE OR REPLACE VIEW FamiliarData as select Familiars.Id, People.DNI, Name, Surname, BirthDate, Gender, Dossier, Income FROM People inner join Familiars on (People.Id = Familiars.Id_Person);

CREATE OR REPLACE VIEW FrescoData as select Concessions.Id, Dossier, BeginDate, FinishDate, Notes FROM Concessions inner join Fresco on (Concessions.Id = Fresco.Concession);

CREATE OR REPLACE VIEW FegaData as select Concessions.Id, Dossier, BeginDate, FinishDate, Notes, State FROM Concessions inner join Fega on (Concessions.Id = Fega.Concession);