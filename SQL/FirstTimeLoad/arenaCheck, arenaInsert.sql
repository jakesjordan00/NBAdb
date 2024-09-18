

create procedure arenaCheck @id int, @arena_id int
as
select *, (select count(*) from arena where season_id = @id) Arenas
from arena
where season_id = @id and arena_id = @arena_id
go


create procedure arenaInsert
@id			int,
@arena_id	int,
@team_id	int,
@name		varchar(100),
@city		varchar(50),
@state		varchar(30),
@country	varchar(20)
as
insert into arena values(
@id,
@arena_id,
@team_id,
@name,	
@city,	
@state,
@country)
