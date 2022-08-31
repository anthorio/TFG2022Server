using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFG2022Server.Migrations
{
    public partial class AddVentasPedidoReportesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VentasPedidoReportes",
                columns: table => new
                {
                    IdVentasPedido = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PedidoId = table.Column<int>(type: "int", nullable: false),
                    FechaPedido = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PedidoTotal = table.Column<double>(type: "float", nullable: false),
                    CantidadTotal = table.Column<int>(type: "int", nullable: false),
                    LineaPedidoId = table.Column<int>(type: "int", nullable: false),
                    LineaPedidoCantidad = table.Column<int>(type: "int", nullable: false),
                    LineaPedidoPrecio = table.Column<double>(type: "float", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    NombreProducto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FamiliaProductoId = table.Column<int>(type: "int", nullable: false),
                    FamiliaProductoNombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    UsuarioNombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioApellidos = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VentasPedidoReportes", x => x.IdVentasPedido);
                });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 1,
                column: "FechaNacimiento",
                value: new DateTime(2022, 8, 31, 19, 41, 56, 803, DateTimeKind.Local).AddTicks(534));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VentasPedidoReportes");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 1,
                column: "FechaNacimiento",
                value: new DateTime(2022, 8, 23, 19, 50, 47, 793, DateTimeKind.Local).AddTicks(4396));
        }
    }
}
