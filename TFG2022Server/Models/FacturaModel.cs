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
        [RegularExpression(@"^([0-9]|[1-9][0-9]|100)$", ErrorMessage = "Introduce el {0} como un número entero del 0 al 100.")]
        public int Iva { get; set; }
        [Required]
        [RegularExpression(@"^(([1-9]\d*)|0)(\,\d{2})?", ErrorMessage = "El {0} no es correcto.")]
        public double Total { get; set; }
        [Required]
        public string EstadoFactura { get; set; }
    }
}
