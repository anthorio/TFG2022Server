using Microsoft.EntityFrameworkCore;
using TFG2022Server.Data;
using TFG2022Server.Extensions;
using TFG2022Server.Models;
using TFG2022Server.Services.Contracts;

namespace TFG2022Server.Services
{
    public class UsuarioManagementService : IUsuarioManagementService
    {
        private readonly TFG2022Context tfg2022Context;

        public UsuarioManagementService(TFG2022Context tfg2022Context)
        {
            this.tfg2022Context = tfg2022Context;
        }

        public string[] GetRoles()
        {
            return Constants.UsuarioRoles;
        }

        public async Task<List<UsuarioModel>> GetUsuarios()
        {
            try
            {
                return await this.tfg2022Context.Usuarios.Convert();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
