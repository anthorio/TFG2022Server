using Microsoft.EntityFrameworkCore;
using TFG2022Server.Data;
using TFG2022Server.Entities;
using TFG2022Server.Extensions;
using TFG2022Server.Models;
using TFG2022Server.Services.Contracts;

namespace TFG2022Server.Services
{
    public class FacturaManagementService : IFacturaManagementService
    {
        private readonly TFG2022Context tfg2022Context;

        public FacturaManagementService(TFG2022Context tfg2022Context)
        {
            this.tfg2022Context = tfg2022Context;
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
    }
}
