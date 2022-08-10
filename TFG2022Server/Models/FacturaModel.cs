namespace TFG2022Server.Models
{
    public class FacturaModel
    {
        public int FacturaId { get; set; }
        public int PedidoFactura { get; set; }
        public string? InfoPedido { get; set; }
        public DateTime FechaFactura { get; set; }
        public int Iva { get; set; }
        public double Total { get; set; }
        public string EstadoFactura { get; set; }
    }
}
