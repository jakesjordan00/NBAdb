
create view TipOffs
as
select b.game_id, g.date, player_idJumpW, player_idJumpL
from playByPlay b inner join
		game g on b.game_id = g.game_id and b.season_id = g.season_id
where b.season_id = 2024 and descriptor = 'startperiod' and actionNumber = 4