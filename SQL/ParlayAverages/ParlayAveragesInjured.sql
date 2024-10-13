create procedure ParlayAveragesInjured @Player varchar(255), @Player2 varchar(255), @Player3 varchar(255), @Injured varchar(255), @Team varchar(255), @season int
as
select pb.season_id,
	   t.team_id,
	   CONCAT(t.city, ' ', t.name) AS Team, 
	   p.player_id,
	   p.name, 	   
	   COUNT(pb.game_id) AS Games, 
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
	   AVG(cast(SUBSTRING(pb.minutesCalculated, 3, 2) as int)) Minutes

FROM playerBox pb INNER JOIN
		player p ON pb.player_id = p.player_id AND pb.season_id = p.season_id INNER JOIN
		team t ON pb.team_id = t.team_id AND pb.season_id = t.season_id INNER JOIN
		teamBox tb ON pb.team_id = tb.team_id AND pb.game_id = tb.game_id AND pb.season_id = tb.season_id inner join
		playerBox pb2 on pb.game_id = pb2.game_id and pb.team_id = pb2.team_id and pb.season_id = pb2.season_id inner join
		player p2 on pb2.player_id = p2.player_id and pb.season_id = p2.season_id INNER JOIN
		team t2 on pb2.team_id = t2.team_id AND pb.season_id = t2.season_id 
where pb.status = 'ACTIVE' and pb.minutesCalculated != 'PT00M'
and p.name = @Player and CONCAT(t.city, ' ', t.name) = @Team
and pb.season_id = @season
and (pb2.status != 'ACTIVE' or pb2.minutesCalculated = 'PT00M')
and p2.name = @Injured and CONCAT(t2.city, ' ', t2.name) = @Team
GROUP BY pb.season_id, 
t.team_id,
CONCAT(t.city, ' ', t.name), 
p.player_id,
p.name, 
p2.player_id,
p2.name