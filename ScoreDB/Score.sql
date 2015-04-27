CREATE TABLE [dbo].[Score]
(
	[ScoreId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [YearEnding] INT NOT NULL, 
    [LineId] INT NOT NULL, 
    [Target] INT NULL, 
    [Q1] INT NULL, 
    [Q2] INT NULL, 
    [Q3] INT NULL, 
    [Q4] INT NULL, 
	[Total] int null,
    [Comment] VARCHAR(50) NULL, 
    [GroupId] INT NULL, 
    [SiteId] INT NULL, 
    CONSTRAINT [FK_Score_ToLine] FOREIGN KEY ([LineId]) REFERENCES [Line]([LineId]), 
    CONSTRAINT [FK_Score_ToGroup] FOREIGN KEY ([GroupId]) REFERENCES [Group]([GroupId])
)
