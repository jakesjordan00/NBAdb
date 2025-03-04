


create procedure OldPlayerBoxPlayoffsInsert 
 @season_id int
,@series_id	varchar(30)
,@game_id int
,@game int
,@team_id int
,@player_id int
,@status varchar(255)
,@starter int
,@position varchar(255)
,@points int
,@assists int
,@blocks int
,@fieldGoalsAttempted int
,@fieldGoalsMade int
,@fieldGoalsPercentage float
,@foulsPersonal int
,@freeThrowsAttempted int
,@freeThrowsMade int
,@freeThrowsPercentage float
,@minutes varchar(255)
,@minutesCalculated varchar(255)
,@plusMinusPoints int
,@reboundsDefensive int
,@reboundsOffensive int
,@reboundsTotal int
,@steals int
,@threePointersAttempted int
,@threePointersMade int
,@threePointersPercentage float
,@turnovers int
,@twoPointersAttempted int
,@twoPointersMade int
,@twoPointersPercentage float as
insert into playerBoxPlayoffs(season_id,series_id, game_id, game, team_id, player_id, status, starter, position, points, assists, blocks, fieldGoalsAttempted, fieldGoalsMade, fieldGoalsPercentage, foulsPersonal, freeThrowsAttempted, freeThrowsMade, freeThrowsPercentage, minutes
, minutesCalculated, plusMinusPoints, reboundsDefensive, reboundsOffensive, reboundsTotal, steals, threePointersAttempted, threePointersMade, threePointersPercentage, turnovers, twoPointersAttempted, twoPointersMade, twoPointersPercentage)
values(
 @season_id
,@series_id
,@game_id
,@game
,@team_id
,@player_id
,@status
,@starter
,@position
,@points
,@assists
,@blocks
,@fieldGoalsAttempted
,@fieldGoalsMade
,@fieldGoalsPercentage
,@foulsPersonal
,@freeThrowsAttempted
,@freeThrowsMade
,@freeThrowsPercentage
,@minutes
,@minutesCalculated
,@plusMinusPoints
,@reboundsDefensive
,@reboundsOffensive
,@reboundsTotal
,@steals
,@threePointersAttempted
,@threePointersMade
,@threePointersPercentage
,@turnovers
,@twoPointersAttempted
,@twoPointersMade
,@twoPointersPercentage)