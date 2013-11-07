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
	CONSTRAINT fk_EntryFood_Food FOREIGN KEY (FoodType) REFERENCES Food (Id) ON UPDATE SET NULL ON DELETE SET NULL
);

CREATE TABLE IF NOT EXISTS OutgoingFood
(
	Id int Primary Key AUTO_INCREMENT,
	FoodType int,
	QuantityOut int,
	CONSTRAINT fk_OutgoingFood_Food FOREIGN KEY (FoodType) REFERENCES Food (Id) ON UPDATE SET NULL ON DELETE SET NULL
);

CREATE OR REPLACE VIEW TotalFood as select (entrada.Quantity - salida.Quantity) as Total, entrada.FoodType FROM entrada inner join salida on (entrada.FoodType = salida.FoodType);

CREATE OR REPLACE VIEW Entrada as select sum(QuantityIn) as Quantity, FoodType from entryfood group by FoodType;

CREATE OR REPLACE VIEW Salida as select sum(QuantityOut) as Quantity, FoodType from outgoingfood group by FoodType;