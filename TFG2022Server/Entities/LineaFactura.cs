using System.ComponentModel.DataAnnotations;

namespace TFG2022Server.Entities
{
    public class LineaFactura
    {
        [Key]
        public int LineaFacturaId { get; set; }
        public int ProductoLineaFactura { get; set; }
        public int FacturaLineaFactura { get; set; }
        public int Cantidad { get; set; }
        public double Importe { get; set; }
    }
}
