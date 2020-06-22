-- Problem 1: One-to-One Relationship--

CREATE DATABASE PassportDepartment

USE PassportDepartment

CREATE TABLE Passports(
	[PassportID] INT IDENTITY(101,1) PRIMARY KEY,
	PassportNumber CHAR(8) NOT NULL
)

-- Create references between Person(PassportID) and Passport(PassportID)--
--One-to-One Relationship - FK Column -> UNIQUE Constraint--
CREATE TABLE Persons(
	PersonID INT IDENTITY PRIMARY KEY,
	FirstName NVARCHAR(50) NOT NULL,
	Salary DECIMAL(7,2) NOT NULL,
	PassportID INT NOT NULL FOREIGN KEY REFERENCES Passports(PassportID) UNIQUE
)

INSERT INTO Passports(PassportNumber)
	 VALUES
	 ('N34FG21B'),
	 ('K65LO4R7'),
	 ('ZE657QP2')

INSERT INTO Persons(FirstName, Salary, PassportID)
	 VALUES
	 ('Roberto', 43300.00, 102),
	 ('Tom', 56100.00, 103),
	 ('Yana', 60200.00, 101)


SELECT * FROM Persons
SELECT * FROM Passports

-- Problem 2: One-to-Many Relationship--

CREATE DATABASE CarProduction

USE CarProduction

CREATE TABLE Manufacturers(
	ManufacturerID INT PRIMARY KEY,
	[Name] VARCHAR(50) NOT NULL,
	EstablishedOn DATE NOT NULL
)

CREATE TABLE Models(
	ModelID INT PRIMARY KEY,
	[Name] VARCHAR(50) NOT NULL,
	ManufacturerID INT NOT NULL	FOREIGN KEY REFERENCES Manufacturers(ManufacturerID)
)

INSERT INTO Manufacturers(ManufacturerID, [Name], EstablishedOn)
	 VALUES
			(1, 'BMW', '03/07/1916'),
			(2, 'Tesla', '01/01/2003'),
			(3, 'Lada', '01/05/1966')

INSERT INTO Models(ModelID, [Name], ManufacturerID)
	 VALUES
			(101, 'X1', 1),
			(102, 'i6', 1),
			(103, 'Models S', 2),
			(104, 'Models X', 2),
			(105, 'Models 3', 2),
			(106, 'Lada', 3)

SELECT * FROM Models AS mo
JOIN Manufacturers AS ma
ON mo.ManufacturerID = ma.ManufacturerID

-- Problem 3: Many-To-Many Relationship--

CREATE DATABASE School

USE School

CREATE TABLE Students(
	StudentID INT PRIMARY KEY, 
	[Name] NVARCHAR(20) NOT NULL
)

CREATE TABLE Exams(
	ExamID INT PRIMARY KEY, 
	[Name] NVARCHAR(20) NOT NULL
)

CREATE TABLE StudentsExams(
	StudentID INT FOREIGN KEY REFERENCES Students(StudentID), 
	ExamID INT NOT NULL FOREIGN KEY REFERENCES Exams(ExamID),
	--COMPOSITE PRIMARY KEY--
	PRIMARY KEY(StudentID, ExamID)
)
INSERT INTO Students(StudentID, [Name])
	 VALUES
			(1, 'Mila'),
			(2, 'Toni'),
			(3, 'Ron')

INSERT INTO Exams(ExamID, [Name])
	 VALUES
			(101, 'SpringMVC'),
			(102, 'Neo4j'),
			(103, 'Oracle 11g')

INSERT INTO StudentsExams(StudentID, ExamID)
	 VALUES
			(1, 101),
			(1, 102),
			(2, 101),
			(3, 103),
			(2, 102),
			(2, 103)

SELECT * FROM Students
SELECT * FROM Exams
SELECT * FROM StudentsExams

-- Problem 4: Self-Referencing--

CREATE TABLE Teachers(
TeacherID INT PRIMARY KEY, 
[Name] NVARCHAR(50) NOT NULL,
ManagerID INT FOREIGN KEY REFERENCES Teachers(TeacherID)
)

INSERT INTO Teachers(TeacherID, [Name], ManagerID)
	VALUES
			(101, 'John', NULL),
			(102, 'Maya', 106),
			(103, 'Silvia', 106),
			(104, 'Ted', 105),
			(105, 'Mark', 101),
			(106, 'Greta', 101)

-- Problem 5: Online Store Database--

CREATE DATABASE OnlineStore

USE OnlineStore

CREATE TABLE ItemTypes(
	ItemTypeID INT PRIMARY KEY,
	[Name] VARCHAR(50) NOT NULL
)

CREATE TABLE Items(
	ItemID INT PRIMARY KEY,
	[Name] VARCHAR(50) NOT NULL,
	ItemTypeID INT FOREIGN KEY REFERENCES ItemTypes(ItemTypeID) NOT NULL
)

CREATE TABLE Cities(
	CityID INT PRIMARY KEY,
	[Name] VARCHAR(50) NOT NULL
)

CREATE TABLE Customers(
	CustomerID INT PRIMARY KEY,
	[Name] VARCHAR(50) NOT NULL, 
	Birthday DATE NOT NULL,
	CityID INT FOREIGN KEY REFERENCES Cities(CityID)
)
CREATE TABLE Orders(
	OrderID INT PRIMARY KEY,
	CustomerID INT FOREIGN KEY REFERENCES Customers(CustomerID)
)

CREATE TABLE OrderItems(
	OrderID INT NOT NULL FOREIGN KEY REFERENCES Orders(OrderID),
	ItemID INT NOT NULL FOREIGN KEY REFERENCES Items(ItemID),
	PRIMARY KEY(OrderID, ItemID)
)

-- Problem 6: University Database--

CREATE TABLE Majors(
MajorID INT PRIMARY KEY IDENTITY(1, 1),
[Name] VARCHAR(50) NOT NULL
)

CREATE TABLE Students(
StudentID INT PRIMARY KEY IDENTITY(1, 1),
StudentNumber INT NOT NULL,
StudentName VARCHAR(50) NOT NULL,
MajorID INT FOREIGN KEY REFERENCES Majors(MajorID) NOT NULL
)

CREATE TABLE Payments(
PaymentID INT PRIMARY KEY IDENTITY(1, 1),
PaymentDate DATE NOT NULL,
PaymentAmount DECIMAL(8,2) NOT NULL,
StudentID INT FOREIGN KEY REFERENCES Students(StudentID)
)

CREATE TABLE Subjects(
SubjectID INT PRIMARY KEY IDENTITY(1,1),
SubjrectName VARCHAR(50) NOT NULL
)

CREATE TABLE Agenda(
StudentID INT NOT NULL,
SubjectID INT NOT NULL,
CONSTRAINT PK_StudentSubject PRIMARY KEY (StudentID, SubjectID),
CONSTRAINT FK_Student FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
CONSTRAINT FK_Subject FOREIGN KEY (SubjectID) REFERENCES Subjects(SubjectID)
)

-- Problem 9: *Peaks in Rila--

SELECT m.MountainRange, p.PeakName, p.Elevation FROM Peaks AS p
JOIN Mountains AS m ON p.MountainId = m.Id
WHERE m.MountainRange = 'Rila'
ORDER BY p.Elevation DESC