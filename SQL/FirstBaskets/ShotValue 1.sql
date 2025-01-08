create procedure ShotValue @shot varchar(3), @player int
as
--Player Shot Detail
select f.season_id
	 , f.team
	 , f.player
	 , f.Shot
	 , sum(case when Result = 'Make' then 1 else 0 end) Makes
	 , sum(case when Result != 'Make' then 1 else 0 end) Miss
     , count(Shot) Total,
	 cast(cast(sum(case when Result = 'Make' then 1 else 0 end)as decimal(18, 2))/cast(count(Shot)as decimal(18, 2)) as decimal(18, 2)) Pct
from FirstBaskets f
where f.season_id = 2024 and f.Shot = @shot and f.player_id = @player
group by f.season_id
	 , f.team
	 , f.player
	 , f.Shot
order by Total desc


select * 
from FirstBaskets f


