DROP TABLE IF EXISTS TBC;
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

CREATE TABLE IF NOT EXISTS Personas 
(
  Id int Primary Key AUTO_INCREMENT,
  DNI CHAR(9) Unique,
  Nombre VARCHAR(15) NULL,
  Apellidos VARCHAR(45) NULL
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
  Unique (DNI, Ejecutoria),
  CONSTRAINT fk_TBC_Personas FOREIGN KEY (DNI) REFERENCES Personas (DNI) ON DELETE NO ACTION ON UPDATE NO ACTION
);

CREATE OR REPLACE VIEW TBCPeople as select TBC.Id, Personas.DNI, Nombre, Apellidos, Ejecutoria, Juzgado, FInicio, FFin, NumJornadas, Lunes, Martes, Miercoles, Jueves, Viernes, Sabado, Domingo from Personas inner join TBC on (Personas.DNI = TBC.DNI);

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

INSERT INTO EntryFood(FoodType,QuantityIn, Fecha) VALUES
(
	1,
	1,
	'2012/11/20'
);

INSERT INTO EntryFood(FoodType,QuantityIn, Fecha) VALUES
(
	1,
	2,
	'2012/11/21'
);

INSERT INTO EntryFood(FoodType,QuantityIn,Fecha) VALUES
(
	4,
	4,
	'2012/11/22'
);

INSERT INTO EntryFood(FoodType, QuantityIn, Fecha) VALUES
(
	1,
	3,
	'2012/11/23'
);

INSERT INTO OutgoingFood(FoodType,QuantityOut, Fecha) VALUES
(
	1,
	1,
	'2012/11/24'
);

INSERT INTO OutgoingFood(FoodType,QuantityOut,Fecha) VALUES
(
	1,
	1,
	'2012/11/24'
);

INSERT INTO OutgoingFood(FoodType,QuantityOut, Fecha) VALUES
(
	4,
	2,
	'2012/11/25'
);

INSERT INTO Personas (DNI, Nombre, Apellidos) VALUES
(
	'54508005Y',
	'Pepe',
	'Olivares'
);

INSERT INTO Personas (DNI, Nombre, Apellidos) VALUES
(
	'01086932K',
	'Ana',
	'Entrepinares'
);

INSERT INTO Personas (DNI, Nombre, Apellidos) VALUES
(
	'12218401L',
	'Isabel',
	'Gonzalez'
);

INSERT INTO Personas (DNI, Nombre, Apellidos) VALUES
(
	'93909231R',
	'Yerai',
	'Gallardo'
);

INSERT INTO TBC (DNI, Ejecutoria, Juzgado, FInicio, FFin, NumJornadas,Lunes, Martes, Miercoles, Jueves, Viernes, Sabado, Domingo) VALUES
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
	FALSE
);

INSERT INTO TBC (DNI, Ejecutoria, Juzgado, FInicio, FFin, NumJornadas,Lunes, Martes, Miercoles, Jueves, Viernes, Sabado, Domingo) VALUES
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
	FALSE
);

INSERT INTO TBC (DNI, Ejecutoria, Juzgado, FInicio, FFin, NumJornadas,Lunes, Martes, Miercoles, Jueves, Viernes, Sabado, Domingo) VALUES
(
	'01086932K',
	'93/2012',
	'Juzgado Valencia',
	'2014/05/20',
	'2014/10/10',
	250,
	TRUE,
	TRUE,
	TRUE,
	TRUE,
	TRUE,
	FALSE,
	FALSE
);