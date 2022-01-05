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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Operadores");
        }
    }
}
