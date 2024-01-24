CREATE TABLE `fillflex`.`trendvalues` (
  `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `trdate` DATETIME NOT NULL,
  `par0` DECIMAL(4,1) NULL,
  `par1` DECIMAL(4,1) NULL,
  `par2` DECIMAL(4,1) NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC))
COMMENT = 'Parameter Values For Trend Building';