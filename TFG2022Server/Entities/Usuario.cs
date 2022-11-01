using System.ComponentModel.DataAnnotations;

namespace TFG2022Server.Entities
{
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }
        public string Rol { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Contraseña { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Dni { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public string Direccion { get; set; } = null!;
        public string Poblacion { get; set; } = null!;
        public string CodigoPostal { get; set; } = null!;
        public int Descuento { get; set; }
    }
}
