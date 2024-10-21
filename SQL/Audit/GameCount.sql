

create procedure GameCount
as
select g.season_id Season, count(g.game_id) Games
from game g
group by g.season_id
order by Season