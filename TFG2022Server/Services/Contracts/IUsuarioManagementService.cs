using TFG2022Server.Models;

namespace TFG2022Server.Services.Contracts
{
    public interface IUsuarioManagementService
    {
        Task<List<UsuarioModel>> GetUsuarios();
    }
}
