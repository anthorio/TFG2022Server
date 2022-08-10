using Microsoft.EntityFrameworkCore;
using TFG2022Server.Data;
using TFG2022Server.Entities;
using TFG2022Server.Extensions;
using TFG2022Server.Models;
using TFG2022Server.Services.Contracts;

namespace TFG2022Server.Services
{
    public class LineaCarritoManagementService : ILineaCarritoManagementService
    {
        private readonly TFG2022Context tfg2022Context;

        public LineaCarritoManagementService(TFG2022Context tfg2022Context)
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
    }
}
