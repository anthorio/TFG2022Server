using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Syncfusion.Drawing;
using TFG2022Server.Entities;
using TFG2022Server.Models;
using TFG2022Server.Services;
using TFG2022Server.Extensions;

namespace TFG2022Server.Data
{
    public class ExportService
    {
        //Export weather data to PDF document.
        public static MemoryStream CreateFactura(Pedido pedido, UsuarioModel user, List<ProductoModel> productos, List<LineaPedidoModel> lineas)
        {
            if (pedido == null)
            {
                throw new ArgumentNullException("Pedido cannot be null");
            }
            //Create a new PDF document
            using (PdfDocument pdfDocument = new PdfDocument())
            {
                int paragraphAfterSpacing = 8;
                int cellMargin = 8;

                //Add page to the PDF document
                PdfPage page = pdfDocument.Pages.Add();

                //Create a new font
                PdfStandardFont font = new PdfStandardFont(PdfFontFamily.TimesRoman, 16);

                //Create a text element to draw a text in PDF page
                PdfTextElement title = new PdfTextElement("Pedido", font, PdfBrushes.Black);
                PdfLayoutResult result = title.Draw(page, new PointF(0, 0));


                PdfStandardFont contentFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 12);
                PdfTextElement content = new PdfTextElement("Gracias por comprar en Suministros Nerja. A continuaci�n ofrecemos un resumen con los detalles de tu pedido.", contentFont, PdfBrushes.Black);
                PdfLayoutFormat format = new PdfLayoutFormat();
                format.Layout = PdfLayoutType.Paginate;

                //Draw a text to the PDF document
                result = content.Draw(page, new RectangleF(0, result.Bounds.Bottom + paragraphAfterSpacing, page.GetClientSize().Width, page.GetClientSize().Height), format);

                //1 Create a PdfGrid
                PdfGrid pdfGridPedido = new PdfGrid();
                pdfGridPedido.Style.CellPadding.Left = cellMargin;
                pdfGridPedido.Style.CellPadding.Right = cellMargin;

                //Applying built-in style to the PDF grid
                pdfGridPedido.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent1);

                pdfGridPedido = addPedidoToGrid(pdfGridPedido, pedido, Constants.costeEnvio);
                pdfGridPedido.Style.Font = contentFont;

                //Draw PDF grid into the PDF page
                PdfGridLayoutResult pdfGridLayoutResult0 = pdfGridPedido.Draw(page, new Syncfusion.Drawing.PointF(0, result.Bounds.Bottom + paragraphAfterSpacing));

                // 2 Create a PdfGrid
                PdfGrid pdfGridLineasProductos = new PdfGrid();
                pdfGridLineasProductos.Style.CellPadding.Left = cellMargin;
                pdfGridLineasProductos.Style.CellPadding.Right = cellMargin;
                pdfGridLineasProductos.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent1);
                pdfGridLineasProductos = addLineasToGrid(pdfGridLineasProductos, user, productos, lineas);
                pdfGridLineasProductos.Style.Font = contentFont;

                PdfGridLayoutResult pdfGridLayoutResult1 = pdfGridLineasProductos.Draw(page, new Syncfusion.Drawing.PointF(0, pdfGridLayoutResult0.Bounds.Bottom + 20));

