

create procedure teamCheck @id int, @team_id int
as
select *, (select count(*) from team where season_id = @id) teams
from team
where season_id = @id and team_id = @team_id
go


create procedure teamInsert
@id   int,
@team_id	 int, 
@tricode	 varchar(3),
@city		 varchar(30),
@name		 varchar(30),
@yearFounded int			
as
insert into team values(
@id,
@team_id,	
@tricode,	
@city,		
@name,		
@yearFounded
)
