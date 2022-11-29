using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFG2022Server.Migrations
{
    public partial class addedENVIOtoPEDIDO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StockMinimo",
                table: "Productos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Envio",
                table: "Pedidos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StockMinimo",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Envio",
                table: "Pedidos");
        }
    }
}
