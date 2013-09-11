DROP TABLE Food;
CREATE TABLE Food
(
	Id int Primary Key AUTO_INCREMENT,
	Name varchar(20) not null,
	Quantity int not null
);

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
