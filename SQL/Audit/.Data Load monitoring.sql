

select season_id, player_id, count(player_id) count
from player
group by season_id, player_id
order by count desc, season_id desc, player_id desc


select season_id, team_id, count(team_id) count
from team
group by season_id, team_id
order by count(team_id) desc, season_id desc, team_id desc


select season_id, player_id, team_id, count(player_id) count
from PlayerTeam
group by season_id, player_id, team_id
order by count(player_id) desc, season_id desc, player_id desc


select season_id, arena_id, count(arena_id) count
from arena
group by season_id, arena_id
order by count desc, season_id desc, arena_id desc


select season_id, official_id, count(official_id) count
from official
group by season_id, official_id
order by count desc,  season_id desc, official_id desc

---
------
----------
------
---

select season_id, game_id, team_id, count(game_id) count
from teamBox
group by season_id, game_id, team_id
order by count(game_id) desc, season_id desc, game_id desc


select season_id, game_id, team_id, player_id, count(player_id) count
from playerBox
group by season_id, game_id, team_id, player_id
order by count desc, season_id desc, game_id desc


select season_id, game_id, actionNumber, count(actionNumber) count
from playByPlay
group by season_id, game_id, actionNumber
order by count(actionNumber) desc, season_id desc, game_id desc, actionNumber desc


select season_id, game_id, count(game_id) count
from game
group by season_id, game_id
order by count desc, season_id desc, game_id desc

select * 
from BuildLog
order by BuildID, RunID




--select * 
--from PlayoffPicture

--select * 
--from PlayoffBracket





--delete from [arena]
--delete from [game]
--delete from [official]
--delete from [playByPlay]
--delete from [player]
--delete from [playerBox]
--delete from PlayerTeam
--delete from [PlayoffBracket]
--delete from [PlayoffPicture]
--delete from team
--delete from teamBox