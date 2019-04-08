CREATE TABLE [dbo].[Information]
(
	[PersonId] INT NOT NULL PRIMARY KEY Identity (1,1), 
    [FName] NVARCHAR(50) NOT NULL, 
    [LName] NVARCHAR(50) NOT NULL, 
    [CarModel] NVARCHAR(50) NOT NULL, 
    [MilesDriven] DECIMAL(16, 2) NOT NULL, 
    [GallonsFilled] DECIMAL(16, 2) NOT NULL, 
    [FillUpDate] DATETIME NOT NULL
)
