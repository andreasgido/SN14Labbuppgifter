USE master
GO

IF NOT EXISTS (SELECT name FROM sys.databases
WHERE name = 'Library')	

CREATE DATABASE Library
GO

USE Library
GO

IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'dbo.Authors')
AND	type in(N'U'))
BEGIN	
	CREATE TABLE Authors
	(
		AuthorID int NOT NULL IDENTITY (1,1),
		FirstName varchar(50) NOT NULL,
		LastName varchar(50) NOT NULL,
		BirthDate datetime NULL,
		Deceased datetime NULL,
		[Language] varchar(50) NULL,
		Nationality varchar(50) NULL,

		CONSTRAINT PK_Authors_AuthorID PRIMARY KEY (AuthorID)
	);	
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'dbo.Book')
AND	type in(N'U'))
BEGIN
	CREATE TABLE Book
	(
		BookID int NOT NULL IDENTITY (1,1),
		AuthorID int NOT NULL,
		ISBN varchar(15) NOT NULL,
		Title varchar(100) NOT NULL,
		Published datetime NULL,
		Genre varchar(50) NULL,
		[Language] varchar(50) Null,

		CONSTRAINT PK_Book_BookID PRIMARY KEY (BookID),
		CONSTRAINT FK_Book_Authors_AuthorID FOREIGN KEY (AuthorID) REFERENCES Authors(AuthorID)
	);
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'dbo.CopyStatus')
AND	type in(N'U'))
BEGIN
	CREATE TABLE CopyStatus
	(
		ID int NOT NULL IDENTITY (1,1),
		Name varchar(50) NOT NULL,

		CONSTRAINT PK_CopyStatus_ID PRIMARY KEY (ID)		
	); 
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'dbo.CopyOfBook')
AND	type in(N'U'))
BEGIN
	CREATE TABLE CopyOfBook
	(
		CopyOfBookID int NOT NULL IDENTITY (1,1),
		BookID int NOT NULL,
		PurchasePrice smallmoney NULL,
		PurchaseYear varchar(15) NULL,
		StatusID int NOT NULL
			CONSTRAINT DF_StatusOfBook DEFAULT 1,

		CONSTRAINT PK_CopyOFBook_CopyOfBookID PRIMARY KEY (CopyOfBookID),
		CONSTRAINT FK_CopyOfBook_Book_BookID FOREIGN KEY (BookID) REFERENCES Book(BookID),
		CONSTRAINT FK_CopyOfBook_CopyStatus_ID FOREIGN KEY (StatusID) REFERENCES CopyStatus(ID)
	);
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'dbo.Customers')
AND	type in(N'U'))
BEGIN
	CREATE TABLE Customers
	(
		CustomerID int NOT NULL IDENTITY (1,1),
		FirstName varchar(50) NOT NULL,
		LastName varchar(50) NOT NULL,
		[Address] varchar(50) NULL,
		PhoneNumber varchar(15) NOT NULL,
		EmailAddress varchar(100) NOT NULL
			CONSTRAINT AK_Customers_EmailAddress UNIQUE,
			CONSTRAINT CK_Valid_Email
				CHECK(EmailAddress LIKE '%_@__%.__%'),
		Gender varchar(10) NULL,
		BirthDate datetime NULL,
		EntitledToLoan bit NOT NULL 
			CONSTRAINT DF_EntitledToLoan DEFAULT 1,
			
		CONSTRAINT PK_Customers_CustomersID PRIMARY KEY (CustomerID)  
	);
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects
WHERE object_id = OBJECT_ID(N'dbo.LoanOfBook')
AND	type in(N'U'))
BEGIN
	CREATE TABLE LoanOfBook
	(
		LoanID int NOT NULL IDENTITY (1,1),
		CopyOfBookID int NOT NULL,
		CustomerID int NOT NULL,
		DateOfLoan datetime NOT NULL
			CONSTRAINT DF_DateOfLoan_CurrentDate DEFAULT GETDATE(),
		DateOfReturn AS DATEADD(DAY, 14, DateOfLoan),
		LoanCompleted bit NOT NULL
			CONSTRAINT DF_LoanCompleted_BookIsReturned DEFAULT 0,

		CONSTRAINT PK_LoanOfBook_LoanID PRIMARY KEY (LoanID),
		CONSTRAINT FK_LoanOfBook_CopyOfBook_CopyOfBookID FOREIGN KEY (CopyOfBookID) REFERENCES CopyOfBook(CopyOfBookID),
		CONSTRAINT FK_LoanOfBook_Customers_CustomerID FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
	);
