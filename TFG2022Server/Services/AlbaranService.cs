using Microsoft.EntityFrameworkCore;
using TFG2022Server.Data;
using TFG2022Server.Entities;
using TFG2022Server.Extensions;
using TFG2022Server.Models;
using TFG2022Server.Services.Contracts;

namespace TFG2022Server.Services
{
    public class AlbaranService : IAlbaranService
    {
        private readonly TFG2022Context tfg2022Context;

        public AlbaranService(TFG2022Context tfg2022Context)
        {
            this.tfg2022Context = tfg2022Context;
        }

        public async Task<Albaran> AddAlbaran(AlbaranModel albaran)
        {
            try
            {
                Albaran albaranToAdd = albaran.Convert();
                var result = await this.tfg2022Context.Albaranes.AddAsync(albaranToAdd);
                await this.tfg2022Context.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAlbaran(int id)
        {
            try
            {
                var albaran = await this.tfg2022Context.Albaranes.FindAsync(id);
                if (albaran != null)
                {
                    this.tfg2022Context.Albaranes.Remove(albaran);
                    await this.tfg2022Context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<AlbaranModel>> GetAlbaranes()
        {
            try
            {
                return await this.tfg2022Context.Albaranes.Convert();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAlbaran(AlbaranModel albaran)
        {
            try
            {
                var albaranToUpdate = await this.tfg2022Context.Albaranes.FindAsync(albaran.AlbaranId);

                if (albaranToUpdate != null)
                {
                    albaranToUpdate.ProductoAlbaran = albaran.ProductoAlbaran;
                    albaranToUpdate.PedidoAlbaran = albaran.PedidoAlbaran;
                    albaranToUpdate.FechaEntrega = albaran.FechaEntrega;
                    albaranToUpdate.CantidadProductoAlbaran = albaran.CantidadProductoAlbaran;
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
