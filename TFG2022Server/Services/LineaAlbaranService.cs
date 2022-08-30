using Microsoft.EntityFrameworkCore;
using TFG2022Server.Data;
using TFG2022Server.Entities;
using TFG2022Server.Extensions;
using TFG2022Server.Models;
using TFG2022Server.Services.Contracts;

namespace TFG2022Server.Services
{
    public class LineaAlbaranService : ILineaAlbaranService
    {
        private readonly TFG2022Context tfg2022Context;

        public LineaAlbaranService(TFG2022Context tfg2022Context)
        {
            this.tfg2022Context = tfg2022Context;
        }

        public async Task<List<LineaAlbaranModel>> GetLineaAlbaranes()
        {
            try
            {
                return await this.tfg2022Context.LineaAlbaranes.Convert();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
