

create procedure TipWins @player int
as
select
(select count(player_idJumpW)
from playByPlay b
where b.season_id = 2024 and descriptor = 'startperiod' and actionNumber = 4
and player_idJumpW = @player) Wins, 
(select count(player_idJumpL)
from playByPlay b
where b.season_id = 2024 and descriptor = 'startperiod' and actionNumber = 4
and player_idJumpL = @player) Losses