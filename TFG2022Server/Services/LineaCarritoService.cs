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

                var result = await this.tfg2022Context.LineaCarritos.AddAsync(lineaCarritoToAdd);
                await this.tfg2022Context.SaveChangesAsync();
                return (result.Entity);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
