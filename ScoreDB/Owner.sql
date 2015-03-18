CREATE TABLE [dbo].[Owner]
(
	[OwnerId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [IonName] VARCHAR(50) NOT NULL, 
    [FirstName] VARCHAR(30) NOT NULL, 
    [LastName] VARCHAR(30) NOT NULL, 
    [ManagerId] INT NOT NULL, 
    [IsAdmin] BIT NOT NULL
)
