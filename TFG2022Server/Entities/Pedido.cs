using System.ComponentModel.DataAnnotations;

namespace TFG2022Server.Entities
{
    public class Pedido
    {
        [Key]
        public int PedidoId { get; set; }
        public int UsuarioPedido { get; set; }
        public DateTime FechaPedido { get; set; }
        public string EstadoPedido { get; set; }
        public string TipoEnvio { get; set; }
        public double PrecioTotal { get; set; }
        public int CantidadTotal { get; set; }
    }
}
