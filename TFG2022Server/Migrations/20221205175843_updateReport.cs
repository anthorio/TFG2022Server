using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFG2022Server.Migrations
{
    public partial class updateReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsuarioCodigoPostal",
                table: "VentasPedidoReportes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsuarioCodigoPostal",
                table: "VentasPedidoReportes");
        }
    }
}