END
GO

-- Kollar om tabellen Authors(Författare) är tom. Om den är det så fyll på med lite data i tabellerna att jobba med.....
IF NOT EXISTS (SELECT * FROM dbo.Authors) 
BEGIN

-- Insert in Authors
	INSERT INTO Authors(FirstName, LastName) VALUES('Adam','Freeman')
	INSERT INTO Authors(FirstName, LastName) VALUES('Christian','Nigel')
	INSERT INTO Authors(FirstName, LastName) VALUES('John','Sharp')
	INSERT INTO Authors(FirstName, LastName) VALUES('Dean','Leffingwell')
	INSERT INTO Authors(FirstName, LastName) VALUES('Krister','Trangius')


-- Insert in Book
	INSERT INTO Book(AuthorID, ISBN, Title, Published) VALUES(1, '9781430265290', 'PRO ASP.NET MVC5', '01/01/2013')
	INSERT INTO Book(AuthorID, ISBN, Title, Published) VALUES(2, '9781118314425', 'Professional C# 2012 and .NET 4.5', '05/06/2013')
	INSERT INTO Book(AuthorID, ISBN, Title, Published) VALUES(3, '9780735681835', 'Microsoft Visual C# 2013', '01/01/2014')
	INSERT INTO Book(AuthorID, ISBN, Title, Published) VALUES(4, '9789152323540', 'Agil projektledning ANDRA UPPLAGAN', '01/09/2013')
	INSERT INTO Book(AuthorID, ISBN, Title, Published) VALUES(5, '9789173791717', 'Programmering 1 C# LÄROBOK', '03/05/2012')


-- Insert in CopyStatus

INSERT INTO CopyStatus(Name) VALUES('Available'),('Onloan'),('Delayed')


--Insert in CopOfBook

	INSERT INTO CopyOfBook(BookID, PurchasePrice, PurchaseYear, StatusID) VALUES(1, 485, '2014', '1')
	INSERT INTO CopyOfBook(BookID, PurchasePrice, PurchaseYear, StatusID) VALUES(1, 485, '2014', '2')
	INSERT INTO CopyOfBook(BookID, PurchasePrice, PurchaseYear, StatusID) VALUES(1, 485, '2014', '1')
	INSERT INTO CopyOfBook(BookID, PurchasePrice, PurchaseYear, StatusID) VALUES(1, 485, '2014', '3')

	INSERT INTO CopyOfBook(BookID, PurchasePrice, PurchaseYear, StatusID) VALUES(2, 595, '2014', '1')
	INSERT INTO CopyOfBook(BookID, PurchasePrice, PurchaseYear, StatusID) VALUES(2, 595, '2014', '1')
	INSERT INTO CopyOfBook(BookID, PurchasePrice, PurchaseYear, StatusID) VALUES(2, 595, '2014', '2')
	INSERT INTO CopyOfBook(BookID, PurchasePrice, PurchaseYear, StatusID) VALUES(2, 595, '2014', '2')

	INSERT INTO CopyOfBook(BookID, PurchasePrice, PurchaseYear, StatusID) VALUES(3, 355, '2015', '1')
	INSERT INTO CopyOfBook(BookID, PurchasePrice, PurchaseYear, StatusID) VALUES(3, 355, '2015', '1')
	INSERT INTO CopyOfBook(BookID, PurchasePrice, PurchaseYear, StatusID) VALUES(3, 355, '2015', '1')
	INSERT INTO CopyOfBook(BookID, PurchasePrice, PurchaseYear, StatusID) VALUES(3, 355, '2015', '1')

	INSERT INTO CopyOfBook(BookID, PurchasePrice, PurchaseYear, StatusID) VALUES(4, 299, '2014', '2')
	INSERT INTO CopyOfBook(BookID, PurchasePrice, PurchaseYear, StatusID) VALUES(4, 299, '2014', '2')
	INSERT INTO CopyOfBook(BookID, PurchasePrice, PurchaseYear, StatusID) VALUES(4, 299, '2014', '1')
	INSERT INTO CopyOfBook(BookID, PurchasePrice, PurchaseYear, StatusID) VALUES(4, 299, '2014', '1')

	INSERT INTO CopyOfBook(BookID, PurchasePrice, PurchaseYear, StatusID) VALUES(5, 325, '2103', '1')
	INSERT INTO CopyOfBook(BookID, PurchasePrice, PurchaseYear, StatusID) VALUES(5, 325, '2103', '1')
	INSERT INTO CopyOfBook(BookID, PurchasePrice, PurchaseYear, StatusID) VALUES(5, 325, '2103', '2')
	INSERT INTO CopyOfBook(BookID, PurchasePrice, PurchaseYear, StatusID) VALUES(5, 325, '2103', '1')

