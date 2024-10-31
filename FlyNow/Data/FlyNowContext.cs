using System;
using System.Collections.Generic;
using FlyNow.EfModels;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace FlyNow.Data;

public partial class FlyNowContext : DbContext
{
	public FlyNowContext()
	{
	}

	public FlyNowContext(DbContextOptions<FlyNowContext> options)
			: base(options)
	{
	}

	public virtual DbSet<Aeroporto> Aeroportos { get; set; }

	public virtual DbSet<Agencia> Agencia { get; set; }

	public virtual DbSet<Bilhete> Bilhetes { get; set; }

	public virtual DbSet<CompanhiaAerea> Companhiaaereas { get; set; }

	public virtual DbSet<CompanhiaAereaHasAgencia> CompanhiaaereaHasAgencia { get; set; }

	public virtual DbSet<Funcionario> Funcionarios { get; set; }

	public virtual DbSet<Passagem> Passagems { get; set; }

	public virtual DbSet<Tarifa> Tarifas { get; set; }

	public virtual DbSet<Usuario> Usuarios { get; set; }

	public virtual DbSet<Valorbagagem> Valorbagagems { get; set; }

	public virtual DbSet<Viajante> Viajantes { get; set; }

	public virtual DbSet<Voo> Voos { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder
				.UseCollation("utf8mb4_0900_ai_ci")
				.HasCharSet("utf8mb4");

		modelBuilder.Entity<Aeroporto>(entity =>
		{
			entity.HasKey(e => e.IdAeroporto).HasName("PRIMARY");

			entity.ToTable("aeroporto");

			entity.Property(e => e.IdAeroporto)
							.ValueGeneratedNever()
							.HasColumnName("id_aeroporto");
			entity.Property(e => e.Cidade)
							.HasMaxLength(100)
							.HasColumnName("cidade");
			entity.Property(e => e.Nome)
							.HasMaxLength(100)
							.HasColumnName("nome");
			entity.Property(e => e.Sigla)
							.HasMaxLength(10)
							.HasColumnName("sigla");
			entity.Property(e => e.Uf)
							.HasMaxLength(2)
							.HasColumnName("uf");

			entity.HasMany(d => d.VooIdVoos).WithMany(p => p.AeroportoIdAeroportos)
							.UsingEntity<Dictionary<string, object>>(
									"AeroportoHasVoo",
									r => r.HasOne<Voo>().WithMany()
											.HasForeignKey("VooIdVoo")
											.OnDelete(DeleteBehavior.ClientSetNull)
											.HasConstraintName("fk_aeroporto_has_voo_voo1"),
									l => l.HasOne<Aeroporto>().WithMany()
											.HasForeignKey("AeroportoIdAeroporto")
											.HasConstraintName("fk_aeroporto_has_voo_aeroporto1"),
									j =>
									{
									j.HasKey("AeroportoIdAeroporto", "VooIdVoo")
													.HasName("PRIMARY")
													.HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
									j.ToTable("aeroporto_has_voo");
									j.HasIndex(new[] { "AeroportoIdAeroporto" }, "fk_aeroporto_has_voo_aeroporto1_idx");
									j.HasIndex(new[] { "VooIdVoo" }, "fk_aeroporto_has_voo_voo1_idx");
									j.IndexerProperty<int>("AeroportoIdAeroporto").HasColumnName("aeroporto_id_aeroporto");
									j.IndexerProperty<int>("VooIdVoo").HasColumnName("voo_id_voo");
								});
		});

		modelBuilder.Entity<Agencia>(entity =>
		{
			entity.HasKey(e => new { e.IdAgencia, e.FuncionarioIdFuncionario })
							.HasName("PRIMARY")
							.HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

			entity.ToTable("agencia");

			entity.HasIndex(e => e.FuncionarioIdFuncionario, "fk_agencia_funcionario1_idx");

			entity.Property(e => e.IdAgencia).HasColumnName("id_agencia");
			entity.Property(e => e.FuncionarioIdFuncionario).HasColumnName("funcionario_id-funcionario");
			entity.Property(e => e.Nome)
							.HasMaxLength(100)
							.HasColumnName("nome");
			entity.Property(e => e.TaxaAgencia).HasColumnName("taxaAgencia");

			entity.HasOne(d => d.FuncionarioIdFuncionarioNavigation).WithMany(p => p.Agencia)
							.HasForeignKey(d => d.FuncionarioIdFuncionario)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fk_agencia_funcionario1");
		});

		modelBuilder.Entity<Bilhete>(entity =>
		{
			entity.HasKey(e => e.IdBilhete).HasName("PRIMARY");

			entity.ToTable("bilhete");

			entity.Property(e => e.IdBilhete).HasColumnName("idBilhete");
		});

		modelBuilder.Entity<CompanhiaAerea>(entity =>
		{
			entity.HasKey(e => e.Cod).HasName("PRIMARY");

			entity.ToTable("companhiaaerea");

			entity.Property(e => e.Cod).HasColumnName("cod");
			entity.Property(e => e.Cnpj)
							.HasMaxLength(14)
							.HasColumnName("cnpj");
			entity.Property(e => e.Nome)
							.HasMaxLength(100)
							.HasColumnName("nome");
			entity.Property(e => e.RazaoSocial)
							.HasMaxLength(150)
							.HasColumnName("razaoSocial");
			entity.Property(e => e.TaxaRemuneracao).HasColumnName("taxaRemuneracao");
			entity.Property(e => e.TipoVoo)
							.HasDefaultValueSql("'Domestico'")
							.HasColumnType("enum('Domestico','Internacional')")
							.HasColumnName("tipoVoo");
		});

		modelBuilder.Entity<CompanhiaAereaHasAgencia>(entity =>
		{
			entity.HasKey(e => e.CompanhiaaereaCod).HasName("PRIMARY");

			entity.ToTable("companhiaaerea_has_agencia");

			entity.HasIndex(e => new { e.AgenciaIdAgencia, e.AgenciaFuncionarioIdFuncionario }, "fk_companhiaaerea_has_agencia_agencia1_idx");

			entity.HasIndex(e => e.CompanhiaaereaCod, "fk_companhiaaerea_has_agencia_companhiaaerea1_idx");

			entity.Property(e => e.CompanhiaaereaCod).HasColumnName("companhiaaerea_cod");
			entity.Property(e => e.AgenciaFuncionarioIdFuncionario).HasColumnName("agencia_funcionario_id-funcionario");
			entity.Property(e => e.AgenciaIdAgencia).HasColumnName("agencia_id_agencia");

			entity.HasOne(d => d.CompanhiaaereaCodNavigation).WithOne(p => p.CompanhiaaereaHasAgencium)
							.HasForeignKey<CompanhiaAereaHasAgencia>(d => d.CompanhiaaereaCod)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fk_companhiaaerea_has_agencia_companhiaaerea1");

			entity.HasOne(d => d.Agencium).WithMany(p => p.CompanhiaaereaHasAgencia)
							.HasForeignKey(d => new { d.AgenciaIdAgencia, d.AgenciaFuncionarioIdFuncionario })
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fk_companhiaaerea_has_agencia_agencia1");
		});

		modelBuilder.Entity<Funcionario>(entity =>
		{
			entity.HasKey(e => e.IdFuncionario).HasName("PRIMARY");

			entity.ToTable("funcionario");

			entity.HasIndex(e => e.Cpf, "unique_cpf").IsUnique();

			entity.Property(e => e.IdFuncionario)
							.ValueGeneratedNever()
							.HasColumnName("id-funcionario");
			entity.Property(e => e.Cpf)
							.HasMaxLength(11)
							.HasColumnName("cpf");
			entity.Property(e => e.Email)
							.HasMaxLength(100)
							.HasColumnName("email");
			entity.Property(e => e.Nome)
							.HasMaxLength(100)
							.HasColumnName("nome");
		});

		modelBuilder.Entity<Passagem>(entity =>
		{
			entity.HasKey(e => e.IdPassagem).HasName("PRIMARY");

			entity.ToTable("passagem");

			entity.HasIndex(e => e.BilheteIdBilhete, "fk_passagem_bilhete1_idx");

			entity.HasIndex(e => e.ValorbagagemId, "fk_passagem_valorbagagem1_idx");

			entity.Property(e => e.IdPassagem).HasColumnName("idPassagem");
			entity.Property(e => e.BilheteIdBilhete).HasColumnName("bilhete_idBilhete");
			entity.Property(e => e.CompanhiaAerea)
							.HasMaxLength(45)
							.HasColumnName("companhia_aerea");
			entity.Property(e => e.Moeda)
							.HasMaxLength(3)
							.HasColumnName("moeda");
			entity.Property(e => e.Tarifa)
							.HasMaxLength(45)
							.HasColumnName("tarifa");
			entity.Property(e => e.ValorbagagemId).HasColumnName("valorbagagem_id");

			entity.HasOne(d => d.BilheteIdBilheteNavigation).WithMany(p => p.Passagems)
							.HasForeignKey(d => d.BilheteIdBilhete)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fk_passagem_bilhete1");

			entity.HasOne(d => d.Valorbagagem).WithMany(p => p.Passagems)
							.HasForeignKey(d => d.ValorbagagemId)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fk_passagem_valorbagagem1");
		});

		modelBuilder.Entity<Tarifa>(entity =>
		{
			entity.HasKey(e => new { e.IdTarifa, e.PassagemIdPassagem })
							.HasName("PRIMARY")
							.HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

			entity.ToTable("tarifa");

			entity.HasIndex(e => e.CompanhiaaereaCod, "fk_tarifa_companhiaaerea1_idx");

			entity.HasIndex(e => e.PassagemIdPassagem, "fk_tarifa_passagem1_idx");

			entity.Property(e => e.IdTarifa)
							.ValueGeneratedOnAdd()
							.HasColumnName("idTarifa");
			entity.Property(e => e.PassagemIdPassagem).HasColumnName("passagem_idPassagem");
			entity.Property(e => e.CompanhiaaereaCod).HasColumnName("companhiaaerea_cod");
			entity.Property(e => e.Valor).HasColumnName("valor");

			entity.HasOne(d => d.CompanhiaaereaCodNavigation).WithMany(p => p.Tarifas)
							.HasForeignKey(d => d.CompanhiaaereaCod)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fk_tarifa_companhiaaerea1");

			entity.HasOne(d => d.PassagemIdPassagemNavigation).WithMany(p => p.Tarifas)
							.HasForeignKey(d => d.PassagemIdPassagem)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fk_tarifa_passagem1");
		});

		modelBuilder.Entity<Usuario>(entity =>
		{
			entity.HasKey(e => e.IdUsuario).HasName("PRIMARY");

			entity.ToTable("usuario");

			entity.HasIndex(e => e.FuncionarioIdFuncionario, "fk_usuario_funcionario1_idx");

			entity.HasIndex(e => e.Login, "unique_login").IsUnique();

			entity.Property(e => e.IdUsuario)
							.ValueGeneratedNever()
							.HasColumnName("id_usuario");
			entity.Property(e => e.FuncionarioIdFuncionario).HasColumnName("funcionario_id-funcionario");
			entity.Property(e => e.Login)
							.HasMaxLength(50)
							.HasColumnName("login");
			entity.Property(e => e.Senha)
							.HasMaxLength(100)
							.HasColumnName("senha");

			entity.HasOne(d => d.FuncionarioIdFuncionarioNavigation).WithMany(p => p.Usuarios)
							.HasForeignKey(d => d.FuncionarioIdFuncionario)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fk_usuario_funcionario1");
		});

		modelBuilder.Entity<Valorbagagem>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("PRIMARY");

			entity.ToTable("valorbagagem");

			entity.HasIndex(e => e.CompanhiaaereaCod, "fk_valorbagagem_companhiaaerea1_idx");

			entity.Property(e => e.Id).HasColumnName("id");
			entity.Property(e => e.CompanhiaaereaCod).HasColumnName("companhiaaerea_cod");
			entity.Property(e => e.ValorBagagemAdicional).HasColumnName("valorBagagemAdicional");
			entity.Property(e => e.ValorPrimeiraBagagem).HasColumnName("valorPrimeiraBagagem");

			entity.HasOne(d => d.CompanhiaaereaCodNavigation).WithMany(p => p.Valorbagagems)
							.HasForeignKey(d => d.CompanhiaaereaCod)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fk_valorbagagem_companhiaaerea1");
		});

		modelBuilder.Entity<Viajante>(entity =>
		{
			entity.HasKey(e => e.IdViajante).HasName("PRIMARY");

			entity.ToTable("viajante");

			entity.HasIndex(e => e.BilheteIdBilhete, "bilhete_idBilhete_UNIQUE").IsUnique();

			entity.HasIndex(e => e.UsuarioIdUsuario, "fk_viajante_usuario1_idx");

			entity.HasIndex(e => e.Rg, "rg_UNIQUE").IsUnique();

			entity.Property(e => e.IdViajante).HasColumnName("idViajante");
			entity.Property(e => e.BilheteIdBilhete).HasColumnName("bilhete_idBilhete");
			entity.Property(e => e.Cpf)
							.HasMaxLength(11)
							.HasColumnName("cpf");
			entity.Property(e => e.Nome)
							.HasMaxLength(100)
							.HasColumnName("nome");
			entity.Property(e => e.Rg)
							.HasMaxLength(20)
							.HasColumnName("rg");
			entity.Property(e => e.UsuarioIdUsuario).HasColumnName("usuario_id_usuario");

			entity.HasOne(d => d.BilheteIdBilheteNavigation).WithOne(p => p.Viajante)
							.HasForeignKey<Viajante>(d => d.BilheteIdBilhete)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fk_viajante_bilhete1");

			entity.HasOne(d => d.UsuarioIdUsuarioNavigation).WithMany(p => p.Viajantes)
							.HasForeignKey(d => d.UsuarioIdUsuario)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fk_viajante_usuario1");
		});

		modelBuilder.Entity<Voo>(entity =>
		{
			entity.HasKey(e => e.IdVoo).HasName("PRIMARY");

			entity.ToTable("voo");

			entity.HasIndex(e => e.CompanhiaaereaCod, "fk_voo_companhiaaerea1_idx");

			entity.Property(e => e.IdVoo)
							.ValueGeneratedNever()
							.HasColumnName("id_voo");
			entity.Property(e => e.CodVoo)
							.HasMaxLength(10)
							.HasColumnName("codVoo");
			entity.Property(e => e.CompanhiaaereaCod).HasColumnName("companhiaaerea_cod");
			entity.Property(e => e.Data)
							.HasColumnType("datetime")
							.HasColumnName("data");
			entity.Property(e => e.Destino)
							.HasMaxLength(45)
							.HasColumnName("destino");
			entity.Property(e => e.Origem)
							.HasMaxLength(45)
							.HasColumnName("origem");

			entity.HasOne(d => d.CompanhiaaereaCodNavigation).WithMany(p => p.Voos)
							.HasForeignKey(d => d.CompanhiaaereaCod)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fk_voo_companhiaaerea1");

			entity.HasMany(d => d.PassagemIdPassagems).WithMany(p => p.VooIdVoos)
							.UsingEntity<Dictionary<string, object>>(
									"VooHasPassagem",
									r => r.HasOne<Passagem>().WithMany()
											.HasForeignKey("PassagemIdPassagem")
											.OnDelete(DeleteBehavior.ClientSetNull)
											.HasConstraintName("fk_voo_has_passagem_passagem1"),
									l => l.HasOne<Voo>().WithMany()
											.HasForeignKey("VooIdVoo")
											.OnDelete(DeleteBehavior.ClientSetNull)
											.HasConstraintName("fk_voo_has_passagem_voo1"),
									j =>
									{
									j.HasKey("VooIdVoo", "PassagemIdPassagem")
													.HasName("PRIMARY")
													.HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
									j.ToTable("voo_has_passagem");
									j.HasIndex(new[] { "PassagemIdPassagem" }, "fk_voo_has_passagem_passagem1_idx");
									j.HasIndex(new[] { "VooIdVoo" }, "fk_voo_has_passagem_voo1_idx");
									j.IndexerProperty<int>("VooIdVoo").HasColumnName("voo_id_voo");
									j.IndexerProperty<int>("PassagemIdPassagem").HasColumnName("passagem_idPassagem");
								});
		});

		OnModelCreatingPartial(modelBuilder);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
