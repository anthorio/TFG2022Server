using System.ComponentModel.DataAnnotations;

namespace TFG2022Server.Entities
{
    public class Pago
    {
        [Key]
        public int PagoId { get; set; }
        public int FacturaPago { get; set; }
        public DateTime Fecha { get; set; }
        public double Cantidad { get; set; }
        public string? Observaciones { get; set; }
    }
}
