using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFG2022Server.Migrations
{
    public partial class SeedUsuarioData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "ClienteId", "CodigoPostal", "Descuento", "Direccion", "Poblacion", "UsuarioIdCliente" },
                values: new object[,]
                {
                    { 1, 19285, 20, "Nerja noseque nosecuanto", "Nerja", 1 },
                    { 2, 79852, 50, "Servilla cale noseweqe avenida tu", "Torre", 2 },
                    { 3, 59267, 10, "Torremolainos bar misisipi", "Torremolinos", 3 }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "UsuarioId", "Apellidos", "Contraseña", "Dni", "Email", "FechaNacimiento", "Nombre", "Rol", "Telefono" },
                values: new object[,]
                {
                    { 1, "Perico", "9shjdc78", "12457896G", "sdasdf@sdkfjsd.com", new DateTime(2022, 8, 8, 19, 27, 26, 456, DateTimeKind.Local).AddTicks(4148), "Alberto", "cliente", 154789632 },
                    { 2, "lololol", "asdasd", "14728539M", "askjdhasdj@youtc,on", new DateTime(1974, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "pepep", "cliente", 678867678 },
                    { 3, "Frefre", "312789adshjk", "123890756D", "234980@fj.ocm", new DateTime(2000, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yoigo", "Jefe", 123789798 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "ClienteId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "ClienteId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "ClienteId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 3);
        }
    }
}
