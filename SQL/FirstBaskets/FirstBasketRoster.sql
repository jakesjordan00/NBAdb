create procedure FirstBasketRoster @team int, @season int
as
select distinct p.Name Player, p.player_id PlayerID, count(f.Result) Attempts
from player p inner join
		PlayerTeam pt on p.player_id = pt.player_id and p.season_id = pt.season_id left join
		FirstBaskets f on pt.player_id = f.player_id and pt.team_id = f.team_id and p.season_id = f.season_id
WHERE	pt.team_id = @team and p.season_id = @season
and (LastGameDate is null or LastGameDate < cast(getdate() as date))
group by p.Name, p.player_id
order by Attempts desc