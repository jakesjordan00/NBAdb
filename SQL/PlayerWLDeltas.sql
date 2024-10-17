

create view PlayerWLDeltas
as
select season_id Season, player_id, team_id, Name, Team, Games, Minutes, Points, Assists, Rebounds, Blocks, Steals, 
	   FGM, FGA, [FG%], FG3M, FG3A, [FG3%], 
Round(Minutes -(select Minutes from WLplayerBoxAverage l where l.season_id = pba.season_id and l.player_id = pba.player_id and l.team_id = pba.team_id and l.Win = 0), 2)  MinutesDelta,
Round(Points - (select Points from WLplayerBoxAverage l where l.season_id = pba.season_id and l.player_id = pba.player_id and l.team_id = pba.team_id and l.Win = 0), 2)   PointsDelta,
Round(Assists - (select Assists from WLplayerBoxAverage l where l.season_id = pba.season_id and l.player_id = pba.player_id and l.team_id = pba.team_id and l.Win = 0), 2) AssistsDelta,
Round(Rebounds - (select Rebounds from WLplayerBoxAverage l where l.season_id = pba.season_id and l.player_id = pba.player_id and l.team_id = pba.team_id and l.Win = 0), 2) ReboundsDelta,
Round(Blocks - (select Blocks from WLplayerBoxAverage l where l.season_id = pba.season_id and l.player_id = pba.player_id and l.team_id = pba.team_id and l.Win = 0), 2)   BlocksDelta,
Round(Steals - (select Steals from WLplayerBoxAverage l where l.season_id = pba.season_id and l.player_id = pba.player_id and l.team_id = pba.team_id and l.Win = 0), 2)   StealsDelta,
Round(FGM - (select FGM from WLplayerBoxAverage l where l.season_id = pba.season_id and l.player_id = pba.player_id and l.team_id = pba.team_id and l.Win = 0), 2)		 FGMDelta,
Round(FGA - (select FGA from WLplayerBoxAverage l where l.season_id = pba.season_id and l.player_id = pba.player_id and l.team_id = pba.team_id and l.Win = 0), 2)		 FGADelta,
Round([FG%] - (select [FG%] from WLplayerBoxAverage l where l.season_id = pba.season_id and l.player_id = pba.player_id and l.team_id = pba.team_id and l.Win = 0), 2) [FG%Delta],
Round(FG3M - (select FG3M from WLplayerBoxAverage l where l.season_id = pba.season_id and l.player_id = pba.player_id and l.team_id = pba.team_id and l.Win = 0), 2) FG3MDelta,
Round(FG3A - (select FG3A from WLplayerBoxAverage l where l.season_id = pba.season_id and l.player_id = pba.player_id and l.team_id = pba.team_id and l.Win = 0), 2) FG3ADelta,
Round([FG3%] - (select [FG3%] from WLplayerBoxAverage l where l.season_id = pba.season_id and l.player_id = pba.player_id and l.team_id = pba.team_id and l.Win = 0), 2) [FG3%Delta]
from WLplayerBoxAverage pba
where pba.Win = 1
