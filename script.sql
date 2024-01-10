-- Create the database
CREATE DATABASE CarRentalSystem;
GO

-- Use the database
USE CarRentalSystem;
GO

-- Create Clients table
CREATE TABLE Clients (
    ClientID INT IDENTITY(1,1) PRIMARY KEY,
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    Phone VARCHAR(20),
    -- Add more columns as needed
);
GO

-- Create Cars table
CREATE TABLE Cars (
    CarID INT IDENTITY(1,1) PRIMARY KEY,
    Brand VARCHAR(50),
    Model VARCHAR(50),
    RentalPrice DECIMAL(10, 2),
    -- Add more columns as needed
);
GO

-- Create Invoices table
CREATE TABLE Invoices (
    InvoiceID INT IDENTITY(1,1) PRIMARY KEY,
    ClientID INT FOREIGN KEY REFERENCES Clients(ClientID),
    CarID INT FOREIGN KEY REFERENCES Cars(CarID),
    StartDate DATE,
    EndDate DATE,
    TotalPrice DECIMAL(10, 2),
    -- Add more columns as needed
);
GO
