using System.ComponentModel.DataAnnotations;

namespace TFG2022Server.Models
{
    public class FamiliaProductoModel
    {
        [Required]
        public int FamiliaID { get; set; }
        [Required]
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
    }
}
