using TFG2022Server.Entities;
using TFG2022Server.Models;

namespace TFG2022Server.Services.Contracts
{
    public interface IFamiliaProductoManagementService
    {
        Task<List<FamiliaProductoModel>> GetFamiliaProductos();
    }
}
