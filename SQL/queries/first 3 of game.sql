


--select * from PlayerTeam pt where pt.team_id = 1610612738
--and pt.player_id = 201143




select p.season_id, case when g.game_id is not null then 'Home' else 'Away' end HomeAway, 
	   p.shotResult, t.name, pl.name, p.actionType, p.shotResult, *
from playByPlay p left join
		game g on p.game_id = g.game_id and p.team_id = g.team_idH and p.season_id = g.season_id left join
		game ga on p.game_id = ga.game_id and p.team_id = ga.team_idA and p.season_id = ga.season_id inner join
		player pl on p.player_id = pl.player_id and p.season_id = pl.season_id inner join
		team t on p.team_id = t.team_id and p.season_id = t.season_id
where p.team_id = 1610612738 --and p.player_id = 201143 
and actionType = '3pt' 
and ((case when g.game_id is not null then 'Home' else 'Away' end = 'Home' and scoreHome = 3 and scoreAway = 0)
or(case when g.game_id is not null then 'Home' else 'Away' end = 'Away' and scoreHome = 0 and scoreAway = 3))

order by p.game_id desc

--(and (scoreHome = 0 and scoreAway = 0 and shotResult = 'Missed') or (scoreHome = 3 and scoreaway = 0