

create procedure ParlayTeams @season int
AS 
select concat('(',t.tricode, ') ',t.city, ' ', t.name) Team, team_id TeamID
from Team t
where t.season_id = @season
order by Team 
go


create procedure ParlayRoster @team int, @season int
as
select p.Name Player, p.player_id PlayerID
from player p inner join
		PlayerTeam pt on p.player_id = pt.player_id and p.season_id = pt.season_id 
WHERE	pt.team_id = @team and p.season_id = @season
and (LastGameDate is null or LastGameDate < cast(getdate() as date))
order by Player 
go


/*


SELECT	     player.player_name as Player
		
FROM		 player INNER JOIN 
			 player_season on player.player_id = player_season.player_id INNER JOIN
			 team on player_season.team_id = team.team_id

WHERE		CONCAT('(',team.abbreviation, ') ', team.city, ' ', team.nickname) = @team


*/