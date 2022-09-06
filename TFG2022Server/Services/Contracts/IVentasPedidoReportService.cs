using TFG2022Server.Models.ReportModels;

namespace TFG2022Server.Services.Contracts
{
    public interface IVentasPedidoReportService
    {
        Task<List<GroupedFieldPrecioModel>> GetUsuarioPrecioPorMesData();
        Task<List<GroupedFieldCantidadModel>> GetCantidadPorFamiliaProducto();
        Task<List<GroupedFieldCantidadModel>> GetCantidadPorMesData();
    }
}
