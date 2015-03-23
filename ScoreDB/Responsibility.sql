CREATE TABLE [dbo].[Responsibility]
(
	[ResponsibilityId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [WorkerId] INT NOT NULL, 
    [LineId] INT NOT NULL, 
    CONSTRAINT [FK_Responsibility_ToLine] FOREIGN KEY ([LineId]) REFERENCES [Line]([LineId]), 
    CONSTRAINT [FK_Responsibility_ToWorker] FOREIGN KEY ([WorkerId]) REFERENCES [Worker]([WorkerId])
)
