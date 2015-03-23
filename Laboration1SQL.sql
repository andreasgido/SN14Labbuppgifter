USE AdventureWorks2012;

-- Del 1 - SELECT

-- Uppgift 1.1
SELECT	P.ProductID
	,	P.Name
	,	P.Color
	,	P.ListPrice
FROM Production.Product AS P;


-- Uppgift 1.2
SELECT	P.ProductID
	,	P.Name
	,	P.Color
	,	P.ListPrice
FROM Production.Product AS P
WHERE P.ListPrice > 0;


-- Uppgift 1.3
SELECT	P.ProductID
	,	P.Name
	,	P.Color
	,	P.ListPrice
FROM Production.Product AS P
WHERE P.COLOR IS NULL;


-- Uppgift 1.4
SELECT	P.ProductID
	,	P.Name
	,	P.Color
	,	P.ListPrice
FROM Production.Product AS P
WHERE P.Color IS NOT NULL;


-- Uppgift 1.5
SELECT	P.ProductID
	,	P.Name
	,	P.Color
	,	P.ListPrice
FROM Production.Product AS P
WHERE P.Color IS NOT NULL 
		AND P.ListPrice > 0;


-- Uppgift 1.6
SELECT	P.Name + ' ' + P.Color AS 'Name and Color'
FROM Production.Product AS P
WHERE P.Color IS NOT NULL;


-- Uppgift 1.7
SELECT	'NAME: ' + P.Name + ' -- ' + 'COLOR: ' + P.Color AS 'Name and Color'
FROM Production.Product AS P
WHERE P.Color IS NOT NULL;


-- Uppgift 1.8
SELECT	P.ProductID
	,	P.Name
FROM Production.Product AS P
WHERE	P.ProductID >= 400  
		AND P.ProductID <= 500;


-- Uppgift 1.9
SELECT	P.ProductID
	,	P.Name
	,	P.Color
FROM Production.Product AS P
WHERE	P.Color IN ('BLACK', 'Blue');


-- Uppgift 1.10
SELECT	P.Name
	,	P.ListPrice
FROM Production.Product AS P
WHERE	P.Name LIKE 'S%';


-- Uppgift 1.11
SELECT	P.Name
	,	P.ListPrice
FROM Production.Product AS P
WHERE	P.Name LIKE 'S%' OR
		P.Name LIKE 'A%';


-- Uppgift 1.12
SELECT	P.Name
	,	P.ListPrice
FROM Production.Product AS P
WHERE	P.Name LIKE 'SPO[^K]%' 
ORDER BY P.Name ASC;


-- Uppgift 1.13
SELECT DISTINCT	P.Color
FROM Production.Product AS P;


-- Upppgift 1.14
SELECT DISTINCT	P.ProductSubcategoryID
	,	P.Color
FROM Production.Product AS P
WHERE	P.ProductSubcategoryID  IS NOT NULL 
		AND P.Color IS NOT NULL
ORDER BY P.ProductSubcategoryID ASC 
	,	P.Color DESC;


-- Uppgift 1.15
SELECT	P.ProductSubcategoryID
	,	LEFT([Name],35) AS [Name]
	,	Color
	,	ListPrice
FROM Production.Product AS P
WHERE Color IN('Red', 'Black')
		AND ProductSubcategoryID = 1
		OR P.ListPrice BETWEEN 1000 AND 2000
ORDER BY P.ProductID;


-- Uppgift 1.16
SELECT	P.Name
	,	ISNULL(P.Color, 'Unknown') AS Color
	,	P.ListPrice
FROM Production.Product AS P;


-- DEL 2 - GROUP BY och aggregring

-- Uppgift 2.1 
SELECT COUNT(*) AS AmountOfProducts
FROM Production.Product AS P;


-- Uppgift 2.2
SELECT COUNT(P.ProductSubCategoryID) AS 'ProductAmount'
FROM Production.Product AS P;


-- Uppgift 2.3
SELECT	P.ProductSubcategoryID
	,	COUNT(P.Name) AS 'ProductAmount'	 
FROM Production.Product AS P
GROUP BY P.ProductSubCategoryID;


-- Uppgift 2.4
SELECT	COUNT(*) AS 'ProductsWithoutSubCategory'
FROM Production.Product AS P
WHERE P.ProductSubcategoryID IS Null;

SELECT COUNT(*) - COUNT(P.ProductSubcategoryID) AS 'ProductsWithoutSubCategory'
FROM Production.Product AS P;


-- Uppgift 2.5
SELECT	P.ProductID
	,	SUM(P.Quantity) AS Quantity
FROM Production.ProductInventory AS P
GROUP BY P.ProductID;


-- Uppgift 2.6
SELECT	P.ProductID
	,	SUM(P.Quantity) AS Quantity
FROM Production.ProductInventory AS P
WHERE P.LocationID = 40
GROUP BY P.ProductID
	,	 P.Quantity
