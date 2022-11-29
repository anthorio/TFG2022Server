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
        [RegularExpression(@"^(0|[1-9]\d*)$", ErrorMessage = "La {0} tiene que ser de al menos 1.")]
        public int Cantidad { get; set; }
        [RegularExpression(@"^(0|[1-9]\d*)$", ErrorMessage = "La {0} tiene que ser de al menos 1.")]
        public int StockMinimo { get; set; }
        [Required]
        [RegularExpression(@"^(([1-9]\d*)|0)(\,\d{2})?", ErrorMessage = "El {0} no es correcto.")]
        public double Precio { get; set; }
        public string? UrlImagen { get; set; }
    }
}
