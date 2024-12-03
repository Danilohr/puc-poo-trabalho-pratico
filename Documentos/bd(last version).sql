-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema sistema_aeroporto
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `sistema_aeroporto` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci ;
USE `mydb` ;

-- -----------------------------------------------------
-- Table `sistema_aeroporto`.`companhiaaerea`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sistema_aeroporto`.`companhiaaerea` (
  `cod` INT NOT NULL AUTO_INCREMENT,
  `nome` VARCHAR(100) NOT NULL,
  `razaoSocial` VARCHAR(150) NOT NULL,
  `cnpj` VARCHAR(14) NOT NULL,
  `taxaRemuneracao` DOUBLE NULL DEFAULT NULL,
  PRIMARY KEY (`cod`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `sistema_aeroporto`.`funcionario`
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
-- Table `sistema_aeroporto`.`usuario`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sistema_aeroporto`.`usuario` (
  `id_usuario` INT NOT NULL,
  `login` VARCHAR(50) NULL,
  `senha` VARCHAR(100) NULL DEFAULT NULL,
  `funcionario_id-funcionario` INT NOT NULL,
  PRIMARY KEY (`id_usuario`),
  INDEX `fk_usuario_funcionario1_idx` (`funcionario_id-funcionario` ASC) VISIBLE,
  CONSTRAINT `fk_usuario_funcionario1`
    FOREIGN KEY (`funcionario_id-funcionario`)
    REFERENCES `sistema_aeroporto`.`funcionario` (`id-funcionario`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `sistema_aeroporto`.`passageiro`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sistema_aeroporto`.`passageiro` (
  `idPassageiro` INT NOT NULL AUTO_INCREMENT,
  `nome` VARCHAR(100) NULL DEFAULT NULL,
  `cpf` VARCHAR(11) NULL DEFAULT NULL,
  `rg` VARCHAR(20) NULL DEFAULT NULL,
  `usuario_id_usuario` INT NOT NULL,
  PRIMARY KEY (`idPassageiro`),
  UNIQUE INDEX `rg_UNIQUE` (`rg` ASC) VISIBLE,
  INDEX `fk_viajante_usuario1_idx` (`usuario_id_usuario` ASC) VISIBLE,
  CONSTRAINT `fk_viajante_usuario1`
    FOREIGN KEY (`usuario_id_usuario`)
    REFERENCES `sistema_aeroporto`.`usuario` (`id_usuario`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `passageiroVIP`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS sistema_aeroporto.passageiroVIP (
  companhiaaerea_cod INT NOT NULL,
  passageiro_idPassageiro INT NOT NULL,
  PRIMARY KEY (companhiaaerea_cod, passageiro_idPassageiro),
  INDEX fk_passageiroVIP_companhiaaerea_idx (companhiaaerea_cod ASC) VISIBLE,
  INDEX fk_passageiroVIP_passageiro1_idx (passageiro_idPassageiro ASC) VISIBLE,
  CONSTRAINT fk_passageiroVIP_companhiaaerea
    FOREIGN KEY (companhiaaerea_cod)
    REFERENCES sistema_aeroporto.companhiaaerea (cod)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT fk_passageiroVIP_passageiro1
    FOREIGN KEY (passageiro_idPassageiro)
    REFERENCES sistema_aeroporto.passageiro (idPassageiro)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;

INSERT INTO sistema_aeroporto.passageiroVIP (companhiaaerea_cod, passageiro_idPassageiro) VALUES
(1, 1),
(2, 2),
(3, 3);
ALTER TABLE sistema_aeroporto.passageiro
ADD COLUMN email VARCHAR(100) NULL DEFAULT NULL AFTER rg;

-- -----------------------------------------------------
-- Table `sistema_aeroporto`.`aeroporto`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sistema_aeroporto`.`aeroporto` (
  `id_aeroporto` INT NOT NULL AUTO_INCREMENT,
  `sigla` VARCHAR(10) NULL,
  `nome` VARCHAR(100) NULL DEFAULT NULL,
  `cidade` VARCHAR(100) NULL DEFAULT NULL,
  `uf` VARCHAR(2) NULL DEFAULT NULL,
  PRIMARY KEY (`id_aeroporto`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `sistema_aeroporto`.`agencia`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sistema_aeroporto`.`agencia` (
  `id_agencia` INT NOT NULL,
  `nome` VARCHAR(100) NULL,
  `taxaAgencia` DOUBLE NULL DEFAULT NULL,
  `funcionario_id-funcionario` INT NOT NULL,
  PRIMARY KEY (`id_agencia`, `funcionario_id-funcionario`),
  INDEX `fk_agencia_funcionario1_idx` (`funcionario_id-funcionario` ASC) VISIBLE,
  CONSTRAINT `fk_agencia_funcionario1`
    FOREIGN KEY (`funcionario_id-funcionario`)
    REFERENCES `sistema_aeroporto`.`funcionario` (`id-funcionario`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `sistema_aeroporto`.`valorbagagem`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sistema_aeroporto`.`valorbagagem` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `valorPrimeiraBagagem` DOUBLE NULL DEFAULT NULL,
  `valorBagagemAdicional` DOUBLE NULL DEFAULT NULL,
  `companhiaaerea_cod` INT NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_valorbagagem_companhiaaerea1_idx` (`companhiaaerea_cod` ASC) VISIBLE,
  UNIQUE INDEX `companhiaaerea_cod_UNIQUE` (`companhiaaerea_cod` ASC) VISIBLE,
  CONSTRAINT `fk_valorbagagem_companhiaaerea1`
    FOREIGN KEY (`companhiaaerea_cod`)
    REFERENCES `sistema_aeroporto`.`companhiaaerea` (`cod`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `sistema_aeroporto`.`aeronave`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sistema_aeroporto`.`aeronave` (
  `id_aeronave` INT NOT NULL AUTO_INCREMENT,
  `nome` VARCHAR(45) NOT NULL,
  `capacidadePassageiros` INT NOT NULL,
  `capacidadeBagagens` INT NOT NULL,
  PRIMARY KEY (`id_aeronave`),
  UNIQUE INDEX `capacidadePassageiros_UNIQUE` (`capacidadePassageiros` ASC) VISIBLE,
  UNIQUE INDEX `capacidadeBagagens_UNIQUE` (`capacidadeBagagens` ASC) VISIBLE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `sistema_aeroporto`.`voo`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sistema_aeroporto`.`voo` (
  `id_voo` INT NOT NULL AUTO_INCREMENT,
  `codVoo` VARCHAR(10) NULL,
  `data` DATETIME NULL DEFAULT NULL,
  `ehInternacional` TINYINT NULL DEFAULT 0,
  `duracao` DATE NULL,
  `companhiaaerea_cod` INT NOT NULL,
  `aeronave_id_aeronave` INT NOT NULL,
  `idAeroportoOrigem` INT NOT NULL,
  `idAeroportoDestino` INT NOT NULL,
  INDEX `fk_voo_companhiaaerea1_idx` (`companhiaaerea_cod` ASC) VISIBLE,
  PRIMARY KEY (`id_voo`),
  INDEX `fk_voo_aeronave1_idx` (`aeronave_id_aeronave` ASC) VISIBLE,
  INDEX `fk_voo_aeroporto1_idx` (`idAeroportoOrigem` ASC) VISIBLE,
  INDEX `fk_voo_aeroporto2_idx` (`idAeroportoDestino` ASC) VISIBLE,
  CONSTRAINT `fk_voo_companhiaaerea1`
    FOREIGN KEY (`companhiaaerea_cod`)
    REFERENCES `sistema_aeroporto`.`companhiaaerea` (`cod`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_voo_aeronave1`
    FOREIGN KEY (`aeronave_id_aeronave`)
    REFERENCES `sistema_aeroporto`.`aeronave` (`id_aeronave`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_voo_aeroporto1`
    FOREIGN KEY (`idAeroportoOrigem`)
    REFERENCES `sistema_aeroporto`.`aeroporto` (`id_aeroporto`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_voo_aeroporto2`
    FOREIGN KEY (`idAeroportoDestino`)
    REFERENCES `sistema_aeroporto`.`aeroporto` (`id_aeroporto`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `sistema_aeroporto`.`tarifa`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sistema_aeroporto`.`tarifa` (
  `idTarifa` INT NOT NULL AUTO_INCREMENT,
  `valor` DOUBLE NULL DEFAULT NULL,
  `companhiaaerea_cod` INT NOT NULL,
  PRIMARY KEY (`idTarifa`),
  INDEX `fk_tarifa_companhiaaerea1_idx` (`companhiaaerea_cod` ASC) VISIBLE,
  CONSTRAINT `fk_tarifa_companhiaaerea1`
    FOREIGN KEY (`companhiaaerea_cod`)
    REFERENCES `sistema_aeroporto`.`companhiaaerea` (`cod`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `sistema_aeroporto`.`passagem`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sistema_aeroporto`.`passagem` (
  `idPassagem` INT NOT NULL AUTO_INCREMENT,
  `moeda` VARCHAR(3) NULL,
  `tarifa_idTarifa` INT NOT NULL,
  `valorbagagem_id` INT NOT NULL,
  `idVoo1` INT NOT NULL,
  `idVoo2` INT NULL,
  PRIMARY KEY (`idPassagem`),
  INDEX `fk_passagem_valorbagagem1_idx` (`valorbagagem_id` ASC) VISIBLE,
  INDEX `fk_passagem_voo1_idx` (`idVoo1` ASC) VISIBLE,
  UNIQUE INDEX `voo_id_voo_UNIQUE` (`idVoo1` ASC) VISIBLE,
  INDEX `fk_passagem_voo2_idx` (`idVoo2` ASC) VISIBLE,
  INDEX `fk_passagem_tarifa1_idx` (`tarifa_idTarifa` ASC) VISIBLE,
  CONSTRAINT `fk_passagem_valorbagagem1`
    FOREIGN KEY (`valorbagagem_id`)
    REFERENCES `sistema_aeroporto`.`valorbagagem` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_passagem_voo1`
    FOREIGN KEY (`idVoo1`)
    REFERENCES `sistema_aeroporto`.`voo` (`id_voo`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_passagem_voo2`
    FOREIGN KEY (`idVoo2`)
    REFERENCES `sistema_aeroporto`.`voo` (`id_voo`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_passagem_tarifa1`
    FOREIGN KEY (`tarifa_idTarifa`)
    REFERENCES `sistema_aeroporto`.`tarifa` (`idTarifa`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `sistema_aeroporto`.`bilhete`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sistema_aeroporto`.`bilhete` (
  `passagem_idPassagem` INT NOT NULL,
  `passageiro_idPassageiro` INT NOT NULL,
  `BilheteInternacional` TINYINT NULL,
  `statusPassageiro` ENUM('Passagem adquirida', 'Passagem cancelada', 'Check-in realizado', 'Embarque realizado', 'NO SHOW') NULL,
  PRIMARY KEY (`passagem_idPassagem`, `passageiro_idPassageiro`),
  INDEX `fk_bilhete_passagem1_idx` (`passagem_idPassagem` ASC) VISIBLE,
  INDEX `fk_bilhete_passageiro1_idx` (`passageiro_idPassageiro` ASC) VISIBLE,
  CONSTRAINT `fk_bilhete_passagem1`
    FOREIGN KEY (`passagem_idPassagem`)
    REFERENCES `sistema_aeroporto`.`passagem` (`idPassagem`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_bilhete_passageiro1`
    FOREIGN KEY (`passageiro_idPassageiro`)
    REFERENCES `sistema_aeroporto`.`passageiro` (`idPassageiro`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `sistema_aeroporto`.`companhiaaerea_has_agencia`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sistema_aeroporto`.`companhiaaerea_has_agencia` (
  `companhiaaerea_cod` INT NOT NULL,
  `agencia_id_agencia` INT NOT NULL,
  PRIMARY KEY (`agencia_id_agencia`, `companhiaaerea_cod`),
  INDEX `fk_companhiaaerea_has_agencia_companhiaaerea1_idx` (`companhiaaerea_cod` ASC) VISIBLE,
  INDEX `fk_companhiaaerea_has_agencia_agencia1_idx` (`agencia_id_agencia` ASC) VISIBLE,
  CONSTRAINT `fk_companhiaaerea_has_agencia_companhiaaerea1`
    FOREIGN KEY (`companhiaaerea_cod`)
    REFERENCES `sistema_aeroporto`.`companhiaaerea` (`cod`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_companhiaaerea_has_agencia_agencia1`
    FOREIGN KEY (`agencia_id_agencia`)
    REFERENCES `sistema_aeroporto`.`agencia` (`id_agencia`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `sistema_aeroporto`.`assento`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `sistema_aeroporto`.`assento` (
  `id_assento` INT NOT NULL AUTO_INCREMENT,
  `numeroFileira` INT NULL,
  `letraAssento` CHAR NULL,
  `aeronave_id_aeronave` INT NOT NULL,
  `ocupado` TINYINT NULL DEFAULT 0,
  PRIMARY KEY (`id_assento`, `aeronave_id_aeronave`),
  INDEX `fk_assento_aeronave1_idx` (`aeronave_id_aeronave` ASC) VISIBLE,
  CONSTRAINT `fk_assento_aeronave1`
    FOREIGN KEY (`aeronave_id_aeronave`)
    REFERENCES `sistema_aeroporto`.`aeronave` (`id_aeronave`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;