create table PlayerTeam(
season_id int,
player_id int,
team_id int,
FirstGameDate date,
LastGameDate date,
Primary Key(season_id, player_id, team_id))
go


create procedure PlayerTeamCheck @player int, @team int, @id int
as
select * 
from PlayerTeam
where season_id = @id and player_id = @player and team_id = @team
go

create procedure PlayerTeamPost @player int, @team int, @id int, @FirstGame datetime
as
insert into PlayerTeam(season_id, player_id, team_id, FirstGameDate) values(@id, @player, @team, cast(@FirstGame as date))
go


create procedure PlayerTeamCheckOtherTeams @player int, @team int, @id int
as
select *
from PlayerTeam
where season_id = @id and player_id = @player and team_id != @team and LastGameDate is null
go

create procedure PlayerTeamGetLastGame @player int, @team int, @id int
as
select Max(g.date) Date
from playerBox p inner join
		game g on p.game_id = g.game_id and p.season_id = g.season_id
where p.season_id = @id and p.player_id = @player and p.team_id = @team 
go

create procedure PlayerTeamUpdateLastGame @player int, @team int, @id int, @LastGame datetime
as
update PlayerTeam set LastGameDate = cast(@LastGame as date) where player_id = @player and team_id = @team and season_id = @id
go