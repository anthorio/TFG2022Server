﻿using TFG2022Server.Models.ReportModels;

namespace TFG2022Server.Services.Contracts
{
    public interface IVentasPedidoReportService
    {
        Task<List<GroupedFieldCantidadModel>> GetProductosVendidosPorTiempoData(DateTime startDate, DateTime endDate);
        Task<List<GroupedFieldCantidadModel>> GetFamiliaProductosVendidosPorTiempoData(DateTime startDate, DateTime endDate);
        Task<List<GroupedFieldTogetherModel>> GetProductosVendidosJuntosPorTiempoData(DateTime startDate, DateTime endDate);
        Task<List<GroupedFieldDateModel>> GetPedidosPorTiempoData(DateTime startDate, DateTime endDate);
        Task<List<GroupedFieldCantidadModel>> GetPedidosDeUsuarioPorTiempoData(DateTime startDate, DateTime endDate);

        // Graficas generales
        Task<List<GroupedFieldCantidadModel>> GetCantidadPorFamiliaProducto();
        Task<List<GroupedFieldCantidadModel>> GetProductosEnCarritos();
        Task<List<MunicipioDetailsModel>> GetUsuariosPuntosMapa();
        Task<List<MunicipioDetailsModel>> GetProductosPuntosMapa();
        Task<List<GroupedFieldCantidadModel>> GetEstadoPedidos();
        Task<List<GroupedFieldCantidadModel>> GetProductosProveedores();


    }
}
