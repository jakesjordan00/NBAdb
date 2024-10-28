

create view TeamExtended
as
select t.season_id, t.team_id, c.Conference, t.tricode, t.city, t.name, concat('(', t.tricode, ') ', t.city, ' ', t.name) FullName,
sum(case when g.team_idW = t.team_id then 1 else 0 end) Wins,
sum(case when g.team_idL = t.team_id then 1 else 0 end) Losses,
rank()OVER (PARTITION by t.season_id, c.Conference order by sum(case when g.team_idW = t.team_id then 1 else 0 end) desc) ConferenceRank,
rank()OVER (PARTITION by t.season_id order by sum(case when g.team_idW = t.team_id then 1 else 0 end) desc) LeagueRank
from team t left join
		game g on (t.team_id = g.team_idW or t.team_id = g.team_idL) and t.season_id = g.season_id inner join
		TeamConference c on t.team_id = c.team_id

group by t.season_id, t.team_id, t.tricode, t.city, t.name, concat('(', t.tricode, ') ', t.city, ' ', t.name), c.Conference
