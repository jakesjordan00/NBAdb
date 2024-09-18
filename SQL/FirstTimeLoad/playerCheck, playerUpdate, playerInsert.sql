create procedure playerCheck @player_id	int, @id int
as
select * from player where player_id = @player_id and season_id = @id
go

create procedure playerInsert 
@id int,
@player_id	int,
@name		varchar(100),
@number		int,
@position	varchar(20),
@college	varchar(50),
@country	varchar(50),	
@draftYear	int,			
@draftRound	int,			
@draftPick	int				
as
insert into player values(
@id,
@player_id,
@name,		
@number,		
@position,
@college,	
@country,	
@draftYear,
@draftRound,
@draftPick
)
go

create procedure playerUpdate @player_id int, @position varchar(30), @id int
as
update player set position = @position where player_id = @player_id and season_id = @id

--playerCheck, playerUpdate, playerInsert