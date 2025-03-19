select concat(season_id, '') season, max(points) points, (select top 1 game_id from playerBox p where p.points =max(b.points) and p.player_id = b.player_id and p.season_id = b.season_id),
concat('https://www.nba.com/game/', '...-vs-...', '-00', (select top 1 game_id from playerBox p where p.points =max(b.points) and p.player_id = b.player_id and p.season_id = b.season_id), '/play-by-play?watchFullGame=true') Link
from playerBox b
where b.player_id = 202695
group by season_id, player_id

union

select concat(season_id, ' playoffs'), max(points) points, (select top 1 game_id from playerBoxPlayoffs p where p.points =max(b.points) and p.player_id = b.player_id and p.season_id = b.season_id),
concat('https://www.nba.com/game/', '...-vs-...', '-00', (select top 1 game_id from playerBoxPlayoffs p where p.points =max(b.points) and p.player_id = b.player_id and p.season_id = b.season_id), '/play-by-play?watchFullGame=true') Link
from playerBoxPlayoffs b
where b.player_id = 202695
group by season_id, player_id
order by season
