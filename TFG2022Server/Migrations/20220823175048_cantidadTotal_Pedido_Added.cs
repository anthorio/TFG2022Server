using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFG2022Server.Migrations
{
    public partial class cantidadTotal_Pedido_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CantidadTotal",
                table: "Pedidos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 1,
                column: "FechaNacimiento",
                value: new DateTime(2022, 8, 23, 19, 50, 47, 793, DateTimeKind.Local).AddTicks(4396));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CantidadTotal",
                table: "Pedidos");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 1,
                column: "FechaNacimiento",
                value: new DateTime(2022, 8, 23, 19, 12, 9, 398, DateTimeKind.Local).AddTicks(4932));
        }
    }
}
