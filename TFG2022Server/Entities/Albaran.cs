using System.ComponentModel.DataAnnotations;

namespace TFG2022Server.Entities
{
    public class Albaran
    {
        [Key]
        public int AlbaranId { get; set; }
        public int PedidoAlbaran { get; set; }
        public DateTime FechaEntrega { get; set; }
        public int ProductoAlbaran { get; set; }
        public int CantidadProductoAlbaran { get; set; }
    }
}
