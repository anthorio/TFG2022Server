using System.ComponentModel.DataAnnotations;

namespace TFG2022Server.Models
{
    public class LineaCarritoModel
    {
        [Required]
        public int LineaCarritoId { get; set; }
        [Required]
        public int CarritoLineaCarrito { get; set; }
        [Required]
        public int ProductoLineaCarrito { get; set; }
        [Required]
        public int Cantidad { get; set; }
    }
}
