﻿using Microsoft.EntityFrameworkCore;
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
    }
}