

--Kawhi's shots by location - Excel Friendly
SELECT pbp.[game_id]
      ,[shotType]
      ,[actionSub]
      ,case when g.team_idH = pbp.team_id then scoreHome else scoreAway end Score
      ,case when g.team_idA = pbp.team_id then scoreHome else scoreAway end OpScore
      ,abs(scoreHome - scoreAway) Lead
      ,[period] Q
      ,replace(replace(replace(clock, 'PT', ''), 'M', ':'), 'S', '') clock
      ,concat(pbp.[season_id], '') season
      ,[teamTricode] Tri,
concat('=hyperlink("https://www.nba.com/stats/events?CFID=&CFPARAMS=&GameEventID=', actionNumber, '&GameID=00', pbp.game_id, '&Season=', pbp.season_id, '-', pbp.season_id - 2000 + 1, '&flag=1', '&title=', 
replace(REPLACE(replace(description, concat(Left(p.name, 1), '. '), ''), ' ', '%20'), 'S.%20', ''), '", "', concat(upper(left(replace(description, 'K. Leonard ', ''), 1)),right(replace(description, 'K. Leonard ', ''), len(replace(description, 'K. Leonard ', '')) -1)), '")') Play,

concat('=hyperlink("https://www.nba.com/game/', '...-vs-...', '-00', pbp.game_id, '/play-by-play?watchFullGame=true', '", "', teamTricode, ' vs ', 
(select distinct top 1 teamTricode from playByPlay b where pbp.game_id = b.game_id and pbp.team_id != b.team_id and pbp.season_id = b.season_id and teamTricode is not null and teamTricode != ''),  ' - Q', period, ' ', replace(replace(replace(clock, 'PT', ''), 'M', ':'), 'S', ''), '")') Link
      ,shotDistance
      ,case when x is null then xLegacy else cast(x as decimal(18,2)) end x
      ,case when y is null then yLegacy else cast(y as decimal(18,2)) end y
      ,case when xLegacy is null then cast(x as int) else cast(xLegacy as int) end xLegacy
      ,case when yLegacy is null then cast(y as int) else cast(yLegacy as int) end yLegacy
      ,cast(x as int) trueX
      ,cast(y as int) trueY, side
from playByPlay pbp inner join
        player p on pbp.player_id = p.player_id and p.season_id = 2021 inner join
        game g on pbp.game_id = g.game_id
where p.name = 'Kawhi Leonard' and shotType in('2PTM', '3PTM')

union

SELECT pbp.[game_id]
      ,[shotType]
      ,[actionSub]
      ,case when g.team_idH = pbp.team_id then scoreHome else scoreAway end Score
      ,case when g.team_idA = pbp.team_id then scoreHome else scoreAway end OpScore
      ,abs(scoreHome - scoreAway) Lead
      ,[period] Q
      ,replace(replace(replace(clock, 'PT', ''), 'M', ':'), 'S', '') clock
       ,concat(pbp.[season_id],
       case when s.roundDesc = 'First Round' then ' 1st Round'
            when s.roundDesc = 'Conf. Semifinals' then concat(' ', left(s.seriesConference, 4), ' Semis')
            when s.roundDesc = 'Conf. Finals' then concat(' ', left(s.seriesConference, 1), 'CF')
            when s.roundDesc = 'NBA Finals' then ' Finals' 
       else ' Playoffs' end)
      ,[teamTricode] Tri,
concat('=hyperlink("https://www.nba.com/stats/events?CFID=&CFPARAMS=&GameEventID=', actionNumber, '&GameID=00', pbp.game_id, '&Season=', pbp.season_id, '-', pbp.season_id - 2000 + 1, '&flag=1', '&title=', 
replace(REPLACE(replace(description, concat(Left(p.name, 1), '. '), ''), ' ', '%20'), 'S.%20', ''), '", "', concat(upper(left(replace(description, 'K. Leonard ', ''), 1)),right(replace(description, 'K. Leonard ', ''), len(replace(description, 'K. Leonard ', '')) -1)), '")') Play,

concat('=hyperlink("https://www.nba.com/game/', '...-vs-...', '-00', pbp.game_id, '/play-by-play?watchFullGame=true', '", "', teamTricode, ' vs ', 
(select distinct top 1 teamTricode from playByPlayPlayoffs b where pbp.game_id = b.game_id and pbp.team_id != b.team_id and pbp.season_id = b.season_id and teamTricode is not null and teamTricode != ''),  ' - Q', period, ' ', replace(replace(replace(clock, 'PT', ''), 'M', ':'), 'S', ''), '")') Link
      ,shotDistance
      ,case when x is null then xLegacy else cast(x as decimal(18,2)) end x
      ,case when y is null then yLegacy else cast(y as decimal(18,2)) end y
      ,case when xLegacy is null then cast(x as int) else cast(xLegacy as int) end xLegacy
      ,case when yLegacy is null then cast(y as int) else cast(yLegacy as int) end yLegacy   
      ,cast(x as int) trueX
      ,cast(y as int) trueY, side
from playByPlayPlayoffs pbp inner join
        player p on pbp.player_id = p.player_id and p.season_id = 2021 left join
        PlayoffSeries s on pbp.series_id = s.series_id inner join
        playoffGame g on pbp.game_id = g.game_id
where p.name = 'Kawhi Leonard' and shotType in('2PTM', '3PTM')
order by trueX, trueY



--4981
