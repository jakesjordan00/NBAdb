create procedure FillMaximums @season int, @team int, @player int
as
select Max(points) points,
	   Max(assists) assists,
	   Max(reboundsTotal) reboundsTotal, 
	   Max(threePointersMade) threePointersMade, 
	   Max(blocks) blocks, 
	   Max(steals) steals,
	   Max(points) + Max(assists) [PA],
	   Max(points) + Max(reboundsTotal) [PR],
	   Max(assists) + Max(reboundsTotal) [AR],
	   Max(points) + Max(assists) + Max(reboundsTotal) [PAR]
from playerBox p

where p.season_id = @season
and p.team_id = @team
and p.player_id = @player
and p.status = 'ACTIVE'
and replace(replace(p.minutesCalculated, 'PT', ''), 'M', '') >  
(select Minutes from playerBoxAverage a where a.season_id = p.season_id and a.team_id = p.team_id and a.player_id = p.player_id) - 
((select Minutes from playerBoxAverage a where a.season_id = p.season_id and a.team_id = p.team_id and a.player_id = p.player_id)/3)


