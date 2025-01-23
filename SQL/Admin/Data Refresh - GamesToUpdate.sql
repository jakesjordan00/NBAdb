







create procedure GamesToUpdate
as
select distinct s.game_id 
from GameSchedule s left join
		game g on s.game_id = g.game_id and s.season_id = g.season_id
where s.season_id = 2024 
and s.gameLabel != 'Preseason' 
and gameStatusText != 'PPD' 
and s.dateTime <= getdate() 
and(g.game_id is null 
or s.date >= cast(getdate() as date))



--select distinct game_id from GameSchedule where date >= '20250123' and datetime <= getdate()  and gameStatusText != 'PPD'