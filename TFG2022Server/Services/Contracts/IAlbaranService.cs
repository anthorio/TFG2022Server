using TFG2022Server.Entities;
using TFG2022Server.Models;

namespace TFG2022Server.Services.Contracts
{
    public interface IAlbaranService
    {
        Task<List<AlbaranModel>> GetAlbaranes();
        // Task<List<Pedido>> GetPedidos();
        Task<Albaran> AddAlbaran(AlbaranModel albaran);
        Task UpdateAlbaran(AlbaranModel albaran);
        Task DeleteAlbaran(int id);
    }
}
