CREATE TABLE dbo.playByPlayPlayoffs(
    season_id				int null,
	series_id				varchar(30),
	game_id					int NULL,
	game					int null,
	actionNumber			int NULL,
	clock					varchar(20) NULL,
	timeActual				datetime NULL,
	period					int NULL,
	periodType				varchar(20) NULL,
	team_id					int NULL,
	teamTricode				varchar(3) NULL,
	event_msg_type_id		int NULL,
	shotType				varchar(4) NULL,
	ptsGenerated			int NULL,
	actionType				varchar(30) NULL,
	actionSub				varchar(20) NULL,
	description				varchar(100) NULL,
	descriptor				varchar(30) NULL,
	qualifier1				varchar(30) NULL,
	qualifier2				varchar(30) NULL,
	qualifier3				varchar(30) NULL,
	player_id				int NULL,
	x						float NULL,
	y						float NULL,
	area					varchar(50) NULL,
	areaDetail				varchar(50) NULL,
	side					varchar(30) NULL,
	shotDistance			float NULL,
	scoreHome				int NULL,
	scoreAway				int NULL,
	isFieldGoal				bit NULL,
	shotResult				varchar(20) NULL,
	shotActionNumber		int NULL,
	player_idAST			int NULL,
	player_idBLK			int NULL,
	player_idSTL			int NULL,
	player_idFoulDrawn		int NULL,
	player_idJumpW			int NULL,
	player_idJumpL			int NULL,
	official_id				int NULL
) 