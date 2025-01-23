

select s.game_id, e.name Team, m.name Matchup, s.dateTime time, concat('=HYPERLINK(""https://www.nba.com/stats/player/', t.player_id,'/boxscores-traditional"", ""', t.name, '"")') Player, t.Points, d5.PtsDeviation,
case when cast((8 - t.Points)/d5.PtsDeviation as decimal(18,2)) <= -4  then 1 
when cast((8 - t.Points)/d5.PtsDeviation as decimal(18,2)) >= 4 then 0
else (select 1 - CumulativeProbability from StandardNormalDistribution where ZScore = cast((8 - t.Points)/d5.PtsDeviation as decimal(18,2)))
end [8+],
case when cast((9 - t.Points)/d5.PtsDeviation as decimal(18,2)) <= -4 then 1 
when cast((9 - t.Points)/d5.PtsDeviation as decimal(18,2)) >= 4 then 0
else (select 1 - CumulativeProbability from StandardNormalDistribution where ZScore = cast((9 - t.Points)/d5.PtsDeviation as decimal(18,2)))
end [9+],
case when cast((10 - t.Points)/d5.PtsDeviation as decimal(18,2)) <= -4 then 1 
when cast((10 - t.Points)/d5.PtsDeviation as decimal(18,2)) >= 4 then 0
else (select 1 - CumulativeProbability from StandardNormalDistribution where ZScore = cast((10 - t.Points)/d5.PtsDeviation as decimal(18,2)))
end [10+],
case when cast((11 - t.Points)/d5.PtsDeviation as decimal(18,2)) <= -4 then 1 
when cast((11 - t.Points)/d5.PtsDeviation as decimal(18,2)) >= 4 then 0
else (select 1 - CumulativeProbability from StandardNormalDistribution where ZScore = cast((11 - t.Points)/d5.PtsDeviation as decimal(18,2)))
end [11+],
case when cast((12 - t.Points)/d5.PtsDeviation as decimal(18,2)) <= -4 then 1 
when cast((12 - t.Points)/d5.PtsDeviation as decimal(18,2)) >= 4 then 0
else (select 1 - CumulativeProbability from StandardNormalDistribution where ZScore = cast((12 - t.Points)/d5.PtsDeviation as decimal(18,2)))
end [12+],
case when cast((13 - t.Points)/d5.PtsDeviation as decimal(18,2)) <= -4 then 1 
when cast((13 - t.Points)/d5.PtsDeviation as decimal(18,2)) >= 4 then 0
else (select 1 - CumulativeProbability from StandardNormalDistribution where ZScore = cast((13 - t.Points)/d5.PtsDeviation as decimal(18,2)))
end [13+],
case when cast((14 - t.Points)/d5.PtsDeviation as decimal(18,2)) <= -4 then 1 
when cast((14 - t.Points)/d5.PtsDeviation as decimal(18,2)) >= 4 then 0
else (select 1 - CumulativeProbability from StandardNormalDistribution where ZScore = cast((14 - t.Points)/d5.PtsDeviation as decimal(18,2)))
end [14+],
case when cast((15 - t.Points)/d5.PtsDeviation as decimal(18,2)) <= -4 then 1 
when cast((15 - t.Points)/d5.PtsDeviation as decimal(18,2)) >= 4 then 0
else (select 1 - CumulativeProbability from StandardNormalDistribution where ZScore = cast((15 - t.Points)/d5.PtsDeviation as decimal(18,2)))
end [15+],
case when cast((16 - t.Points)/d5.PtsDeviation as decimal(18,2)) <= -4 then 1 
when cast((16 - t.Points)/d5.PtsDeviation as decimal(18,2)) >= 4 then 0
else (select 1 - CumulativeProbability from StandardNormalDistribution where ZScore = cast((16 - t.Points)/d5.PtsDeviation as decimal(18,2)))
end [16+],
case when cast((17 - t.Points)/d5.PtsDeviation as decimal(18,2)) <= -4 then 1 
when cast((17 - t.Points)/d5.PtsDeviation as decimal(18,2)) >= 4 then 0
else (select 1 - CumulativeProbability from StandardNormalDistribution where ZScore = cast((17 - t.Points)/d5.PtsDeviation as decimal(18,2)))
end [17+],
case when cast((18 - t.Points)/d5.PtsDeviation as decimal(18,2)) <= -4 then 1 
when cast((18 - t.Points)/d5.PtsDeviation as decimal(18,2)) >= 4 then 0
else (select 1 - CumulativeProbability from StandardNormalDistribution where ZScore = cast((18 - t.Points)/d5.PtsDeviation as decimal(18,2)))
end [18+],
case when cast((19 - t.Points)/d5.PtsDeviation as decimal(18,2)) <= -4 then 1 
when cast((19 - t.Points)/d5.PtsDeviation as decimal(18,2)) >= 4 then 0
else (select 1 - CumulativeProbability from StandardNormalDistribution where ZScore = cast((19 - t.Points)/d5.PtsDeviation as decimal(18,2)))
end [19+],
case when cast((20 - t.Points)/d5.PtsDeviation as decimal(18,2)) <= -4 then 1 
when cast((20 - t.Points)/d5.PtsDeviation as decimal(18,2)) >= 4 then 0
else (select 1 - CumulativeProbability from StandardNormalDistribution where ZScore = cast((20 - t.Points)/d5.PtsDeviation as decimal(18,2)))
end [20+],
case when cast((21 - t.Points)/d5.PtsDeviation as decimal(18,2)) <= -4 then 1 
when cast((21 - t.Points)/d5.PtsDeviation as decimal(18,2)) >= 4 then 0
else (select 1 - CumulativeProbability from StandardNormalDistribution where ZScore = cast((21 - t.Points)/d5.PtsDeviation as decimal(18,2)))
end [21+],
case when cast((22 - t.Points)/d5.PtsDeviation as decimal(18,2)) <= -4 then 1 
when cast((22 - t.Points)/d5.PtsDeviation as decimal(18,2)) >= 4 then 0
else (select 1 - CumulativeProbability from StandardNormalDistribution where ZScore = cast((22 - t.Points)/d5.PtsDeviation as decimal(18,2)))
end [22+],
case when cast((23 - t.Points)/d5.PtsDeviation as decimal(18,2)) <= -4 then 1 
when cast((23 - t.Points)/d5.PtsDeviation as decimal(18,2)) >= 4 then 0
else (select 1 - CumulativeProbability from StandardNormalDistribution where ZScore = cast((23 - t.Points)/d5.PtsDeviation as decimal(18,2)))
end [23+],
case when cast((24 - t.Points)/d5.PtsDeviation as decimal(18,2)) <= -4 then 1 
when cast((24 - t.Points)/d5.PtsDeviation as decimal(18,2)) >= 4 then 0
else (select 1 - CumulativeProbability from StandardNormalDistribution where ZScore = cast((24 - t.Points)/d5.PtsDeviation as decimal(18,2)))
end [24+],
case when cast((25 - t.Points)/d5.PtsDeviation as decimal(18,2)) <= -4 then 1 
when cast((25 - t.Points)/d5.PtsDeviation as decimal(18,2)) >= 4 then 0
else (select 1 - CumulativeProbability from StandardNormalDistribution where ZScore = cast((25 - t.Points)/d5.PtsDeviation as decimal(18,2)))
end [25+],
case when cast((26 - t.Points)/d5.PtsDeviation as decimal(18,2)) <= -4 then 1 
when cast((26 - t.Points)/d5.PtsDeviation as decimal(18,2)) >= 4 then 0
else (select 1 - CumulativeProbability from StandardNormalDistribution where ZScore = cast((26 - t.Points)/d5.PtsDeviation as decimal(18,2)))
end [26+],
case when cast((27 - t.Points)/d5.PtsDeviation as decimal(18,2)) <= -4 then 1 
when cast((27 - t.Points)/d5.PtsDeviation as decimal(18,2)) >= 4 then 0
else (select 1 - CumulativeProbability from StandardNormalDistribution where ZScore = cast((27 - t.Points)/d5.PtsDeviation as decimal(18,2)))
end [27+],
case when cast((28 - t.Points)/d5.PtsDeviation as decimal(18,2)) <= -4 then 1 
when cast((28 - t.Points)/d5.PtsDeviation as decimal(18,2)) >= 4 then 0
else (select 1 - CumulativeProbability from StandardNormalDistribution where ZScore = cast((28 - t.Points)/d5.PtsDeviation as decimal(18,2)))
end [28+],
case when cast((29 - t.Points)/d5.PtsDeviation as decimal(18,2)) <= -4 then 1 
when cast((29 - t.Points)/d5.PtsDeviation as decimal(18,2)) >= 4 then 0
else (select 1 - CumulativeProbability from StandardNormalDistribution where ZScore = cast((29 - t.Points)/d5.PtsDeviation as decimal(18,2)))
end [29+],
case when cast((30 - t.Points)/d5.PtsDeviation as decimal(18,2)) <= -4 then 1 
when cast((30 - t.Points)/d5.PtsDeviation as decimal(18,2)) >= 4 then 0
else (select 1 - CumulativeProbability from StandardNormalDistribution where ZScore = cast((30 - t.Points)/d5.PtsDeviation as decimal(18,2)))
end [30+],
case when cast((31 - t.Points)/d5.PtsDeviation as decimal(18,2)) <= -4 then 1 
when cast((31 - t.Points)/d5.PtsDeviation as decimal(18,2)) >= 4 then 0
else (select 1 - CumulativeProbability from StandardNormalDistribution where ZScore = cast((31 - t.Points)/d5.PtsDeviation as decimal(18,2)))
end [31+],
case when cast((32 - t.Points)/d5.PtsDeviation as decimal(18,2)) <= -4 then 1 
when cast((32 - t.Points)/d5.PtsDeviation as decimal(18,2)) >= 4 then 0
else (select 1 - CumulativeProbability from StandardNormalDistribution where ZScore = cast((32 - t.Points)/d5.PtsDeviation as decimal(18,2)))
end [32+],
case when cast((33 - t.Points)/d5.PtsDeviation as decimal(18,2)) <= -4 then 1 
when cast((33 - t.Points)/d5.PtsDeviation as decimal(18,2)) >= 4 then 0
else (select 1 - CumulativeProbability from StandardNormalDistribution where ZScore = cast((33 - t.Points)/d5.PtsDeviation as decimal(18,2)))
end [33+],
case when cast((34 - t.Points)/d5.PtsDeviation as decimal(18,2)) <= -4 then 1 
when cast((34 - t.Points)/d5.PtsDeviation as decimal(18,2)) >= 4 then 0
else (select 1 - CumulativeProbability from StandardNormalDistribution where ZScore = cast((34 - t.Points)/d5.PtsDeviation as decimal(18,2)))
end [34+],
case when cast((35 - t.Points)/d5.PtsDeviation as decimal(18,2)) <= -4 then 1 
when cast((35 - t.Points)/d5.PtsDeviation as decimal(18,2)) >= 4 then 0
else (select 1 - CumulativeProbability from StandardNormalDistribution where ZScore = cast((35 - t.Points)/d5.PtsDeviation as decimal(18,2)))
end [35+],
case when cast((40 - t.Points)/d5.PtsDeviation as decimal(18,2)) <= -4 then 1 
when cast((40 - t.Points)/d5.PtsDeviation as decimal(18,2)) >= 4 then 0
else (select 1 - CumulativeProbability from StandardNormalDistribution where ZScore = cast((40 - t.Points)/d5.PtsDeviation as decimal(18,2)))
end [40+],
case when cast((45 - t.Points)/d5.PtsDeviation as decimal(18,2)) <= -4 then 1 
when cast((45 - t.Points)/d5.PtsDeviation as decimal(18,2)) >= 4 then 0
else (select 1 - CumulativeProbability from StandardNormalDistribution where ZScore = cast((45 - t.Points)/d5.PtsDeviation as decimal(18,2)))
end [45+]
from PlayerTrend5 t inner join
		PlayerTrend5StdDeviation d5 on t.player_id = d5.player_id inner join
		team e on t.team_id = e.team_id and e.season_id = 2024 inner join
		GameSchedule s on (t.team_id = s.home_id or t.team_id = s.away_id) and s.date = cast(getdate() as date) inner join
		team m on (s.home_id = m.team_id or s.away_id = m.team_id) and e.team_id != m.team_id  and e.season_id = m.season_id
where t.Points >= 8 and d5.PtsDeviation != 0
order by datetime, s.game_id, team, [8+] desc
