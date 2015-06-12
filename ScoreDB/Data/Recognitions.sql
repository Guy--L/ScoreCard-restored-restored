go;

SET IDENTITY_INSERT [dbo].[Group] ON
INSERT INTO [dbo].[Group] ([GroupId], [Group], [GroupName], [ADName]) VALUES (7, N'Shares', N'Recognition Shares', NULL)
SET IDENTITY_INSERT [dbo].[Group] OFF
go

SET IDENTITY_INSERT [dbo].[Site] ON
INSERT INTO [dbo].[Site] ([SiteId], [Site], [SiteName]) VALUES (22, N'Platinum', N'Platinum')
INSERT INTO [dbo].[Site] ([SiteId], [Site], [SiteName]) VALUES (23, N'Gold', N'Gold')
INSERT INTO [dbo].[Site] ([SiteId], [Site], [SiteName]) VALUES (24, N'Pearl', N'Pearl')
INSERT INTO [dbo].[Site] ([SiteId], [Site], [SiteName]) VALUES (25, N'Silver', N'Silver')
INSERT INTO [dbo].[Site] ([SiteId], [Site], [SiteName]) VALUES (26, N'E-Card', N'E-Card')
SET IDENTITY_INSERT [dbo].[Site] OFF
go

INSERT [dbo].[Score]  ([YearEnding],[LineId],[GroupId],[SiteId]) VALUES 
	(2014,35,7,22),
	(2014,35,7,23),
	(2014,35,7,24),
	(2014,35,7,25),
	(2014,35,7,26),	
	(2015,35,7,22),
	(2015,35,7,23),
	(2015,35,7,24),
	(2015,35,7,25),
	(2015,35,7,26)
go
