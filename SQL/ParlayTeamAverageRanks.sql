create procedure ParlayTeamAverageRanks @team int, @season int
as
select * 
from teamBoxAverageRanks t inner join
		TeamExtended e on t.team_id = e.team_id and t.season_id = e.season_id
where t.team_id = @team and t.season_id = @season


select t.reboundsDefensive, t.reboundsOffensive, t.reboundsTotal, t.reboundsPersonal, t.reboundsTeam, *
from teamBox t




select * from teamBoxAverageRanks
where season_id = 2024 and team_id = 1610612756

select * from teamBox 
where season_id = 2024 and team_id = 1610612756