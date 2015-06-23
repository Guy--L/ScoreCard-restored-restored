alter table [Responsibility] NOCHECK CONSTRAINT all
insert into [dbo].[Responsibility]  ([lineid], [workerid] ) 
select 39, r.[workerid] from [dbo].[Responsibility] r where lineid = 27
alter table [Responsibility] CHECK CONSTRAINT all
