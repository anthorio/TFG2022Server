namespace TFG2022Server.Models
{
    public class LineaPedidoModel
    {
        public int LineaPedidoId { get; set; }
        public int PedidoLineaPedido { get; set; }
        public int ProductoLineaPedido { get; set; }
        public int Cantidad { get; set; }
        public double PrecioFinal { get; set; }
    }
}
