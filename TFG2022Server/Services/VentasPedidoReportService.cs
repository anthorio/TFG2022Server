using Microsoft.EntityFrameworkCore;
using TFG2022Server.Data;
using TFG2022Server.Models.ReportModels;
using TFG2022Server.Services.Contracts;

namespace TFG2022Server.Services
{
    public class VentasPedidoReportService : IVentasPedidoReportService
    {
        private readonly TFG2022Context tfg2022Context;
        public VentasPedidoReportService(TFG2022Context tfg2022Context)
        {
            this.tfg2022Context = tfg2022Context;
        }

        public async Task<List<GroupedFieldPrecioModel>> GetUsuarioPrecioPorMesData()
        {
            try
            {
                var reportData = await (from v in this.tfg2022Context.VentasPedidoReportes
                                        where v.UsuarioId == 1
                                        group v by v.FechaPedido.Month into GroupedData
                                        orderby GroupedData.Key
                                        select new GroupedFieldPrecioModel
                                        {
                                            GroupedFieldKey = (
                                              GroupedData.Key == 1 ? "Ene" :
                                              GroupedData.Key == 2 ? "Feb" :
                                              GroupedData.Key == 3 ? "Mar" :
                                              GroupedData.Key == 4 ? "Abr" :
                                              GroupedData.Key == 5 ? "May" :
                                              GroupedData.Key == 6 ? "Jun" :
                                              GroupedData.Key == 7 ? "Jul" :
                                              GroupedData.Key == 8 ? "Ago" :
                                              GroupedData.Key == 9 ? "Sep" :
                                              GroupedData.Key == 1 ? "Oct" :
                                              GroupedData.Key == 11 ? "Nov" :
                                               GroupedData.Key == 12 ? "Dic" :
                                              ""
                                            ),
                                            Precio = Math.Round(GroupedData.Sum(lp => lp.LineaPedidoPrecio), 2)
                                        }).ToListAsync();
                return reportData;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GroupedFieldCantidadModel>> GetCantidadPorFamiliaProducto()
        {
            try
            {
                var reportData = await (from v in this.tfg2022Context.VentasPedidoReportes
                                        group v by v.FamiliaProductoNombre into GroupedData
                                        orderby GroupedData.Key
                                        select new GroupedFieldCantidadModel
                                        {
                                            GroupedFieldCantidadKey = GroupedData.Key,
                                            Cantidad = GroupedData.Sum(lp => lp.LineaPedidoCantidad)
                                        }).ToListAsync();
                return reportData;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GroupedFieldCantidadModel>> GetCantidadPorMesData()
        {
            try
            {
                var reportData = await (from v in this.tfg2022Context.VentasPedidoReportes
                                        where v.UsuarioId == 1
                                        group v by v.FechaPedido.Month into GroupedData
                                        orderby GroupedData.Key
                                        select new GroupedFieldCantidadModel
                                        {
                                            GroupedFieldCantidadKey = (
                                              GroupedData.Key == 1 ? "Ene" :
                                              GroupedData.Key == 2 ? "Feb" :
                                              GroupedData.Key == 3 ? "Mar" :
                                              GroupedData.Key == 4 ? "Abr" :
                                              GroupedData.Key == 5 ? "May" :
                                              GroupedData.Key == 6 ? "Jun" :
                                              GroupedData.Key == 7 ? "Jul" :
                                              GroupedData.Key == 8 ? "Ago" :
                                              GroupedData.Key == 9 ? "Sep" :
                                              GroupedData.Key == 1 ? "Oct" :
                                              GroupedData.Key == 11 ? "Nov" :
                                               GroupedData.Key == 12 ? "Dic" :
                                              ""
                                            ),
                                            Cantidad = GroupedData.Sum(lp => lp.LineaPedidoCantidad)
                                        }).ToListAsync();
                return reportData;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GroupedFieldPrecioModel>> GetVentasTotalesPorCliente()
        {
            try
            {
                List<int> trabajadoresIds = await GetTrabajadoresIds();
                var reportData = await (from s in this.tfg2022Context.VentasPedidoReportes
                                        where trabajadoresIds.Contains(s.UsuarioId)
                                        group s by s.UsuarioNombre into GroupedData
                                        orderby GroupedData.Key
                                        select new GroupedFieldPrecioModel
                                        {
                                            GroupedFieldKey = GroupedData.Key,
                                            Precio = Math.Round(GroupedData.Sum(oi => oi.LineaPedidoPrecio), 2)
                                        }).ToListAsync();
                return reportData;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private async Task<List<int>> GetTrabajadoresIds()
        {
            List<int> trabajadoresIds = await this.tfg2022Context.Usuarios
                                        .Where(e => e.Rol == "Empleado")
                                        .Select(e => e.UsuarioId).ToListAsync();
            return trabajadoresIds;
        }

        public async Task<List<GroupedFieldCantidadModel>> GetProductosVendidosPorTiempoData(DateTime startDate, DateTime endDate)
        {
            try
            {
                var reportData = await (from v in this.tfg2022Context.VentasPedidoReportes
                                        where v.FechaPedido >= startDate && v.FechaPedido <= endDate
                                        group v by v.NombreProducto into GroupedData
                                        orderby GroupedData.Key
                                        select new GroupedFieldCantidadModel
                                        {
                                            GroupedFieldCantidadKey = GroupedData.Key.Substring(0, 20),
                                            Cantidad = GroupedData.Sum(lp => lp.LineaPedidoCantidad)
                                        }).ToListAsync();
                return reportData;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<GroupedFieldCantidadModel>> GetFamiliaProductosVendidosPorTiempoData(DateTime startDate, DateTime endDate)
        {
            try
            {
                var reportData = await (from v in this.tfg2022Context.VentasPedidoReportes
                                        where v.FechaPedido >= startDate && v.FechaPedido <= endDate
                                        group v by v.FamiliaProductoNombre into GroupedData
                                        orderby GroupedData.Key
                                        select new GroupedFieldCantidadModel
                                        {
                                            GroupedFieldCantidadKey = GroupedData.Key.Substring(0, 20),
                                            Cantidad = GroupedData.Sum(lp => lp.LineaPedidoCantidad)
                                        }).ToListAsync();
                return reportData;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<GroupedFieldTogetherModel>> GetProductosVendidosJuntosPorTiempoData(DateTime startDate, DateTime endDate)
        {
            try
            {
                List<GroupedFieldTogetherModel> result = new List<GroupedFieldTogetherModel>();

                var reportData = await (from t1 in this.tfg2022Context.VentasPedidoReportes
                                        join t2 in this.tfg2022Context.VentasPedidoReportes on t1.PedidoId equals t2.PedidoId
                                        select new { t1 = t1, t2 = t2 }
                 ).Where(x => x.t1.ProductoId < x.t2.ProductoId)
                  .GroupBy(x => new { t1Id = x.t1.ProductoId, t2Id = x.t2.ProductoId })
                  .Select(x => new { GroupedFieldProduct1Key = x.First().t1.NombreProducto, GroupedFieldProduct2Key = x.First().t2.NombreProducto, Cantidad = x.Select(y => y.t1.PedidoId).Distinct().Count(), Fecha = x.First().t1.FechaPedido })
                  .OrderByDescending(x => x.Cantidad).ToListAsync();

                foreach (var item in reportData)
                {
                    if (item.Fecha >= startDate && item.Fecha <= endDate)
                        result.Add(new GroupedFieldTogetherModel(item.GroupedFieldProduct1Key, item.GroupedFieldProduct2Key, item.Cantidad));
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
