﻿// <auto-generated />
using System;
using FlyNow.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FlyNow.Migrations
{
    [DbContext(typeof(FlyNowContext))]
    [Migration("20241130202746_AtualizarEnumPassagem")]
    partial class AtualizarEnumPassagem
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("utf8mb4_0900_ai_ci")
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.HasCharSet(modelBuilder, "utf8mb4");
            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("FlyNow.EfModels.Aeronave", b =>
                {
                    b.Property<int>("IdAeronave")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_aeronave");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdAeronave"));

                    b.Property<int>("CapacidadeBagagens")
                        .HasColumnType("int")
                        .HasColumnName("capacidadeBagagens");

                    b.Property<int>("CapacidadePassageiros")
                        .HasColumnType("int")
                        .HasColumnName("capacidadePassageiros");

                    b.HasKey("IdAeronave")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "CapacidadeBagagens" }, "capacidadeBagagens_UNIQUE")
                        .IsUnique();

                    b.HasIndex(new[] { "CapacidadePassageiros" }, "capacidadePassageiros_UNIQUE")
                        .IsUnique();

                    b.ToTable("aeronave", (string)null);
                });

            modelBuilder.Entity("FlyNow.EfModels.Aeroporto", b =>
                {
                    b.Property<int>("IdAeroporto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_aeroporto");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdAeroporto"));

                    b.Property<string>("Cidade")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("cidade");

                    b.Property<string>("Nome")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("nome");

                    b.Property<string>("Sigla")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("sigla");

                    b.Property<string>("Uf")
                        .HasMaxLength(2)
                        .HasColumnType("varchar(2)")
                        .HasColumnName("uf");

                    b.HasKey("IdAeroporto")
                        .HasName("PRIMARY");

                    b.ToTable("aeroporto", (string)null);
                });

            modelBuilder.Entity("FlyNow.EfModels.Agencium", b =>
                {
                    b.Property<int>("IdAgencia")
                        .HasColumnType("int")
                        .HasColumnName("id_agencia");

                    b.Property<int>("FuncionarioIdFuncionario")
                        .HasColumnType("int")
                        .HasColumnName("funcionario_id-funcionario");

                    b.Property<string>("Nome")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("nome");

                    b.Property<double?>("TaxaAgencia")
                        .HasColumnType("double")
                        .HasColumnName("taxaAgencia");

                    b.HasKey("IdAgencia", "FuncionarioIdFuncionario")
                        .HasName("PRIMARY")
                        .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                    b.HasIndex(new[] { "FuncionarioIdFuncionario" }, "fk_agencia_funcionario1_idx");

                    b.ToTable("agencia", (string)null);
                });

            modelBuilder.Entity("FlyNow.EfModels.Assento", b =>
                {
                    b.Property<int>("IdAssento")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_assento");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdAssento"));

                    b.Property<int>("AeronaveIdAeronave")
                        .HasColumnType("int")
                        .HasColumnName("aeronave_id_aeronave");

                    b.Property<string>("LetraAssento")
                        .HasMaxLength(1)
                        .HasColumnType("char(1)")
                        .HasColumnName("letraAssento")
                        .IsFixedLength();

                    b.Property<int?>("NumeroFileira")
                        .HasColumnType("int")
                        .HasColumnName("numeroFileira");

                    b.Property<sbyte?>("Ocupado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasColumnName("ocupado")
                        .HasDefaultValueSql("'0'");

                    b.HasKey("IdAssento", "AeronaveIdAeronave")
                        .HasName("PRIMARY")
                        .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                    b.HasIndex(new[] { "AeronaveIdAeronave" }, "fk_assento_aeronave1_idx");

                    b.ToTable("assento", (string)null);
                });

            modelBuilder.Entity("FlyNow.EfModels.Bilhete", b =>
                {
                    b.Property<int>("PassagemIdPassagem")
                        .HasColumnType("int")
                        .HasColumnName("passagem_idPassagem");

                    b.Property<int>("PassageiroIdPassageiro")
                        .HasColumnType("int")
                        .HasColumnName("passageiro_idPassageiro");

                    b.Property<sbyte?>("BilheteInternacional")
                        .HasColumnType("tinyint");

                    b.Property<string>("StatusPassageiro")
                        .IsRequired()
                        .HasColumnType("enum('Passagem adquirida','Passagem cancelada','Check-in realizado','Embarque realizado','NO SHOW')")
                        .HasColumnName("statusPassageiro");

                    b.HasKey("PassagemIdPassagem", "PassageiroIdPassageiro")
                        .HasName("PRIMARY")
                        .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                    b.HasIndex(new[] { "PassageiroIdPassageiro" }, "fk_bilhete_passageiro1_idx");

                    b.HasIndex(new[] { "PassagemIdPassagem" }, "fk_bilhete_passagem1_idx");

                    b.ToTable("bilhete", (string)null);
                });

            modelBuilder.Entity("FlyNow.EfModels.CompanhiaAerea", b =>
                {
                    b.Property<int>("Cod")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("cod");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Cod"));

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("varchar(14)")
                        .HasColumnName("cnpj");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("nome");

                    b.Property<string>("RazaoSocial")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)")
                        .HasColumnName("razaoSocial");

                    b.Property<double?>("TaxaRemuneracao")
                        .HasColumnType("double")
                        .HasColumnName("taxaRemuneracao");

                    b.HasKey("Cod")
                        .HasName("PRIMARY");

                    b.ToTable("companhiaaerea", (string)null);
                });

            modelBuilder.Entity("FlyNow.EfModels.CompanhiaaereaHasAgencium", b =>
                {
                    b.Property<int>("AgenciaIdAgencia")
                        .HasColumnType("int")
                        .HasColumnName("agencia_id_agencia");

                    b.Property<int>("CompanhiaaereaCod")
                        .HasColumnType("int")
                        .HasColumnName("companhiaaerea_cod");

                    b.HasKey("AgenciaIdAgencia", "CompanhiaaereaCod")
                        .HasName("PRIMARY")
                        .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                    b.HasIndex(new[] { "AgenciaIdAgencia" }, "fk_companhiaaerea_has_agencia_agencia1_idx");

                    b.HasIndex(new[] { "CompanhiaaereaCod" }, "fk_companhiaaerea_has_agencia_companhiaaerea1_idx");

                    b.ToTable("companhiaaerea_has_agencia", (string)null);
                });

            modelBuilder.Entity("FlyNow.EfModels.Funcionario", b =>
                {
                    b.Property<int>("IdFuncionario")
                        .HasColumnType("int")
                        .HasColumnName("idFuncionario");

                    b.Property<string>("Cpf")
                        .HasMaxLength(11)
                        .HasColumnType("varchar(11)")
                        .HasColumnName("cpf");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("email");

                    b.Property<string>("Nome")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("nome");

                    b.HasKey("IdFuncionario")
                        .HasName("PRIMARY");

                    b.ToTable("funcionario", (string)null);
                });

            modelBuilder.Entity("FlyNow.EfModels.Passageiro", b =>
                {
                    b.Property<int>("IdPassageiro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idPassageiro");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdPassageiro"));

                    b.Property<string>("Cpf")
                        .HasMaxLength(11)
                        .HasColumnType("varchar(11)")
                        .HasColumnName("cpf");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("nome");

                    b.Property<string>("Rg")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("rg");

                    b.Property<int>("UsuarioIdUsuario")
                        .HasColumnType("int")
                        .HasColumnName("usuario_id_usuario");

                    b.HasKey("IdPassageiro")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "UsuarioIdUsuario" }, "fk_viajante_usuario1_idx");

                    b.HasIndex(new[] { "Rg" }, "rg_UNIQUE")
                        .IsUnique();

                    b.ToTable("passageiro", (string)null);
                });

            modelBuilder.Entity("FlyNow.EfModels.PassageiroVip", b =>
                {
                    b.Property<int>("CompanhiaaereaCod")
                        .HasColumnType("int")
                        .HasColumnName("companhiaaerea_cod");

                    b.Property<int>("PassageiroIdPassageiro")
                        .HasColumnType("int")
                        .HasColumnName("passageiro_idPassageiro");

                    b.HasKey("CompanhiaaereaCod", "PassageiroIdPassageiro");

                    b.HasIndex("PassageiroIdPassageiro");

                    b.ToTable("passageiroVIP", "sistema_aeroporto");
                });

            modelBuilder.Entity("FlyNow.EfModels.Passagem", b =>
                {
                    b.Property<int>("IdPassagem")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idPassagem");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdPassagem"));

                    b.Property<int>("IdVoo1")
                        .HasColumnType("int")
                        .HasColumnName("idVoo1");

                    b.Property<int?>("IdVoo2")
                        .HasColumnType("int")
                        .HasColumnName("idVoo2");

                    b.Property<string>("Moeda")
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)")
                        .HasColumnName("moeda");

                    b.Property<int>("TarifaIdTarifa")
                        .HasColumnType("int")
                        .HasColumnName("tarifa_idTarifa");

                    b.Property<int>("ValorbagagemId")
                        .HasColumnType("int")
                        .HasColumnName("valorbagagem_id");

                    b.HasKey("IdPassagem")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "TarifaIdTarifa" }, "fk_passagem_tarifa1_idx");

                    b.HasIndex(new[] { "ValorbagagemId" }, "fk_passagem_valorbagagem1_idx");

                    b.HasIndex(new[] { "IdVoo1" }, "fk_passagem_voo1_idx")
                        .IsUnique();

                    b.HasIndex(new[] { "IdVoo2" }, "fk_passagem_voo2_idx");

                    b.ToTable("passagem", (string)null);
                });

            modelBuilder.Entity("FlyNow.EfModels.Tarifa", b =>
                {
                    b.Property<int>("IdTarifa")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idTarifa");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdTarifa"));

                    b.Property<int>("CompanhiaaereaCod")
                        .HasColumnType("int")
                        .HasColumnName("companhiaaerea_cod");

                    b.Property<double?>("Valor")
                        .HasColumnType("double")
                        .HasColumnName("valor");

                    b.HasKey("IdTarifa")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "CompanhiaaereaCod" }, "fk_tarifa_companhiaaerea1_idx");

                    b.ToTable("tarifa", (string)null);
                });

            modelBuilder.Entity("FlyNow.EfModels.Usuario", b =>
                {
                    b.Property<int>("IdUsuario")
                        .HasColumnType("int")
                        .HasColumnName("id_usuario");

                    b.Property<int>("FuncionarioIdFuncionario")
                        .HasColumnType("int")
                        .HasColumnName("funcionario_idFuncionario");

                    b.Property<string>("Login")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("login");

                    b.Property<string>("Senha")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("senha");

                    b.HasKey("IdUsuario")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "FuncionarioIdFuncionario" }, "fk_usuario_funcionario1_idx");

                    b.ToTable("usuario", (string)null);
                });

            modelBuilder.Entity("FlyNow.EfModels.Valorbagagem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CompanhiaaereaCod")
                        .HasColumnType("int")
                        .HasColumnName("companhiaaerea_cod");

                    b.Property<double?>("ValorBagagemAdicional")
                        .HasColumnType("double")
                        .HasColumnName("valorBagagemAdicional");

                    b.Property<double?>("ValorPrimeiraBagagem")
                        .HasColumnType("double")
                        .HasColumnName("valorPrimeiraBagagem");

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "CompanhiaaereaCod" }, "companhiaaerea_cod_UNIQUE")
                        .IsUnique();

                    b.ToTable("valorbagagem", (string)null);
                });

            modelBuilder.Entity("FlyNow.EfModels.Voo", b =>
                {
                    b.Property<int>("IdVoo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_voo");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdVoo"));

                    b.Property<int>("AeronaveIdAeronave")
                        .HasColumnType("int")
                        .HasColumnName("aeronave_id_aeronave");

                    b.Property<string>("CodVoo")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("codVoo");

                    b.Property<int>("CompanhiaaereaCod")
                        .HasColumnType("int")
                        .HasColumnName("companhiaaerea_cod");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime")
                        .HasColumnName("data");

                    b.Property<TimeOnly?>("Duracao")
                        .HasColumnType("time")
                        .HasColumnName("duracao");

                    b.Property<sbyte?>("EhInternacional")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasColumnName("ehInternacional")
                        .HasDefaultValueSql("'0'");

                    b.Property<int>("IdAeroportoDestino")
                        .HasColumnType("int")
                        .HasColumnName("idAeroportoDestino");

                    b.Property<int>("IdAeroportoOrigem")
                        .HasColumnType("int")
                        .HasColumnName("idAeroportoOrigem");

                    b.HasKey("IdVoo")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "AeronaveIdAeronave" }, "fk_voo_aeronave1_idx");

                    b.HasIndex(new[] { "IdAeroportoOrigem" }, "fk_voo_aeroporto1_idx");

                    b.HasIndex(new[] { "IdAeroportoDestino" }, "fk_voo_aeroporto2_idx");

                    b.HasIndex(new[] { "CompanhiaaereaCod" }, "fk_voo_companhiaaerea1_idx");

                    b.ToTable("voo", (string)null);
                });

            modelBuilder.Entity("FlyNow.EfModels.Agencium", b =>
                {
                    b.HasOne("FlyNow.EfModels.Funcionario", "FuncionarioIdFuncionarioNavigation")
                        .WithMany("Agencia")
                        .HasForeignKey("FuncionarioIdFuncionario")
                        .IsRequired()
                        .HasConstraintName("fk_agencia_funcionario1");

                    b.Navigation("FuncionarioIdFuncionarioNavigation");
                });

            modelBuilder.Entity("FlyNow.EfModels.Assento", b =>
                {
                    b.HasOne("FlyNow.EfModels.Aeronave", "AeronaveIdAeronaveNavigation")
                        .WithMany("Assentos")
                        .HasForeignKey("AeronaveIdAeronave")
                        .IsRequired()
                        .HasConstraintName("fk_assento_aeronave1");

                    b.Navigation("AeronaveIdAeronaveNavigation");
                });

            modelBuilder.Entity("FlyNow.EfModels.Bilhete", b =>
                {
                    b.HasOne("FlyNow.EfModels.Passageiro", "PassageiroIdPassageiroNavigation")
                        .WithMany("Bilhetes")
                        .HasForeignKey("PassageiroIdPassageiro")
                        .IsRequired()
                        .HasConstraintName("fk_bilhete_passageiro1");

                    b.HasOne("FlyNow.EfModels.Passagem", "PassagemIdPassagemNavigation")
                        .WithMany("Bilhetes")
                        .HasForeignKey("PassagemIdPassagem")
                        .IsRequired()
                        .HasConstraintName("fk_bilhete_passagem1");

                    b.Navigation("PassageiroIdPassageiroNavigation");

                    b.Navigation("PassagemIdPassagemNavigation");
                });

            modelBuilder.Entity("FlyNow.EfModels.CompanhiaaereaHasAgencium", b =>
                {
                    b.HasOne("FlyNow.EfModels.CompanhiaAerea", "CompanhiaaereaCodNavigation")
                        .WithMany("CompanhiaaereaHasAgencia")
                        .HasForeignKey("CompanhiaaereaCod")
                        .IsRequired()
                        .HasConstraintName("fk_companhiaaerea_has_agencia_companhiaaerea1");

                    b.Navigation("CompanhiaaereaCodNavigation");
                });

            modelBuilder.Entity("FlyNow.EfModels.Passageiro", b =>
                {
                    b.HasOne("FlyNow.EfModels.Usuario", "UsuarioIdUsuarioNavigation")
                        .WithMany("Passageiros")
                        .HasForeignKey("UsuarioIdUsuario")
                        .IsRequired()
                        .HasConstraintName("fk_viajante_usuario1");

                    b.Navigation("UsuarioIdUsuarioNavigation");
                });

            modelBuilder.Entity("FlyNow.EfModels.PassageiroVip", b =>
                {
                    b.HasOne("FlyNow.EfModels.CompanhiaAerea", "CompanhiaaereaCodNavigation")
                        .WithMany()
                        .HasForeignKey("CompanhiaaereaCod")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FlyNow.EfModels.Passageiro", "PassageiroIdPassageiroNavigation")
                        .WithMany()
                        .HasForeignKey("PassageiroIdPassageiro")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CompanhiaaereaCodNavigation");

                    b.Navigation("PassageiroIdPassageiroNavigation");
                });

            modelBuilder.Entity("FlyNow.EfModels.Passagem", b =>
                {
                    b.HasOne("FlyNow.EfModels.Voo", "IdVoo1Navigation")
                        .WithOne("PassagemIdVoo1Navigation")
                        .HasForeignKey("FlyNow.EfModels.Passagem", "IdVoo1")
                        .IsRequired()
                        .HasConstraintName("fk_passagem_voo1");

                    b.HasOne("FlyNow.EfModels.Voo", "IdVoo2Navigation")
                        .WithMany("PassagemIdVoo2Navigations")
                        .HasForeignKey("IdVoo2")
                        .HasConstraintName("fk_passagem_voo2");

                    b.HasOne("FlyNow.EfModels.Tarifa", "TarifaIdTarifaNavigation")
                        .WithMany("Passagems")
                        .HasForeignKey("TarifaIdTarifa")
                        .IsRequired()
                        .HasConstraintName("fk_passagem_tarifa1");

                    b.HasOne("FlyNow.EfModels.Valorbagagem", "Valorbagagem")
                        .WithMany("Passagems")
                        .HasForeignKey("ValorbagagemId")
                        .IsRequired()
                        .HasConstraintName("fk_passagem_valorbagagem1");

                    b.Navigation("IdVoo1Navigation");

                    b.Navigation("IdVoo2Navigation");

                    b.Navigation("TarifaIdTarifaNavigation");

                    b.Navigation("Valorbagagem");
                });

            modelBuilder.Entity("FlyNow.EfModels.Tarifa", b =>
                {
                    b.HasOne("FlyNow.EfModels.CompanhiaAerea", "CompanhiaaereaCodNavigation")
                        .WithMany("Tarifas")
                        .HasForeignKey("CompanhiaaereaCod")
                        .IsRequired()
                        .HasConstraintName("fk_tarifa_companhiaaerea1");

                    b.Navigation("CompanhiaaereaCodNavigation");
                });

            modelBuilder.Entity("FlyNow.EfModels.Usuario", b =>
                {
                    b.HasOne("FlyNow.EfModels.Funcionario", "FuncionarioIdFuncionarioNavigation")
                        .WithMany("Usuarios")
                        .HasForeignKey("FuncionarioIdFuncionario")
                        .IsRequired()
                        .HasConstraintName("fk_usuario_funcionario1");

                    b.Navigation("FuncionarioIdFuncionarioNavigation");
                });

            modelBuilder.Entity("FlyNow.EfModels.Valorbagagem", b =>
                {
                    b.HasOne("FlyNow.EfModels.CompanhiaAerea", "CompanhiaaereaCodNavigation")
                        .WithOne("Valorbagagem")
                        .HasForeignKey("FlyNow.EfModels.Valorbagagem", "CompanhiaaereaCod")
                        .IsRequired()
                        .HasConstraintName("fk_valorbagagem_companhiaaerea1");

                    b.Navigation("CompanhiaaereaCodNavigation");
                });

            modelBuilder.Entity("FlyNow.EfModels.Voo", b =>
                {
                    b.HasOne("FlyNow.EfModels.Aeronave", "AeronaveIdAeronaveNavigation")
                        .WithMany("Voos")
                        .HasForeignKey("AeronaveIdAeronave")
                        .IsRequired()
                        .HasConstraintName("fk_voo_aeronave1");

                    b.HasOne("FlyNow.EfModels.CompanhiaAerea", "CompanhiaaereaCodNavigation")
                        .WithMany("Voos")
                        .HasForeignKey("CompanhiaaereaCod")
                        .IsRequired()
                        .HasConstraintName("fk_voo_companhiaaerea1");

                    b.HasOne("FlyNow.EfModels.Aeroporto", "IdAeroportoDestinoNavigation")
                        .WithMany("VooIdAeroportoDestinoNavigations")
                        .HasForeignKey("IdAeroportoDestino")
                        .IsRequired()
                        .HasConstraintName("fk_voo_aeroporto2");

                    b.HasOne("FlyNow.EfModels.Aeroporto", "IdAeroportoOrigemNavigation")
                        .WithMany("VooIdAeroportoOrigemNavigations")
                        .HasForeignKey("IdAeroportoOrigem")
                        .IsRequired()
                        .HasConstraintName("fk_voo_aeroporto1");

                    b.Navigation("AeronaveIdAeronaveNavigation");

                    b.Navigation("CompanhiaaereaCodNavigation");

                    b.Navigation("IdAeroportoDestinoNavigation");

                    b.Navigation("IdAeroportoOrigemNavigation");
                });

            modelBuilder.Entity("FlyNow.EfModels.Aeronave", b =>
                {
                    b.Navigation("Assentos");

                    b.Navigation("Voos");
                });

            modelBuilder.Entity("FlyNow.EfModels.Aeroporto", b =>
                {
                    b.Navigation("VooIdAeroportoDestinoNavigations");

                    b.Navigation("VooIdAeroportoOrigemNavigations");
                });

            modelBuilder.Entity("FlyNow.EfModels.CompanhiaAerea", b =>
                {
                    b.Navigation("CompanhiaaereaHasAgencia");

                    b.Navigation("Tarifas");

                    b.Navigation("Valorbagagem");

                    b.Navigation("Voos");
                });

            modelBuilder.Entity("FlyNow.EfModels.Funcionario", b =>
                {
                    b.Navigation("Agencia");

                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("FlyNow.EfModels.Passageiro", b =>
                {
                    b.Navigation("Bilhetes");
                });

            modelBuilder.Entity("FlyNow.EfModels.Passagem", b =>
                {
                    b.Navigation("Bilhetes");
                });

            modelBuilder.Entity("FlyNow.EfModels.Tarifa", b =>
                {
                    b.Navigation("Passagems");
                });

            modelBuilder.Entity("FlyNow.EfModels.Usuario", b =>
                {
                    b.Navigation("Passageiros");
                });

            modelBuilder.Entity("FlyNow.EfModels.Valorbagagem", b =>
                {
                    b.Navigation("Passagems");
                });

            modelBuilder.Entity("FlyNow.EfModels.Voo", b =>
                {
                    b.Navigation("PassagemIdVoo1Navigation");

                    b.Navigation("PassagemIdVoo2Navigations");
                });
#pragma warning restore 612, 618
        }
    }
}
