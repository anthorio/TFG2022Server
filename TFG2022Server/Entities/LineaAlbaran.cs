using System.ComponentModel.DataAnnotations;

namespace TFG2022Server.Entities
{
    public class LineaAlbaran
    {
        [Key]
        public int LineaAlbaranId { get; set; }
        public int AlbaranLineaAlbaran { get; set; }
        public int LineaPedidoLineaAlbaran { get; set; }
        public int Cantidad { get; set; }
        public double Importe { get; set; }
    }
}
