DROP TABLE IF EXISTS Telefonos;
DROP TABLE IF EXISTS TBC;
DROP TABLE IF EXISTS Delitos;
DROP TABLE IF EXISTS Personas;
DROP TABLE IF EXISTS Almacen;
DROP TABLE IF EXISTS OutgoingFood;
DROP TABLE IF EXISTS EntryFood;
DROP TABLE IF EXISTS Food;
DROP TABLE IF EXISTS Type;

CREATE TABLE IF NOT EXISTS Delitos
(
	Id int Primary Key AUTO_INCREMENT,
	Name VARCHAR(45) NOT NULL
);

INSERT INTO Delitos (Name) VALUES
(
	'Robo'
);