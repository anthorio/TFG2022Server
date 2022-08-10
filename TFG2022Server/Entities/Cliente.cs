using System.ComponentModel.DataAnnotations;

namespace TFG2022Server.Entities
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }
        public string Direccion { get; set; } = null!;
        public string Poblacion { get; set; } = null!;
        public int CodigoPostal { get; set; }
        public int Descuento { get; set; }
        public int UsuarioIdCliente { get; set; }
    }
}
