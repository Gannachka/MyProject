CREATE database CourseProgect
USE CourseProgect
GO

CREATE TABLE ADMINISTRATOR ( 
ADMINID INT IDENTITY (1,1) CONSTRAINT ADMIN_PPK PRIMARY KEY)

alter table ADMINISTRATOR add NICK char(30) 


CREATE TABLE PACIENT(
PACIENTID INT IDENTITY (100,1) PRIMARY KEY,
IDADMIN INT FOREIGN KEY REFERENCES ADMINISTRATOR(ADMINID),
BIRTHDAY DATE ,
NAME NVARCHAR(100),
GENDER NVARCHAR(7) NOT NULL CHECK( GENDER='М' OR GENDER='Ж'),
EMAIL NVARCHAR(30),
PHOTO IMAGE )

ALTER TABLE PACIENT ADD  GENDER NVARCHAR(10) NOT NULL CHECK( GENDER='Man' OR GENDER = 'Woman')

CREATE TABLE DOCTOR(
DOCTORID INT IDENTITY(10,1) PRIMARY KEY,
IDADMIN INT FOREIGN KEY REFERENCES ADMINISTRATOR(ADMINID),
NAME NVARCHAR(100),
SPECIALISATION VARCHAR(50),
EXPIRIENCE FLOAT,
ROOM CHAR(10))



CREATE TABLE AUTINTIFICATION (
PASSWORD VARCHAR(60),
LOGIN NVARCHAR(60)CONSTRAINT LOGIN_PK PRIMARY KEY,
IDPACIENT INT  FOREIGN KEY 
				REFERENCES PACIENT(PACIENTID),
IDDOCTOR INT   FOREIGN KEY 
			    REFERENCES DOCTOR(DOCTORID),
IDADMIN INT  FOREIGN KEY 
				REFERENCES ADMINISTRATOR(ADMINID))


CREATE TABLE VISIT (
VISITID INT IDENTITY (1,1),
IDPACIENT INT  CONSTRAINT PACIENTS_FK FOREIGN KEY 
								REFERENCES PACIENT(PACIENTID),
IDDOCTOR INT CONSTRAINT DOCTORS_FK FOREIGN KEY 
								REFERENCES DOCTOR(DOCTORID),
DATE DATE,
TIME TIME,
DIAGNOSIS NVARCHAR(50),
TREATMENT NVARCHAR(100))


select IDDOCTOR, IDPACIENT, IDADMIN FROM AUTINTIFICATION WHERE LOGIN='' AND PASSWORD=''; 


insert into ADMINISTRATOR values ('Admin')
INSERT INTO AUTINTIFICATION (PASSWORD, LOGIN,IDDOCTOR)
VALUES ('LwSmn/J00xBtfnBCjIlgHw==', 'Doctor1',10),
	   ('yEjP9JbUbA9oVYRtMDvFlQ==', 'Doctor2',11), 
	   ('DVuwh4443X9+erK3xIz29w==', 'Doctor3',12),
	   ('yRljxJC8aDl/31d7M6yFXA==', 'Doctor4',13), 
	   ('lt+rJ6KWY0bBqfWxDOGKVg==', 'Doctor5',14), 
	   ('wAvFMiBFVdP0BdlGrypGvw==', 'Doctor6',15), 
	   ('ZO0W6ZE+HUpl/H7Maw/5pg==', 'Doctor7',16), 
	   ('tt2qJ2bq+0lQ74bFTelNjg==', 'Doctor8',17),
	   ('r5njxYbbguTakIU0xn2rLw==', 'Doctor9',18), 
	   ('uopvm4YHdgSfNGaJ+xSdBQ==', 'Doctor10',19), 
	   ('jzbP6hjFnBcManyod3tJWg==', 'Doctor11',20) 
 
 
INSERT INTO DOCTOR (NAME ,IDADMIN, SPECIALISATION,EXPIRIENCE,ROOM )
VALUES('Ардыцкая Татьяна Николаевна', 1,'врач-офтальмолог',10, 100),
	  ('Ардыцкая Наталья Николаевна',1, 'врач-косметолог',2, 101),
	  ('Микляев Сергей Вячеславович',1, 'врач-ортопед',5.5, 102),
	  ('Юхник Роман Юрьевич', 1,'врач-оториларинголок', 5.5, 103),
	  ('Николаенков Николай Николаевисч',1, 'врач-офтальмолог',8, 100),
	  ('Север Александра Сергеевна', 1,'врач-хирург',1.5, 115),
	  ('Витвор Алексей Сергеевич', 1,'врач-хирург',7, 110),
	  ('Качан Александр Дмитриевич',1, 'врач-дерматолог',6, 112),
	  ('Мощинская Полина Юрьевна',1, 'врач-невролог',20,113),
	  ('Коляда Алеся Степановна',1, 'врач-кардиолог',40,114),
	  ('Антипова Александра Александровна',1, 'врач-ортопед',1, 111)


	  
