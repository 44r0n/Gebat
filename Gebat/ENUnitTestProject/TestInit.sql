DROP TABLE IF EXISTS Phones;
DROP TABLE IF EXISTS TBC;
DROP TABLE IF EXISTS Crimes;
DROP TABLE IF EXISTS Fega;
DROP TABLE IF EXISTS Fresco;
DROP TABLE IF EXISTS Concessions;
DROP TABLE IF EXISTS Familiars;
DROP TABLE IF EXISTS People;
DROP TABLE IF EXISTS PersonalDossier;
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
	Fega tinyint not null,
	CONSTRAINT fk_Food_Type FOREIGN KEY (QuantityType) REFERENCES Type (Id) ON UPDATE SET NULL ON DELETE SET NULL
);

CREATE INDEX fegafood ON Food (Fega);

CREATE TABLE IF NOT EXISTS EntryFood
(
	Id int Primary Key AUTO_INCREMENT,
	FoodType int,
	QuantityIn int,
	DateTime date,
	CONSTRAINT fk_EntryFood_Food FOREIGN KEY (FoodType) REFERENCES Food (Id) ON UPDATE SET NULL ON DELETE SET NULL
);

CREATE TABLE IF NOT EXISTS OutgoingFood
(
	Id int Primary Key AUTO_INCREMENT,
	FoodType int,
	QuantityOut int,
	DateTime date,
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

CREATE TABLE IF NOT EXISTS People
(
  Id int Primary Key AUTO_INCREMENT,
  DNI VARCHAR(255) Unique,
  Name VARCHAR(255) NULL,
  Surname VARCHAR(255) NULL,
  BirthDate VARCHAR(255) NULL,
  Gender VARCHAR(255) NULL
);

CREATE TABLE IF NOT EXISTS PersonalDossier
(
	Id int PRIMARY KEY AUTO_INCREMENT,
	Observations varchar(255)
);

CREATE TABLE IF NOT EXISTS Familiars
(
	Id int Primary Key AUTO_INCREMENT,
	Id_Person int Unique,
	Dossier int,
	Income varchar(255),
	CONSTRAINT fk_Familiars_People FOREIGN KEY (Id_Person) REFERENCES People (Id) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT fk_Familiars_Dossier FOREIGN KEY (Dossier) REFERENCES PersonalDossier (Id) ON DELETE SET NULL ON UPDATE CASCADE
);

CREATE TABLE IF NOT EXISTS Concessions
(
	Id int Primary Key AUTO_INCREMENT,
	Dossier INT,
	BeginDate varchar(255) NULL,
	FinishDate varchar(255) NULL,
	Notes VARCHAR(150),
	CONSTRAINT fk_Concessions_Dossier FOREIGN KEY (Dossier) REFERENCES PersonalDossier (Id) ON DELETE SET NULL ON UPDATE CASCADE
);

CREATE TABLE IF NOT EXISTS Fresco
(
	Concession Int Unique,
	CONSTRAINT fk_Fresco_Concessions FOREIGN KEY (Concession) REFERENCES Concessions (Id) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE IF NOT EXISTS Fega
(
	Concession Int Unique,
	State varchar(10),
	CONSTRAINT fk_Fega_Concessions FOREIGN KEY (Concession) REFERENCES Concessions (Id) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE IF NOT EXISTS Crimes
(
	Id int Primary Key AUTO_INCREMENT,
	Name VARCHAR(45) NOT NULL
);

CREATE TABLE IF NOT EXISTS TBC 
(
  Id int PRIMARY KEY AUTO_INCREMENT,
  Id_Person int NOT NULL,
  Judgement VARCHAR(255) NOT NULL,
  Court VARCHAR(255) NULL,
  BeginDate varchar(255) NULL,
  FinishDate varchar(255) NULL,
  NumJourney INT,
  Monday BOOLEAN DEFAULT FALSE,
  Tuesday BOOLEAN DEFAULT FALSE,
  Wednesday BOOLEAN DEFAULT FALSE,
  Thursday BOOLEAN DEFAULT FALSE,
  Friday BOOLEAN DEFAULT FALSE,
  Saturday BOOLEAN DEFAULT FALSE,
  Sunday BOOLEAN DEFAULT FALSE,
  BeginHour TIME,
  FinishHour TIME,
  Crime int NOT NULL,
  Unique (Id_Person, Judgement),
  CONSTRAINT fk_TBC_Crimes FOREIGN KEY (Crime) REFERENCES Crimes (Id) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT fk_TBC_People FOREIGN KEY (Id_Person) REFERENCES People (Id) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE IF NOT EXISTS Phones
(
	Id int PRIMARY KEY AUTO_INCREMENT,
	PhoneNumber varchar(255) NOT NULL,
	Owner int NOT NULL,
	CONSTRAINT fk_Phones_People FOREIGN KEY (Owner) REFERENCES People(Id) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE OR REPLACE VIEW TBCPeople as select TBC.Id, People.DNI, Name, Surname, BirthDate, Gender ,Judgement, Court, BeginDate, FinishDate, NumJourney, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday, BeginHour, FinishHour,Crime from People inner join TBC on (People.Id = TBC.Id_Person);

CREATE OR REPLACE VIEW FamiliarData as select Familiars.Id, People.DNI, Name, Surname, BirthDate, Gender, Dossier, Income FROM People inner join Familiars on (People.Id = Familiars.Id_Person);

CREATE OR REPLACE VIEW FrescoData as select Concessions.Id, Dossier, BeginDate, FinishDate, Notes FROM Concessions inner join Fresco on (Concessions.Id = Fresco.Concession);

CREATE OR REPLACE VIEW FegaData as select Concessions.Id, Dossier, BeginDate, FinishDate, Notes, State FROM Concessions inner join Fega on (Concessions.Id = Fega.Concession);

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

INSERT INTO Food (Name,QuantityType, Fega) VALUES
(
    'Patates',
    1,
	true
);

INSERT INTO Food (Name,QuantityType, Fega) VALUES
(
    'Tomates',
    1,
	false
);

INSERT INTO Food (Name, Fega) VALUES
(
    'Pa borrar',
	false
);

INSERT INTO Food (Name,QuantityType, Fega) VALUES
(
    'Pomes',
    1,
	false
);

DELETE FROM Food WHERE Id = 3;

INSERT INTO EntryFood(FoodType,QuantityIn, DateTime) VALUES
(
	1,
	1,
	'2012/11/20'
);

INSERT INTO EntryFood(FoodType,QuantityIn, DateTime) VALUES
(
	1,
	2,
	'2012/11/21'
);

INSERT INTO EntryFood(FoodType,QuantityIn,DateTime) VALUES
(
	4,
	4,
	'2012/11/22'
);

INSERT INTO EntryFood(FoodType, QuantityIn, DateTime) VALUES
(
	1,
	3,
	'2012/11/23'
);

INSERT INTO OutgoingFood(FoodType,QuantityOut, DateTime) VALUES
(
	1,
	1,
	'2012/11/24'
);

INSERT INTO OutgoingFood(FoodType,QuantityOut,DateTime) VALUES
(
	1,
	1,
	'2012/11/24'
);

INSERT INTO OutgoingFood(FoodType,QuantityOut, DateTime) VALUES
(
	4,
	2,
	'2012/11/25'
);

INSERT INTO Crimes (Name) VALUES
(
	"Robo"
);

INSERT INTO Crimes (Name) VALUES
(
	"Pelea"
);

INSERT INTO Crimes (Name) VALUES
(
	"Otro"
);

INSERT INTO PersonalDossier(Observations) VALUES
(
	"ZUWtBuY07UN7gifN1/S6/JrcoiBZH09rhocIIuozAeMdPWDWKzNlmmjBOybAIuuZZFpHDi1n1zFfLGMhJwqIAIaXvxdTYGTIJqgSJocR1Yg4HRwdvkRdoOahV49MQs3CprxtNcrRhjKQB7GkxLE9D4PLVkrjd9tBJszhvrg3YlY="
);

INSERT INTO PersonalDossier(Observations) VALUES
(
	"ZHMq0yjLFPs2zn1ENGzcbRZVo4GKbLFgusD3YAmX2UUyMAjI6PtCqi9ISj7XPCEzRzS+SdMRmcgPwCw770YI1g=="
);

INSERT INTO PersonalDossier(Observations) VALUES
(
	"/R9ruqPCgNQ5UJyufNgIROU7JSDd37I8hqMpce7K+Hz4CxFIEObXiBXwZLO2BXn2rXXOPS2UTsOPxxyZHv/vRQ=="
);

INSERT INTO PersonalDOssier(Observations) VALUES
(
	"MTNCQOOOMtRZWnHY9w2Ormv47jBnpirO0P5Or9GHWsENA9iJSGXBMpnZ0N22EYp4YgE0jldansLAuXxy7KUHEw=="
);

INSERT INTO People (DNI, Name, Surname, BirthDate, Gender) VALUES
(
	'MFUY6+kHcwZRMK65HMsmUtgM1p8XxX72Nh2JNXT2HaW3DFBIZF1BcLKFq2yzpYN/JZ24Yb4TlHFMpwePnwtKCcVlTrels9lMmNYGgy7KhN8=',
	'Ipb0qy7vQ5ThmVliZCOTUuQBN0mZ7mWrmGtBrQFkO338ACpHQZNoJz68d/+xD9fQKjwoFt+QSG7iE7oa+HgWvQ==',
	'APM4O4AD7+G13MtWgvRpQK7VKX84QaEg+b2fWOrYO8aDOCzochLe2pyKPIR6KLDWvfwhgoyFTDU/XWAedkrdC81B99t9oi3bTRWMh4UqBXQ=',
	'8LwOGwf+8RQQ/HdQZkCdWebK6gXXpTsGaKKMnf7sWYyui8coruy2wx9F8J3n/ID/1MGPVv2Xx08kVaMleC2MNqBjCPDlFiMS6S+G2+g7Q+U=',
	'/hzwqg1Koi9uK9ETtGEjk5R4YhYnOUWzv8qS6hAMAoU='
);

INSERT INTO People (DNI, Name, Surname, BirthDate, Gender) VALUES
(
	'i67Yfj7rub3tM7b9TmGSdLZ6fm8uH2YB3qBCk8Ts5QtpO3O2oAHwXZLv0tT8QSNhFQQdpaUGm7K1JMO9AkyKHuR3HGNRKYTO1mt22J+cp+M=',
	'k22H1l0sgUEbfwyXsNZiZyjLuadlrSS+VzXLfNAxFH9dlwDt9Mb1PhisANBIfcNKKseB8bcxsxtH2MvNTwDsQw==',
	'wwIr+ODpQsmWVhl+wqGaBdFUx3Fax/jPoTG10Yv//valuXRyVGLrqVOk0Ruanw799DVQJ/SXoIsiw+Ven/oWQ2pjvj8tf38zUAsVItNaHrpGbctJk8+RKTKdlKmcsMXd',
	'LtVGvNhutpohWDP2iBzVm+LrCYrkDWA/dSoC9KSwEVx5M9IE1JkxM70CkpV6IJtfqVOXXlMOI8O80VhI3Owh3QrNC328tD4vt7OmrNrYvrw=',
	'2BptBXhZEaCgj078oawKqWBvjA0opj+S7g6Y1UHH2pU='
);

INSERT INTO People (DNI, Name, Surname, BirthDate, Gender) VALUES
(
	'eJzxSUyeP4Q/JXXGzmiSiFQEOpfupmT8HNpbjv/tR0S4XlRvIx5OPjfpSsGkISjWvWqXVj+bmLb5RWQsLM4beyRBN0mkySUQ3U5rnfuS+qI=',
	'zHocuH4MHv3o54Ty2gzKISwCtvh/w/pbV3H2bYkH7llsZxvmvGBPQ0xF/Ql8ELNYCjGFKi+qt2/tKK04NC+YCw==',
	'S6rdsVMlFwFlKIucp/P4Ltbs0xUgrroEwkYzDcroV8Y1GKR0t7s1kWd7gif1yzMElEE/wqpeRDvgWgVwePQ6c4969mO83ZqSuFJ94Dce1p0=',
	'8LwOGwf+8RQQ/HdQZkCdWREp8Hd/4NSrm+fbkeYHK+zx3BQZuPuC4BtDu6Ro8ixX9IYHXdIdFAH8hfS5zo7YfUXdWGXXx/MjIgtgvfZl6qo=',
	'2BptBXhZEaCgj078oawKqWBvjA0opj+S7g6Y1UHH2pU='
);

INSERT INTO People (DNI, Name, Surname, BirthDate, Gender) VALUES
(
	'0lyP2suU0Oecb41SpalVhaoQymXy3QM83G4cEqxCiW+gSEW6UqpvXX6ZKqrnVi7KRf88Dsy4k6X2K4YNeEJAXWlBOZh20nXlUBNZgy8hrDA=',
	'5ftIK9bo2P6KD81GUF7GnpnG1eULOWr/5BnX3iIpntV8ZrczTOXr0dWfrLH31pSleyikHpRkXuqXLTOiKevu+g==',
	'FYcDbPcoh/fRrGa69KUj3S2Ma7Dek9mUsNFnPotr4cYNsI5DajMnFMjPdiijKq/6UYmAxPxmDfTAr+hxrf3BSS2G9t1h/KnT5z6K8dyrpJY=',
	'7Qh881wJJLpI/95t5x7VNtnCb/QInpesQJymOz9Y0yiV3acMSFR0RTcLf5eBuYKbAKFDG6K4IvValJhGc8HD6biexcRegdDEK4rlol5p2Fc=',
	'/hzwqg1Koi9uK9ETtGEjk5R4YhYnOUWzv8qS6hAMAoU='
);

INSERT INTO People (DNI, Name, Surname, BirthDate, Gender) VALUES
(
	'YanK2nYHd+aMsxkuknTt0qmz/YOtWb6WF5jBlavXwbk+b1R/D28crMZb5TQtXJ3ju0NMBpc5jqAN/iyljxyTX4Ptu0AKehDHZLh6zJYtcfI=',
	'X9Xl/R/jZn9d/2T9LwxvvZJ6Zbh29Yo9Of3+mju6dqmWBxeqNBy3MGhBBhIks2tKXf11FsUVG/elM1QF6niJlA==',
	'X9Xl/R/jZn9d/2T9LwxvvaewHuHcvCeC4cmY5JJRew05UGX6CRGfH9BeB/HnAlHoiBbzv5MyNnuFxZ1j2otxctmb/lC331HgaOEptNfU760=',
	'LtVGvNhutpohWDP2iBzVmxmQzjORSxr/oevRPueuYCpsIOmCj+U1RR+XtfWKzZ0JL7UJBU+xEiGuw4YvGS4zZ5yXYO46CIos6949xIv+yBs=',
	'2BptBXhZEaCgj078oawKqWBvjA0opj+S7g6Y1UHH2pU='
);

INSERT INTO People (DNI, Name, Surname, BirthDate, Gender) VALUES
(
	'vgXsF4Ofjju+pFp0mqOpoSuuUrXSD0LpZc2EnUv5Evk+GWLqCgdISiq3HxNVyQa3FjK0uRAhceJHlofYXbo6bB/5YEpyaB5EHioE9ymay8c=',
	'+pbW31nn3s7gf8u8eJla52nFOx4m0KtEp0TpW6Iqn5CeQ0xTWIRpubUbSWaGaaHiU0h6KNiUNMRXDpO9inwBtQ==',
	'vF4sf6bdoIyf56btu0jjr8agYqtZ4gNDI9z2fG97B7ffmiXXLnRuB3xFc3i0IzC8Mu2/CLvWoRoVEvZRnzOOL4hRkaWFg+b/Or6zhha15AY=',
	'8LwOGwf+8RQQ/HdQZkCdWbSbh/6b+7tu1H9kFlRQKOtKPK3nPzuLWsypXGJ+YKF4EKDpGpxbodNodUJi1h4fkIwfg1ytTLOrwO5836Jn5dE=',
	'/hzwqg1Koi9uK9ETtGEjk5R4YhYnOUWzv8qS6hAMAoU='
);

INSERT INTO People (DNI, Name,Surname,BirthDate, Gender) VALUES
(
	'kx9zer/CeOXS0V77lzJxRUde8tVHilfMCvZ4lmAxYlC6BXMEXd5GdTd2kYEsGxQ+FGx7ACx/H7wEPOijMD6WsU+K3El97zhxGM3wivrqJFc=',
	'FOA2cljRm2Ie8nea/b4ZA/jDPfjLaiwuZmmfOs8IHb9wbK6RYQ+dmnpFBCvfzqxxj8Cl7ntkgKB/YRX4FzznkA==',
	'WrFBlikC2l5eOc87OzU7mqt/b2mv7UkptRkei5b//aJRi9pZzYdwuu4Vtjmolaao/b8XedMs2wZuNnwKF4AvhQ==',
	'Wyzl4ThlTtiFmMw9TuMhMangRthykd5PFrY/rtv40W3TVOYAJfsvgagglKEt9j3ZsnU9MfwbjJWPMVpPUCdB+V60rVNY0J0QGQGmOBgBuuY=',
	'2BptBXhZEaCgj078oawKqWBvjA0opj+S7g6Y1UHH2pU='
);

INSERT INTO People(DNI, Name, Surname, BirthDate, Gender) VALUES
(
	'zE3Di9VbYdcg6RCzcPlxNwss3+X9NHwzu6uVS3VkngMYs1SYbhTONWszF8Ftgao0ZGIc2yPpPEFU/lFH1K0o2eEwlVWbkUkjdOdyiVjY0T8=',
	'Uc+xtmDIthiJDJ7mY6A3iPHUw38BS56CXPr0EVS8GdbQO8lXNXfoYqu5dnSZMKSvceYUej7wgKMi7JKk4ZK0QG7Whlb7CGbaNH/G/Y3I3xs=',
	'nqrxSWD2G2tAIBoThBw/tNR0nHM81zLIrimGdujdHgoH5loIw25ke5JcT/xyskAzh8sAR26urCHtQ1ZHfL8hOQ==',
	'LtVGvNhutpohWDP2iBzVmwPe46YLycV5P+hU2U2jHaOXAEsbU+a8Tz0xxjQ0fcOwqB3fWMPxBsm+XI4vJwbs6muYhKkqkWrU3z4+BZbeAPc=',
	'/hzwqg1Koi9uK9ETtGEjk5R4YhYnOUWzv8qS6hAMAoU='
);

INSERT INTO TBC (Id_Person, Judgement, Court, BeginDate, FinishDate, NumJourney,Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday, BeginHour, FinishHour, Crime) VALUES
(
	1,
	'f4860+OW6X888C02IYarkYZqw65kd0B2RO9JRFxLbrnZwMKVwE53SwTJDSKTA9eAoqLaPLwpvM86KeUGcbiBoKkeoXP0VCnWWwG0ZCUMtMo=',
	'8TAWAG8jtf6EKc4naRxX+g7uScnaBawhZ8wDioaM57FQSL5Hlh3/kRM7DyzPT7R5A1l+bL4si8V3TZKRgHLaIUzK9IIk1jbiztTRwXFuF4Y=',
	'Yi1nS5Pl3y7HmvGPVAexOpV3CNNQsCVdGgLSO/+RoH2WnlzMrf6yj/fNWAeDbgxUmdAwLuI4I0Hvh8Mh2P2DRK6YMOZF468r+UjN2DkuAB8=',
	'Yi1nS5Pl3y7HmvGPVAexOoj5o1fbz2XBoIbzBpprLefQmhiQiiCelQr7hNsbGlmV7RlU07zlKdvYGM7JU2vF9qERHYWRkfPR537elive9UQ=',
	180,
	TRUE,
	TRUE,
	TRUE,
	TRUE,
	TRUE,
	FALSE,
	FALSE,
	'9:00:00',
	'15:30:00',
	1
);

INSERT INTO TBC (Id_Person, Judgement, Court, BeginDate, FinishDate, NumJourney,Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday, BeginHour, FinishHour, Crime) VALUES
(
	2,
	'A3aU/z1rScWoDEJpqNwJoMdaV49PqnlTo4FMEJjeZuN13GAeOF8nA+UGbrbHqThXjunbg4ChER8XMvntpgRFTA==',
	'wRvuHK81j2rx4S09gASAiOn38587U2ySL53g3abJvBYXlUbF8ygbBHRktlBMEugahSYRAXqCll9N8q0Jf3vlWbfuidsM4oD1sPSBbZGQ+yxPJBNSdH02CVH98tb/KmhCs35+lC3ygiU4a2ZzwtwDo5zKppeiwK/rfSdT9ltqMmM=',
	'Yi1nS5Pl3y7HmvGPVAexOjcIm9ktCX8y71iLwGfC8ZQmWIkrjq17TWKG5hUg2gN1D8wgyJlg+HardG9S5vtEcQFYGLTtioUHl1ZlYzpdG2g=',
	'Yi1nS5Pl3y7HmvGPVAexOkBy0OrFwZ3+nv973VBHlPn8MMJ6B/2nB45WfDYHNY/4hpNEeUD/IDfHF4fe0ZuSgR+hzdhBLunjruKjxxJg4ME=',
	250,
	TRUE,
	TRUE,
	TRUE,
	TRUE,
	TRUE,
	FALSE,
	FALSE,
	'9:00:00',
	'15:30:00',
	2
);

INSERT INTO TBC (Id_Person, Judgement, Court, BeginDate, FinishDate, NumJourney,Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday, BeginHour, FinishHour, Crime) VALUES
(
	2,
	'I/7Y2z6Ui4EKONYhYwfHAkBGijJR6UMqjfV0ydxjiys3WsE9vetLLhZjdSsbvj/Bc9r+Q4tIeGdW0xn5ZN7sLKp+Hbcy1xLbcdVOiCBCcXU=',
	'toQD7ACu3nCHReMJPek66ReU+1WwOKvSeiFCwWsklssV7AVR+/p8LiOGU0DZZ884JM3vM/1XwjiM4sFyDykKp2QvQK50FNm0E0nxqocLkRnerkiWvFd3Sr2ioByEtlp7',
	'Yi1nS5Pl3y7HmvGPVAexOlM9jJDowx6pfJzYehdHZwlqqH3b10pFazm1FjAlO3zsmDeVO7kaiRRilm4Q001+5o9G/QpGGsCG9y3t7vv/fzs=',
	'Yi1nS5Pl3y7HmvGPVAexOmyh0XRHQ6HEQcmfPFArt0UQyG/wNQIVFHdiMqmUtwW8K8FSP8saVIdS6QFKAfEk64bCJ/+KqRQnPLw5Bx4/lRQ=',
	250,
	TRUE,
	TRUE,
	TRUE,
	TRUE,
	TRUE,
	FALSE,
	FALSE,
	'9:00:00',
	'15:30:00',
	1
);

INSERT INTO Phones(PhoneNumber, Owner) VALUES
(
	'zE3Di9VbYdcg6RCzcPlxNwss3+X9NHwzu6uVS3VkngPUJAtpO8IuGrrlc1OX4RlC/mUTjnTtDVXX30m2xrabRWvC6fpOJ89Lq9Ij9Mel3d0=',
	2
);

INSERT INTO Phones (PhoneNumber, Owner) VALUES
(
	'ShsgH6xXeByxN1t+zoVMzvf3cnbp/TOJErdU/I6y15giv02uA19UxMTN5yaKZlbOeIAWFB8nobomYtUf08Exe8KBOw26XoiUabjbQziZWVU=',
	2
);

INSERT INTO Familiars(Id_Person, Dossier,Income) VALUES
(
	5,
	1,
	'FN+CZMoeEzawPmSRHSrvd+tP415+tF+8gUTFmEYsb0QP8TxgXN2fcY3CAgUVMwhkXZ+g3hoVVZG+ykNTGPpyag=='
);

INSERT INTO Familiars(Id_Person, Dossier, Income) VALUES
(
	6,
	1,
	'FN+CZMoeEzawPmSRHSrvd+tP415+tF+8gUTFmEYsb0QP8TxgXN2fcY3CAgUVMwhkXZ+g3hoVVZG+ykNTGPpyag=='
);

INSERT INTO Familiars(Id_Person, Dossier,Income) VALUES
(
	7,
	2,
	'vSNPZvuGYdk735LzcDH77lnPUlQDBuy6a/ErLndZT9OndBWi+s6t4FuL1ApE2mnTk4jqdZh4b6oygHX56fItDA=='
);

INSERT INTO Familiars(Id_Person, Dossier, Income) VALUES
(
	8,
	3,
	'OA/YwsuP1HFK6Pxxnh4kUoAyIvXc63BB6ozfNBe9CrHzZY/s+pb9S7lSOyeGh9kKuPFDCPv1t5hx8EjDeY9pCg=='
);

INSERT INTO Concessions (Dossier, BeginDate, FinishDate, Notes) VALUES
(
	1,
	'Yi1nS5Pl3y7HmvGPVAexOoyef9krLY91eV51px8cCbr669hMiWVlzvlfSNAhhgpElPrEKA+Ncu7vIT5glqXNApm8IDWSJ4GVrrl+a/A60QE=',
	'Yi1nS5Pl3y7HmvGPVAexOlYBLr4UeTrPW3nSoDTn91O2T7hqTuOWryuyt9r3IcEzK9PDwGEsduyYk3GRDRDFOvDNODE2rELMSf3gzXNxs08=',
	'Algo'
);

INSERT INTO Concessions (Dossier, BeginDate, FinishDate, Notes) VALUES
(
	1,
	'Yi1nS5Pl3y7HmvGPVAexOlYBLr4UeTrPW3nSoDTn91MRW8zYr0poFk32FAWUy77rEP8ZWaGUmy1Q8ErwulDpivaKG0KeNBSksFl1obJPoY0=',
	'Yi1nS5Pl3y7HmvGPVAexOmyh0XRHQ6HEQcmfPFArt0UzKpBV7RBzfl+Ze8st1zYr6lSIUaJpMJlKY77I742jS/ZJANqvNunO3bAmXrJIJ78=',
	'Otro'
);

INSERT INTO Concessions (Dossier, BeginDate, FinishDate, Notes) VALUES
(
	2,
	'Yi1nS5Pl3y7HmvGPVAexOnTFkdvwFFpm3XxU5oPOgcv9Eapl8cr+gUUMnHywhHp3jhGHE82UMLroa1EOwbKFG5621vEnUmikqKCYges2AWE=',
	'Yi1nS5Pl3y7HmvGPVAexOmF3s3c/lTwKUAKHVnGjdFEFHesg819hbr5HKfAWtzRDFa+AMMntKPfl9L5wdejTl+BDmoJqBHtzntBgYplr5CQ=',
	'Tercero'
);

INSERT INTO Concessions (Dossier, BeginDate, Notes) VALUES
(
	2,
	'Yi1nS5Pl3y7HmvGPVAexOmF3s3c/lTwKUAKHVnGjdFE+OGS/XwGUn3UW+B/XMXkpkUVwjr8L5x7Z9gF1VFDTw2veNb+lq9Ais4z0tjJwCLI=',
	'NullFinishDateConcession'
);

INSERT INTO Concessions (Dossier, BeginDate, FinishDate, Notes) VALUES
(
	3,
	'Yi1nS5Pl3y7HmvGPVAexOojLJA6nVySU2Ioi5Je+iocJDUZXJbtEzfkLh12VvKMzFQYjxGfXsdypYhvkE/o5Hw122kf2Qr0TYtLpgaC6L/U=',
	'Yi1nS5Pl3y7HmvGPVAexOo1BtOxAp8W43Eua/++FbN2IaNaX7T/RSUY4mwOogVxqmopVj4rSLCj+91EJR6nQwPmz4MEDrE0s9DUaWclquUU=',
	'Un fresco'
);

INSERT INTO Concessions (Dossier, BeginDate, FinishDate, Notes) VALUES
(
	3,
	'Yi1nS5Pl3y7HmvGPVAexOo1BtOxAp8W43Eua/++FbN2IaNaX7T/RSUY4mwOogVxqmopVj4rSLCj+91EJR6nQwPmz4MEDrE0s9DUaWclquUU=',
	'Yi1nS5Pl3y7HmvGPVAexOpV3CNNQsCVdGgLSO/+RoH1Qli2cFtmMM7TRvxQszfCVbc+yjysr/8kdfvujzkmDoYkZrOvBfGy9DgcDGNeVTVI=',
	'Fega'
);

INSERT INTO Concessions (Dossier, BeginDate, FinishDate, Notes) VALUES
(
	4,
	'Yi1nS5Pl3y7HmvGPVAexOojLJA6nVySU2Ioi5Je+iocjb7m5hHvwlNXSGzvoihxLRSocyEw46Csb5XKwajtcGWKll7UdZyULidWJKxEnmCI=',
	'Yi1nS5Pl3y7HmvGPVAexOo1BtOxAp8W43Eua/++FbN0G3tYmE0FipvvHsFHVYtqrykBqb65ilsCi2VWfMvCz//e8j7dpX5sZCmHi2bU3dME=',
	'Fresco'
);

INSERT INTO Concessions (Dossier, BeginDate, FinishDate, Notes) VALUES
(
	4,
	'Yi1nS5Pl3y7HmvGPVAexOo1BtOxAp8W43Eua/++FbN0G3tYmE0FipvvHsFHVYtqrykBqb65ilsCi2VWfMvCz//e8j7dpX5sZCmHi2bU3dME=',
	'Yi1nS5Pl3y7HmvGPVAexOpV3CNNQsCVdGgLSO/+RoH0JtsjRf0ARPEHKwup9Pk12yPXeZYwW6a2UoL5zdgPA1xm2jZKz5M4raUvxMXIVcYw=',
	'Fega'
);

INSERT INTO Fresco (Concession) VALUES
(
	1
);

INSERT INTO Fresco (Concession) VALUES
(
	2
);

INSERT INTO Fresco (Concession) VALUES
(
	5
);

INSERT INTO Fega (Concession,State) VALUES
(
	6,
	'Awaiting'
);

INSERT INTO Fresco (Concession) VALUES
(
	7
);

INSERT INTO Fega (Concession, State) VALUES
(
	8,
	'Awaiting'
);