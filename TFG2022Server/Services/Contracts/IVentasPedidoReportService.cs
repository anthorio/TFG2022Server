using TFG2022Server.Models.ReportModels;

namespace TFG2022Server.Services.Contracts
{
    public interface IVentasPedidoReportService
    {
        Task<List<GroupedFieldCantidadModel>> GetProductosVendidosPorTiempoData(DateTime startDate, DateTime endDate);
        Task<List<GroupedFieldCantidadModel>> GetFamiliaProductosVendidosPorTiempoData(DateTime startDate, DateTime endDate);
        Task<List<GroupedFieldTogetherModel>> GetProductosVendidosJuntosPorTiempoData(DateTime startDate, DateTime endDate);
        Task<List<GroupedFieldDateModel>> GetPedidosPorTiempoData(DateTime startDate, DateTime endDate);

        // Graficas generales
        Task<List<GroupedFieldCantidadModel>> GetCantidadPorFamiliaProducto();
        Task<List<GroupedFieldCantidadModel>> GetProductosEnCarritos();

    }
}
