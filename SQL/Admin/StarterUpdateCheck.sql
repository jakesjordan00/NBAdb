create procedure StarterUpdateCheck
as
select distinct concat(substring(cast(date as varchar(20)), 1, 4), substring(cast(date as varchar(20)), 6, 2), substring(cast(date as varchar(20)), 9, 2)) date,
g.game_id, s.lineupStatus
from GameSchedule g left join
		StartingLineups s on g.game_id = s.game_id
where g.season_id = 2024 
and date <= cast(getdate() as date) 
and gameLabel != 'Preseason' 
and (s.lineupStatus != 'Confirmed' or s.lineupStatus is null)
order by date desc, game_id 
