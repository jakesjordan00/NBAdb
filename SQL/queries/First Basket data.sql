select p.season_id, t.name, t.team_id, pl.name, 
	   p.actionType, p.shotResult, 
	   count(shotResult) Count
from playByPlay p left join
		game g on p.game_id = g.game_id and p.team_id = g.team_idH and p.season_id = g.season_id left join
		game ga on p.game_id = ga.game_id and p.team_id = ga.team_idA and p.season_id = ga.season_id inner join
		player pl on p.player_id = pl.player_id and p.season_id = pl.season_id inner join
		team t on p.team_id = t.team_id and p.season_id = t.season_id
where actionType = '3pt' and p.season_id = 2024 and shotResult = 'made'
and ((case when g.game_id is not null then 'Home' else 'Away' end = 'Home' and scoreHome = 3 and scoreAway = 0)
or(case when g.game_id is not null then 'Home' else 'Away' end = 'Away' and scoreHome = 0 and scoreAway = 3))
group by p.season_id, p.shotResult, t.name, t.team_id, pl.name, p.actionType
union
select p.season_id, t.name, t.team_id, pl.name, 
	   p.actionType, p.shotResult, 
	   count(shotResult) Count
from playByPlay p left join
		game g on p.game_id = g.game_id and p.team_id = g.team_idH and p.season_id = g.season_id left join
		game ga on p.game_id = ga.game_id and p.team_id = ga.team_idA and p.season_id = ga.season_id inner join
		player pl on p.player_id = pl.player_id and p.season_id = pl.season_id inner join
		team t on p.team_id = t.team_id and p.season_id = t.season_id
where actionType = '3pt' and p.season_id = 2024 and shotResult = 'missed'
and ((case when g.game_id is not null then 'Home' else 'Away' end = 'Home' and scoreHome = 0 and scoreAway = 0)
or(case when g.game_id is not null then 'Home' else 'Away' end = 'Away' and scoreHome = 0 and scoreAway = 0))
group by p.season_id, p.shotResult, t.name, t.team_id, pl.name, p.actionType



union
select p.season_id, t.name, t.team_id, pl.name, 
	   p.actionType, p.shotResult, 
	   count(shotResult) Count
from playByPlay p left join
		game g on p.game_id = g.game_id and p.team_id = g.team_idH and p.season_id = g.season_id left join
		game ga on p.game_id = ga.game_id and p.team_id = ga.team_idA and p.season_id = ga.season_id inner join
		player pl on p.player_id = pl.player_id and p.season_id = pl.season_id inner join
		team t on p.team_id = t.team_id and p.season_id = t.season_id
where actionType = '2pt' and p.season_id = 2024 and shotResult = 'made'
and ((case when g.game_id is not null then 'Home' else 'Away' end = 'Home' and scoreHome = 2 and scoreAway = 0)
or(case when g.game_id is not null then 'Home' else 'Away' end = 'Away' and scoreHome = 0 and scoreAway = 2))
group by p.season_id, p.shotResult, t.name, t.team_id, pl.name, p.actionType
union
select p.season_id, t.name, t.team_id, pl.name, 
	   p.actionType, p.shotResult, 
	   count(shotResult) Count
from playByPlay p left join
		game g on p.game_id = g.game_id and p.team_id = g.team_idH and p.season_id = g.season_id left join
		game ga on p.game_id = ga.game_id and p.team_id = ga.team_idA and p.season_id = ga.season_id inner join
		player pl on p.player_id = pl.player_id and p.season_id = pl.season_id inner join
		team t on p.team_id = t.team_id and p.season_id = t.season_id
where actionType = '2pt' and p.season_id = 2024 and shotResult = 'missed'
and ((case when g.game_id is not null then 'Home' else 'Away' end = 'Home' and scoreHome = 0 and scoreAway = 0)
or(case when g.game_id is not null then 'Home' else 'Away' end = 'Away' and scoreHome = 0 and scoreAway = 0))
group by p.season_id, p.shotResult, t.name, t.team_id, pl.name, p.actionType



union
select p.season_id, t.name, t.team_id, pl.name, 
	   p.actionType, 'Total' Total, 
	   count(shotResult) Count
from playByPlay p left join
		game g on p.game_id = g.game_id and p.team_id = g.team_idH and p.season_id = g.season_id left join
		game ga on p.game_id = ga.game_id and p.team_id = ga.team_idA and p.season_id = ga.season_id inner join
		player pl on p.player_id = pl.player_id and p.season_id = pl.season_id inner join
		team t on p.team_id = t.team_id and p.season_id = t.season_id
where actionType = '2pt' and p.season_id = 2024 and (shotResult = 'missed'
and ((case when g.game_id is not null then 'Home' else 'Away' end = 'Home' and scoreHome = 0 and scoreAway = 0)
or(case when g.game_id is not null then 'Home' else 'Away' end = 'Away' and scoreHome = 0 and scoreAway = 0)) 
or(shotResult = 'made'
and ((case when g.game_id is not null then 'Home' else 'Away' end = 'Home' and scoreHome = 2 and scoreAway = 0)
or(case when g.game_id is not null then 'Home' else 'Away' end = 'Away' and scoreHome = 0 and scoreAway = 2))))
group by p.season_id, t.name, t.team_id, pl.name, p.actionType
union
select p.season_id, t.name, t.team_id, pl.name, 
	   p.actionType, 'Total' Total, 
	   count(shotResult) Count
