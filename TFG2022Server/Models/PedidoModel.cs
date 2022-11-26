using System.ComponentModel.DataAnnotations;
using TFG2022Server.Entities;

namespace TFG2022Server.Models
{
    public class PedidoModel
    {
        [Required]
        public int PedidoId { get; set; }
        [Required]
        public int UsuarioPedido { get; set; }
        [Required]
        public DateTime FechaPedido { get; set; }
        [Required]
        public string EstadoPedido { get; set; }
        public List<LineaPedido> LineasPedido { get; set; }
        [RegularExpression(@"^(([1-9]\d*)|0)(\,\d{2})?", ErrorMessage = "El {0} no es correcto.")]
        public double PrecioTotal { get; set; }
        [RegularExpression(@"^(0|[1-9]\d*)", ErrorMessage = "La {0} tiene que ser de al menos 1.")]
        public int CantidadTotal { get; set; }
    }
}