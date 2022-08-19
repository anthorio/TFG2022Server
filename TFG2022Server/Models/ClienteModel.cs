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
        [StringLength(5, ErrorMessage = "The {0} value must have 5 characters.", MinimumLength = 5)]
        public string CodigoPostal { get; set; } = null!;
        [Required]
        public int Descuento { get; set; }
        [Required]
        public int UsuarioIdCliente { get; set; }
    }
}
