CREATE DATABASE IF NOT EXISTS `renew` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `renew`;
SET global optimizer_switch='derived_merge=off';

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `customers`
--

DROP TABLE IF EXISTS `customers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `customers` (
  `id`           int                                                     NOT NULL AUTO_INCREMENT,
  `type`         int                                                     NOT NULL DEFAULT '0',
  `active`       int                                                     NOT NULL DEFAULT '0',
  `managementid` int                                                     NOT NULL DEFAULT '0', -- Weak FK since there is no need for cascades
  `technicianid` int                                                     NOT NULL DEFAULT '0', -- Weak FK since technician is only needed for certain 'type' fields
  `name`         varchar(45)  CHARACTER SET utf8 COLLATE utf8_swedish_ci NOT NULL DEFAULT '',
  `phone`        varchar(20)                                             NOT NULL DEFAULT '',
  `email`        varchar(60)                                             NOT NULL DEFAULT '',
  `address`      varchar(45)  CHARACTER SET utf8 COLLATE utf8_swedish_ci NOT NULL DEFAULT '',
  `zip`          varchar(10)                                             NOT NULL DEFAULT '',
  `city`         varchar(45)  CHARACTER SET utf8 COLLATE utf8_swedish_ci NOT NULL DEFAULT '',
  `orgnumber`    varchar(14)                                             NOT NULL DEFAULT '',
  `vismaref`     varchar(12)                                             NOT NULL DEFAULT '',
  `contact`      varchar(45)  CHARACTER SET utf8 COLLATE utf8_swedish_ci NOT NULL DEFAULT '',
  `note`         varchar(200) CHARACTER SET utf8 COLLATE utf8_swedish_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `customers_type_INDEX` (`type`),
  KEY `customers_active_INDEX` (`active`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `accounts`
--

DROP TABLE IF EXISTS `accounts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `accounts` (
  `id`              int                                                    NOT NULL AUTO_INCREMENT,
  `authz`           int                                                    NOT NULL DEFAULT '0',
  `active`          int                                                    NOT NULL DEFAULT '0',
  `lastlogin`       bigint                                                 NOT NULL DEFAULT '0',
  `email`           varchar(60)                                            NOT NULL DEFAULT '',
  `password`        varchar(45)                                            NOT NULL DEFAULT '',
  `firstname`       varchar(45) CHARACTER SET utf8 COLLATE utf8_swedish_ci NOT NULL DEFAULT '',
  `lastname`        varchar(45) CHARACTER SET utf8 COLLATE utf8_swedish_ci NOT NULL DEFAULT '',
  `phone`           varchar(20)                                            NOT NULL DEFAULT '',
  `address`         varchar(45) CHARACTER SET utf8 COLLATE utf8_swedish_ci NOT NULL DEFAULT '',
  `floor`           varchar(10) CHARACTER SET utf8 COLLATE utf8_swedish_ci NOT NULL DEFAULT '',
  `apartment`       varchar(10) CHARACTER SET utf8 COLLATE utf8_swedish_ci NOT NULL DEFAULT '',
  `token`           varchar(45)                                            NOT NULL DEFAULT '',
  `tokenvaliduntil` bigint                                                 NOT NULL DEFAULT '0',
  `PIN`             varchar(10)                                            NOT NULL DEFAULT '',
  `PINvaliduntil`   bigint                                                 NOT NULL DEFAULT '0',
  `failedattempts`  int                                                    NOT NULL DEFAULT '0',
  `lockeduntil`     bigint                                                 NOT NULL DEFAULT '0',
  `customerid`      int                                                    NOT NULL DEFAULT '0', -- Weak FK since employees don't belong to customers
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `accounts_authz_INDEX` (`authz`),
  KEY `accounts_active_INDEX` (`active`),
  KEY `accounts_customerid_INDEX` (`customerid`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Create admin account
--

SET @NOW = unix_timestamp();
LOCK TABLES `accounts` WRITE;
/*!40000 ALTER TABLE `accounts` DISABLE KEYS */;
INSERT INTO `accounts` SET authz=128, active=1, lastlogin=@NOW, email='admin', password='2f61b14077ba93d58396a4a6f8281505', firstname='Admin', lastname='Admin';
/*!40000 ALTER TABLE `accounts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `timetypes`
--

DROP TABLE IF EXISTS `timetypes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `timetypes` (
  `id`          int                                                    NOT NULL AUTO_INCREMENT,
  `code`        varchar(20)                                            NOT NULL DEFAULT '',
  `name`        varchar(40) CHARACTER SET utf8 COLLATE utf8_swedish_ci NOT NULL DEFAULT '',
  `unit`        varchar(10) CHARACTER SET utf8 COLLATE utf8_swedish_ci NOT NULL DEFAULT '',
  `description` varchar(40) CHARACTER SET utf8 COLLATE utf8_swedish_ci NOT NULL DEFAULT '',
  `active`      int                                                    NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `news`
--

DROP TABLE IF EXISTS `news`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `news` (
  `id`         int                                                     NOT NULL AUTO_INCREMENT,
  `customerid` int                                                     NOT NULL DEFAULT '0', -- Weak FK since some news belong to all customers
  `category`   int                                                     NOT NULL DEFAULT '0',
  `headline`   varchar(45)  CHARACTER SET utf8 COLLATE utf8_swedish_ci NOT NULL DEFAULT '',
  `message`    varchar(500) CHARACTER SET utf8 COLLATE utf8_swedish_ci NOT NULL DEFAULT '',
  `author`     varchar(100) CHARACTER SET utf8 COLLATE utf8_swedish_ci NOT NULL DEFAULT '',
  `sent`       bigint                                                  NOT NULL DEFAULT '0',
  `showfrom`   bigint                                                  NOT NULL DEFAULT '0',
  `showuntil`  bigint                                                  NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `news_customerid_INDEX` (`customerid`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `newsreaders`
--

DROP TABLE IF EXISTS `newsreaders`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `newsreaders` (
  `id`         bigint NOT NULL AUTO_INCREMENT,
  `newsid`     int    NOT NULL DEFAULT '0',
  `residentid` int    NOT NULL DEFAULT '0',
  `date`       bigint NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `newsreaders_newsid_INDEX` (`newsid`),
  KEY `newsreaders_residentid_INDEX` (`residentid`),
  CONSTRAINT `newsreaders_newsid`     FOREIGN KEY (`newsid`)     REFERENCES `news`     (`id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `newsreaders_residentid` FOREIGN KEY (`residentid`) REFERENCES `accounts` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `offers`
--

DROP TABLE IF EXISTS `offers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `offers` (
  `id`         int                                                     NOT NULL AUTO_INCREMENT,
  `customerid` int                                                     NOT NULL DEFAULT '0', -- Weak FK since some offers belong to all customers
  `category`   int                                                     NOT NULL DEFAULT '0',
  `headline`   varchar(45)  CHARACTER SET utf8 COLLATE utf8_swedish_ci NOT NULL DEFAULT '',
  `message`    varchar(500) CHARACTER SET utf8 COLLATE utf8_swedish_ci NOT NULL DEFAULT '',
  `author`     varchar(100) CHARACTER SET utf8 COLLATE utf8_swedish_ci NOT NULL DEFAULT '',
  `sent`       bigint                                                  NOT NULL DEFAULT '0',
  `showfrom`   bigint                                                  NOT NULL DEFAULT '0',
  `showuntil`  bigint                                                  NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `offers_customerid_INDEX` (`customerid`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `offerreaders`
--

DROP TABLE IF EXISTS `offerreaders`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `offerreaders` (
  `id`         bigint NOT NULL AUTO_INCREMENT,
  `offerid`    int    NOT NULL DEFAULT '0',
  `residentid` int    NOT NULL DEFAULT '0',
  `date`       bigint NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `offerreaders_offerid_INDEX` (`offerid`),
  KEY `offerreaders_residentid_INDEX` (`residentid`),
  CONSTRAINT `offerreaders_offerid`    FOREIGN KEY (`offerid`)    REFERENCES `offers`   (`id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `offerreaders_residentid` FOREIGN KEY (`residentid`) REFERENCES `accounts` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `issueclasses`
--

DROP TABLE IF EXISTS `issueclasses`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `issueclasses` (
  `id`   int                                                    NOT NULL AUTO_INCREMENT,
  `name` varchar(45) CHARACTER SET utf8 COLLATE utf8_swedish_ci NOT NULL DEFAULT '',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Create 'Övrigt' option
--

LOCK TABLES `issueclasses` WRITE;
/*!40000 ALTER TABLE `issueclasses` DISABLE KEYS */;
INSERT INTO `issueclasses` SET name='Övrigt';
/*!40000 ALTER TABLE `issueclasses` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `issues`
--

DROP TABLE IF EXISTS `issues`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `issues` (
  `id`           int                                                     NOT NULL AUTO_INCREMENT,
  `customerid`   int                                                     NOT NULL DEFAULT '0',
  `residentid`   int                                                     NOT NULL DEFAULT '0',
  `assignedid`   int                                                     NOT NULL DEFAULT '0',
  `issueclassid` int                                                     NOT NULL DEFAULT '0',
  `areatype`     int                                                     NOT NULL DEFAULT '0',
  `status`       int                                                     NOT NULL DEFAULT '0',
  `prio`         int                                                     NOT NULL DEFAULT '0',
  `responsible`  int                                                     NOT NULL DEFAULT '0',
  `startdate`    bigint                                                  NOT NULL DEFAULT '0',
  `enddate`      bigint                                                  NOT NULL DEFAULT '0',
  `name`         varchar(45)  CHARACTER SET utf8 COLLATE utf8_swedish_ci NOT NULL DEFAULT '',
  `description`  varchar(400) CHARACTER SET utf8 COLLATE utf8_swedish_ci NOT NULL DEFAULT '',
  `vismaref`     varchar(12)                                             NOT NULL DEFAULT '',
  `latitude`     double                                                  NOT NULL DEFAULT '0',
  `longitude`    double                                                  NOT NULL DEFAULT '0',
  `firstname`    varchar(45)  CHARACTER SET utf8 COLLATE utf8_swedish_ci NOT NULL DEFAULT '',
  `lastname`     varchar(45)  CHARACTER SET utf8 COLLATE utf8_swedish_ci NOT NULL DEFAULT '',
  `phone`        varchar(20)                                             NOT NULL DEFAULT '',
  `address`      varchar(45)  CHARACTER SET utf8 COLLATE utf8_swedish_ci NOT NULL DEFAULT '',
  `floor`        varchar(10)  CHARACTER SET utf8 COLLATE utf8_swedish_ci NOT NULL DEFAULT '',
  `apartment`    varchar(10)  CHARACTER SET utf8 COLLATE utf8_swedish_ci NOT NULL DEFAULT '',
  `email`        varchar(60)                                             NOT NULL DEFAULT '',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `issues_customerid_INDEX` (`customerid`),
  KEY `issues_residentid_INDEX` (`residentid`),
  KEY `issues_assignedid_INDEX` (`assignedid`),
  KEY `issues_issueclassid_INDEX` (`issueclassid`),
  CONSTRAINT `issues_customerid` FOREIGN KEY (`customerid`) REFERENCES `customers` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `issues_residentid` FOREIGN KEY (`residentid`) REFERENCES `accounts` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `issues_assignedid` FOREIGN KEY (`assignedid`) REFERENCES `accounts` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `issues_issueclassid` FOREIGN KEY (`issueclassid`) REFERENCES `issueclasses` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=10000 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `issuereaders`
--

DROP TABLE IF EXISTS `issuereaders`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `issuereaders` (
  `id`        bigint NOT NULL AUTO_INCREMENT,
  `issueid`   int    NOT NULL DEFAULT '0',
  `accountid` int    NOT NULL DEFAULT '0',
  `date`      bigint NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `issuereaders_issueid_INDEX`   (`issueid`),
  KEY `issuereaders_accountid_INDEX` (`accountid`),
  CONSTRAINT `issuereaders_issueid`   FOREIGN KEY (`issueid`)   REFERENCES `issues`   (`id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `issuereaders_accountid` FOREIGN KEY (`accountid`) REFERENCES `accounts` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `issuefeedbacks`
--

DROP TABLE IF EXISTS `issuefeedbacks`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `issuefeedbacks` (
  `id`           int                                                     NOT NULL AUTO_INCREMENT,
  `issueid`      int                                                     NOT NULL DEFAULT '0',
  `description`  varchar(200) CHARACTER SET utf8 COLLATE utf8_swedish_ci NOT NULL DEFAULT '',
  `createdby`    int                                                     NOT NULL DEFAULT '0',
  `createdname`  varchar(90)  CHARACTER SET utf8 COLLATE utf8_swedish_ci NOT NULL DEFAULT '',
  `createddate`  bigint                                                  NOT NULL DEFAULT '0',
  `createdauthz` int                                                     NOT NULL DEFAULT '0',	 
  `accesstype`   int                                                     NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `issuefeedbacks_issueid_INDEX` (`issueid`),
  KEY `issuefeedbacks_createdby_INDEX` (`createdby`),
  CONSTRAINT `issuefeedbacks_issueid` FOREIGN KEY (`issueid`) REFERENCES `issues` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `issuefeedbacks_createdby` FOREIGN KEY (`createdby`) REFERENCES `accounts` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `issuematerials`
--

DROP TABLE IF EXISTS `issuematerials`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `issuematerials` (
  `id`          int                                                     NOT NULL AUTO_INCREMENT,
  `issueid`     int                                                     NOT NULL DEFAULT '0',
  `description` varchar(200) CHARACTER SET utf8 COLLATE utf8_swedish_ci NOT NULL DEFAULT '',
  `amount`      double                                                  NOT NULL DEFAULT '0',
  `createdby`   int                                                     NOT NULL DEFAULT '0',
  `createddate` bigint                                                  NOT NULL DEFAULT '0',
  `price`       decimal(12,2)                                           NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `issuematerials_issueid_INDEX` (`issueid`),
  KEY `issuematerials_createdby_INDEX` (`createdby`),
  CONSTRAINT `issuematerials_issueid` FOREIGN KEY (`issueid`) REFERENCES `issues` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `issuematerials_createdby` FOREIGN KEY (`createdby`) REFERENCES `accounts` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `issuephotos`
--

DROP TABLE IF EXISTS `issuephotos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `issuephotos` (
  `id`          int                                                    NOT NULL AUTO_INCREMENT,
  `issueid`     int                                                    NOT NULL DEFAULT '0',
  `caption`     varchar(45) CHARACTER SET utf8 COLLATE utf8_swedish_ci NOT NULL DEFAULT '',
  `rotation`    int                                                    NOT NULL DEFAULT '0',
  `platform`    int                                                    NOT NULL DEFAULT '0',
  `osversion`   varchar(45)                                            NOT NULL DEFAULT '',
  `image`       mediumblob                                             NOT NULL,
  `createdby`   int                                                    NOT NULL DEFAULT '0',
  `createddate` bigint                                                 NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `issuephotos_issueid_INDEX` (`issueid`),
  KEY `issuephotos_createdby_INDEX` (`createdby`),
  CONSTRAINT `issuephotos_issueid` FOREIGN KEY (`issueid`) REFERENCES `issues` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `issuephotos_createdby` FOREIGN KEY (`createdby`) REFERENCES `accounts` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `issuetimes`
--

DROP TABLE IF EXISTS `issuetimes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `issuetimes` (
  `id`          int    NOT NULL AUTO_INCREMENT,
  `issueid`     int    NOT NULL DEFAULT '0',
  `timetypeid`  int    NOT NULL DEFAULT '0',
  `starttime`   bigint NOT NULL DEFAULT '0',
  `endtime`     bigint NOT NULL DEFAULT '0',
  `createdby`   int    NOT NULL DEFAULT '0',
  `createddate` bigint NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `issuetimes_issueid_INDEX` (`issueid`),
  KEY `issuetimes_timetypeid_INDEX` (`timetypeid`),
  KEY `issuetimes_createdby_INDEX` (`createdby`),
  CONSTRAINT `issuetimes_issueid` FOREIGN KEY (`issueid`) REFERENCES `issues` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `issuetimes_timetypeid` FOREIGN KEY (`timetypeid`) REFERENCES `timetypes` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `issuetimes_createdby` FOREIGN KEY (`createdby`) REFERENCES `accounts` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `issuetransitions`
--

DROP TABLE IF EXISTS `issuetransitions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `issuetransitions` (
  `id`          int                                                    NOT NULL AUTO_INCREMENT,
  `issueid`     int                                                    NOT NULL DEFAULT '0',
  `fromstatus`  int                                                    NOT NULL DEFAULT '0',
  `tostatus`    int                                                    NOT NULL DEFAULT '0',
  `createdby`   int                                                    NOT NULL DEFAULT '0',
  `createdname` varchar(90) CHARACTER SET utf8 COLLATE utf8_swedish_ci NOT NULL DEFAULT '',
  `createddate` bigint                                                 NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `issuetransitions_issueid_INDEX` (`issueid`),
  KEY `issuetransitions_createdby_INDEX` (`createdby`),
  CONSTRAINT `issuetransitions_issueid` FOREIGN KEY (`issueid`) REFERENCES `issues` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  CONSTRAINT `issuetransitions_createdby` FOREIGN KEY (`createdby`) REFERENCES `accounts` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
