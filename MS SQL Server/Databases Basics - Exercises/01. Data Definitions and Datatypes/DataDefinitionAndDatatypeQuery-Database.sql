--Problem 1: Create Database--

CREATE DATABASE Minions

USE Minions

--Problem 2: Create Tables--

CREATE TABLE Minions(
	Id INT PRIMARY KEY NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,
	Age TINYINT
)

CREATE TABLE Towns(
	Id INT PRIMARY KEY NOT NULL,
	[Name] NVARCHAR(50) NOT NULL
)

--Problem 3: Alter Minions Table--

ALTER TABLE Minions
ADD TownId INT FOREIGN KEY REFERENCES Towns(Id)

--Problem 4: Insert Records in Both Tables--

INSERT INTO Towns(Id , [Name])
	 VALUES
			(1, 'Sofia'),
			(2, 'Plovdiv'),
			(3, 'Varna')


INSERT INTO Minions(Id, [Name], Age, TownId)
	 VALUES
			(1, 'Kevin', 22, 1),
			(2, 'Bob', 15, 3),
			(3, 'Steward',NULL, 2)

SELECT * FROM Towns
SELECT * FROM Minions

--Problem 5: Truncate Table Minions--

TRUNCATE TABLE Minions

 --Problem 6: Drop All Tables--

 DROP TABLE Towns

 DROP TABLE Minions

--Problem 7: Create Table People--

CREATE TABLE People(
	Id BIGINT PRIMARY KEY IDENTITY NOT NULL,
	[Name] NVARCHAR(200) NOT NULL,
	Picture VARBINARY(MAX) CHECK(DATALENGTH(Picture) <= 2048 * 1024),
	Height DECIMAL(5,2),
	[Weight] DECIMAL(5,2),
	[Gender] CHAR(1) CHECK(Gender = 'm' OR Gender = 'f'),
	[Birthdate] DATE NOT NULL,
	[Biography] NVARCHAR(MAX)
)

INSERT INTO People(Name,Picture,Height,Weight,Gender,Birthdate,Biography) Values
('Stela',Null,1.65,44.55,'f','2000-09-22',Null),
('Ivan',Null,2.15,95.55,'m','1989-11-02',Null),
('Qvor',Null,1.55,33.00,'m','2010-04-11',Null),
('Karolina',Null,2.15,55.55,'f','2001-11-11',Null),
('Pesho',Null,1.85,90.00,'m','1983-07-22',Null)

SELECT * FROM People

--Problem 8: Create Table Users--

CREATE TABLE Users(
Id BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
Username VARCHAR(30) UNIQUE NOT NULL,
[Password] VARCHAR(26) NOT NULL,
ProfilePicture VARBINARY(MAX),
LastLoginTime DATETIME NOT NULL,
IsDeleted BIT NOT NULL
)

INSERT INTO Users (Username, [Password], ProfilePicture, LastLoginTime, IsDeleted)
VALUES ('Mitko', 'simsun', null, '2010-12-10', 'false'),
('Hristo', 'darac89', null, '2010-12-10', 'true'),
('Marian', 'ppas', null, '2011-12-11', 'false'),
('Pesho', 'a94', null, '2016-12-10', 'true'),
('Rumen', 'd12564', null, '2015-12-18', 'false')

SELECT * FROM Users

USE Minions

--Problem 9: Change Primary Key--

ALTER TABLE Users
DROP CONSTRAINT [PK__Users__3214EC0713EB269B]

ALTER TABLE Users
ADD CONSTRAINT PK_Userts_CompositeIdUsername
PRIMARY KEY(Id, Username)

--Problem 10: Add Check Constraint--

ALTER TABLE Users
ADD CHECK(LEN([Password]) >= 5)

ALTER TABLE Users
ADD CONSTRAINT DF_Users_LastLoginTime
DEFAULT GETDATE() FOR LastLoginTime

--Problem 11: Set Default Value of a Field--

ALTER TABLE Users
ADD CONSTRAINT CK_Users_UsernameLength
CHECK(LEN(Username) >= 3)

--Problem 12: Set Unique Field--

  ALTER TABLE Users
  DROP CONSTRAINT PK_Id_Username

  ALTER TABLE Users
  ADD CONSTRAINT PK_Id PRIMARY KEY (Id)

  ALTER TABLE Users
  ADD UNIQUE (Username)

  ALTER TABLE Users
  ADD CHECK (LEN(Username) >=3)

--Problem 13: Movies Database--

USE master

CREATE DATABASE Movie

USE Movie

CREATE TABLE Directors(
	Id INT PRIMARY KEY IDENTITY NOT NULL,
	DirectorName NVARCHAR(50) NOT NULL,
	Notes NVARCHAR(MAX)
	)

CREATE TABLE Genres(
	Id INT PRIMARY KEY IDENTITY NOT NULL,
	GenreName NVARCHAR(50) NOT NULL,
	Notes NVARCHAR(MAX)
	)

