﻿using Microsoft.EntityFrameworkCore;
using TFG2022Server.Data;
using TFG2022Server.Entities;
using TFG2022Server.Extensions;
using TFG2022Server.Models;
using TFG2022Server.Services.Contracts;

namespace TFG2022Server.Services
{
    public class FacturaService : IFacturaService
    {
        private readonly TFG2022Context tfg2022Context;

        public FacturaService(TFG2022Context tfg2022Context)
        {
            this.tfg2022Context = tfg2022Context;
        }

        public string[] GetEstadosFactura()
        {
            return Constants.EstadosFactura;
        }

        public async Task<List<FacturaModel>> GetFacturas()
        {
            try
            {
                return await this.tfg2022Context.Facturas.Convert();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Factura> CreateFacturaFromPedido(Pedido pedido)
        {
            try
            {
                Factura facturaToAdd = new Factura
                {
                    PedidoFactura = pedido.PedidoId,
                    EstadoFactura = "",
                    FechaFactura = pedido.FechaPedido,
                    Iva = 21,
                    Total = pedido.PrecioTotal
                };

                var result = await this.tfg2022Context.Facturas.AddAsync(facturaToAdd);
                await this.tfg2022Context.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task UpdateFactura(FacturaModel factura)
        {
            throw new NotImplementedException();
        }
    }
}
