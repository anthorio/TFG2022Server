using System.ComponentModel.DataAnnotations;

namespace TFG2022Server.Entities
{
    public class Carrito
    {
        [Key]
        public int CarritoId { get; set; }
        public int UsuarioCarrito { get; set; }
    }
}
