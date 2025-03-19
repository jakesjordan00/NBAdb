select top 5 * 
from playByPlay pbp inner join
        player p on pbp.player_id = p.player_id and p.season_id = 2021 left join
        game g on pbp.game_id = g.game_id
where p.name = 'Kawhi Leonard' and shotType in('2PTM', '3PTM')
and pbp.season_id = 2017 and description like '%ast%'