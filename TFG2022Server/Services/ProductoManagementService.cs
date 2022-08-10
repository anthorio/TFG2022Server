using Microsoft.EntityFrameworkCore;
using TFG2022Server.Data;
using TFG2022Server.Entities;
using TFG2022Server.Extensions;
using TFG2022Server.Models;
using TFG2022Server.Services.Contracts;

namespace TFG2022Server.Services
{
    public class ProductoManagementService : IProductoManagementService
    {
        private readonly TFG2022Context tfg2022Context;

        public ProductoManagementService(TFG2022Context tfg2022Context)
        {
            this.tfg2022Context = tfg2022Context;
        }

        public async Task<List<ProductoModel>> GetProductos()
        {
            try
            {
                return await this.tfg2022Context.Productos.Convert();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
