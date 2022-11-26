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
        [RegularExpression(@"^(0|[1-9]\d*)$", ErrorMessage = "La {0} tiene que ser de al menos 1.")]
        public int Cantidad { get; set; }
    }
}
