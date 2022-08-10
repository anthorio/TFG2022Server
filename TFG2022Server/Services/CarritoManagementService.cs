using Microsoft.EntityFrameworkCore;
using TFG2022Server.Data;
using TFG2022Server.Entities;
using TFG2022Server.Extensions;
using TFG2022Server.Models;
using TFG2022Server.Services.Contracts;

namespace TFG2022Server.Services
{
    public class CarritoManagementService : ICarritoManagementService
    {
        private readonly TFG2022Context tfg2022Context;

        public CarritoManagementService(TFG2022Context tfg2022Context)
        {
            this.tfg2022Context = tfg2022Context;
        }

        public async Task<List<CarritoModel>> GetCarritos()
        {
            try
            {
                return await this.tfg2022Context.Carritos.Convert();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
