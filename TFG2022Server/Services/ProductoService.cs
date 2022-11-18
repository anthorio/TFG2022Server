using Microsoft.EntityFrameworkCore;
using TFG2022Server.Data;
using TFG2022Server.Entities;
using TFG2022Server.Extensions;
using TFG2022Server.Models;
using TFG2022Server.Services.Contracts;

namespace TFG2022Server.Services
{
    public class ProductoService : IProductoService
    {
        private readonly TFG2022Context tfg2022Context;

        public ProductoService(TFG2022Context tfg2022Context)
        {
            this.tfg2022Context = tfg2022Context;
        }

        public async Task<List<ProductoModel>> GetProductos()
        {
            try
            {
                //return await this.tfg2022Context.Productos.Convert();
                return await this.tfg2022Context.Productos.Convert(tfg2022Context);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Producto> GetProducto(int productId)
        {
            try
            {
                return await tfg2022Context.Productos.FindAsync(productId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
