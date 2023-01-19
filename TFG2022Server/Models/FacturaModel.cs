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
        public DateTime FechaFactura { get; set; } = DateTime.Now;
        [Required]
        [RegularExpression(@"^(([1-9]\d*)|0)([\,|\.]?\d{1,2})?", ErrorMessage = "El {0} no es correcto.")]
        public double Total { get; set; }
        [Required]
        public string EstadoFactura { get; set; }
    }
}
