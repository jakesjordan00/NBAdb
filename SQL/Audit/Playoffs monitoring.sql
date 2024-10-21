

select * 
from playerBoxPlayoffs p inner join
		PlayoffSeries s on p.season_id = s.season_id and p.series_id = s.series_id
order by p.season_id desc, s.roundNumber desc, s.seriesNumber desc, p.game_id desc



select * 
from teamBoxPlayoffs p inner join
		PlayoffSeries s on p.season_id = s.season_id and p.series_id = s.series_id
order by p.season_id desc, s.roundNumber desc, s.seriesNumber desc, p.game_id desc


select * 
from playByPlayPlayoffs p inner join
		PlayoffSeries s on p.season_id = s.season_id and p.series_id = s.series_id
order by p.season_id desc, s.roundNumber desc, s.seriesNumber desc, p.game_id desc, p.actionNumber desc



select * 
from PlayoffSeries s
order by s.season_id desc, s.roundNumber desc, s.seriesNumber desc



/*

delete from playerBoxPlayoffs
delete from teamBoxPlayoffs
delete from playByPlayPlayoffs



delete from PlayoffSeries
 

*/