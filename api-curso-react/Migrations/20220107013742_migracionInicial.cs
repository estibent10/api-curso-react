using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace api_curso_react.Migrations
{
    public partial class migracionInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Operadores",
                columns: table => new
                {
                    TipoIdentificacion = table.Column<int>(nullable: false),
                    Identificacion = table.Column<string>(maxLength: 22, nullable: false),
                    Nombre = table.Column<string>(maxLength: 150, nullable: false),
                    Email = table.Column<string>(maxLength: 150, nullable: false),
                    CuentaIBAN = table.Column<string>(maxLength: 50, nullable: false),
                    Activo = table.Column<bool>(nullable: false, defaultValueSql: "1"),
                    CreadoPor = table.Column<string>(maxLength: 75, nullable: true),
                    ModificadoPor = table.Column<string>(maxLength: 75, nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    FechaModificacion = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operadores", x => new { x.TipoIdentificacion, x.Identificacion });
                });

            migrationBuilder.CreateTable(
                name: "Cobradores",
                columns: table => new
                {
                    TipoIdentificacion = table.Column<int>(nullable: false),
                    Identificacion = table.Column<string>(maxLength: 22, nullable: false),
                    Nombre = table.Column<string>(maxLength: 150, nullable: false),
                    Email = table.Column<string>(maxLength: 150, nullable: false),
                    Activo = table.Column<bool>(nullable: false, defaultValueSql: "1"),
                    CreadoPor = table.Column<string>(maxLength: 75, nullable: true),
                    ModificadoPor = table.Column<string>(maxLength: 75, nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    FechaModificacion = table.Column<DateTime>(nullable: true),
                    TipoIdentificacionOperador = table.Column<int>(nullable: false),
                    IdentificacionOperador = table.Column<string>(maxLength: 22, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cobradores", x => new { x.TipoIdentificacion, x.Identificacion });
                    table.ForeignKey(
                        name: "FK_Operadores_TipoIdentificacionOperador_IdentificacionOperador",
                        columns: x => new { x.TipoIdentificacionOperador, x.IdentificacionOperador },
                        principalTable: "Operadores",
                        principalColumns: new[] { "TipoIdentificacion", "Identificacion" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cobradores_TipoIdentificacionOperador_IdentificacionOperador",
                table: "Cobradores",
                columns: new[] { "TipoIdentificacionOperador", "IdentificacionOperador" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cobradores");

            migrationBuilder.DropTable(
                name: "Operadores");
        }
    }
}
