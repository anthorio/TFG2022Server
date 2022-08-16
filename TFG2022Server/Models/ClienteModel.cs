using System.ComponentModel.DataAnnotations;

namespace TFG2022Server.Models
{
    public class ClienteModel
    {
        [Required]
        public int ClienteId { get; set; }
        [Required]
        public string Direccion { get; set; } = null!;
        [Required]
        public string Poblacion { get; set; } = null!;
        [Required]
        public int CodigoPostal { get; set; }
        [Required]
        public int Descuento { get; set; }
        [Required]
        public int UsuarioIdCliente { get; set; }
    }
}
