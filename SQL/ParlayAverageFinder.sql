
create procedure ParlayAverageFinder @Player varchar(255), @Team varchar(255), @Season int
as
select * 
from PlayerWLDeltas
where Name = @Player and Team = @Team and Season = @Season