namespace TFG2022Server.Models
{
    public class PedidoModel
    {
        public int PedidoId { get; set; }
        public int UsuarioPedido { get; set; }
        public DateTime FechaPedido { get; set; }
        public string EstadoPedido { get; set; }
        public string TipoEnvio { get; set; }
    }
}
