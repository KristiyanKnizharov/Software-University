USE master

CREATE DATABASE TripService

USE TripService

CREATE TABLE Cities(
	Id INT IDENTITY PRIMARY KEY,
	[Name] NVARCHAR(20) NOT NULL,
	[CountryCode] CHAR(2) NOT NULL
)

CREATE TABLE Hotels(
	Id INT IDENTITY PRIMARY KEY,
	[Name] NVARCHAR(30) NOT NULL,
	[CityId] INT REFERENCES Cities(Id) NOT NULL,
	[EmployeeCount] INT NOT NULL,
	[BaseRate] DECIMAL(10,2)
)

CREATE TABLE Rooms(
	Id INT IDENTITY PRIMARY KEY,
	[Price] DECIMAL(15,2) NOT NULL,
	[Type] NVARCHAR(20) NOT NULL,
	[Beds] INT NOT NULL,
	[HotelId] INT REFERENCES Hotels(Id) NOT NULL
)

CREATE TABLE Trips(
	Id INT IDENTITY PRIMARY KEY,
	RoomId INT REFERENCES Rooms(Id) NOT NULL,
	BookDate DATE NOT NULL,
	ArrivalDate DATE NOT NULL,
	ReturnDate DATE NOT NULL,
	CancelDate DATE
)

ALTER TABLE Trips
ADD CHECK(BookDate < ArrivalDate)

ALTER TABLE Trips
ADD CHECK(ArrivalDate < ReturnDate)

CREATE TABLE Accounts(
	Id INT IDENTITY PRIMARY KEY,
	FirstName NVARCHAR(50) NOT NULL,
	MiddleName NVARCHAR(20),
	LastName NVARCHAR(50) NOT NULL,
	CityId INT REFERENCES Cities(Id) NOT NULL,
	BirthDate DATE NOT NULL,
	Email NVARCHAR(100) NOT NULL UNIQUE
)

CREATE TABLE AccountsTrips(
	AccountId INT REFERENCES Accounts(Id) NOT NULL,
	TripId INT REFERENCES Trips(Id) NOT NULL,
	Luggage INT CHECK(Luggage >= 0) NOT NULL
)
-- Section 2. DML(10 pts)--

INSERT INTO Accounts ( FirstName, MiddleName, LastName, CityId, BirthDate, Email)
VALUES
	('John', 'Smith', 'Smith', 34, '1975-07-21', 'j_smith@gmail.com'),
	('Gosho', NULL, 'Petrov', 11, '1978-05-16', 'g_petrov@gmail.com'),
	('Ivan', 'Petrovich', 'Pavlov', 59, '1849-09-26', 'i_pavlov@softuni.bg'),
	('Friedrich', 'Wilhelm', 'Nietzsche', 2, '1844-10-15', 'f_nietzsche@softuni.bg')

INSERT INTO Trips ( RoomId, BookDate, ArrivalDate, ReturnDate, CancelDate)
VALUES
	(101, '2015-04-12', '2015-04-14', '2015-04-20', '2015-02-02'),
	(102, '2015-07-07', '2015-07-15', '2015-07-22', '2015-04-29'),
	(103, '2013-07-17', '2013-07-23', '2013-07-24', NULL),
	(104, '2012-03-17', '2012-03-31', '2012-04-01', '2012-01-10'),
	(109, '2017-08-17', '2017-08-28', '2017-08-29', NULL)

-- 3. Update--
SELECT * FROM Rooms

UPDATE Rooms
SET Price *= 1.14
WHERE HotelId = 5 OR HotelId = 7 OR HotelId = 9

-- 4. Delate--
SELECT * FROM AccountsTrips

DELETE FROM AccountsTrips
	WHERE AccountId IN(SELECT Id FROM Accounts
                    WHERE ID = 47)

DELETE FROM Accounts
WHERE Id = 47 

-- 5. ЕЕЕ-Mails--

