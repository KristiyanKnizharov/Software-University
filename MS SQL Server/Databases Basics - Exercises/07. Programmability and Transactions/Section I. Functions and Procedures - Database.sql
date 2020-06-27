-- Problem 1: Employees with Salary Above 35000--

USE softuni

GO

CREATE PROCEDURE usp_GetEmployeesSalaryAbove35000
AS
BEGIN
	SELECT FirstName, LastName FROM Employees
	WHERE Salary > 35000
END

EXEC usp_GetEmployeesSalaryAbove35000

-- Problem 2: Employees with Salary Above Number--

GO

CREATE PROCEDURE usp_GetEmployeesSalaryAboveNumber(@VAR1 DECIMAL(18,4))
AS
BEGIN
	SELECT FirstName, LastName FROM Employees
	WHERE Salary >= @VAR1
END

EXEC usp_GetEmployeesSalaryAboveNumber 51321

-- Problem 3: Town Names Starting With--

GO

CREATE PROCEDURE usp_GetTownsStartingWith(@var2 VARCHAR(50))
AS
BEGIN
	SELECT [Name] FROM Towns
	WHERE SUBSTRING(Name, 1, 1) = @var2
END

EXEC usp_GetTownsStartingWith b

-- Problem 4: Employees from Town--

GO

CREATE PROCEDURE usp_GetEmployeesFromTown(@townName VARCHAR(50))
AS
BEGIN
	SELECT E.FirstName, E.LastName FROM Employees AS E
	INNER JOIN Addresses AS A
	ON E.AddressID = A.AddressID
	INNER JOIN Towns AS T
	ON A.TownID = T.TownID
	WHERE T.Name = @townName
END

EXEC usp_GetEmployeesFromTown Sofia

-- Problem 5: Salary Level Function--

GO

CREATE FUNCTION ufn_GetSalaryLevel(@salary DECIMAL(18,4))
RETURNS VARCHAR(7)
AS
BEGIN
	DECLARE @salaryLevel VARCHAR(7);
	IF (@salary < 30000)
	BEGIN
		SET @salaryLevel = 'Low';
	END
	ELSE IF (@salary >= 30000 AND @salary <= 50000)
	BEGIN
		SET @salaryLevel = 'Average';
	END
	ELSE 
	BEGIN
		SET @salaryLevel = 'High';
	END

	RETURN @salaryLevel;
END

SELECT FirstName,
		LastName,
		dbo.ufn_GetSalaryLevel(Salary) AS [Salary Level]
FROM Employees

-- Problem 6: Employees by Salary Level--

GO

CREATE PROCEDURE usp_EmployeesBySalaryLevel(@levelOfSalary VARCHAR(7))
AS 
BEGIN 
	SELECT E.FirstName, E.LastName FROM Employees AS E
	WHERE dbo.ufn_GetSalaryLevel(Salary) = @levelOfSalary
END

EXEC usp_EmployeesBySalaryLevel 'High'

-- Problem 7: Define Function--

GO

CREATE FUNCTION ufn_IsWordComprised(@setOfLetters VARCHAR(100), @word VARCHAR(100))
RETURNS BIT
AS
BEGIN
	DECLARE @count TINYINT = 1;
	DECLARE @lenWord INT = LEN(@word);

	WHILE(@count <= @lenWord)
		BEGIN
			DECLARE @CurentChar VARCHAR(1)= SUBSTRING(@word,@Count ,1)
			DECLARE @Result INT =  CHARINDEX(@CurentChar,@setOfLetters)

			SET @Count += 1

			IF @Result = 0 
				RETURN 0
		END
	RETURN 1
END

-- Problem 8: Delete Employees and Departments--

CREATE PROCEDURE usp_DeleteEmployeesFromDepartment (@departmentId INT) 
AS
	DELETE FROM EmployeesProjects
	WHERE EmployeeID IN ( (SELECT EmployeeID FROM Employees
	                       WHERE DepartmentID = @departmentId))

	ALTER TABLE Departments
	ALTER COLUMN ManagerID INT NULL

	UPDATE Employees
	SET ManagerID = NULL
	WHERE ManagerID IN (SELECT EmployeeID FROM Employees
                        WHERE DepartmentID = @departmentId);

	UPDATE Departments
	SET ManagerID = NULL
	WHERE DepartmentID = @departmentId
	
	DELETE FROM Employees
	WHERE DepartmentID = @departmentId

	DELETE FROM Departments
	WHERE DepartmentID = @departmentId

	SELECT COUNT(*) FROM Employees
	WHERE DepartmentID = @departmentId

GO

-- Problem 9: Find Full Name--

USE Bank

CREATE PROCEDURE usp_GetHoldersFullName 
AS
	SELECT CONCAT(FirstName,' ',LastName) AS [Fuul Name]
	FROM AccountHolders

GO

-- Problem 10: People with Balance Higher Than--

CREATE PROCEDURE usp_GetHoldersWithBalanceHigherThan
				(@Money MONEY) 
AS
	SELECT AccountHolderId, SUM(Balance) AS SumBalance
	INTO #GroupAccount
    FROM Accounts 
	GROUP BY  AccountHolderId
	
	
	SELECT ah.FirstName, ah.LastName FROM AccountHolders AS ah
	JOIN #GroupAccount AS a ON ah.Id = a.AccountHolderId
	WHERE SumBalance > @Money 
	ORDER BY ah.FirstName, ah.LastName

GO

-- Problem 11: Fature Value Function--

CREATE FUNCTION ufn_CalculateFutureValue 
               (@sum DECIMAL (15, 4),
				@yearlyInterestRate FLOAT,
				@numberOfYears int)
RETURNS DECIMAL(15,4)
AS
BEGIN
	DECLARE @Result DECIMAL(15,4);

	SET @Result = @sum * POWER((1 + @yearlyInterestRate), @numberOfYears); 

	RETURN @Result;
END;

-- Problem 12: Calculating Interest--

CREATE PROCEDURE usp_CalculateFutureValueForAccount (@Id INT, @InterestRate FLOAT) 
AS
	SELECT 
		ah.Id AS [Account Id],
		ah.FirstName AS [First Name],
		ah.LastName AS [Last Name],
		a.Balance AS [Current Balance],
		dbo.ufn_CalculateFutureValue (a.Balance, @InterestRate, 5) AS [Balance in 5 years]
	FROM AccountHolders AS ah
	JOIN Accounts AS a ON a.Id = ah.Id
	WHERE ah.Id = @Id

GO