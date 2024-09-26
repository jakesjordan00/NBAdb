

create view playerBoxAverage
as
select pb.season_id,
	   concat(t.City, ' ', t.name) Team,
	   p.name Name, 
	   case when tb.points > tb.pointsAgainst then 1 else 0 end Win, 
	   count(pb.game_id) Games,
	   round(avg(cast(pb.points as float)), 2) Points,
	   round(avg(cast(pb.Assists as float)), 2) Assists,
	   round(avg(cast(pb.reboundsTotal as float)), 2) Rebounds,
	   round(avg(cast(pb.blocks as float)), 2) Blocks,
	   round(avg(cast(pb.steals as float)), 2) Steals,
	   round(avg(cast(pb.fieldGoalsMade as float)), 2) FGM,
	   round(avg(cast(pb.fieldGoalsAttempted as float)), 2) FGA,
	   round(avg(pb.fieldGoalsPercentage) * 100, 2) [FG%],
	   round(avg(cast(pb.threePointersMade as float)), 2) FG3M,
	   round(avg(cast(pb.threePointersAttempted as float)), 2) FG3A,
	   round(avg(pb.threePointersPercentage) * 100, 2) [FG3%]

from playerBox pb inner join
		player p on pb.player_id = p.player_id and pb.season_id = p.season_id inner join
		team t on pb.team_id = t.team_id and pb.season_id = t.season_id inner join
		teamBox tb on pb.team_id = tb.team_id and pb.game_id = tb.game_id and pb.season_id = tb.season_id
group by pb.season_id, concat(t.City, ' ', t.name), p.name, case when tb.points > tb.pointsAgainst then 1 else 0 end