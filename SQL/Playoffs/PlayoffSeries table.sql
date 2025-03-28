CREATE TABLE PlayoffSeries(
    season_id int,
    series_id varchar(50),
	games int,
    roundNumber INT,
    roundDesc varchar(100),
    seriesNumber INT,
    seriesConference varchar(50),
    seriesText varchar(255),
    seriesStatus INT,
    seriesWinner INT,
    highSeed_id INT,
    highSeedCity varchar(100),
    highSeedName varchar(100),
    highSeedTricode varchar(10),
    highSeedRank INT,
    highSeedSeriesWins INT,
    highSeedRegSeasonWins INT,
    highSeedRegSeasonLosses INT,
    lowSeed_id INT,
    lowSeedCity varchar(100),
    lowSeedName varchar(100),
    lowSeedTricode varchar(10),
    lowSeedRank INT,
    lowSeedSeriesWins INT,
    lowSeedRegSeasonWins INT,
    lowSeedRegSeasonLosses INT,
	firstGame_id varchar(50),
	lastGame_id varchar(50),
    displayOrderNumber INT,
    displayTopTeam INT,
    displayBottomTeam INT,
    nextGame_id varchar(50),
    nextGameNumber varchar(50),
    nextGameDateTimeEt varchar(50),
    nextGameDateTimeUTC varchar(50),
    nextGameStatus INT,
    nextGameStatusText varchar(100),
    nextGameBroadcaster_id INT,
    nextGameBroadcasterDisplay varchar(100),
    lastCompletedGame_id varchar(50),
	Primary Key(season_id, series_id, highSeed_id, lowSeed_id)
);
