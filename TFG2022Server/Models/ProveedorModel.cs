namespace TFG2022Server.Models
{
    public class ProveedorModel
    {
        public int ProveedorId { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string Direccion { get; set; }
        public int CodigoPostal { get; set; }
        public int Telefono { get; set; }
        public string? Email { get; set; }
    }
}
