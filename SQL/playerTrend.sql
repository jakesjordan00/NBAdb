create view PlayerTrend
as
WITH PlayerGameRanks AS (
    SELECT 
        p.season_id, p.game_id, team_id, player_id, status, starter, position, points, assists, blocks, blocksReceived, 
        fieldGoalsAttempted, fieldGoalsMade, fieldGoalsPercentage, foulsOffensive, foulsDrawn, foulsPersonal, 
        foulsTechnical, freeThrowsAttempted, freeThrowsMade, freeThrowsPercentage, minus, minutes, minutesCalculated, 
        plus, plusMinusPoints, pointsFastBreak, pointsInThePaint, pointsSecondChance, reboundsDefensive, 
        reboundsOffensive, reboundsTotal, steals, threePointersAttempted, threePointersMade, threePointersPercentage, 
        turnovers, twoPointersAttempted, twoPointersMade, twoPointersPercentage, statusReason, statusDescription, 
        ROW_NUMBER() OVER (PARTITION BY player_id ORDER BY date DESC) AS game_rank
    FROM 
        playerBox p inner join
				game g on p.game_id = g.game_id and p.season_id = g.season_id
where status = 'ACTIVE' and minutesCalculated != 'PT00M' and p.season_id = (select max(season_id) from team)
)
    SELECT        r.player_id, r.team_id, p.name, ROUND(AVG(CAST(points AS float)), 2) AS Points, ROUND(AVG(CAST(assists AS float)), 2) AS Assists, ROUND(AVG(CAST(reboundsTotal AS float)), 2) AS Rebounds, ROUND(AVG(CAST(blocks AS float)), 2) 
                              AS Blocks, ROUND(AVG(CAST(steals AS float)), 2) AS Steals, ROUND(AVG(CAST(fieldGoalsMade AS float)), 2) AS FGM, ROUND(AVG(CAST(fieldGoalsAttempted AS float)), 2) AS FGA, ROUND(AVG(fieldGoalsPercentage) * 100, 2) 
                              AS [FG%], ROUND(AVG(CAST(threePointersMade AS float)), 2) AS FG3M, ROUND(AVG(CAST(threePointersAttempted AS float)), 2) AS FG3A, ROUND(AVG(threePointersPercentage) * 100, 2) AS [FG3%], 
                              AVG(cast(SUBSTRING(minutesCalculated, 3, 2) AS int)) Minutes, ROUND(AVG(CAST(turnovers AS float)), 2) AS Turnovers,
							  ROUND(AVG(CAST(twoPointersMade AS float)), 2) AS FG2M, ROUND(AVG(CAST(twoPointersAttempted AS float)), 2) AS FG2A, ROUND(AVG(twoPointersPercentage) * 100, 2) AS [FG2%], 
							  ROUND(AVG(CAST(freeThrowsMade AS float)), 2) AS FTM, ROUND(AVG(CAST(freeThrowsAttempted AS float)), 2) AS FTA, ROUND(AVG(freeThrowsPercentage) * 100, 2) AS [FT%]
     FROM            PlayerGameRanks r INNER JOIN
                              player p ON r.player_id = p.player_id AND p.season_id =
                                  (SELECT        max(season_id)
                                    FROM            team)
     WHERE        game_rank <= 3
     GROUP BY r.player_id, p.name, r.team_id


