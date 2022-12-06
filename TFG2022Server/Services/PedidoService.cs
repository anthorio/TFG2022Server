using Microsoft.EntityFrameworkCore;
using TFG2022Server.Data;
using TFG2022Server.Entities;
using TFG2022Server.Extensions;
using TFG2022Server.Models;
using TFG2022Server.Services.Contracts;

namespace TFG2022Server.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly TFG2022Context tfg2022Context;

        public PedidoService(TFG2022Context tfg2022Context)
        {
            this.tfg2022Context = tfg2022Context;
        }

        public async Task<Pedido> CreatePedido(PedidoModel pedidoModel)
        {
            try
            {
                Pedido pedido = new Pedido
                {
                    UsuarioPedido = pedidoModel.UsuarioPedido,
                    FechaPedido = pedidoModel.FechaPedido,
                    EstadoPedido = pedidoModel.EstadoPedido,
                    PrecioTotal = pedidoModel.LineasPedido.Sum(o => o.PrecioFinal)+GetCosteEnvio(),
                    CantidadTotal = pedidoModel.LineasPedido.Sum(o => o.Cantidad),
                    Envio = pedidoModel.Envio
                };
                var addedPedido = await this.tfg2022Context.Pedidos.AddAsync(pedido);
                await this.tfg2022Context.SaveChangesAsync();

                int pedidoId = addedPedido.Entity.PedidoId;
                // https://youtu.be/xO17P9LVkK0?t=14573 aqui explicado

                var lineasPedidoToAdd = ReturnLineaPedidoConPedidoId(pedidoId, pedidoModel.LineasPedido);
                this.tfg2022Context.AddRange(lineasPedidoToAdd);
                
                // Al crear la factura se debe crear el pago también (pendiente de pago)

                await this.tfg2022Context.SaveChangesAsync();

                await UpdateVentasPedidoReportes(pedidoId, pedido);
                return addedPedido.Entity;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Pedido> CreateVentaFisica(PedidoModel pedidoModel)
        {
            try
            {
                Pedido pedido = new Pedido
                {
                    UsuarioPedido = pedidoModel.UsuarioPedido,
                    FechaPedido = pedidoModel.FechaPedido,
                    EstadoPedido = pedidoModel.EstadoPedido,
                    PrecioTotal = pedidoModel.LineasPedido.Sum(o => o.PrecioFinal),
                    CantidadTotal = pedidoModel.LineasPedido.Sum(o => o.Cantidad),
                    Envio = pedidoModel.Envio
                };
                var addedPedido = await this.tfg2022Context.Pedidos.AddAsync(pedido);
                await this.tfg2022Context.SaveChangesAsync();

                int pedidoId = addedPedido.Entity.PedidoId;
                // https://youtu.be/xO17P9LVkK0?t=14573 aqui explicado

                var lineasPedidoToAdd = ReturnLineaPedidoConPedidoId(pedidoId, pedidoModel.LineasPedido);
                this.tfg2022Context.AddRange(lineasPedidoToAdd);
                await this.tfg2022Context.SaveChangesAsync();

                await UpdateVentasPedidoReportes(pedidoId, pedido);
                return addedPedido.Entity;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public double GetCosteEnvio()
        {
            return Constants.costeEnvio;
        }
        public async Task<List<PedidoModel>> GetPedidos()
        {
            try
            {
                return await this.tfg2022Context.Pedidos.Convert();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string[] GetTiposPedido()
        {
            return Constants.TiposPedido;
        }

        private List<LineaPedido> ReturnLineaPedidoConPedidoId(int pedidoId, List<LineaPedido> lineasPedido)
        {
            return (from lp in lineasPedido
                    select new LineaPedido
                    {
                        PedidoLineaPedido = pedidoId,
                        ProductoLineaPedido = lp.ProductoLineaPedido,
                        Cantidad = lp.Cantidad,
                        PrecioFinal = lp.PrecioFinal
                    }).ToList();
        }
        private async Task UpdateVentasPedidoReportes(int pedidoId, Pedido pedido)
        {
            try
            {
                List<VentasPedidoReport> vpr = await (from pi in tfg2022Context.LineaPedidos
                                                      where pi.PedidoLineaPedido == pedidoId
                                                      select new VentasPedidoReport
                                                      {
                                                          PedidoId = pedidoId,
                                                          FechaPedido = pedido.FechaPedido,
                                                          PedidoTotal = pedido.PrecioTotal,
                                                          CantidadTotal = pedido.CantidadTotal,
                                                          LineaPedidoId = pi.LineaPedidoId,
                                                          LineaPedidoCantidad = pi.Cantidad,
                                                          LineaPedidoPrecio = pi.PrecioFinal,
                                                          ProductoId = pi.ProductoLineaPedido,
                                                          NombreProducto = tfg2022Context.Productos.FirstOrDefault(p => p.ProductoId == pi.ProductoLineaPedido).Nombre,
                                                          FamiliaProductoId = tfg2022Context.Productos.FirstOrDefault(p => p.ProductoId == pi.ProductoLineaPedido).FamiliaProductoProducto,
                                                          FamiliaProductoNombre = tfg2022Context.FamiliaProductos.FirstOrDefault(f => f.FamiliaID == tfg2022Context.Productos.FirstOrDefault(p => p.ProductoId == pi.ProductoLineaPedido).FamiliaProductoProducto).Nombre,
                                                          UsuarioId = tfg2022Context.Usuarios.FirstOrDefault(u => u.UsuarioId == pedido.UsuarioPedido).UsuarioId,
                                                          UsuarioNombre = tfg2022Context.Usuarios.FirstOrDefault(u => u.UsuarioId == pedido.UsuarioPedido).Nombre,
                                                          UsuarioCodigoPostal = tfg2022Context.Usuarios.FirstOrDefault(u => u.UsuarioId == pedido.UsuarioPedido).CodigoPostal,
                                                          Envio = pedido.Envio,

                                                      }).ToListAsync();
                this.tfg2022Context.AddRange(vpr);
                await this.tfg2022Context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<PedidoModel>> GetPedidosFromUser(int user)
        {
            try
            {
                var pedidos = await this.tfg2022Context.Pedidos.Convert();
                return pedidos.FindAll(e => e.UsuarioPedido == user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdatePedido(PedidoModel pedidoMod)
        {
            try
            {
                var pedidoToUpdate = await this.tfg2022Context.Pedidos.FindAsync(pedidoMod.PedidoId);

                if (pedidoToUpdate != null)
                {
                    pedidoToUpdate.EstadoPedido = pedidoMod.EstadoPedido;
                    await this.tfg2022Context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
