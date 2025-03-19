select distinct pbp.game_id,
concat('=hyperlink("https://www.nba.com/game/', '...-vs-...', '-00', pbp.game_id, '/play-by-play?watchFullGame=true', '", "', teamTricode, ' vs ', 
(select distinct top 1 teamTricode from playByPlayPlayoffs b where pbp.game_id = b.game_id and pbp.team_id != b.team_id and pbp.season_id = b.season_id and teamTricode is not null and teamTricode != ''), 
'")') Link
from playByPlayPlayoffs pbp left join
        player p on pbp.player_id = p.player_id and p.season_id = 2021 left join
        PlayoffSeries s on pbp.series_id = s.series_id left join
        playoffGame g on pbp.game_id = g.game_id
where p.name = 'Kawhi Leonard' 
