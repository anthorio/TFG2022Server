using System.ComponentModel.DataAnnotations;

namespace TFG2022Server.Models
{
    public class FacturaModel
    {
        [Required]
        public int FacturaId { get; set; }
        [Required]
        public int PedidoFactura { get; set; }
        public string? InfoPedido { get; set; }
        [Required]
        public DateTime FechaFactura { get; set; }
        [Required]
        public int Iva { get; set; }
        [Required]
        public double Total { get; set; }
        [Required]
        public string EstadoFactura { get; set; }
    }
}
