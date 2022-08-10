using System.ComponentModel.DataAnnotations;

namespace TFG2022Server.Entities
{
    public class FamiliaProducto
    {
        [Key]
        public int FamiliaID { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
    }
}
