using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFG2022Server.Migrations
{
    public partial class removedDescuentoUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descuento",
                table: "Usuarios");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Descuento",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
