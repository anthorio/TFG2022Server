using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFG2022Server.Migrations
{
    public partial class addedToReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Envio",
                table: "VentasPedidoReportes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Envio",
                table: "VentasPedidoReportes");
        }
    }
}
