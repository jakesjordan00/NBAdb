create procedure officialCheck @official_id	int, @id int
as
select * 
from official 
where official_id = @official_id and season_id = @id
go


create procedure officialInsert
@id int,
@official_id int,
@name		 varchar(50),
@number		 int,
@assignment	 varchar(50)
as
insert into official values(
@id,
@official_id,
@name,
@number,
@assignment
)


