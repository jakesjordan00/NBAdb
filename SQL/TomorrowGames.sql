create procedure TomorrowGames @day int
as
SELECT  FORMAT(s.dateTime, 'h:mm tt') dateTime
      , s.game_id
      , s.home_id
      , s.homeTri
	  , h.city homeCity
	  , h.name homeName
      , s.homeScore
      , s.away_id
      , s.awayTri
	  , a.city awayCity
	  , a.name awayName
      , s.awayScore
      , s.homeWs
      , s.homeLs
      , s.awayWs
      , s.awayLs
      , s.broadcast
      , s.gameLabel
      , s.gameSubLabel
      , s.gameSubType
      , s.gameStatus
      , s.gameStatusText
      , s.date
      , s.day
      , s.season_id
from GameSchedule s inner join
		team h on s.home_id = h.team_id and s.season_id = h.season_id inner join
		team a on s.away_id = a.team_id and s.season_id = a.season_id
where s.date = cast(getdate() + @day as date)
order by s.dateTime