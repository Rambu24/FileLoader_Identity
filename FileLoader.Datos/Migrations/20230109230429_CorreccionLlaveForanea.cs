using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileLoader.Datos.Migrations
{
    public partial class CorreccionLlaveForanea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DatosReporte_Catalogo_ReportesCLS_CodigoReporte",
                table: "DatosReporte");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Catalogo_ReportesCLS",
                table: "Catalogo_ReportesCLS");

            migrationBuilder.RenameTable(
                name: "Catalogo_ReportesCLS",
                newName: "Catalogo_Reportes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Catalogo_Reportes",
                table: "Catalogo_Reportes",
                column: "Codigo_Reporte");

            migrationBuilder.AddForeignKey(
                name: "FK_DatosReporte_Catalogo_Reportes_CodigoReporte",
                table: "DatosReporte",
                column: "CodigoReporte",
                principalTable: "Catalogo_Reportes",
                principalColumn: "Codigo_Reporte",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DatosReporte_Catalogo_Reportes_CodigoReporte",
                table: "DatosReporte");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Catalogo_Reportes",
                table: "Catalogo_Reportes");

            migrationBuilder.RenameTable(
                name: "Catalogo_Reportes",
                newName: "Catalogo_ReportesCLS");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Catalogo_ReportesCLS",
                table: "Catalogo_ReportesCLS",
                column: "Codigo_Reporte");

            migrationBuilder.AddForeignKey(
                name: "FK_DatosReporte_Catalogo_ReportesCLS_CodigoReporte",
                table: "DatosReporte",
                column: "CodigoReporte",
                principalTable: "Catalogo_ReportesCLS",
                principalColumn: "Codigo_Reporte",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
