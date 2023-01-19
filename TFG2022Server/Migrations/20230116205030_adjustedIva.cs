using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFG2022Server.Migrations
{
    public partial class adjustedIva : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Iva",
                table: "Facturas");

            migrationBuilder.AddColumn<int>(
                name: "Iva",
                table: "Productos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Iva",
                table: "Productos");

            migrationBuilder.AddColumn<int>(
                name: "Iva",
                table: "Facturas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
