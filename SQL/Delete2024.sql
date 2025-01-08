create procedure Delete2024
as
delete from [arena]					where season_id = 2024
delete from [game]					where season_id = 2024		
delete from [official]				where season_id = 2024
delete from [playByPlay]			where season_id = 2024	
delete from playByPlayPlayoffs		where season_id = 2024		
delete from [player]				where season_id = 2024
delete from [playerBox]				where season_id = 2024		
delete from playerBoxPlayoffs		where season_id = 2024	
delete from PlayerTeam				where season_id = 2024
delete from [PlayoffBracket]		where season_id = 2024	
delete from [PlayoffPicture]		where season_id = 2024	
delete from PlayoffSeries			where season_id = 2024	
delete from team					where season_id = 2024
delete from teamBox					where season_id = 2024		
delete from teamBoxPlayoffs			where season_id = 2024		
