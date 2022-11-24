using Microsoft.EntityFrameworkCore;
using TFG2022Server.Data;
using TFG2022Server.Entities;
using TFG2022Server.Extensions;
using TFG2022Server.Models;
using TFG2022Server.Services.Contracts;

namespace TFG2022Server.Services
{
    public class FamiliaProductoService : IFamiliaProductoService
    {
        private readonly TFG2022Context tfg2022Context;

        public FamiliaProductoService(TFG2022Context tfg2022Context)
        {
            this.tfg2022Context = tfg2022Context;
        }

        public async Task<List<FamiliaProductoModel>> GetFamiliaProductos()
        {
            try
            {
                return await this.tfg2022Context.FamiliaProductos.Convert();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<FamiliaProducto> GetFamiliaProducto(int fId)
        {
            try
            {
                var familia = await tfg2022Context.FamiliaProductos.FindAsync(fId);
                return familia;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<FamiliaProducto> AddFamilia(FamiliaProductoModel fproductoMod)
        {
            try
            {
                FamiliaProducto productoToAdd = fproductoMod.Convert();
                var result = await this.tfg2022Context.FamiliaProductos.AddAsync(productoToAdd);
                await this.tfg2022Context.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateFamilia(FamiliaProductoModel fproductoMod)
        {
            try
            {
                var fproductoToUpdate = await this.tfg2022Context.FamiliaProductos.FindAsync(fproductoMod.FamiliaID);

                if (fproductoToUpdate != null)
                {
                    fproductoToUpdate.Nombre = fproductoMod.Nombre;
                    fproductoToUpdate.Descripcion = fproductoMod.Descripcion;
                    await this.tfg2022Context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteFamilia(int id)
        {
            try
            {
                var fproducto = await this.tfg2022Context.FamiliaProductos.FindAsync(id);
                if (fproducto != null)
                {
                    this.tfg2022Context.FamiliaProductos.Remove(fproducto);
                    await this.tfg2022Context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
