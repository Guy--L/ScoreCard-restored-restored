CREATE TABLE [dbo].[Permit]
(
	[PermitId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [WorkerId] INT NOT NULL, 
    [GroupId] INT NOT NULL, 
    [SiteId] INT NOT NULL
)
