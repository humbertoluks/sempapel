using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace backend.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GuiaStatus",
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
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrestadorId = table.Column<string>(nullable: true),
                    UnidadeId = table.Column<string>(nullable: true),
                    PushId = table.Column<string>(nullable: true),
                    TokenId = table.Column<string>(nullable: true),
                    Numero = table.Column<string>(nullable: true),
                    Beneficiario = table.Column<string>(nullable: true),
                    BeneficiarioCartao = table.Column<string>(nullable: true),
                    Valor = table.Column<decimal>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false),
                    GuiaXML = table.Column<string>(nullable: true),
                    GuiaStatusId = table.Column<int>(nullable: false),
                    GuiaTipoId = table.Column<int>(nullable: false),
                    StatusCheckInId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Guias_GuiaStatus_GuiaStatusId",
                        column: x => x.GuiaStatusId,
                        principalTable: "GuiaStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Guias_GuiaTipos_GuiaTipoId",
                        column: x => x.GuiaTipoId,
                        principalTable: "GuiaTipos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Guias_GuiaStatusCheckIns_StatusCheckInId",
                        column: x => x.StatusCheckInId,
                        principalTable: "GuiaStatusCheckIns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Guias_GuiaStatusId",
                table: "Guias",
                column: "GuiaStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Guias_GuiaTipoId",
                table: "Guias",
                column: "GuiaTipoId");

            migrationBuilder.CreateIndex(
                name: "IX_Guias_StatusCheckInId",
                table: "Guias",
                column: "StatusCheckInId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Guias");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "GuiaStatus");

            migrationBuilder.DropTable(
                name: "GuiaTipos");

            migrationBuilder.DropTable(
                name: "GuiaStatusCheckIns");
        }
    }
}
