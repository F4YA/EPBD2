using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EPBD2.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "epbd2");

            migrationBuilder.CreateTable(
                name: "Editais",
                schema: "epbd2",
                columns: table => new
                {
                    nome = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    objeto = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    data_publicacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    data_resultado_parcial = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    data_resultado_definitivo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    data_homologacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    num_lotes = table.Column<int>(type: "integer", nullable: false),
                    num_equipamentos = table.Column<int>(type: "integer", nullable: false),
                    situacao = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Editais", x => x.nome);
                });

            migrationBuilder.CreateTable(
                name: "Oscs",
                schema: "epbd2",
                columns: table => new
                {
                    cnpj = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    nome = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oscs", x => x.cnpj);
                });

            migrationBuilder.CreateTable(
                name: "Vereador",
                schema: "epbd2",
                columns: table => new
                {
                    cpf = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    nome = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    contato = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vereador", x => x.cpf);
                });

            migrationBuilder.CreateTable(
                name: "ContatoOscs",
                schema: "epbd2",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tipo = table.Column<short>(type: "smallint", nullable: false),
                    contato = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    OscCnpj = table.Column<string>(type: "character varying(14)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContatoOscs", x => x.id);
                    table.ForeignKey(
                        name: "FK_ContatoOscs_Oscs_OscCnpj",
                        column: x => x.OscCnpj,
                        principalSchema: "epbd2",
                        principalTable: "Oscs",
                        principalColumn: "cnpj");
                });

            migrationBuilder.CreateTable(
                name: "LocalizacoesOscs",
                schema: "epbd2",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    logradouro = table.Column<string>(type: "character varying(180)", maxLength: 180, nullable: false),
                    numero = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    complemento = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    cep = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    distrito = table.Column<short>(type: "smallint", nullable: false),
                    subprefeitura = table.Column<short>(type: "smallint", nullable: false),
                    regiao = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false),
                    latitude = table.Column<decimal>(type: "numeric", nullable: true),
                    longitude = table.Column<decimal>(type: "numeric", nullable: true),
                    OscCnpj = table.Column<string>(type: "character varying(14)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalizacoesOscs", x => x.id);
                    table.ForeignKey(
                        name: "FK_LocalizacoesOscs_Oscs_OscCnpj",
                        column: x => x.OscCnpj,
                        principalSchema: "epbd2",
                        principalTable: "Oscs",
                        principalColumn: "cnpj");
                });

            migrationBuilder.CreateTable(
                name: "RepresentanteLegais",
                schema: "epbd2",
                columns: table => new
                {
                    cpf = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    nome = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    email = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    telefone = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: false),
                    OscCnpj = table.Column<string>(type: "character varying(14)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepresentanteLegais", x => x.cpf);
                    table.ForeignKey(
                        name: "FK_RepresentanteLegais_Oscs_OscCnpj",
                        column: x => x.OscCnpj,
                        principalSchema: "epbd2",
                        principalTable: "Oscs",
                        principalColumn: "cnpj");
                });

            migrationBuilder.CreateTable(
                name: "Termos",
                schema: "epbd2",
                columns: table => new
                {
                    termo = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    nome_projeto = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    objeto = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    qtd_beneficarios = table.Column<short>(type: "smallint", nullable: false),
                    tipo_contrato = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    valor_total = table.Column<decimal>(type: "numeric", nullable: true),
                    sei_pagamento = table.Column<int>(type: "integer", nullable: true),
                    data_inicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    data_termino = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    data_assinatura = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    origem_recurso = table.Column<bool>(type: "boolean", nullable: true),
                    EditalNome = table.Column<string>(type: "character varying(60)", nullable: true),
                    OscCnpj = table.Column<string>(type: "character varying(14)", nullable: true),
                    RepresentanteLegalCpf = table.Column<string>(type: "character varying(11)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Termos", x => x.termo);
                    table.ForeignKey(
                        name: "FK_Termos_Editais_EditalNome",
                        column: x => x.EditalNome,
                        principalSchema: "epbd2",
                        principalTable: "Editais",
                        principalColumn: "nome");
                    table.ForeignKey(
                        name: "FK_Termos_Oscs_OscCnpj",
                        column: x => x.OscCnpj,
                        principalSchema: "epbd2",
                        principalTable: "Oscs",
                        principalColumn: "cnpj");
                    table.ForeignKey(
                        name: "FK_Termos_RepresentanteLegais_RepresentanteLegalCpf",
                        column: x => x.RepresentanteLegalCpf,
                        principalSchema: "epbd2",
                        principalTable: "RepresentanteLegais",
                        principalColumn: "cpf");
                });

            migrationBuilder.CreateTable(
                name: "AditivosApostilamentos",
                schema: "epbd2",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sei = table.Column<int>(type: "integer", nullable: false),
                    objeto = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    tipo = table.Column<bool>(type: "boolean", nullable: false),
                    TermoNome = table.Column<string>(type: "character varying(60)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AditivosApostilamentos", x => x.id);
                    table.ForeignKey(
                        name: "FK_AditivosApostilamentos_Termos_TermoNome",
                        column: x => x.TermoNome,
                        principalSchema: "epbd2",
                        principalTable: "Termos",
                        principalColumn: "termo");
                });

            migrationBuilder.CreateTable(
                name: "EmendaParlamentar",
                schema: "epbd2",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    valor = table.Column<decimal>(type: "numeric", nullable: false),
                    VereadorCpf = table.Column<string>(type: "character varying(11)", nullable: true),
                    TermoNome = table.Column<string>(type: "character varying(60)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmendaParlamentar", x => x.id);
                    table.ForeignKey(
                        name: "FK_EmendaParlamentar_Termos_TermoNome",
                        column: x => x.TermoNome,
                        principalSchema: "epbd2",
                        principalTable: "Termos",
                        principalColumn: "termo");
                    table.ForeignKey(
                        name: "FK_EmendaParlamentar_Vereador_VereadorCpf",
                        column: x => x.VereadorCpf,
                        principalSchema: "epbd2",
                        principalTable: "Vereador",
                        principalColumn: "cpf");
                });

            migrationBuilder.CreateTable(
                name: "LocalizacoesTermos",
                schema: "epbd2",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    logradouro = table.Column<string>(type: "character varying(180)", maxLength: 180, nullable: false),
                    numero = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    complemento = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    cep = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    distrito = table.Column<short>(type: "smallint", nullable: false),
                    subprefeitura = table.Column<short>(type: "smallint", nullable: false),
                    regiao = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false),
                    latitude = table.Column<decimal>(type: "numeric", nullable: true),
                    longitude = table.Column<decimal>(type: "numeric", nullable: true),
                    TermoNome = table.Column<string>(type: "character varying(60)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalizacoesTermos", x => x.id);
                    table.ForeignKey(
                        name: "FK_LocalizacoesTermos_Termos_TermoNome",
                        column: x => x.TermoNome,
                        principalSchema: "epbd2",
                        principalTable: "Termos",
                        principalColumn: "termo");
                });

            migrationBuilder.CreateTable(
                name: "Alteracoes",
                schema: "epbd2",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    atributo = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    valor_antigo = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    AditivoApostilamentoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alteracoes", x => x.id);
                    table.ForeignKey(
                        name: "FK_Alteracoes_AditivosApostilamentos_AditivoApostilamentoId",
                        column: x => x.AditivoApostilamentoId,
                        principalSchema: "epbd2",
                        principalTable: "AditivosApostilamentos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AditivosApostilamentos_TermoNome",
                schema: "epbd2",
                table: "AditivosApostilamentos",
                column: "TermoNome");

            migrationBuilder.CreateIndex(
                name: "IX_Alteracoes_AditivoApostilamentoId",
                schema: "epbd2",
                table: "Alteracoes",
                column: "AditivoApostilamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_ContatoOscs_OscCnpj",
                schema: "epbd2",
                table: "ContatoOscs",
                column: "OscCnpj");

            migrationBuilder.CreateIndex(
                name: "IX_EmendaParlamentar_TermoNome",
                schema: "epbd2",
                table: "EmendaParlamentar",
                column: "TermoNome");

            migrationBuilder.CreateIndex(
                name: "IX_EmendaParlamentar_VereadorCpf",
                schema: "epbd2",
                table: "EmendaParlamentar",
                column: "VereadorCpf");

            migrationBuilder.CreateIndex(
                name: "IX_LocalizacoesOscs_OscCnpj",
                schema: "epbd2",
                table: "LocalizacoesOscs",
                column: "OscCnpj");

            migrationBuilder.CreateIndex(
                name: "IX_LocalizacoesTermos_TermoNome",
                schema: "epbd2",
                table: "LocalizacoesTermos",
                column: "TermoNome");

            migrationBuilder.CreateIndex(
                name: "IX_RepresentanteLegais_OscCnpj",
                schema: "epbd2",
                table: "RepresentanteLegais",
                column: "OscCnpj");

            migrationBuilder.CreateIndex(
                name: "IX_Termos_EditalNome",
                schema: "epbd2",
                table: "Termos",
                column: "EditalNome");

            migrationBuilder.CreateIndex(
                name: "IX_Termos_OscCnpj",
                schema: "epbd2",
                table: "Termos",
                column: "OscCnpj");

            migrationBuilder.CreateIndex(
                name: "IX_Termos_RepresentanteLegalCpf",
                schema: "epbd2",
                table: "Termos",
                column: "RepresentanteLegalCpf");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alteracoes",
                schema: "epbd2");

            migrationBuilder.DropTable(
                name: "ContatoOscs",
                schema: "epbd2");

            migrationBuilder.DropTable(
                name: "EmendaParlamentar",
                schema: "epbd2");

            migrationBuilder.DropTable(
                name: "LocalizacoesOscs",
                schema: "epbd2");

            migrationBuilder.DropTable(
                name: "LocalizacoesTermos",
                schema: "epbd2");

            migrationBuilder.DropTable(
                name: "AditivosApostilamentos",
                schema: "epbd2");

            migrationBuilder.DropTable(
                name: "Vereador",
                schema: "epbd2");

            migrationBuilder.DropTable(
                name: "Termos",
                schema: "epbd2");

            migrationBuilder.DropTable(
                name: "Editais",
                schema: "epbd2");

            migrationBuilder.DropTable(
                name: "RepresentanteLegais",
                schema: "epbd2");

            migrationBuilder.DropTable(
                name: "Oscs",
                schema: "epbd2");
        }
    }
}
