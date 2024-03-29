﻿namespace TFG2022Server.Extensions
{
    public static class Constants
    {
        public static string[] UsuarioRoles = new string[] { "Cliente", "Empleado", "Administrador" }; // El primer rol tiene que ser el cliente
        public static string[] EstadosFactura = new string[] { "Pendiente", "Completada" }; // Una FACTURA siempre va a estar pagada... No es un "pagaré"... El primero es recien creado y el de enmedio es completada
        public static string[] EstadosPedido = new string[] { "Recién creado", "Venta física", "Rechazado", "Aceptado", "Completado" }; // El primer rol tiene que ser recien creado, el segundo es venta fisica, el 3º es Rechazado
        public static double costeEnvio = 4.5;
    }
}
