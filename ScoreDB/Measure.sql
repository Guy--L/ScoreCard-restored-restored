CREATE TABLE [dbo].[Measure]
(
	[MeasureId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Symbol] VARCHAR(10) NOT NULL, 
    [Mask] VARCHAR(10) NOT NULL, 
    [Description] VARCHAR(50) NOT NULL, 
    [DecimalPoint] INT NOT NULL, 
    [VerticalAvg] BIT NOT NULL, 
    [HorizontalAvg] BIT NOT NULL
)
