using TFG2022Server.Models;

namespace TFG2022Server.Services.Contracts
{
    public interface ICarritoManagementService
    {
        Task<List<CarritoModel>> GetCarritos();
    }
}
