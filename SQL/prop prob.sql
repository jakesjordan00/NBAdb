



create procedure PropProbabilty @Prop float, @Average float, @StdDev float
as
select 1 - CumulativeProbability Probability
from StandardNormalDistribution
where ZScore = Round((@Prop - @Average)/@StdDev, 2) 
go


execute PropProbabilty @Prop = 29, @Average = 26.56, @StdDev = 6.61 --Lebron Points in Win

execute PropProbabilty @Prop = 29, @Average = 29.67, @StdDev = 6.61 --Lebron Points trend


select 1 - CumulativeProbability
from StandardNormalDistribution
where ZScore = (select Round((30 - 25.66)/6.61, 2))

select *
from StandardNormalDistribution
where ZScore = '0.66'



select *
from WLplayerBoxAverage where player_id = 203076 and season_id = 2023


select * from PlayerTrend where player_id = 2544


select * from player p where p.name = 'Anthony Davis' --203076


select *
from WinsPlayerStdDeviation where player_id = 1630559 and season_id = 2023



execute PropProbabilty @Prop = 29, @Average = 26.56, @StdDev = 6.61 --Lebron Points in Win

execute PropProbabilty @Prop = 29, @Average = 29.67, @StdDev = 6.61 --Lebron Points trend


execute PropProbabilty @Prop = 7, @Average = 8.41, @StdDev = 2.822 --Lebron Assists in Win

execute PropProbabilty @Prop = 7, @Average = 12, @StdDev = 2.822 --Lebron Assists trend


execute PropProbabilty @Prop = 6, @Average = 7, @StdDev = 3.334 --Lebron Reb in Win

execute PropProbabilty @Prop = 6, @Average = 8, @StdDev = 3.334 --Lebron Reb trend



select 1 - CumulativeProbability Probability
from StandardNormalDistribution
where ZScore = Round((29 - (select avg(Points)
from playerBox p
where p.player_id = 2544 and p.season_id = 2023 and p.assists > 8 and p.reboundsTotal > 8))/6.61, 2) 
go

select avg(Points)
from playerBox p
where p.player_id = 2544 and p.season_id = 2023 and p.assists > 8 and p.reboundsTotal > 8












/*
p > 30 

z = (30-25)/6.61 = .756429
z = .77337 
z = 1 - .77337 = .22663 = 22.663% chance to score 30 or more


p > 28 = .546

*/