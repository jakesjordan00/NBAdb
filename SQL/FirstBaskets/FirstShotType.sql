
create procedure FirstShotType
as
select distinct case when actionsub = 'DUNK' then 'Dunk'
					 when actionSub = 'personal' then 'Fouled'
					 when actionSub like '%1%' then 'Free Throw'
					 else actionSub end actionsub
from FirstBaskets
where actionSub like '%1%' or actionsub not like '% of %'