
create procedure StartingLineupSelect @team int, @season int
as
select s.team_id, s.player_id, 
case when s.player_id = 1627751 then 'Jakob Poeltl' else s.player end player,

sum(case when position != '' then 1 else 0 end) Starts,
concat(case when s.player_id = 1627751 then 'Jakob Poeltl' else s.player end, ' - ', sum(case when position != '' then 1 else 0 end)) PlayerStarts
from StartingLineups s inner join
		GameSchedule g on s.game_id = g.game_id and s.season_id = g.season_id
where team_id = @team and s.season_id = @season and g.date < cast(getdate() as date)
group by s.team_id, case when s.player_id = 1627751 then 'Jakob Poeltl' else s.player end, s.player_id
order by Starts desc



