USE [ScoreCard]
GO
ALTER TABLE [dbo].[Worker] DROP CONSTRAINT [FK_Worker_ToGroup]
GO
ALTER TABLE [dbo].[Score] DROP CONSTRAINT [FK_Score_ToLine]
GO
ALTER TABLE [dbo].[Responsibility] DROP CONSTRAINT [FK_Responsibility_ToWorker]
GO
ALTER TABLE [dbo].[Responsibility] DROP CONSTRAINT [FK_Responsibility_ToLine]
GO
ALTER TABLE [dbo].[Presence] DROP CONSTRAINT [FK_Presence_ToSite]
GO
ALTER TABLE [dbo].[Presence] DROP CONSTRAINT [FK_Presence_ToGroup]
GO
ALTER TABLE [dbo].[Line] DROP CONSTRAINT [FK_Line_ToMeasure]
GO
ALTER TABLE [dbo].[Line] DROP CONSTRAINT [FK_Line_ToLine]
GO
/****** Object:  Table [dbo].[Worker]    Script Date: 3/31/2015 5:08:54 PM ******/
DROP TABLE [dbo].[Worker]
GO
/****** Object:  Table [dbo].[Site]    Script Date: 3/31/2015 5:08:54 PM ******/
DROP TABLE [dbo].[Site]
GO
/****** Object:  Table [dbo].[Score]    Script Date: 3/31/2015 5:08:54 PM ******/
DROP TABLE [dbo].[Score]
GO
/****** Object:  Table [dbo].[Responsibility]    Script Date: 3/31/2015 5:08:54 PM ******/
DROP TABLE [dbo].[Responsibility]
GO
/****** Object:  Table [dbo].[Presence]    Script Date: 3/31/2015 5:08:54 PM ******/
DROP TABLE [dbo].[Presence]
GO
/****** Object:  Table [dbo].[Measure]    Script Date: 3/31/2015 5:08:54 PM ******/
DROP TABLE [dbo].[Measure]
GO
/****** Object:  Table [dbo].[Line]    Script Date: 3/31/2015 5:08:54 PM ******/
DROP TABLE [dbo].[Line]
GO
/****** Object:  Table [dbo].[Group]    Script Date: 3/31/2015 5:08:54 PM ******/
DROP TABLE [dbo].[Group]
GO
/****** Object:  Table [dbo].[Group]    Script Date: 3/31/2015 5:08:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Group](
	[GroupId] [int] IDENTITY(1,1) NOT NULL,
	[Group] [varchar](10) NOT NULL,
	[GroupName] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON
GO
/****** Object:  Table [dbo].[Line]    Script Date: 3/31/2015 5:08:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Line](
	[LineId] [int] IDENTITY(1,1) NOT NULL,
	[ParentLineId] [int] NOT NULL,
	[Order] [int] NOT NULL,
	[MeasureId] [int] NOT NULL,
	[Description] [varchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[LineId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON
GO
/****** Object:  Table [dbo].[Measure]    Script Date: 3/31/2015 5:08:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Measure](
	[MeasureId] [int] IDENTITY(1,1) NOT NULL,
	[Symbol] [varchar](10) NOT NULL,
	[Mask] [varchar](10) NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[DecimalPoint] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MeasureId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON
GO
/****** Object:  Table [dbo].[Presence]    Script Date: 3/31/2015 5:08:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Presence](
	[PresenceId] [int] IDENTITY(1,1) NOT NULL,
	[GroupId] [int] NOT NULL,
	[SiteId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PresenceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Responsibility]    Script Date: 3/31/2015 5:08:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Responsibility](
	[ResponsibilityId] [int] IDENTITY(1,1) NOT NULL,
	[WorkerId] [int] NOT NULL,
	[LineId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ResponsibilityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Score]    Script Date: 3/31/2015 5:08:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Score](
	[ScoreId] [int] IDENTITY(1,1) NOT NULL,
	[YearEnding] [int] NOT NULL,
	[LineId] [int] NOT NULL,
	[Target] [int] NULL,
	[Q1] [int] NULL,
	[Q2] [int] NULL,
	[Q3] [int] NULL,
	[Q4] [int] NULL,
	[Comment] [varchar](50) NULL,
	[GroupId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ScoreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON
GO
/****** Object:  Table [dbo].[Site]    Script Date: 3/31/2015 5:08:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Site](
	[SiteId] [int] IDENTITY(1,1) NOT NULL,
	[Site] [nvarchar](10) NOT NULL,
	[SiteName] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[SiteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Worker]    Script Date: 3/31/2015 5:08:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Worker](
	[WorkerId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeNumber] [nvarchar](10) NULL,
	[LevelId] [int] NULL,
	[WorkDeptId] [int] NULL,
	[FacilityId] [int] NULL,
	[RoleId] [int] NULL,
	[GroupId] [int] NOT NULL,
	[FirstName] [nvarchar](30) NOT NULL,
	[LastName] [nvarchar](30) NOT NULL,
	[IsManager] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsPartTime] [bit] NOT NULL,
	[OnDisability] [bit] NOT NULL,
	[IonName] [nvarchar](50) NULL,
	[IsAdmin] [bit] NOT NULL,
	[ManagerId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[WorkerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Group] ON 

INSERT [dbo].[Group] ([GroupId], [Group], [GroupName]) VALUES (1, N'RDSS', N'Research & Development Scalable Systems')
INSERT [dbo].[Group] ([GroupId], [Group], [GroupName]) VALUES (2, N'HSE', NULL)
INSERT [dbo].[Group] ([GroupId], [Group], [GroupName]) VALUES (3, N'CMG', NULL)
INSERT [dbo].[Group] ([GroupId], [Group], [GroupName]) VALUES (4, N'QA', NULL)
INSERT [dbo].[Group] ([GroupId], [Group], [GroupName]) VALUES (5, N'VP', NULL)
SET IDENTITY_INSERT [dbo].[Group] OFF
SET IDENTITY_INSERT [dbo].[Line] ON 

INSERT [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (6, 6, 1, 8, N'Financials ($MM)')
INSERT [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (9, 9, 2, 8, N'Performance')
INSERT [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (10, 10, 3, 8, N'Governance')
INSERT [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (11, 11, 4, 8, N'Customer Satisfaction')
INSERT [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (12, 6, 1, 1, N'Budget')
INSERT [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (13, 6, 2, 2, N'FTE')
INSERT [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (14, 9, 1, 3, N'Total capital managed')
INSERT [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (15, 9, 2, 3, N'Total revenue managed (contracts/operations)')
INSERT [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (16, 9, 3, 4, N'Contractors managed')
INSERT [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (17, 9, 4, 4, N'Total number of containers created')
INSERT [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (18, 9, 5, 4, N'Number of shipments')
INSERT [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (19, 9, 6, 5, N'Number of Project completed / Success criteria met')
INSERT [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (20, 9, 7, 4, N'Operations continuity')
INSERT [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (21, 9, 8, 3, N'Site costs saved / avoidance')
INSERT [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (22, 9, 9, 2, N'FTE efficiency increase')
INSERT [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (23, 10, 1, 6, N'Employee TIR')
INSERT [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (24, 10, 2, 6, N'Contractor TIR')
INSERT [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (25, 10, 3, 5, N'Site capable for HS&E')
INSERT [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (26, 10, 4, 5, N'Site capable for Fire Protection')
INSERT [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (27, 10, 5, 4, N'Legal compliance issues/significant incidents')
INSERT [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (28, 10, 6, 4, N'QI/SQI')
INSERT [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (29, 10, 7, 5, N'R&D Utilities Validated (Comp Air, HVAC, etc)')
INSERT [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (30, 10, 8, 5, N'R&D Water Systems Validated')
INSERT [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (31, 11, 1, 7, N'Overall Rating')
INSERT [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (32, 11, 2, 7, N'In touch (speed to reply)')
INSERT [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (33, 11, 3, 7, N'Fit For Use solutions')
INSERT [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (34, 11, 4, 7, N'Simplification')
INSERT [dbo].[Line] ([LineId], [ParentLineId], [Order], [MeasureId], [Description]) VALUES (35, 11, 5, 4, N'Recognitions')
SET IDENTITY_INSERT [dbo].[Line] OFF
SET IDENTITY_INSERT [dbo].[Measure] ON 

INSERT [dbo].[Measure] ([MeasureId], [Symbol], [Mask], [Description], [DecimalPoint]) VALUES (1, N'$MM/%', N'N', N'Million USD', 2)
INSERT [dbo].[Measure] ([MeasureId], [Symbol], [Mask], [Description], [DecimalPoint]) VALUES (2, N'FTE', N'N', N'Full Time Employees', 1)
INSERT [dbo].[Measure] ([MeasureId], [Symbol], [Mask], [Description], [DecimalPoint]) VALUES (3, N'$MM', N'N', N'Million USD', 2)
INSERT [dbo].[Measure] ([MeasureId], [Symbol], [Mask], [Description], [DecimalPoint]) VALUES (4, N'number', N'N', N'Number', 0)
INSERT [dbo].[Measure] ([MeasureId], [Symbol], [Mask], [Description], [DecimalPoint]) VALUES (5, N'%', N'N', N'Percent', 0)
INSERT [dbo].[Measure] ([MeasureId], [Symbol], [Mask], [Description], [DecimalPoint]) VALUES (6, N'TIR', N'N', N'TIR', 2)
INSERT [dbo].[Measure] ([MeasureId], [Symbol], [Mask], [Description], [DecimalPoint]) VALUES (7, N'score', N'N', N'Score', 0)
INSERT [dbo].[Measure] ([MeasureId], [Symbol], [Mask], [Description], [DecimalPoint]) VALUES (8, N'blank', N'0', N'Label', 0)
INSERT [dbo].[Measure] ([MeasureId], [Symbol], [Mask], [Description], [DecimalPoint]) VALUES (9, N'decimal', N'N', N'Decimal', 1)
SET IDENTITY_INSERT [dbo].[Measure] OFF
SET IDENTITY_INSERT [dbo].[Responsibility] ON 

INSERT [dbo].[Responsibility] ([ResponsibilityId], [WorkerId], [LineId]) VALUES (41, 20, 12)
INSERT [dbo].[Responsibility] ([ResponsibilityId], [WorkerId], [LineId]) VALUES (42, 9, 13)
INSERT [dbo].[Responsibility] ([ResponsibilityId], [WorkerId], [LineId]) VALUES (43, 77, 13)
INSERT [dbo].[Responsibility] ([ResponsibilityId], [WorkerId], [LineId]) VALUES (44, 79, 13)
INSERT [dbo].[Responsibility] ([ResponsibilityId], [WorkerId], [LineId]) VALUES (45, 80, 13)
INSERT [dbo].[Responsibility] ([ResponsibilityId], [WorkerId], [LineId]) VALUES (46, 81, 13)
INSERT [dbo].[Responsibility] ([ResponsibilityId], [WorkerId], [LineId]) VALUES (47, 9, 14)
INSERT [dbo].[Responsibility] ([ResponsibilityId], [WorkerId], [LineId]) VALUES (48, 77, 14)
INSERT [dbo].[Responsibility] ([ResponsibilityId], [WorkerId], [LineId]) VALUES (49, 9, 15)
INSERT [dbo].[Responsibility] ([ResponsibilityId], [WorkerId], [LineId]) VALUES (50, 77, 15)
INSERT [dbo].[Responsibility] ([ResponsibilityId], [WorkerId], [LineId]) VALUES (51, 9, 16)
INSERT [dbo].[Responsibility] ([ResponsibilityId], [WorkerId], [LineId]) VALUES (52, 77, 16)
INSERT [dbo].[Responsibility] ([ResponsibilityId], [WorkerId], [LineId]) VALUES (53, 78, 17)
INSERT [dbo].[Responsibility] ([ResponsibilityId], [WorkerId], [LineId]) VALUES (54, 78, 18)
INSERT [dbo].[Responsibility] ([ResponsibilityId], [WorkerId], [LineId]) VALUES (55, 9, 19)
INSERT [dbo].[Responsibility] ([ResponsibilityId], [WorkerId], [LineId]) VALUES (56, 77, 19)
INSERT [dbo].[Responsibility] ([ResponsibilityId], [WorkerId], [LineId]) VALUES (57, 9, 20)
INSERT [dbo].[Responsibility] ([ResponsibilityId], [WorkerId], [LineId]) VALUES (58, 77, 20)
INSERT [dbo].[Responsibility] ([ResponsibilityId], [WorkerId], [LineId]) VALUES (59, 9, 21)
INSERT [dbo].[Responsibility] ([ResponsibilityId], [WorkerId], [LineId]) VALUES (60, 77, 21)
INSERT [dbo].[Responsibility] ([ResponsibilityId], [WorkerId], [LineId]) VALUES (61, 9, 22)
INSERT [dbo].[Responsibility] ([ResponsibilityId], [WorkerId], [LineId]) VALUES (62, 77, 22)
INSERT [dbo].[Responsibility] ([ResponsibilityId], [WorkerId], [LineId]) VALUES (63, 80, 23)
INSERT [dbo].[Responsibility] ([ResponsibilityId], [WorkerId], [LineId]) VALUES (64, 79, 23)
INSERT [dbo].[Responsibility] ([ResponsibilityId], [WorkerId], [LineId]) VALUES (65, 80, 24)
INSERT [dbo].[Responsibility] ([ResponsibilityId], [WorkerId], [LineId]) VALUES (66, 79, 24)
INSERT [dbo].[Responsibility] ([ResponsibilityId], [WorkerId], [LineId]) VALUES (67, 80, 25)
INSERT [dbo].[Responsibility] ([ResponsibilityId], [WorkerId], [LineId]) VALUES (68, 79, 25)
INSERT [dbo].[Responsibility] ([ResponsibilityId], [WorkerId], [LineId]) VALUES (69, 80, 26)
INSERT [dbo].[Responsibility] ([ResponsibilityId], [WorkerId], [LineId]) VALUES (70, 79, 26)
INSERT [dbo].[Responsibility] ([ResponsibilityId], [WorkerId], [LineId]) VALUES (71, 80, 27)
INSERT [dbo].[Responsibility] ([ResponsibilityId], [WorkerId], [LineId]) VALUES (72, 79, 27)
INSERT [dbo].[Responsibility] ([ResponsibilityId], [WorkerId], [LineId]) VALUES (73, 82, 28)
INSERT [dbo].[Responsibility] ([ResponsibilityId], [WorkerId], [LineId]) VALUES (74, 82, 29)
INSERT [dbo].[Responsibility] ([ResponsibilityId], [WorkerId], [LineId]) VALUES (75, 82, 30)
INSERT [dbo].[Responsibility] ([ResponsibilityId], [WorkerId], [LineId]) VALUES (76, 20, 31)
INSERT [dbo].[Responsibility] ([ResponsibilityId], [WorkerId], [LineId]) VALUES (77, 20, 32)
INSERT [dbo].[Responsibility] ([ResponsibilityId], [WorkerId], [LineId]) VALUES (78, 20, 33)
INSERT [dbo].[Responsibility] ([ResponsibilityId], [WorkerId], [LineId]) VALUES (79, 20, 34)
INSERT [dbo].[Responsibility] ([ResponsibilityId], [WorkerId], [LineId]) VALUES (80, 83, 35)
SET IDENTITY_INSERT [dbo].[Responsibility] OFF
SET IDENTITY_INSERT [dbo].[Score] ON 

INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (1, 2015, 16, 3, 2, 4, 1, 3, N'item1', 1)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (2, 2015, 16, 4, 3, 5, 2, 4, N'item2', 2)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (3, 2015, 16, 3, 2, 3, 3, 2, N'item3', 3)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (4, 2015, 16, 5, 6, 4, 5, 5, N'item4', 4)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (5, 2014, 16, 4, 2, 2, 0, 0, N'itema', 1)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (6, 2014, 16, 5, 1, 2, 1, 1, N'itemb', 2)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (7, 2014, 16, 4, 1, 0, 1, 0, N'itemc', 3)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (9, 2015, 20, 5, 3, 4, 4, 5, N'Test1', 1)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (10, 2015, 20, 5, 3, 3, 3, 7, N'Test2', 2)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (11, 2015, 20, 5, 3, 2, 5, 6, N'Test3', 3)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (12, 2015, 20, 5, 3, 1, 2, 7, N'Test4', 4)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (13, 2015, 14, 3, 1, 1, 1, 2, NULL, 1)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (14, 2015, 14, 111, 25, 25, 26, 27, N'Test1', 1)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (15, 2015, 15, 111, 25, 25, 26, 27, N'Test2', 1)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (16, 2015, 15, 111, 25, 25, 26, 27, N'Test3', 2)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (17, 2015, 15, 111, 25, 25, 26, 27, N'Test4', 3)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (18, 2015, 17, 111, 25, 25, 26, 27, N'Test5', 3)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (19, 2015, 18, 111, 25, 25, 26, 27, N'Test6', 3)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (20, 2015, 19, 111, 25, 25, 26, 27, N'Test7', 1)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (21, 2015, 20, 111, 25, 25, 26, 27, N'Test8', 1)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (22, 2015, 20, 111, 25, 25, 26, 27, N'Test9', 2)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (23, 2015, 20, 111, 25, 25, 26, 27, N'Test10', 3)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (24, 2015, 21, 111, 25, 25, 26, 27, N'Test11', 1)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (25, 2015, 21, 111, 25, 25, 26, 27, N'Test12', 2)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (26, 2015, 21, 111, 25, 25, 26, 27, N'Test13', 3)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (27, 2015, 22, 111, 25, 25, 26, 27, N'Test14', 1)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (28, 2015, 22, 111, 25, 25, 26, 27, N'Test15', 2)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (29, 2015, 22, 111, 25, 25, 26, 27, N'Test16', 3)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (30, 2014, 14, 130, 17, 23, 26, 27, N'4Test1', 1)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (31, 2014, 15, 130, 200, 25, 26, 27, N'4Test2', 1)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (32, 2014, 15, 130, 4480, 40, 33, 27, N'4Tex', 2)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (33, 2014, 15, 130, 25, 25, 26, 27, N'4Test4', 3)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (34, 2014, 17, 130, 25, 25, 26, 27, N'4Test5', 3)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (35, 2014, 18, 130, 25, 25, 26, 27, N'4Test6', 3)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (36, 2014, 19, 130, 25, 25, 26, 27, N'4Test7', 1)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (37, 2014, 20, 130, 25, 25, 26, 27, N'4Test8', 1)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (38, 2014, 20, 130, 25, 25, 26, 27, N'4Test9', 2)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (39, 2014, 20, 130, 25, 25, 26, 27, N'4Test10', 3)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (40, 2014, 21, 130, 25, 25, 26, 27, N'4Test11', 1)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (41, 2014, 21, 130, 25, 25, 26, 27, N'4Test12', 2)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (42, 2014, 21, 130, 25, 25, 26, 27, N'4Test13', 3)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (43, 2014, 22, 130, 25, 25, 26, 27, N'4Test14', 1)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (44, 2014, 22, 130, 25, 25, 26, 27, N'4Test15', 2)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (45, 2014, 22, 130, 25, 25, 26, 27, N'4Test16', 3)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (48, 2014, 12, 0, 0, 0, 0, 0, NULL, 0)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (49, 2014, 13, 0, 0, 0, 0, 0, NULL, 0)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (50, 2014, 23, 0, 0, 0, 0, 0, NULL, 0)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (51, 2014, 24, 0, 0, 0, 0, 0, NULL, 0)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (52, 2014, 25, 0, 0, 0, 0, 0, NULL, 0)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (53, 2014, 26, 0, 0, 0, 0, 0, NULL, 0)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (54, 2014, 27, 0, 0, 0, 0, 0, NULL, 0)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (55, 2014, 28, 0, 0, 0, 0, 0, NULL, 0)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (56, 2014, 29, 0, 0, 0, 0, 0, NULL, 0)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (57, 2014, 30, 0, 0, 0, 0, 0, NULL, 0)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (58, 2014, 31, 0, 1, 2, 4, 5, NULL, 0)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (59, 2014, 32, 0, 0, 0, 0, 0, NULL, 0)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (60, 2014, 33, 0, 0, 0, 0, 0, NULL, 0)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (61, 2014, 34, 0, 0, 0, 0, 0, NULL, 0)
INSERT [dbo].[Score] ([ScoreId], [YearEnding], [LineId], [Target], [Q1], [Q2], [Q3], [Q4], [Comment], [GroupId]) VALUES (62, 2014, 35, 0, 0, 0, 0, 0, NULL, 0)
SET IDENTITY_INSERT [dbo].[Score] OFF
SET IDENTITY_INSERT [dbo].[Worker] ON 

INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (2, N'1', 1034671, 1, 12, 12, 1, N'Brian', N'Kiley', 1, 1, 0, 0, N'Kiley.bn', 0, 8)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (3, N'2', 1034951, 1, 4, 7, 1, N'Randy', N'Gross', 0, 0, 0, 0, NULL, 0, 0)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (4, N'3', 1045179, 1, 12, 12, 1, N'Kerry', N'Becker', 1, 1, 0, 0, N'becker.kg', 0, 8)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (5, N'4', 1051237, 1, 11, 0, 1, N'Steve', N'Winbigler', 1, 0, 0, 0, NULL, 0, 0)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (6, N'5', 1130528, 1, 12, 4, 1, N'Kelly', N'Teegarden', 1, 1, 0, 0, N'teegarden.ks', 1, 14)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (7, N'6', 1138951, 1, 12, 11, 1, N'Brian', N'Burns', 1, 1, 0, 0, N'burns.bm', 0, 8)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (8, N'7', 1141335, 1, 12, 7, 1, N'Tim', N'Fiedeldey', 1, 1, 0, 0, N'fiedeldey.tp', 0, 8)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (9, N'8', 1141807, 1, 12, 11, 1, N'April', N'Hills', 1, 0, 0, 0, N'hills.al', 0, 0)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (10, N'9', 1142705, 1, 2, 7, 1, N'Peter', N'Tran', 1, 1, 0, 0, N'tran.pm', 0, 8)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (11, N'10', 1146593, 1, 11, 0, 1, N'Dan', N'Williams', 1, 0, 0, 0, NULL, 0, 0)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (12, N'11', 1147062, 1, 12, 4, 1, N'Bryony', N'Marshall', 1, 1, 0, 1, N'marshall.be', 0, 8)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (13, N'12', 1148540, 1, 12, 11, 1, N'Tom', N'Brady', 1, 1, 0, 0, N'brady.tv', 0, 8)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (14, N'13', 1508001, 1, 3, 6, 1, N'Dave', N'Wise', 1, 0, 0, 0, NULL, 0, 0)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (15, N'14', 1605464, 1, 12, 4, 1, N'Matt', N'Paumier', 1, 1, 0, 0, N'paumier.m', 0, 8)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (16, N'15', 1657999, 1, 12, 11, 1, N'Matt', N'Stophlet', 0, 1, 0, 0, N'stophlet.mg', 0, 12)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (17, N'16', 1658002, 1, 12, 12, 1, N'Terrell', N'Byrd', 1, 1, 0, 0, N'byrd.td', 1, 8)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (18, N'17', 1658008, 1, 12, 7, 1, N'Cai', N'Feng', 1, 1, 0, 0, N'feng.ch', 0, 7)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (19, N'18', 1022062, 3, 9, 7, 1, N'Ngornly', N'Ly', 0, 0, 0, 0, NULL, 0, 0)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (20, N'19', 1033524, 1, 12, 7, 1, N'Mike', N'Burke', 0, 0, 0, 0, N'burke.mc.1', 1, 0)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (21, N'20', 1034926, 3, 9, 7, 1, N'Charlie', N'Mahler', 0, 0, 0, 0, NULL, 0, 0)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (22, N'21', 1037965, 3, 12, 3, 1, N'Steve', N'Kolesar', 0, 0, 0, 0, NULL, 0, 0)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (23, N'22', 1040146, 3, 11, 0, 1, N'Mark', N'Neumann', 0, 0, 0, 0, NULL, 0, 0)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (24, N'23', 1042270, 3, 3, 6, 1, N'Greg', N'Campbell', 0, 0, 0, 0, NULL, 0, 0)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (25, N'24', 1042944, 3, 12, 7, 1, N'Tom', N'Lemmink', 0, 0, 0, 0, NULL, 0, 0)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (26, N'25', 1043877, 3, 11, 0, 1, N'Gary', N'Piatt', 0, 0, 0, 0, NULL, 0, 0)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (27, N'26', 1044794, 3, 12, 4, 1, N'Lou', N'Zeiser', 0, 1, 0, 0, N'zeiser.lr', 0, 14)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (28, N'27', 1049970, 3, 12, 6, 1, N'Gary', N'Manos', 0, 0, 0, 0, NULL, 0, 0)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (29, N'28', 1053294, 3, 12, 11, 1, N'Buzz', N'Watters', 0, 1, 0, 0, N'watters.wa', 0, 12)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (30, N'29', 1054834, 3, 3, 6, 1, N'Tom', N'Dierig', 0, 0, 0, 0, NULL, 0, 0)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (31, N'30', 1054926, 3, 9, 7, 1, N'Dennis', N'Piper', 0, 0, 0, 0, NULL, 0, 0)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (32, N'31', 1085651, 3, 12, 4, 1, N'Rusty', N'Perry', 0, 1, 0, 0, N'perry.r', 0, 14)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (33, N'32', 1085674, 3, 3, 6, 1, N'Bob', N'Swensen', 0, 0, 0, 0, NULL, 0, 0)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (34, N'33', 1101227, 3, 12, 7, 1, N'Doni', N'Hatz', 0, 1, 0, 0, N'hatz.dj', 0, 7)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (35, N'34', 1128653, 3, 9, 7, 1, N'Tom', N'Bender', 0, 0, 0, 0, NULL, 0, 0)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (36, N'35', 1130328, 3, 12, 11, 1, N'Dave', N'Anderson', 0, 1, 0, 0, N'anderson.do', 0, 6)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (37, N'36', 1130454, 3, 11, 0, 1, N'Rob', N'Pfeifer', 0, 0, 0, 0, NULL, 0, 0)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (38, N'37', 1130996, 3, 3, 6, 1, N'Barbara', N'Williams', 0, 0, 0, 0, NULL, 0, 0)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (39, N'38', 1133520, 3, 12, 0, 1, N'John', N'Herlinger', 0, 0, 0, 0, N'herlinger.jp', 0, 0)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (40, N'39', 1133589, 3, 12, 4, 1, N'Kevin', N'Tewell', 0, 1, 0, 0, N'tewell.ka', 0, 14)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (41, N'40', 1142704, 3, 12, 4, 1, N'Mark', N'Wiley', 0, 1, 0, 0, N'wiley.mp', 0, 3)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (42, N'41', 1142787, 3, 12, 4, 1, N'Mike', N'Schell', 0, 1, 0, 0, N'schell.mr', 0, 11)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (43, N'42', 1145679, 3, 12, 12, 1, N'Mike', N'Bell', 0, 1, 0, 0, N'bell.mj', 0, 16)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (44, N'43', 1145912, 3, 12, 7, 1, N'Earl', N'Osborne', 0, 0, 0, 0, NULL, 0, 0)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (45, N'44', 1146264, 3, 12, 4, 1, N'Phil', N'Hughes', 0, 1, 0, 0, N'Hughes.p', 0, 11)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (46, N'45', 1148760, 3, 12, 11, 1, N'Mike', N'Roaden', 0, 1, 0, 0, N'roaden.mr', 0, 6)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (47, N'46', 1502060, 3, 12, 3, 1, N'Dan', N'Schmaltz', 0, 0, 0, 0, NULL, 0, 0)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (48, N'47', 1502103, 3, 12, 7, 1, N'Bill', N'Dunaway', 0, 1, 0, 0, N'dunaway.ws', 0, 7)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (49, N'48', 1502125, 3, 12, 4, 1, N'Bill', N'McLaughlin', 0, 1, 0, 0, N'mclaughlin.wk', 0, 14)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (50, N'49', 1502862, 3, 12, 11, 1, N'Rob', N'Dawn', 0, 1, 0, 0, N'dawn.rk', 0, 6)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (51, N'50', 1502863, 3, 12, 11, 1, N'Ski', N'Buchenau', 0, 1, 0, 0, N'buchenau.sk', 0, 12)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (52, N'51', 1503314, 3, 12, 11, 1, N'Dean', N'Albright', 0, 1, 0, 0, N'albright.dc', 0, 12)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (53, N'52', 1504668, 3, 12, 12, 1, N'Mike', N'Griffin', 0, 1, 0, 0, N'griffin.mc', 0, 16)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (54, N'53', 1505298, 3, 12, 4, 1, N'Freddie', N'Kendall', 0, 1, 0, 0, N'kendall.f', 0, 11)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (55, N'54', 1507087, 3, 12, 6, 1, N'Doug', N'Otting', 0, 0, 0, 0, NULL, 0, 0)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (56, N'55', 1512401, 3, 12, 3, 1, N'Bill', N'Meier', 0, 1, 0, 0, N'meier.we', 0, 3)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (57, N'56', 1514378, 3, 12, 7, 1, N'Kirk', N'Schrotel', 0, 1, 0, 0, N'schrotel.kr.2', 0, 7)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (58, N'57', 1520944, 3, 12, 7, 1, N'Rich', N'Maupin', 0, 1, 0, 1, N'maupin.rc', 0, 7)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (59, N'58', 1523939, 3, 9, 7, 1, N'Dan', N'Conrad', 0, 0, 0, 0, NULL, 0, 0)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (60, N'59', 1525303, 3, 12, 11, 1, N'Corey', N'Moore', 0, 1, 0, 0, N'moore.cd.1', 0, 6)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (61, N'60', 1558237, 3, 12, 4, 1, N'Rick', N'Ponton', 0, 1, 0, 0, N'ponton.rj', 0, 11)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (62, N'61', 1607383, 3, 12, 4, 1, N'Michael', N'Byerly', 0, 1, 0, 0, N'byerly.ms', 1, 5)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (63, N'62', 1612935, 3, 12, 12, 1, N'Stanley', N'Gregory', 0, 1, 0, 0, N'Gregory.sr', 0, 3)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (64, N'63', 1613624, 3, 12, 11, 1, N'Louis', N'Hudepohl', 0, 1, 0, 0, N'hudepohl.lc', 0, 12)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (65, N'64', 1629089, 3, 12, 12, 1, N'Barron', N'Weant', 0, 1, 0, 0, N'Weant.ba', 0, 1)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (66, N'65', 1631373, 3, 12, 12, 1, N'Jody', N'Moye', 0, 1, 0, 0, N'Moye.j', 0, 1)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (67, N'66', 1635456, 3, 12, 7, 1, N'Gregory', N'Dougherty', 0, 1, 0, 0, N'Dougherty.gr', 0, 7)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (68, N'67', 1635956, 3, 12, 12, 1, N'Matt', N'Kiefer', 0, 0, 0, 0, N'kiefer.mj', 0, 16)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (69, N'68', 1635957, 3, 12, 7, 1, N'Matt', N'Mueller', 0, 1, 0, 0, N'Mueller.mr', 0, 7)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (70, N'69', 1657927, 3, 12, 12, 1, N'Mike', N'Galligan', 0, 1, 0, 0, N'Galligan.mj', 0, 9)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (71, N'70', 1658001, 3, 12, 12, 1, N'Dan', N'Machenheimer', 0, 1, 0, 0, N'Machenheimer.de', 0, 3)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (72, N'71', 1658009, 3, 12, 7, 1, N'Sean', N'Thomas', 0, 1, 0, 0, N'thomas.sl', 0, 7)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (73, N'72', 9999999, 6, 12, 1, 1, N'Mark', N'Steiner', 0, 1, 0, 0, N'steiner.ma', 1, NULL)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (74, N'73', 8888888, 3, 12, 7, 1, N'Guy', N'Lister', 0, 1, 0, 0, N'lister.g.1', 1, 0)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (77, NULL, NULL, NULL, NULL, NULL, 2, N'Francesco', N'Agostini', 1, 1, 0, 0, N'agostini.f', 0, NULL)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (78, NULL, NULL, NULL, NULL, NULL, 2, N'Raif', N'Akif', 1, 1, 0, 0, N'akif.r', 0, NULL)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (79, NULL, NULL, NULL, NULL, NULL, 2, N'Konrad', N'Van Gestel', 1, 1, 0, 0, N'vangestel.k', 0, NULL)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (80, NULL, NULL, NULL, NULL, NULL, 2, N'Joan', N'Karner', 1, 1, 0, 0, N'karner.j', 0, NULL)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (81, NULL, NULL, NULL, NULL, NULL, 2, N'Norman', N'Higginson', 1, 1, 0, 0, N'higginson.n', 0, NULL)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (82, NULL, NULL, NULL, NULL, NULL, 2, N'Tim', N'Owens', 1, 1, 0, 0, N'owens.t', 0, NULL)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (83, NULL, NULL, NULL, NULL, NULL, 2, N'Andrew', N'Priessman', 1, 1, 0, 0, N'priessman.a', 0, NULL)
INSERT [dbo].[Worker] ([WorkerId], [EmployeeNumber], [LevelId], [WorkDeptId], [FacilityId], [RoleId], [GroupId], [FirstName], [LastName], [IsManager], [IsActive], [IsPartTime], [OnDisability], [IonName], [IsAdmin], [ManagerId]) VALUES (84, NULL, NULL, NULL, NULL, NULL, 5, N'Michelle', N'Mansfield', 1, 1, 0, 0, N'mansfield.ma', 0, NULL)
SET IDENTITY_INSERT [dbo].[Worker] OFF
ALTER TABLE [dbo].[Line]  WITH CHECK ADD  CONSTRAINT [FK_Line_ToLine] FOREIGN KEY([ParentLineId])
REFERENCES [dbo].[Line] ([LineId])
GO
ALTER TABLE [dbo].[Line] CHECK CONSTRAINT [FK_Line_ToLine]
GO
ALTER TABLE [dbo].[Line]  WITH CHECK ADD  CONSTRAINT [FK_Line_ToMeasure] FOREIGN KEY([MeasureId])
REFERENCES [dbo].[Measure] ([MeasureId])
GO
ALTER TABLE [dbo].[Line] CHECK CONSTRAINT [FK_Line_ToMeasure]
GO
ALTER TABLE [dbo].[Presence]  WITH CHECK ADD  CONSTRAINT [FK_Presence_ToGroup] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Group] ([GroupId])
GO
ALTER TABLE [dbo].[Presence] CHECK CONSTRAINT [FK_Presence_ToGroup]
GO
ALTER TABLE [dbo].[Presence]  WITH CHECK ADD  CONSTRAINT [FK_Presence_ToSite] FOREIGN KEY([SiteId])
REFERENCES [dbo].[Site] ([SiteId])
GO
ALTER TABLE [dbo].[Presence] CHECK CONSTRAINT [FK_Presence_ToSite]
GO
ALTER TABLE [dbo].[Responsibility]  WITH CHECK ADD  CONSTRAINT [FK_Responsibility_ToLine] FOREIGN KEY([LineId])
REFERENCES [dbo].[Line] ([LineId])
GO
ALTER TABLE [dbo].[Responsibility] CHECK CONSTRAINT [FK_Responsibility_ToLine]
GO
ALTER TABLE [dbo].[Responsibility]  WITH NOCHECK ADD  CONSTRAINT [FK_Responsibility_ToWorker] FOREIGN KEY([WorkerId])
REFERENCES [dbo].[Worker] ([WorkerId])
GO
ALTER TABLE [dbo].[Responsibility] CHECK CONSTRAINT [FK_Responsibility_ToWorker]
GO
ALTER TABLE [dbo].[Score]  WITH CHECK ADD  CONSTRAINT [FK_Score_ToLine] FOREIGN KEY([LineId])
REFERENCES [dbo].[Line] ([LineId])
GO
ALTER TABLE [dbo].[Score] CHECK CONSTRAINT [FK_Score_ToLine]
GO
ALTER TABLE [dbo].[Worker]  WITH NOCHECK ADD  CONSTRAINT [FK_Worker_ToGroup] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Group] ([GroupId])
GO
ALTER TABLE [dbo].[Worker] CHECK CONSTRAINT [FK_Worker_ToGroup]
GO
