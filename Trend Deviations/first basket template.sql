



select g.date datea, *

from FirstBaskets f inner join
		game g on f.game_id = g.game_id and f.season_id = g.season_id

where f.Opponent = 'Celtics'
order by g.date desc







select g.date datea, case when result = 'Miss' then 1 when result = 'Fouled' then 2 when Result = 'Make' then 3 else 0 end Results, *
from FirstBaskets f inner join
		game g on f.game_id = g.game_id and f.season_id = g.season_id
where f.team = 'Cavaliers'  and player = 'Evan Mobley'
--or f.Opponent = 'Cavaliers'
order by g.date desc, results asc



select * 
from TipOffs where name like '%hay%' or game_id in(
22400174,
22400468,
22400585,
22400674,
22400692)
order by date desc


select * 
from TipOffs where name like '%zubac%' or game_id in(
22400016,
22400034,
22400044,
22400060,
22400071,
22400087,
22400101,
22400127,
22400131,
22400150,
22400168,
22400179,
22400196,
22400200,
22400215,
22400222,
22400243,
22400251,
22400259,
22400274,
22400281,
22400291,
22400314,
22400324,
22400356,
22400368,
22400385,
22400399,
22400426,
22400444,
22400451,
22400466,
22400486,
22400500,
22400515,
22400553,
22400571,
22400575,
22400596,
22400646,
22400659,
22400671,
22400679,
22400697,
22400983,
22401213,
22401226)
order by date desc