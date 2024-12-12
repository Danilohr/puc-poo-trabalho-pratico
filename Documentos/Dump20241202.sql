CREATE DATABASE  IF NOT EXISTS `sistema_aeroporto` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `sistema_aeroporto`;
-- MySQL dump 10.13  Distrib 8.0.40, for Win64 (x86_64)
--
-- Host: localhost    Database: sistema_aeroporto
-- ------------------------------------------------------
-- Server version	8.0.40

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `aeronave`
--

DROP TABLE IF EXISTS `aeronave`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aeronave` (
  `id_aeronave` int NOT NULL AUTO_INCREMENT,
  `capacidadePassageiros` int NOT NULL,
  `capacidadeBagagens` int NOT NULL,
  PRIMARY KEY (`id_aeronave`),
  UNIQUE KEY `capacidadePassageiros_UNIQUE` (`capacidadePassageiros`),
  UNIQUE KEY `capacidadeBagagens_UNIQUE` (`capacidadeBagagens`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aeronave`
--

LOCK TABLES `aeronave` WRITE;
/*!40000 ALTER TABLE `aeronave` DISABLE KEYS */;
INSERT INTO `aeronave` VALUES (1,200,500),(2,150,350),(3,250,600);
/*!40000 ALTER TABLE `aeronave` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aeroporto`
--

DROP TABLE IF EXISTS `aeroporto`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aeroporto` (
  `id_aeroporto` int NOT NULL AUTO_INCREMENT,
  `sigla` varchar(10) DEFAULT NULL,
  `nome` varchar(100) DEFAULT NULL,
  `cidade` varchar(100) DEFAULT NULL,
  `uf` varchar(2) DEFAULT NULL,
  PRIMARY KEY (`id_aeroporto`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aeroporto`
--

LOCK TABLES `aeroporto` WRITE;
/*!40000 ALTER TABLE `aeroporto` DISABLE KEYS */;
INSERT INTO `aeroporto` VALUES (1,'GRU','Aeroporto Internacional de São Paulo','São Paulo','SP'),(2,'GIG','Aeroporto Internacional do Rio de Janeiro','Rio de Janeiro','RJ'),(3,'CNF','Aeroporto Internacional Tancredo Neves','Belo Horizonte','MG');
/*!40000 ALTER TABLE `aeroporto` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `agencia`
--

DROP TABLE IF EXISTS `agencia`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `agencia` (
  `id_agencia` int NOT NULL,
  `nome` varchar(100) DEFAULT NULL,
  `taxaAgencia` double DEFAULT NULL,
  `funcionario_id-funcionario` int NOT NULL,
  PRIMARY KEY (`id_agencia`,`funcionario_id-funcionario`),
  KEY `fk_agencia_funcionario1_idx` (`funcionario_id-funcionario`),
  CONSTRAINT `fk_agencia_funcionario1` FOREIGN KEY (`funcionario_id-funcionario`) REFERENCES `funcionario` (`idFuncionario`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `agencia`
--

LOCK TABLES `agencia` WRITE;
/*!40000 ALTER TABLE `agencia` DISABLE KEYS */;
/*!40000 ALTER TABLE `agencia` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `assento`
--

DROP TABLE IF EXISTS `assento`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `assento` (
  `id_assento` int NOT NULL AUTO_INCREMENT,
  `numeroFileira` int DEFAULT NULL,
  `letraAssento` char(1) DEFAULT NULL,
  `aeronave_id_aeronave` int NOT NULL,
  `ocupado` tinyint DEFAULT '0',
  PRIMARY KEY (`id_assento`,`aeronave_id_aeronave`),
  KEY `fk_assento_aeronave1_idx` (`aeronave_id_aeronave`),
  CONSTRAINT `fk_assento_aeronave1` FOREIGN KEY (`aeronave_id_aeronave`) REFERENCES `aeronave` (`id_aeronave`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `assento`
--

LOCK TABLES `assento` WRITE;
/*!40000 ALTER TABLE `assento` DISABLE KEYS */;
INSERT INTO `assento` VALUES (1,1,'A',1,0),(2,3,'C',1,0),(3,2,'B',1,1);
/*!40000 ALTER TABLE `assento` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `bilhete`
--

DROP TABLE IF EXISTS `bilhete`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `bilhete` (
  `passagem_idPassagem` int NOT NULL,
  `passageiro_idPassageiro` int NOT NULL,
  `BilheteInternacional` tinyint DEFAULT NULL,
  `statusPassageiro` enum('Passagem adquirida','Passagem cancelada','Check-in realizado','Embarque realizado','NO SHOW') DEFAULT NULL,
  PRIMARY KEY (`passagem_idPassagem`,`passageiro_idPassageiro`),
  KEY `fk_bilhete_passagem1_idx` (`passagem_idPassagem`),
  KEY `fk_bilhete_passageiro1_idx` (`passageiro_idPassageiro`),
  CONSTRAINT `fk_bilhete_passageiro1` FOREIGN KEY (`passageiro_idPassageiro`) REFERENCES `passageiro` (`idPassageiro`),
  CONSTRAINT `fk_bilhete_passagem1` FOREIGN KEY (`passagem_idPassagem`) REFERENCES `passagem` (`idPassagem`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `bilhete`
--

LOCK TABLES `bilhete` WRITE;
/*!40000 ALTER TABLE `bilhete` DISABLE KEYS */;
/*!40000 ALTER TABLE `bilhete` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `companhiaaerea`
--

DROP TABLE IF EXISTS `companhiaaerea`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `companhiaaerea` (
  `cod` int NOT NULL AUTO_INCREMENT,
  `nome` varchar(100) NOT NULL,
  `razaoSocial` varchar(150) NOT NULL,
  `cnpj` varchar(14) NOT NULL,
  `taxaRemuneracao` double DEFAULT NULL,
  PRIMARY KEY (`cod`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `companhiaaerea`
--

LOCK TABLES `companhiaaerea` WRITE;
/*!40000 ALTER TABLE `companhiaaerea` DISABLE KEYS */;
INSERT INTO `companhiaaerea` VALUES (1,'Azul Linhas Aéreas','Azul Linhas Aéreas Brasileiras S.A.','12345678000195',12.5),(2,'Gol Linhas Aéreas','Gol Linhas Aéreas Inteligentes S.A.','23456789000185',10),(3,'Latam Airlines','Latam Airlines Group S.A.','34567890000175',11),(4,'Azul Linhas Aéreas','Azul Linhas Aéreas Brasileiras S.A.','12345678000195',12.5),(5,'Gol Linhas Aéreas','Gol Linhas Aéreas Inteligentes S.A.','23456789000185',10),(6,'Latam Airlines','Latam Airlines Group S.A.','34567890000175',11),(7,'Azul Linhas Aéreas','Azul Linhas Aéreas Brasileiras S.A.','12345678000195',12.5),(8,'Gol Linhas Aéreas','Gol Linhas Aéreas Inteligentes S.A.','23456789000185',10),(9,'Latam Airlines','Latam Airlines Group S.A.','34567890000175',11);
/*!40000 ALTER TABLE `companhiaaerea` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `companhiaaerea_has_agencia`
--

DROP TABLE IF EXISTS `companhiaaerea_has_agencia`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `companhiaaerea_has_agencia` (
  `companhiaaerea_cod` int NOT NULL,
  `agencia_id_agencia` int NOT NULL,
  PRIMARY KEY (`agencia_id_agencia`,`companhiaaerea_cod`),
  KEY `fk_companhiaaerea_has_agencia_companhiaaerea1_idx` (`companhiaaerea_cod`),
  KEY `fk_companhiaaerea_has_agencia_agencia1_idx` (`agencia_id_agencia`),
  CONSTRAINT `fk_companhiaaerea_has_agencia_agencia1` FOREIGN KEY (`agencia_id_agencia`) REFERENCES `agencia` (`id_agencia`),
  CONSTRAINT `fk_companhiaaerea_has_agencia_companhiaaerea1` FOREIGN KEY (`companhiaaerea_cod`) REFERENCES `companhiaaerea` (`cod`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `companhiaaerea_has_agencia`
--

LOCK TABLES `companhiaaerea_has_agencia` WRITE;
/*!40000 ALTER TABLE `companhiaaerea_has_agencia` DISABLE KEYS */;
/*!40000 ALTER TABLE `companhiaaerea_has_agencia` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `funcionario`
--

DROP TABLE IF EXISTS `funcionario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `funcionario` (
  `idFuncionario` int NOT NULL,
  `cpf` varchar(11) DEFAULT NULL,
  `nome` varchar(100) DEFAULT NULL,
  `email` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`idFuncionario`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `funcionario`
--

LOCK TABLES `funcionario` WRITE;
/*!40000 ALTER TABLE `funcionario` DISABLE KEYS */;
INSERT INTO `funcionario` VALUES (1,'12345678901','Carlos Silva','carlos.silva@empresa.com'),(2,'23456789012','Mariana Costa','mariana.costa@empresa.com'),(3,'34567890123','Ana Souza','ana.souza@empresa.com');
/*!40000 ALTER TABLE `funcionario` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `passageiro`
--

DROP TABLE IF EXISTS `passageiro`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `passageiro` (
  `idPassageiro` int NOT NULL AUTO_INCREMENT,
  `nome` varchar(100) DEFAULT NULL,
  `cpf` varchar(11) DEFAULT NULL,
  `rg` varchar(20) DEFAULT NULL,
  `email` varchar(100) DEFAULT NULL,
  `usuario_id_usuario` int NOT NULL,
  PRIMARY KEY (`idPassageiro`),
  UNIQUE KEY `rg_UNIQUE` (`rg`),
  KEY `fk_viajante_usuario1_idx` (`usuario_id_usuario`),
  CONSTRAINT `fk_viajante_usuario1` FOREIGN KEY (`usuario_id_usuario`) REFERENCES `usuario` (`id_usuario`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `passageiro`
--

LOCK TABLES `passageiro` WRITE;
/*!40000 ALTER TABLE `passageiro` DISABLE KEYS */;
INSERT INTO `passageiro` VALUES (1,'João Mendes','98765432100','MG1234567','joao.mendes@email.com',1),(2,'Maria Fernanda','87654321099','SP7654321','maria.fernanda@email.com',2),(3,'Lucas Silva','76543210988','RJ6543210','lucas.silva@email.com',3);
/*!40000 ALTER TABLE `passageiro` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `passageirovip`
--

DROP TABLE IF EXISTS `passageirovip`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `passageirovip` (
  `companhiaaerea_cod` int NOT NULL,
  `passageiro_idPassageiro` int NOT NULL,
  PRIMARY KEY (`companhiaaerea_cod`,`passageiro_idPassageiro`),
  KEY `fk_passageiroVIP_companhiaaerea_idx` (`companhiaaerea_cod`),
  KEY `fk_passageiroVIP_passageiro1_idx` (`passageiro_idPassageiro`),
  CONSTRAINT `fk_passageiroVIP_companhiaaerea` FOREIGN KEY (`companhiaaerea_cod`) REFERENCES `companhiaaerea` (`cod`),
  CONSTRAINT `fk_passageiroVIP_passageiro1` FOREIGN KEY (`passageiro_idPassageiro`) REFERENCES `passageiro` (`idPassageiro`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `passageirovip`
--

LOCK TABLES `passageirovip` WRITE;
/*!40000 ALTER TABLE `passageirovip` DISABLE KEYS */;
INSERT INTO `passageirovip` VALUES (1,1),(2,2),(3,3);
/*!40000 ALTER TABLE `passageirovip` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `passagem`
--

DROP TABLE IF EXISTS `passagem`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `passagem` (
  `idPassagem` int NOT NULL AUTO_INCREMENT,
  `moeda` varchar(3) DEFAULT NULL,
  `tarifa_idTarifa` int NOT NULL,
  `valorbagagem_id` int NOT NULL,
  `idVoo1` int NOT NULL,
  `idVoo2` int DEFAULT NULL,
  PRIMARY KEY (`idPassagem`),
  UNIQUE KEY `voo_id_voo_UNIQUE` (`idVoo1`),
  KEY `fk_passagem_valorbagagem1_idx` (`valorbagagem_id`),
  KEY `fk_passagem_voo1_idx` (`idVoo1`),
  KEY `fk_passagem_voo2_idx` (`idVoo2`),
  KEY `fk_passagem_tarifa1_idx` (`tarifa_idTarifa`),
  CONSTRAINT `fk_passagem_tarifa1` FOREIGN KEY (`tarifa_idTarifa`) REFERENCES `tarifa` (`idTarifa`),
  CONSTRAINT `fk_passagem_valorbagagem1` FOREIGN KEY (`valorbagagem_id`) REFERENCES `valorbagagem` (`id`),
  CONSTRAINT `fk_passagem_voo1` FOREIGN KEY (`idVoo1`) REFERENCES `voo` (`id_voo`),
  CONSTRAINT `fk_passagem_voo2` FOREIGN KEY (`idVoo2`) REFERENCES `voo` (`id_voo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `passagem`
--

LOCK TABLES `passagem` WRITE;
/*!40000 ALTER TABLE `passagem` DISABLE KEYS */;
/*!40000 ALTER TABLE `passagem` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tarifa`
--

DROP TABLE IF EXISTS `tarifa`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tarifa` (
  `idTarifa` int NOT NULL AUTO_INCREMENT,
  `valor` double DEFAULT NULL,
  `companhiaaerea_cod` int NOT NULL,
  PRIMARY KEY (`idTarifa`),
  KEY `fk_tarifa_companhiaaerea1_idx` (`companhiaaerea_cod`),
  CONSTRAINT `fk_tarifa_companhiaaerea1` FOREIGN KEY (`companhiaaerea_cod`) REFERENCES `companhiaaerea` (`cod`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tarifa`
--

LOCK TABLES `tarifa` WRITE;
/*!40000 ALTER TABLE `tarifa` DISABLE KEYS */;
/*!40000 ALTER TABLE `tarifa` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usuario`
--

DROP TABLE IF EXISTS `usuario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `usuario` (
  `id_usuario` int NOT NULL,
  `login` varchar(50) DEFAULT NULL,
  `senha` varchar(100) DEFAULT NULL,
  `funcionario_id-funcionario` int NOT NULL,
  PRIMARY KEY (`id_usuario`),
  KEY `fk_usuario_funcionario1_idx` (`funcionario_id-funcionario`),
  CONSTRAINT `fk_usuario_funcionario1` FOREIGN KEY (`funcionario_id-funcionario`) REFERENCES `funcionario` (`idFuncionario`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuario`
--

LOCK TABLES `usuario` WRITE;
/*!40000 ALTER TABLE `usuario` DISABLE KEYS */;
INSERT INTO `usuario` VALUES (1,'carloss','senha123',1),(2,'marianac','senha456',2),(3,'anas','senha789',3);
/*!40000 ALTER TABLE `usuario` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `valorbagagem`
--

DROP TABLE IF EXISTS `valorbagagem`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `valorbagagem` (
  `id` int NOT NULL AUTO_INCREMENT,
  `valorPrimeiraBagagem` double DEFAULT NULL,
  `valorBagagemAdicional` double DEFAULT NULL,
  `companhiaaerea_cod` int NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `companhiaaerea_cod_UNIQUE` (`companhiaaerea_cod`),
  KEY `fk_valorbagagem_companhiaaerea1_idx` (`companhiaaerea_cod`),
  CONSTRAINT `fk_valorbagagem_companhiaaerea1` FOREIGN KEY (`companhiaaerea_cod`) REFERENCES `companhiaaerea` (`cod`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `valorbagagem`
--

LOCK TABLES `valorbagagem` WRITE;
/*!40000 ALTER TABLE `valorbagagem` DISABLE KEYS */;
/*!40000 ALTER TABLE `valorbagagem` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `voo`
--

DROP TABLE IF EXISTS `voo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `voo` (
  `id_voo` int NOT NULL AUTO_INCREMENT,
  `codVoo` varchar(10) DEFAULT NULL,
  `data` datetime DEFAULT NULL,
  `ehInternacional` tinyint DEFAULT '0',
  `duracao` date DEFAULT NULL,
  `companhiaaerea_cod` int NOT NULL,
  `aeronave_id_aeronave` int NOT NULL,
  `idAeroportoOrigem` int NOT NULL,
  `idAeroportoDestino` int NOT NULL,
  PRIMARY KEY (`id_voo`),
  KEY `fk_voo_companhiaaerea1_idx` (`companhiaaerea_cod`),
  KEY `fk_voo_aeronave1_idx` (`aeronave_id_aeronave`),
  KEY `fk_voo_aeroporto1_idx` (`idAeroportoOrigem`),
  KEY `fk_voo_aeroporto2_idx` (`idAeroportoDestino`),
  CONSTRAINT `fk_voo_aeronave1` FOREIGN KEY (`aeronave_id_aeronave`) REFERENCES `aeronave` (`id_aeronave`),
  CONSTRAINT `fk_voo_aeroporto1` FOREIGN KEY (`idAeroportoOrigem`) REFERENCES `aeroporto` (`id_aeroporto`),
  CONSTRAINT `fk_voo_aeroporto2` FOREIGN KEY (`idAeroportoDestino`) REFERENCES `aeroporto` (`id_aeroporto`),
  CONSTRAINT `fk_voo_companhiaaerea1` FOREIGN KEY (`companhiaaerea_cod`) REFERENCES `companhiaaerea` (`cod`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `voo`
--

LOCK TABLES `voo` WRITE;
/*!40000 ALTER TABLE `voo` DISABLE KEYS */;
/*!40000 ALTER TABLE `voo` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-12-02  9:53:01
