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
	Quantity int not null,
	QuantityType int,
	CONSTRAINT fk_Food_Type FOREIGN KEY (QuantityType) REFERENCES Type (Id) ON UPDATE SET NULL ON DELETE SET NULL
);

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

INSERT INTO Food (Name,Quantity,QuantityType) VALUES
(
	'Patates',
	2,
	1
);

INSERT INTO Food (Name,Quantity,QuantityType) VALUES
(
	'Tomates',
	3,
	1
);

INSERT INTO Food (Name,Quantity) VALUES
(
	'Pa borrar',
	0
);

INSERT INTO Food (Name,Quantity,QuantityType) VALUES
(
	'Pomes',
	4,
	1
);

DELETE FROM Food WHERE Id = 3;
