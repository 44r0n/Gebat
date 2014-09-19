SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

DROP SCHEMA IF EXISTS `gebat` ;
CREATE SCHEMA IF NOT EXISTS `gebat` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci ;
USE `gebat` ;

-- -----------------------------------------------------
-- Table `gebat`.`Person`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `gebat`.`Person` (
  `idPerson` INT NOT NULL AUTO_INCREMENT,
  `Name` BLOB NOT NULL,
  `Surname` BLOB NOT NULL,
  `BirthDate` BLOB NOT NULL,
  `DNI` BLOB NOT NULL,
  `Gender` BLOB NOT NULL,
  PRIMARY KEY (`idPerson`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `gebat`.`TBC`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `gebat`.`TBC` (
  `Judgement` BLOB NOT NULL,
  `Court` BLOB NOT NULL,
  `Person_idPerson` INT NOT NULL,
  `BeginDate` BLOB NOT NULL,
  `FinishDate` BLOB NOT NULL,
  `Numjourney` BLOB NOT NULL,
  `Monday` TINYINT(1) NULL,
  `Tuesday` TINYINT(1) NULL,
  `Wednesday` TINYINT(1) NULL,
  `Thursday` TINYINT(1) NULL,
  `Friday` TINYINT(1) NULL,
  `Saturday` TINYINT(1) NULL,
  `Sunday` TINYINT(1) NULL,
  `TBCcol` VARCHAR(45) NULL,
  `BeginHour` BLOB NOT NULL,
  `FinishHour` BLOB NOT NULL,
  PRIMARY KEY (`Person_idPerson`),
  INDEX `fk_TBC_People_idx` (`Person_idPerson` ASC),
  CONSTRAINT `fk_TBC_People`
    FOREIGN KEY (`Person_idPerson`)
    REFERENCES `gebat`.`Person` (`idPerson`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `gebat`.`Dosier`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `gebat`.`Dosier` (
  `idDosier` INT NOT NULL AUTO_INCREMENT,
  `Observations` VARCHAR(255) NULL,
  PRIMARY KEY (`idDosier`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `gebat`.`Familiar`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `gebat`.`Familiar` (
  `Person_idPerson` INT NOT NULL,
  `Income` BLOB NOT NULL,
  `Dosier_idDosier` INT NOT NULL,
  PRIMARY KEY (`Person_idPerson`),
  INDEX `fk_Familiar_Dosier1_idx` (`Dosier_idDosier` ASC),
  CONSTRAINT `fk_Familiar_People1`
    FOREIGN KEY (`Person_idPerson`)
    REFERENCES `gebat`.`Person` (`idPerson`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Familiar_Dosier1`
    FOREIGN KEY (`Dosier_idDosier`)
    REFERENCES `gebat`.`Dosier` (`idDosier`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `gebat`.`ConcesionType`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `gebat`.`ConcesionType` (
  `idConcesionType` INT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(15) NOT NULL,
  `MonthRestriction` INT NULL,
  `IncomeRestriction` INT NULL,
  PRIMARY KEY (`idConcesionType`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `gebat`.`Concesion`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `gebat`.`Concesion` (
  `idConcesion` INT NOT NULL AUTO_INCREMENT,
  `BeginDate` DATE NOT NULL,
  `FinishDate` DATE NOT NULL,
  `Dosier_idDosier` INT NOT NULL,
  `Notes` VARCHAR(255) NULL,
  `ConcesionType_idConcesionType` INT NOT NULL,
  PRIMARY KEY (`idConcesion`, `Dosier_idDosier`, `ConcesionType_idConcesionType`),
  INDEX `fk_Concesion_Dosier1_idx` (`Dosier_idDosier` ASC),
  INDEX `fk_Concesion_ConcesionType1_idx` (`ConcesionType_idConcesionType` ASC),
  CONSTRAINT `fk_Concesion_Dosier1`
    FOREIGN KEY (`Dosier_idDosier`)
    REFERENCES `gebat`.`Dosier` (`idDosier`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Concesion_ConcesionType1`
    FOREIGN KEY (`ConcesionType_idConcesionType`)
    REFERENCES `gebat`.`ConcesionType` (`idConcesionType`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `gebat`.`Crime`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `gebat`.`Crime` (
  `idCrimes` INT NOT NULL AUTO_INCREMENT,
  `Nombre` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idCrimes`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `gebat`.`TBC_has_Crime`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `gebat`.`TBC_has_Crime` (
  `TBC_Person_idPerson` INT NOT NULL,
  `Crimes_idCrimes` INT NOT NULL,
  PRIMARY KEY (`TBC_Person_idPerson`, `Crimes_idCrimes`),
  INDEX `fk_TBC_has_Crimes_Crimes1_idx` (`Crimes_idCrimes` ASC),
  INDEX `fk_TBC_has_Crimes_TBC1_idx` (`TBC_Person_idPerson` ASC),
  CONSTRAINT `fk_TBC_has_Crimes_TBC1`
    FOREIGN KEY (`TBC_Person_idPerson`)
    REFERENCES `gebat`.`TBC` (`Person_idPerson`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_TBC_has_Crimes_Crimes1`
    FOREIGN KEY (`Crimes_idCrimes`)
    REFERENCES `gebat`.`Crime` (`idCrimes`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `gebat`.`FoodType`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `gebat`.`FoodType` (
  `idFoodType` INT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idFoodType`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `gebat`.`Food`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `gebat`.`Food` (
  `idFood` INT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(45) NOT NULL,
  `Quantity` INT NOT NULL,
  `FoodType_idFoodType` INT NOT NULL,
  PRIMARY KEY (`idFood`, `FoodType_idFoodType`),
  INDEX `fk_Food_FoodType1_idx` (`FoodType_idFoodType` ASC),
  CONSTRAINT `fk_Food_FoodType1`
    FOREIGN KEY (`FoodType_idFoodType`)
    REFERENCES `gebat`.`FoodType` (`idFoodType`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `gebat`.`Concesion_has_Food`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `gebat`.`Concesion_has_Food` (
  `Concesion_idConcesion` INT NOT NULL,
  `Concesion_Dosier_idDosier` INT NOT NULL,
  `Concesion_ConcesionType_idConcesionType` INT NOT NULL,
  `Food_idFood` INT NOT NULL,
  `Food_FoodType_idFoodType` INT NOT NULL,
  PRIMARY KEY (`Concesion_idConcesion`, `Concesion_Dosier_idDosier`, `Concesion_ConcesionType_idConcesionType`, `Food_idFood`, `Food_FoodType_idFoodType`),
  INDEX `fk_Concesion_has_Food_Food1_idx` (`Food_idFood` ASC, `Food_FoodType_idFoodType` ASC),
  INDEX `fk_Concesion_has_Food_Concesion1_idx` (`Concesion_idConcesion` ASC, `Concesion_Dosier_idDosier` ASC, `Concesion_ConcesionType_idConcesionType` ASC),
  CONSTRAINT `fk_Concesion_has_Food_Concesion1`
    FOREIGN KEY (`Concesion_idConcesion` , `Concesion_Dosier_idDosier` , `Concesion_ConcesionType_idConcesionType`)
    REFERENCES `gebat`.`Concesion` (`idConcesion` , `Dosier_idDosier` , `ConcesionType_idConcesionType`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Concesion_has_Food_Food1`
    FOREIGN KEY (`Food_idFood` , `Food_FoodType_idFoodType`)
    REFERENCES `gebat`.`Food` (`idFood` , `FoodType_idFoodType`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `gebat`.`FoodIn`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `gebat`.`FoodIn` (
  `idFoodIn` INT NOT NULL AUTO_INCREMENT,
  `Date` DATE NULL,
  `QuantityIn` INT NOT NULL,
  `Food_idFood` INT NOT NULL,
  `Food_FoodType_idFoodType` INT NOT NULL,
  PRIMARY KEY (`idFoodIn`, `Food_idFood`, `Food_FoodType_idFoodType`),
  INDEX `fk_FoodIn_Food1_idx` (`Food_idFood` ASC, `Food_FoodType_idFoodType` ASC),
  CONSTRAINT `fk_FoodIn_Food1`
    FOREIGN KEY (`Food_idFood` , `Food_FoodType_idFoodType`)
    REFERENCES `gebat`.`Food` (`idFood` , `FoodType_idFoodType`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `gebat`.`FoodOut`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `gebat`.`FoodOut` (
  `idFoodOut` INT NOT NULL AUTO_INCREMENT,
  `Date` DATE NULL,
  `QuantityOut` INT NOT NULL,
  `Food_idFood` INT NOT NULL,
  `Food_FoodType_idFoodType` INT NOT NULL,
  PRIMARY KEY (`idFoodOut`, `Food_idFood`, `Food_FoodType_idFoodType`),
  INDEX `fk_FoodOut_Food1_idx` (`Food_idFood` ASC, `Food_FoodType_idFoodType` ASC),
  CONSTRAINT `fk_FoodOut_Food1`
    FOREIGN KEY (`Food_idFood` , `Food_FoodType_idFoodType`)
    REFERENCES `gebat`.`Food` (`idFood` , `FoodType_idFoodType`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

USE `gebat` ;

-- -----------------------------------------------------
-- Placeholder table for view `gebat`.`TBCPerson`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `gebat`.`TBCPerson` (`Person_idPerson` INT, `DNI` INT, `Name` INT, `Surname` INT, `BirthDate` INT, `Gender` INT, `Judgement` INT, `Court` INT, `BeginDate` INT, `FinishDate` INT, `NumJourney` INT, `Monday` INT, `Tuesday` INT, `Wednesday` INT, `Thursday` INT, `Friday` INT, `Saturday` INT, `Sunday` INT, `BeginHour` INT, `FinishHour` INT);

-- -----------------------------------------------------
-- Placeholder table for view `gebat`.`FamiliarData`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `gebat`.`FamiliarData` (`Person_idPerson` INT, `DNI` INT, `Name` INT, `Surname` INT, `BirthDate` INT, `Gender` INT, `Income` INT);

-- -----------------------------------------------------
-- View `gebat`.`TBCPerson`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `gebat`.`TBCPerson`;
USE `gebat`;
CREATE  OR REPLACE VIEW `TBCPerson` as select TBC.Person_idPerson, DNI, Name, Surname, BirthDate, Gender ,Judgement, Court, BeginDate, FinishDate, NumJourney, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday, BeginHour, FinishHour from Person inner join TBC on (Person.idPerson = TBC.Person_idPerson);


-- -----------------------------------------------------
-- View `gebat`.`FamiliarData`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `gebat`.`FamiliarData`;
USE `gebat`;
CREATE OR REPLACE VIEW `FamiliarData` as select Familiar.Person_idPerson, DNI, Name, Surname, BirthDate, Gender, Income FROM Person inner join Familiar on (Person.idPerson = Familiar.Person_idPerson);


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
