using System.ComponentModel.DataAnnotations;

namespace TFG2022Server.Models
{
    public class ProductoModel
    {
        [Required]
        public int ProductoId { get; set; }
        [Required]
        public int FamiliaProductoProducto { get; set; }
        [Required]
        public int ProveedorProducto { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} value cannot exceed {1} characters or be less than {2} characters.", MinimumLength = 2)]
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required]
        public double Precio { get; set; }
        public string? UrlImagen { get; set; }
    }
}
