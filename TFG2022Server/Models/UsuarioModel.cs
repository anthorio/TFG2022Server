using System.ComponentModel.DataAnnotations;

namespace TFG2022Server.Models
{
    public class UsuarioModel
    {
        [Required]
        public int UsuarioId { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} value cannot exceed {1} characters or be less than {2} characters.", MinimumLength = 2)]
        public string Nombre { get; set; } = null!;
        [Required]
        [StringLength(100, ErrorMessage = "The {0} value cannot exceed {1} characters or be less than {2} characters.", MinimumLength = 2)]
        public string Apellidos { get; set; } = null!;
        [Required]
        public string Rol { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Contraseña { get; set; } = null!;
        [Required]
        [StringLength(9, ErrorMessage = "The {0} value must have 9 characters.", MinimumLength = 9)]
        public string Telefono { get; set; } = null!;
        [Required]
        [StringLength(9, ErrorMessage = "The {0} value must have 9 characters.", MinimumLength = 9)]
        public string Dni { get; set; } = null!;
        [Required]
        public DateTime FechaNacimiento { get; set; }
        [Required]
        public string Direccion { get; set; } = null!;
        [Required]
        public string Poblacion { get; set; } = null!;
        [Required]
        [StringLength(5, ErrorMessage = "The {0} value must have 5 characters.", MinimumLength = 5)]
        public string CodigoPostal { get; set; } = null!;
        public int Descuento { get; set; }

    }
}
