create procedure LoadSavedParlay @ID int
as
select *, concat('(', tricode, ') ', City, ' ', Name) Team
from SavedParlays s inner join
		team t on s.team_id = t.team_id and t.season_id = (select max(season_id) from team)
where ID = @ID
order by ID