                // 3 Create a PdfGrid
                PdfGrid pdfGridDireccion = new PdfGrid();
                pdfGridDireccion.Style.CellPadding.Left = cellMargin;
                pdfGridDireccion.Style.CellPadding.Right = cellMargin;
                pdfGridDireccion.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent1);
                pdfGridDireccion = addDireccionGrid(pdfGridDireccion, user);
                pdfGridDireccion.Style.Font = contentFont;

                pdfGridDireccion.Draw(page, new Syncfusion.Drawing.PointF(0, pdfGridLayoutResult1.Bounds.Bottom + 20));


                using (MemoryStream stream = new MemoryStream())
                {
                    //Saving the PDF document into the stream
                    pdfDocument.Save(stream);
                    //Closing the PDF document
                    pdfDocument.Close(true);
                    return stream;

                }
            }
        }

        private static PdfGrid addDireccionGrid(PdfGrid pdfGrid, UsuarioModel user)
        {
            //Add three columns.
            pdfGrid.Columns.Add(2);

            //Add rows.
            PdfGridRow pdfGridRow0 = pdfGrid.Rows.Add();
            PdfGridRow pdfGridRow1 = pdfGrid.Rows.Add();
            PdfGridRow pdfGridRow2 = pdfGrid.Rows.Add();
            PdfGridRow pdfGridRow3 = pdfGrid.Rows.Add();
            PdfGridRow pdfGridRow4 = pdfGrid.Rows.Add();
            PdfGridRow pdfGridRow5 = pdfGrid.Rows.Add();
            PdfGridRow pdfGridRow6 = pdfGrid.Rows.Add();
            PdfGridRow pdfGridRow7 = pdfGrid.Rows.Add();

            pdfGridRow0.Cells[0].Value = "Nombre";
            pdfGridRow0.Cells[1].Value = user.Nombre.ToString();

            pdfGridRow1.Cells[0].Value = "Apellidos";
            pdfGridRow1.Cells[1].Value = user.Apellidos.ToString();

            pdfGridRow2.Cells[0].Value = "Email";
            pdfGridRow2.Cells[1].Value = user.Email.ToString();

            pdfGridRow3.Cells[0].Value = "Tel�fono";
            pdfGridRow3.Cells[1].Value = user.Telefono.ToString();

            pdfGridRow4.Cells[0].Value = "DNI";
            pdfGridRow4.Cells[1].Value = user.Dni.ToString();

            pdfGridRow5.Cells[0].Value = "Direcci�n de entrega";
            pdfGridRow5.Cells[1].Value = user.Direccion.ToString();

            pdfGridRow6.Cells[0].Value = "C�digo Postal";
            pdfGridRow6.Cells[1].Value = user.CodigoPostal.ToString();

            pdfGridRow7.Cells[0].Value = "Poblaci�n";
            pdfGridRow7.Cells[1].Value = user.Poblacion.ToString();

            return pdfGrid;
        }

        private static PdfGrid addLineasToGrid(PdfGrid pdfGrid, UsuarioModel user, List<ProductoModel> productos, List<LineaPedidoModel> lineas)
        {
            //Add three columns.
            pdfGrid.Columns.Add(4);

            //Add header.
            pdfGrid.Headers.Add(1);
            PdfGridRow pdfGridHeader = pdfGrid.Headers[0];

            pdfGridHeader.Cells[0].Value = "Producto";
            pdfGridHeader.Cells[1].Value = "Precio /ud.";
            pdfGridHeader.Cells[2].Value = "Cantidad";
            pdfGridHeader.Cells[3].Value = "Importe total (IVA incluido)";

            //Add rows.
            foreach (var producto in productos)
            {
                foreach (var linea in lineas)
                {
                    if (linea.ProductoLineaPedido == producto.ProductoId)
                    {
                        PdfGridRow pdfGridRow = pdfGrid.Rows.Add();
                        pdfGridRow.Cells[0].Value = producto.Nombre.ToString();
                        pdfGridRow.Cells[1].Value = producto.Precio.ToString() + " �";
                        pdfGridRow.Cells[2].Value = linea.Cantidad.ToString();
                        pdfGridRow.Cells[3].Value = linea.PrecioFinal.ToString() + " �";
                    }
                }

            }
            return pdfGrid;
        }

        private static PdfGrid addPedidoToGrid(PdfGrid pdfGrid, Pedido pedido, double costePedido)
        {
            if (pedido.Envio)
            {
                //Add three columns.
                pdfGrid.Columns.Add(2);

                //Add rows.
                PdfGridRow pdfGridRow0 = pdfGrid.Rows.Add();
                PdfGridRow pdfGridRow1 = pdfGrid.Rows.Add();
                PdfGridRow pdfGridRow2 = pdfGrid.Rows.Add();
                PdfGridRow pdfGridRow3 = pdfGrid.Rows.Add();
                PdfGridRow pdfGridRow4 = pdfGrid.Rows.Add();

                pdfGridRow0.Cells[0].Value = "ID del pedido";
                pdfGridRow0.Cells[1].Value = pedido.PedidoId.ToString();

                pdfGridRow1.Cells[0].Value = "Fecha de compra";
                pdfGridRow1.Cells[1].Value = pedido.FechaPedido.ToString();

                pdfGridRow2.Cells[0].Value = "Importe";
                pdfGridRow2.Cells[1].Value = Math.Round(pedido.PrecioTotal - costePedido, 2).ToString() + " �";

                pdfGridRow3.Cells[0].Value = "Gastos de env�o";
                pdfGridRow3.Cells[1].Value = costePedido.ToString();

                pdfGridRow4.Cells[0].Value = "Total";
                pdfGridRow4.Cells[1].Value = Math.Round(pedido.PrecioTotal, 2).ToString() + " �";

                return pdfGrid;
            }
            else
            {
                //Add three columns.
                pdfGrid.Columns.Add(2);

                //Add rows.
                PdfGridRow pdfGridRow0 = pdfGrid.Rows.Add();
                PdfGridRow pdfGridRow1 = pdfGrid.Rows.Add();
                PdfGridRow pdfGridRow2 = pdfGrid.Rows.Add();
                PdfGridRow pdfGridRow4 = pdfGrid.Rows.Add();

                pdfGridRow0.Cells[0].Value = "ID del pedido";
                pdfGridRow0.Cells[1].Value = pedido.PedidoId.ToString();

                pdfGridRow1.Cells[0].Value = "Fecha de compra";
                pdfGridRow1.Cells[1].Value = pedido.FechaPedido.ToString();

                pdfGridRow2.Cells[0].Value = "Importe";
                pdfGridRow2.Cells[1].Value = Math.Round(pedido.PrecioTotal, 2).ToString() + " �";

                pdfGridRow4.Cells[0].Value = "Total";
                pdfGridRow4.Cells[1].Value = Math.Round(pedido.PrecioTotal, 2).ToString() + " �";

                return pdfGrid;
            }

        }
    }
}