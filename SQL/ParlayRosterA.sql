
create procedure ParlayRosterA @team varchar(255), @season int
as
select p.Name Player
from player p inner join
		PlayerTeam pt on p.player_id = pt.player_id and p.season_id = pt.season_id inner join
		team t on pt.team_id = t.team_id and p.season_id = t.season_id
WHERE		CONCAT('(', t.tricode, ') ', t.city, ' ', t.name) = @team and p.season_id = @season
and (LastGameDate is null or LastGameDate < cast(getdate() as date))
order by Player 