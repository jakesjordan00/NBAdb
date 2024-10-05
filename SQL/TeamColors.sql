create table TeamColors(
team_id int,
Team varchar(255),
Background varchar(255),
Color1 varchar(255),
Color2 varchar(255),
Color3 varchar(255),
Black varchar(255),
White varchar(255))

insert into TeamColors values((select distinct team_id from team where name = 'Wizards'), 'Wizards', 'c4ced4', '002b5c', 'e31837', '', '000000', 'FFFFFF')
insert into TeamColors values((select distinct team_id from team where name = 'Warriors'), 'Warriors', '000000', '006bb6', 'fdb927', '26282a', '000000', 'FFFFFF')
insert into TeamColors values((select distinct team_id from team where name = 'Trail Blazers'), 'Trail Blazers', '000000', 'e03a3e', '', '', '000000', 'FFFFFF')
insert into TeamColors values((select distinct team_id from team where name = 'Timberwolves'), 'Timberwolves', '9ea2a2', '0c2340', '78be20', '', '000000', 'FFFFFF')
insert into TeamColors values((select distinct team_id from team where name = 'Thunder'), 'Thunder', '000000', '007ac1', 'ef3b24', '', '000000', 'FFFFFF')
insert into TeamColors values((select distinct team_id from team where name = 'Suns'), 'Suns', '000000', '1d1160', 'e56020', '', '000000', 'FFFFFF')
insert into TeamColors values((select distinct team_id from team where name = 'Spurs'), 'Spurs', '000000', 'c4ced4', '', '', '000000', 'FFFFFF')
insert into TeamColors values((select distinct team_id from team where name = 'Rockets'), 'Rockets', '000000', 'ce1141', 'eee1c6', '00471b', '000000', 'FFFFFF')
insert into TeamColors values((select distinct team_id from team where name = 'Raptors'), 'Raptors', '000000', 'b4975a', 'b4975a', '', '000000', 'FFFFFF')
insert into TeamColors values((select distinct team_id from team where name = 'Pistons'), 'Pistons', '000000', '1d428a', 'C8102E', '', '000000', 'FFFFFF')
insert into TeamColors values((select distinct team_id from team where name = 'Pelicans'), 'Pelicans', '000000', '002a5c', 'b5985a', 'C8102E', '000000', 'FFFFFF')
insert into TeamColors values((select distinct team_id from team where name = 'Pacers'), 'Pacers', 'bec0c2', '002d62', 'fdbb30', '', '000000', 'FFFFFF')
insert into TeamColors values((select distinct team_id from team where name = 'Nuggets'), 'Nuggets', '0e2240', '8b2131', 'fec524', '', '000000', 'FFFFFF')
insert into TeamColors values((select distinct team_id from team where name = 'Nets'), 'Nets', '000000', '777d84', '', '', '000000', 'FFFFFF')
insert into TeamColors values((select distinct team_id from team where name = 'Mavericks'), 'Mavericks', 'b8c4ca', '00538c', '', '', '000000', 'FFFFFF')
insert into TeamColors values((select distinct team_id from team where name = 'Magic'), 'Magic', 'c4ced4', '0077c0', '', '', '000000', 'FFFFFF')
insert into TeamColors values((select distinct team_id from team where name = 'Lakers'), 'Lakers', '000000', '552583', 'f9a01b', 'f9a01b', '000000', 'FFFFFF')
insert into TeamColors values((select distinct team_id from team where name = 'Knicks'), 'Knicks', 'bec0c2', '006bb6', 'f58426', '', '000000', 'FFFFFF')
insert into TeamColors values((select distinct team_id from team where name = 'Kings'), 'Kings', '000000', '5a2d81', '63727a', '', '000000', 'FFFFFF')
insert into TeamColors values((select distinct team_id from team where name = 'Jazz'), 'Jazz', '000000', '002b5c', 'f9a01b', '', '000000', 'FFFFFF')
insert into TeamColors values((select distinct team_id from team where name = 'Hornets'), 'Hornets', '000000', '00788c', '1d1160', '', '000000', 'FFFFFF')
insert into TeamColors values((select distinct team_id from team where name = 'Heat'), 'Heat', '000000', '98002e', '', '', '000000', 'FFFFFF')
insert into TeamColors values((select distinct team_id from team where name = 'Hawks'), 'Hawks', '26282a', 'e03a3e', '', '', '000000', 'FFFFFF')
insert into TeamColors values((select distinct team_id from team where name = 'Grizzlies'), 'Grizzlies', '707271', '12173f', '5d76a9', '', '000000', 'FFFFFF')
insert into TeamColors values((select distinct team_id from team where name = 'Clippers'), 'Clippers', 'bec0c2', '1d428a', '', '', '000000', 'FFFFFF')
insert into TeamColors values((select distinct team_id from team where name = 'Celtics'), 'Celtics', '000000', '007a33', 'ba9653', '', '000000', 'FFFFFF')
insert into TeamColors values((select distinct team_id from team where name = 'Cavaliers'), 'Cavaliers', '000000', '041e42', 'fdbb30', '860038', '000000', 'FFFFFF')
insert into TeamColors values((select distinct team_id from team where name = 'Bulls'), 'Bulls', '000000', 'ce1141', '', '', '000000', 'FFFFFF')
insert into TeamColors values((select distinct team_id from team where name = 'Bucks'), 'Bucks', 'eee1c6', '0077c0', '00471b', '', '000000', 'FFFFFF')
insert into TeamColors values((select distinct team_id from team where name = '76ers'), '76ers', 'c4ced4', '006bb6', '002b5c', '', '000000', 'FFFFFF')
