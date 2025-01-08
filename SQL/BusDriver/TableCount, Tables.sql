

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


