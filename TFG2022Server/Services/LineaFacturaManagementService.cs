using Microsoft.EntityFrameworkCore;
using TFG2022Server.Data;
using TFG2022Server.Entities;
using TFG2022Server.Extensions;
using TFG2022Server.Models;
using TFG2022Server.Services.Contracts;

namespace TFG2022Server.Services
{
    public class LineaFacturaManagementService : ILineaFacturaManagementService
    {
        private readonly TFG2022Context tfg2022Context;

        public LineaFacturaManagementService(TFG2022Context tfg2022Context)
        {
            this.tfg2022Context = tfg2022Context;
        }

        public async Task<List<LineaFacturaModel>> GetLineaFacturas()
        {
            try
            {
                return await this.tfg2022Context.LineaFacturas.Convert();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
