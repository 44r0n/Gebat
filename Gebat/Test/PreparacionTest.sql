DROP TABLE IF EXISTS Type;
CREATE TABLE IF NOT EXISTS Type
(
	Id int Primary Key AUTO_INCREMENT,
	Name varchar(10) not null
);


DROP TABLE IF EXISTS Food;
CREATE TABLE IF NOT EXISTS Food
(
	Id int Primary Key AUTO_INCREMENT,
	Name varchar(20) not null,
	Quantity int not null
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

INSERT INTO Food (Name,Quantity) VALUES
(
	'Patates',
	2
);

INSERT INTO Food (Name,Quantity) VALUES
(
	'Tomates',
	3
);

INSERT INTO Food (Name,Quantity) VALUES
(
	'Pa borrar',
	0
);

INSERT INTO Food (Name,Quantity) VALUES
(
	'Pomes',
	4
);

DELETE FROM Food WHERE Id = 3;
