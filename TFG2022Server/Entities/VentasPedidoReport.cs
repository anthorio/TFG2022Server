using System.ComponentModel.DataAnnotations;

namespace TFG2022Server.Entities
{
    public class VentasPedidoReport
    {
        [Key]
        public int IdVentasPedido { get; set; }
        public int PedidoId { get; set; }
        public DateTime FechaPedido { get; set; }
        public double PedidoTotal { get; set; }
        public int CantidadTotal { get; set; }
        public int LineaPedidoId { get; set; }
        public int LineaPedidoCantidad { get; set; }
        public double LineaPedidoPrecio { get; set; }
        public int ProductoId { get; set; }
        public string NombreProducto { get; set; }
        public int FamiliaProductoId { get; set; }
        public string FamiliaProductoNombre { get; set; }
        public int UsuarioId { get; set; }
        public string UsuarioNombre { get; set; }
    }
}
