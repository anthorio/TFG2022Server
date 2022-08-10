namespace TFG2022Server.Models
{
    public class LineaFacturaModel
    {
        public int LineaFacturaId { get; set; }
        public int ProductoLineaFactura { get; set; }
        public int FacturaLineaFactura { get; set; }
        public int Cantidad { get; set; }
        public double Importe { get; set; }
    }
}
