declare @id int 

update line set [Description] = 'Total expense fund managed (contracts/operations)' where lineid = 15
update line set [Description] = 'Capital Projects Managed > 500M$ (appropriations)' where lineid = 19
update line set [Description] = 'Capital Projects Managed < 500M$' where lineid = 20
go

CREATE TABLE [dbo].[Hide]
(
	[HideId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [LineId] INT NOT NULL, 
    [StartYear] INT NULL, 
    [EndYear] INT NULL
)
go

CREATE TABLE [dbo].[Lock]
(
	[LockId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [LineId] INT NOT NULL, 
    [StartYear] INT NULL, 
    [EndYear] INT NULL
)
go

SET IDENTITY_INSERT [dbo].[Hide] ON
INSERT INTO [dbo].[Hide] ([HideId], [LineId], [StartYear], [EndYear]) VALUES (1, 36, 2016, NULL)
SET IDENTITY_INSERT [dbo].[Hide] OFF
go

INSERT INTO [dbo].[Line] ([ParentLineId], [Order], [MeasureId], [Description], [ShowTotal]) VALUES (17, N'1', 5, N'Integrated CMG performance indicator', 1)
select @id = Scope_Identity()

INSERT INTO [dbo].[Score]  ([YearEnding],[LineId],[GroupId],[SiteId]) VALUES 
	(2014,@id,0,0),
	(2014,@id,3,1),
	(2014,@id,3,2),
	(2014,@id,3,3),
	(2014,@id,3,4),
	(2014,@id,3,5),
	(2014,@id,3,6),
	(2014,@id,3,7),
	(2014,@id,3,8),
	(2014,@id,3,9),
	(2014,@id,3,10),
	(2014,@id,3,11),
	(2014,@id,3,12),
	(2014,@id,3,13),
	(2014,@id,3,14),
	(2014,@id,3,15),
	(2014,@id,3,16),
	(2014,@id,3,17),
	(2014,@id,3,18),
	(2014,@id,3,19),
	(2014,@id,3,20),
	(2014,@id,3,21),
	(2015,@id,0,0),
	(2015,@id,3,1),
	(2015,@id,3,2),
	(2015,@id,3,3),
	(2015,@id,3,4),
	(2015,@id,3,5),
	(2015,@id,3,6),
	(2015,@id,3,7),
	(2015,@id,3,8),
	(2015,@id,3,9),
	(2015,@id,3,10),
	(2015,@id,3,11),
	(2015,@id,3,12),
	(2015,@id,3,13),
	(2015,@id,3,14),
	(2015,@id,3,15),
	(2015,@id,3,16),
	(2015,@id,3,17),
	(2015,@id,3,18),
	(2015,@id,3,19),
	(2015,@id,3,20),
	(2015,@id,3,21),
	(2016,@id,0,0),
	(2016,@id,3,1),
	(2016,@id,3,2),
	(2016,@id,3,3),
	(2016,@id,3,4),
	(2016,@id,3,5),
	(2016,@id,3,6),
	(2016,@id,3,7),
	(2016,@id,3,8),
	(2016,@id,3,9),
	(2016,@id,3,10),
	(2016,@id,3,11),
	(2016,@id,3,12),
	(2016,@id,3,13),
	(2016,@id,3,14),
	(2016,@id,3,15),
	(2016,@id,3,16),
	(2016,@id,3,17),
	(2016,@id,3,18),
	(2016,@id,3,19),
	(2016,@id,3,20),
	(2016,@id,3,21)
go

insert into Responsibility (workerid, lineid)
select workerid, @id from Responsibility where lineid = 17
go

delete from score where lineid = 20 and groupid > 1
go


