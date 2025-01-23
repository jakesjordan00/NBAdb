

--create procedure TableCount
--as
SELECT COUNT(*) Tables, case when SUM(rows) is null then 0 else sum(rows) end Rows
FROM sys.tables t inner join
		sys.partitions p on t.object_id = p.object_id
WHERE type_desc = 'USER_TABLE'
go

--create procedure Tables
--as
SELECT t.Name, p.rows Rows
from sys.tables t inner join
		sys.partitions p on t.object_id = p.object_id
WHERE type_desc = 'USER_TABLE'
order by Rows desc


SELECT sum(p.rows) Rows
from sys.tables t inner join
		sys.partitions p on t.object_id = p.object_id
WHERE type_desc = 'USER_TABLE'
order by Rows desc
go


create procedure Views
as
SELECT v.Name
from sys.views v 
order by create_date desc

select * from team where season_id = 2024

select distinct concat(substring(cast(date as varchar(20)), 1, 4), substring(cast(date as varchar(20)), 6, 2), substring(cast(date as varchar(20)), 9, 2)) date from GameSchedule where season_id = 2024 and date <= cast(getdate() as date) and gameLabel != 'Preseason' order by date desc