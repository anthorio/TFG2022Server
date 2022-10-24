using TFG2022Server.Entities;
using TFG2022Server.Models;

namespace TFG2022Server.Services.Contracts
{
    public interface ICarritoService
    {
        Task<List<CarritoModel>> GetCarritos();
        Task<Carrito> GetCarritoByUser(int user);
    }
}
