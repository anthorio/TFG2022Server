using System.ComponentModel.DataAnnotations;

namespace TFG2022Server.Entities
{
    public class LineaCarrito
    {
        [Key]
        public int LineaCarritoId { get; set; }
        public int CarritoLineaCarrito { get; set; }
        public int ProductoLineaCarrito { get; set; }
        public int Cantidad { get; set; }
    }
}
