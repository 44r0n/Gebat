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