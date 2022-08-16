using System.ComponentModel.DataAnnotations;

namespace TFG2022Server.Models
{
    public class LineaAlbaranModel
    {
        [Required]
        public int LineaAlbaranId { get; set; }
        [Required]
        public int AlbaranLineaAlbaran { get; set; }
        [Required]
        public int LineaPedidoLineaAlbaran { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required]
        public double Importe { get; set; }

    }
}
