using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFG2022Server.Migrations
{
    public partial class seed_disabled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "CodigoPostal",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Descuento",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Direccion",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Poblacion",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoPostal",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Descuento",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Direccion",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Poblacion",
                table: "Usuarios");

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "ClienteId", "CodigoPostal", "Descuento", "Direccion", "Poblacion", "UsuarioIdCliente" },
                values: new object[,]
                {
                    { 1, "19285", 20, "Nerja noseque nosecuanto", "Nerja", 1 },
                    { 2, "79852", 50, "Servilla cale noseweqe avenida tu", "Torre", 2 },
                    { 3, "59267", 10, "Torremolainos bar misisipi", "Torremolinos", 3 }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "UsuarioId", "Apellidos", "Contraseña", "Dni", "Email", "FechaNacimiento", "Nombre", "Rol", "Telefono" },
                values: new object[,]
                {
                    { 1, "Perico", "9shjdc78", "12457896G", "sdasdf@sdkfjsd.com", new DateTime(2022, 8, 31, 20, 48, 12, 395, DateTimeKind.Local).AddTicks(7388), "Alberto", "cliente", "123123123" },
                    { 2, "lololol", "asdasd", "14728539M", "askjdhasdj@youtc,on", new DateTime(1974, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "pepep", "cliente", "678867678" },
                    { 3, "Frefre", "312789adshjk", "123890756D", "234980@fj.ocm", new DateTime(2000, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Yoigo", "Jefe", "123789798" }
                });
        }
    }
}
