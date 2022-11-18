using TFG2022Server.Entities;
using TFG2022Server.Models;

namespace TFG2022Server.Services.Contracts
{
    public interface IPedidoService
    {
        Task<List<PedidoModel>> GetPedidos();
        string[] GetTiposPedido();
        Task<Pedido> CreatePedido(PedidoModel pedidoModel);
        double GetCosteEnvio();
        Task<List<PedidoModel>> GetPedidosFromUser(int user);
    }
}
