select *
from playerBox 
where left(cast(game_id as varchar(255)), 1) = '1'



select *
from playByPlay 
where left(cast(game_id as varchar(255)), 1) = '1'

select *
from teamBox 
where left(cast(game_id as varchar(255)), 1) = '1'

select *
from GameSchedule 
where left(cast(game_id as varchar(255)), 1) = '1'

select *
from StartingLineups 
where left(cast(game_id as varchar(255)), 1) = '1'

select *
from game 
where left(cast(game_id as varchar(255)), 1) = '1'