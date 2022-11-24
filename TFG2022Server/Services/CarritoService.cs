using Microsoft.EntityFrameworkCore;
using TFG2022Server.Data;
using TFG2022Server.Entities;
using TFG2022Server.Extensions;
using TFG2022Server.Models;
using TFG2022Server.Services.Contracts;

namespace TFG2022Server.Services
{
    public class CarritoService : ICarritoService
    {
        private readonly TFG2022Context tfg2022Context;

        public CarritoService(TFG2022Context tfg2022Context)
        {
            this.tfg2022Context = tfg2022Context;
        }

        public async Task<List<CarritoModel>> GetCarritos()
        {
            try
            {
                return await this.tfg2022Context.Carritos.Convert();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Carrito> GetCarritoByUser(int user)
        {
            var carrito = new Carrito();
            try
            {
                // Obtener el usuario a travez del email (sólo habrá 1 usuario por email espero)
                carrito = (await this.tfg2022Context.Carritos.Where(u => u.UsuarioCarrito == user).ToListAsync()).ElementAt(0);

                return carrito;
            }
            catch (Exception)
            {
                // El usuario no tiene carrito, así que le creamos uno.
                // 
                try
                {
                    Carrito carritoToAdd = new Carrito();
                    carritoToAdd.UsuarioCarrito = user;

                    var result = await this.tfg2022Context.Carritos.AddAsync(carritoToAdd);
                    await this.tfg2022Context.SaveChangesAsync();
                    return result.Entity;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
