using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlyNow.Migrations
{
    /// <inheritdoc />
    public partial class AtualizarEnumPassagem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "sistema_aeroporto");

            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "aeronave",
                columns: table => new
                {
                    id_aeronave = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    capacidadePassageiros = table.Column<int>(type: "int", nullable: false),
                    capacidadeBagagens = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_aeronave);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "aeroporto",
                columns: table => new
                {
                    id_aeroporto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    sigla = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cidade = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    uf = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_aeroporto);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "companhiaaerea",
                columns: table => new
                {
                    cod = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    razaoSocial = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cnpj = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    taxaRemuneracao = table.Column<double>(type: "double", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.cod);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "funcionario",
                columns: table => new
                {
                    idFuncionario = table.Column<int>(type: "int", nullable: false),
                    cpf = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idFuncionario);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "assento",
                columns: table => new
                {
                    id_assento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    aeronave_id_aeronave = table.Column<int>(type: "int", nullable: false),
                    numeroFileira = table.Column<int>(type: "int", nullable: true),
                    letraAssento = table.Column<string>(type: "char(1)", fixedLength: true, maxLength: 1, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ocupado = table.Column<sbyte>(type: "tinyint", nullable: true, defaultValueSql: "'0'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.id_assento, x.aeronave_id_aeronave })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    table.ForeignKey(
                        name: "fk_assento_aeronave1",
                        column: x => x.aeronave_id_aeronave,
                        principalTable: "aeronave",
                        principalColumn: "id_aeronave");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "companhiaaerea_has_agencia",
                columns: table => new
                {
                    companhiaaerea_cod = table.Column<int>(type: "int", nullable: false),
                    agencia_id_agencia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.agencia_id_agencia, x.companhiaaerea_cod })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    table.ForeignKey(
                        name: "fk_companhiaaerea_has_agencia_companhiaaerea1",
                        column: x => x.companhiaaerea_cod,
                        principalTable: "companhiaaerea",
                        principalColumn: "cod");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "tarifa",
                columns: table => new
                {
                    idTarifa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    valor = table.Column<double>(type: "double", nullable: true),
                    companhiaaerea_cod = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idTarifa);
                    table.ForeignKey(
                        name: "fk_tarifa_companhiaaerea1",
                        column: x => x.companhiaaerea_cod,
                        principalTable: "companhiaaerea",
                        principalColumn: "cod");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "valorbagagem",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    valorPrimeiraBagagem = table.Column<double>(type: "double", nullable: true),
                    valorBagagemAdicional = table.Column<double>(type: "double", nullable: true),
                    companhiaaerea_cod = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_valorbagagem_companhiaaerea1",
                        column: x => x.companhiaaerea_cod,
                        principalTable: "companhiaaerea",
                        principalColumn: "cod");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "voo",
                columns: table => new
                {
                    id_voo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    codVoo = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    data = table.Column<DateTime>(type: "datetime", nullable: false),
                    ehInternacional = table.Column<sbyte>(type: "tinyint", nullable: true, defaultValueSql: "'0'"),
                    duracao = table.Column<TimeOnly>(type: "time", nullable: true),
                    companhiaaerea_cod = table.Column<int>(type: "int", nullable: false),
                    aeronave_id_aeronave = table.Column<int>(type: "int", nullable: false),
                    idAeroportoOrigem = table.Column<int>(type: "int", nullable: false),
                    idAeroportoDestino = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_voo);
                    table.ForeignKey(
                        name: "fk_voo_aeronave1",
                        column: x => x.aeronave_id_aeronave,
                        principalTable: "aeronave",
                        principalColumn: "id_aeronave");
                    table.ForeignKey(
                        name: "fk_voo_aeroporto1",
                        column: x => x.idAeroportoOrigem,
                        principalTable: "aeroporto",
                        principalColumn: "id_aeroporto");
                    table.ForeignKey(
                        name: "fk_voo_aeroporto2",
                        column: x => x.idAeroportoDestino,
                        principalTable: "aeroporto",
                        principalColumn: "id_aeroporto");
                    table.ForeignKey(
                        name: "fk_voo_companhiaaerea1",
                        column: x => x.companhiaaerea_cod,
                        principalTable: "companhiaaerea",
                        principalColumn: "cod");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "agencia",
                columns: table => new
                {
                    id_agencia = table.Column<int>(type: "int", nullable: false),
                    funcionario_idfuncionario = table.Column<int>(name: "funcionario_id-funcionario", type: "int", nullable: false),
                    nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    taxaAgencia = table.Column<double>(type: "double", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.id_agencia, x.funcionario_idfuncionario })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    table.ForeignKey(
                        name: "fk_agencia_funcionario1",
                        column: x => x.funcionario_idfuncionario,
                        principalTable: "funcionario",
                        principalColumn: "idFuncionario");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "int", nullable: false),
                    login = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    senha = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    funcionario_idFuncionario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_usuario);
                    table.ForeignKey(
                        name: "fk_usuario_funcionario1",
                        column: x => x.funcionario_idFuncionario,
                        principalTable: "funcionario",
                        principalColumn: "idFuncionario");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "passagem",
                columns: table => new
                {
                    idPassagem = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    moeda = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tarifa_idTarifa = table.Column<int>(type: "int", nullable: false),
                    valorbagagem_id = table.Column<int>(type: "int", nullable: false),
                    idVoo1 = table.Column<int>(type: "int", nullable: false),
                    idVoo2 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idPassagem);
                    table.ForeignKey(
                        name: "fk_passagem_tarifa1",
                        column: x => x.tarifa_idTarifa,
                        principalTable: "tarifa",
                        principalColumn: "idTarifa");
                    table.ForeignKey(
                        name: "fk_passagem_valorbagagem1",
                        column: x => x.valorbagagem_id,
                        principalTable: "valorbagagem",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_passagem_voo1",
                        column: x => x.idVoo1,
                        principalTable: "voo",
                        principalColumn: "id_voo");
                    table.ForeignKey(
                        name: "fk_passagem_voo2",
                        column: x => x.idVoo2,
                        principalTable: "voo",
                        principalColumn: "id_voo");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "passageiro",
                columns: table => new
                {
                    idPassageiro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cpf = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    rg = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    usuario_id_usuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idPassageiro);
                    table.ForeignKey(
                        name: "fk_viajante_usuario1",
                        column: x => x.usuario_id_usuario,
                        principalTable: "usuario",
                        principalColumn: "id_usuario");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "bilhete",
                columns: table => new
                {
                    passagem_idPassagem = table.Column<int>(type: "int", nullable: false),
                    passageiro_idPassageiro = table.Column<int>(type: "int", nullable: false),
                    BilheteInternacional = table.Column<sbyte>(type: "tinyint", nullable: true),
                    statusPassageiro = table.Column<string>(type: "enum('Passagemadquirida','Passagemcancelada','Checkinrealizado','Embarquerealizado','NOSHOW')", nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.passagem_idPassagem, x.passageiro_idPassageiro })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    table.ForeignKey(
                        name: "fk_bilhete_passageiro1",
                        column: x => x.passageiro_idPassageiro,
                        principalTable: "passageiro",
                        principalColumn: "idPassageiro");
                    table.ForeignKey(
                        name: "fk_bilhete_passagem1",
                        column: x => x.passagem_idPassagem,
                        principalTable: "passagem",
                        principalColumn: "idPassagem");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "passageiroVIP",
                schema: "sistema_aeroporto",
                columns: table => new
                {
                    companhiaaerea_cod = table.Column<int>(type: "int", nullable: false),
                    passageiro_idPassageiro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_passageiroVIP", x => new { x.companhiaaerea_cod, x.passageiro_idPassageiro });
                    table.ForeignKey(
                        name: "FK_passageiroVIP_companhiaaerea_companhiaaerea_cod",
                        column: x => x.companhiaaerea_cod,
                        principalTable: "companhiaaerea",
                        principalColumn: "cod",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_passageiroVIP_passageiro_passageiro_idPassageiro",
                        column: x => x.passageiro_idPassageiro,
                        principalTable: "passageiro",
                        principalColumn: "idPassageiro",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateIndex(
                name: "capacidadeBagagens_UNIQUE",
                table: "aeronave",
                column: "capacidadeBagagens",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "capacidadePassageiros_UNIQUE",
                table: "aeronave",
                column: "capacidadePassageiros",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_agencia_funcionario1_idx",
                table: "agencia",
                column: "funcionario_id-funcionario");

            migrationBuilder.CreateIndex(
                name: "fk_assento_aeronave1_idx",
                table: "assento",
                column: "aeronave_id_aeronave");

            migrationBuilder.CreateIndex(
                name: "fk_bilhete_passageiro1_idx",
                table: "bilhete",
                column: "passageiro_idPassageiro");

            migrationBuilder.CreateIndex(
                name: "fk_bilhete_passagem1_idx",
                table: "bilhete",
                column: "passagem_idPassagem");

            migrationBuilder.CreateIndex(
                name: "fk_companhiaaerea_has_agencia_agencia1_idx",
                table: "companhiaaerea_has_agencia",
                column: "agencia_id_agencia");

            migrationBuilder.CreateIndex(
                name: "fk_companhiaaerea_has_agencia_companhiaaerea1_idx",
                table: "companhiaaerea_has_agencia",
                column: "companhiaaerea_cod");

            migrationBuilder.CreateIndex(
                name: "fk_viajante_usuario1_idx",
                table: "passageiro",
                column: "usuario_id_usuario");

            migrationBuilder.CreateIndex(
                name: "rg_UNIQUE",
                table: "passageiro",
                column: "rg",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_passageiroVIP_passageiro_idPassageiro",
                schema: "sistema_aeroporto",
                table: "passageiroVIP",
                column: "passageiro_idPassageiro");

            migrationBuilder.CreateIndex(
                name: "fk_passagem_tarifa1_idx",
                table: "passagem",
                column: "tarifa_idTarifa");

            migrationBuilder.CreateIndex(
                name: "fk_passagem_valorbagagem1_idx",
                table: "passagem",
                column: "valorbagagem_id");

            migrationBuilder.CreateIndex(
                name: "fk_passagem_voo1_idx",
                table: "passagem",
                column: "idVoo1",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_passagem_voo2_idx",
                table: "passagem",
                column: "idVoo2");

            migrationBuilder.CreateIndex(
                name: "fk_tarifa_companhiaaerea1_idx",
                table: "tarifa",
                column: "companhiaaerea_cod");

            migrationBuilder.CreateIndex(
                name: "fk_usuario_funcionario1_idx",
                table: "usuario",
                column: "funcionario_idFuncionario");

            migrationBuilder.CreateIndex(
                name: "companhiaaerea_cod_UNIQUE",
                table: "valorbagagem",
                column: "companhiaaerea_cod",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_voo_aeronave1_idx",
                table: "voo",
                column: "aeronave_id_aeronave");

            migrationBuilder.CreateIndex(
                name: "fk_voo_aeroporto1_idx",
                table: "voo",
                column: "idAeroportoOrigem");

            migrationBuilder.CreateIndex(
                name: "fk_voo_aeroporto2_idx",
                table: "voo",
                column: "idAeroportoDestino");

            migrationBuilder.CreateIndex(
                name: "fk_voo_companhiaaerea1_idx",
                table: "voo",
                column: "companhiaaerea_cod");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "agencia");

            migrationBuilder.DropTable(
                name: "assento");

            migrationBuilder.DropTable(
                name: "bilhete");

            migrationBuilder.DropTable(
                name: "companhiaaerea_has_agencia");

            migrationBuilder.DropTable(
                name: "passageiroVIP",
                schema: "sistema_aeroporto");

            migrationBuilder.DropTable(
                name: "passagem");

            migrationBuilder.DropTable(
                name: "passageiro");

            migrationBuilder.DropTable(
                name: "tarifa");

            migrationBuilder.DropTable(
                name: "valorbagagem");

            migrationBuilder.DropTable(
                name: "voo");

            migrationBuilder.DropTable(
                name: "usuario");

            migrationBuilder.DropTable(
                name: "aeronave");

            migrationBuilder.DropTable(
                name: "aeroporto");

            migrationBuilder.DropTable(
                name: "companhiaaerea");

            migrationBuilder.DropTable(
                name: "funcionario");
        }
    }
}
