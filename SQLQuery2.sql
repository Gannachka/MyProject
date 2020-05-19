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
GENDER NVARCHAR(7) NOT NULL CHECK( GENDER='�' OR GENDER='�'),
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
VALUES('�������� ������� ����������', 1,'����-�����������',10, 100),
	  ('�������� ������� ����������',1, '����-����������',2, 101),
	  ('������� ������ ������������',1, '����-�������',5.5, 102),
	  ('����� ����� �������', 1,'����-���������������', 5.5, 103),
	  ('����������� ������� �����������',1, '����-�����������',8, 100),
	  ('����� ���������� ���������', 1,'����-������',1.5, 115),
	  ('������ ������� ���������', 1,'����-������',7, 110),
	  ('����� ��������� ����������',1, '����-����������',6, 112),
	  ('��������� ������ �������',1, '����-��������',20,113),
	  ('������ ����� ����������',1, '����-���������',40,114),
	  ('�������� ���������� �������������',1, '����-�������',1, 111)


	  
