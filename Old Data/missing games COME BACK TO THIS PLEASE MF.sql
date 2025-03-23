select distinct g.game_id game, b.game_id playerbox, t.game_id teambox, p.game_id playbyplay, h.name Home, a.name Away
from game g left join
		team h on g.team_idH = h.team_id and h.season_id = 2024 left join
		team a on g.team_idA = a.team_id and h.season_id = 2024 left join
		playerBox b on g.game_id = b.game_id left join
		teamBox t on g.game_id = t.game_id left join
		playByPlay p on g.game_id = p.game_id
where g.season_id = 2019
and (g.game_id is null or b.game_id is null or t.game_id is null or p.game_id is null)
order by g.game_id