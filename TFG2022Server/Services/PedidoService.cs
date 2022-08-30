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

        public async Task CreatePedido(PedidoModel pedidoModel)
        {
            try
            {
                Pedido pedido = new Pedido
                {
                    UsuarioPedido = pedidoModel.UsuarioPedido,
                    FechaPedido = pedidoModel.FechaPedido,
                    TipoEnvio = pedidoModel.TipoEnvio,
                    EstadoPedido = pedidoModel.EstadoPedido,
                    PrecioTotal = pedidoModel.LineasPedido.Sum(o => o.PrecioFinal),
                    CantidadTotal = pedidoModel.LineasPedido.Sum(o => o.Cantidad)
                };
                var addedPedido = await this.tfg2022Context.Pedidos.AddAsync(pedido);
                await this.tfg2022Context.SaveChangesAsync();

                int pedidoId = addedPedido.Entity.PedidoId;
                // https://youtu.be/xO17P9LVkK0?t=14573 aqui explicado

                var lineasPedidoToAdd = ReturnLineaPedidoConPedidoId(pedidoId, pedidoModel.LineasPedido);
                this.tfg2022Context.AddRange(lineasPedidoToAdd);
                await this.tfg2022Context.SaveChangesAsync();


            }
            catch (Exception)
            {

                throw;
            }
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
    }
}
