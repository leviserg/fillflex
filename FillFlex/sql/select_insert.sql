/* !!!!!!!! install MySQL WorkBench and MySQL connector from install dir set login & password !!!!!!! */
/* !!!!!!!! create database and table from script above !!!!!!! */
/* !!!!!!!! create odbc source named fillflex & check connection !!!!!!! */
/* !!!!!!!! install spire.pdf-fe_4.3 for reports creation !!!!!!! */
/* !!!!!!!! create dir & copy instance to C:\FillFlex for init & saving reports !!!!!!! */
/* !!!!!!!! create dir C:\EasyModbus\EasyModbus.dll Modbus library including !!!!!!! */

create schema if not exists `fillflex` default character set utf8 collate utf8_bin;

CREATE TABLE `datatable` (
  `id` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
  `plcid` varchar(20) DEFAULT NULL,
  `datecreate` datetime DEFAULT NULL,
  `datestart` datetime DEFAULT NULL,
  `datefin` datetime DEFAULT NULL,
  `setp` decimal(8,0) DEFAULT NULL,
  `mass` decimal(8,3) DEFAULT NULL,
  `vol` decimal(8,3) DEFAULT NULL,
  `dens` decimal(7,4) DEFAULT NULL,
  `temp` decimal(5,2) DEFAULT NULL,
  `cntst` decimal(15,3) DEFAULT NULL,
  `cntfin` decimal(15,3) DEFAULT NULL,
  `status` varchar(60) DEFAULT NULL,
  `tankId` varchar(45) DEFAULT NULL,
  `transp` varchar(190) DEFAULT NULL,
  `driver` varchar(190) DEFAULT NULL,
  `customer` varchar(190) DEFAULT NULL,
  `sourcetank` INT NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8


SELECT * FROM fillflex.datatable;
insert into `fillflex`.`datatable` (datecreate,datestart,datefin,setp,vol,mass,dens,temp,cntst,cntfin,plcid,status,tankId,transp,driver,customer,sourcetank) values (now(),now(),now(),395,481,394.42,0.820,21.6,1555,1949,"20190706091325","виконано","123456789","AA1124BX","Петренко М. П.","ЗАТ КиївБУД",5);

id, plcid, datecreate, datestart, datefin, setp, mass, vol, dens, temp, cntst, cntfin, status

select id as ID, datecreate as Створено, datefin as Завершено, setp as Завдання, mass as Маса, vol as Об´єм, dens as Густина, temp as Температура, cntst as Початкове, cntfin as Кінцеве, status as Статус from datatable order by id desc limit 5;