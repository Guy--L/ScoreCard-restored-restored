CREATE TABLE [dbo].[Presence]
(
	[PresenceId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [GroupId] INT NOT NULL, 
    [SiteId] INT NOT NULL, 
    CONSTRAINT [FK_Presence_ToGroup] FOREIGN KEY ([GroupId]) REFERENCES [Group]([GroupId]), 
    CONSTRAINT [FK_Presence_ToSite] FOREIGN KEY ([SiteId]) REFERENCES [Site]([SiteId])
)
