
create procedure playoffGameInsert
@id		    int,
@series_id  varchar(30),
@game_id	int,
@date		date,
@team_idH	int,
@team_idA	int,
@team_idW	int,
@wScore		int,
@team_idL	int,
@lScore		int,
@arena_id	int,
@sellout	bit
as
insert into playoffGame values(
@id, 
@series_id,
@game_id,
@date,
@team_idH,
@team_idA,
@team_idW,
@wScore,
@team_idL,	
@lScore,	
@arena_id,	
@sellout	
)
