USE master

USE Gringotts

SELECT * FROM WizzardDeposits

-- Problem 8: Deposit Charge--

SELECT W.DepositGroup, W.MagicWandCreator, MIN(W.DepositCharge) AS MinDepositCharge
	FROM WizzardDeposits AS W
	GROUP BY W.DepositGroup, W.MagicWandCreator
	ORDER BY MagicWandCreator ASC, DepositGroup ASC

-- Problem 9: Age Groups--

SELECT AgeGroup, COUNT(*) FROM (SELECT 
		(CASE 
				WHEN Age <= 10 THEN '[0-10]'
				WHEN Age BETWEEN 11 AND 20 THEN '[11-20]'
				WHEN Age BETWEEN 21 AND 30 THEN '[21-30]'
				WHEN Age BETWEEN 31 AND 40 THEN '[31-40]'
				WHEN Age BETWEEN 41 AND 50 THEN '[41-50]'
				WHEN Age BETWEEN 51 AND 60 THEN '[51-60]'
				ELSE '[61+]'
			END) AS [AgeGroup]
	FROM WizzardDeposits AS W) AS [AgeGroup] 
	GROUP BY AgeGroup
	
-- Problem 10: First Letter--

SELECT * FROM (SELECT SUBSTRING(FirstName, 1, 1) AS FirstLetter FROM WizzardDeposits
               WHERE DepositGroup = 'Troll Chest') AS t
GROUP BY FirstLetter
ORDER BY FirstLetter

-- Problem 11: Average Interest--

SELECT DepositGroup, IsDepositExpired, AVG(DepositInterest) FROM WizzardDeposits
WHERE DepositStartDate > '01/01/1985'
GROUP BY DepositGroup, IsDepositExpired
ORDER BY DepositGroup DESC, IsDepositExpired 

-- Problem 12: Rich Wizard, Poor Wizard--

SELECT SUM(t.diff) AS SumDifference
FROM (
      SELECT (wd2.DepositAmount- wd1.DepositAmount) AS diff 
	  FROM WizzardDeposits AS wd1
      JOIN WizzardDeposits AS wd2 ON wd1.Id = wd2.Id + 1) AS t

-- Problem 13: Departments Total Salaries--

USE SoftUni

SELECT DepartmentID, SUM(Salary) FROM Employees
GROUP BY DepartmentID
ORDER BY DepartmentID

-- Problem 14: Employees Minimum Salaries--

SELECT DepartmentID, MIN(Salary) AS MinimumSalary FROM Employees
	WHERE DepartmentID IN (2,5,7) AND HireDate > '01.01.2000'
	GROUP BY DepartmentID

-- Problem 15: Employees Average Salaries--

SELECT * INTO NewEmployeesWithHighSalaries FROM Employees AS E
WHERE E.Salary > 30000

DELETE FROM NewEmployeesWithHighSalaries
WHERE ManagerID = 42

UPDATE NewEmployeesWithHighSalaries
SET Salary += 5000
WHERE DepartmentID = 1

SELECT DepartmentID, AVG(Salary) AS AverageSalary FROM NewEmployeesWithHighSalaries
GROUP BY DepartmentID

-- Problem 16: Employees Maximum Salaries--

SELECT DepartmentID, MAX(Salary) AS [MaxSalary] FROM Employees 
GROUP BY DepartmentID
HAVING MAX(Salary) NOT BETWEEN 30000 AND 70000