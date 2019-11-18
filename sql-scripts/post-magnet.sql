-- MySQL dump 10.13  Distrib 5.7.12, for Win64 (x86_64)
--
-- Host: localhost    Database: post_magnet
-- ------------------------------------------------------
-- Server version	5.7.9-log

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
-- Table structure for table `pm_employees`
--

DROP TABLE IF EXISTS `pm_employees`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pm_employees` (
  `employee_id` int(11) NOT NULL AUTO_INCREMENT,
  `employee_username` varchar(15) NOT NULL,
  `employee_password` varchar(255) NOT NULL,
  `employee_email` varchar(500) DEFAULT NULL,
  `employee_phone` varchar(13) DEFAULT NULL,
  `employee_name` varchar(100) NOT NULL,
  `employee_is_active` tinyint(1) NOT NULL,
  `employee_role_id` int(11) NOT NULL,
  `employee_rate` int(11) NOT NULL,
  `employee_creator_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`employee_id`),
  KEY `FK_Employees_Roles_idx` (`employee_role_id`),
  KEY `FK_Employees_Creator_idx` (`employee_creator_id`),
  CONSTRAINT `FK_Employees_Creator` FOREIGN KEY (`employee_creator_id`) REFERENCES `pm_employees` (`employee_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Employees_Roles` FOREIGN KEY (`employee_role_id`) REFERENCES `pm_roles` (`role_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8 COMMENT='Post Magnet Employees Table';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pm_employees`
--

LOCK TABLES `pm_employees` WRITE;
/*!40000 ALTER TABLE `pm_employees` DISABLE KEYS */;
INSERT INTO `pm_employees` VALUES (5,'admin','1000:c+YCGtW8SkJsEnxm/Xa1tVEuY+TngSpl:v1irIZV4gkLsn7NCYnrirLFR8+UVlu4j','thanhledev@gmail.com','09062003021','Thanh Le',1,1,0,NULL),(6,'thanhnguyen','1000:rl+L8oRM8aW5QSTiU/C0I8zRcG4+/lsa:oFw6aDvD98+AFfzhDTxavsZFvdZMxY0d','thanhnguyen@gmail.com','0909998881','Thanh Nguyen',1,2,0,5),(7,'philuong','1000:oSoZaHtXhLCM2Qnzq4GqE8tRJsZHh6iN:rAUJ0DgtjhJvbxT7wsEQMMIFJQAi9Zfd','philuong@gmail.com','0911112345','Phi Luong',1,3,20000,5),(8,'annatran','1000:26sLqDNSpiF+MKCwSvwAY6mP454UH0Oe:lm9IFCV8zLam/w1BmYXAEGUsCQ5zYZ5H','annatran@gmail.com','0911112345','Anna Tran',1,3,20000,6),(9,'dungtran','1000:rFf5hqbvMallJdyZ5gIRvKWa1ymFSTRe:Ba5qe//PrN8lLcHH3sUStm5Qvo9duWA+','dungtran@gmail.com','09071231245','Dung Tran',1,3,25000,5),(10,'ductran','1000:x0Tomf4VMmzMzOEbvuuLPvB7E72EiHpM:3kAMjQe47ZOPZiMubcjhmxb8Mow4voWH','ductran@gmail.com','09063452346','Duc Tran',1,2,0,5),(11,'vietnguyen','1000:1zRO7y2+voRyNpk6OuTzhjv0scIHvTt3:f2I8HPpD4XZjS17cOT8yVvTUymbIS6tr','vietnguyen@gmail.com','09762352357','Viet Nguyen',1,2,0,5);
/*!40000 ALTER TABLE `pm_employees` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pm_invoices`
--

DROP TABLE IF EXISTS `pm_invoices`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pm_invoices` (
  `invoice_id` int(11) NOT NULL AUTO_INCREMENT,
  `invoice_created` datetime NOT NULL,
  `invoice_status` int(11) NOT NULL,
  `invoice_total_amount` int(11) NOT NULL,
  `invoice_note` varchar(5000) DEFAULT NULL,
  `invoice_file` varchar(5000) NOT NULL,
  `employee_id` int(11) NOT NULL,
  PRIMARY KEY (`invoice_id`),
  KEY `FK_Invoices_Employees_idx` (`employee_id`),
  CONSTRAINT `FK_Invoices_Employees` FOREIGN KEY (`employee_id`) REFERENCES `pm_employees` (`employee_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='Post Magnet Invoices Table';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pm_invoices`
--

LOCK TABLES `pm_invoices` WRITE;
/*!40000 ALTER TABLE `pm_invoices` DISABLE KEYS */;
/*!40000 ALTER TABLE `pm_invoices` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pm_logs`
--

DROP TABLE IF EXISTS `pm_logs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pm_logs` (
  `log_id` int(11) NOT NULL AUTO_INCREMENT,
  `log_created` datetime NOT NULL,
  `log_content` varchar(5000) DEFAULT NULL,
  PRIMARY KEY (`log_id`)
) ENGINE=InnoDB AUTO_INCREMENT=64 DEFAULT CHARSET=utf8 COMMENT='Post Magnet Logs Table';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pm_logs`
--

LOCK TABLES `pm_logs` WRITE;
/*!40000 ALTER TABLE `pm_logs` DISABLE KEYS */;
INSERT INTO `pm_logs` VALUES (1,'2016-10-22 20:18:11','admin has logged successfully from ::1'),(2,'2016-10-22 20:18:57','admin has logged out successfully from ::1'),(3,'2016-10-22 20:25:22','admin has logged successfully from ::1'),(4,'2016-10-22 20:26:28','admin has logged out successfully from ::1'),(5,'2016-10-22 21:11:43','admin has logged successfully from ::1'),(6,'2016-10-22 21:15:54','admin has created a new employee with value [ Username: dungtran, Name: Dung Tran, Email: dungtran@gmail.com, Phone: 09071231245, Active: Yes, Role: Contributor ]'),(7,'2016-10-22 21:18:46','admin has logged successfully from ::1'),(8,'2016-10-22 21:56:44','admin has logged out successfully from ::1'),(9,'2016-10-22 21:59:30','admin has logged successfully from ::1'),(10,'2016-10-22 22:00:46','admin has created a new employee with value [ Username: ductran, Name: Duc Tran, Email: ductran@gmail.com, Phone: 09063452346, Active: Yes, Role: Editor ]'),(11,'2016-10-22 22:07:51','admin has created a new employee with value [ Username: vietnguyen, Name: Viet Nguyen, Email: vietnguyen@gmail.com, Phone: 09762352357, Active: Yes, Role: Editor ]'),(12,'2016-10-22 22:19:56','admin has logged out successfully from ::1'),(13,'2016-10-22 22:29:56','admin has logged successfully from ::1'),(14,'2016-10-22 22:31:21','admin has logged out successfully from ::1'),(15,'2016-10-22 23:42:14','admin has logged successfully from ::1'),(16,'2016-10-22 23:44:10','admin has logged out successfully from ::1'),(17,'2016-10-22 23:48:12','admin has logged successfully from ::1'),(18,'2016-10-22 23:49:07','admin has logged out successfully from ::1'),(19,'2016-10-22 23:50:47','admin has logged successfully from ::1'),(20,'2016-10-22 23:53:57','admin has logged out successfully from ::1'),(21,'2016-10-23 01:17:49','admin has logged successfully from ::1'),(22,'2016-10-23 01:20:26','admin has logged out successfully from ::1'),(23,'2016-10-23 01:29:31','admin has logged successfully from ::1'),(24,'2016-10-23 01:30:05','admin has updated his/her password'),(25,'2016-10-23 01:30:15','admin has logged out successfully from ::1'),(26,'2016-10-23 01:30:20','admin failed when trying to login from ::1 at 1 times'),(27,'2016-10-23 01:30:25','admin has logged successfully from ::1'),(28,'2016-10-23 01:30:28','admin has logged out successfully from ::1'),(29,'2016-10-23 23:29:37','admin has logged successfully from ::1'),(30,'2016-10-23 23:32:57','admin has created a new website with value [ Host: http://dudoanbong.net, Username: owneradm,'),(31,'2016-10-23 23:34:11','admin has logged out successfully from ::1'),(32,'2016-10-24 00:23:24','admin has logged successfully from ::1'),(33,'2016-10-24 00:26:03','admin has logged out successfully from ::1'),(34,'2016-10-24 00:27:28','admin has logged successfully from ::1'),(35,'2016-10-24 00:29:43','admin has logged out successfully from ::1'),(36,'2016-10-24 00:38:43','admin has logged successfully from ::1'),(37,'2016-10-24 14:12:48','admin has logged successfully from ::1'),(38,'2016-10-24 14:13:05','admin has logged out successfully from ::1'),(39,'2016-10-24 14:13:13','admin has logged successfully from ::1'),(40,'2016-10-24 14:17:48','admin has logged out successfully from ::1'),(41,'2016-10-24 19:15:52','admin has logged successfully from ::1'),(42,'2016-10-24 19:21:53','admin has logged out successfully from ::1'),(43,'2016-10-31 16:56:48','admin has logged successfully from ::1'),(44,'2016-10-31 16:59:33','admin has logged out successfully from ::1'),(45,'2016-10-31 17:16:52','admin has logged successfully from ::1'),(46,'2016-10-31 17:40:22','admin has logged out successfully from ::1'),(47,'2016-10-31 19:01:13','admin has logged successfully from ::1'),(48,'2016-10-31 19:16:34','admin has logged out successfully from ::1'),(49,'2016-10-31 19:21:03','admin has logged successfully from ::1'),(50,'2016-10-31 19:35:21','admin has logged out successfully from ::1'),(51,'2016-10-31 19:38:16','admin failed when trying to login from ::1 at 1 times'),(52,'2016-10-31 19:38:20','admin has logged successfully from ::1'),(53,'2016-10-31 20:41:21','admin has logged out successfully from ::1'),(54,'2016-11-09 15:11:48','admin has logged successfully from ::1'),(55,'2016-11-09 15:52:11','admin has logged successfully from ::1'),(56,'2016-11-09 16:00:50','admin has logged out successfully from ::1'),(57,'2016-11-11 11:32:58','admin has logged successfully from ::1'),(58,'2016-11-11 11:34:34','admin has created a new website with value [ Host: http://bong99.asia, Username: owneradm,'),(59,'2016-11-11 14:56:24','admin has logged out successfully from ::1'),(60,'2016-11-12 00:32:20','admin has logged successfully from ::1'),(61,'2016-11-12 00:33:08','admin has update website with a new value [ Host: http://dudoanbong.net, Username: owneradm,'),(62,'2016-11-12 00:33:25','admin has update website with a new value [ Host: http://bong99.asia, Username: owneradm,'),(63,'2016-11-12 00:34:06','admin has logged out successfully from ::1');
/*!40000 ALTER TABLE `pm_logs` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pm_messages`
--

DROP TABLE IF EXISTS `pm_messages`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pm_messages` (
  `message_id` int(11) NOT NULL AUTO_INCREMENT,
  `message_code` varchar(40) NOT NULL,
  `message_sent` datetime NOT NULL,
  `message_content` varchar(5000) NOT NULL,
  `message_is_read` tinyint(1) NOT NULL,
  `message_author_id` int(11) NOT NULL,
  `message_recipient_id` int(11) NOT NULL,
  PRIMARY KEY (`message_id`),
  KEY `FK_Message_Employees_Author_idx` (`message_author_id`),
  KEY `FK_Messages_Employees_Recipient_idx` (`message_recipient_id`),
  CONSTRAINT `FK_Messages_Employees_Author` FOREIGN KEY (`message_author_id`) REFERENCES `pm_employees` (`employee_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Messages_Employees_Recipient` FOREIGN KEY (`message_recipient_id`) REFERENCES `pm_employees` (`employee_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8 COMMENT='Post Magnet Employee Messages';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pm_messages`
--

LOCK TABLES `pm_messages` WRITE;
/*!40000 ALTER TABLE `pm_messages` DISABLE KEYS */;
INSERT INTO `pm_messages` VALUES (1,'h2EUbuFopKQDp3Iemy4w5N0iymXS0IyTi49GxnP1','2016-10-31 01:48:37','Noi dung test',0,6,5),(2,'1gofeXSoS4mmoOI6JT71qNO1tuJxY5WDefx9TxFa','2016-10-31 02:48:37','Noi dung test',0,5,6),(3,'jY4HbzgzBZjdgQpILkqsRPndg9pE6RZijw7EMctn','2016-10-31 08:48:37','Noi dung test 1',1,6,5);
/*!40000 ALTER TABLE `pm_messages` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pm_notifications`
--

DROP TABLE IF EXISTS `pm_notifications`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pm_notifications` (
  `notification_id` int(11) NOT NULL AUTO_INCREMENT,
  `notification_created` datetime NOT NULL,
  `notification_type` int(11) NOT NULL,
  `notification_content` varchar(5000) NOT NULL,
  `notification_is_read` tinyint(1) NOT NULL,
  `notification_employee_id` int(11) NOT NULL,
  PRIMARY KEY (`notification_id`),
  KEY `FK_Notifications_Employees_idx` (`notification_employee_id`),
  CONSTRAINT `FK_Notifications_Employees` FOREIGN KEY (`notification_employee_id`) REFERENCES `pm_employees` (`employee_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8 COMMENT='Post Magnet Employee''s Notifications';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pm_notifications`
--

LOCK TABLES `pm_notifications` WRITE;
/*!40000 ALTER TABLE `pm_notifications` DISABLE KEYS */;
INSERT INTO `pm_notifications` VALUES (1,'2016-10-31 01:48:37',0,'New User Created',0,5),(2,'2016-10-31 02:48:37',1,'New Post Submitted',0,5),(3,'2016-10-31 04:15:37',2,'New Invoice Created',0,5);
/*!40000 ALTER TABLE `pm_notifications` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pm_permissions`
--

DROP TABLE IF EXISTS `pm_permissions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pm_permissions` (
  `prm_id` int(11) NOT NULL AUTO_INCREMENT,
  `prm_name` varchar(255) NOT NULL,
  `prm_controller_name` varchar(255) NOT NULL,
  `prm_action_name` varchar(255) NOT NULL,
  `prm_is_main_menu` tinyint(1) NOT NULL,
  PRIMARY KEY (`prm_id`)
) ENGINE=InnoDB AUTO_INCREMENT=38 DEFAULT CHARSET=utf8 COMMENT='Post Magnet Permissions Table';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pm_permissions`
--

LOCK TABLES `pm_permissions` WRITE;
/*!40000 ALTER TABLE `pm_permissions` DISABLE KEYS */;
INSERT INTO `pm_permissions` VALUES (1,'Dashboard','Home','Index',1),(2,'Members','Employee','Members',1),(3,'Profile','Employee','MyProfile',0),(4,'UpdateProfile','Employee','UpdateProfile',0),(5,'UpdatePassword','Employee','UpdatePassword',0),(6,'Create Employee','Employee','Create',0),(7,'View Profile','Employee','ViewProfile',0),(8,'Reset Password','Employee','ResetPassword',0),(9,'Update Accessibility','Employee','UpdateAccessibility',0),(10,'Update Rate','Employee','UpdateRate',0),(11,'Log','Log','Index',1),(12,'Websites List','Website','Websites',1),(13,'Create Website','Website','Create',0),(14,'Update Website','Website','Update',0),(15,'Remove Website','Website','Remove',0),(16,'Check Connectivity','Website','CheckConnectivity',0),(17,'3rd List','ThirdParty','List',1),(18,'Update','ThirdParty','Update',0),(19,'Posts List','Post','Posts',1),(20,'Create Post','Post','Create',1),(21,'Update Post Content','Post','UpdateContent',0),(22,'Remove Post','Post','Remove',0),(23,'Submit Post','Post','Submit',0),(24,'Approve Post','Post','Approve',0),(25,'Delivery Post','Post','Delivery',0),(26,'Schedule Post','Post','Schedule',0),(27,'Change Schedule Post','Post','ChangeSchedule',0),(29,'Administrator Post List','Post','AdministratorPosts',0),(30,'Editor Post List','Post','EditorPosts',0),(31,'Contributor Post List','Post','ContributorPosts',0),(32,'View Extra Payment List','Post','ViewExtraPayments',0),(33,'Add Extra Payment','Post','AddExtraPayment',0),(34,'Update Extra Payment','Post','UpdateExtraPayment',0),(35,'Remove Extra Payment','Post','RemoveExtraPayment',0),(36,'Get Available Websites','Website','AvailableWebsites',0),(37,'Get Available Category','Website','AvailableCategories',0);
/*!40000 ALTER TABLE `pm_permissions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pm_post_extra_payments`
--

DROP TABLE IF EXISTS `pm_post_extra_payments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pm_post_extra_payments` (
  `extra_payment_id` int(11) NOT NULL AUTO_INCREMENT,
  `extra_payment_amount` int(11) NOT NULL,
  `extra_payment_note` varchar(5000) DEFAULT NULL,
  `post_id` int(11) NOT NULL,
  PRIMARY KEY (`extra_payment_id`),
  KEY `FK_PostExtraPayments_Posts_idx` (`post_id`),
  CONSTRAINT `FK_PostExtraPayments_Posts` FOREIGN KEY (`post_id`) REFERENCES `pm_posts` (`post_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='Post Magent Post Extra Payments Table';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pm_post_extra_payments`
--

LOCK TABLES `pm_post_extra_payments` WRITE;
/*!40000 ALTER TABLE `pm_post_extra_payments` DISABLE KEYS */;
/*!40000 ALTER TABLE `pm_post_extra_payments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pm_posts`
--

DROP TABLE IF EXISTS `pm_posts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pm_posts` (
  `post_id` int(11) NOT NULL AUTO_INCREMENT,
  `post_code` varchar(40) NOT NULL,
  `post_title` varchar(5000) NOT NULL,
  `post_content` mediumtext NOT NULL,
  `post_keywords` varchar(1000) DEFAULT NULL,
  `post_meta_title` varchar(2000) NOT NULL,
  `post_meta_description` varchar(5000) NOT NULL,
  `post_tags` varchar(1000) DEFAULT NULL,
  `post_created` datetime NOT NULL,
  `post_submitted` datetime DEFAULT NULL,
  `post_approved` datetime DEFAULT NULL,
  `post_status` int(11) NOT NULL,
  `post_unique_percentage` int(11) DEFAULT NULL,
  `post_note` varchar(5000) DEFAULT NULL,
  `employee_id` int(11) NOT NULL,
  `invoice_id` int(11) DEFAULT NULL,
  `post_scheduled` datetime DEFAULT NULL,
  `post_scheduled_website` varchar(500) DEFAULT NULL,
  `post_scheduled_category` varchar(500) DEFAULT NULL,
  `post_posted` datetime DEFAULT NULL,
  `post_url` varchar(1000) DEFAULT NULL,
  PRIMARY KEY (`post_id`),
  KEY `FK_Posts_Employees_idx` (`employee_id`),
  KEY `FK_Posts_Invoices_idx` (`invoice_id`),
  CONSTRAINT `FK_Posts_Employees` FOREIGN KEY (`employee_id`) REFERENCES `pm_employees` (`employee_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Posts_Invoices` FOREIGN KEY (`invoice_id`) REFERENCES `pm_invoices` (`invoice_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8 COMMENT='Post Magent Posts Table';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pm_posts`
--

LOCK TABLES `pm_posts` WRITE;
/*!40000 ALTER TABLE `pm_posts` DISABLE KEYS */;
INSERT INTO `pm_posts` VALUES (1,'h2EUbuF0IyTi49GxopKQDp3Iemy4w5N0iymXSnP1','Noi dung thu nghiem','<p>Noi dung thu nghiem</p>','Noi dung','Noi dung thu nghiem','Mo ta Noi dung thu nghiem',NULL,'2016-10-31 01:48:37','0001-01-01 00:00:00','0001-01-01 00:00:00',0,0,NULL,7,NULL,'0001-01-01 00:00:00',NULL,NULL,'0001-01-01 00:00:00',NULL),(2,'49GxopKQDp3Iemy4h2EUbuFN0iymXSnP10IyTiw5','Noi dung thu nghiem 2','<p>Noi dung thu nghiem 2</p>','Noi dung 2','Noi dung thu nghiem 2','Mo ta Noi dung thu nghiem 2',NULL,'2016-10-31 12:48:37','0001-01-01 00:00:00','0001-01-01 00:00:00',0,0,NULL,8,NULL,'0001-01-01 00:00:00',NULL,NULL,'0001-01-01 00:00:00',NULL),(3,'7NytRR7NjW44dOMJ6fJ3iLJp3u12Rt2ghl3WfPlQ','Noi dung thu nghiem 3','<p>Noi dung thu nghiem 3</p>','Noi dung 3','Noi dung thu nghiem 3','Mo ta Noi dung thu nghiem 3',NULL,'2016-11-08 01:48:37','0001-01-01 00:00:00','0001-01-01 00:00:00',0,0,NULL,9,NULL,'0001-01-01 00:00:00',NULL,NULL,'0001-01-01 00:00:00',NULL),(4,'GcOAvyiz2UARFOuMKJ3wqowfZPAimVu0iiMrxBSY','Noi dung thu nghiem so 4','<p>Noi dung thu nghiem 4</p>','Noi dung 4','Noi dung thu nghiem 4','Mo ta Noi dung thu nghiem 4',NULL,'2016-11-08 11:48:37','2016-11-08 13:48:37','0001-01-01 00:00:00',1,67,NULL,7,NULL,'0001-01-01 00:00:00',NULL,NULL,'0001-01-01 00:00:00',NULL),(5,'Yp0HD8w54QeM6psUEE5iozd1csF2CQ7oEHQW2Zqs','Noi dung thu nghiem so 5','<p>Noi dung thu nghiem 5</p>','Noi dung 5','Noi dung thu nghiem 5','Mo ta Noi dung thu nghiem 5',NULL,'2016-11-09 11:48:37','2016-11-09 13:48:37','0001-01-01 00:00:00',1,86,NULL,8,NULL,'0001-01-01 00:00:00',NULL,NULL,'0001-01-01 00:00:00',NULL),(6,'JgBx6fGe50ZSuBXPxlwIMMWYhjnKxGqgfnvYMo9e','Noi dung thu nghiem so 6','<p>Noi dung thu nghiem 6</p>','Noi dung 6','Noi dung thu nghiem 6','Mo ta Noi dung thu nghiem 6',NULL,'2016-11-09 11:48:37','2016-11-09 14:48:37','0001-01-01 00:00:00',1,34,NULL,9,NULL,'0001-01-01 00:00:00',NULL,NULL,'0001-01-01 00:00:00',NULL);
/*!40000 ALTER TABLE `pm_posts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pm_roles`
--

DROP TABLE IF EXISTS `pm_roles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pm_roles` (
  `role_id` int(11) NOT NULL AUTO_INCREMENT,
  `role_name` varchar(255) NOT NULL,
  `role_description` varchar(1000) DEFAULT NULL,
  PRIMARY KEY (`role_id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8 COMMENT='Post Magnet Role Table';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pm_roles`
--

LOCK TABLES `pm_roles` WRITE;
/*!40000 ALTER TABLE `pm_roles` DISABLE KEYS */;
INSERT INTO `pm_roles` VALUES (1,'Administrator','Administrator Priviledges'),(2,'Editor','Editor Priviledges'),(3,'Contributor','Contributor Priviledges');
/*!40000 ALTER TABLE `pm_roles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pm_roles_permissions`
--

DROP TABLE IF EXISTS `pm_roles_permissions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pm_roles_permissions` (
  `role_id` int(11) NOT NULL,
  `prm_id` int(11) NOT NULL,
  KEY `FK_RolesPermissions_Roles_idx` (`role_id`),
  KEY `FK_RolesPermissions_Permissions_idx` (`prm_id`),
  CONSTRAINT `FK_RolesPermissions_Permissions` FOREIGN KEY (`prm_id`) REFERENCES `pm_permissions` (`prm_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_RolesPermissions_Roles` FOREIGN KEY (`role_id`) REFERENCES `pm_roles` (`role_id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='Post Magnet Roles & Permissions Relation Table';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pm_roles_permissions`
--

LOCK TABLES `pm_roles_permissions` WRITE;
/*!40000 ALTER TABLE `pm_roles_permissions` DISABLE KEYS */;
INSERT INTO `pm_roles_permissions` VALUES (1,1),(1,2),(1,3),(1,4),(1,5),(1,6),(1,7),(1,8),(1,9),(1,10),(2,1),(2,2),(2,3),(2,4),(2,5),(2,6),(2,7),(2,8),(2,9),(2,10),(3,1),(3,3),(3,4),(3,5),(1,11),(1,12),(1,13),(1,14),(1,15),(1,16),(1,17),(1,18),(1,19),(2,19),(3,19),(3,20),(1,21),(2,21),(3,21),(3,22),(3,23),(1,24),(2,24),(1,25),(2,25),(1,26),(2,26),(1,27),(2,27),(1,29),(2,30),(3,31),(1,32),(2,32),(3,32),(1,33),(2,33),(1,34),(2,34),(1,35),(2,35),(1,36),(2,36),(1,37),(2,37);
/*!40000 ALTER TABLE `pm_roles_permissions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pm_websites`
--

DROP TABLE IF EXISTS `pm_websites`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pm_websites` (
  `website_id` int(11) NOT NULL AUTO_INCREMENT,
  `website_host` varchar(500) NOT NULL,
  `website_username` varchar(100) NOT NULL,
  `website_password` varchar(100) NOT NULL,
  `website_timezone` varchar(1000) NOT NULL,
  `website_seo_plugin` int(11) NOT NULL,
  `website_tested` datetime DEFAULT NULL,
  `website_note` varchar(1000) DEFAULT NULL,
  PRIMARY KEY (`website_id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COMMENT='Post Magnet Websites Table';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pm_websites`
--

LOCK TABLES `pm_websites` WRITE;
/*!40000 ALTER TABLE `pm_websites` DISABLE KEYS */;
INSERT INTO `pm_websites` VALUES (1,'http://dudoanbong.net','owneradm','5@71i76#0wn4r','SE Asia Standard Time',1,'2016-11-11 12:29:51','Test successfully!'),(2,'http://bong99.asia','owneradm','5@71i76#0wn4r','SE Asia Standard Time',1,'2016-11-11 11:34:34','Test successfully!');
/*!40000 ALTER TABLE `pm_websites` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2016-11-12  0:36:16
