use [ScoreCard]

alter table [Line] nocheck constraint all
alter table [Measure] nocheck constraint all 
delete from [Measure]
alter table [Measure] check constraint all 

SET IDENTITY_INSERT [dbo].[Measure] ON
INSERT INTO [dbo].[Measure] ([MeasureId], [Symbol], [Mask], [Description], [DecimalPoint], [HorizontalAvg], [VerticalAvg]) VALUES (1, N'$MM/%', N'N', N'Million USD',			2, 0, 0)
INSERT INTO [dbo].[Measure] ([MeasureId], [Symbol], [Mask], [Description], [DecimalPoint], [HorizontalAvg], [VerticalAvg]) VALUES (2, N'FTE', N'N', N'Full Time Employees',		2, 0, 0)
INSERT INTO [dbo].[Measure] ([MeasureId], [Symbol], [Mask], [Description], [DecimalPoint], [HorizontalAvg], [VerticalAvg]) VALUES (3, N'$MM', N'N', N'Million USD',				2, 0, 0)
INSERT INTO [dbo].[Measure] ([MeasureId], [Symbol], [Mask], [Description], [DecimalPoint], [HorizontalAvg], [VerticalAvg]) VALUES (4, N'number', N'N', N'Number',				0, 0, 0)
INSERT INTO [dbo].[Measure] ([MeasureId], [Symbol], [Mask], [Description], [DecimalPoint], [HorizontalAvg], [VerticalAvg]) VALUES (5, N'%', N'N', N'Percent',					2, 1, 1)
INSERT INTO [dbo].[Measure] ([MeasureId], [Symbol], [Mask], [Description], [DecimalPoint], [HorizontalAvg], [VerticalAvg]) VALUES (6, N'TIR', N'N', N'TIR',						2, 1, 0)
INSERT INTO [dbo].[Measure] ([MeasureId], [Symbol], [Mask], [Description], [DecimalPoint], [HorizontalAvg], [VerticalAvg]) VALUES (7, N'score', N'N', N'Score',					0, 0, 0)
INSERT INTO [dbo].[Measure] ([MeasureId], [Symbol], [Mask], [Description], [DecimalPoint], [HorizontalAvg], [VerticalAvg]) VALUES (8, N'blank', N'0', N'Label',					0, 0, 0)
INSERT INTO [dbo].[Measure] ([MeasureId], [Symbol], [Mask], [Description], [DecimalPoint], [HorizontalAvg], [VerticalAvg]) VALUES (9, N'decimal', N'N', N'Decimal',				1, 0, 0)
INSERT INTO [dbo].[Measure] ([MeasureId], [Symbol], [Mask], [Description], [DecimalPoint], [HorizontalAvg], [VerticalAvg]) VALUES (10, N'quarterly', N'N', N'Quarterly',		2, 1, 0)
INSERT INTO [dbo].[Measure] ([MeasureId], [Symbol], [Mask], [Description], [DecimalPoint], [HorizontalAvg], [VerticalAvg]) VALUES (11, N'%>=8', N'N', N'Threshold8',			2, 0, 0)
INSERT INTO [dbo].[Measure] ([MeasureId], [Symbol], [Mask], [Description], [DecimalPoint], [HorizontalAvg], [VerticalAvg]) VALUES (12, N'recent', N'N', N'Recent',				0, 0, 0)
SET IDENTITY_INSERT [dbo].[Measure] OFF

alter table [Line] check constraint all

update [dbo].[Line] set [Description] = N'Legal compliance issues', [MeasureId] = 12 where LineId = 27
SET IDENTITY_INSERT [dbo].[Line] ON
INSERT INTO [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description], [ShowTotal]) VALUES (39, 27, 'a', 12, N'Significant incidents', 1)
SET IDENTITY_INSERT [dbo].[Line] OFF

go

 alter table [Responsibility] NOCHECK CONSTRAINT all
 insert into [dbo].[Responsibility]  ([lineid], [workerid] ) 
 select 39, r.[workerid] from [dbo].[Responsibility] r where lineid = 27
 alter table [Responsibility] CHECK CONSTRAINT all
 go

alter table [Score] nocheck constraint all
INSERT INTO [dbo].[Score]  ([YearEnding],[LineId],[GroupId],[SiteId]) 
select [YearEnding], 39, [GroupId], [SiteId] from [dbo].[Score] where [LineId] = 27
alter table [Score] check constraint all
go