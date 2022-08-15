using TFG2022Server.Models;

namespace TFG2022Server.Services.Contracts
{
    public interface IPedidoManagementService
    {
        Task<List<PedidoModel>> GetPedidos();
        string[] GetTiposPedido();
    }
}
