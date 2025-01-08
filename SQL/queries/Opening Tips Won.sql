select teamTricode, count(teamTricode) TipsWon
from playByPlay p
where p.season_id = 2024 --and clock in('PT12M00.00S', 'PT11M59.00S', 'PT11M58.00S', 'PT11M57.00S', 'PT11M56.00S', 'PT11M56.00S', 'PT11M55.00S')
and actionType = 'jumpball' and actionNumber < 10
group by teamTricode
order by count(teamTricode) desc
--order by game_id, actionNumber