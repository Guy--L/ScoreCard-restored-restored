CREATE TABLE [dbo].[Hide]
(
	[HideId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [LineId] INT NOT NULL, 
    [StartYear] INT NULL, 
    [EndYear] INT NULL
)
