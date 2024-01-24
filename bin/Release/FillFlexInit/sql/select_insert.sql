/* !!!!!!!! install MySQL WorkBench and MySQL connector from install dir set login & password !!!!!!! */
/* !!!!!!!! create database and table from script above !!!!!!! */
/* !!!!!!!! create odbc source named fillflex & check connection !!!!!!! */
/* !!!!!!!! install spire.pdf-fe_4.3 for reports creation !!!!!!! */
/* !!!!!!!! create dir & copy instance to C:\FillFlex for init & saving reports !!!!!!! */
/* !!!!!!!! create dir C:\EasyModbus\EasyModbus.dll Modbus library including !!!!!!! */

create schema if not exists `fillflex` default character set utf8 collate utf8_bin;

create table if not exists `datatable` (
  `id` bigint(20) unsigned not null auto_increment,
  `plcid` varchar(20) default null,
  `datecreate` datetime default null,
  `datestart` datetime default null,
  `datefin` datetime default null,
  `setp` decimal(8,0) default null,
  `mass` decimal(8,3) default null,
  `vol` decimal(8,3) default null,
  `dens` decimal(7,4) default null,
  `temp` decimal(5,2) default null,
  `cntst` decimal(15,3) default null,
  `cntfin` decimal(15,3) default null,
  `status` varchar(60) default null,
  `tankId` varchar(45) default null,
  `transp` varchar(190) default null,
  `driver` varchar(190) default null,
  `customer` varchar(190) default null,
  `sourcetank` int not null,
  primary key (`id`),
  unique key `id_UNIQUE` (`id`)
) engine=InnoDB auto_increment=1 default charset=utf8;


SELECT * FROM fillflex.datatable;
insert into `fillflex`.`datatable` (datecreate,datestart,datefin,setp,vol,mass,dens,temp,cntst,cntfin,plcid,status,tankId,transp,driver,customer,sourcetank) values (now(),now(),now(),395,481,394.42,0.820,21.6,1555,1949,"20190706091325","виконано","123456789","AA1124BX","Петренко М. П.","ЗАТ КиївБУД",5);

id, plcid, datecreate, datestart, datefin, setp, mass, vol, dens, temp, cntst, cntfin, status

select id as ID, datecreate as Створено, datefin as Завершено, setp as Завдання, mass as Маса, vol as Об´єм, dens as Густина, temp as Температура, cntst as Початкове, cntfin as Кінцеве, status as Статус from datatable order by id desc limit 5;


