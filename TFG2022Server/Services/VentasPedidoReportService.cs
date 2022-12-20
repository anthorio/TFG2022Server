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

        public async Task<List<GroupedFieldDateModel>> GetPedidosPorTiempoData(DateTime startDate, DateTime endDate)
        {
            try
            {
                var reportData = await (from v in this.tfg2022Context.VentasPedidoReportes
                                        group v by new { v.FechaPedido, v.PedidoId } into g
                                        where g.Key.FechaPedido >= startDate && g.Key.FechaPedido <= endDate
                                        select new { g.Key.FechaPedido, g.Key.PedidoId })
                    .GroupBy(x => new { year = x.FechaPedido.Year, month = x.FechaPedido.Month, day = x.FechaPedido.Day })
                    .Select(x => new GroupedFieldDateModel { GroupedFieldDateKey = new DateTime(x.Key.year, x.Key.month, x.Key.day), Cantidad = x.Select(y => y.PedidoId).Distinct().Count() }).ToListAsync();


                return reportData;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<List<GroupedFieldCantidadModel>> GetMetodosPagoPorTiempoData(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }
    }
}
