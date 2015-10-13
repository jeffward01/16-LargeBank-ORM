Create database LargeBank;
go

USE LargeBank;
go

create table Customer
(
	CustomerId int primary key IDENTITY(1,1),
	CreatedDate DateTime NOT NULL,
	FirstName VARCHAR(25) NOT NULL,
	LastName VARCHAR(25) NOT NULL,
	Address1 VARCHAR(50) NOT NULL,
	Address2 VARCHAR(30),
	City VARCHAR(50)NOT NULL,
	States VARCHAR(50)NOT NULL,
	Zip VARCHAR(20) NOT NULL
);


CREATE TABLE Accounts
(
	AccountId int Primary key IDENTITY(1,1),
	CustomerId int FOREIGN KEY REFERENCES Customer(CustomerId), 
	AccountNumber int NOT NULL,
	CreatedDate DATETIME NOT NULL,
	Balance money NOT NULL

);

create table Transactions
(
	TransactionId int PRIMARY KEY IDENTITY(1,1),
	AccountId int FOREIGN KEY REFERENCES Accounts(AccountId),
	TransactionDate DATETIME,
	Amount money
);

CREATE TABLE Statements
(
	StatementID int PRIMARY KEY IDENTITY(1,1),
	AccountID int FOREIGN KEY REFERENCES Accounts(AccountId),
	CreatedDate DATETIME,
	StartDate DATETIME,
	EndDate DATETIME
);

go

INSERT INTO Customer (CreatedDate, FirstName, LastName, Address1, City, States, Zip) VALUES (2013-06-09, 'John', 'Doe', '123 Fake Street', 'San Diego', 'CA', '92101');

INSERT INTO Accounts (CreatedDate, CustomerId, AccountNumber, Balance) VALUES (2013-06-09, 1, '1000', 937.67);

INSERT INTO Transactions (TransactionDate, Amount) VALUES  (2013-06-09, 1000);
INSERT INTO Transactions (TransactionDate, Amount) VALUES (2013-06-09, -55.34);
INSERT INTO Transactions (TransactionDate, Amount) VALUES (2013-06-09, -6.99);

INSERT INTO Statements (StartDate,EndDate) VALUES (2013-06-09, 2013-06-30);
INSERT INTO Statements (StartDate,EndDate) VALUES (22013-07-01, 2013-07-31);

INSERT INTO Customer (CreatedDate, FirstName, LastName, Address1, City, States,  Zip) VALUES (2014-02-01, 'Jane', 'Doe', '124 Fake Street', 'San Diego', 'CA', '92101');

INSERT INTO Accounts (CreatedDate, CustomerId, AccountNumber, Balance) VALUES (2014-02-01, 2, '1002', 1197465.05);

INSERT INTO Transactions (TransactionDate, Amount) VALUES  (2014-02-01, 1200000);
INSERT INTO Transactions (TransactionDate, Amount) VALUES (2014-02-10, -2534.95);

INSERT INTO Statements (StartDate,EndDate) VALUES (2014-02-01, 2014-02-28);

SELECT * FROM Customer;
Select CustomerId FROM Accounts WHERE Balance > 5000;
SELECT * FROM Customer WHERE (SELECT Sum(Balance) FROM Accounts WHERE Accounts.CustomerId = Customer.CustomerId) > 50000;