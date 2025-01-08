create table StartingLineups(
season_id int,
game_id int,
team_id int,
player_id int,
position varchar(3),
rosterStatus varchar(20),
lineupStatus varchar(20),
home int,
tricode varchar(3),
player varchar(100),
Primary Key (season_id, game_id, team_id, player_id))
go


create procedure StartingLineupInsert 
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
insert into StartingLineups values(
@season_id		,
@game_id		,
@team_id		,
@player_id		,
@position		,
@rosterStatus	,
@lineupStatus	,
@home			,
@tricode		,
@player			)
