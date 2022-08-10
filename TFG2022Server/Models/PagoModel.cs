namespace TFG2022Server.Models
{
    public class PagoModel
    {
        public int PagoId { get; set; }
        public int FacturaPago { get; set; }
        public DateTime Fecha { get; set; }
        public double Cantidad { get; set; }
        public string? Observaciones { get; set; }
    }
}