from playByPlay p left join
		game g on p.game_id = g.game_id and p.team_id = g.team_idH and p.season_id = g.season_id left join
		game ga on p.game_id = ga.game_id and p.team_id = ga.team_idA and p.season_id = ga.season_id inner join
		player pl on p.player_id = pl.player_id and p.season_id = pl.season_id inner join
		team t on p.team_id = t.team_id and p.season_id = t.season_id
where actionType = '3pt' and p.season_id = 2024 and (shotResult = 'missed'
and ((case when g.game_id is not null then 'Home' else 'Away' end = 'Home' and scoreHome = 0 and scoreAway = 0)
or(case when g.game_id is not null then 'Home' else 'Away' end = 'Away' and scoreHome = 0 and scoreAway = 0)) or(shotResult = 'made'
and ((case when g.game_id is not null then 'Home' else 'Away' end = 'Home' and scoreHome = 3 and scoreAway = 0)
or(case when g.game_id is not null then 'Home' else 'Away' end = 'Away' and scoreHome = 0 and scoreAway = 3))))
group by p.season_id, t.name, t.team_id, pl.name, p.actionType


union
select p.season_id, t.name, t.team_id,  '', 
	   p.actionType, 'Total' Total, 
	   count(shotResult) Count
from playByPlay p left join
		game g on p.game_id = g.game_id and p.team_id = g.team_idH and p.season_id = g.season_id left join
		game ga on p.game_id = ga.game_id and p.team_id = ga.team_idA and p.season_id = ga.season_id inner join
		player pl on p.player_id = pl.player_id and p.season_id = pl.season_id inner join
		team t on p.team_id = t.team_id and p.season_id = t.season_id
where actionType = '2pt' and p.season_id = 2024 and (shotResult = 'missed'
and ((case when g.game_id is not null then 'Home' else 'Away' end = 'Home' and scoreHome = 0 and scoreAway = 0)
or(case when g.game_id is not null then 'Home' else 'Away' end = 'Away' and scoreHome = 0 and scoreAway = 0)) 
or(shotResult = 'made'
and ((case when g.game_id is not null then 'Home' else 'Away' end = 'Home' and scoreHome = 2 and scoreAway = 0)
or(case when g.game_id is not null then 'Home' else 'Away' end = 'Away' and scoreHome = 0 and scoreAway = 2))))
group by p.season_id, t.name, t.team_id,  p.actionType
union
select p.season_id, t.name, t.team_id, '', 
	   p.actionType, 'Total' Total, 
	   count(shotResult) Count
from playByPlay p left join
		game g on p.game_id = g.game_id and p.team_id = g.team_idH and p.season_id = g.season_id left join
		game ga on p.game_id = ga.game_id and p.team_id = ga.team_idA and p.season_id = ga.season_id inner join
		player pl on p.player_id = pl.player_id and p.season_id = pl.season_id inner join
		team t on p.team_id = t.team_id and p.season_id = t.season_id
where actionType = '3pt' and p.season_id = 2024 and (shotResult = 'missed'
and ((case when g.game_id is not null then 'Home' else 'Away' end = 'Home' and scoreHome = 0 and scoreAway = 0)
or(case when g.game_id is not null then 'Home' else 'Away' end = 'Away' and scoreHome = 0 and scoreAway = 0)) or(shotResult = 'made'
and ((case when g.game_id is not null then 'Home' else 'Away' end = 'Home' and scoreHome = 3 and scoreAway = 0)
or(case when g.game_id is not null then 'Home' else 'Away' end = 'Away' and scoreHome = 0 and scoreAway = 3))))
group by p.season_id, t.name, t.team_id,  p.actionType


union
select p.season_id, t.name, t.team_id, '', 
	   'Total' Total, 'Total' Total1, 
	   count(shotResult) Count
from playByPlay p left join
		game g on p.game_id = g.game_id and p.team_id = g.team_idH and p.season_id = g.season_id left join
		game ga on p.game_id = ga.game_id and p.team_id = ga.team_idA and p.season_id = ga.season_id inner join
		player pl on p.player_id = pl.player_id and p.season_id = pl.season_id inner join
		team t on p.team_id = t.team_id and p.season_id = t.season_id
where (actionType = '3pt' and p.season_id = 2024 and (shotResult = 'missed'
and ((case when g.game_id is not null then 'Home' else 'Away' end = 'Home' and scoreHome = 0 and scoreAway = 0)
or(case when g.game_id is not null then 'Home' else 'Away' end = 'Away' and scoreHome = 0 and scoreAway = 0)) or(shotResult = 'made'
and ((case when g.game_id is not null then 'Home' else 'Away' end = 'Home' and scoreHome = 3 and scoreAway = 0)
or(case when g.game_id is not null then 'Home' else 'Away' end = 'Away' and scoreHome = 0 and scoreAway = 3))))) or(
actionType = '2pt' and p.season_id = 2024 and (shotResult = 'missed'
and ((case when g.game_id is not null then 'Home' else 'Away' end = 'Home' and scoreHome = 0 and scoreAway = 0)
or(case when g.game_id is not null then 'Home' else 'Away' end = 'Away' and scoreHome = 0 and scoreAway = 0)) 
or(shotResult = 'made'
and ((case when g.game_id is not null then 'Home' else 'Away' end = 'Home' and scoreHome = 2 and scoreAway = 0)
or(case when g.game_id is not null then 'Home' else 'Away' end = 'Away' and scoreHome = 0 and scoreAway = 2)))))
group by p.season_id, t.name, t.team_id


order by count desc