using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFG2022Server.Migrations
{
    public partial class removedLineaAlbaran_LineaFacturas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "LineaAlbaranes");

            migrationBuilder.DropTable(
                name: "LineaFacturas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    ClienteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoPostal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descuento = table.Column<int>(type: "int", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Poblacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioIdCliente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.ClienteId);
                });

            migrationBuilder.CreateTable(
                name: "LineaAlbaranes",
                columns: table => new
                {
                    LineaAlbaranId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlbaranLineaAlbaran = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Importe = table.Column<double>(type: "float", nullable: false),
                    LineaPedidoLineaAlbaran = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineaAlbaranes", x => x.LineaAlbaranId);
                });

            migrationBuilder.CreateTable(
                name: "LineaFacturas",
                columns: table => new
                {
                    LineaFacturaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    FacturaLineaFactura = table.Column<int>(type: "int", nullable: false),
                    Importe = table.Column<double>(type: "float", nullable: false),
                    ProductoLineaFactura = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineaFacturas", x => x.LineaFacturaId);
                });
        }
    }
}
