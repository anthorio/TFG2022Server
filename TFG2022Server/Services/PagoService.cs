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
    }
}
