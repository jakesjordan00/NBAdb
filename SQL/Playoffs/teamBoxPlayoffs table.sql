CREATE TABLE dbo.teamBoxPlayoffs(
    season_id					 int null,
	series_id					 varchar(30),
	game_id						 int NULL,
	game						 int null,
	team_id						 int NULL,
	atHome						 int NULL,
	matchup_id					 int NULL,
	points						 int NULL,
	pointsAgainst				 int NULL,
	q1Points					 int NULL,
	q1PointsAgainst				 int NULL,
	q2Points					 int NULL,
	q2PointsAgainst				 int NULL,
	q3Points					 int NULL,
	q3PointsAgainst				 int NULL,
	q4Points					 int NULL,
	q4PointsAgainst				 int NULL,
	otPoints					 int NULL,
	otPointsAgainst				 int NULL,
	assists						 int NULL,
	blocks						 int NULL,
	blocksReceived				 int NULL,
	fieldGoalsAttempted			 int NULL,
	fieldGoalsMade				 int NULL,
	fieldGoalsPercentage		 float NULL,
	foulsOffensive				 int NULL,
	foulsDrawn					 int NULL,
	foulsPersonal				 int NULL,
	foulsTechnical				 int NULL,
	freeThrowsAttempted			 int NULL,
	freeThrowsMade				 int NULL,
	freeThrowsPercentage		 float NULL,
	minus						 float NULL,
	minutes						 varchar(30) NULL,
	minutesCalculated			 varchar(30) NULL,
	plus						 float NULL,
	plusMinusPoints				 float NULL,
	pointsFastBreak				 int NULL,
	pointsInThePaint			 int NULL,
	pointsSecondChance			 int NULL,
	reboundsDefensive			 int NULL,
	reboundsOffensive			 int NULL,
	reboundsTotal				 int NULL,
	steals						 int NULL,
	threePointersAttempted		 int NULL,
	threePointersMade			 int NULL,
	threePointersPercentage		 float NULL,
	turnovers					 int NULL,
	twoPointersAttempted		 int NULL,
	twoPointersMade				 int NULL,
	twoPointersPercentage		 float NULL,
	assistsTurnoverRatio		 float NULL,
	benchPoints					 int NULL,
	biggestLead					 int NULL,
	biggestLeadScore			 varchar(30) NULL,
	biggestScoringRun			 int NULL,
	biggestScoringRunScore		 varchar(30) NULL,
	fastBreakPointsAttempted	 int NULL,
	fastBreakPointsMade			 int NULL,
	fastBreakPointsPercentage	 float NULL,
	fieldGoalsEffectiveAdjusted	 float NULL,
	foulsTeam					 int NULL,
	foulsTeamTechnical			 int NULL,
	leadChanges					 int NULL,
	pointsFromTurnovers			 int NULL,
	pointsInThePaintAttempted	 int NULL,
	pointsInThePaintMade		 int NULL,
	pointsInThePaintPercentage	 float NULL,
	reboundsPersonal			 int NULL,
	reboundsTeam				 int NULL,
	reboundsTeamDefensive		 int NULL,
	reboundsTeamOffensive		 int NULL,
	secondChancePointsAttempted	 int NULL,
	secondChancePointsMade		 int NULL,
	secondChancePointsPercentage float NULL,
	timeLeading					 varchar(30) NULL,
	timesTied					 int NULL,
	trueShootingAttempts		 float NULL,
	trueShootingPercentage		 float NULL,
	turnoversTeam				 int NULL,
	turnoversTotal				 int NULL
) 