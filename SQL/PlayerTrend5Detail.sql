create view PlayerTrend5Detail
as
WITH PlayerGameRanks AS (
    SELECT 
        p.season_id, p.game_id, team_id, player_id, status, starter, position, points, assists, blocks, blocksReceived, 
        fieldGoalsAttempted, fieldGoalsMade, fieldGoalsPercentage, foulsOffensive, foulsDrawn, foulsPersonal, 
        foulsTechnical, freeThrowsAttempted, freeThrowsMade, freeThrowsPercentage, minus, minutes, minutesCalculated, 
        plus, plusMinusPoints, pointsFastBreak, pointsInThePaint, pointsSecondChance, reboundsDefensive, 
        reboundsOffensive, reboundsTotal, steals, threePointersAttempted, threePointersMade, threePointersPercentage, 
        turnovers, twoPointersAttempted, twoPointersMade, twoPointersPercentage, statusReason, statusDescription, g.date,
        ROW_NUMBER() OVER (PARTITION BY player_id ORDER BY date DESC) AS game_rank
    FROM 
        playerBox p inner join
				game g on p.game_id = g.game_id and p.season_id = g.season_id
where status = 'ACTIVE' and replace(replace(p.minutesCalculated, 'PT', ''), 'M', '') > (select cast(Minutes as decimal(18, 2))/2 from playerBoxAverage a where a.season_id = p.season_id and a.team_id = p.team_id and a.player_id = p.player_id)
and p.season_id = (select max(season_id) from team)
)
SELECT r.player_id, 
r.team_id, 
r.game_id,
r.date,
p.name, 
points AS Points, 
assists AS Assists, 
reboundsTotal AS Rebounds, 
blocks AS Blocks, 
steals AS Steals, 
fieldGoalsMade AS FGM, 
fieldGoalsAttempted AS FGA, 
round(fieldGoalsPercentage * 100, 2) AS [FG%], 
threePointersMade AS FG3M, 
threePointersAttempted AS FG3A, 
round(threePointersPercentage * 100, 2) AS [FG3%], 
SUBSTRING(minutesCalculated, 3, 2) Minutes, 
turnovers AS Turnovers,
twoPointersMade AS FG2M, 
twoPointersAttempted AS FG2A, 
round(twoPointersPercentage * 100, 2) AS [FG2%], 
freeThrowsMade AS FTM, 
freeThrowsAttempted AS FTA, 
round(freeThrowsPercentage * 100, 2) AS [FT%]
     FROM            PlayerGameRanks r INNER JOIN
                              player p ON r.player_id = p.player_id AND p.season_id =
                                  (SELECT        max(season_id)
                                    FROM            team)
     WHERE        game_rank <= 5


