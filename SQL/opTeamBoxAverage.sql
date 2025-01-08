create view opTeamBoxAverage
as
SELECT  tb.season_id 
      , tb.matchup_id,
	  concat('(', t.tricode, ') ', t.city, ' ', t.name) Team				 
      , ROUND(AVG(CAST(assists 						 AS float)), 2)	opAssists 						 
      , ROUND(AVG(CAST(fieldGoalsAttempted 			 AS float)), 2)	opFieldGoalsAttempted 			 
      , ROUND(AVG(CAST(fieldGoalsMade 				 AS float)), 2)	opFieldGoalsMade 				 
      , ROUND(AVG(CAST(fieldGoalsPercentage 		 AS float)), 2)	opFieldGoalsPercentage 		 
      , ROUND(AVG(CAST(foulsOffensive 				 AS float)), 2)	opFoulsOffensive 				 
      , ROUND(AVG(CAST(foulsDrawn 					 AS float)), 2)	opFoulsDrawn 					 
      , ROUND(AVG(CAST(foulsPersonal 				 AS float)), 2)	opFoulsPersonal 				 
      , ROUND(AVG(CAST(foulsTechnical 				 AS float)), 2)	opFoulsTechnical 				 
      , ROUND(AVG(CAST(freeThrowsAttempted 			 AS float)), 2)	opFreeThrowsAttempted 			 
      , ROUND(AVG(CAST(freeThrowsMade 				 AS float)), 2)	opFreeThrowsMade 				 
      , ROUND(AVG(CAST(freeThrowsPercentage 		 AS float)), 2)	opFreeThrowsPercentage 		 
      , ROUND(AVG(CAST(minus 						 AS float)), 2)	opMinus 						 
      , ROUND(AVG(CAST(plus 						 AS float)), 2)	opPlus 						 
      , ROUND(AVG(CAST(plusMinusPoints 				 AS float)), 2)	opPlusMinusPoints 				 
      , ROUND(AVG(CAST(pointsFastBreak 				 AS float)), 2)	opPointsFastBreak 				 
      , ROUND(AVG(CAST(pointsInThePaint 			 AS float)), 2)	opPointsInThePaint 			 
      , ROUND(AVG(CAST(pointsSecondChance 			 AS float)), 2)	opPointsSecondChance 			 
      , ROUND(AVG(CAST(reboundsDefensive 			 AS float)), 2)	opReboundsDefensive 			 
      , ROUND(AVG(CAST(reboundsOffensive 			 AS float)), 2)	opReboundsOffensive 			 
      , ROUND(AVG(CAST(reboundsTotal 				 AS float)), 2)	opReboundsTotal 				 
      , ROUND(AVG(CAST(steals 						 AS float)), 2)	opSteals 						 
      , ROUND(AVG(CAST(threePointersAttempted 		 AS float)), 2)	opThreePointersAttempted 		 
      , ROUND(AVG(CAST(threePointersMade 			 AS float)), 2)	opThreePointersMade 			 
      , ROUND(AVG(CAST(threePointersPercentage 		 AS float)), 2)	opThreePointersPercentage 		 
      , ROUND(AVG(CAST(turnovers 					 AS float)), 2)	opTurnovers 					 
      , ROUND(AVG(CAST(twoPointersAttempted 		 AS float)), 2)	opTwoPointersAttempted 		 
      , ROUND(AVG(CAST(twoPointersMade 				 AS float)), 2)	opTwoPointersMade 				 
      , ROUND(AVG(CAST(twoPointersPercentage 		 AS float)), 2)	opTwoPointersPercentage 		 
      , ROUND(AVG(CAST(assistsTurnoverRatio 		 AS float)), 2)	opAssistsTurnoverRatio 		 
      , ROUND(AVG(CAST(benchPoints 					 AS float)), 2)	opBenchPoints 					 
      , ROUND(AVG(CAST(biggestLead 					 AS float)), 2)	opBiggestLead 				 
      , ROUND(AVG(CAST(biggestScoringRun 			 AS float)), 2)	opBiggestScoringRun 			 
      , ROUND(AVG(CAST(fastBreakPointsAttempted 	 AS float)), 2)	opFastBreakPointsAttempted 	 
      , ROUND(AVG(CAST(fastBreakPointsMade 			 AS float)), 2)	opFastBreakPointsMade 			 
      , ROUND(AVG(CAST(fastBreakPointsPercentage 	 AS float)), 2)	opFastBreakPointsPercentage 	 
      , ROUND(AVG(CAST(fieldGoalsEffectiveAdjusted 	 AS float)), 2)	opFieldGoalsEffectiveAdjusted 	 
      , ROUND(AVG(CAST(foulsTeam 					 AS float)), 2)	opFoulsTeam 					 
      , ROUND(AVG(CAST(foulsTeamTechnical 			 AS float)), 2)	opFoulsTeamTechnical 			 
      , ROUND(AVG(CAST(leadChanges 					 AS float)), 2)	opLeadChanges 					 
      , ROUND(AVG(CAST(pointsFromTurnovers 			 AS float)), 2)	opPointsFromTurnovers 			 
      , ROUND(AVG(CAST(pointsInThePaintAttempted 	 AS float)), 2)	opPointsInThePaintAttempted 	 
      , ROUND(AVG(CAST(pointsInThePaintMade 		 AS float)), 2)	opPointsInThePaintMade 		 
      , ROUND(AVG(CAST(pointsInThePaintPercentage 	 AS float)), 2)	opPointsInThePaintPercentage 	 
      , ROUND(AVG(CAST(reboundsPersonal 			 AS float)), 2)	opReboundsPersonal 			 
      , ROUND(AVG(CAST(reboundsTeam 				 AS float)), 2)	opReboundsTeam 				 
      , ROUND(AVG(CAST(reboundsTeamDefensive 		 AS float)), 2)	opReboundsTeamDefensive 		 
      , ROUND(AVG(CAST(reboundsTeamOffensive 		 AS float)), 2)	opReboundsTeamOffensive 		 
      , ROUND(AVG(CAST(secondChancePointsAttempted 	 AS float)), 2)	opSecondChancePointsAttempted 	 
      , ROUND(AVG(CAST(secondChancePointsMade 		 AS float)), 2)	opSecondChancePointsMade 		 
      , ROUND(AVG(CAST(secondChancePointsPercentage  AS float)), 2)	opSecondChancePointsPercentage  	 
      , ROUND(AVG(CAST(timesTied 					 AS float)), 2)	opTimesTied 					 
      , ROUND(AVG(CAST(trueShootingAttempts 		 AS float)), 2)	opTrueShootingAttempts 		 
      , ROUND(AVG(CAST(trueShootingPercentage 		 AS float)), 2)	opTrueShootingPercentage 		 
      , ROUND(AVG(CAST(turnoversTeam 				 AS float)), 2)	opTurnoversTeam 				 
      , ROUND(AVG(CAST(turnoversTotal 				 AS float)), 2)	opTurnoversTotal 				 
  FROM  teamBox tb inner join 
			team t on tb.matchup_id = t.team_id and tb.season_id = t.season_id
group by tb.season_id, tb.matchup_id, concat('(', t.tricode, ') ', t.city, ' ', t.name)
