using Microsoft.EntityFrameworkCore;
using TFG2022Server.Data;
using TFG2022Server.Entities;
using TFG2022Server.Extensions;
using TFG2022Server.Models;
using TFG2022Server.Services.Contracts;

namespace TFG2022Server.Services
{
    public class FamiliaProductoService : IFamiliaProductoService
    {
        private readonly TFG2022Context tfg2022Context;

        public FamiliaProductoService(TFG2022Context tfg2022Context)
        {
            this.tfg2022Context = tfg2022Context;
        }

        public async Task<List<FamiliaProductoModel>> GetFamiliaProductos()
        {
            try
            {
                return await this.tfg2022Context.FamiliaProductos.Convert();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<FamiliaProducto> GetFamiliaProducto(int fId)
        {
            try
            {
                var familia = await tfg2022Context.FamiliaProductos.FindAsync(fId);
                return familia;

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
