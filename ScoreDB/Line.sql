CREATE TABLE [dbo].[Line]
(
	[LineId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ParentLineId] INT NOT NULL, 
    [Order] INT NOT NULL, 
    [MeasureId] INT NOT NULL, 
    [Description] VARCHAR(200) NOT NULL, 
    CONSTRAINT [FK_Line_ToLine] FOREIGN KEY ([ParentLineId]) REFERENCES [Line]([LineId]), 
    CONSTRAINT [FK_Line_ToMeasure] FOREIGN KEY ([MeasureId]) REFERENCES [Measure]([MeasureId])
)
