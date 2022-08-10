using TFG2022Server.Models;

namespace TFG2022Server.Services.Contracts
{
    public interface IPagoManagementService
    {
        Task<List<PagoModel>> GetPagos();
    }
}
