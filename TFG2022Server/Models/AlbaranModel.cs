using System.ComponentModel.DataAnnotations;

namespace TFG2022Server.Models
{
    public class AlbaranModel
    {

        [Required]
        public int AlbaranId { get; set; }
        [Required]
        public int PedidoAlbaran { get; set; }
        [Required]
        public DateTime FechaEntrega { get; set; }
    }
}
