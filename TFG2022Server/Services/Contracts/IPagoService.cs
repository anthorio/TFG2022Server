using TFG2022Server.Entities;
using TFG2022Server.Models;

namespace TFG2022Server.Services.Contracts
{
    public interface IPagoService
    {
        Task<List<PagoModel>> GetPagos();
        Task<Pago> AddPago(PagoModel pagoM);
        Task UpdatePago(PagoModel pagoM);
        Task DeletePago(int id);
    }
}
