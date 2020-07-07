-- Section 1. DDL(30 pts)--

CREATE DATABASE Airport

USE Airport

CREATE TABLE Planes (
	[Id] INT IDENTITY PRIMARY KEY,
	[Name] NVARCHAR(30) NOT NULL,
	[Seats] INT NOT NULL,
	[Range] INT NOT NULL
);

CREATE TABLE Flights (
	[Id] INT IDENTITY PRIMARY KEY,
	[DepartureTime] DATETIME,
	[ArrivalTime] DATETIME,
	[Origin] NVARCHAR(50) NOT NULL,
	[Destination] NVARCHAR(50) NOT NULL,
	[PlaneId] INT FOREIGN KEY REFERENCES Planes(Id) NOT NULL
);

CREATE TABLE Passengers (
	[Id] INT IDENTITY PRIMARY KEY,
	[FirstName] NVARCHAR(30) NOT NULL,
	[LastName] NVARCHAR(30) NOT NULL,
	[Age] INT NOT NULL,
	[Address] NVARCHAR(30) NOT NULL,
	[PassportId] CHAR(11) NOT NULL
);

CREATE TABLE LuggageTypes (
	[Id] INT IDENTITY PRIMARY KEY,
	[Type] NVARCHAR(30) NOT NULL,
);

CREATE TABLE Luggages (
	[Id] INT IDENTITY PRIMARY KEY,
	[LuggageTypeId] INT FOREIGN KEY REFERENCES LuggageTypes([Id]) NOT NULL,
	[PassengerId] INT FOREIGN KEY REFERENCES Passengers([Id]) NOT NULL

);

CREATE TABLE Tickets (
	[Id] INT IDENTITY PRIMARY KEY,
	[PassengerId] INT FOREIGN KEY REFERENCES Passengers([Id]) NOT NULL,
	[FlightId] INT FOREIGN KEY REFERENCES Flights([Id]) NOT NULL,
	[LuggageId] INT FOREIGN KEY REFERENCES Luggages([Id]) NOT NULL,
	[Price] DECIMAL(10,2) NOT NULL
);

-- Section 2. DML (10 pts)--

-- Insert--

INSERT INTO Planes ([Name], [Seats], [Range])
	VALUES
			('Airbus 336', 112, 5132),
			('Airbus 330', 432, 5325),
			('Boeing 369', 231, 2355),
			('Stelt 297', 254, 2143),
			('Boeing 338', 165, 5111),
			('Airbus 558', 387, 1342),
			('Boeing 128', 345, 5541);

INSERT INTO LuggageTypes ([Type])
	VALUES
			('Crossbody Bag'),
			('School Backpack'),
			('Shoulder Bag');

--NOT IN THE TASK--
INSERT INTO Flights([DepartureTime], [ArrivalTime], [Origin], [Destination], [PlaneId])
	VALUES
			('01-01-2020', '05-05-2020', 'Groosie', 'Melburn', 6),
			('02-02-2020', '06-06-2020', 'Groosie', 'Carlsbad', 41)


SELECT Id FROM Flights
WHERE Destination = 'Carlsbad'

-- 3. Update--

UPDATE Tickets
SET Price += Price * 0.13
WHERE FlightId IN (
		SELECT Id FROM Flights
		WHERE Destination = 'Carlsbad'
		)

-- 4. Delate--

SELECT Id FROM Flights
WHERE Destination = 'Ayn Halagim'

DELETE FROM Tickets
WHERE FlightId IN (
	SELECT Id FROM Flights
	WHERE Destination = 'Ayn Halagim'
	)

DELETE FROM Flights
WHERE Destination = 'Ayn Halagim'

-- Section 3. Querying(40 pts)--

-- The "Tr" Planes--

SELECT * FROM Planes
WHERE [Name] LIKE '%tr%'
ORDER BY Id ASC, [Name] ASC, [Range] ASC

