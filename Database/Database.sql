USE master
GO

CREATE DATABASE RouteListsDB
GO
USE RouteListsDB
GO

CREATE TABLE PassTypes(
	ID INT NOT NULL IDENTITY(0, 1),
	Title NVARCHAR(150) NOT NULL,

	PRIMARY KEY(ID)
)
GO

INSERT INTO PassTypes (Title)
VALUES
(N'Нет'),
(N'ТТК'),
(N'СК')
GO

CREATE TABLE Vehicles(
	ID INT NOT NULL IDENTITY(1, 1),
	Brand NVARCHAR(150) NOT NULL,
	Number NVARCHAR(15) NOT NULL UNIQUE,
	Tonnage INT NOT NULL,

	PRIMARY KEY(ID)
)
GO

CREATE TABLE VehiclePasses(
	VehicleID INT NOT NULL,
	PassTypeID INT NOT NULL,
	ExpireDate DATE NOT NULL,

	PRIMARY KEY(VehicleID),
	FOREIGN KEY(VehicleID) REFERENCES Vehicles(ID) ON DELETE CASCADE,
	FOREIGN KEY(PassTypeID) REFERENCES PassTypes(ID)
)
GO

CREATE TABLE Drivers(
	ID INT NOT NULL IDENTITY(1, 1),
	Surname NVARCHAR(75) NOT NULL,
	Name NVARCHAR(75) NOT NULL,
	Patronymic NVARCHAR(75) NULL,
	Bithday DATE NOT NULL,
	DrivingExperience INT NOT NULL,
	MainVehicle INT NULL,

	PRIMARY KEY(ID),
	FOREIGN KEY(MainVehicle) REFERENCES Vehicles(ID) ON DELETE SET NULL
)
GO

CREATE TABLE Companies(
	ID INT NOT NULL IDENTITY(1, 1),
	Title NVARCHAR(150) NOT NULL UNIQUE,

	PRIMARY KEY(ID)
)
GO

CREATE TABLE Managers(
	ID INT NOT NULL IDENTITY(1, 1),
	CompanyID INT NOT NULL,
	Surname NVARCHAR(75) NULL,
	Name NVARCHAR(75) NULL,
	Patronymic NVARCHAR(75) NULL,
	Phone NVARCHAR(20) NOT NULL,

	PRIMARY KEY(ID),
	FOREIGN KEY(CompanyID) REFERENCES Companies(ID) ON DELETE CASCADE
)
GO

CREATE TABLE RouteLists(
	ID INT NOT NULL IDENTITY(1, 1),
	DriverID INT NOT NULL,
	VehicleID INT NOT NULL,
	Date DATE NOT NULL,
	ListNumberPerMonth INT NOT NULL,

	PRIMARY KEY(ID),
	FOREIGN KEY(DriverID) REFERENCES Drivers(ID) ON DELETE CASCADE,
	FOREIGN KEY(VehicleID) REFERENCES Vehicles(ID) ON DELETE CASCADE
)
GO

CREATE TABLE RoutePoints(
	ID INT NOT NULL IDENTITY(1, 1),
	RouteID INT NOT NULL,
	Address NVARCHAR(500) NOT NULL,
	Action NVARCHAR(20) NOT NULL,
	ManagerID INT NOT NULL,
	InvoiceNumbers NVARCHAR(150) NOT NULL,
	Cost DECIMAL(15,2) NULL,
	Comment NVARCHAR(500) NULL,
	PointNumber INT NULL,

	PRIMARY KEY(ID),
	FOREIGN KEY(RouteID) REFERENCES RouteLists(ID) ON DELETE CASCADE,
	FOREIGN KEY(ManagerID) REFERENCES Managers(ID) ON DELETE CASCADE
)
GO
