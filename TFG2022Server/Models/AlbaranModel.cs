using System.ComponentModel.DataAnnotations;

namespace TFG2022Server.Models
{
    public class AlbaranModel : IValidatableObject
    {

        [Required]
        public int AlbaranId { get; set; }
        public int PedidoAlbaran { get; set; }
        [Required]
        public DateTime FechaEntrega { get; set; } = DateTime.Now;
        public int ProductoAlbaran { get; set; }
        public int CantidadProductoAlbaran { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PedidoAlbaran!=0 && (ProductoAlbaran != 0 || CantidadProductoAlbaran!=0) )
            {
                yield return new ValidationResult(
                    "No puedes tener un pedido y especificar un producto o una cantidad entrante.",
                    new[] { nameof(PedidoAlbaran), nameof(ProductoAlbaran), nameof(CantidadProductoAlbaran) });
            }
        }
    }
}
