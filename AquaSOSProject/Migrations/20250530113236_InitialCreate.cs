using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AquaSOSProject.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PontosDistribuicao",
                columns: table => new
                {
                    Id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Endereco = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Cidade = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CapacidadeTotalLitros = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PontosDistribuicao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PedidosAgua",
                columns: table => new
                {
                    Id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    UsuarioId = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    PontoDistribuicaoId = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    QuantidadeLitros = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DataSolicitacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Status = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidosAgua", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PedidosAgua_PontosDistribuicao_PontoDistribuicaoId",
                        column: x => x.PontoDistribuicaoId,
                        principalTable: "PontosDistribuicao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidosAgua_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PedidosAgua_PontoDistribuicaoId",
                table: "PedidosAgua",
                column: "PontoDistribuicaoId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidosAgua_UsuarioId",
                table: "PedidosAgua",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidosAgua");

            migrationBuilder.DropTable(
                name: "PontosDistribuicao");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