SELECT FirstName, LastName, FORMAT(BirthDate, 'MM-dd-yyyy') AS BirthDate, C.[Name] AS HomeTown, Email 
	FROM Accounts AS A
	JOIN Cities AS C
	ON C.Id = A.CityId
	WHERE Email LIKE 'e%'
	ORDER BY C.[Name] ASC
	
-- 6. City Statistics--
SELECT * FROM Cities

SELECT C.[Name], COUNT(*) AS Hotel FROM Cities AS C
	JOIN Hotels AS H
	ON C.Id = H.CityId
	GROUP BY C.[Name]
	ORDER BY COUNT(*) DESC, C.[Name]

-- 7. Longest and Shortest Trips--
SELECT * FROM Trips



SELECT T1.AccountId, T1.FullName, T1.LongestTrip FROM (SELECT A.Id AS [AccountId], (A.FirstName + ' ' + A.LastName) AS [FullName],
		DATEDIFF(DAY, ArrivalDate, ReturnDate) AS [LongestTrip]
	FROM Trips AS T
	JOIN AccountsTrips AS ACT
	ON T.Id = ACT.TripId
	JOIN Accounts AS A
	ON A.Id = ACT.AccountId
	WHERE T.CancelDate IS NOT NULL
	GROUP BY A.Id, (A.FirstName + ' ' + A.LastName),
	DATEDIFF(DAY, ArrivalDate, ReturnDate)
	ORDER BY [LongestTrip] DESC) AS T1


-- 8. Metropolis--

SELECT TOP(10) C.Id, C.[Name], C.CountryCode, COUNT(*) AS [Accounts]
	FROM Cities AS C
	JOIN Accounts AS A
	ON C.Id = A.CityId
	GROUP BY C.ID, C.[Name], C.CountryCode
	ORDER BY [Accounts] DESC
	
-- 9. Romantic Getaways--

-- 10. GDPR Violation--

SELECT GDPR.ID, GDPR.[FullName], C.[Name] AS [From], GDPR.[To], GDPR.Duration FROM (
	SELECT T.Id, CASE 
				 WHEN A.MiddleName IS NULL 
				 THEN (A.FirstName + ' ' + A.LastName)
				 ELSE (A.FirstName + ' ' + A.MiddleName + ' ' + A.LastName)
			 END AS [FullName], C.[Name] AS [To], 
			 CASE
				WHEN T.CancelDate IS NOT NULL
					THEN 'Canceled'
				ELSE CONCAT(DATEDIFF(DAY, T.ArrivalDate, T.ReturnDate), ' ', 'days')
			 END AS [Duration], A.[CityId]
	FROM Accounts AS A
	INNER JOIN AccountsTrips as ACT
	ON ACT.AccountId = A.Id
	INNER JOIN Trips AS T
	ON ACT.TripId = T.Id
	INNER JOIN Rooms AS R
	ON R.Id = T.RoomId
	INNER JOIN Hotels AS H
	ON H.Id = R.HotelId
	INNER JOIN Cities AS C
	ON H.CityId = C.Id
) AS GDPR
INNER JOIN Cities AS C
ON GDPR.CityId = C.Id
ORDER BY [FullName] ASC, GDPR.Id ASC
 

-- Section 4. Programmability (14 pts)--

-- 11. Available Room--

CREATE FUNCTION udf_GetAvailableRoom(@HotelId INT, @Date DATE, @People INT)
RETURNS VARCHAR(100)
AS
BEGIN
	DECLARE @TotalPriceOfTheRoom INT = ;
	DECLARE @Name VARCHAR(50);
	 
		SELECT @Count = COUNT(*), @Name = s.FirstName FROM StudentsExams AS se
		JOIN Students AS s ON se.StudentId = s.Id
		WHERE se.StudentId = @studentId AND Grade BETWEEN @grade AND @grade + 0.5
		GROUP BY s.FirstName

	IF @grade > 6.00
		RETURN 'Grade cannot be above 6.00!'
	ELSE IF @Count = 0
		RETURN 'The student with provided id does not exist in the school!'
	ELSE
		RETURN 'You have to update '+ CONVERT(VARCHAR, @Count ) + ' grades for the student ' + @Name
	RETURN''
END