using Microsoft.EntityFrameworkCore;
using TFG2022Server.Data;
using TFG2022Server.Entities;
using TFG2022Server.Extensions;
using TFG2022Server.Models;
using TFG2022Server.Services.Contracts;

namespace TFG2022Server.Services
{
    public class ProductoService : IProductoService
    {
        private readonly TFG2022Context tfg2022Context;

        public ProductoService(TFG2022Context tfg2022Context)
        {
            this.tfg2022Context = tfg2022Context;
        }

        public async Task<List<ProductoModel>> GetProductos()
        {
            try
            {
                //return await this.tfg2022Context.Productos.Convert();
                return await this.tfg2022Context.Productos.Convert(tfg2022Context);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Producto> GetProducto(int productId)
        {
            try
            {
                return await tfg2022Context.Productos.FindAsync(productId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Producto> AddProducto(ProductoModel productoMod)
        {
            try
            {
                Producto productoToAdd = productoMod.Convert();
                var result = await this.tfg2022Context.Productos.AddAsync(productoToAdd);
                await this.tfg2022Context.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateProducto(ProductoModel productoMod)
        {
            try
            {
                var productoToUpdate = await this.tfg2022Context.Productos.FindAsync(productoMod.ProductoId);

                if (productoToUpdate != null)
                {
                    productoToUpdate.FamiliaProductoProducto = productoMod.FamiliaProductoProducto;
                    productoToUpdate.ProveedorProducto = productoMod.ProveedorProducto;
                    productoToUpdate.Nombre = productoMod.Nombre;
                    productoToUpdate.Descripcion = productoMod.Descripcion;
                    productoToUpdate.Cantidad = productoMod.Cantidad;
                    productoToUpdate.Precio = productoMod.Precio;
                    productoToUpdate.UrlImagen = productoMod.UrlImagen;
                    productoToUpdate.StockMinimo= productoMod.StockMinimo;
                    await this.tfg2022Context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteProducto(int id)
        {
            try
            {
                var producto = await this.tfg2022Context.Productos.FindAsync(id);
                if (producto != null)
                {
                    this.tfg2022Context.Productos.Remove(producto);
                    await this.tfg2022Context.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateCantidadProducto(int productoid, int cantidad)
        {
            try
            {
                var productoToUpdate = await this.tfg2022Context.Productos.FindAsync(productoid);

                if (productoToUpdate != null)
                {
                    productoToUpdate.Cantidad = productoToUpdate.Cantidad-cantidad;
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
