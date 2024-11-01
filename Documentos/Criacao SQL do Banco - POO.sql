-- MySQL Script generated by MySQL Workbench
-- Fri Oct 25 20:43:31 2024
-- Model: New Model    Version: 1.0
-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Schema sistema_aeroporto
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema sistema_aeroporto
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `sistema_aeroporto` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci ;
USE `sistema_aeroporto` ;

-- -----------------------------------------------------
-- Table `sistema_aeroporto`.`aeroporto`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sistema_aeroporto`.`aeroporto` (
  `id_aeroporto` INT NOT NULL,
  `sigla` VARCHAR(10) NULL,
  `nome` VARCHAR(100) NULL DEFAULT NULL,
  `cidade` VARCHAR(100) NULL DEFAULT NULL,
  `uf` VARCHAR(2) NULL DEFAULT NULL,
  PRIMARY KEY (`id_aeroporto`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Criando Tabela `sistema_aeroporto`.`funcionario`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sistema_aeroporto`.`funcionario` (
  `id-funcionario` INT NOT NULL,
  `cpf` VARCHAR(11) NULL,
  `nome` VARCHAR(100) NULL DEFAULT NULL,
  `email` VARCHAR(100) NULL DEFAULT NULL,
  PRIMARY KEY (`id-funcionario`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Criando Tabela `sistema_aeroporto`.`agencia`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sistema_aeroporto`.`agencia` (
  `id_agencia` INT NOT NULL,
  `nome` VARCHAR(100) NULL,
  `taxaAgencia` DOUBLE NULL DEFAULT NULL,
  `funcionario_id-funcionario` INT NOT NULL,
  PRIMARY KEY (`id_agencia`, `funcionario_id-funcionario`),
  INDEX `fk_agencia_funcionario1_idx` (`funcionario_id-funcionario` ASC) ,
  CONSTRAINT `fk_agencia_funcionario1`
    FOREIGN KEY (`funcionario_id-funcionario`)
    REFERENCES `sistema_aeroporto`.`funcionario` (`id-funcionario`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Criando Tabela `sistema_aeroporto`.`bilhete`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sistema_aeroporto`.`bilhete` (
  `idBilhete` INT NOT NULL AUTO_INCREMENT,
  `BilheteInternacional` TINYINT NULL,
  PRIMARY KEY (`idBilhete`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Criando Tabela `sistema_aeroporto`.`companhiaaerea`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sistema_aeroporto`.`companhiaaerea` (
  `cod` DOUBLE NOT NULL,
  `nome` VARCHAR(100) NULL DEFAULT NULL,
  `razaoSocial` VARCHAR(150) NULL DEFAULT NULL,
  `cnpj` VARCHAR(14) NULL DEFAULT NULL,
  `tipoVoo` ENUM('Domestico', 'Internacional') NULL DEFAULT NULL,
  `taxaRemuneracao` DOUBLE NULL DEFAULT NULL,
  PRIMARY KEY (`cod`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Criando Tabela `sistema_aeroporto`.`valorbagagem`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sistema_aeroporto`.`valorbagagem` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `valorPrimeiraBagagem` DOUBLE NULL DEFAULT NULL,
  `valorBagagemAdicional` DOUBLE NULL DEFAULT NULL,
  `companhiaaerea_cod` DOUBLE NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_valorbagagem_companhiaaerea1_idx` (`companhiaaerea_cod` ASC) ,
  CONSTRAINT `fk_valorbagagem_companhiaaerea1`
    FOREIGN KEY (`companhiaaerea_cod`)
    REFERENCES `sistema_aeroporto`.`companhiaaerea` (`cod`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Criando Tabela `sistema_aeroporto`.`passagem`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sistema_aeroporto`.`passagem` (
  `idPassagem` INT NOT NULL AUTO_INCREMENT,
  `moeda` VARCHAR(3) NULL DEFAULT NULL,
  `tarifa` VARCHAR(45) NULL,
  `companhia_aerea` VARCHAR(45) NULL,
  `bilhete_idBilhete` INT NOT NULL,
  `valorbagagem_id` INT NOT NULL,
  PRIMARY KEY (`idPassagem`),
  INDEX `fk_passagem_bilhete1_idx` (`bilhete_idBilhete` ASC) ,
  INDEX `fk_passagem_valorbagagem1_idx` (`valorbagagem_id` ASC) ,
  CONSTRAINT `fk_passagem_bilhete1`
    FOREIGN KEY (`bilhete_idBilhete`)
    REFERENCES `sistema_aeroporto`.`bilhete` (`idBilhete`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_passagem_valorbagagem1`
    FOREIGN KEY (`valorbagagem_id`)
    REFERENCES `sistema_aeroporto`.`valorbagagem` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Criando Tabela `sistema_aeroporto`.`tarifa`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sistema_aeroporto`.`tarifa` (
  `idTarifa` INT NOT NULL AUTO_INCREMENT,
  `valor` DOUBLE NULL DEFAULT NULL,
  `companhiaaerea_cod` DOUBLE NOT NULL,
  `passagem_idPassagem` INT NOT NULL,
  PRIMARY KEY (`idTarifa`, `passagem_idPassagem`),
  INDEX `fk_tarifa_companhiaaerea1_idx` (`companhiaaerea_cod` ASC) ,
  INDEX `fk_tarifa_passagem1_idx` (`passagem_idPassagem` ASC) ,
  CONSTRAINT `fk_tarifa_companhiaaerea1`
    FOREIGN KEY (`companhiaaerea_cod`)
    REFERENCES `sistema_aeroporto`.`companhiaaerea` (`cod`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_tarifa_passagem1`
    FOREIGN KEY (`passagem_idPassagem`)
    REFERENCES `sistema_aeroporto`.`passagem` (`idPassagem`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Criando Tabela `sistema_aeroporto`.`usuario`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sistema_aeroporto`.`usuario` (
  `id_usuario` INT NOT NULL,
  `login` VARCHAR(50) NULL,
  `senha` VARCHAR(100) NULL DEFAULT NULL,
  `funcionario_id-funcionario` INT NOT NULL,
  PRIMARY KEY (`id_usuario`),
  INDEX `fk_usuario_funcionario1_idx` (`funcionario_id-funcionario` ASC) ,
  CONSTRAINT `fk_usuario_funcionario1`
    FOREIGN KEY (`funcionario_id-funcionario`)
    REFERENCES `sistema_aeroporto`.`funcionario` (`id-funcionario`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Criando Tabela `sistema_aeroporto`.`viajante`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sistema_aeroporto`.`viajante` (
  `idViajante` INT NOT NULL AUTO_INCREMENT,
  `nome` VARCHAR(100) NULL DEFAULT NULL,
  `cpf` VARCHAR(11) NULL DEFAULT NULL,
  `rg` VARCHAR(20) NULL DEFAULT NULL,
  `bilhete_idBilhete` INT NOT NULL,
  `usuario_id_usuario` INT NOT NULL,
  PRIMARY KEY (`idViajante`),
  INDEX `fk_viajante_bilhete1_idx` (`bilhete_idBilhete` ASC) ,
  UNIQUE INDEX `rg_UNIQUE` (`rg` ASC) ,
  UNIQUE INDEX `bilhete_idBilhete_UNIQUE` (`bilhete_idBilhete` ASC) ,
  INDEX `fk_viajante_usuario1_idx` (`usuario_id_usuario` ASC) ,
  CONSTRAINT `fk_viajante_bilhete1`
    FOREIGN KEY (`bilhete_idBilhete`)
    REFERENCES `sistema_aeroporto`.`bilhete` (`idBilhete`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_viajante_usuario1`
    FOREIGN KEY (`usuario_id_usuario`)
    REFERENCES `sistema_aeroporto`.`usuario` (`id_usuario`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Criando Tabela `sistema_aeroporto`.`voo`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sistema_aeroporto`.`voo` (
  `id_voo` INT NOT NULL,
  `codVoo` VARCHAR(10) NULL,
  `data` DATETIME NULL DEFAULT NULL,
  `destino` VARCHAR(45) NULL,
  `origem` VARCHAR(45) NULL,
  `companhiaaerea_cod` DOUBLE NOT NULL,
  INDEX `fk_voo_companhiaaerea1_idx` (`companhiaaerea_cod` ASC) ,
  PRIMARY KEY (`id_voo`),
  CONSTRAINT `fk_voo_companhiaaerea1`
    FOREIGN KEY (`companhiaaerea_cod`)
    REFERENCES `sistema_aeroporto`.`companhiaaerea` (`cod`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Criando Tabela `sistema_aeroporto`.`companhiaaerea_has_agencia`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sistema_aeroporto`.`companhiaaerea_has_agencia` (
  `companhiaaerea_cod` DOUBLE NOT NULL,
  `agencia_id_agencia` INT NOT NULL,
  `agencia_funcionario_id-funcionario` INT NOT NULL,
  PRIMARY KEY (`companhiaaerea_cod`),
  INDEX `fk_companhiaaerea_has_agencia_companhiaaerea1_idx` (`companhiaaerea_cod` ASC) ,
  INDEX `fk_companhiaaerea_has_agencia_agencia1_idx` (`agencia_id_agencia` ASC, `agencia_funcionario_id-funcionario` ASC) ,
  CONSTRAINT `fk_companhiaaerea_has_agencia_companhiaaerea1`
    FOREIGN KEY (`companhiaaerea_cod`)
    REFERENCES `sistema_aeroporto`.`companhiaaerea` (`cod`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_companhiaaerea_has_agencia_agencia1`
    FOREIGN KEY (`agencia_id_agencia` , `agencia_funcionario_id-funcionario`)
    REFERENCES `sistema_aeroporto`.`agencia` (`id_agencia` , `funcionario_id-funcionario`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Criando Tabela `sistema_aeroporto`.`aeroporto_has_voo`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sistema_aeroporto`.`aeroporto_has_voo` (
  `aeroporto_id_aeroporto` INT NOT NULL,
  `voo_id_voo` INT NOT NULL,
  PRIMARY KEY (`aeroporto_id_aeroporto`, `voo_id_voo`),
  INDEX `fk_aeroporto_has_voo_aeroporto1_idx` (`aeroporto_id_aeroporto` ASC) ,
  INDEX `fk_aeroporto_has_voo_voo1_idx` (`voo_id_voo` ASC),
  CONSTRAINT `fk_aeroporto_has_voo_aeroporto1`
    FOREIGN KEY (`aeroporto_id_aeroporto`)
    REFERENCES `sistema_aeroporto`.`aeroporto` (`id_aeroporto`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_aeroporto_has_voo_voo1`
    FOREIGN KEY (`voo_id_voo`)
    REFERENCES `sistema_aeroporto`.`voo` (`id_voo`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Criando Tabela `sistema_aeroporto`.`voo_has_passagem`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sistema_aeroporto`.`voo_has_passagem` (
  `voo_id_voo` INT NOT NULL,
  `passagem_idPassagem` INT NOT NULL,
  PRIMARY KEY (`voo_id_voo`, `passagem_idPassagem`),
  INDEX `fk_voo_has_passagem_voo1_idx` (`voo_id_voo` ASC) ,
  INDEX `fk_voo_has_passagem_passagem1_idx` (`passagem_idPassagem` ASC) ,
  CONSTRAINT `fk_voo_has_passagem_voo1`
    FOREIGN KEY (`voo_id_voo`)
    REFERENCES `sistema_aeroporto`.`voo` (`id_voo`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_voo_has_passagem_passagem1`
    FOREIGN KEY (`passagem_idPassagem`)
    REFERENCES `sistema_aeroporto`.`passagem` (`idPassagem`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;

-- -------------------------------------------------------------------------------- --
-- algumas coisas que pensei depois -- 
-- Definindo campos como not null --
ALTER TABLE aeroporto MODIFY sigla VARCHAR(10) NOT NULL;
ALTER TABLE funcionario MODIFY cpf VARCHAR(11) NOT NULL;

-- Definindo colunas com valores unicos -- 
ALTER TABLE funcionario ADD CONSTRAINT unique_cpf UNIQUE (cpf);
ALTER TABLE usuario ADD CONSTRAINT unique_login UNIQUE (login);

-- Restringindo valores de algumas colunas -- 
ALTER TABLE passagem ADD CONSTRAINT check_moeda CHECK (LENGTH(moeda) = 3);
ALTER TABLE valorbagagem ADD CONSTRAINT check_valor_positivo CHECK (valorPrimeiraBagagem >= 0 AND valorBagagemAdicional >= 0);
ALTER TABLE agencia ADD CONSTtipoVooRAINT check_taxa CHECK (taxaAgencia BETWEEN 0 AND 1);

-- definindo um valor default para tipovoo de companhiaaerea--
ALTER TABLE companhiaaerea MODIFY tipoVoo ENUM('Domestico', 'Internacional') DEFAULT 'Domestico';