using Microsoft.EntityFrameworkCore;
using TFG2022Server.Data;
using TFG2022Server.Entities;
using TFG2022Server.Extensions;

using TFG2022Server.Models;
using TFG2022Server.Services.Contracts;

namespace TFG2022Server.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly TFG2022Context tfg2022Context;

        public UsuarioService(TFG2022Context tfg2022Context)
        {
            this.tfg2022Context = tfg2022Context;
        }

        public string[] GetRoles()
        {
            return Constants.UsuarioRoles;
        }

        public async Task<List<UsuarioModel>> GetUsuarios()
        {
            try
            {
                return await this.tfg2022Context.Usuarios.Convert(tfg2022Context);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<UsuarioModel>> GetAllUsuarios()
        {
            try
            {
                return await this.tfg2022Context.Usuarios.Convert();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Usuario> AddUsuario(UsuarioModel usuarioMod)
        {
            try
            {
                Usuario usuarioToAdd = usuarioMod.Convert();

                var result = await this.tfg2022Context.Usuarios.AddAsync(usuarioToAdd);
                await this.tfg2022Context.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateUsuario(UsuarioModel usuarioMod)
        {
            try
            {
                var usuarioToUpdate = await this.tfg2022Context.Usuarios.FindAsync(usuarioMod.UsuarioId);

                if (usuarioToUpdate != null)
                {
                    // usuarioToUpdate.UsuarioId = usuarioMod.UsuarioId;
                    usuarioToUpdate.Rol = usuarioMod.Rol;
                    usuarioToUpdate.Dni = usuarioMod.Dni;
                    usuarioToUpdate.Apellidos = usuarioMod.Dni;
                    usuarioToUpdate.Nombre = usuarioMod.Nombre;
                    usuarioToUpdate.Email = usuarioMod.Email;
                    usuarioToUpdate.Contraseña = usuarioMod.Contraseña;
                    usuarioToUpdate.FechaNacimiento = usuarioMod.FechaNacimiento;
                    usuarioToUpdate.Telefono = usuarioMod.Telefono;
                    await this.tfg2022Context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteUsuario(int id)
        {
            try
            {
                var usuario = await this.tfg2022Context.Usuarios.FindAsync(id);
                if (usuario != null)
                {
                    this.tfg2022Context.Usuarios.Remove(usuario);
                    await this.tfg2022Context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Usuario> CheckLogin(string usermail, string password)
        {
            var usuario = new Usuario();
            try
            {
                // Obtener el usuario a travez del email (sólo habrá 1 usuario por email espero)
                usuario = (await this.tfg2022Context.Usuarios.Where(u => u.Email == usermail).ToListAsync()).ElementAt(0);
                if (usuario != null)
                {
                    if (password == usuario.Contraseña)
                    {
                        return usuario;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Usuario> ReadUserbyEmail(string usermail)
        {
            var usuario = new Usuario();
            try
            {
                // Obtener el usuario a travez del email (sólo habrá 1 usuario por email espero)
                usuario = (await this.tfg2022Context.Usuarios.Where(u => u.Email == usermail).ToListAsync()).ElementAt(0);
                if (usuario != null)
                {
                    return usuario;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
