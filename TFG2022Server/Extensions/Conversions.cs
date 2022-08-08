using Microsoft.EntityFrameworkCore;
using TFG2022Server.Entities;
using TFG2022Server.Models;

namespace TFG2022Server.Extensions
{
    public static class Conversions
    {
        public static async Task<List<UsuarioModel>>Convert(this IQueryable<Usuario> usuarios)
        {
            return await (from w in usuarios
                          select new UsuarioModel
                          {
                              UsuarioId = w.UsuarioId,
                              Rol=w.Rol,
                              Email=w.Email,
                              Contraseña=w.Contraseña,
                              Nombre=w.Nombre,
                              Apellidos=w.Apellidos,
                              Telefono=w.Telefono,
                              Dni=w.Dni,
                              FechaNacimiento=w.FechaNacimiento
                          }).ToListAsync();
        }
    }
}
