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
        public int Cantidad { get; set; }
        [Required]
        public double PrecioFinal { get; set; }
    }
}
