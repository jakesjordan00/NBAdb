create procedure ParlayTeamAverageRanksWL @team int, @season int, @win int
as
select * 
from teamBoxAverageRanksWL t inner join
		TeamExtended e on t.team_id = e.team_id and t.season_id = e.season_id
where t.team_id = @team and t.season_id = @season and Win = @win