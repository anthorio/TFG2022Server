using System.ComponentModel.DataAnnotations;

namespace TFG2022Server.Entities
{
    public class LineaPedido
    {
        [Key]
        public int LineaPedidoId { get; set; }
        public int PedidoLineaPedido { get; set; }
        public int ProductoLineaPedido { get; set; }
        public int Cantidad { get; set; }
        public double PrecioFinal { get; set; }
    }
}
