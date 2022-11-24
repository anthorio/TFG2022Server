using Microsoft.EntityFrameworkCore;
using TFG2022Server.Data;
using TFG2022Server.Entities;
using TFG2022Server.Extensions;
using TFG2022Server.Models;
using TFG2022Server.Services.Contracts;

namespace TFG2022Server.Services
{
    public class ProveedorService : IProveedorService
    {
        private readonly TFG2022Context tfg2022Context;

        public ProveedorService(TFG2022Context tfg2022Context)
        {
            this.tfg2022Context = tfg2022Context;
        }
        public async Task<List<ProveedorModel>> GetProveedores()
        {
            try
            {
                return await this.tfg2022Context.Proveedores.Convert();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Proveedor> AddProveedor(ProveedorModel proveedorMod)
        {
            try
            {
                Proveedor proveedorToAdd = proveedorMod.Convert();
                var result = await this.tfg2022Context.Proveedores.AddAsync(proveedorToAdd);
                await this.tfg2022Context.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteProveedor(int id)
        {
            try
            {
                var prov = await this.tfg2022Context.Proveedores.FindAsync(id);
                if (prov != null)
                {
                    this.tfg2022Context.Proveedores.Remove(prov);
                    await this.tfg2022Context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task UpdateProveedor(ProveedorModel proveedorMod)
        {
            try
            {
                var provToUpdate = await this.tfg2022Context.Proveedores.FindAsync(proveedorMod.ProveedorId);

                if (provToUpdate != null)
                {
                    provToUpdate.ProveedorId = proveedorMod.ProveedorId;
                    provToUpdate.Direccion = proveedorMod.Direccion;
                    provToUpdate.Nombre = proveedorMod.Nombre;
                    provToUpdate.Descripcion = proveedorMod.Descripcion;
                    provToUpdate.CodigoPostal = proveedorMod.CodigoPostal;
                    provToUpdate.Telefono = proveedorMod.Telefono;
                    provToUpdate.Email = proveedorMod.Email;

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
