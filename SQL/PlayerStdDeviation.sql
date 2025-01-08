
--Standard Deviation of player average points
create view PlayerStdDeviation
as
select b.season_id, p.player_id, p.name, t.team_id, concat('(', t.tricode, ') ', t.city, ' ', t.name) Team,
Round(SQRT(sum(SQUARE(b.points - a.Points))/count(b.game_id)), 3) PtsDeviation,
	   Round(SQRT(sum(SQUARE(b.assists - a.assists))/count(b.game_id)), 3) AstDeviation,
	   Round(SQRT(sum(SQUARE(b.reboundsTotal - a.Rebounds))/count(b.game_id)), 3) RebDeviation,
	   Round(SQRT(sum(SQUARE(b.blocks - a.blocks))/count(b.game_id)), 3) BlkDeviation,
	   Round(SQRT(sum(SQUARE(b.steals - a.steals))/count(b.game_id)), 3) StlDeviation,
	   Round(SQRT(sum(SQUARE(b.fieldGoalsMade - a.FGM))/count(b.game_id)), 3) FGMDeviation,
	   Round(SQRT(sum(SQUARE(b.fieldGoalsAttempted - a.FGA))/count(b.game_id)), 3) FGADeviation,
	   Round(SQRT(sum(SQUARE(b.threePointersMade - a.FG3M))/count(b.game_id)), 3) FG3MDeviation,
	   Round(SQRT(sum(SQUARE(b.threePointersAttempted - a.FG3A))/count(b.game_id)), 3) FG3ADeviation,
	   Round(SQRT(sum(SQUARE(cast(SUBSTRING(b.minutesCalculated, 3, 2) as int) - a.Minutes))/count(b.game_id)), 3) MinDeviation,
	   Round(SQRT(sum(SQUARE(b.twoPointersMade - a.FG3M))/count(b.game_id)), 3) FG2MDeviation,
	   Round(SQRT(sum(SQUARE(b.twoPointersAttempted - a.FG3A))/count(b.game_id)), 3) FG2ADeviation,
	   Round(SQRT(sum(SQUARE(b.freeThrowsMade - a.FG3M))/count(b.game_id)), 3) FTMDeviation,
	   Round(SQRT(sum(SQUARE(b.freeThrowsAttempted - a.FG3A))/count(b.game_id)), 3) FTADeviation
from playerBox b inner join
		player p on b.player_id = p.player_id and b.season_id = p.season_id inner join
		team t on b.team_id = t.team_id and b.season_id = t.season_id inner join
		playerBoxAverage a on p.player_id = a.player_id and t.team_id= a.team_id and b.season_id = a.season_id inner join
		teamBox tb on b.game_id = tb.game_id and t.team_id = tb.team_id and b.season_id = tb.season_id
where b.status = 'ACTIVE' and b.season_id = 2024
and replace(replace(b.minutesCalculated, 'PT', ''), 'M', '') > (select cast(Minutes as decimal(18, 2))/2 from playerBoxAverage a where a.season_id = b.season_id and a.team_id = b.team_id and a.player_id = b.player_id)
group by b.season_id, p.player_id, p.name, t.team_id, concat('(', t.tricode, ') ', t.city, ' ', t.name)

