using Microsoft.EntityFrameworkCore;
using TFG2022Server.Data;
using TFG2022Server.Entities;
using TFG2022Server.Extensions;
using TFG2022Server.Models;
using TFG2022Server.Services.Contracts;

namespace TFG2022Server.Services
{
    public class ProveedorManagementService : IProveedorManagementService
    {
        private readonly TFG2022Context tfg2022Context;

        public ProveedorManagementService(TFG2022Context tfg2022Context)
        {
            this.tfg2022Context = tfg2022Context;
        }

        public async Task<List<ProveedorModel>> GetProveedores()
        {
            try
            {
                return await this.tfg2022Context.Proveedores.Convert();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
