


create procedure OldPlayerBoxInsert 
 @season_id int
,@game_id int
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
insert into playerBox(season_id,game_id,team_id, player_id, status, starter, position, points, assists, blocks, fieldGoalsAttempted, fieldGoalsMade, fieldGoalsPercentage, foulsPersonal, freeThrowsAttempted, freeThrowsMade, freeThrowsPercentage, minutes
, minutesCalculated, plusMinusPoints, reboundsDefensive, reboundsOffensive, reboundsTotal, steals, threePointersAttempted, threePointersMade, threePointersPercentage, turnovers, twoPointersAttempted, twoPointersMade, twoPointersPercentage)
values(
 @season_id
,@game_id
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