-- Insert in Customers

	INSERT INTO Customers(FirstName, LastName, EmailAddress, PhoneNumber, EntitledToLoan) VALUES('Andreas', 'Gidö', 'andreas.gido@gmail.com', '0123-12345', '1')
	INSERT INTO Customers(FirstName, LastName, EmailAddress, PhoneNumber, EntitledToLoan) VALUES('Karl', 'Karlsson', 'kalle.kula@hotmail.com', '445-123456', '1')
	INSERT INTO Customers(FirstName, LastName, EmailAddress, PhoneNumber, EntitledToLoan) VALUES('Svea', 'Larsson', 'svea.rike@nobodysmail.nu', '555-789456', '0')
	INSERT INTO Customers(FirstName, LastName, EmailAddress, PhoneNumber, EntitledToLoan) VALUES('Maja', 'Svensson', 'svenssonsmaja@hej.se', '0457-87452', '1')


-- Insert in LoanOfBook

	INSERT INTO LoanOfBook(CopyOfBookID, CustomerID, DateOfLoan) VALUES(2, 4, GETDATE())
	INSERT INTO LoanOfBook(CopyOfBookID, CustomerID, DateOfLoan) VALUES(7, 2, GETDATE())
	INSERT INTO LoanOfBook(CopyOfBookID, CustomerID, DateOfLoan) VALUES(8, 1, '2015-03-15')
	INSERT INTO LoanOfBook(CopyOfBookID, CustomerID, DateOfLoan) VALUES(13,3, GETDATE())
	INSERT INTO LoanOfBook(CopyOfBookID, CustomerID, DateOfLoan) VALUES(14,2, GETDATE())
	INSERT INTO LoanOfBook(CopyOfBookID, CustomerID, DateOfLoan) VALUES(19,1, '2015-03-19')
	INSERT INTO LoanOfBook(CopyOfBookID, CustomerID, DateOfLoan) VALUES(4,3, '2015-02-01')

END
GO


-- Skapa en vy som visar vilken bok som är lånad av vilken kund och vilket datum den ska vara tillbaka
IF NOT EXISTS (SELECT * FROM sys.objects 
WHERE name = 'dbo.vBookOnLoan')
BEGIN
	EXEC ('CREATE VIEW dbo.vBookOnLoan
	AS
	SELECT	B.Title
		,	C.FirstName
		,	LOB.DateOfReturn

	FROM dbo.LoanOfBook AS LOB 
		INNER JOIN CopyOfBook AS COB ON LOB.CopyOfBookID = COB.CopyOfBookID
		INNER JOIN Book AS B ON COB.BookID = B.BookID
		INNER JOIN Customers AS C ON LOB.CustomerID = C.CustomerID 
	WHERE LOB.LoanCompleted = 0')
END
GO

SELECT * FROM dbo.vBookOnLoan
GO


-- Skapa en vy som visar hur många kopior av respektive bok som finns i biblioteket och är tillgängliga för lån
IF NOT EXISTS (SELECT * FROM sys.objects 
WHERE name = 'dbo.vAvailableBooks')
BEGIN

	EXEC ('CREATE VIEW dbo.vAvailableBooks
	AS
	SELECT	B.Title
		,	COB.CopyOfBookID AS CopyID
		,	CS.Name AS StatusOfBook
	FROM dbo.CopyOfBook AS COB 
		INNER JOIN dbo.CopyStatus AS CS ON COB.StatusID = CS.ID
		INNER JOIN dbo.Book AS B ON COB.BookID = B.BookID
	WHERE COB.StatusID = 1')
END
GO

SELECT * FROM dbo.vAvailableBooks
ORDER BY CopyID ASC
GO

