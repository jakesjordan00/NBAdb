
select s.game_id, e.name Team, m.name Matchup, s.datetime time, concat('=HYPERLINK(""https://www.nba.com/stats/player/', t.player_id,'/boxscores-traditional"", ""', t.name, '"")') Player, t.FG3M, d5.FG3MDeviation Dev,
case when cast((1 - t.FG3M)/d5.FG3MDeviation as decimal(18,2)) <= -4 then 1 
when cast((1 - t.FG3M)/d5.FG3MDeviation as decimal(18,2)) >= 4 then 0
else (select 1 - CumulativeProbability from StandardNormalDistribution where ZScore = cast((1 - t.FG3M)/d5.FG3MDeviation as decimal(18,2)))
end [1+],
case when cast((2 - t.FG3M)/d5.FG3MDeviation as decimal(18,2)) <= -4 then 1 
when cast((2 - t.FG3M)/d5.FG3MDeviation as decimal(18,2)) >= 4 then 0
else (select 1 - CumulativeProbability from StandardNormalDistribution where ZScore = cast((2 - t.FG3M)/d5.FG3MDeviation as decimal(18,2)))
end [2+],
case when cast((3 - t.FG3M)/d5.FG3MDeviation as decimal(18,2)) <= -4 then 1 
when cast((3 - t.FG3M)/d5.FG3MDeviation as decimal(18,2)) >= 4 then 0
else (select 1 - CumulativeProbability from StandardNormalDistribution where ZScore = cast((3 - t.FG3M)/d5.FG3MDeviation as decimal(18,2)))
end [3+],
case when cast((4 - t.FG3M)/d5.FG3MDeviation as decimal(18,2)) <= -4 then 1 
when cast((4 - t.FG3M)/d5.FG3MDeviation as decimal(18,2)) >= 4 then 0
else (select 1 - CumulativeProbability from StandardNormalDistribution where ZScore = cast((4 - t.FG3M)/d5.FG3MDeviation as decimal(18,2)))
end [4+],
case when cast((5 - t.FG3M)/d5.FG3MDeviation as decimal(18,2)) <= -4 then 1 
when cast((5 - t.FG3M)/d5.FG3MDeviation as decimal(18,2)) >= 4 then 0
else (select 1 - CumulativeProbability from StandardNormalDistribution where ZScore = cast((5 - t.FG3M)/d5.FG3MDeviation as decimal(18,2)))
end [5+],
case when cast((6 - t.FG3M)/d5.FG3MDeviation as decimal(18,2)) <= -4 then 1 
when cast((6 - t.FG3M)/d5.FG3MDeviation as decimal(18,2)) >= 4 then 0
else (select 1 - CumulativeProbability from StandardNormalDistribution where ZScore = cast((6 - t.FG3M)/d5.FG3MDeviation as decimal(18,2)))
end [6+]
from PlayerTrend5 t inner join
		PlayerTrend5StdDeviation d5 on t.player_id = d5.player_id and t.team_id = d5.team_id inner join
		team e on t.team_id = e.team_id and e.season_id = 2024 inner join
		GameSchedule s on (t.team_id = s.home_id or t.team_id = s.away_id) and s.date = cast(getdate() as date) inner join
		team m on (s.home_id = m.team_id or s.away_id = m.team_id) and e.team_id != m.team_id  and e.season_id = m.season_id
where t.FG3M >= 1 and d5.FG3MDeviation != 0
order by datetime, s.game_id, team, [1+] desc
