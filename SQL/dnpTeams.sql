create PROCEDURE [dbo].[dnpTeams]
AS 
select concat('(',t.tricode, ') ',t.city, ' ', t.name) Team from Team t