-- SQL Script to create ReactAPIDB and tables
-- Run this on your SQL Server instance

-- Create Database
CREATE DATABASE ReactAPIDB;
GO

-- Switch to the new database
USE ReactAPIDB;
GO

-- Create Employees Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Employees')
BEGIN
	CREATE TABLE Employees (
		Id INT PRIMARY KEY IDENTITY(1,1),
		[Name] NVARCHAR(MAX) NOT NULL,
		[Position] NVARCHAR(MAX) NOT NULL,
		Salary DECIMAL(18,2) NOT NULL
	);
END
GO

-- Create Registers Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Registers')
BEGIN
	CREATE TABLE Registers (
		Id INT PRIMARY KEY IDENTITY(1,1),
		[Name] NVARCHAR(MAX) NOT NULL,
		Email NVARCHAR(MAX) NOT NULL,
		Username NVARCHAR(MAX) NOT NULL,
		[Password] NVARCHAR(MAX) NOT NULL,
		VehicleNumber NVARCHAR(MAX) NOT NULL,
		DateOfBirth DATETIME2 NOT NULL
	);
END
GO

-- Create Bookings Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Bookings')
BEGIN
	CREATE TABLE Bookings (
		Id BIGINT PRIMARY KEY IDENTITY(1,1),
		BookingDate DATETIME2 NOT NULL,
		[Name] NVARCHAR(MAX) NOT NULL,
		PhoneNumber NVARCHAR(MAX) NOT NULL,
		VehicleNumber NVARCHAR(MAX) NOT NULL,
		SlotNumber NVARCHAR(MAX) NOT NULL
	);
END
GO

-- Create Slots Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Slots')
BEGIN
	CREATE TABLE Slots (
		Id BIGINT PRIMARY KEY IDENTITY(1,1),
		SlotNumber NVARCHAR(50) NOT NULL,
		IsAvailable BIT NOT NULL
	);
END
GO

-- Create __EFMigrationsHistory table for EF Core tracking
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = '__EFMigrationsHistory')
BEGIN
	CREATE TABLE __EFMigrationsHistory (
		MigrationId NVARCHAR(150) NOT NULL PRIMARY KEY,
		ProductVersion NVARCHAR(32) NOT NULL
	);

	-- Insert existing migrations
	INSERT INTO __EFMigrationsHistory (MigrationId, ProductVersion) VALUES 
	('20250224102233_InitialCreate', '6.0.0'),
	('20250411060915_InitialCr', '6.0.0');
END
GO

PRINT 'Database ReactAPIDB created successfully with all tables!';
