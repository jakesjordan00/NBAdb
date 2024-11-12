create procedure FillMinimums @season int, @team int, @player int
as
select Min(points) points,
	   Min(assists) assists,
	   Min(reboundsTotal) reboundsTotal, 
	   Min(threePointersMade) threePointersMade, 
	   Min(blocks) blocks, 
	   Min(steals) steals,
	   Min(points) + Min(assists) [PA],
	   Min(points) + Min(reboundsTotal) [PR],
	   Min(assists) + Min(reboundsTotal) [AR],
	   Min(points) + Min(assists) + Min(reboundsTotal) [PAR]
from playerBox p

where p.season_id = @season
and p.team_id = @team
and p.player_id = @player
and p.status = 'ACTIVE'
and replace(replace(p.minutesCalculated, 'PT', ''), 'M', '') >  
(select Minutes from playerBoxAverage a where a.season_id = p.season_id and a.team_id = p.team_id and a.player_id = p.player_id) - 
((select Minutes from playerBoxAverage a where a.season_id = p.season_id and a.team_id = p.team_id and a.player_id = p.player_id)/3)


