namespace TFG2022Server.Models
{
    public class UsuarioModel
    {
        public int UsuarioId { get; set; }
        public string Rol { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Contraseña { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public int Telefono { get; set; }
        public string Dni { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
    }
}
