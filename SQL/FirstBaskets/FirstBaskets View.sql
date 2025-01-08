create view FirstBaskets
as
SELECT p.season_id
      , p.game_id
      , case when p.actionType = 'Foul' and descriptor = 'Shooting' then o.team_id else p.team_id end team_id
      , case when p.actionType = 'Foul' and descriptor = 'Shooting' then player_idFoulDrawn else p.player_id end player_id
      , case when p.actionType = 'Foul' and descriptor = 'Shooting' then o.name else t.name end team
	  , pl.name player
      , case when p.actionType = 'freethrow' then 'FT'
			 when p.actionType = '2pt' then 'FG2'
			 when p.actionType = '3pt' then 'FG3'
			 when p.actionType = 'Foul' and descriptor = 'Shooting' then concat('FG', left(p.qualifier1, 1))
			 else null end Shot
      , case when p.shotResult = 'Made' then 'Make' 
		     when p.shotResult = 'Missed' then 'Miss' 
			 when p.actionType = 'foul' and descriptor = 'Shooting' and p.shotResult is null then 'Fouled'
			 else null end Result
      , actionSub
	  , case when p.actionType = 'Foul' and descriptor = 'Shooting' then t.team_id else o.team_id end Opponent_id
	  , case when p.actionType = 'Foul' and descriptor = 'Shooting' then t.name else o.name end Opponent
	  , case when t.team_id = (select team_id from playByPlay b where p.game_id = b.game_id and p.season_id = b.season_id and actionNumber = 4) then 1 
			 when p.actionType = 'Foul' and o.team_id = (select team_id from playByPlay b where p.game_id = b.game_id and p.season_id = b.season_id and actionNumber = 4) then 1	  
	  else 0 end TipWon
from playByPlay p left join
		game g on p.game_id = g.game_id and p.team_id = g.team_idH and p.season_id = g.season_id left join
		game ga on p.game_id = ga.game_id and p.team_id = ga.team_idA and p.season_id = ga.season_id inner join
		player pl on case when p.actionType = 'Foul' and descriptor = 'Shooting' then player_idFoulDrawn else p.player_id end = pl.player_id
		and p.season_id = pl.season_id inner join
		team t on p.team_id = t.team_id and p.season_id = t.season_id inner join
		team o on t.team_id != o.team_id and (g.team_idA = o.team_id or ga.team_idH = o.team_id) and p.season_id = o.season_id 
where 
((concat(p.scoreHome, p.scoreAway) = '00' and p.shotResult = 'Missed') or
(concat(p.scoreHome, p.scoreAway) in('02', '03', '20', '30')and p.shotResult = 'Made' and ptsGenerated > 1) or
(concat(p.scoreHome, p.scoreAway) in('01', '10')and p.shotResult = 'Made' and ptsGenerated = 1) or
(concat(p.scoreHome, p.scoreAway) = '00' and p.description like '%shooting%' and p.description like '%FOUL%'))
and p.season_id = 2024
order by game_id