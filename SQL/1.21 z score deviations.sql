


select *

from PlayerTrend5Detail d inner join
		PlayerTrend5 t on d.player_id = t.player_id 
where d.player_id in(
203897, --Zach LaVine, Bulls
202696, --Nikola Vucevic, Bulls
1628366, --Lonzo Ball, Bulls
1629632, --Coby White, Bulls
1630245, --Ayo Dosunmu, Bulls
1630172, --Patrick Williams, Bulls
1630581, --Josh Giddey, Bulls
1627826, --Ivica Zubac, Clippers
201935, --James Harden, Clippers
1626181 --Norman Powell, Clippers

)



--Norman
select Zscore, 
1 - CumulativeProbability CumulativeProbability
from StandardNormalDistribution
where ZScore = (
select cast((25 - t5.Points)/d5.PtsDeviation as decimal(18,2))
from PlayerTrend5 t5 inner join
		PlayerTrend5StdDeviation d5 on t5.player_id = d5.player_id
where t5.player_id = 1626181
)




select Zscore, 
1 - CumulativeProbability CumulativeProbability
from StandardNormalDistribution
where ZScore = (
select cast((3 - t5.FG3M)/d5.FG3MDeviation as decimal(18,2))
from PlayerTrend5 t5 inner join
		PlayerTrend5StdDeviation d5 on t5.player_id = d5.player_id
where t5.player_id = 1626181
)


--James Harden
select Zscore, 
1 - CumulativeProbability CumulativeProbability
from StandardNormalDistribution
where ZScore = (
select cast((19 - t5.Points)/d5.PtsDeviation as decimal(18,2))
from PlayerTrend5 t5 inner join
		PlayerTrend5StdDeviation d5 on t5.player_id = d5.player_id
where t5.player_id = 201935
)
select Zscore, 
1 - CumulativeProbability CumulativeProbability
from StandardNormalDistribution
where ZScore = (
select cast((8 - t5.Assists)/d5.AstDeviation as decimal(18,2))
from PlayerTrend5 t5 inner join
		PlayerTrend5StdDeviation d5 on t5.player_id = d5.player_id
where t5.player_id = 201935
)

select Zscore, 
1 - CumulativeProbability CumulativeProbability
from StandardNormalDistribution
where ZScore = (
select cast((2 - t5.FG3M)/d5.FG3MDeviation as decimal(18,2))
from PlayerTrend5 t5 inner join
		PlayerTrend5StdDeviation d5 on t5.player_id = d5.player_id
where t5.player_id = 201935
)

select Zscore, 
1 - CumulativeProbability CumulativeProbability
from StandardNormalDistribution
where ZScore = (
select cast((4 - t5.Rebounds)/d5.RebDeviation as decimal(18,2))
from PlayerTrend5 t5 inner join
		PlayerTrend5StdDeviation d5 on t5.player_id = d5.player_id
where t5.player_id = 201935
)



--1630581 giddey
--203897, --Zach LaVine, Bulls
--202696, vuc
--1630172, --Patrick Williams, Bulls
select Zscore, 
1 - CumulativeProbability CumulativeProbability
from StandardNormalDistribution
where ZScore = (
select cast((23 - t5.Points)/d5.PtsDeviation as decimal(18,2))
from PlayerTrend5 t5 inner join
		PlayerTrend5StdDeviation d5 on t5.player_id = d5.player_id
where t5.player_id = 203897
)

select Zscore, 
1 - CumulativeProbability CumulativeProbability
from StandardNormalDistribution
where ZScore = (
select cast((3 - t5.FG3M)/d5.FG3MDeviation as decimal(18,2))
from PlayerTrend5 t5 inner join
		PlayerTrend5StdDeviation d5 on t5.player_id = d5.player_id
where t5.player_id = 1628366
)


select Zscore, 
1 - CumulativeProbability CumulativeProbability
from StandardNormalDistribution
where ZScore = (
select cast((20 - t5.Points)/d5.PtsDeviation as decimal(18,2))
from PlayerTrend5 t5 inner join
		PlayerTrend5StdDeviation d5 on t5.player_id = d5.player_id
where t5.player_id = 202696
)

select Zscore, 
1 - CumulativeProbability CumulativeProbability
from StandardNormalDistribution
where ZScore = (
select cast((3 - t5.Assists)/d5.AstDeviation as decimal(18,2))
from PlayerTrend5 t5 inner join
		PlayerTrend5StdDeviation d5 on t5.player_id = d5.player_id
where t5.player_id = 202696
)

select Zscore, 
1 - CumulativeProbability CumulativeProbability
from StandardNormalDistribution
where ZScore = (
select cast((2 - t5.FG3M)/d5.FG3MDeviation as decimal(18,2))
from PlayerTrend5 t5 inner join
		PlayerTrend5StdDeviation d5 on t5.player_id = d5.player_id
where t5.player_id = 1630581
)

select Zscore, 
1 - CumulativeProbability CumulativeProbability
from StandardNormalDistribution
where ZScore = (
select cast((11 - t5.Rebounds)/d5.RebDeviation as decimal(18,2))
from PlayerTrend5 t5 inner join
		PlayerTrend5StdDeviation d5 on t5.player_id = d5.player_id
where t5.player_id = 202696
)
