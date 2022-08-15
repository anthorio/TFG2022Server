﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TFG2022Server.Data;

#nullable disable

namespace TFG2022Server.Migrations
{
    [DbContext(typeof(TFG2022Context))]
    partial class TFG2022ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TFG2022Server.Entities.Albaran", b =>
                {
                    b.Property<int>("AlbaranId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AlbaranId"), 1L, 1);

                    b.Property<DateTime>("FechaEntrega")
                        .HasColumnType("datetime2");

                    b.Property<int>("PedidoAlbaran")
                        .HasColumnType("int");

                    b.HasKey("AlbaranId");

                    b.ToTable("Albaranes");
                });

            modelBuilder.Entity("TFG2022Server.Entities.Carrito", b =>
                {
                    b.Property<int>("CarritoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CarritoId"), 1L, 1);

                    b.Property<int>("UsuarioCarrito")
                        .HasColumnType("int");

                    b.HasKey("CarritoId");

                    b.ToTable("Carritos");
                });

            modelBuilder.Entity("TFG2022Server.Entities.Cliente", b =>
                {
                    b.Property<int>("ClienteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClienteId"), 1L, 1);

                    b.Property<int>("CodigoPostal")
                        .HasColumnType("int");

                    b.Property<int>("Descuento")
                        .HasColumnType("int");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Poblacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UsuarioIdCliente")
                        .HasColumnType("int");

                    b.HasKey("ClienteId");

                    b.ToTable("Clientes");

                    b.HasData(
                        new
                        {
                            ClienteId = 1,
                            CodigoPostal = 19285,
                            Descuento = 20,
                            Direccion = "Nerja noseque nosecuanto",
                            Poblacion = "Nerja",
                            UsuarioIdCliente = 1
                        },
                        new
                        {
                            ClienteId = 2,
                            CodigoPostal = 79852,
                            Descuento = 50,
                            Direccion = "Servilla cale noseweqe avenida tu",
                            Poblacion = "Torre",
                            UsuarioIdCliente = 2
                        },
                        new
                        {
                            ClienteId = 3,
                            CodigoPostal = 59267,
                            Descuento = 10,
                            Direccion = "Torremolainos bar misisipi",
                            Poblacion = "Torremolinos",
                            UsuarioIdCliente = 3
                        });
                });

            modelBuilder.Entity("TFG2022Server.Entities.Factura", b =>
                {
                    b.Property<int>("FacturaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FacturaId"), 1L, 1);

                    b.Property<string>("EstadoFactura")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaFactura")
                        .HasColumnType("datetime2");

                    b.Property<string>("InfoPedido")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Iva")
                        .HasColumnType("int");

                    b.Property<int>("PedidoFactura")
                        .HasColumnType("int");

                    b.Property<double>("Total")
                        .HasColumnType("float");

                    b.HasKey("FacturaId");

                    b.ToTable("Facturas");
                });

            modelBuilder.Entity("TFG2022Server.Entities.FamiliaProducto", b =>
                {
                    b.Property<int>("FamiliaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FamiliaID"), 1L, 1);

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FamiliaID");

                    b.ToTable("FamiliaProductos");
                });

            modelBuilder.Entity("TFG2022Server.Entities.LineaAlbaran", b =>
                {
                    b.Property<int>("LineaAlbaranId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LineaAlbaranId"), 1L, 1);

                    b.Property<int>("AlbaranLineaAlbaran")
                        .HasColumnType("int");

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<double>("Importe")
                        .HasColumnType("float");

                    b.Property<int>("LineaPedidoLineaAlbaran")
                        .HasColumnType("int");

                    b.HasKey("LineaAlbaranId");

                    b.ToTable("LineaAlbaranes");
                });

            modelBuilder.Entity("TFG2022Server.Entities.LineaCarrito", b =>
                {
                    b.Property<int>("LineaCarritoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LineaCarritoId"), 1L, 1);

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<int>("CarritoLineaCarrito")
                        .HasColumnType("int");

                    b.Property<int>("ProductoLineaCarrito")
                        .HasColumnType("int");

                    b.HasKey("LineaCarritoId");

                    b.ToTable("LineaCarritos");
                });

            modelBuilder.Entity("TFG2022Server.Entities.LineaFactura", b =>
                {
                    b.Property<int>("LineaFacturaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LineaFacturaId"), 1L, 1);

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<int>("FacturaLineaFactura")
                        .HasColumnType("int");

                    b.Property<double>("Importe")
                        .HasColumnType("float");

                    b.Property<int>("ProductoLineaFactura")
                        .HasColumnType("int");

                    b.HasKey("LineaFacturaId");

                    b.ToTable("LineaFacturas");
                });

            modelBuilder.Entity("TFG2022Server.Entities.LineaPedido", b =>
                {
                    b.Property<int>("LineaPedidoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LineaPedidoId"), 1L, 1);

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<int>("PedidoLineaPedido")
                        .HasColumnType("int");

                    b.Property<double>("PrecioFinal")
                        .HasColumnType("float");

                    b.Property<int>("ProductoLineaPedido")
                        .HasColumnType("int");

                    b.HasKey("LineaPedidoId");

                    b.ToTable("LineaPedidos");
                });

            modelBuilder.Entity("TFG2022Server.Entities.Pago", b =>
                {
                    b.Property<int>("PagoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PagoId"), 1L, 1);

                    b.Property<double>("Cantidad")
                        .HasColumnType("float");

                    b.Property<int>("FacturaPago")
                        .HasColumnType("int");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("Observaciones")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PagoId");

                    b.ToTable("Pagos");
                });

            modelBuilder.Entity("TFG2022Server.Entities.Pedido", b =>
                {
                    b.Property<int>("PedidoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PedidoId"), 1L, 1);

                    b.Property<string>("EstadoPedido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaPedido")
                        .HasColumnType("datetime2");

                    b.Property<string>("TipoEnvio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UsuarioPedido")
                        .HasColumnType("int");

                    b.HasKey("PedidoId");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("TFG2022Server.Entities.Producto", b =>
                {
                    b.Property<int>("ProductoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductoId"), 1L, 1);

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FamiliaProductoProducto")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Precio")
                        .HasColumnType("float");

                    b.Property<int>("ProveedorProducto")
                        .HasColumnType("int");

                    b.Property<string>("UrlImagen")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductoId");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("TFG2022Server.Entities.Proveedor", b =>
                {
                    b.Property<int>("ProveedorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProveedorId"), 1L, 1);

                    b.Property<int>("CodigoPostal")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Telefono")
                        .HasColumnType("int");

                    b.HasKey("ProveedorId");

                    b.ToTable("Proveedores");
                });

            modelBuilder.Entity("TFG2022Server.Entities.Usuario", b =>
                {
                    b.Property<int>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UsuarioId"), 1L, 1);

                    b.Property<string>("Apellidos")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Contraseña")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Dni")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Telefono")
                        .HasColumnType("int");

                    b.HasKey("UsuarioId");

                    b.ToTable("Usuarios");

                    b.HasData(
                        new
                        {
                            UsuarioId = 1,
                            Apellidos = "Perico",
                            Contraseña = "9shjdc78",
                            Dni = "12457896G",
                            Email = "sdasdf@sdkfjsd.com",
                            FechaNacimiento = new DateTime(2022, 8, 11, 21, 28, 26, 437, DateTimeKind.Local).AddTicks(922),
                            Nombre = "Alberto",
                            Rol = "cliente",
                            Telefono = 154789632
                        },
                        new
                        {
                            UsuarioId = 2,
                            Apellidos = "lololol",
                            Contraseña = "asdasd",
                            Dni = "14728539M",
                            Email = "askjdhasdj@youtc,on",
                            FechaNacimiento = new DateTime(1974, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Nombre = "pepep",
                            Rol = "cliente",
                            Telefono = 678867678
                        },
                        new
                        {
                            UsuarioId = 3,
                            Apellidos = "Frefre",
                            Contraseña = "312789adshjk",
                            Dni = "123890756D",
                            Email = "234980@fj.ocm",
                            FechaNacimiento = new DateTime(2000, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Nombre = "Yoigo",
                            Rol = "Jefe",
                            Telefono = 123789798
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
