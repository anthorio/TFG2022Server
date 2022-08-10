using Microsoft.EntityFrameworkCore;
using TFG2022Server.Data;
using TFG2022Server.Entities;
using TFG2022Server.Extensions;
using TFG2022Server.Models;
using TFG2022Server.Services.Contracts;

namespace TFG2022Server.Services
{
    public class LineaPedidoManagementService : ILineaPedidoManagementService
    {
        private readonly TFG2022Context tfg2022Context;

        public LineaPedidoManagementService(TFG2022Context tfg2022Context)
        {
            this.tfg2022Context = tfg2022Context;
        }

        public async Task<List<LineaPedidoModel>> GetLineaPedidos()
        {
            try
            {
                return await this.tfg2022Context.LineaPedidos.Convert();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
