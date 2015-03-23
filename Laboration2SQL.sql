-- Skapa en backup på databasen AdventureWorks2012
--BACKUP DATABASE AdventureWorks2012
--TO DISK = 'C:\MyBackups\AW_BU.bak'

USE AdventureWorks2012

-- Del 4 

-- Uppgift 4.1
SELECT LastName
FROM Person.Person

BEGIN TRANSACTION
UPDATE Person.Person
SET LastName = 'Hult'

SELECT @@TRANCOUNT AS ActiveTransaction
ROLLBACK TRANSACTION

-- Uppgift 4.2

CREATE TABLE dbo.TempCustomers
(
	ContactID int Null,
	FirstName nvarchar(50) Null,
	LastName nvarchar(50) Null,
	City nvarchar(30) Null,
	StateProvince nvarchar(50) Null
);
GO

INSERT INTO dbo.TempCustomers VALUES 
( 1, 'Kalen', 'Delaney' ),
( 2, 'Herman', 'Karlsson', 'Vislanda', 'Kronoberg')

INSERT INTO dbo.TempCustomers VALUES 
( 3, 'Tora', 'Eriksson', 'Guldsmedshyttan'),
( 4, 'Charlie', 'Carpenter', 'Tappström')

INSERT INTO dbo.TempCustomers (ContactID, FirstName, LastName, City) VALUES 
( 3, 'Tora', 'Eriksson', 'Guldsmedshyttan'),
( 4, 'Charlie', 'Carpenter', 'Tappström')

SELECT	TC.ContactID
	,	TC.FirstName
	,	TC.LastName
	,	TC.City
	,	TC.StateProvince
FROM dbo.TempCustomers AS TC

-- Uppgift 4.3
INSERT INTO Production.Product (Name, ProductNumber, SafetyStockLevel, ReorderPoint, StandardCost, ListPrice, DaysToManufacture, SellStartDate) VALUES
( 'Racing-Gizmo', ' ', 5, 2, 1000, 500, 2, GETDATE() )


-- Uppgift 4.4
INSERT INTO dbo.TempCustomers (ContactID, FirstName, LastName, City, StateProvince)
(
SELECT P.BusinessEntityID, P.FirstName
	, P.LastName, PA.City , SP.Name
FROM Person.Person AS P
	JOIN Person.BusinessEntity AS BE
		ON P.BusinessEntityID=BE.BusinessEntityID
	JOIN Person.BusinessEntityAddress AS BEA
		ON BE.BusinessEntityID = BEA.BusinessEntityID
	JOIN Person.Address PA
		ON BEA.AddressID=PA.AddressID
	JOIN Person.StateProvince AS SP
		ON PA.StateProvinceID = SP.StateProvinceID
);


-- Upppgift 4.5
-- Töm tabellen
-- och töm buffer och cache
TRUNCATE TABLE dbo.TempCustomers
GO
DBCC DROPCLEANBUFFERS
DBCC FREEPROCCACHE
GO
-- Lägg till data och mät tiden
DECLARE @Start DATETIME2, @Stop DATETIME2
SELECT @Start = SYSDATETIME()

INSERT INTO dbo.TempCustomers
(ContactID, FirstName, LastName)
SELECT BusinessEntityID, FirstName, LastName
FROM Person.Person

SELECT @Stop = SYSDATETIME()
SELECT DATEDIFF(ms,@Start,@Stop) as MilliSeconds

-- 257ms
-- 34ms, 32ms, 33ms, 126ms, 120ms, 36ms, 31ms

--CREATE UNIQUE CLUSTERED INDEX [Unique_Clustered]
--ON [dbo].[TempCustomers]
--( [ContactID] ASC )
--GO
--CREATE NONCLUSTERED INDEX [NonClustered_LName]
--ON [dbo].[TempCustomers]
--( [LastName] ASC )
--GO
--CREATE NONCLUSTERED INDEX [NonClustered_FName]
--ON [dbo].[TempCustomers]
--( [FirstName] ASC )

-- 1591ms
-- 301ms, 307ms, 245ms


-- Uppgift 4.6
SELECT	BusinessEntityID
	,	P.PersonType
	,	P.FirstName
	,	P.LastName
	,	P.Title
	,	P.EmailPromotion
INTO #TempTable
FROM Person.Person AS P
WHERE P.LastName IN('Achong', 'Acevedo')

SELECT * FROM #TempTable

INSERT INTO Person.Person (BusinessEntityID, PersonType, FirstName, LastName, Title, EmailPromotion)

	SELECT	BusinessEntityID		
	,		PersonType
	,		FirstName
	,		LastName
	,		Title
	,		EmailPromotion
FROM #TempTable

UPDATE #TempTable
SET BusinessEntityID = (SELECT MAX(P.BusinessEntityID) + 1
FROM Person.Person AS P)

-- Kolla MSDN TOP
UPDATE TOP (1) #TempTable
SET BusinessEntityID = BusinessEntityID + 1
FROM #TempTable
	 
INSERT INTO Person.BusinessEntity
VALUES (DEFAULT, DEFAULT)

DROP TABLE #TempTable

SELECT	P.BusinessEntityID
	,	P.PersonType
	,	P.FirstName
	,	P.LastName
	,	P.Title
	,	P.EmailPromotion
FROM Person.Person AS P
WHERE P.ModifiedDate > '2015-03-10'


-- Uppgift 4.7
UPDATE Person.Person
SET	FirstName = 'Gurra'
	,	LastName = 'Tjong'
FROM Person.Person
-- WHERE BusinessEntityID (Kan man få ut senaste numret eller måste man jämföra med namnet som man då måste veta??)




-- Uppgift 4.8
SELECT	P.ListPrice
	,	PSC.Name
	,	P.ListPrice * 1.1 AS 'NewListPrice'
FROM Production.Product AS P
	INNER JOIN Production.ProductSubcategory AS PSC ON P.ProductSubcategoryID = PSC.ProductSubcategoryID
WHERE PSC.Name = 'Gloves'

--SELECT	P.ListPrice
--	,	PSC.Name
--FROM Production.Product AS P
--	INNER JOIN Production.ProductSubcategory AS PSC ON P.ProductSubcategoryID = PSC.ProductSubcategoryID
--WHERE PSC.Name = 'Gloves'


-- Uppgift 4.9
DELETE FROM dbo.TempCustomers
WHERE LastName = 'Smith'

--SELECT	TC.LastName
--FROM dbo.TempCustomers AS TC
--WHERE TC.LastName = 'Smith'




