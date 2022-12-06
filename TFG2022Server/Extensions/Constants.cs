namespace TFG2022Server.Extensions
{
    public static class Constants
    {
        public static string[] UsuarioRoles = new string[] { "Cliente", "Empleado", "Administrador" }; // El primer rol tiene que ser el cliente
        public static string[] EstadosFactura = new string[] { "estado1", "estado2", "estado3" }; // Una FACTURA siempre va a estar pagada... No es un "pagaré"...
        public static string[] EstadosPedido = new string[] { "Recién creado", "Venta física", "estado3" }; // El primer rol tiene que ser recien creado, el segundo es venta fisica
        public static string[] TiposPedido = new string[] { "pedido1", "pedido2", "pedido3" };
        public static double costeEnvio = 4.5;
    }

}
