create procedure deleteGames @game_id int
as
delete from playerBox  where game_id = @game_id
delete from teamBox    where game_id = @game_id
select count(actionNumber) from playByPlay where game_id = @game_id

