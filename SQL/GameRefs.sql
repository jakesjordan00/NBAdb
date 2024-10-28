create view GameRefs
as
select distinct season_id, game_id, official_id
from playByPlay
where official_id is not null