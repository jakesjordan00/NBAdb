
--Player Averages for Parlay Assistant


create procedure ParlayAverages @Player varchar(255), @Team varchar(255), @Season int
as
select season_id Season, player_id, team_id, Name, Team, Games, Minutes, Points, Assists, Rebounds, Blocks, Steals, FGM, FGA, [FG%], FG3M, FG3A, [FG3%], 
'Total' [Win/Loss/Total]
from playerBoxAverage pba
where season_id = @Season
and Name = @Player and Team = @Team
union
select season_id Season, player_id, team_id, Name, Team, Games, Minutes, Points, Assists, Rebounds, Blocks, Steals, FGM, FGA, [FG%], FG3M, FG3A, [FG3%],
case when pba.Win = 1 then 'Win' else 'Loss' end [Win/Loss/Total]
from WLplayerBoxAverage pba
where season_id = @Season
and Name = @Player and Team = @Team
order by Games desc