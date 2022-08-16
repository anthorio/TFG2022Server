using System.ComponentModel.DataAnnotations;

namespace TFG2022Server.Models
{
    public class ProveedorModel
    {
        [Required]
        public int ProveedorId { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} value cannot exceed {1} characters or be less than {2} characters.", MinimumLength = 2)]
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        [StringLength(5, ErrorMessage = "The {0} value must have 5 characters.", MinimumLength = 5)]
        public int CodigoPostal { get; set; }
        [Required]
        public int Telefono { get; set; }
        public string? Email { get; set; }
    }
}
