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

SET IDENTITY_INSERT [dbo].[Line] ON
INSERT INTO [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description], [ShowTotal]) VALUES (6, 6, N'1', 8, N'Financials ($MM)', 0)
INSERT INTO [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description], [ShowTotal]) VALUES (9, 9, N'2', 8, N'Performance', 0)
INSERT INTO [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description], [ShowTotal]) VALUES (10, 10, N'3', 8, N'Governance', 0)
INSERT INTO [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description], [ShowTotal]) VALUES (11, 11, N'4', 8, N'Customer Satisfaction', 0)
INSERT INTO [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description], [ShowTotal]) VALUES (12, 6, N'1', 1, N'Budget', 0)
INSERT INTO [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description], [ShowTotal]) VALUES (13, 6, N'2', 2, N'FTE', 0)
INSERT INTO [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description], [ShowTotal]) VALUES (14, 9, N'1', 3, N'Total capital managed', 1)
INSERT INTO [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description], [ShowTotal]) VALUES (15, 9, N'2', 3, N'Total revenue managed (contracts/operations)', 1)
INSERT INTO [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description], [ShowTotal]) VALUES (16, 9, N'3', 10, N'Contractors managed', 1)
INSERT INTO [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description], [ShowTotal]) VALUES (17, 9, N'4', 4, N'Total number of containers created', 1)
INSERT INTO [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description], [ShowTotal]) VALUES (18, 9, N'5', 4, N'Number of shipments', 1)
INSERT INTO [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description], [ShowTotal]) VALUES (19, 9, N'6', 5, N'Number of projects completed', 1)
INSERT INTO [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description], [ShowTotal]) VALUES (20, 9, N'7', 4, N'Operations continuity', 1)
INSERT INTO [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description], [ShowTotal]) VALUES (21, 9, N'8', 3, N'Site costs saved / avoidance', 1)
INSERT INTO [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description], [ShowTotal]) VALUES (22, 9, N'9', 2, N'FTE efficiency increase (P&G)', 1)
INSERT INTO [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description], [ShowTotal]) VALUES (23, 10, N'1', 6, N'Employee TIR', 0)
INSERT INTO [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description], [ShowTotal]) VALUES (24, 10, N'2', 6, N'Contractor TIR', 0)
INSERT INTO [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description], [ShowTotal]) VALUES (25, 10, N'3', 11, N'Site capable for HS&E', 0)
INSERT INTO [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description], [ShowTotal]) VALUES (26, 10, N'4', 11, N'Site capable for Fire Protection', 0)
INSERT INTO [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description], [ShowTotal]) VALUES (27, 10, N'5', 4, N'Legal compliance issues/significant incidents', 1)
INSERT INTO [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description], [ShowTotal]) VALUES (28, 10, N'6', 4, N'QI', 1)
INSERT INTO [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description], [ShowTotal]) VALUES (29, 10, N'7', 5, N'R&D Utilities Validated (Comp Air, HVAC, etc)', 1)
INSERT INTO [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description], [ShowTotal]) VALUES (30, 10, N'8', 5, N'R&D Water Systems Validated', 1)
INSERT INTO [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description], [ShowTotal]) VALUES (31, 11, N'1', 7, N'Overall Rating', 1)
INSERT INTO [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description], [ShowTotal]) VALUES (32, 11, N'2', 7, N'In touch (speed to reply)', 1)
INSERT INTO [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description], [ShowTotal]) VALUES (33, 11, N'3', 7, N'Fit For Use solutions', 1)
INSERT INTO [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description], [ShowTotal]) VALUES (34, 11, N'4', 7, N'Simplification', 1)
INSERT INTO [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description], [ShowTotal]) VALUES (35, 11, N'5', 4, N'Recognitions', 1)
INSERT INTO [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description], [ShowTotal]) VALUES (36, 19, N'a', 5, N'Projects meeting success criteria', 1)
INSERT INTO [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description], [ShowTotal]) VALUES (37, 22, N'a', 2, N'FTE efficiency increase (Contractors)', 1)
INSERT INTO [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description], [ShowTotal]) VALUES (38, 28, N'a', 4, N'SQI', 1)
SET IDENTITY_INSERT [dbo].[Line] OFF
