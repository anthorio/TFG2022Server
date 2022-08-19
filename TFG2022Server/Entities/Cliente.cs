using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TFG2022Server.Entities
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }
        public string Direccion { get; set; } = null!;
        public string Poblacion { get; set; } = null!;
        public string CodigoPostal { get; set; } = null!;
        public int Descuento { get; set; }
        public int UsuarioIdCliente { get; set; }
    }
}
