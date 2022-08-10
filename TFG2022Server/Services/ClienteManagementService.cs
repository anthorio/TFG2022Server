using Microsoft.EntityFrameworkCore;
using TFG2022Server.Data;
using TFG2022Server.Entities;
using TFG2022Server.Extensions;
using TFG2022Server.Models;
using TFG2022Server.Services.Contracts;

namespace TFG2022Server.Services
{
    public class ClienteManagementService : IClienteManagementService
    {
        private readonly TFG2022Context tfg2022Context;

        public ClienteManagementService(TFG2022Context tfg2022Context)
        {
            this.tfg2022Context = tfg2022Context;
        }

        public async Task<List<ClienteModel>> GetClientes()
        {
            try
            {
                return await this.tfg2022Context.Clientes.Convert();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Usuario>> GetUsuarios()
        {

            try
            {
                return await this.tfg2022Context.Usuarios.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
