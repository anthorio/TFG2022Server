using TFG2022Server.Entities;
using TFG2022Server.Models;

namespace TFG2022Server.Services.Contracts
{
    public interface IProductoService
    {
        Task<List<ProductoModel>> GetProductos();
        Task<Producto> GetProducto(int id);
        Task<Producto> AddProducto(ProductoModel productoMod);
        Task UpdateProducto(ProductoModel productoMod);
        Task DeleteProducto(int id);
        Task UpdateCantidadProducto(int productoid, int cantidad);
    }
}
