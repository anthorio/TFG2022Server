using Microsoft.EntityFrameworkCore;
using TFG2022Server.Data;
using TFG2022Server.Entities;
using TFG2022Server.Extensions;
using TFG2022Server.Models;
using TFG2022Server.Services.Contracts;

namespace TFG2022Server.Services
{
    public class LineaCarritoService : ILineaCarritoService
    {
        private readonly TFG2022Context tfg2022Context;

        public LineaCarritoService(TFG2022Context tfg2022Context)
        {
            this.tfg2022Context = tfg2022Context;
        }

        public async Task<List<LineaCarritoModel>> GetLineaCarritos()
        {
            try
            {
                return await this.tfg2022Context.LineaCarritos.Convert();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<LineaCarrito> AnadirLinea(LineaCarritoModel lineaCarrito)
        {
            try
            {
                LineaCarrito lineaCarritoToAdd = lineaCarrito.Convert();

                var lineaCarritoExistente = await this.tfg2022Context.LineaCarritos.SingleOrDefaultAsync(lincarr => lincarr.CarritoLineaCarrito == lineaCarrito.CarritoLineaCarrito && lincarr.ProductoLineaCarrito == lineaCarrito.ProductoLineaCarrito);
                if (lineaCarritoExistente == null)
                {
                    var result = await this.tfg2022Context.LineaCarritos.AddAsync(lineaCarritoToAdd);
                    await this.tfg2022Context.SaveChangesAsync();
                    return (result.Entity);
                }
                else
                {
                    lineaCarritoExistente.Cantidad = lineaCarritoToAdd.Cantidad + lineaCarritoExistente.Cantidad;
                    await this.tfg2022Context.SaveChangesAsync();
                    return (lineaCarritoToAdd);
                }
            }
            catch (Exception)
            {
                List<LineaCarritoModel> lineasRepetidas = (await GetLineaCarritos()).FindAll(e => e.CarritoLineaCarrito == lineaCarrito.CarritoLineaCarrito && e.ProductoLineaCarrito == lineaCarrito.ProductoLineaCarrito);
                if (lineasRepetidas.Count > 1)
                {
                    foreach (var lineaRepe in lineasRepetidas)
                    {
                        await EliminarLinea(lineaRepe);
                    }
                    LineaCarrito lineaCombinada = new LineaCarrito
                    {
                        LineaCarritoId = lineasRepetidas.First().LineaCarritoId,
                        CarritoLineaCarrito = lineasRepetidas.First().CarritoLineaCarrito,
                        ProductoLineaCarrito = lineasRepetidas.First().ProductoLineaCarrito,
                        Cantidad = 0
                    };
                    foreach (var lineaRepe in lineasRepetidas)
                    {
                        lineaCombinada.Cantidad = lineaCombinada.Cantidad + lineaRepe.Cantidad;
                    }
                    var result = await this.tfg2022Context.LineaCarritos.AddAsync(lineaCombinada);
                    await this.tfg2022Context.SaveChangesAsync();
                    return (result.Entity);
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task Aumentar1CantidadLinea(LineaCarritoModel lineaCarrito)
        {
            try
            {
                var lineaCarritoExistente = await this.tfg2022Context.LineaCarritos.FindAsync(lineaCarrito.LineaCarritoId);
                if (lineaCarritoExistente != null)
                {
                    lineaCarritoExistente.Cantidad = lineaCarrito.Cantidad + 1;
                    await this.tfg2022Context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Disminuir1CantidadLinea(LineaCarritoModel lineaCarrito)
        {
            try
            {
                var lineaCarritoExistente = await this.tfg2022Context.LineaCarritos.FindAsync(lineaCarrito.LineaCarritoId);
                if (lineaCarritoExistente != null)
                {
                    if (lineaCarrito.Cantidad > 1)
                    {
                        lineaCarritoExistente.Cantidad = lineaCarrito.Cantidad - 1;
                        await this.tfg2022Context.SaveChangesAsync();
                    }
                    else
                    {
                        await EliminarLinea(lineaCarrito);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task EliminarLinea(LineaCarritoModel lineaCarrito)
        {
            try
            {
                var linea = await this.tfg2022Context.LineaCarritos.FindAsync(lineaCarrito.LineaCarritoId);
                if (linea != null)
                {
                    this.tfg2022Context.LineaCarritos.Remove(linea);
                    await this.tfg2022Context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task DeleteAllLineasByCarrito(int carrito)
        {

            try
            {
                var lineas = this.tfg2022Context.LineaCarritos.Where(lc => lc.CarritoLineaCarrito == carrito);
                foreach (var linea in lineas)
                {
                    this.tfg2022Context.LineaCarritos.Remove(linea);
                }
                await this.tfg2022Context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
