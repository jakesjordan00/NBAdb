







create procedure InsertOldPlayByPlayPlayoffData
@season_id int, 
@series_id varchar(50),
@game int,
@game_id int, 
@actionNumber int, 
@clock nvarchar(350) null, 
@period int, 
@team_id int null, 
@teamTricode nvarchar(35) null, 
@player_id int null, 
@x float null, 
@y float null, 
@shotDistance int null, 
@shotResult nvarchar(999) null, 
@isFieldGoal int null, 
@scoreHome int null, 
@scoreAway int null, 
@description nvarchar(355) null, 
@actionType nvarchar(355) null, 
@actionSub nvarchar(355) null, 
@shotType nvarchar(355) null,
@actionID int
as
insert into playByPlayPlayoffs(season_id, game_id, series_id, game, actionNumber, clock, period, team_id, 
teamTricode, player_id, xLegacy, yLegacy, shotDistance, shotResult, isFieldGoal, scoreHome, scoreAway, description, actionType, actionSub, shotType, actionID)
values(
@season_id, 
@game_id, 
@series_id,
@game,
@actionNumber, 
@clock, 
@period, 
@team_id, 
@teamTricode, 
@player_id , 
@x, 
@y, 
@shotDistance, 
@shotResult, 
@isFieldGoal, 
@scoreHome, 
@scoreAway, 
@description, 
@actionType, 
@actionSub, 
@shotType,
@actionID)


