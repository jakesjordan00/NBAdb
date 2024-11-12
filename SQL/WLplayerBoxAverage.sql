create view WLplayerBoxAverage
as
SELECT pb.season_id, 
	   t.team_id,
	   CONCAT(t.city, ' ', t.name) AS Team, 
	   p.player_id,
	   p.name, 
	   CASE WHEN tb.points > tb.pointsAgainst 
	   THEN 1 ELSE 0 END AS Win, 
	   COUNT(pb.player_id) AS Games, 
	   ROUND(AVG(CAST(pb.points AS float)), 2) AS Points, 
	   ROUND(AVG(CAST(pb.assists AS float)), 2) AS Assists, 
	   ROUND(AVG(CAST(pb.reboundsTotal AS float)), 2) AS Rebounds, 
	   ROUND(AVG(CAST(pb.blocks AS float)), 2) AS Blocks, 
	   ROUND(AVG(CAST(pb.steals AS float)), 2) AS Steals, 
	   ROUND(AVG(CAST(pb.fieldGoalsMade AS float)), 2) AS FGM, 
	   ROUND(AVG(CAST(pb.fieldGoalsAttempted AS float)), 2) AS FGA, 
	   ROUND(AVG(pb.fieldGoalsPercentage) * 100, 2) AS [FG%], 
	   ROUND(AVG(CAST(pb.threePointersMade AS float)), 2) AS FG3M, 
	   ROUND(AVG(CAST(pb.threePointersAttempted AS float)), 2) AS FG3A, 
	   ROUND(AVG(pb.threePointersPercentage) * 100, 2) AS [FG3%],
	   AVG(cast(SUBSTRING(pb.minutesCalculated, 3, 2) as int)) Minutes,
	   ROUND(AVG(CAST(pb.twoPointersMade AS float)), 2) AS FG2M, 
	   ROUND(AVG(CAST(pb.twoPointersAttempted AS float)), 2) AS FG2A, 
	   ROUND(AVG(pb.twoPointersPercentage) * 100, 2) AS [FG2%],
	   ROUND(AVG(CAST(pb.freeThrowsMade AS float)), 2) AS FTM, 
	   ROUND(AVG(CAST(pb.freeThrowsAttempted AS float)), 2) AS FTA, 
	   ROUND(AVG(pb.freeThrowsPercentage) * 100, 2) AS [FT%]
FROM playerBox AS pb INNER JOIN
		player AS p ON pb.player_id = p.player_id AND pb.season_id = p.season_id INNER JOIN
		team AS t ON pb.team_id = t.team_id AND pb.season_id = t.season_id INNER JOIN
		teamBox AS tb ON pb.team_id = tb.team_id AND pb.game_id = tb.game_id AND pb.season_id = tb.season_id
where pb.status = 'ACTIVE' and pb.minutesCalculated != 'PT00M'
GROUP BY pb.season_id, 
t.team_id,
CONCAT(t.city, ' ', t.name), 
p.player_id,
p.name, 
CASE WHEN tb.points > tb.pointsAgainst THEN 1 ELSE 0 END