-- 6. Flight Profits--

SELECT T.FlightId, SUM(T.Price) AS [Price] FROM Flights AS F
LEFT JOIN Tickets AS T
ON F.Id = T.FlightId
GROUP BY T.FlightId
ORDER BY [Price] DESC, FlightId ASC

-- 7. Passanger Trips--

SELECT (P.FirstName + ' ' + P.LastName) AS [Full Name], F.Origin, F.Destination 
	FROM Passengers AS P
	INNER JOIN Tickets AS T
	ON P.Id = T.PassengerId
	INNER JOIN Flights AS F
	ON T.FlightId = F.Id
	ORDER BY [Full Name] ASC, Origin ASC, Destination DESC

-- 8. Non Adventures People--
SELECT * FROM Passengers AS P
LEFT JOIN Tickets AS T
ON P.Id = T.PassengerId

SELECT P.FirstName, P.LastName, P.Age FROM [Passengers] AS P
LEFT JOIN Tickets AS T
ON P.Id = T.PassengerId
WHERE T.Id IS NULL
ORDER BY Age DESC, FirstName ASC, LastName ASC

-- 9. Full Info--

SELECT (P.FirstName + ' ' + P.LastName) AS [Full Name],
		PL.[Name] AS [Plane Name],
		(F.Origin + ' - ' + F.Destination) AS [Trip],
		LT.[Type] AS [Luggage Type]
	FROM Passengers AS P
	INNER JOIN Tickets AS T
	ON T.PassengerId = P.Id
	INNER JOIN Flights AS F
	ON T.FlightId = F.Id
	INNER JOIN Planes AS PL
	ON F.PlaneId = PL.Id
	INNER JOIN Luggages AS L
	ON T.LuggageId = L.Id
	INNER JOIN LuggageTypes AS LT
	ON L.LuggageTypeId = LT.Id
	ORDER BY [Full Name] ASC, [Name] ASC, [Origin] ASC, [Destination] ASC, [Luggage Type] ASC

-- 10. PSP
SELECT * FROM Planes

SELECT P.[Name], P.Seats, COUNT(T.PassengerId) AS [Passangers Count]
	FROM Planes AS P
	LEFT OUTER JOIN Flights AS F
	ON P.Id = F.PlaneId
	LEFT OUTER JOIN Tickets AS T
	ON F.Id = T.FlightId
	GROUP BY P.[Name], P.Seats
	ORDER BY [Passangers Count] DESC, [Name] ASC, Seats ASC

-- 11. Vacation--

CREATE FUNCTION udf_CalculateTickets (@origin VARCHAR(50),
									 @destination VARCHAR(50),
									 @peopleCount INT)
RETURNS NVARCHAR(MAX)
AS
BEGIN
	DECLARE @VAR1 NVARCHAR(20)
	IF(@peopleCount <= 0)
		RETURN 'Invalid people count!'
	DECLARE @FlightID INT = 0;
		SELECT @FlightID = Id FROM Flights
		WHERE Origin = @origin AND Destination = @destination

	IF @FlightID = 0
		RETURN  'Invalid flight!'

	DECLARE @Price DECIMAL(15,2) 
	SELECT @Price = Price FROM Tickets 
	WHERE FlightId = @FlightID
	
	DECLARE @TotalPrice DECIMAL(15,2)  = @Price * @peopleCount;

	RETURN 'Total price ' + CAST(@TotalPrice AS VARCHAR)
END

-- 12. Wrong Data--
SELECT DepartureTime, ArrivalTime, DATEDIFF(SECOND, DepartureTime, ArrivalTime)
		FROM Flights

CREATE PROCEDURE usp_CancelFlights
AS
BEGIN
	UPDATE Flights
	SET DepartureTime = NULL, ArrivalTime = NULL
	WHERE DATEDIFF(SECOND, ArrivalTime, DepartureTime) < 0
END

EXEC usp_CancelFlights