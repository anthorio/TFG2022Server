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

        public List<string> GetRoles()
        {
            List<string> lista= new List<string>();
            lista.Add("1");
            lista.Add("2");
            lista.Add("3");
            return lista;
        }

        public async Task<List<UsuarioModel>> GetUsuarios()
        {
            try
            {
                return await this.tfg2022Context.Usuarios.Convert();
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
