create procedure ToolboxAverages @season int, @player int, @team int
as
select season_id Season, pba.player_id, pba.team_id, pba.Name, pba.Team, pba.Games, pba.Minutes, pba.Points, pba.Assists, pba.Rebounds, pba.Blocks, pba.Steals, pba.FGM, pba.FGA, pba.[FG%], pba.FG3M, pba.FG3A, pba.[FG3%], pba.FG2M, pba.FG2A, pba.[FG2%], pba.FTM, pba.FTA, pba.[FT%],
pt.Minutes		TrendMinutes, 	
pt.Points	    TrendPoints, 
pt.Assists	    TrendAssists, 
pt.Rebounds     TrendRebounds, 
pt.Blocks	    TrendBlocks, 
pt.Steals	    TrendSteals, 
pt.FGM		    TrendFGM, 
pt.FGA		    TrendFGA, 
pt.[FG%]	    [TrendFG%], 
pt.FG3M		    TrendFG3M, 
pt.FG3A		    TrendFG3A, 
pt.[FG3%]	    [TrendFG3%], 
pt.FG2M         TrendFG2M, 
pt.FG2A         TrendFG2A, 
pt.[FG2%]       [TrendFG2%], 
pt.FTM          TrendFTM, 
pt.FTA          TrendFTA, 
pt.[FT%]        [TrendFT%]
from playerBoxAverage pba inner join
		PlayerTrend pt on pba.player_id = pt.player_id
where season_id = @Season
and pba.player_id = @player and team_id = @team
go


create procedure ToolboxAveragesWL @season int, @player int, @team int, @win int
as
select season_id Season, player_id, team_id, Name, Team, Games, Minutes, Points, Assists, Rebounds, Blocks, Steals, FGM, FGA, [FG%], FG3M, FG3A, [FG3%], FG2M, FG2A, [FG2%], FTM, FTA, [FT%]
from WLplayerBoxAverage pba
where season_id = @Season and player_id = @player and team_id = @team and Win = @win
