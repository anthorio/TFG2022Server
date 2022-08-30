using Microsoft.EntityFrameworkCore;
using TFG2022Server.Data;
using TFG2022Server.Entities;
using TFG2022Server.Extensions;
using TFG2022Server.Models;
using TFG2022Server.Services.Contracts;

namespace TFG2022Server.Services
{
    public class AlbaranService : IAlbaranService
    {
        private readonly TFG2022Context tfg2022Context;

        public AlbaranService(TFG2022Context tfg2022Context)
        {
            this.tfg2022Context = tfg2022Context;
        }

        public async Task<List<AlbaranModel>> GetAlbaranes()
        {
            try
            {
                return await this.tfg2022Context.Albaranes.Convert();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
