Create Table Employee(
employee_id  int identity primary key not null,
Department_id  int Foreign key references Department(Department_id) ,
Firstname varchar(50),
Lastname varchar(50),
Empcode varchar(50),
Dob Datetime,
City varchar (50),
Is_active bit DEFAULT 1,
Created_at Datetime Default GETDATE(),
Updated_at Datetime
)

Select * from EMPLOYEE

Create table Department(
Department_id int identity primary key not null,
Department_name varchar(100)
)

Insert into Department values('Accounts & Finance'),
('HR Department'),
('Sales & Marketing'),
('Research Development'),
('IT Services')

SELECT * FROM DEPARTMENT
Drop Table Department
Drop  Table Employee





SELECT E.Firstname, E.Lastname, E.Empcode, E.Dob, E.City, E.Is_active, E.Created_at, E.Updated_at, D.Department_name 
FROM EMPLOYEE E JOIN DEPARTMENT D ON E.Department_id=D.Department_id