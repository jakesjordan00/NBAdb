--Full Query Under
SELECT  t.player_id
      ,t.name
      ,e.name Team
      ,g.dateTime                           [Game Time]
      ,g.game_id
      ,t.Minutes                            [3_Minutes]
      ,t5.Minutes                            [5_Minutes]
      ,t.Points                             [3_Points]
      ,t5.Points                             [5_Points]
      ,t.Points + s.PtsDeviation            [3_65.87pct]
      ,t5.Points + s5.PtsDeviation            [5_65.87pct]
      ,t.Points + (s.PtsDeviation * 2)      [3_86.41pct]
      ,t5.Points + (s5.PtsDeviation * 2)      [5_86.41pct]
      ,t.Assists                            [3_Assists]
      ,t5.Assists                            [5_Assists]
      ,t.Assists +  s.AstDeviation          [3_65.87pct]
      ,t5.Assists +  s5.AstDeviation          [5_65.87pct]
      ,t.Assists + (s.AstDeviation * 2)     [3_86.41pct]
      ,t5.Assists + (s5.AstDeviation * 2)     [5_86.41pct]
      ,t.Rebounds                           [3_Rebounds]
      ,t5.Rebounds                           [5_Rebounds]
      ,t.Rebounds +  s.RebDeviation         [3_65.87pct]
      ,t5.Rebounds +  s5.RebDeviation         [5_65.87pct]
      ,t.Rebounds + (s.RebDeviation * 2)    [3_86.41pct]
      ,t5.Rebounds + (s5.RebDeviation * 2)    [5_86.41pct]
      ,t.FG3M                               [3_FG3M]
      ,t5.FG3M                               [5_FG3M]
      ,t.FG3M +  s.FG3MDeviation            [3_65.87pct]
      ,t5.FG3M +  s5.FG3MDeviation            [5_65.87pct]
      ,t.FG3M + (s.FG3MDeviation * 2)       [3_86.41pct]
      ,t5.FG3M + (s5.FG3MDeviation * 2)       [5_86.41pct]
      ,t.FG3A                               [3_FG3A]
      ,t5.FG3A                               [5_FG3A]
      ,t.FG3A +  s.FG3ADeviation            [3_65.87pct]
      ,t5.FG3A +  s5.FG3ADeviation            [5_65.87pct]
      ,t.FG3A + (s.FG3ADeviation * 2)       [3_86.41pct]
      ,t5.FG3A + (s5.FG3ADeviation * 2)       [5_86.41pct]
      ,t.[FG3%]                             [3_FG3%]
      ,t5.[FG3%]                             [5_FG3%]
      ,t.FGM                                [3_FGM]
      ,t5.FGM                                [5_FGM]
      ,t.FGM +  s.FGMDeviation              [3_65.87pct]
      ,t5.FGM +  s5.FGMDeviation              [5_65.87pct]
      ,t.FGM + (s.FGMDeviation * 2)         [3_86.41pct]
      ,t5.FGM + (s5.FGMDeviation * 2)         [5_86.41pct]
      ,t.FGA                                [3_FGA]
      ,t5.FGA                                [5_FGA]
      ,t.FGA +  s.FGADeviation              [3_65.87pct]
      ,t5.FGA +  s5.FGADeviation              [5_65.87pct]
      ,t.FGA + (s.FGADeviation * 2)         [3_86.41pct]
      ,t5.FGA + (s5.FGADeviation * 2)         [5_86.41pct]
      ,t.[FG%]                              [3_FG%]
      ,t5.[FG%]                              [5_FG%]
      ,t.FG2M                               [3_FG2M]
      ,t5.FG2M                               [5_FG2M]
      ,t.FG2M +  s.FG2MDeviation            [3_65.87pct]
      ,t5.FG2M +  s5.FG2MDeviation            [5_65.87pct]
      ,t.FG2M + (s.FG2MDeviation * 2)       [3_86.41pct]
      ,t5.FG2M + (s5.FG2MDeviation * 2)       [5_86.41pct]
      ,t.FG2A                               [3_FG2A]
      ,t5.FG2A                               [5_FG2A]
      ,t.FG2A +  s.FG2ADeviation            [3_65.87pct]
      ,t5.FG2A +  s5.FG2ADeviation            [5_65.87pct]
      ,t.FG2A + (s.FG2ADeviation * 2)       [3_86.41pct]
      ,t5.FG2A + (s5.FG2ADeviation * 2)       [5_86.41pct]
      ,t.[FG2%]     [3_FG2%]
      ,t5.[FG2%]    [5_FG2%]
      ,t.FTM        [3_FTM]
      ,t5.FTM       [5_FTM]
      ,t.FTA        [3_FTA]
      ,t5.FTA       [5_FTA]
      ,t.[FT%]      [3_FT%]
      ,t5.[FT%]     [5_FT%]
      ,t.Blocks     [3_Blocks]
      ,t5.Blocks    [5_Blocks]
      ,t.Steals     [3_Steals]
      ,t5.Steals    [5_Steals]
      ,t.Turnovers  [3_Turnovers]
      ,t5.Turnovers [5_Turnovers]
from PlayerTrend t inner join
		PlayerTrend3StdDeviation s on t.player_id = s.player_id and t.team_id = s.team_id inner join
        team e on t.team_id = e.team_id and e.season_id = 2024 inner join
        PlayerTrend5 t5 on t.player_id = t5.player_id and t.team_id = t5.team_id inner join
		PlayerTrend5StdDeviation s5 on t5.player_id = s5.player_id and t5.team_id = s5.team_id inner join
        GameSchedule g on (t.team_id = g.home_id or t.team_id = g.away_id) and g.date = cast(getdate() as date)
where 
(t.Points + s.PtsDeviation > 9 
and t5.Points + s5.PtsDeviation > 9) or 
(t.Assists + s.AstDeviation > 1.5 
and t5.Assists + s5.AstDeviation > 1.5) or 
(t.Rebounds + s.RebDeviation > 3 
and t5.Rebounds + s5.RebDeviation > 3) or
(t.FG3M + s.FG3MDeviation > 1 
and t5.FG3M + s5.FG3MDeviation > 1) or
(t.FG3A + s.FG3ADeviation > 4 
and t5.FG3A + s5.FG3ADeviation > 4)
order by [Game Time], game_id, Team