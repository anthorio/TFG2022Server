using System.ComponentModel.DataAnnotations;

namespace TFG2022Server.Models
{
    public class ProveedorModel
    {
        public int ProveedorId { get; set; }
        [StringLength(100, ErrorMessage = "The {0} value cannot exceed {1} characters or be less than {2} characters.", MinimumLength = 2)]
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string Direccion { get; set; }
        public int CodigoPostal { get; set; }
        public int Telefono { get; set; }
        public string? Email { get; set; }
    }
}
