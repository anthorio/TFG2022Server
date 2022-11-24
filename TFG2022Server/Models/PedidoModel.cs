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
        public double PrecioTotal { get; set; }
        public int CantidadTotal { get; set; }
    }
}