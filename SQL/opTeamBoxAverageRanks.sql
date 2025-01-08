create view opTeamBoxAverageRanks
as
SELECT season_id
      ,matchup_id
      ,Team
      ,opAssists
      ,opFieldGoalsAttempted
      ,opFieldGoalsMade
      ,opFieldGoalsPercentage
      ,opFoulsOffensive
      ,opFoulsDrawn
      ,opFoulsPersonal
      ,opFoulsTechnical
      ,opFreeThrowsAttempted
      ,opFreeThrowsMade
      ,opFreeThrowsPercentage
      ,opMinus
      ,opPlus
      ,opPlusMinusPoints
      ,opPointsFastBreak
      ,opPointsInThePaint
      ,opPointsSecondChance
      ,opReboundsDefensive
      ,opReboundsOffensive
      ,opReboundsTotal
      ,opSteals
      ,opThreePointersAttempted
      ,opThreePointersMade
      ,opThreePointersPercentage
      ,opTurnovers
      ,opTwoPointersAttempted
      ,opTwoPointersMade
      ,opTwoPointersPercentage
      ,opAssistsTurnoverRatio
      ,opBenchPoints
      ,opBiggestLead
      ,opBiggestScoringRun
      ,opFastBreakPointsAttempted
      ,opFastBreakPointsMade
      ,opFastBreakPointsPercentage
      ,opFieldGoalsEffectiveAdjusted
      ,opFoulsTeam
      ,opFoulsTeamTechnical
      ,opLeadChanges
      ,opPointsFromTurnovers
      ,opPointsInThePaintAttempted
      ,opPointsInThePaintMade
      ,opPointsInThePaintPercentage
      ,opReboundsPersonal
      ,opReboundsTeam
      ,opReboundsTeamDefensive
      ,opReboundsTeamOffensive
      ,opSecondChancePointsAttempted
      ,opSecondChancePointsMade
      ,opSecondChancePointsPercentage
      ,opTimesTied
      ,opTrueShootingAttempts
      ,opTrueShootingPercentage
      ,opTurnoversTeam
      ,opTurnoversTotal			
      ,rank()OVER (PARTITION by season_id order by opassists						asc)opassistsRank								
      ,rank()OVER (PARTITION by season_id order by opfieldGoalsAttempted			asc)opfieldGoalsAttemptedRank			
      ,rank()OVER (PARTITION by season_id order by opfieldGoalsMade					asc)opfieldGoalsMadeRank					
      ,rank()OVER (PARTITION by season_id order by opfieldGoalsPercentage			asc)opfieldGoalsPercentageRank			
      ,rank()OVER (PARTITION by season_id order by opfoulsOffensive					desc)opfoulsOffensiveRank					
      ,rank()OVER (PARTITION by season_id order by opfoulsDrawn						asc)opfoulsDrawnRank						
      ,rank()OVER (PARTITION by season_id order by opfoulsPersonal					desc)opfoulsPersonalRank					
      ,rank()OVER (PARTITION by season_id order by opfoulsTechnical					desc)opfoulsTechnicalRank					
      ,rank()OVER (PARTITION by season_id order by opfreeThrowsAttempted			asc)opfreeThrowsAttemptedRank			
      ,rank()OVER (PARTITION by season_id order by opfreeThrowsMade					asc)opfreeThrowsMadeRank					
      ,rank()OVER (PARTITION by season_id order by opfreeThrowsPercentage			asc)opfreeThrowsPercentageRank			
      ,rank()OVER (PARTITION by season_id order by opminus							asc)opminusRank							
      ,rank()OVER (PARTITION by season_id order by opplus							asc)opplusRank							
      ,rank()OVER (PARTITION by season_id order by opplusMinusPoints				asc)opplusMinusPointsRank				
      ,rank()OVER (PARTITION by season_id order by oppointsFastBreak				asc)oppointsFastBreakRank				
      ,rank()OVER (PARTITION by season_id order by oppointsInThePaint				asc)oppointsInThePaintRank				
      ,rank()OVER (PARTITION by season_id order by oppointsSecondChance				asc)oppointsSecondChanceRank				
      ,rank()OVER (PARTITION by season_id order by opreboundsDefensive				asc)opreboundsDefensiveRank				
      ,rank()OVER (PARTITION by season_id order by opreboundsOffensive				asc)opreboundsOffensiveRank				
      ,rank()OVER (PARTITION by season_id order by opreboundsTotal					asc)opreboundsTotalRank					
      ,rank()OVER (PARTITION by season_id order by opsteals							asc)opstealsRank							
      ,rank()OVER (PARTITION by season_id order by opthreePointersAttempted			asc)opthreePointersAttemptedRank			
      ,rank()OVER (PARTITION by season_id order by opthreePointersMade				asc)opthreePointersMadeRank				
      ,rank()OVER (PARTITION by season_id order by opthreePointersPercentage		asc)opthreePointersPercentageRank		
      ,rank()OVER (PARTITION by season_id order by opturnovers						desc)opturnoversRank						
      ,rank()OVER (PARTITION by season_id order by optwoPointersAttempted			asc)optwoPointersAttemptedRank			
      ,rank()OVER (PARTITION by season_id order by optwoPointersMade				asc)optwoPointersMadeRank				
      ,rank()OVER (PARTITION by season_id order by optwoPointersPercentage			asc)optwoPointersPercentageRank			
      ,rank()OVER (PARTITION by season_id order by opassistsTurnoverRatio			asc)opassistsTurnoverRatioRank			
      ,rank()OVER (PARTITION by season_id order by opbenchPoints					asc)opbenchPointsRank					
      ,rank()OVER (PARTITION by season_id order by opbiggestLead					asc)opbiggestLeadRank					
      ,rank()OVER (PARTITION by season_id order by opbiggestScoringRun				asc)opbiggestScoringRunRank				
      ,rank()OVER (PARTITION by season_id order by opfastBreakPointsAttempted		asc)opfastBreakPointsAttemptedRank		
      ,rank()OVER (PARTITION by season_id order by opfastBreakPointsMade			asc)opfastBreakPointsMadeRank			
      ,rank()OVER (PARTITION by season_id order by opfastBreakPointsPercentage		asc)opfastBreakPointsPercentageRank		
      ,rank()OVER (PARTITION by season_id order by opfieldGoalsEffectiveAdjusted	asc)opfieldGoalsEffectiveAdjustedRank	
      ,rank()OVER (PARTITION by season_id order by opfoulsTeam						desc)opfoulsTeamRank						
      ,rank()OVER (PARTITION by season_id order by opfoulsTeamTechnical				desc)opfoulsTeamTechnicalRank				
      ,rank()OVER (PARTITION by season_id order by opleadChanges					asc)opleadChangesRank					
      ,rank()OVER (PARTITION by season_id order by oppointsFromTurnovers			asc)oppointsFromTurnoversRank			
      ,rank()OVER (PARTITION by season_id order by oppointsInThePaintAttempted		asc)oppointsInThePaintAttemptedRank		
      ,rank()OVER (PARTITION by season_id order by oppointsInThePaintMade			asc)oppointsInThePaintMadeRank			
      ,rank()OVER (PARTITION by season_id order by oppointsInThePaintPercentage		asc)oppointsInThePaintPercentageRank	
      ,rank()OVER (PARTITION by season_id order by opreboundsPersonal				asc)opreboundsPersonalRank				
      ,rank()OVER (PARTITION by season_id order by opreboundsTeam					asc)opreboundsTeamRank					
      ,rank()OVER (PARTITION by season_id order by opreboundsTeamDefensive			asc)opreboundsTeamDefensiveRank			
      ,rank()OVER (PARTITION by season_id order by opreboundsTeamOffensive			asc)opreboundsTeamOffensiveRank			
      ,rank()OVER (PARTITION by season_id order by opsecondChancePointsAttempted	asc)opsecondChancePointsAttemptedRank	
      ,rank()OVER (PARTITION by season_id order by opsecondChancePointsMade			asc)opsecondChancePointsMadeRank			
      ,rank()OVER (PARTITION by season_id order by opsecondChancePointsPercentage	asc)opsecondChancePointsPercentageRank	
      ,rank()OVER (PARTITION by season_id order by optimesTied						asc)optimesTiedRank						
      ,rank()OVER (PARTITION by season_id order by optrueShootingAttempts			asc)optrueShootingAttemptsRank			
      ,rank()OVER (PARTITION by season_id order by optrueShootingPercentage			asc)optrueShootingPercentageRank			
      ,rank()OVER (PARTITION by season_id order by opturnoversTeam					desc) opturnoversTeamRank					
      ,rank()OVER (PARTITION by season_id order by opturnoversTotal					desc) opturnoversTotalRank
  FROM opTeamBoxAverage