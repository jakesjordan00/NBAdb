create procedure LoadParlays
as
select *
from SavedParlays s inner join
		team t on s.team_id = t.team_id and t.season_id = (select max(season_id) from team)
order by ID

