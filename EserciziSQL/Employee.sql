use sp_creazione;

-- DDL
 
CREATE TABLE Employees (
    EmployeeID INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    Email NVARCHAR(100),
    JobTitle NVARCHAR(50),
    Department NVARCHAR(50),
    StartDate DATE				-- 'YYYY-MM-DD'
);
 
-- DML
INSERT INTO Employees (FirstName, LastName, Email, JobTitle, Department, StartDate) VALUES 
('James', 'Smith', 'james.smith@example.com', 'Software Engineer', 'Engineering', '2019-03-01'),
('Maria', 'Garcia', 'maria.garcia@example.com', 'Project Manager', 'Marketing', '2018-06-15'),
('Robert', 'Johnson', 'robert.johnson@example.com', 'Database Administrator', 'IT', '2017-05-29'),
('Patricia', 'Miller', 'patricia.miller@example.com', 'Product Manager', 'Sales', '2020-02-17'),
('Michael', 'Davis', 'michael.davis@example.com', 'Web Developer', 'Engineering', '2021-08-23'),
('Linda', 'Martinez', 'linda.martinez@example.com', 'Graphic Designer', 'Design', '2016-04-11'),
('Elizabeth', 'Hernandez', 'elizabeth.hernandez@example.com', 'Sales Associate', 'Sales', '2019-07-19'),
('William', 'Brown', 'william.brown@example.com', 'Systems Analyst', 'IT', '2018-09-03'),
('Jennifer', 'Wilson', 'jennifer.wilson@example.com', 'Consultant', 'Customer Service', '2017-12-08'),
('David', 'Anderson', 'david.anderson@example.com', 'Quality Assurance', 'Engineering', '2020-05-01'),
('Susan', 'Thomas', 'susan.thomas@example.com', 'HR Specialist', 'Human Resources', '2018-03-23'),
('Joseph', 'Moore', 'joseph.moore@example.com', 'Finance Analyst', 'Finance', '2019-11-30'),
('Margaret', 'Taylor', 'margaret.taylor@example.com', 'Content Writer', 'Marketing', '2021-01-15'),
('Charles', 'Lee', 'charles.lee@example.com', 'UX Designer', 'Design', '2017-07-09'),
('Christopher', 'Harris', 'christopher.harris@example.com', 'Network Engineer', 'IT', '2018-08-21'),
('Jessica', 'Clark', 'jessica.clark@example.com', 'Social Media Manager', 'Marketing', '2020-06-05'),
('Daniel', 'Lewis', 'daniel.lewis@example.com', 'Business Analyst', 'Business Development', '2019-04-12'),
('Sarah', 'Walker', 'sarah.walker@example.com', 'Recruiter', 'Human Resources', '2021-09-20'),
('Thomas', 'Robinson', 'thomas.robinson@example.com', 'Technical Support', 'Customer Service', '2017-11-13'),
('Nancy', 'Rodriguez', 'nancy.rodriguez@example.com', 'Legal Advisor', 'Legal', '2018-01-29');

--QL
select * from Employees;

--STORE Procedure CREAZIONE
-- la sp è un metodo di QL qui per il corpo non ci sono le parentesi, ma begin e end
-- la sp non è interrogata, come la view, ma è eseguita
-- non è compilata è un espressione primitiva 
-- funziona come js
create procedure GetAllEmployees AS
BEGIN
	--CORPO
	SELECT * from Employees;
END;

EXEC GetAllEmployees;

--LA ELIMINO
DROP PROCEDURE GetAllEmployees;

--ISTRUZIONI DELLA SP
EXEC sp_helptext 'GetAllEmployees';

--UPDATE SP
ALTER PROCEDURE GetAllEmployees AS 
BEGIN
	select * from Employees where Department = 'Legal'
END;

-----------------
-- Voglio una SP che prenda il dipartimento in modo dinamico
create procedure GetEmployeeByDepartment
	@Department nvarchar(50)
AS
BEGIN 
	SELECT * from Employees where Department = @Department
END;

exec GetEmployeeByDepartment @Department = 'Engineering';


-- se definito il parametro dipartimento restituisco tutti gli impiegati afferenti,altrimetni tuyyu
create procedure GetEmployeesBydepartmentOrAll
	@Department NVARCHAR(50) = NULL
AS
BEGIN
	IF @Department IS NULL
		BEGIN
			SELECT * FROM Employees;
		END
	ELSE
		BEGIN
			SELECT * FROM Employees where Department = @Department
		END
END;

exec GetEmployeesBydepartmentOrAll;
exec GetEmployeesBydepartmentOrAll @Department = 'Engineering';






create procedure InsertEmployee
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@Email nvarchar(50),
	@JobTitle nvarchar(50),
	@Department nvarchar(50)
AS
BEGIN
	IF (@Department IS NULL)
		BEGIN
			insert into Employees(FirstName, LastName, Email, JobTitle, Department, StartDate) VALUES
			(@Firstname,@LastName,@Email,@JobTitle,'Stagista',CURRENT_TIMESTAMP);
		END
	ELSE
		BEGIN
			insert into Employees(FirstName, LastName, Email, JobTitle, Department, StartDate) VALUES
			(@Firstname,@LastName,@Email,@JobTitle,@Department,CURRENT_TIMESTAMP);
		END
END;

exec InsertEmployee @Firstname = 'ENRICO',@LastName = 'CUOMO',@Email = 'EEEEEEE',@JobTitle = 'PROGRAMMATRE' ,@Department = 'INFORMATICA';
exec InsertEmployee @Firstname = 'ENRICO',@LastName = 'CUOMO',@Email = 'EEEEEEE',@JobTitle = 'PROGRAMMATRE' ,@Department = NULL;

select * from Employees where FirstName = 'ENRICO';










-- DDL
CREATE TABLE Impiegato (
    id_impiegato INT PRIMARY KEY,
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    Salary DECIMAL(10, 2)				-- 123456769.11
);
 
-- DML
INSERT INTO Impiegato (id_impiegato, FirstName, LastName, Salary) VALUES
(1, 'John', 'Doe', 50000),
(2, 'Jane', 'Smith', 60000),
(3, 'Emily', 'Jones', 70000);



create procedure UpdateSalaryById
	@EmployeeId int,
	@salaryValue decimal(10,2)

AS
BEGIN 
	BEGIN TRY
		UPDATE Impiegato SET
			Salary = @salaryValue
			where EmployeeID = @EmployeeId

		IF (@@ROWCOUNT = 0)
			THROW
	END TRY
	BEGIN CATCH

	END CATCH
END;