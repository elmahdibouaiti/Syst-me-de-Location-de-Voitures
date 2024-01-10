CREATE TABLE [dbo].[Cars]
(
	[Id] INT IDENTITY(1,1) PRIMARY KEY, 
    [Brand] NVARCHAR(50) NULL, 
    [Model] NVARCHAR(50) NULL, 
    [Year] NCHAR(10) NULL, 
    [RentalPrice] DECIMAL(10, 2) NULL
)
