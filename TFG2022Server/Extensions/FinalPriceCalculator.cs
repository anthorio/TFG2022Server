using TFG2022Server.Entities;
using TFG2022Server.Models;
using TFG2022Server.Data;

namespace TFG2022Server.Extensions
{
    public class FinalPriceCalculator
    {
        public static double FinalPrice(double precio, int IVA)
        {
            return Math.Round(precio + (precio * IVA * 0.01), 2);
        }
        public static double FinalPrice(Producto producto)
        {
            return Math.Round(producto.Precio + (producto.Precio * producto.Iva * 0.01), 2);
        }
        public static double FinalPrice(ProductoModel producto)
        {
            return Math.Round(producto.Precio + (producto.Precio * producto.Iva * 0.01), 2);
        }
    }
}