CREATE TABLE Categories(
	Id INT PRIMARY KEY IDENTITY NOT NULL,
	CategoryName NVARCHAR(50) NOT NULL,
	Notes NVARCHAR(MAX)
	)

CREATE TABLE Movies(
	Id INT PRIMARY KEY IDENTITY NOT NULL,
	Title NVARCHAR(50) NOT NULL,
	DirectorId BIGINT NOT NULL,
	CopyrightYear DATETIME2,
	[Length] DECIMAL(10,2),
	GenreId BIGINT,
	CategoryId BIGINT,
	Rating INT,
	Notes NVARCHAR(MAX)
	)

INSERT INTO Directors(DirectorName, Notes)
	 VALUES
			('Pesho', null),
			('Gosho', null),
			('Rasho', null),
			('Misho', null),
			('Grisho', null)

INSERT INTO Genres(GenreName, Notes)
	 VALUES
			('Tiri', null),
			('Miri', null),
			('Biri', null),
			('Kiri', null),
			('Siri', null)

INSERT INTO Categories(CategoryName, Notes)
	 VALUES
			('Comedy', null),
			('Drama', null),
			('Si-fi', null),
			('Music', null),
			('Bougy', null)

INSERT INTO Movies(Title, DirectorId, CopyrightYear, [Length], 
					GenreId, CategoryId, Rating, Notes)
	 VALUES
			('FILM1', 123, GETDATE(), null, null, null, null, null),
			('FILM2', 234, GETDATE(), null, null, null, null, null),
			('FILM3', 345, GETDATE(), null, null, null, null, null),
			('FILM4', 456, GETDATE(), null, null, null, null, null),
			('FILM+', 567, GETDATE(), null, null, null, null, null)

SELECT * FROM Directors
SELECT * FROM Genres
SELECT * FROM Categories

--Problem 14: Car Rental Database

USE master

CREATE DATABASE CarRental

USE CarRental

