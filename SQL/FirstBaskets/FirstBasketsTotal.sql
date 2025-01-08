create view FirstBasketsTotal
as
SELECT p.season_id
      , p.game_id
      , p.team_id
      , p.player_id
	  , t.name team
	  , pl.name player
      , case when p.actionType = 'freethrow' then 'FT'
			 when p.actionType = '2pt' then 'FG2'
			 when p.actionType = '3pt' then 'FG3'
			 else null end Shot
      , case when p.shotResult = 'Made' then 'Make' when p.shotResult = 'Missed' then 'Miss' else null end Result
      , actionSub
	  , o.team_id Opponent_id
	  , o.name Opponent
from playByPlay p left join
		game g on p.game_id = g.game_id and p.team_id = g.team_idH and p.season_id = g.season_id left join
		game ga on p.game_id = ga.game_id and p.team_id = ga.team_idA and p.season_id = ga.season_id inner join
		player pl on p.player_id = pl.player_id and p.season_id = pl.season_id inner join
		team t on p.team_id = t.team_id and p.season_id = t.season_id inner join
		team o on t.team_id != o.team_id and (g.team_idA = o.team_id or ga.team_idH = o.team_id) and p.season_id = o.season_id
where 
((concat(p.scoreHome, p.scoreAway) = '00' and p.shotResult = 'Missed') or
(concat(p.scoreHome, p.scoreAway) in('02', '03', '20', '30')and p.shotResult = 'Made' and ptsGenerated > 1) or
(concat(p.scoreHome, p.scoreAway) in('01', '10')and p.shotResult = 'Made' and ptsGenerated = 1))