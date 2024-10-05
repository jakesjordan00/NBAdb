
select SQRT(sum(SQUARE(b.points - a.Points))/count(b.game_id))
from playerBox b inner join
		player p on b.player_id = p.player_id and b.season_id = p.season_id inner join
		team t on b.team_id = t.team_id and b.season_id = t.season_id inner join
		playerBoxAverage a on p.name = a.Name and concat(t.city, ' ', t.name) = a.Team and b.season_id = a.season_id inner join
		teamBox tb on b.game_id = tb.game_id and t.team_id = tb.team_id and b.season_id = tb.season_id
where b.player_id = 1629627 and b.season_id = 2023 and b.status = 'ACTIVE' and b.minutesCalculated != 'PT00M'
and tb.points - tb.pointsAgainst > 0