HAVING P.Quantity < 100;


-- Uppgift 2.7
SELECT	P.ProductID
	,	SUM(P.Quantity) AS Quantity
	,	P.Shelf
FROM Production.ProductInventory AS P
WHERE P.LocationID = 40
GROUP BY P.ProductID
	,	 P.Quantity
	,	 P.Shelf
HAVING P.Quantity < 100;


-- Uppgift 2.8
SELECT	P.LocationID
	,	AVG(P.Quantity) AS AverageAmount
FROM Production.ProductInventory AS P
WHERE P.LocationID = 10
GROUP BY P.LocationID;


-- Uppgift 2.9
SELECT	ROW_NUMBER() OVER(ORDER BY PC.Name ASC) AS Row
	,	PC.Name
FROM Production.ProductCategory AS PC;


-- Del 3 - JOINS

-- Uppgift 3.1
SELECT	CR.Name AS 'CountryName'
	,	SP.Name	AS 'ProvinceName'
FROM Person.CountryRegion AS CR
		INNER JOIN Person.StateProvince AS SP ON CR.CountryRegionCode = SP.CountryRegionCode;


-- Uppgift 3.2
SELECT	CR.Name AS 'Country'
	,	SP.Name	AS 'Province'
FROM Person.CountryRegion AS CR
		INNER JOIN Person.StateProvince AS SP ON CR.CountryRegionCode = SP.CountryRegionCode
WHERE CR.Name IN('Germany', 'Canada')
ORDER BY CR.Name ASC
	,	 SP.Name ASC;


-- Uppgift 3.3
SELECT	SOH.SalesOrderID
	,	SOH.OrderDate
	,	SOH.SalesPersonID
	,	SP.BusinessEntityID
	,	SP.Bonus
	,	SP.SalesYTD
FROM Sales.SalesOrderHeader AS SOH
		INNER JOIN Sales.SalesPerson AS SP ON SOH.SalesPersonID = SP.BusinessEntityID


-- Uppgift 3.4
SELECT	SOH.SalesOrderID
	,	SOH.OrderDate
	,	SP.Bonus
	,	SP.SalesYTD
	,	HE.JobTitle
FROM Sales.SalesOrderHeader AS SOH
		INNER JOIN Sales.SalesPerson AS SP ON SOH.SalesPersonID = SP.BusinessEntityID
		INNER JOIN HumanResources.Employee AS HE ON SP.BusinessEntityID = HE.BusinessEntityID


-- Uppgift 3.5
SELECT	SOH.SalesOrderID
	,	SOH.OrderDate
	,	SP.Bonus
	,	P.FirstName
	,	P.LastName
FROM Sales.SalesOrderHeader AS SOH
		INNER JOIN Sales.SalesPerson AS SP ON SOH.SalesPersonID = SP.BusinessEntityID
		INNER JOIN HumanResources.Employee AS HE ON SP.BusinessEntityID = HE.BusinessEntityID
		INNER JOIN Person.Person AS P ON SOH.SalesPersonID = P.BusinessEntityID


-- Uppgift 3.6
SELECT	SOH.SalesOrderID
	,	SOH.OrderDate
	,	SP.Bonus
	,	P.FirstName
	,	P.LastName
FROM Sales.SalesOrderHeader AS SOH
		INNER JOIN Sales.SalesPerson AS SP ON SOH.SalesPersonID = SP.BusinessEntityID
		INNER JOIN Person.Person AS P ON SOH.SalesPersonID = P.BusinessEntityID


-- Uppgift 3.7
SELECT	SOH.SalesOrderID
	,	SOH.OrderDate
	,	P.FirstName
	,	P.LastName
FROM Sales.SalesOrderHeader AS SOH
		INNER JOIN Person.Person AS P ON SOH.SalesPersonID = P.BusinessEntityID


-- Uppgift 3.8
SELECT	SOH.SalesOrderID
	,	SOH.OrderDate
	,	P.FirstName + ' ' +	P.LastName AS 'SalesPerson'
	,	SOD.ProductID
	,	SOD.OrderQty
FROM Sales.SalesOrderHeader AS SOH
		INNER JOIN Person.Person AS P ON SOH.SalesPersonID = P.BusinessEntityID
		INNER JOIN Sales.SalesOrderDetail AS SOD ON SOH.SalesOrderID = SOD.SalesOrderID
ORDER BY SOH.OrderDate
	,	SOH.SalesOrderID


-- Uppgift 3.9
SELECT	SOH.SalesOrderID
	,	SOH.OrderDate
	,	P.FirstName + ' ' +	P.LastName AS 'SalesPerson'
	,	PP.Name
	,	SOD.OrderQty
FROM Sales.SalesOrderHeader AS SOH
		INNER JOIN Person.Person AS P ON SOH.SalesPersonID = P.BusinessEntityID
		INNER JOIN Sales.SalesOrderDetail AS SOD ON SOH.SalesOrderID = SOD.SalesOrderID
		INNER JOIN Production.Product AS PP ON SOD.ProductID = PP.ProductID
