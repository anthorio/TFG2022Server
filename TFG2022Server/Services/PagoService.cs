using Microsoft.EntityFrameworkCore;
using TFG2022Server.Data;
using TFG2022Server.Entities;
using TFG2022Server.Extensions;
using TFG2022Server.Models;
using TFG2022Server.Services.Contracts;

namespace TFG2022Server.Services
{
    public class PagoService : IPagoService
    {
        private readonly TFG2022Context tfg2022Context;

        public PagoService(TFG2022Context tfg2022Context)
        {
            this.tfg2022Context = tfg2022Context;
        }

        public async Task<Pago> AddPago(PagoModel pagoM)
        {
            try
            {
                Pago pagoToAdd = pagoM.Convert();
                var result = await this.tfg2022Context.Pagos.AddAsync(pagoToAdd);
                await this.tfg2022Context.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeletePago(int id)
        {
            try
            {
                var producto = await this.tfg2022Context.Pagos.FindAsync(id);
                if (producto != null)
                {
                    this.tfg2022Context.Pagos.Remove(producto);
                    await this.tfg2022Context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<PagoModel>> GetPagos()
        {
            try
            {
                return await this.tfg2022Context.Pagos.Convert();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdatePago(PagoModel pagoM)
        {
            try
            {
                var pagoToUpdate = await this.tfg2022Context.Pagos.FindAsync(pagoM.PagoId);

                if (pagoToUpdate != null)
                {
                    pagoToUpdate.FacturaPago = pagoM.FacturaPago;
                    pagoToUpdate.Fecha = pagoM.Fecha;
                    pagoToUpdate.Cantidad = pagoM.Cantidad;
                    pagoToUpdate.Observaciones = pagoM.Observaciones;
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
