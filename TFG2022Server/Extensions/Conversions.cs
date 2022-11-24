using Microsoft.EntityFrameworkCore;
using TFG2022Server.Entities;
using TFG2022Server.Models;
using TFG2022Server.Data;

namespace TFG2022Server.Extensions
{
    public static class Conversions
    {
        public static async Task<List<UsuarioModel>> Convert(this IQueryable<Usuario> usuarios)
        {
            return await (from w in usuarios
                          select new UsuarioModel
                          {
                              UsuarioId = w.UsuarioId,
                              Rol = w.Rol,
                              Email = w.Email,
                              Contraseña = w.Contraseña,
                              Nombre = w.Nombre,
                              Apellidos = w.Apellidos,
                              Telefono = w.Telefono,
                              Dni = w.Dni,
                              FechaNacimiento = w.FechaNacimiento,
                              Direccion = w.Direccion,
                              Poblacion = w.Poblacion,
                              CodigoPostal = w.CodigoPostal,
                              Descuento = w.Descuento
                          }).ToListAsync();
        }
        public static async Task<List<UsuarioModel>> Convert(this IQueryable<Usuario> usuarios, TFG2022Context context)
        {
            return await (from u in usuarios
                          join r in context.Carritos
                          on u.UsuarioId equals r.UsuarioCarrito
                          select new UsuarioModel
                          {
                              UsuarioId = u.UsuarioId,
                              Rol = u.Rol,
                              Email = u.Email,
                              Contraseña = u.Contraseña,
                              Nombre = u.Nombre,
                              Apellidos = u.Apellidos,
                              Telefono = u.Telefono,
                              Dni = u.Dni,
                              FechaNacimiento = u.FechaNacimiento,
                              Direccion = u.Direccion,
                              Poblacion = u.Poblacion,
                              CodigoPostal = u.CodigoPostal,
                              Descuento = u.Descuento
                          }).ToListAsync();
        }

        public static async Task<List<FamiliaProductoModel>> Convert(this IQueryable<FamiliaProducto> familiaProductos)
        {
            return await (from w in familiaProductos
                          select new FamiliaProductoModel
                          {
                              FamiliaID = w.FamiliaID,
                              Nombre = w.Nombre,
                              Descripcion = w.Descripcion
                          }).ToListAsync();
        }

        public static async Task<List<AlbaranModel>> Convert(this IQueryable<Albaran> albaranes)
        {
            return await (from w in albaranes
                          select new AlbaranModel
                          {
                              AlbaranId = w.AlbaranId,
                              PedidoAlbaran = w.PedidoAlbaran,
                              FechaEntrega = w.FechaEntrega
                          }).ToListAsync();
        }

        public static async Task<List<CarritoModel>> Convert(this IQueryable<Carrito> carritos)
        {
            return await (from w in carritos
                          select new CarritoModel
                          {
                              CarritoId = w.CarritoId,
                              UsuarioCarrito = w.UsuarioCarrito
                          }).ToListAsync();
        }

        public static async Task<List<FacturaModel>> Convert(this IQueryable<Factura> facturas)
        {
            return await (from w in facturas
                          select new FacturaModel
                          {
                              FacturaId = w.FacturaId,
                              PedidoFactura = w.PedidoFactura,
                              InfoPedido = w.InfoPedido,
                              FechaFactura = w.FechaFactura,
                              Iva = w.Iva,
                              Total = Math.Round(w.Total, 2),
                              EstadoFactura = w.EstadoFactura
                          }).ToListAsync();
        }

        public static async Task<List<LineaCarritoModel>> Convert(this IQueryable<LineaCarrito> lineaCarritos)
        {
            return await (from w in lineaCarritos
                          select new LineaCarritoModel
                          {
                              LineaCarritoId = w.LineaCarritoId,
                              CarritoLineaCarrito = w.CarritoLineaCarrito,
                              ProductoLineaCarrito = w.ProductoLineaCarrito,
                              Cantidad = w.Cantidad
                          }).ToListAsync();
        }

        public static async Task<List<LineaPedidoModel>> Convert(this IQueryable<LineaPedido> lineaPedidos)
        {
            return await (from w in lineaPedidos
                          select new LineaPedidoModel
                          {
                              LineaPedidoId = w.LineaPedidoId,
                              PedidoLineaPedido = w.PedidoLineaPedido,
                              ProductoLineaPedido = w.ProductoLineaPedido,
                              Cantidad = w.Cantidad,
                              PrecioFinal = Math.Round(w.PrecioFinal, 2)
                          }).ToListAsync();
        }

