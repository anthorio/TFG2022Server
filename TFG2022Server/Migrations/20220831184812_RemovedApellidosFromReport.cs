using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFG2022Server.Migrations
{
    public partial class RemovedApellidosFromReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsuarioApellidos",
                table: "VentasPedidoReportes");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 1,
                column: "FechaNacimiento",
                value: new DateTime(2022, 8, 31, 20, 48, 12, 395, DateTimeKind.Local).AddTicks(7388));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsuarioApellidos",
                table: "VentasPedidoReportes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 1,
                column: "FechaNacimiento",
                value: new DateTime(2022, 8, 31, 19, 41, 56, 803, DateTimeKind.Local).AddTicks(534));
        }
    }
}
