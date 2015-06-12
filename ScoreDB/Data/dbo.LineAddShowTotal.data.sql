ALTER TABLE [dbo].[Line] NOCHECK CONSTRAINT ALL
delete from [dbo].[Line]

SET IDENTITY_INSERT [dbo].[Line] ON
INSERT INTO [dbo].[Line] ([LineId], [ShowTotal], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (6,  0, 6, N'1', 8, N'Financials ($MM)')
INSERT INTO [dbo].[Line] ([LineId], [ShowTotal], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (9,  0, 9, N'2', 8, N'Performance')
INSERT INTO [dbo].[Line] ([LineId], [ShowTotal], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (10, 0, 10, N'3', 8, N'Governance')
INSERT INTO [dbo].[Line] ([LineId], [ShowTotal], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (11, 0, 11, N'4', 8, N'Customer Satisfaction')
INSERT INTO [dbo].[Line] ([LineId], [ShowTotal], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (12, 0, 6, N'1', 1, N'Budget')
INSERT INTO [dbo].[Line] ([LineId], [ShowTotal], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (13, 0, 6, N'2', 2, N'FTE')
INSERT INTO [dbo].[Line] ([LineId], [ShowTotal], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (14, 1, 9, N'1', 3, N'Total capital managed')
INSERT INTO [dbo].[Line] ([LineId], [ShowTotal], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (15, 1, 9, N'2', 3, N'Total revenue managed (contracts/operations)')
INSERT INTO [dbo].[Line] ([LineId], [ShowTotal], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (16, 1, 9, N'3', 10, N'Contractors managed')
INSERT INTO [dbo].[Line] ([LineId], [ShowTotal], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (17, 1, 9, N'4', 4, N'Total number of containers created')
INSERT INTO [dbo].[Line] ([LineId], [ShowTotal], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (18, 1, 9, N'5', 4, N'Number of shipments')
INSERT INTO [dbo].[Line] ([LineId], [ShowTotal], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (19, 1, 9, N'6', 5, N'Number of projects completed')
INSERT INTO [dbo].[Line] ([LineId], [ShowTotal], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (20, 1, 9, N'7', 4, N'Operations continuity')
INSERT INTO [dbo].[Line] ([LineId], [ShowTotal], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (21, 1, 9, N'8', 3, N'Site costs saved / avoidance')
INSERT INTO [dbo].[Line] ([LineId], [ShowTotal], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (22, 1, 9, N'9', 2, N'FTE efficiency increase (P&G)')
INSERT INTO [dbo].[Line] ([LineId], [ShowTotal], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (23, 0, 10, N'1', 6, N'Employee TIR')
INSERT INTO [dbo].[Line] ([LineId], [ShowTotal], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (24, 0, 10, N'2', 6, N'Contractor TIR')
INSERT INTO [dbo].[Line] ([LineId], [ShowTotal], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (25, 0, 10, N'3', 5, N'Site capable for HS&E')
INSERT INTO [dbo].[Line] ([LineId], [ShowTotal], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (26, 0, 10, N'4', 5, N'Site capable for Fire Protection')
INSERT INTO [dbo].[Line] ([LineId], [ShowTotal], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (27, 1, 10, N'5', 4, N'Legal compliance issues/significant incidents')
INSERT INTO [dbo].[Line] ([LineId], [ShowTotal], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (28, 1, 10, N'6', 4, N'QI')
INSERT INTO [dbo].[Line] ([LineId], [ShowTotal], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (29, 1, 10, N'7', 5, N'R&D Utilities Validated (Comp Air, HVAC, etc)')
INSERT INTO [dbo].[Line] ([LineId], [ShowTotal], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (30, 1, 10, N'8', 5, N'R&D Water Systems Validated')
INSERT INTO [dbo].[Line] ([LineId], [ShowTotal], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (31, 1, 11, N'1', 7, N'Overall Rating')
INSERT INTO [dbo].[Line] ([LineId], [ShowTotal], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (32, 1, 11, N'2', 7, N'In touch (speed to reply)')
INSERT INTO [dbo].[Line] ([LineId], [ShowTotal], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (33, 1, 11, N'3', 7, N'Fit For Use solutions')
INSERT INTO [dbo].[Line] ([LineId], [ShowTotal], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (34, 1, 11, N'4', 7, N'Simplification')
INSERT INTO [dbo].[Line] ([LineId], [ShowTotal], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (35, 1, 11, N'5', 4, N'Recognitions')
INSERT INTO [dbo].[Line] ([LineId], [ShowTotal], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (36, 1, 19, N'a', 5, N'Projects meeting success criteria')
INSERT INTO [dbo].[Line] ([LineId], [ShowTotal], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (37, 1, 22, N'a', 2, N'FTE efficiency increase (Contractors)')
INSERT INTO [dbo].[Line] ([LineId], [ShowTotal], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (38, 1, 28, N'a', 4, N'SQI')
SET IDENTITY_INSERT [dbo].[Line] OFF

alter TABLE [dbo].[Line] check constraint all