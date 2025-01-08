create procedure FirstBasketCenters @team int
as
select distinct t.team_id, t.name, p.player_id PlayerID, p.name Player
from playByPlay b inner join
		PlayerTeam pt on b.player_idJumpL = pt.player_id and b.season_id = pt.season_id inner join
		player p on pt.player_id = p.player_id and b.season_id = p.season_id inner join
		team t on pt.team_id = t.team_id and b.season_id = t.season_id
where b.season_id = 2024
and pt.LastGameDate is null
and descriptor = 'startperiod' and actionNumber = 4 and t.team_id = @team
union
select distinct t.team_id, t.name, p.player_id PlayerID, p.name Player
from playByPlay b inner join
		PlayerTeam pt on b.player_idJumpW = pt.player_id and b.season_id = pt.season_id inner join
		player p on pt.player_id = p.player_id and b.season_id = p.season_id inner join
		team t on pt.team_id = t.team_id and b.season_id = t.season_id
where b.season_id = 2024 and descriptor = 'startperiod' and actionNumber = 4
and pt.LastGameDate is null and t.team_id = @team