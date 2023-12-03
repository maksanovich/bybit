/*
SQLyog Ultimate v13.1.1 (64 bit)
MySQL - 10.4.25-MariaDB : Database - db_trade
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`db_trade` /*!40100 DEFAULT CHARACTER SET latin1 */;

USE `db_trade`;

/*Table structure for table `bybit_datas` */

DROP TABLE IF EXISTS `bybit_datas`;

CREATE TABLE `bybit_datas` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `coin_name` varchar(255) DEFAULT NULL,
  `entry_price` varchar(255) DEFAULT NULL,
  `type_order` varchar(255) DEFAULT NULL,
  `vol_nums` varchar(255) DEFAULT NULL,
  `slice_size` varchar(255) DEFAULT NULL,
  `stopless` varchar(255) DEFAULT NULL,
  `tp1` varchar(255) DEFAULT NULL,
  `tp2` varchar(255) DEFAULT NULL,
  `tp3` varchar(255) DEFAULT NULL,
  `tp4` varchar(255) DEFAULT NULL,
  `tp5` varchar(255) DEFAULT NULL,
  `tp6` varchar(255) DEFAULT NULL,
  `qty` varchar(255) DEFAULT NULL,
  `type` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

/*Data for the table `bybit_datas` */

insert  into `bybit_datas`(`id`,`coin_name`,`entry_price`,`type_order`,`vol_nums`,`slice_size`,`stopless`,`tp1`,`tp2`,`tp3`,`tp4`,`tp5`,`tp6`,`qty`,`type`) values 
(1,'ETHUSDT','2063.3','Limit','1','1','2006.25','2151.4','2200','2300','2400','2500','2600','1','Buy');

/*Table structure for table `bybit_users` */

DROP TABLE IF EXISTS `bybit_users`;

CREATE TABLE `bybit_users` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `email` varchar(255) DEFAULT NULL,
  `api_key` varchar(255) DEFAULT NULL,
  `api_secret` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;

/*Data for the table `bybit_users` */

insert  into `bybit_users`(`id`,`email`,`api_key`,`api_secret`) values 
(1,'devmaster926@gmail.com','bPh4Xx9MLmlid9YPYP','qLeNCeT8FE5GPgjZku6nNrHOISMM4X8Mgqns');

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
