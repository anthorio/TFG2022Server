using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFG2022Server.Migrations
{
    public partial class All_Models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Albaranes",
                columns: table => new
                {
                    AlbaranId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PedidoAlbaran = table.Column<int>(type: "int", nullable: false),
                    FechaEntrega = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albaranes", x => x.AlbaranId);
                });

            migrationBuilder.CreateTable(
                name: "Carritos",
                columns: table => new
                {
                    CarritoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioCarrito = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carritos", x => x.CarritoId);
                });

            migrationBuilder.CreateTable(
                name: "Facturas",
                columns: table => new
                {
                    FacturaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PedidoFactura = table.Column<int>(type: "int", nullable: false),
                    InfoPedido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaFactura = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Iva = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false),
                    EstadoFactura = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facturas", x => x.FacturaId);
                });

            migrationBuilder.CreateTable(
                name: "LineaAlbaranes",
                columns: table => new
                {
                    LineaAlbaranId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlbaranLineaAlbaran = table.Column<int>(type: "int", nullable: false),
                    LineaPedidoLineaAlbaran = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Importe = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineaAlbaranes", x => x.LineaAlbaranId);
                });

            migrationBuilder.CreateTable(
                name: "LineaCarritos",
                columns: table => new
                {
                    LineaCarritoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarritoLineaCarrito = table.Column<int>(type: "int", nullable: false),
                    ProductoLineaCarrito = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineaCarritos", x => x.LineaCarritoId);
                });

            migrationBuilder.CreateTable(
                name: "LineaFacturas",
                columns: table => new
                {
                    LineaFacturaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductoLineaFactura = table.Column<int>(type: "int", nullable: false),
                    FacturaLineaFactura = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Importe = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineaFacturas", x => x.LineaFacturaId);
                });

            migrationBuilder.CreateTable(
                name: "LineaPedidos",
                columns: table => new
                {
                    LineaPedidoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PedidoLineaPedido = table.Column<int>(type: "int", nullable: false),
                    ProductoLineaPedido = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioFinal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineaPedidos", x => x.LineaPedidoId);
                });

            migrationBuilder.CreateTable(
                name: "Pagos",
                columns: table => new
                {
                    PagoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacturaPago = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cantidad = table.Column<double>(type: "float", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagos", x => x.PagoId);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    PedidoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioPedido = table.Column<int>(type: "int", nullable: false),
                    FechaPedido = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoPedido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoEnvio = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.PedidoId);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    ProductoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FamiliaProductoProducto = table.Column<int>(type: "int", nullable: false),
                    ProveedorProducto = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<double>(type: "float", nullable: false),
                    UrlImagen = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.ProductoId);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    ProveedorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodigoPostal = table.Column<int>(type: "int", nullable: false),
                    Telefono = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.ProveedorId);
                });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 1,
                column: "FechaNacimiento",
                value: new DateTime(2022, 8, 10, 21, 11, 11, 152, DateTimeKind.Local).AddTicks(1683));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Albaranes");

            migrationBuilder.DropTable(
                name: "Carritos");

            migrationBuilder.DropTable(
                name: "Facturas");

            migrationBuilder.DropTable(
                name: "LineaAlbaranes");

            migrationBuilder.DropTable(
                name: "LineaCarritos");

            migrationBuilder.DropTable(
                name: "LineaFacturas");

            migrationBuilder.DropTable(
                name: "LineaPedidos");

            migrationBuilder.DropTable(
                name: "Pagos");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 1,
                column: "FechaNacimiento",
                value: new DateTime(2022, 8, 10, 19, 26, 59, 108, DateTimeKind.Local).AddTicks(2712));
        }
    }
}
