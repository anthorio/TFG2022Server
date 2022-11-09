using TFG2022Server.Entities;
using TFG2022Server.Models;

namespace TFG2022Server.Services.Contracts
{
    public interface ILineaCarritoService
    {
        Task<List<LineaCarritoModel>> GetLineaCarritos();
        Task<LineaCarrito> AnadirLinea(LineaCarritoModel lineaCarrito);
        Task Aumentar1CantidadLinea(LineaCarritoModel lineaCarrito);
        Task Disminuir1CantidadLinea(LineaCarritoModel lineaCarrito);
        Task EliminarLinea(LineaCarritoModel lineaCarrito);
        Task DeleteAllLineasByCarrito(int carrito);

    }
}
