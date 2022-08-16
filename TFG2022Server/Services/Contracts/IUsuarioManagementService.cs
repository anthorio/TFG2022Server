using TFG2022Server.Entities;
using TFG2022Server.Models;

namespace TFG2022Server.Services.Contracts
{
    public interface IUsuarioManagementService
    {
        Task<List<UsuarioModel>> GetUsuarios();
        string[] GetRoles();
        Task<Usuario> AddUsuario(UsuarioModel usuarioMod);
        Task UpdateUsuario(UsuarioModel usuarioMod);
        Task DeleteUsuario(int id);
    }
}
