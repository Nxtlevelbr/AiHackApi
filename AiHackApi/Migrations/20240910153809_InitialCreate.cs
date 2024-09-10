using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AiHackApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_bairros",
                columns: table => new
                {
                    id_bairro = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nome_bairro = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_bairros", x => x.id_bairro);
                });

            migrationBuilder.CreateTable(
                name: "tb_contatos",
                columns: table => new
                {
                    id_contato = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    telefone = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_contatos", x => x.id_contato);
                });

            migrationBuilder.CreateTable(
                name: "tb_enderecos",
                columns: table => new
                {
                    id_endereco = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    rua = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    numero = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    bairro = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_enderecos", x => x.id_endereco);
                });

            migrationBuilder.CreateTable(
                name: "tb_especialidades",
                columns: table => new
                {
                    id_especialidade = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nome_especialidade = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ativo = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_especialidades", x => x.id_especialidade);
                });

            migrationBuilder.CreateTable(
                name: "tb_pacientes",
                columns: table => new
                {
                    id_paciente = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nome_paciente = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    cpf = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_pacientes", x => x.id_paciente);
                });

            migrationBuilder.CreateTable(
                name: "tb_medicos",
                columns: table => new
                {
                    id_medico = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    nm_medico = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    crm_medico = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    tb_especialidades_id_especialidade = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    tb_contatos_id_contato = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    tb_enderecos_id_endereco = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    salario_medico = table.Column<decimal>(type: "NUMBER(18,2)", precision: 18, scale: 2, nullable: false),
                    ativo = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_medicos", x => x.id_medico);
                    table.ForeignKey(
                        name: "FK_tb_medicos_tb_contatos_tb_contatos_id_contato",
                        column: x => x.tb_contatos_id_contato,
                        principalTable: "tb_contatos",
                        principalColumn: "id_contato",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_medicos_tb_enderecos_tb_enderecos_id_endereco",
                        column: x => x.tb_enderecos_id_endereco,
                        principalTable: "tb_enderecos",
                        principalColumn: "id_endereco",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_medicos_tb_especialidades_tb_especialidades_id_especialidade",
                        column: x => x.tb_especialidades_id_especialidade,
                        principalTable: "tb_especialidades",
                        principalColumn: "id_especialidade",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_consultas",
                columns: table => new
                {
                    id_consulta = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    data_hora_consulta = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    status_consulta = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    tb_medicos_id_medico = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    tb_pacientes_id_paciente = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_consultas", x => x.id_consulta);
                    table.ForeignKey(
                        name: "FK_tb_consultas_tb_medicos_tb_medicos_id_medico",
                        column: x => x.tb_medicos_id_medico,
                        principalTable: "tb_medicos",
                        principalColumn: "id_medico",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_consultas_tb_pacientes_tb_pacientes_id_paciente",
                        column: x => x.tb_pacientes_id_paciente,
                        principalTable: "tb_pacientes",
                        principalColumn: "id_paciente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_consultas_tb_medicos_id_medico",
                table: "tb_consultas",
                column: "tb_medicos_id_medico");

            migrationBuilder.CreateIndex(
                name: "IX_tb_consultas_tb_pacientes_id_paciente",
                table: "tb_consultas",
                column: "tb_pacientes_id_paciente");

            migrationBuilder.CreateIndex(
                name: "IX_tb_medicos_tb_contatos_id_contato",
                table: "tb_medicos",
                column: "tb_contatos_id_contato");

            migrationBuilder.CreateIndex(
                name: "IX_tb_medicos_tb_enderecos_id_endereco",
                table: "tb_medicos",
                column: "tb_enderecos_id_endereco");

            migrationBuilder.CreateIndex(
                name: "IX_tb_medicos_tb_especialidades_id_especialidade",
                table: "tb_medicos",
                column: "tb_especialidades_id_especialidade");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_bairros");

            migrationBuilder.DropTable(
                name: "tb_consultas");

            migrationBuilder.DropTable(
                name: "tb_medicos");

            migrationBuilder.DropTable(
                name: "tb_pacientes");

            migrationBuilder.DropTable(
                name: "tb_contatos");

            migrationBuilder.DropTable(
                name: "tb_enderecos");

            migrationBuilder.DropTable(
                name: "tb_especialidades");
        }
    }
}
