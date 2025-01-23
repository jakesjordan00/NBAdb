
--Standard Deviation of player average points
create view PlayerTrend5StdDeviation
as
select  p.player_id, p.name, t.team_id, concat('(', t.tricode, ') ', t.city, ' ', t.name) Team,
Round(SQRT(sum(SQUARE(b.points - a.Points))/count(a.game_id)), 3) PtsDeviation,
	   Round(SQRT(sum(SQUARE(b.assists - a.assists))/count(a.game_id)), 3) AstDeviation,
	   Round(SQRT(sum(SQUARE(b.Rebounds - a.Rebounds))/count(a.game_id)), 3) RebDeviation,
	   Round(SQRT(sum(SQUARE(b.blocks - a.blocks))/count(a.game_id)), 3) BlkDeviation,
	   Round(SQRT(sum(SQUARE(b.steals - a.steals))/count(a.game_id)), 3) StlDeviation,
	   Round(SQRT(sum(SQUARE(b.FGM - a.FGM))/count(a.game_id)), 3) FGMDeviation,
	   Round(SQRT(sum(SQUARE(b.fga - a.FGA))/count(a.game_id)), 3) FGADeviation,
	   Round(SQRT(sum(SQUARE(b.FG3M - a.FG3M))/count(a.game_id)), 3) FG3MDeviation,
	   Round(SQRT(sum(SQUARE(b.FG3A - a.FG3A))/count(a.game_id)), 3) FG3ADeviation,
	   Round(SQRT(sum(SQUARE(cast(b.Minutes as int) - a.Minutes))/count(a.game_id)), 3) MinDeviation,
	   Round(SQRT(sum(SQUARE(b.FG2M - a.FG3M))/count(a.game_id)), 3) FG2MDeviation,
	   Round(SQRT(sum(SQUARE(b.FG2A - a.FG3A))/count(a.game_id)), 3) FG2ADeviation,
	   Round(SQRT(sum(SQUARE(b.FTM - a.FG3M))/count(a.game_id)), 3) FTMDeviation,
	   Round(SQRT(sum(SQUARE(b.FTA - a.FG3A))/count(a.game_id)), 3) FTADeviation,
	   Round(SQRT(sum(SQUARE(b.[P+A] - a.[P+A]))/count(a.game_id)), 3) [P+ADeviation],
	   Round(SQRT(sum(SQUARE(b.[P+R] - a.[P+R]))/count(a.game_id)), 3) [P+RDeviation],
	   Round(SQRT(sum(SQUARE(b.[A+R] - a.[A+R]))/count(a.game_id)), 3) [A+RDeviation],
	   Round(SQRT(sum(SQUARE(b.[P+A+R] - a.[P+A+R]))/count(a.game_id)), 3) [P+A+RDeviation]
from PlayerTrend5 b inner join
		player p on b.player_id = p.player_id inner join
		team t on b.team_id = t.team_id and p.season_id = t.season_id inner join
		PlayerTrend5Detail a on p.player_id = a.player_id and t.team_id= a.team_id inner join
		teamBox tb on a.game_id = tb.game_id and t.team_id = tb.team_id and p.season_id = tb.season_id
where p.season_id = 2024

and b.Minutes > 15
group by  p.player_id, p.name, t.team_id, concat('(', t.tricode, ') ', t.city, ' ', t.name)

