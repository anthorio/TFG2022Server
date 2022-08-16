using System.ComponentModel.DataAnnotations;

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
        [Required]
        public string TipoEnvio { get; set; }
    }
}
