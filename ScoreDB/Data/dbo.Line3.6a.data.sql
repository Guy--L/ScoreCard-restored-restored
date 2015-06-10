INSERT INTO [dbo].[Measure] ([Symbol], [Mask], [Description], [DecimalPoint]) VALUES (N'scored', N'N', N'Scored', 2)

update [dbo].[Line] set [Description] = N'QI' where LineId = 28

SET IDENTITY_INSERT [dbo].[Line] ON
INSERT INTO [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (38, 28, N'a', 4, N'SQI')
SET IDENTITY_INSERT [dbo].[Line] OFF

INSERT INTO [dbo].[Responsibility] ([WorkerId], [LineId]) VALUES (82, 38)

INSERT INTO [dbo].[Score]  ([YearEnding],[LineId],[GroupId],[SiteId]) VALUES 
	(2014,38,0,0),
	(2014,38,4,1),
	(2014,38,4,2),
	(2014,38,4,3),
	(2014,38,4,4),
	(2014,38,4,5),
	(2014,38,4,6),
	(2014,38,4,7),
	(2014,38,4,8),
	(2014,38,4,9),
	(2014,38,4,10),
	(2014,38,4,11),
	(2014,38,4,12),
	(2014,38,4,13),
	(2014,38,4,14),
	(2014,38,4,15),
	(2014,38,4,16),
	(2014,38,4,17),
	(2014,38,4,18),
	(2014,38,4,19),
	(2014,38,4,20),
	(2014,38,4,21),
	(2015,38,0,0),
	(2015,38,4,1),
	(2015,38,4,2),
	(2015,38,4,3),
	(2015,38,4,4),
	(2015,38,4,5),
	(2015,38,4,6),
	(2015,38,4,7),
	(2015,38,4,8),
	(2015,38,4,9),
	(2015,38,4,10),
	(2015,38,4,11),
	(2015,38,4,12),
	(2015,38,4,13),
	(2015,38,4,14),
	(2015,38,4,15),
	(2015,38,4,16),
	(2015,38,4,17),
	(2015,38,4,18),
	(2015,38,4,19),
	(2015,38,4,20),
	(2015,38,4,21)

