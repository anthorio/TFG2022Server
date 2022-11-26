using System.ComponentModel.DataAnnotations;

namespace TFG2022Server.Models
{
    public class UsuarioModel
    {
        [Required]
        public int UsuarioId { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "El valor {0} No puede excederse de los {1} carácteres o ser menos de {2} carácteres.", MinimumLength = 2)]
        public string Nombre { get; set; } = null!;
        [Required]
        [StringLength(100, ErrorMessage = "El valor {0} No puede excederse de los {1} carácteres o ser menos de {2} carácteres.", MinimumLength = 2)]
        public string Apellidos { get; set; } = null!;
        [Required]
        public string Rol { get; set; } = null!;
        [Required]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "El {0} no encaja con una dirección de correo estándar.")]
        public string Email { get; set; } = null!;
        [Required]
        public string Contraseña { get; set; } = null!;
        [Required]
        [RegularExpression(@"^[0-9]{9}$", ErrorMessage = "El {0} tiene que estar compuesto de 9 números.")]
        public string Telefono { get; set; } = null!;
        [Required]
        [RegularExpression(@"^[0-9]{8,8}[A-Za-z]$", ErrorMessage = "El {0} no encaja con un DNI correcto.")]
        public string Dni { get; set; } = null!;
        [Required]
        public DateTime FechaNacimiento { get; set; } = DateTime.Now;
        [Required]
        public string Direccion { get; set; } = null!;
        [Required]
        public string Poblacion { get; set; } = null!;
        [Required]
        [RegularExpression(@"^[0-9]{1,5}$", ErrorMessage = "El {0} tiene que estar compuesto mínimo de 5 números.")]
        public string CodigoPostal { get; set; } = null!;
        [RegularExpression(@"^([0-9]|[1-9][0-9]|100)$", ErrorMessage = "El {0} tiene que ser un número del 0 al 100.")]
        public int Descuento { get; set; } = 0;
    }
}
