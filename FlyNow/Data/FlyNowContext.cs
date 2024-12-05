using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using FlyNow.Controllers;
using FlyNow.EfModels;
using Microsoft.EntityFrameworkCore;

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

	public virtual DbSet<Aeronave> Aeronaves { get; set; }

	public virtual DbSet<Aeroporto> Aeroportos { get; set; }

	public virtual DbSet<Agencia> Agencia { get; set; }

	public virtual DbSet<Assento> Assentos { get; set; }

	public virtual DbSet<Bilhete> Bilhetes { get; set; }

	public virtual DbSet<CompanhiaAerea> Companhiaaereas { get; set; }

	public virtual DbSet<CompanhiaaereaHasAgencia> CompanhiaaereaHasAgencia { get; set; }

	public virtual DbSet<Funcionario> Funcionarios { get; set; }

	public virtual DbSet<Passageiro> Passageiros { get; set; }

	public virtual DbSet<Passagem> Passagems { get; set; }

	public virtual DbSet<Tarifa> Tarifas { get; set; }

	public virtual DbSet<Usuario> Usuarios { get; set; }

	public virtual DbSet<Valorbagagem> Valorbagagems { get; set; }

	public virtual DbSet<Voo> Voos { get; set; }

	public virtual DbSet<PassageiroVip> PassageiroVIPs { get; set; }


	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
			=> optionsBuilder.UseMySql("server=localhost;port=3306;database=sistema_aeroporto;uid=root;pwd=Felicidade281003!", ServerVersion.Parse("8.0.28-mysql"));

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Bilhete>()
				.Property(b => b.StatusPassageiro)
				.HasConversion(
						v => GetEnumValue(v),  
						v => MapStringToEnum(v.Trim())
				);

		base.OnModelCreating(modelBuilder);


		modelBuilder.Entity<PassageiroVip>(entity =>
		{
			entity.ToTable("passageiroVIP", "sistema_aeroporto");

			// Definir a chave composta
			entity.HasKey(pv => new { pv.CompanhiaaereaCod, pv.PassageiroIdPassageiro });

			// Mapear as propriedades para as colunas exatas no banco de dados
			entity.Property(pv => pv.CompanhiaaereaCod).HasColumnName("companhiaaerea_cod");
			entity.Property(pv => pv.PassageiroIdPassageiro).HasColumnName("passageiro_idPassageiro");
		});



		// Outras configurações para relacionamentos, caso necessário
		base.OnModelCreating(modelBuilder);
		modelBuilder
				.UseCollation("utf8mb4_0900_ai_ci")
				.HasCharSet("utf8mb4");

		modelBuilder.Entity<Aeronave>(entity =>
		{
			entity.HasKey(e => e.IdAeronave).HasName("PRIMARY");

			entity.ToTable("aeronave");

			entity.HasIndex(e => e.CapacidadeBagagens, "capacidadeBagagens_UNIQUE").IsUnique();

			entity.HasIndex(e => e.CapacidadePassageiros, "capacidadePassageiros_UNIQUE").IsUnique();

			entity.Property(e => e.IdAeronave).HasColumnName("id_aeronave");
			entity.Property(e => e.CapacidadeBagagens).HasColumnName("capacidadeBagagens");
			entity.Property(e => e.CapacidadePassageiros).HasColumnName("capacidadePassageiros");
		});

		modelBuilder.Entity<Aeroporto>(entity =>
		{
			entity.HasKey(e => e.IdAeroporto).HasName("PRIMARY");

			entity.ToTable("aeroporto");

			entity.Property(e => e.IdAeroporto).HasColumnName("id_aeroporto");
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

			entity.HasOne(d => d.Funcionario).WithMany(p => p.Agencia)
							.HasForeignKey(d => d.FuncionarioIdFuncionario)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fk_agencia_funcionario1");
		});

		modelBuilder.Entity<Assento>(entity =>
		{
			entity.HasKey(e => new { e.IdAssento, e.AeronaveIdAeronave })
							.HasName("PRIMARY")
							.HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

			entity.ToTable("assento");

			entity.HasIndex(e => e.AeronaveIdAeronave, "fk_assento_aeronave1_idx");

			entity.Property(e => e.IdAssento)
							.ValueGeneratedOnAdd()
							.HasColumnName("id_assento");
			entity.Property(e => e.AeronaveIdAeronave).HasColumnName("aeronave_id_aeronave");
			entity.Property(e => e.LetraAssento)
							.HasMaxLength(1)
							.IsFixedLength()
							.HasColumnName("letraAssento");
			entity.Property(e => e.NumeroFileira).HasColumnName("numeroFileira");
			entity.Property(e => e.Ocupado)
							.HasDefaultValueSql("'0'")
							.HasColumnName("ocupado");

			entity.HasOne(d => d.Aeronave).WithMany(p => p.Assentos)
							.HasForeignKey(d => d.AeronaveIdAeronave)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fk_assento_aeronave1");
		});

		modelBuilder.Entity<Bilhete>(entity =>
		{
			entity.HasKey(e => new { e.PassagemIdPassagem, e.PassageiroIdPassageiro })
							.HasName("PRIMARY")
							.HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

			entity.ToTable("bilhete");

			entity.HasIndex(e => e.PassageiroIdPassageiro, "fk_bilhete_passageiro1_idx");

			entity.HasIndex(e => e.PassagemIdPassagem, "fk_bilhete_passagem1_idx");

			entity.Property(e => e.PassagemIdPassagem).HasColumnName("passagem_idPassagem");
			entity.Property(e => e.PassageiroIdPassageiro).HasColumnName("passageiro_idPassageiro");
			entity.Property(e => e.StatusPassageiro)
							.HasColumnType("enum('Passagem adquirida','Passagem cancelada','Check-in realizado','Embarque realizado','NO SHOW')")
							.HasColumnName("statusPassageiro");

			entity.HasOne(d => d.PassagemIdPassagemNavigation).WithMany(p => p.Bilhetes)
							.HasForeignKey(d => d.PassageiroIdPassageiro)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fk_bilhete_passageiro1");

			entity.HasOne(d => d.PassageiroIdPassageiroNavigation).WithMany(p => p.Bilhetes)
							.HasForeignKey(d => d.PassagemIdPassagem)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fk_bilhete_passagem1");
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
		});

		modelBuilder.Entity<CompanhiaaereaHasAgencia>(entity =>
		{
			entity.HasKey(e => new { e.AgenciaIdAgencia, e.CompanhiaaereaCod })
							.HasName("PRIMARY")
							.HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

			entity.ToTable("companhiaaerea_has_agencia");

			entity.HasIndex(e => e.AgenciaIdAgencia, "fk_companhiaaerea_has_agencia_agencia1_idx");

			entity.HasIndex(e => e.CompanhiaaereaCod, "fk_companhiaaerea_has_agencia_companhiaaerea1_idx");

			entity.Property(e => e.AgenciaIdAgencia).HasColumnName("agencia_id_agencia");
			entity.Property(e => e.CompanhiaaereaCod).HasColumnName("companhiaaerea_cod");

			entity.HasOne(d => d.CompanhiaaereaCodNavigation).WithMany(p => p.CompanhiaaereaHasAgencia)
							.HasForeignKey(d => d.CompanhiaaereaCod)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fk_companhiaaerea_has_agencia_companhiaaerea1");
		});

		modelBuilder.Entity<Funcionario>(entity =>
		{
			entity.HasKey(e => e.IdFuncionario).HasName("PRIMARY");

			entity.ToTable("funcionario");

			entity.Property(e => e.IdFuncionario)
							.ValueGeneratedNever()
							.HasColumnName("idFuncionario");
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

		modelBuilder.Entity<Passageiro>(entity =>
		{
			entity.HasKey(e => e.IdPassageiro).HasName("PRIMARY");

			entity.ToTable("passageiro");

			entity.HasIndex(e => e.UsuarioIdUsuario, "fk_viajante_usuario1_idx");

			entity.HasIndex(e => e.Rg, "rg_UNIQUE").IsUnique();

			entity.Property(e => e.IdPassageiro).HasColumnName("idPassageiro");
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

			entity.HasOne(d => d.UsuarioIdUsuarioNavigation).WithMany(p => p.Passageiros)
							.HasForeignKey(d => d.UsuarioIdUsuario)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fk_viajante_usuario1");
		});

		modelBuilder.Entity<Passagem>(entity =>
		{
			entity.HasKey(e => e.IdPassagem).HasName("PRIMARY");

			entity.ToTable("passagem");

			entity.HasIndex(e => e.TarifaIdTarifa, "fk_passagem_tarifa1_idx");

			entity.HasIndex(e => e.ValorbagagemId, "fk_passagem_valorbagagem1_idx");

			entity.HasIndex(e => e.IdVoo1, "fk_passagem_voo1_idx").IsUnique();

			entity.HasIndex(e => e.IdVoo2, "fk_passagem_voo2_idx");

			entity.Property(e => e.IdPassagem).HasColumnName("idPassagem");
			entity.Property(e => e.IdVoo1).HasColumnName("idVoo1");
			entity.Property(e => e.IdVoo2).HasColumnName("idVoo2");
			entity.Property(e => e.Moeda)
							.HasMaxLength(3)
							.HasColumnName("moeda");
			entity.Property(e => e.TarifaIdTarifa).HasColumnName("tarifa_idTarifa");
			entity.Property(e => e.ValorbagagemId).HasColumnName("valorbagagem_id");

			entity.HasOne(d => d.Voo1).WithOne(p => p.Passagem)
							.HasForeignKey<Passagem>(d => d.IdVoo1)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fk_passagem_voo1");

			entity.HasOne(d => d.Voo2).WithMany(p => p.PassagensVoo)
							.HasForeignKey(d => d.IdVoo2)
							.HasConstraintName("fk_passagem_voo2");

			entity.HasOne(d => d.Tarifa).WithMany(p => p.Passagems)
							.HasForeignKey(d => d.TarifaIdTarifa)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fk_passagem_tarifa1");

			entity.HasOne(d => d.Valorbagagem).WithMany(p => p.Passagems)
							.HasForeignKey(d => d.ValorbagagemId)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fk_passagem_valorbagagem1");
		});

		modelBuilder.Entity<Tarifa>(entity =>
		{
			entity.HasKey(e => e.IdTarifa).HasName("PRIMARY");

			entity.ToTable("tarifa");

			entity.HasIndex(e => e.CompanhiaaereaCod, "fk_tarifa_companhiaaerea1_idx");

			entity.Property(e => e.IdTarifa).HasColumnName("idTarifa");
			entity.Property(e => e.CompanhiaaereaCod).HasColumnName("companhiaaerea_cod");
			entity.Property(e => e.Valor).HasColumnName("valor");

			entity.HasOne(d => d.CompanhiaaereaCodNavigation).WithMany(p => p.Tarifas)
							.HasForeignKey(d => d.CompanhiaaereaCod)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fk_tarifa_companhiaaerea1");
		});

		modelBuilder.Entity<Usuario>(entity =>
		{
			entity.HasKey(e => e.IdUsuario).HasName("PRIMARY");

			entity.ToTable("usuario");

			entity.HasIndex(e => e.FuncionarioIdFuncionario, "fk_usuario_funcionario1_idx");

			entity.Property(e => e.IdUsuario)
							.ValueGeneratedNever()
							.HasColumnName("id_usuario");
			entity.Property(e => e.FuncionarioIdFuncionario).HasColumnName("funcionario_idFuncionario");
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

			entity.HasIndex(e => e.CompanhiaaereaCod, "companhiaaerea_cod_UNIQUE").IsUnique();

			entity.Property(e => e.Id).HasColumnName("id");
			entity.Property(e => e.CompanhiaaereaCod).HasColumnName("companhiaaerea_cod");
			entity.Property(e => e.ValorBagagemAdicional).HasColumnName("valorBagagemAdicional");
			entity.Property(e => e.ValorPrimeiraBagagem).HasColumnName("valorPrimeiraBagagem");

			entity.HasOne(d => d.CompanhiaaereaCodNavigation).WithOne(p => p.Valorbagagem)
							.HasForeignKey<Valorbagagem>(d => d.CompanhiaaereaCod)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fk_valorbagagem_companhiaaerea1");
		});

		modelBuilder.Entity<Voo>(entity =>
		{
			entity.HasKey(e => e.IdVoo).HasName("PRIMARY");

			entity.ToTable("voo");

			entity.HasIndex(e => e.AeronaveIdAeronave, "fk_voo_aeronave1_idx");

			entity.HasIndex(e => e.IdAeroportoOrigem, "fk_voo_aeroporto1_idx");

			entity.HasIndex(e => e.IdAeroportoDestino, "fk_voo_aeroporto2_idx");

			entity.HasIndex(e => e.CompanhiaaereaCod, "fk_voo_companhiaaerea1_idx");

			entity.Property(e => e.IdVoo).HasColumnName("id_voo");
			entity.Property(e => e.AeronaveIdAeronave).HasColumnName("aeronave_id_aeronave");
			entity.Property(e => e.CodVoo)
							.HasMaxLength(10)
							.HasColumnName("codVoo");
			entity.Property(e => e.CompanhiaaereaCod).HasColumnName("companhiaaerea_cod");
			entity.Property(e => e.Data)
							.HasColumnType("datetime")
							.HasColumnName("data");
			entity.Property(e => e.Duracao)
							.HasColumnType("time")
							.HasColumnName("duracao");
			entity.Property(e => e.EhInternacional)
							.HasDefaultValueSql("'0'")
							.HasColumnName("ehInternacional");
			entity.Property(e => e.IdAeroportoDestino).HasColumnName("idAeroportoDestino");
			entity.Property(e => e.IdAeroportoOrigem).HasColumnName("idAeroportoOrigem");

			entity.HasOne(d => d.AeronaveVoo).WithMany(p => p.Voos)
							.HasForeignKey(d => d.AeronaveIdAeronave)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fk_voo_aeronave1");

			entity.HasOne(d => d.CompanhiaaereaCodNavigation).WithMany(p => p.Voos)
							.HasForeignKey(d => d.CompanhiaaereaCod)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fk_voo_companhiaaerea1");

			entity.HasOne(d => d.IdAeroportoDestinoNavigation).WithMany(p => p.VooIdAeroportoDestinoNavigations)
							.HasForeignKey(d => d.IdAeroportoDestino)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fk_voo_aeroporto2");

			entity.HasOne(d => d.Aeroporto).WithMany(p => p.VooIdAeroportoOrigemNavigations)
							.HasForeignKey(d => d.IdAeroportoOrigem)
							.OnDelete(DeleteBehavior.ClientSetNull)
							.HasConstraintName("fk_voo_aeroporto1");
		});

		OnModelCreatingPartial(modelBuilder);
	}
	private StatusPassagem MapStringToEnum(string status)
	{
		Console.WriteLine(status);
		return status.Trim() switch
		{
			
			"Passagem adquirida" => StatusPassagem.PassagemAdquirida,
			"Passagem cancelada" => StatusPassagem.PassagemCancelada,
			"Check-in realizado" => StatusPassagem.CheckInRealizado,
			"Embarque realizado" => StatusPassagem.EmbarqueRealizado,
			"NO SHOW" => StatusPassagem.NoShow,
			_ => throw new ArgumentException($"Status desconhecido: {status}")
		};
	}
	private string GetEnumValue(StatusPassagem status)
	{
		var enumType = typeof(StatusPassagem);
		var enumName = Enum.GetName(enumType, status);
		var memberInfo = enumType.GetMember(enumName)[0];
		var enumMemberAttribute = memberInfo.GetCustomAttributes(typeof(EnumMemberAttribute), false).FirstOrDefault() as EnumMemberAttribute;
		Console.WriteLine(status);
		return enumMemberAttribute?.Value ?? status.ToString();
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
