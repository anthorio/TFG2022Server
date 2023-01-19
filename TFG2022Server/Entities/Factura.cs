using System.ComponentModel.DataAnnotations;

namespace TFG2022Server.Entities
{
    public class Factura
    {
        [Key]
        public int FacturaId { get; set; }
        public int PedidoFactura { get; set; }
        public string? InfoPedido { get; set; }
        public DateTime FechaFactura { get; set; }
        public double Total { get; set; }
        public string EstadoFactura { get; set; }
    }
}
