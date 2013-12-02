DROP TABLE IF EXISTS TBC;
DROP TABLE IF EXISTS Personas;
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

CREATE TABLE IF NOT EXISTS Personas 
(
  DNI CHAR(9) Primary Key,
  Nombre VARCHAR(15) NULL,
  Apellidos VARCHAR(45) NULL
);

CREATE TABLE IF NOT EXISTS TBC 
(
  DNI CHAR(9) NOT NULL,
  Ejecutoria VARCHAR(10) NOT NULL,
  Juzgado VARCHAR(45) NULL,
  FInicio DATE NULL,
  FFin DATE NULL,
  PRIMARY KEY (DNI, Ejecutoria),
  CONSTRAINT fk_TBC_Personas FOREIGN KEY (DNI) REFERENCES Personas (DNI) ON DELETE NO ACTION ON UPDATE NO ACTION
);

CREATE OR REPLACE VIEW Entrada as select sum(QuantityIn) as Quantity, FoodType from entryfood group by FoodType;

CREATE OR REPLACE VIEW Salida as select sum(QuantityOut) as Quantity, FoodType from outgoingfood group by FoodType;

CREATE OR REPLACE VIEW TotalFood as select (entrada.Quantity - salida.Quantity) as Total, entrada.FoodType as FoodType FROM entrada inner join salida on (entrada.FoodType = salida.FoodType);

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