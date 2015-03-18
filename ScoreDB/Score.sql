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
    [Comment] VARCHAR(50) NULL, 
    CONSTRAINT [FK_Score_ToLine] FOREIGN KEY ([LineId]) REFERENCES [Line]([LineId])
)
