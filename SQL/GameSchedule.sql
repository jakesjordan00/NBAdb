create table GameSchedule(
dateTime datetime,
game_id int,
home_id int,
away_id int,
homeTri varchar(3),
homeScore int,
awayTri varchar(3),
awayScore int,
homeWs int,
homeLs int,
awayWs int,
awayLs int,
broadcast varchar(20),
gameLabel varchar(255),
gameSubLabel varchar(255),
gameSubType varchar(255),
gameStatus int,
gameStatusText varchar(255),
date Date,
day varchar(3),
season_id int)
go

create procedure GameScheduleInsert 
@dateTime datetime,
@game_id int,
@home_id int,
@away_id int,
@homeTri varchar(3),
@homeScore int,
@awayTri varchar(3),
@awayScore int,
@homeWs int,
@homeLs int,
@awayWs int,
@awayLs int,
@broadcast varchar(20),
@gameLabel varchar(255),
@gameSubLabel varchar(255),
@gameSubType varchar(255),
@gameStatus int,
@gameStatusText varchar(255),
@date Date,
@day varchar(3),
@season_id int as
insert into GameSchedule values(
@dateTime,
@game_id,
@home_id,
@away_id,
@homeTri,
@homeScore,
@awayTri,
@awayScore,
@homeWs,
@homeLs,
@awayWs,
@awayLs,
@broadcast,
@gameLabel,
@gameSubLabel,
@gameSubType,
@gameStatus,
@gameStatusText,
cast(@date as date),
@day,
@season_id)