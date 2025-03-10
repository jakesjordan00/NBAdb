CREATE TABLE dbo.playerBoxPlayoffs(
    season_id				int null,
	series_id				varchar(30),
	game_id					int NULL,
	game					int null,
	team_id					int NULL,
	player_id				int NULL,
	status					varchar(30) NULL,
	starter					int NULL,
	position				varchar(2) NULL,
	points					int NULL,
	assists					int NULL,
	blocks					int NULL,
	blocksReceived			int NULL,
	fieldGoalsAttempted		int NULL,
	fieldGoalsMade			int NULL,
	fieldGoalsPercentage	float NULL,
	foulsOffensive			int NULL,
	foulsDrawn				int NULL,
	foulsPersonal			int NULL,
	foulsTechnical			int NULL,
	freeThrowsAttempted		int NULL,
	freeThrowsMade			int NULL,
	freeThrowsPercentage	float NULL,
	minus					float NULL,
	minutes					varchar(30) NULL,
	minutesCalculated		varchar(30) NULL,
	plus					float NULL,
	plusMinusPoints			float NULL,
	pointsFastBreak			int NULL,
	pointsInThePaint		int NULL,
	pointsSecondChance		int NULL,
	reboundsDefensive		int NULL,
	reboundsOffensive		int NULL,
	reboundsTotal			int NULL,
	steals					int NULL,
	threePointersAttempted  int NULL,
	threePointersMade		int NULL,
	threePointersPercentage float NULL,
	turnovers				int NULL,
	twoPointersAttempted	int NULL,
	twoPointersMade			int NULL,
	twoPointersPercentage	float NULL,
	statusReason			varchar(100) NULL,
	statusDescription		varchar(200) NULL
) 