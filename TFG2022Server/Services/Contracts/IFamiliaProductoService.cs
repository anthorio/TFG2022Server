using TFG2022Server.Entities;
using TFG2022Server.Models;

namespace TFG2022Server.Services.Contracts
{
    public interface IFamiliaProductoService
    {
        Task<List<FamiliaProductoModel>> GetFamiliaProductos();
        Task<FamiliaProducto> GetFamiliaProducto(int id);
        Task<FamiliaProducto> AddFamilia(FamiliaProductoModel fproductoMod);
        Task UpdateFamilia(FamiliaProductoModel fproductoMod);
        Task DeleteFamilia(int id);
    }
}
