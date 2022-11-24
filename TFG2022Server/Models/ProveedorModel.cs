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
        [RegularExpression(@"^[0-9]{1,5}$", ErrorMessage = "El {0} tiene que estar compuesto mínimo de 5 números.")]
        public int CodigoPostal { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]{9}$", ErrorMessage = "El {0} tiene que estar compuesto de 9 números.")]
        public int Telefono { get; set; }
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "El {0} no encaja con una dirección de correo estándar.")]
        public string? Email { get; set; }
    }
}
