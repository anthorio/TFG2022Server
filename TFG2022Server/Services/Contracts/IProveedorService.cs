using TFG2022Server.Entities;
using TFG2022Server.Models;

namespace TFG2022Server.Services.Contracts
{
    public interface IProveedorService
    {
        Task<List<ProveedorModel>> GetProveedores();
        Task<Proveedor> AddProveedor(ProveedorModel proveedorMod);
        Task UpdateProveedor(ProveedorModel proveedorMod);
        Task DeleteProveedor(int id);
    }
}
