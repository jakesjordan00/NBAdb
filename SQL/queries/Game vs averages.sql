select g.date, p.name, p.player_id, case when b.position is null then p.position else b.position end position, 
SUBSTRING(b.minutesCalculated, 3, 2) Min, 
a.Minutes MinAvg,
b.points Pts, 
concat(b.fieldGoalsMade, '/', b.fieldGoalsAttempted) FG,
concat(b.twoPointersMade, '/', b.twoPointersAttempted) FG2,
concat(b.threePointersMade, '/', b.threePointersAttempted) FG3,

b.assists Ast, 
a.Assists AstAvg,

b.reboundsTotal Reb, 
a.Rebounds RebAvg
from playerBox b inner join
		teamBox t on b.game_id = t.game_id and b.team_id = t.team_id and b.season_id = t.season_id inner join
		player p on b.player_id = p.player_id and b.season_id = p.season_id inner join
		game g on b.game_id = g.game_id and b.season_id = g.season_id inner join
		playerBoxAverage a on b.player_id = a.player_id and b.season_id = a.season_id
where b.season_id = 2024 and t.matchup_id = 1610612741
order by g.date desc, Min desc