        public static async Task<List<PagoModel>> Convert(this IQueryable<Pago> pagos)
        {
            return await (from w in pagos
                          select new PagoModel
                          {
                              PagoId = w.PagoId,
                              FacturaPago = w.FacturaPago,
                              Fecha = w.Fecha,
                              Cantidad = Math.Round(w.Cantidad, 2),
                              Observaciones = w.Observaciones
                          }).ToListAsync();
        }

        public static async Task<List<PedidoModel>> Convert(this IQueryable<Pedido> pedidos)
        {
            return await (from w in pedidos
                          select new PedidoModel
                          {
                              PedidoId = w.PedidoId,
                              UsuarioPedido = w.UsuarioPedido,
                              FechaPedido = w.FechaPedido,
                              EstadoPedido = w.EstadoPedido,
                              PrecioTotal = Math.Round(w.PrecioTotal, 2),
                              CantidadTotal = w.CantidadTotal
                          }).ToListAsync();
        }

        public static async Task<List<ProductoModel>> Convert(this IQueryable<Producto> productos)
        {
            return await (from w in productos
                          select new ProductoModel
                          {
                              ProductoId = w.ProductoId,
                              FamiliaProductoProducto = w.FamiliaProductoProducto,
                              ProveedorProducto = w.ProveedorProducto,
                              Nombre = w.Nombre,
                              Descripcion = w.Descripcion,
                              Cantidad = w.Cantidad,
                              Precio = Math.Round(w.Precio, 2),
                              UrlImagen = w.UrlImagen
                          }).ToListAsync();
        }

        public static async Task<List<ProductoModel>> Convert(this IQueryable<Producto> Products, TFG2022Context context)
        {
            return await (from prod in Products
                          join familiaProd in context.FamiliaProductos
                          on prod.FamiliaProductoProducto equals familiaProd.FamiliaID
                          select new ProductoModel
                          {
                              ProductoId = prod.ProductoId,
                              FamiliaProductoProducto = prod.FamiliaProductoProducto,
                              ProveedorProducto = prod.ProveedorProducto,
                              Nombre = prod.Nombre,
                              Descripcion = prod.Descripcion,
                              Cantidad = prod.Cantidad,
                              Precio = Math.Round(prod.Precio, 2),
                              UrlImagen = prod.UrlImagen
                          }).ToListAsync();
        }

        public static async Task<List<ProveedorModel>> Convert(this IQueryable<Proveedor> proveedores)
        {
            return await (from w in proveedores
                          select new ProveedorModel
                          {
                              ProveedorId = w.ProveedorId,
                              Nombre = w.Nombre,
                              Descripcion = w.Descripcion,
                              Direccion = w.Direccion,
                              CodigoPostal = w.CodigoPostal,
                              Telefono = w.Telefono,
                              Email = w.Email
                          }).ToListAsync();
        }

        public static Usuario Convert(this UsuarioModel usuarioModel)
        {
            return new Usuario
            {
                // UsuarioId = usuarioModel.UsuarioId, ? hace falta ?
                Rol = usuarioModel.Rol,
                Email = usuarioModel.Email,
                Contraseña = usuarioModel.Contraseña,
                Nombre = usuarioModel.Nombre,
                Apellidos = usuarioModel.Apellidos,
                Telefono = usuarioModel.Telefono,
                Dni = usuarioModel.Dni.ToUpper(),
                FechaNacimiento = usuarioModel.FechaNacimiento,
                Direccion = usuarioModel.Direccion,
                Poblacion = usuarioModel.Poblacion,
                CodigoPostal = usuarioModel.CodigoPostal,
                Descuento = usuarioModel.Descuento
            };
        }
        public static LineaCarrito Convert(this LineaCarritoModel lineaCarritoModel)
        {
            return new LineaCarrito
            {
                CarritoLineaCarrito = lineaCarritoModel.CarritoLineaCarrito,
                ProductoLineaCarrito = lineaCarritoModel.ProductoLineaCarrito,
                Cantidad = lineaCarritoModel.Cantidad
            };
        }
        public static Producto Convert(this ProductoModel productoModel)
        {
            return new Producto
            {
                FamiliaProductoProducto = productoModel.FamiliaProductoProducto,
                ProveedorProducto = productoModel.ProveedorProducto,
                Nombre = productoModel.Nombre,
                Cantidad = productoModel.Cantidad,
                Descripcion = productoModel.Descripcion,
                Precio = Math.Round(productoModel.Precio, 2),
                ProductoId = productoModel.ProductoId,
                UrlImagen = productoModel.UrlImagen
            };
        }
        public static FamiliaProducto Convert(this FamiliaProductoModel fproductoModel)
        {
            return new FamiliaProducto
            {
                FamiliaID = fproductoModel.FamiliaID,
                Nombre = fproductoModel.Nombre,
                Descripcion = fproductoModel.Descripcion
            };
        }
    }
}
