



create view PlayerStatuses
as
select p.season_id, p.game_id, p.team_id, p.player_id,
case when p.status != 'ACTIVE' or replace(replace(p.minutesCalculated, 'PT', ''), 'M', '') <  (select cast(Minutes as decimal(18, 2))/2 from playerBoxAverage a where a.season_id = p.season_id and a.team_id = p.team_id and a.player_id = p.player_id)
		then 'DNP'
when p.status = 'ACTIVE' and replace(replace(p.minutesCalculated, 'PT', ''), 'M', '') >  (select cast(Minutes as decimal(18, 2))/2 from playerBoxAverage a where a.season_id = p.season_id and a.team_id = p.team_id and a.player_id = p.player_id)
		then 'Active'
else 'DNP' end Status

from playerBox p

