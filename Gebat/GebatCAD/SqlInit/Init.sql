DROP TABLE IF EXISTS Telefonos;
DROP TABLE IF EXISTS TBC;
DROP TABLE IF EXISTS Delitos;
DROP TABLE IF EXISTS Familiares;
DROP TABLE IF EXISTS Personas;
DROP TABLE IF EXISTS ExpedientesPersonales;
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
	Fecha date,
	CONSTRAINT fk_EntryFood_Food FOREIGN KEY (FoodType) REFERENCES Food (Id) ON UPDATE SET NULL ON DELETE SET NULL
);

CREATE TABLE IF NOT EXISTS OutgoingFood
(
	Id int Primary Key AUTO_INCREMENT,
	FoodType int,
	QuantityOut int,
	Fecha date,
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

CREATE TABLE IF NOT EXISTS Personas 
(
  Id int Primary Key AUTO_INCREMENT,
  DNI CHAR(9) Unique,
  Nombre VARCHAR(15) NULL,
  Apellidos VARCHAR(45) NULL,
  FechaNac DATE NULL,
  Sexo CHAR(1) NULL
);

CREATE TABLE IF NOT EXISTS ExpedientesPersonales
(
	Id int PRIMARY KEY AUTO_INCREMENT,
	Ingresos INT,
	Observaciones varchar(255)
);

CREATE TABLE IF NOT EXISTS Familiares
(
	Id int Primary Key AUTO_INCREMENT,
	DNI CHAR(9) NOT NULL,
	Expediente int,
	CONSTRAINT fk_Familiares_Personas FOREIGN KEY (DNI) REFERENCES Personas (DNI) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT fk_Familiares_Expediente FOREIGN KEY (Expediente) REFERENCES ExpedientesPersonales (Id) ON DELETE SET NULL ON UPDATE CASCADE
);

CREATE TABLE IF NOT EXISTS Delitos
(
	Id int Primary Key AUTO_INCREMENT,
	Name VARCHAR(45) NOT NULL
);

CREATE TABLE IF NOT EXISTS TBC 
(
  Id int PRIMARY KEY AUTO_INCREMENT,
  DNI CHAR(9) NOT NULL,
  Ejecutoria VARCHAR(10) NOT NULL,
  Juzgado VARCHAR(45) NULL,
  FInicio DATE NULL,
  FFin DATE NULL,
  NumJornadas INT,
  Lunes BOOLEAN DEFAULT FALSE,
  Martes BOOLEAN DEFAULT FALSE,
  Miercoles BOOLEAN DEFAULT FALSE,
  Jueves BOOLEAN DEFAULT FALSE,
  Viernes BOOLEAN DEFAULT FALSE,
  Sabado BOOLEAN DEFAULT FALSE,
  Domingo BOOLEAN DEFAULT FALSE,
  Delito int NOT NULL,
  Unique (DNI, Ejecutoria),
  CONSTRAINT fk_TBC_Delitos FOREIGN KEY (Delito) REFERENCES Delitos (Id) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT fk_TBC_Personas FOREIGN KEY (DNI) REFERENCES Personas (DNI) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE IF NOT EXISTS Telefonos
(
	Id int PRIMARY KEY AUTO_INCREMENT,
	Numero CHAR(9) NOT NULL,
	DNI CHAR(9) NOT NULL,
	CONSTRAINT fk_Telefonos_Personas FOREIGN KEY (DNI) REFERENCES Personas(DNI) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE OR REPLACE VIEW TBCPeople as select TBC.Id, Personas.DNI, Nombre, Apellidos, FechaNac, Sexo ,Ejecutoria, Juzgado, FInicio, FFin, NumJornadas, Lunes, Martes, Miercoles, Jueves, Viernes,Sabado, Domingo, Delito from Personas inner join TBC on (Personas.DNI = TBC.DNI);

CREATE OR REPLACE VIEW DatosFamiliares as select Familiares.Id, Personas.DNI, Nombre, Apellidos, FechaNac, Sexo FROM Personas inner join Familiares on (Personas.DNI = Familiares.DNI);