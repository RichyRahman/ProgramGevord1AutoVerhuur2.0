
-- Autoverhuur schema + data (AutoverhuurRichy.sql)

-- CREATE DATABASE AutoverhuurRichy;
-- GO
-- USE AutoverhuurRichy;
-- GO

CREATE TABLE Establishments (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Airport NVARCHAR(100) NOT NULL,
    Street NVARCHAR(100),
    PostalCode NVARCHAR(20),
    City NVARCHAR(50),
    Country NVARCHAR(50)
);

CREATE TABLE Customers (
    Id INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    Email NVARCHAR(100) UNIQUE,
    Street NVARCHAR(100),
    PostalCode NVARCHAR(20),
    City NVARCHAR(50),
    Country NVARCHAR(50)
);

CREATE TABLE Cars (
    Id INT PRIMARY KEY IDENTITY(1,1),
    LicensePlate NVARCHAR(20) UNIQUE,
    Model NVARCHAR(100),
    Seats INT,
    MotorType NVARCHAR(20),
    EstablishmentId INT FOREIGN KEY REFERENCES Establishments(Id)
);

CREATE TABLE Reservations (
    Id INT PRIMARY KEY IDENTITY(1,1),
    CustomerId INT FOREIGN KEY REFERENCES Customers(Id),
    CarId INT FOREIGN KEY REFERENCES Cars(Id),
    StartDate DATE,
    EndDate DATE
);

INSERT INTO Establishments (Airport, Street, PostalCode, City, Country)
VALUES 
('Zaventem', 'Leopoldlaan', '1930', 'Brussel', 'België'),
('Schiphol', 'Evert van de Beekstraat', '1118 CP', 'Amsterdam', 'Nederland');

INSERT INTO Customers (FirstName, LastName, Email, Street, PostalCode, City, Country)
VALUES 
('Jan', 'Peeters', 'jan@peeters.be', 'Kerkstraat 12', '2000', 'Antwerpen', 'België'),
('Lotte', 'Jansen', 'lotte.jansen@gmail.com', 'Boomstraat 5', '1000', 'Brussel', 'België');

INSERT INTO Cars (LicensePlate, Model, Seats, MotorType, EstablishmentId)
VALUES 
('1-ABC-123', 'Renault Clio', 5, 'Gasoline', 1),
('2-XYZ-789', 'Tesla Model 3', 5, 'Electric', 2),
('3-DEF-456', 'Peugeot 208', 4, 'Diesel', 1);

INSERT INTO Reservations (CustomerId, CarId, StartDate, EndDate)
VALUES 
(1, 1, '2025-06-01', '2025-06-05'),
(2, 2, '2025-06-10', '2025-06-15');
