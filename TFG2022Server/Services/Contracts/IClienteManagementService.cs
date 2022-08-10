using TFG2022Server.Entities;
using TFG2022Server.Models;

namespace TFG2022Server.Services.Contracts
{
    public interface IClienteManagementService
    {
        Task<List<ClienteModel>> GetClientes();
        Task<List<Usuario>> GetUsuarios();
    }
}
