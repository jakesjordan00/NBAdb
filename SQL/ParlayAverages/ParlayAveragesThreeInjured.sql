create procedure ParlayAveragesThreeInjured @Player varchar(255), @Player2 varchar(255), @Player3 varchar(255), @Injured varchar(255), @Team varchar(255), @season int
as
select pb.season_id,
	   t.team_id,
	   CONCAT(t.city, ' ', t.name) AS Team, 
	   p.player_id,
	   p.name,    
	   COUNT(pb.game_id) AS Games, 
	   ROUND(AVG(CAST(pb.points AS float)), 2) AS Points1, 
	   ROUND(AVG(CAST(pb.assists AS float)), 2) AS Assists1, 
	   ROUND(AVG(CAST(pb.reboundsTotal AS float)), 2) AS Rebounds1, 
	   ROUND(AVG(CAST(pb.blocks AS float)), 2) AS Blocks1, 
	   ROUND(AVG(CAST(pb.steals AS float)), 2) AS Steals1, 
	   ROUND(AVG(CAST(pb.fieldGoalsMade AS float)), 2) AS FGM1, 
	   ROUND(AVG(CAST(pb.fieldGoalsAttempted AS float)), 2) AS FGA1, 
	   ROUND(AVG(pb.fieldGoalsPercentage) * 100, 2) AS [FG%1], 
	   ROUND(AVG(CAST(pb.threePointersMade AS float)), 2) AS FG3M1, 
	   ROUND(AVG(CAST(pb.threePointersAttempted AS float)), 2) AS FG3A1, 
	   ROUND(AVG(pb.threePointersPercentage) * 100, 2) AS [FG3%1],
	   AVG(cast(SUBSTRING(pb.minutesCalculated, 3, 2) as int)) Minutes1,
	   p2.player_id player2_id,
	   p2.name name2, 
	   ROUND(AVG(CAST(pb2.points AS float)), 2) AS Points2, 
	   ROUND(AVG(CAST(pb2.assists AS float)), 2) AS Assists2, 
	   ROUND(AVG(CAST(pb2.reboundsTotal AS float)), 2) AS Rebounds2, 
	   ROUND(AVG(CAST(pb2.blocks AS float)), 2) AS Blocks2, 
	   ROUND(AVG(CAST(pb2.steals AS float)), 2) AS Steals2, 
	   ROUND(AVG(CAST(pb2.fieldGoalsMade AS float)), 2) AS FGM2, 
	   ROUND(AVG(CAST(pb2.fieldGoalsAttempted AS float)), 2) AS FGA2, 
	   ROUND(AVG(pb2.fieldGoalsPercentage) * 100, 2) AS [FG%2], 
	   ROUND(AVG(CAST(pb2.threePointersMade AS float)), 2) AS FG3M2, 
	   ROUND(AVG(CAST(pb2.threePointersAttempted AS float)), 2) AS FG3A2, 
	   ROUND(AVG(pb2.threePointersPercentage) * 100, 2) AS [FG3%2],
	   AVG(cast(SUBSTRING(pb2.minutesCalculated, 3, 2) as int)) Minutes2,
	   p3.player_id player3_id,
	   p3.name name3, 
	   ROUND(AVG(CAST(pb3.points AS float)), 2) AS Points3, 
	   ROUND(AVG(CAST(pb3.assists AS float)), 2) AS Assists3, 
	   ROUND(AVG(CAST(pb3.reboundsTotal AS float)), 2) AS Rebounds3, 
	   ROUND(AVG(CAST(pb3.blocks AS float)), 2) AS Blocks3, 
	   ROUND(AVG(CAST(pb3.steals AS float)), 2) AS Steals3, 
	   ROUND(AVG(CAST(pb3.fieldGoalsMade AS float)), 2) AS FGM3, 
	   ROUND(AVG(CAST(pb3.fieldGoalsAttempted AS float)), 2) AS FGA3, 
	   ROUND(AVG(pb3.fieldGoalsPercentage) * 100, 2) AS [FG%3], 
	   ROUND(AVG(CAST(pb3.threePointersMade AS float)), 2) AS FG3M3, 
	   ROUND(AVG(CAST(pb3.threePointersAttempted AS float)), 2) AS FG3A3, 
	   ROUND(AVG(pb3.threePointersPercentage) * 100, 2) AS [FG3%3],
	   AVG(cast(SUBSTRING(pb3.minutesCalculated, 3, 2) as int)) Minutes3

FROM playerBox pb INNER JOIN
		player p ON pb.player_id = p.player_id AND pb.season_id = p.season_id INNER JOIN
		team t ON pb.team_id = t.team_id AND pb.season_id = t.season_id INNER JOIN
		teamBox tb ON pb.team_id = tb.team_id AND pb.game_id = tb.game_id AND pb.season_id = tb.season_id inner join
		playerBox pb2 on pb.game_id = pb2.game_id and pb.team_id = pb2.team_id and pb.season_id = pb2.season_id inner join
		player p2 on pb2.player_id = p2.player_id and pb.season_id = p2.season_id INNER JOIN
		team t2 on pb2.team_id = t2.team_id AND pb.season_id = t2.season_id inner join
		playerBox pb3 on pb.game_id = pb3.game_id and pb.team_id = pb3.team_id and pb.season_id = pb3.season_id inner join
		player p3 on pb3.player_id = p3.player_id and pb.season_id = p3.season_id INNER JOIN
		team t3 on pb3.team_id = t3.team_id AND pb.season_id = t3.season_id inner join
		playerBox pb4 on pb.game_id = pb4.game_id and pb.team_id = pb4.team_id and pb.season_id = pb4.season_id inner join
		player p4 on pb4.player_id = p4.player_id and pb.season_id = p4.season_id INNER JOIN
		team t4 on pb4.team_id = t4.team_id AND pb.season_id = t4.season_id 
where pb.status = 'ACTIVE' and pb.minutesCalculated != 'PT00M'
and p.name = @Player and CONCAT(t.city, ' ', t.name) = @Team
and pb.season_id = @season
and pb2.status = 'ACTIVE' and pb2.minutesCalculated != 'PT00M'
and p2.name = @Player2 and CONCAT(t2.city, ' ', t2.name) = @Team
and pb3.status = 'ACTIVE' and pb3.minutesCalculated != 'PT00M'
and p3.name = @Player3 and CONCAT(t3.city, ' ', t3.name) = @Team
and (pb4.status != 'ACTIVE' or pb4.minutesCalculated = 'PT00M')
and p4.name = @Injured and CONCAT(t4.city, ' ', t4.name) = @Team
GROUP BY pb.season_id, 
t.team_id,
CONCAT(t.city, ' ', t.name), 
p.player_id,
p.name, 
p2.player_id,
p2.name,
p3.player_id,
p3.name