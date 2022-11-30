using System.ComponentModel.DataAnnotations;

namespace TFG2022Server.Models
{
    public class PagoModel
    {
        [Required]
        public int PagoId { get; set; }
        [Required]
        public int FacturaPago { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        [RegularExpression(@"^(([1-9]\d*)|0)([\,|\.]?\d{1,2})?", ErrorMessage = "La {0} no es correcta.")]
        public double Cantidad { get; set; }
        public string? Observaciones { get; set; }
    }
}
