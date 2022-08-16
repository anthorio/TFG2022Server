using System.ComponentModel.DataAnnotations;

namespace TFG2022Server.Models
{
    public class LineaFacturaModel
    {
        [Required]
        public int LineaFacturaId { get; set; }
        [Required]
        public int ProductoLineaFactura { get; set; }
        [Required]
        public int FacturaLineaFactura { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required]
        public double Importe { get; set; }
    }
}