ORDER BY SOH.OrderDate
	,	SOH.SalesOrderID


-- Upppgift 3.10
SELECT	SOH.SalesOrderID
	,	SOH.OrderDate
	,	P.FirstName + ' ' +	P.LastName AS 'SalesPerson'
	,	PP.Name
	,	SOD.OrderQty
FROM Sales.SalesOrderHeader AS SOH
		INNER JOIN Person.Person AS P ON SOH.SalesPersonID = P.BusinessEntityID
		INNER JOIN Sales.SalesOrderDetail AS SOD ON SOH.SalesOrderID = SOD.SalesOrderID
		INNER JOIN Production.Product AS PP ON SOD.ProductID = PP.ProductID
WHERE SOH.SubTotal > 100000
		AND SOH.OrderDate BETWEEN '2005' AND '2006'
ORDER BY SOH.OrderDate
	,	SOH.SalesOrderID


-- Uppgift 3.11
SELECT	CR.Name AS 'CountryName'
	,	ISNULL(SP.Name, 'No Province') AS 'ProvinceName'
FROM Person.CountryRegion AS CR
		LEFT JOIN Person.StateProvince AS SP ON CR.CountryRegionCode = SP.CountryRegionCode


-- Uppgift 3.12
SELECT	C.CustomerID
FROM Sales.Customer AS C 
		FULL JOIN Sales.SalesOrderHeader AS SOH ON C.CustomerID = SOH.CustomerID
WHERE SOH.PurchaseOrderNumber IS Null


-- Uppgift 3.13
SELECT	P.Name AS 'ProductName'
	,	PM.Name AS 'ProductModelName'
FROM Production.Product AS P
		FULL JOIN Production.ProductModel AS PM ON P.ProductModelID = PM.ProductModelID 
WHERE PM.Name IS Null
		OR P.Name IS Null
		

-- Uppgift 3.14
SELECT	DISTINCT SOH.SalesPersonID
	,	P.FirstName + ' ' + P.LastName AS 'FullName'
	,	COUNT(SOH.SalesOrderNumber) AS 'NoOfOrders'
	,	SUM(SOH.TotalDue) AS 'TotalSum'
FROM Sales.SalesPerson AS SP
		INNER JOIN Sales.SalesOrderHeader AS SOH ON SP.BusinessEntityID = SOH.SalesPersonID
		INNER JOIN Person.Person AS P ON SP.BusinessEntityID = P.BusinessEntityID
		INNER JOIN HumanResources.Employee AS EMP ON SP.BusinessEntityID = EMP.BusinessEntityID
GROUP BY SOH.SalesPersonID
	,	P.FirstName
	,	P.LastName


-- Uppgift 3.15
SELECT	DATEPART(YEAR , SOH.OrderDate) AS 'Year'
	,	ST.Name	AS 'Region'
	,	SUM(SOH.SubTotal) AS 'SumTotal'
FROM Sales.SalesOrderHeader AS SOH 
		INNER JOIN Sales.SalesTerritory AS ST ON SOH.TerritoryID = ST.TerritoryID
GROUP BY SOH.OrderDate
	,	 ST.Name
ORDER BY SOH.OrderDate ASC
	,	 ST.Name ASC


-- Uppgift 3.16
SELECT	P.FirstName + ' ' + P.LastName AS 'FullName'
FROM Person.Person AS P 
		INNER JOIN HumanResources.Employee AS EMP ON P.BusinessEntityID = EMP.BusinessEntityID
		INNER JOIN HumanResources.EmployeeDepartmentHistory AS EDH ON P.BusinessEntityID = EDH.BusinessEntityID
GROUP BY P.FirstName
	,	 P.LastName
HAVING COUNT(EDH.EndDate) > 0


-- Uppgift 3.17
SELECT	MAX(SOH.SubTotal) AS 'SubTotal', 'Max' AS 'Result'
FROM Sales.SalesOrderHeader AS SOH

UNION

SELECT	Min(SOH.SubTotal) AS 'MinSubTotal', 'Min'
FROM Sales.SalesOrderHeader AS SOH

UNION

SELECT	AVG(SOH.SubTotal) AS 'AverageSubTotal', 'Average'
FROM Sales.SalesOrderHeader AS SOH


-- Uppgift 3.18
SELECT	TOP (10) MAX(P.ListPrice) AS 'TopPrice'
	,	P.Name
FROM Production.Product AS P
GROUP BY P.Name
	,	P.ListPrice
ORDER BY P.ListPrice DESC


-- Uppgift 3.19
SELECT	Top (1) PERCENT P.DaysToManufacture
	,	P.Name	
FROM Production.Product AS P
ORDER BY P.DaysToManufacture DESC








