
create view TipOffs
as
select distinct b.game_id, g.date, p.player_id, p.name, case when p.player_id = b.player_idJumpW then 1 else 0 end Win,  case when p.player_id = b.player_idJumpL then 1 else 0 end Loss
from player p inner join
		playByPlay b on (p.player_id = b.player_idJumpW or p.player_id = b.player_idJumpL) and p.season_id = b.season_id  and descriptor = 'startperiod' and actionNumber = 4 inner join
		game g on b.game_id = g.game_id and b.season_id = g.season_id
where p.season_id = 2024 and descriptor = 'startperiod' and actionNumber = 4 
