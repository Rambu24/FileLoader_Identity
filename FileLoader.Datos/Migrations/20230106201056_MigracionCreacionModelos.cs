using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileLoader.Data.Migrations
{
    public partial class MigracionCreacionModelos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BalanceFocrede",
                columns: table => new
                {
                    Indice = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Reporte = table.Column<int>(type: "int", nullable: true),
                    CodigoCuenta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreCuenta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SaldoAlDia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SaldoDiaAnterior = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Variacion = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BalanceFocrede", x => x.Indice);
                });

            migrationBuilder.CreateTable(
                name: "Catalogo_CuentasFocrede",
                columns: table => new
                {
                    CodigoCuenta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCuenta = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalogo_CuentasFocrede", x => x.CodigoCuenta);
                });

            migrationBuilder.CreateTable(
                name: "DatosReporte",
                columns: table => new
                {
                    Indice = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Moneda = table.Column<int>(type: "int", nullable: false),
                    Fecha_Reporte = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Usuario_Generador = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatosReporte", x => x.Indice);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BalanceFocrede");

            migrationBuilder.DropTable(
                name: "Catalogo_CuentasFocrede");

            migrationBuilder.DropTable(
                name: "DatosReporte");
        }
    }
}
