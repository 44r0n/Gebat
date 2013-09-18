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