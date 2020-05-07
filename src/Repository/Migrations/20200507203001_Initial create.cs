using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class Initialcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "AtendimentoSemPapel");

            migrationBuilder.CreateTable(
                name: "GuiaStatus",
                schema: "AtendimentoSemPapel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuiaStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GuiaStatusCheckIns",
                schema: "AtendimentoSemPapel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuiaStatusCheckIns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GuiaTipos",
                schema: "AtendimentoSemPapel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuiaTipos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "AtendimentoSemPapel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Guias",
                schema: "AtendimentoSemPapel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrestadorId = table.Column<string>(nullable: false),
                    UnidadeId = table.Column<int>(nullable: true),
                    PushId = table.Column<string>(nullable: true),
                    TokenId = table.Column<string>(nullable: true),
                    Numero = table.Column<string>(nullable: false),
                    Beneficiario = table.Column<string>(nullable: false),
                    BeneficiarioCartao = table.Column<string>(nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Data = table.Column<DateTime>(nullable: false),
                    GuiaXML = table.Column<string>(nullable: true),
                    GuiaStatusId = table.Column<int>(nullable: false),
                    GuiaTipoId = table.Column<int>(nullable: false),
                    StatusCheckInId = table.Column<int>(nullable: false),
                    IdGuiaExterno = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Guias_GuiaStatus_GuiaStatusId",
                        column: x => x.GuiaStatusId,
                        principalSchema: "AtendimentoSemPapel",
                        principalTable: "GuiaStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Guias_GuiaTipos_GuiaTipoId",
                        column: x => x.GuiaTipoId,
                        principalSchema: "AtendimentoSemPapel",
                        principalTable: "GuiaTipos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Guias_GuiaStatusCheckIns_StatusCheckInId",
                        column: x => x.StatusCheckInId,
                        principalSchema: "AtendimentoSemPapel",
                        principalTable: "GuiaStatusCheckIns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Guias_GuiaStatusId",
                schema: "AtendimentoSemPapel",
                table: "Guias",
                column: "GuiaStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Guias_GuiaTipoId",
                schema: "AtendimentoSemPapel",
                table: "Guias",
                column: "GuiaTipoId");

            migrationBuilder.CreateIndex(
                name: "IX_Guias_StatusCheckInId",
                schema: "AtendimentoSemPapel",
                table: "Guias",
                column: "StatusCheckInId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Guias",
                schema: "AtendimentoSemPapel");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "AtendimentoSemPapel");

            migrationBuilder.DropTable(
                name: "GuiaStatus",
                schema: "AtendimentoSemPapel");

            migrationBuilder.DropTable(
                name: "GuiaTipos",
                schema: "AtendimentoSemPapel");

            migrationBuilder.DropTable(
                name: "GuiaStatusCheckIns",
                schema: "AtendimentoSemPapel");
        }
    }
}
