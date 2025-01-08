
create procedure Lineups_pbpGamesV1
as
select distinct p.game_id, s.home_id, s.away_id
from playByPlay p left join
		game g on p.game_id = g.game_id and p.season_id = g.season_id left join
		GameSchedule s on p.game_id = s.game_id and p.season_id = s.season_id
where g.date <= cast(getdate() as date) and p.season_id = 2024
and s.gameLabel != 'Preseason'
go







create procedure Lineups_starters @game_id int
as
select *
from StartingLineups s
where s.game_id = @game_id
and position != ''




create procedure Lineups_pbpSelect @game_id int
as
select *
from playByPlay p
where p.game_id = @game_id
order by actionNumber
go