/****** Script for SelectTopNRows command from SSMS  ******/
create view teamBoxAverage
as
SELECT  tb.season_id 
      , tb.team_id,
	  concat('(', t.tricode, ') ', t.city, ' ', t.name) Team
      , ROUND(AVG(CAST(points						 AS float)), 2) points						 
      , ROUND(AVG(CAST(pointsAgainst 				 AS float)), 2)	pointsAgainst 	
	  , ROUND(AVG(CAST(points						 AS float)), 2) -
	  ROUND(AVG(CAST(pointsAgainst 				 AS float)), 2) MoV
      , ROUND(AVG(CAST(q1Points 					 AS float)), 2)	q1Points 					 
      , ROUND(AVG(CAST(q1PointsAgainst 				 AS float)), 2)	q1PointsAgainst 				 
      , ROUND(AVG(CAST(q2Points 					 AS float)), 2)	q2Points 					 
      , ROUND(AVG(CAST(q2PointsAgainst 				 AS float)), 2)	q2PointsAgainst 				 
      , ROUND(AVG(CAST(q3Points 					 AS float)), 2)	q3Points 					 
      , ROUND(AVG(CAST(q3PointsAgainst 				 AS float)), 2)	q3PointsAgainst 				 
      , ROUND(AVG(CAST(q4Points 					 AS float)), 2)	q4Points 					 
      , ROUND(AVG(CAST(q4PointsAgainst 				 AS float)), 2)	q4PointsAgainst 				 
      , ROUND(AVG(CAST(otPoints 					 AS float)), 2)	otPoints 					 
      , ROUND(AVG(CAST(otPointsAgainst 				 AS float)), 2)	otPointsAgainst 				 
      , ROUND(AVG(CAST(assists 						 AS float)), 2)	assists 						 
      , ROUND(AVG(CAST(blocks 						 AS float)), 2)	blocks 						 
      , ROUND(AVG(CAST(blocksReceived 				 AS float)), 2)	blocksReceived 				 
      , ROUND(AVG(CAST(fieldGoalsAttempted 			 AS float)), 2)	fieldGoalsAttempted 			 
      , ROUND(AVG(CAST(fieldGoalsMade 				 AS float)), 2)	fieldGoalsMade 				 
      , ROUND(AVG(CAST(fieldGoalsPercentage 		 AS float)), 2)	fieldGoalsPercentage 		 
      , ROUND(AVG(CAST(foulsOffensive 				 AS float)), 2)	foulsOffensive 				 
      , ROUND(AVG(CAST(foulsDrawn 					 AS float)), 2)	foulsDrawn 					 
      , ROUND(AVG(CAST(foulsPersonal 				 AS float)), 2)	foulsPersonal 				 
      , ROUND(AVG(CAST(foulsTechnical 				 AS float)), 2)	foulsTechnical 				 
      , ROUND(AVG(CAST(freeThrowsAttempted 			 AS float)), 2)	freeThrowsAttempted 			 
      , ROUND(AVG(CAST(freeThrowsMade 				 AS float)), 2)	freeThrowsMade 				 
      , ROUND(AVG(CAST(freeThrowsPercentage 		 AS float)), 2)	freeThrowsPercentage 		 
      , ROUND(AVG(CAST(minus 						 AS float)), 2)	minus 						 
    --, ROUND(AVG(CAST(minutes 						 AS float)), 2)	minutes 						 
    --, ROUND(AVG(CAST(minutesCalculated 			 AS float)), 2)	minutesCalculated 			 
      , ROUND(AVG(CAST(plus 						 AS float)), 2)	plus 						 
      , ROUND(AVG(CAST(plusMinusPoints 				 AS float)), 2)	plusMinusPoints 				 
      , ROUND(AVG(CAST(pointsFastBreak 				 AS float)), 2)	pointsFastBreak 				 
      , ROUND(AVG(CAST(pointsInThePaint 			 AS float)), 2)	pointsInThePaint 			 
      , ROUND(AVG(CAST(pointsSecondChance 			 AS float)), 2)	pointsSecondChance 			 
      , ROUND(AVG(CAST(reboundsDefensive 			 AS float)), 2)	reboundsDefensive 			 
      , ROUND(AVG(CAST(reboundsOffensive 			 AS float)), 2)	reboundsOffensive 			 
      , ROUND(AVG(CAST(reboundsTotal 				 AS float)), 2)	reboundsTotal 				 
      , ROUND(AVG(CAST(steals 						 AS float)), 2)	steals 						 
      , ROUND(AVG(CAST(threePointersAttempted 		 AS float)), 2)	threePointersAttempted 		 
      , ROUND(AVG(CAST(threePointersMade 			 AS float)), 2)	threePointersMade 			 
      , ROUND(AVG(CAST(threePointersPercentage 		 AS float)), 2)	threePointersPercentage 		 
      , ROUND(AVG(CAST(turnovers 					 AS float)), 2)	turnovers 					 
      , ROUND(AVG(CAST(twoPointersAttempted 		 AS float)), 2)	twoPointersAttempted 		 
      , ROUND(AVG(CAST(twoPointersMade 				 AS float)), 2)	twoPointersMade 				 
      , ROUND(AVG(CAST(twoPointersPercentage 		 AS float)), 2)	twoPointersPercentage 		 
      , ROUND(AVG(CAST(assistsTurnoverRatio 		 AS float)), 2)	assistsTurnoverRatio 		 
      , ROUND(AVG(CAST(benchPoints 					 AS float)), 2)	benchPoints 					 
      , ROUND(AVG(CAST(biggestLead 					 AS float)), 2)	biggestLead 					 
	--, ROUND(AVG(CAST(biggestLeadScore 			 AS float)), 2)	biggestLeadScore 			 
      , ROUND(AVG(CAST(biggestScoringRun 			 AS float)), 2)	biggestScoringRun 			 
    --, ROUND(AVG(CAST(biggestScoringRunScore 		 AS float)), 2)	biggestScoringRunScore 		 
      , ROUND(AVG(CAST(fastBreakPointsAttempted 	 AS float)), 2)	fastBreakPointsAttempted 	 
      , ROUND(AVG(CAST(fastBreakPointsMade 			 AS float)), 2)	fastBreakPointsMade 			 
      , ROUND(AVG(CAST(fastBreakPointsPercentage 	 AS float)), 2)	fastBreakPointsPercentage 	 
      , ROUND(AVG(CAST(fieldGoalsEffectiveAdjusted 	 AS float)), 2)	fieldGoalsEffectiveAdjusted 	 
      , ROUND(AVG(CAST(foulsTeam 					 AS float)), 2)	foulsTeam 					 
      , ROUND(AVG(CAST(foulsTeamTechnical 			 AS float)), 2)	foulsTeamTechnical 			 
      , ROUND(AVG(CAST(leadChanges 					 AS float)), 2)	leadChanges 					 
      , ROUND(AVG(CAST(pointsFromTurnovers 			 AS float)), 2)	pointsFromTurnovers 			 
      , ROUND(AVG(CAST(pointsInThePaintAttempted 	 AS float)), 2)	pointsInThePaintAttempted 	 
      , ROUND(AVG(CAST(pointsInThePaintMade 		 AS float)), 2)	pointsInThePaintMade 		 
      , ROUND(AVG(CAST(pointsInThePaintPercentage 	 AS float)), 2)	pointsInThePaintPercentage 	 
      , ROUND(AVG(CAST(reboundsPersonal 			 AS float)), 2)	reboundsPersonal 			 
      , ROUND(AVG(CAST(reboundsTeam 				 AS float)), 2)	reboundsTeam 				 
      , ROUND(AVG(CAST(reboundsTeamDefensive 		 AS float)), 2)	reboundsTeamDefensive 		 
      , ROUND(AVG(CAST(reboundsTeamOffensive 		 AS float)), 2)	reboundsTeamOffensive 		 
      , ROUND(AVG(CAST(secondChancePointsAttempted 	 AS float)), 2)	secondChancePointsAttempted 	 
      , ROUND(AVG(CAST(secondChancePointsMade 		 AS float)), 2)	secondChancePointsMade 		 
      , ROUND(AVG(CAST(secondChancePointsPercentage  AS float)), 2)	secondChancePointsPercentage  
    --, ROUND(AVG(CAST(timeLeading 					 AS float)), 2)	timeLeading 					 
      , ROUND(AVG(CAST(timesTied 					 AS float)), 2)	timesTied 					 
      , ROUND(AVG(CAST(trueShootingAttempts 		 AS float)), 2)	trueShootingAttempts 		 
      , ROUND(AVG(CAST(trueShootingPercentage 		 AS float)), 2)	trueShootingPercentage 		 
      , ROUND(AVG(CAST(turnoversTeam 				 AS float)), 2)	turnoversTeam 				 
      , ROUND(AVG(CAST(turnoversTotal 				 AS float)), 2)	turnoversTotal 				 
  FROM  teamBox tb inner join 
			team t on tb.team_id = t.team_id and tb.season_id = t.season_id
group by tb.season_id, tb.team_id, concat('(', t.tricode, ') ', t.city, ' ', t.name)