CREATE TABLE Categories(
 Id INT PRIMARY KEY IDENTITY(1, 1), 
 CategoryName NVARCHAR(50) NOT NULL, 
 DailyRate DECIMAL(7, 2) NOT NULL, 
 WeeklyRate DECIMAL(7, 2), 
 MonthlyRate DECIMAL(7, 2), 
 WeekendRate DECIMAL(7, 2)
 )

 CREATE TABLE Cars(
 Id INT PRIMARY KEY IDENTITY(1, 1), 
 PlateNumber VARCHAR(20) NOT NULL, 
 Manufacturer VARCHAR(50) NOT NULL, 
 Model VARCHAR(50) NOT NULL, 
 CarYear DATE, 
 CategoryId INT FOREIGN KEY REFERENCES Categories(Id) NOT NULL, 
 Doors INT NOT NULL, 
 Picture VARBINARY(2500), 
 Condition VARCHAR(50) NOT NULL, 
 Available VARCHAR(MAX) NOT NULL
 )

 CREATE TABLE Employees(
 Id INT PRIMARY KEY IDENTITY(1, 1), 
 FirstName NVARCHAR(50) NOT NULL, 
 LastName NVARCHAR(50) NOT NULL, 
 Title VARCHAR(50) NOT NULL, 
 Notes NVARCHAR(MAX)
 )

 CREATE TABLE Customers(
 Id INT PRIMARY KEY IDENTITY(1, 1), 
 DriverLicenceNumber INT NOT NULL, 
 FullName NVARCHAR(100) NOT NULL, 
 [Address] NVARCHAR(100), 
 City NVARCHAR(50) NOT NULL, 
 ZIPCode INT, 
 Notes NVARCHAR(MAX)
 )

 CREATE TABLE RentalOrders(
 Id INT PRIMARY KEY IDENTITY(1, 1), 
 EmployeeId INT FOREIGN KEY REFERENCES Employees(Id) NOT NULL, 
 CustomerId INT FOREIGN KEY REFERENCES Customers(Id) NOT NULL, 
 CarId INT FOREIGN KEY REFERENCES Cars(Id) NOT NULL, 
 TankLevel DECIMAL(10, 2) NOT NULL, 
 KilometrageStart DECIMAL(10,2) NOT NULL, 
 KilometrageEnd DECIMAL(10,2) NOT NULL, 
 TotalKilometrage  DECIMAL(10,2) NOT NULL, 
 StartDate DATE NOT NULL, 
 EndDate DATE NOT NULL, 
 TotalDays INT NOT NULL, 
 RateApplied DECIMAL(10,2) NOT NULL, 
 TaxRate DECIMAL(10,2) NOT NULL, 
 OrderStatus BIT NOT NULL, 
 Notes NVARCHAR(MAX)
 )

 INSERT INTO Categories( CategoryName, DailyRate, WeeklyRate, MonthlyRate, WeekendRate) 
 VALUES
 ('Tir', 10.2, 24.4, 20.6, 44.5),
 ('Car', 10.2, 24.4, 20.6, 44.5),
 ('Bus', 10.2, 24.4, 20.6, 44.5)

 INSERT INTO Cars( PlateNumber, Manufacturer, Model, CarYear, CategoryId, Doors, Picture, Condition, Available)
 VALUES
 (1111, 'Opel', 'Astra', '2007', 2, 4, null, 'Perfect', 'Many'),
 (2222, 'Ford', 'Ka', '1996', 2, 4, null, 'Not perfect', 'Many'),
 (3333, 'MB', 'AMG', '2015', 2, 4, null, 'Perfect', 'Many')


 INSERT INTO Employees (FirstName, LastName, Title, Notes)
 VALUES
 ('Milan', 'Semov', 'Boss', null),
 ('Juventus', 'Berov', 'Boss', null),
 ('Palermo', 'Kolev', 'Boss', null)

 INSERT INTO Customers (DriverLicenceNumber, FullName, [Address], City, ZIPCode, Notes) 
 VALUES
 (1234, 'Pavel', 'gk.Lulin', 'Sofia', 1330, null),
 (5432, 'Venchi', 'gk.Mladost', 'Sofia', 1320, null),
 (7856, 'Ivan', 'gk.Gerena', 'Sofia', null, null)

 INSERT INTO RentalOrders (EmployeeId, CustomerId, CarId, TankLevel, KilometrageStart, KilometrageEnd,
  TotalKilometrage, StartDate, EndDate, TotalDays, RateApplied, TaxRate, OrderStatus, Notes)
 VALUES
 (2,3,1, 55.5, 100.5, 200.5, 300.47, '2020-05-20', '2010-03-23', 60, 45.05, 122.06, 1, null),
 (1,2,3, 55.5, 200.5, 300.5, 500.36, '2020-05-20', '2010-03-23', 60, 45.54, 122.06, 0, null),
 (3,2,1, 55.5, 300.5, 400.5, 800.17, '2020-05-20', '2010-03-23', 60, 45.30, 122.06, 1, null)

 --Problem 16: Create Softuni Database--

 use master

 CREATE DATABASE SoftUni

 USE Softuni
 
 CREATE TABLE Towns(
 Id INT PRIMARY KEY IDENTITY(1, 1),
 [Name] NVARCHAR(50) NOT NULL
 )
 
 CREATE TABLE Addresses(
 Id INT PRIMARY KEY IDENTITY(1, 1),
 AddressText NVARCHAR(100) NOT NULL, 
 TownId INT FOREIGN KEY REFERENCES Towns(Id) NOT NULL
 )
 
 CREATE TABLE Departments(
  Id INT PRIMARY KEY IDENTITY(1, 1),
 [Name] NVARCHAR(50) NOT NULL
 )
 
 CREATE TABLE Employees(
 Id INT PRIMARY KEY IDENTITY(1, 1), 
 FirstName  NVARCHAR(50) NOT NULL, 
 MiddleName  NVARCHAR(50), 
 LastName  NVARCHAR(50) NOT NULL, 
 JobTitle  NVARCHAR(50) NOT NULL, 
 DepartmentId INT FOREIGN KEY REFERENCES Departments(Id) NOT NULL, 
 HireDate DATE NOT NULL, 
 Salary DECIMAL (8, 2) NOT NULL, 
 AddressId INT FOREIGN KEY REFERENCES Addresses(Id)
 )

 --Problem 18: Basic Insert--

 INSERT INTO Towns ([Name])
	  VALUES
			('Sofia'),
			('Plovdiv'),
			('Varna'),
			('Burgas')

 INSERT INTO Departments ([Name])
	  VALUES
			('Engineering'),
			('Sales'),
			('Marketing'),
			('Software Development'),
			('Quality Assurance')

 INSERT INTO Employees (FirstName, MiddleName, LastName, JobTitle, DepartmentId, HireDate, Salary)
	  VALUES
			('Ivan', 'Ivanov', 'Ivanov', '.NET Developer', 4, '2013-02-01', 3500.00),
			('Petar', 'Petrov', 'Petrov', 'Senior Engineer', 1, '2004-03-02', 4000.00),
			('Maria', 'Petrova', 'Ivanova', 'Intern', 5, '2016-08-28', 525.25),
			('Georgi', 'Teziev', 'Ivanov', 'CEO', 2, '2007-12-09', 3000.00),
			('Peter', 'Pan', 'Pan', 'Intern', 3, '2016-08-28', 599.88) 

 --Problem 19: Basic Select All Fields--

 SELECT * FROM Towns

 SELECT * FROM Departments

 SELECT * FROM Employees

 --Problem 20: Basic Select Some Fields--
 
 SElECT [Name] FROM Towns
 ORDER BY [Name]

 SELECT [Name] FROM Departments
 ORDER BY [Name]

 SELECT FirstName, LastName, JobTitle, Salary FROM Employees
 ORDER BY Salary DESC

 --Problem 22: Increase Employees Salary--

 UPDATE Employees
 SET Salary = Salary * 1.1
 SELECT Salary FROM Employees
