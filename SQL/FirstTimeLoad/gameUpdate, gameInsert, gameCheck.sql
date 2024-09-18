

create procedure gameUpdate @game_id int, @team_idW int, @team_idL int, @wScore int, @lScore int, @id int
as
update game set team_idW = @team_idW, wScore = @wScore, team_idL = @team_idL, lScore = @lScore
where game_id = @game_id and season_id = @id
go

create procedure gameInsert
@id		    int,
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
insert into game values(
@id, 
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
go


create procedure gameCheck @game_id	int, @id int
as
select * from game where game_id = @game_id and season_id = @id