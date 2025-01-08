]
create procedure SelectDNPs @team int, @season int
as
select p.team_id, p.player_id, pl.name player,
sum(case when p.Status = 'DNP' then 1 else 0 end) DNPs,
concat(pl.name, ' - ', sum(case when p.Status = 'DNP' then 1 else 0 end)) PlayerDNPs
from PlayerStatuses p inner join
		GameSchedule g on p.game_id = g.game_id and p.season_id = g.season_id inner join
		player pl on p.player_id = pl.player_id and p.season_id = pl.season_id inner join
		playerBoxAverage a on g.season_id = a.season_id and pl.player_id = a.player_id and p.team_id = a.team_id

where p.team_id = @team and p.season_id = @season and g.date < cast(getdate() as date)
and a.Minutes > 10
group by p.team_id, p.player_id, pl.name
order by DNPs desc