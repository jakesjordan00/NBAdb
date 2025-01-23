
select s.game_id, e.name Team, m.name Matchup, s.datetime time, concat('=HYPERLINK(""https://www.nba.com/stats/player/', t.player_id,'/boxscores-traditional"", ""', t.name, '"")') Player, t.Assists, d5.AstDeviation Dev,
case when cast((2 - t.Assists)/d5.AstDeviation as decimal(18,2)) <= -4 then 1 
when cast((2 - t.Assists)/d5.AstDeviation as decimal(18,2)) >= 4 then 0
else (select 1 - CumulativeProbability from StandardNormalDistribution where ZScore = cast((2 - t.Assists)/d5.AstDeviation as decimal(18,2)))
end [2+],
case when cast((3 - t.Assists)/d5.AstDeviation as decimal(18,2)) <= -4 then 1 
when cast((3 - t.Assists)/d5.AstDeviation as decimal(18,2)) >= 4 then 0
else (select 1 - CumulativeProbability from StandardNormalDistribution where ZScore = cast((3 - t.Assists)/d5.AstDeviation as decimal(18,2)))
end [3+],
case when cast((4 - t.Assists)/d5.AstDeviation as decimal(18,2)) <= -4 then 1 
when cast((4 - t.Assists)/d5.AstDeviation as decimal(18,2)) >= 4 then 0
else (select 1 - CumulativeProbability from StandardNormalDistribution where ZScore = cast((4 - t.Assists)/d5.AstDeviation as decimal(18,2)))
end [4+],
case when cast((5 - t.Assists)/d5.AstDeviation as decimal(18,2)) <= -4 then 1 
when cast((5 - t.Assists)/d5.AstDeviation as decimal(18,2)) >= 4 then 0
else (select 1 - CumulativeProbability from StandardNormalDistribution where ZScore = cast((5 - t.Assists)/d5.AstDeviation as decimal(18,2)))
end [5+],
case when cast((6 - t.Assists)/d5.AstDeviation as decimal(18,2)) <= -4 then 1 
when cast((6 - t.Assists)/d5.AstDeviation as decimal(18,2)) >= 4 then 0
else (select 1 - CumulativeProbability from StandardNormalDistribution where ZScore = cast((6 - t.Assists)/d5.AstDeviation as decimal(18,2)))
end [6+],
case when cast((7 - t.Assists)/d5.AstDeviation as decimal(18,2)) <= -4 then 1 
when cast((7 - t.Assists)/d5.AstDeviation as decimal(18,2)) >= 4 then 0
else (select 1 - CumulativeProbability from StandardNormalDistribution where ZScore = cast((7 - t.Assists)/d5.AstDeviation as decimal(18,2)))
end [7+],
case when cast((8 - t.Assists)/d5.AstDeviation as decimal(18,2)) <= -4 then 1 
when cast((8 - t.Assists)/d5.AstDeviation as decimal(18,2)) >= 4 then 0
else (select 1 - CumulativeProbability from StandardNormalDistribution where ZScore = cast((8 - t.Assists)/d5.AstDeviation as decimal(18,2)))
end [8+],
case when cast((9 - t.Assists)/d5.AstDeviation as decimal(18,2)) <= -4 then 1 
when cast((9 - t.Assists)/d5.AstDeviation as decimal(18,2)) >= 4 then 0
else (select 1 - CumulativeProbability from StandardNormalDistribution where ZScore = cast((9 - t.Assists)/d5.AstDeviation as decimal(18,2)))
end [9+],
case when cast((10 - t.Assists)/d5.AstDeviation as decimal(18,2)) <= -4 then 1 
when cast((10 - t.Assists)/d5.AstDeviation as decimal(18,2)) >= 4 then 0
else (select 1 - CumulativeProbability from StandardNormalDistribution where ZScore = cast((10 - t.Assists)/d5.AstDeviation as decimal(18,2)))
end [10+],
case when cast((11 - t.Assists)/d5.AstDeviation as decimal(18,2)) <= -4 then 1 
when cast((11 - t.Assists)/d5.AstDeviation as decimal(18,2)) >= 4 then 0
else (select 1 - CumulativeProbability from StandardNormalDistribution where ZScore = cast((11 - t.Assists)/d5.AstDeviation as decimal(18,2)))
end [11+],
case when cast((12 - t.Assists)/d5.AstDeviation as decimal(18,2)) <= -4 then 1 
when cast((12 - t.Assists)/d5.AstDeviation as decimal(18,2)) >= 4 then 0
else (select 1 - CumulativeProbability from StandardNormalDistribution where ZScore = cast((12 - t.Assists)/d5.AstDeviation as decimal(18,2)))
end [12+]
from PlayerTrend5 t inner join
		PlayerTrend5StdDeviation d5 on t.player_id = d5.player_id and t.team_id = d5.team_id inner join
		team e on t.team_id = e.team_id and e.season_id = 2024 inner join
		GameSchedule s on (t.team_id = s.home_id or t.team_id = s.away_id) and s.date = cast(getdate() as date) inner join
		team m on (s.home_id = m.team_id or s.away_id = m.team_id) and e.team_id != m.team_id  and e.season_id = m.season_id
where t.Assists >= 2 and d5.AstDeviation != 0
order by datetime, s.game_id, team, [2+] desc
