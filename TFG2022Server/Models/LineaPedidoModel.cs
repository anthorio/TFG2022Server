using System.ComponentModel.DataAnnotations;

namespace TFG2022Server.Models
{
    public class LineaPedidoModel
    {
        [Required]
        public int LineaPedidoId { get; set; }
        [Required]
        public int PedidoLineaPedido { get; set; }
        [Required]
        public int ProductoLineaPedido { get; set; }
        [Required]
        [RegularExpression(@"^(0|[1-9]\d*)$", ErrorMessage = "La {0} tiene que ser de al menos 1.")]
        public int Cantidad { get; set; }
        [Required]
        [RegularExpression(@"^(([1-9]\d*)|0)([\,|\.]?\d{1,2})?", ErrorMessage = "El {0} no es correcto.")]
        public double PrecioFinal { get; set; }
    }
}
