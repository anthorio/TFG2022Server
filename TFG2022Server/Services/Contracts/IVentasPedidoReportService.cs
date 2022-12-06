using TFG2022Server.Models.ReportModels;

namespace TFG2022Server.Services.Contracts
{
    public interface IVentasPedidoReportService
    {
        Task<List<GroupedFieldCantidadModel>> GetProductosVendidosPorMesData(DateTime mes);



        // Graficas que sólo puede ver x
        Task<List<GroupedFieldPrecioModel>> GetUsuarioPrecioPorMesData();
        Task<List<GroupedFieldCantidadModel>> GetCantidadPorFamiliaProducto();
        Task<List<GroupedFieldCantidadModel>> GetCantidadPorMesData();

        // Graficas que solo puede ver el encargado de almacen por ejemplo
        Task<List<GroupedFieldPrecioModel>> GetVentasTotalesPorCliente();
    }
}
