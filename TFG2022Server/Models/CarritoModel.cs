using System.ComponentModel.DataAnnotations;

namespace TFG2022Server.Models
{
    public class CarritoModel
    {
        [Required]
        public int CarritoId { get; set; }
        [Required]
        public int UsuarioCarrito { get; set; }
    }
}
