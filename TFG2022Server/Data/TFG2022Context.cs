using Microsoft.EntityFrameworkCore;
using TFG2022Server.Entities;

namespace TFG2022Server.Data
{
    public class TFG2022Context : DbContext
    {
        public TFG2022Context(DbContextOptions<TFG2022Context> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            SeedData.AddUsuarioData(modelBuilder);
        }

        public DbSet<Albaran> Albaranes { get; set; }// = null!;
        public DbSet<Carrito> Carritos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }// = null!;
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<FamiliaProducto> FamiliaProductos { get; set; }
        public DbSet<LineaAlbaran> LineaAlbaranes { get; set; }
        public DbSet<LineaCarrito> LineaCarritos { get; set; }
        public DbSet<LineaFactura> LineaFacturas { get; set; }
        public DbSet<LineaPedido> LineaPedidos { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }// = null!;
        public DbSet<VentasPedidoReport> VentasPedidoReportes { get; set; }
    }
}
