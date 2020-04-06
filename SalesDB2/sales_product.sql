-- MySQL dump 10.13  Distrib 8.0.13, for macos10.14 (x86_64)
--
-- Host: 127.0.0.1    Database: sales
-- ------------------------------------------------------
-- Server version	8.0.19

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
 SET NAMES utf8 ;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `product`
--

DROP TABLE IF EXISTS `product`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
 SET character_set_client = utf8mb4 ;
CREATE TABLE `product` (
  `productId` int NOT NULL AUTO_INCREMENT,
  `description` varchar(200) NOT NULL,
  `remarks` varchar(500) DEFAULT NULL,
  `imagePath` varchar(200) DEFAULT NULL,
  `price` decimal(10,0) NOT NULL,
  `isAvailable` tinyint NOT NULL,
  `publishOn` datetime NOT NULL,
  `fromWeb` tinyint NOT NULL,
  PRIMARY KEY (`productId`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product`
--

LOCK TABLES `product` WRITE;
/*!40000 ALTER TABLE `product` DISABLE KEYS */;
INSERT INTO `product` VALUES (1,'Mi primer producto','Mi producto estrella','lib/bootstrap/Content/Products/audifonos.jpeg',88776,1,'2020-04-01 00:00:00',1),(4,'Prueba','Imagen desde android',NULL,61945,1,'2020-04-02 17:04:48',1),(5,'Imagen ','Desde Android Edit','Products/86a74184-dc60-4009-8956-243eb5f72bdc.jpg',54848,0,'2020-04-02 22:25:27',0),(7,'Last try',NULL,'Products/68c3555d-19ab-4cf1-9546-c7b9927848f9.jpg',64928,0,'2020-04-03 20:18:48',0),(12,'try again',NULL,'lib/bootstrap/Content/Products/audifonos.jpeg',87896,1,'2020-04-02 00:00:00',1),(13,'Mi nuevo producto','Otro producto','lib/bootstrap/Content/Products/audifonos.jpeg',342,1,'2020-04-02 00:00:00',1),(14,'Pikachu','Mi pikachu editado','Products/90519116-f825-4e23-a9d3-0f8f47252fbe.jpg',6454,0,'2020-04-03 22:29:10',0),(15,'Peppa','Hola mundo editado','Products/d6d87c71-f1ab-4b7f-adfa-82bec364443f.jpg',54848,0,'2020-04-03 22:29:52',0),(16,'Mi nuevo producto',NULL,'Products/23f7109a-1574-495a-a34b-c511ab971756.jpg',65446,1,'2020-04-03 21:43:55',0),(17,'Charmander',NULL,'Products/c46b73e2-a29a-4932-932a-1739b200dda3.jpg',61540,0,'2020-04-03 22:40:20',0);
/*!40000 ALTER TABLE `product` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-04-06  8:36:18
