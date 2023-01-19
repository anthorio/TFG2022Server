using System.ComponentModel.DataAnnotations;

namespace TFG2022Server.Entities
{
    public class Producto
    {
        [Key]
        public int ProductoId { get; set; }
        public int FamiliaProductoProducto { get; set; }
        public int ProveedorProducto { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public int Cantidad { get; set; }
        public int StockMinimo { get; set; }
        public double Precio { get; set; }
        public int Iva { get; set; }
        public string? UrlImagen { get; set; }
    }
}
