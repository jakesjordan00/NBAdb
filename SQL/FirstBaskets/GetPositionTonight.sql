create procedure GetPositionTonight @player int, @team int, @season int
as
select position, (select count(position) from StartingLineups l where s.player_id = l.player_id and s.team_id = l.team_id and s.season_id = l.season_id and s.position = l.position) starts
from StartingLineups s inner join
		GameSchedule g on s.game_id = g.game_id and s.season_id = g.season_id
where s.player_id = @player and s.team_id = @team and s.season_id = @season
and g.date = cast(getdate() as date)
group by position, s.player_id, s.team_id, s.season_id

create procedure GetPosition @player int, @team int, @season int
as
select top 1 position, count(position) Starts
from StartingLineups  s
where s.player_id = @player and s.team_id = @team and s.season_id = @season
group by position
order by Starts desc