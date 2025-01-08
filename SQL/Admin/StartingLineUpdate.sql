create procedure StartingLineUpdate
@season_id		int,
@game_id		int,
@team_id		int,
@player_id		int,
@position		varchar(3),
@rosterStatus	varchar(20),
@lineupStatus	varchar(20),
@home			int,
@tricode		varchar(3),
@player			varchar(100)
as
update StartingLineups set 
position = @position,  
rosterStatus = @rosterStatus, 
lineupStatus = @lineupStatus,
home = @home, 
tricode = @tricode, 
player = @player
where game_id = @game_id 
and team_id = @team_id 
and player_id = @player_id 
and season_id = @season_id