DROP TABLE IF EXISTS TBC;
DROP TABLE IF EXISTS Delitos;
DROP TABLE IF EXISTS Personas;
DROP TABLE IF EXISTS Almacen;
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
  Apellidos VARCHAR(45) NULL
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
  CONSTRAINT fk_TBC_Personas FOREIGN KEY (DNI) REFERENCES Personas (DNI) ON DELETE NO ACTION ON UPDATE NO ACTION
  
);

CREATE OR REPLACE VIEW TBCPeople as select TBC.Id, Personas.DNI, Nombre, Apellidos, Ejecutoria, Juzgado, FInicio, FFin, NumJornadas, Lunes, Martes, Miercoles, Jueves, Viernes,Sabado, Domingo from Personas inner join TBC on (Personas.DNI = TBC.DNI);