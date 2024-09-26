

create PROCEDURE ParlayTeams
AS 
select concat('(',t.tricode, ') ',t.city, ' ', t.name) Team 
from Team t
where t.season_id = (select max(season_id) from team)
go


create procedure ParlayRoster @team varchar(255)
as
select p.Name Player
from player p inner join
		PlayerTeam pt on p.player_id = pt.player_id and p.season_id = pt.season_id inner join
		team t on pt.team_id = t.team_id and p.season_id = t.season_id
WHERE		CONCAT('(', t.tricode, ') ', t.city, ' ', t.name) = @team and p.season_id = (select max(season_id) from team)
and (LastGameDate is null or LastGameDate < cast(getdate() as date))
go


/*


SELECT	     player.player_name as Player
		
FROM		 player INNER JOIN 
			 player_season on player.player_id = player_season.player_id INNER JOIN
			 team on player_season.team_id = team.team_id

WHERE		CONCAT('(',team.abbreviation, ') ', team.city, ' ', team.nickname) = @team


*/