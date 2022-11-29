using TFG2022Server.Entities;
using TFG2022Server.Models;

namespace TFG2022Server.Services.Contracts
{
    public interface IFacturaService
    {
        Task<List<FacturaModel>> GetFacturas();
        string[] GetEstadosFactura();
        Task<Factura> CreateFacturaFromPedido(Pedido pedido);
        Task UpdateFactura(FacturaModel factura);

    }
}
