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
                PdfTextElement title = new PdfTextElement("Factura de Pedido", font, PdfBrushes.Black);
                PdfLayoutResult result = title.Draw(page, new PointF(0, 0));


                PdfStandardFont contentFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 12);
                PdfTextElement content = new PdfTextElement("Suministros Nerja SL.\nCalle Chaparil, 3, 29780 Nerja, Málaga\nTeléfono: 906171870\n", contentFont, PdfBrushes.Black);
                PdfLayoutFormat format = new PdfLayoutFormat();
                format.Layout = PdfLayoutType.Paginate;

                //Draw a text to the PDF document
                result = content.Draw(page, new RectangleF(0, result.Bounds.Bottom + paragraphAfterSpacing, page.GetClientSize().Width, page.GetClientSize().Height), format);

                //0 Create las dos primeras lineas con el ID y la Fecha encima de los productos
                PdfGrid pdfGridPedido1 = new PdfGrid();
                pdfGridPedido1.Style.CellPadding.Left = cellMargin;
                pdfGridPedido1.Style.CellPadding.Right = cellMargin;

                pdfGridPedido1.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent1);

                pdfGridPedido1 = addIdPedidoYFechaToGrid(pdfGridPedido1, pedido, Constants.costeEnvio);
                pdfGridPedido1.Style.Font = contentFont;

                PdfGridLayoutResult pdfGridLayoutResult00 = pdfGridPedido1.Draw(page, new Syncfusion.Drawing.PointF(0, result.Bounds.Bottom + paragraphAfterSpacing));

                // 1 Añadir los productos del pedido
                PdfGrid pdfGridLineasProductos = new PdfGrid();
                pdfGridLineasProductos.Style.CellPadding.Left = cellMargin;
                pdfGridLineasProductos.Style.CellPadding.Right = cellMargin;
                pdfGridLineasProductos.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent1);
                pdfGridLineasProductos = addLineasToGrid(pdfGridLineasProductos, user, productos, lineas);
                pdfGridLineasProductos.Style.Font = contentFont;

                PdfGridLayoutResult pdfGridLayoutResult1 = pdfGridLineasProductos.Draw(page, new Syncfusion.Drawing.PointF(0, pdfGridLayoutResult00.Bounds.Bottom + 20));

                // 2 Create la tabla con la informacion del pedido en general
                PdfGrid pdfGridPedido = new PdfGrid();
                pdfGridPedido.Style.CellPadding.Left = cellMargin;
                pdfGridPedido.Style.CellPadding.Right = cellMargin;

                //Applying built-in style to the PDF grid
                pdfGridPedido.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent1);

                pdfGridPedido = addPedidoToGrid(pdfGridPedido, pedido, Constants.costeEnvio);
                pdfGridPedido.Style.Font = contentFont;

                //Draw PDF grid into the PDF page
                PdfGridLayoutResult pdfGridLayoutResult0 = pdfGridPedido.Draw(page, new Syncfusion.Drawing.PointF(0, pdfGridLayoutResult1.Bounds.Bottom + paragraphAfterSpacing));

                // 3 Dirección del cliente

                PdfTextElement contentDatos = new PdfTextElement("Datos del cliente\n", contentFont, PdfBrushes.Black);
                format.Layout = PdfLayoutType.Paginate;
                result = contentDatos.Draw(page, new RectangleF(0, pdfGridLayoutResult0.Bounds.Bottom + paragraphAfterSpacing, page.GetClientSize().Width, page.GetClientSize().Height), format);

                PdfGrid pdfGridDireccion = new PdfGrid();
                pdfGridDireccion.Style.CellPadding.Left = cellMargin;
                pdfGridDireccion.Style.CellPadding.Right = cellMargin;
                pdfGridDireccion.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent1);
                pdfGridDireccion = addDireccionGrid(pdfGridDireccion, user);
                pdfGridDireccion.Style.Font = contentFont;

                pdfGridDireccion.Draw(page, new Syncfusion.Drawing.PointF(0, pdfGridLayoutResult0.Bounds.Bottom + 30));


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
        public static MemoryStream CreateAlbaran(AlbaranModel albaran, UsuarioModel user, List<ProductoModel> productos, List<LineaPedidoModel> lineas, ProveedorModel proveedor)
        {
            if (albaran == null)
            {
                throw new ArgumentNullException("Albaran cannot be null");
            }
            //Create a new PDF document
            using (PdfDocument pdfDocument = new PdfDocument())
            {
                int paragraphAfterSpacing = 8;
                int cellMargin = 8;
                bool esAlbaranDePedido = albaran.PedidoAlbaran != 0;

                if (esAlbaranDePedido)
                {
                    //Add page to the PDF document
                    PdfPage page = pdfDocument.Pages.Add();

                    //Create a new font
                    PdfStandardFont font = new PdfStandardFont(PdfFontFamily.TimesRoman, 16);

                    //Create a text element to draw a text in PDF page
                    PdfTextElement title = title = new PdfTextElement("Albaran de entrega", font, PdfBrushes.Black); // Si es un albarán de un pedido // Si es un albarán de recibir un producto

                    PdfLayoutResult result = title.Draw(page, new PointF(0, 0));

                    PdfStandardFont contentFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 12);
                    PdfTextElement content = new PdfTextElement("Resumen con la información de la entrega.\nSuministros Nerja SL.\nCalle Chaparil, 3, 29780 Nerja, Málaga\nTeléfono: 906171870\n", contentFont, PdfBrushes.Black);
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

                    pdfGridPedido = addAlbaranToGrid(pdfGridPedido, albaran);
                    pdfGridPedido.Style.Font = contentFont;

                    //Draw PDF grid into the PDF page
                    PdfGridLayoutResult pdfGridLayoutResult0 = pdfGridPedido.Draw(page, new Syncfusion.Drawing.PointF(0, result.Bounds.Bottom + paragraphAfterSpacing));

                    // 2 Create a PdfGrid
                    PdfGrid pdfGridDetallesAlbaran = new PdfGrid();
                    pdfGridDetallesAlbaran.Style.CellPadding.Left = cellMargin;
                    pdfGridDetallesAlbaran.Style.CellPadding.Right = cellMargin;
                    pdfGridDetallesAlbaran.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent1);
                    pdfGridDetallesAlbaran = addLineasAlbaranEntregaToGrid(pdfGridDetallesAlbaran, productos, lineas);
                    pdfGridDetallesAlbaran.Style.Font = contentFont;

                    PdfGridLayoutResult pdfGridLayoutResult1 = pdfGridDetallesAlbaran.Draw(page, new Syncfusion.Drawing.PointF(0, pdfGridLayoutResult0.Bounds.Bottom + 20));

                    // 3 Create a PdfGrid con los datos del usuario

                    PdfTextElement contentDatos = new PdfTextElement("Dirección de entrega\n", contentFont, PdfBrushes.Black);
                    format.Layout = PdfLayoutType.Paginate;
                    PdfLayoutResult resultede = contentDatos.Draw(page, new RectangleF(0, pdfGridLayoutResult1.Bounds.Bottom + paragraphAfterSpacing, page.GetClientSize().Width, page.GetClientSize().Height), format);

                    PdfGrid pdfGridDireccion = new PdfGrid();
                    pdfGridDireccion.Style.CellPadding.Left = cellMargin;
                    pdfGridDireccion.Style.CellPadding.Right = cellMargin;
                    pdfGridDireccion.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent1);
                    pdfGridDireccion = addDireccionGrid(pdfGridDireccion, user);
                    pdfGridDireccion.Style.Font = contentFont;

                    pdfGridDireccion.Draw(page, new Syncfusion.Drawing.PointF(0, resultede.Bounds.Bottom + 20));


                    using (MemoryStream stream = new MemoryStream())
                    {
                        //Saving the PDF document into the stream
                        pdfDocument.Save(stream);
                        //Closing the PDF document
                        pdfDocument.Close(true);
                        return stream;

                    }
                }
                else // Albarán de recibir un producto en el almacén
                {
                    //Add page to the PDF document
                    PdfPage page = pdfDocument.Pages.Add();

                    //Create a new font
                    PdfStandardFont font = new PdfStandardFont(PdfFontFamily.TimesRoman, 16);

                    //Create a text element to draw a text in PDF page
                    PdfTextElement title = title = new PdfTextElement("Albaran de recepción de producto", font, PdfBrushes.Black); // Si es un albarán de un pedido // Si es un albarán de recibir un producto

                    PdfLayoutResult result = title.Draw(page, new PointF(0, 0));

                    PdfStandardFont contentFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 12);
                    PdfTextElement content = new PdfTextElement("Información del producto recibido en el almacén de:\nSuministros Nerja SL.\nCalle Chaparil, 3, 29780 Nerja, Málaga\nTeléfono: 906171870\n", contentFont, PdfBrushes.Black);
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

                    pdfGridPedido = addAlbaranToGrid(pdfGridPedido, albaran);
                    pdfGridPedido.Style.Font = contentFont;

                    //Draw PDF grid into the PDF page
                    PdfGridLayoutResult pdfGridLayoutResult0 = pdfGridPedido.Draw(page, new Syncfusion.Drawing.PointF(0, result.Bounds.Bottom + paragraphAfterSpacing));

                    // 2 Create a PdfGrid
                    PdfGrid pdfGridDetallesAlbaran = new PdfGrid();
                    pdfGridDetallesAlbaran.Style.CellPadding.Left = cellMargin;
                    pdfGridDetallesAlbaran.Style.CellPadding.Right = cellMargin;
                    pdfGridDetallesAlbaran.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent1);
                    pdfGridDetallesAlbaran = addLineasAlbaranRecepcionToGrid(pdfGridDetallesAlbaran, albaran);
                    pdfGridDetallesAlbaran.Style.Font = contentFont;

                    PdfGridLayoutResult pdfGridLayoutResult1 = pdfGridDetallesAlbaran.Draw(page, new Syncfusion.Drawing.PointF(0, pdfGridLayoutResult0.Bounds.Bottom + 20));

                    // 3 Create a PdfGrid datos del proveedor

                    PdfTextElement contentDatos = new PdfTextElement("Datos del proveedor\n", contentFont, PdfBrushes.Black);
                    format.Layout = PdfLayoutType.Paginate;
                    PdfLayoutResult resultede = contentDatos.Draw(page, new RectangleF(0, pdfGridLayoutResult1.Bounds.Bottom + paragraphAfterSpacing, page.GetClientSize().Width, page.GetClientSize().Height), format);

                    PdfGrid pdfGridDireccion = new PdfGrid();
                    pdfGridDireccion.Style.CellPadding.Left = cellMargin;
                    pdfGridDireccion.Style.CellPadding.Right = cellMargin;
                    pdfGridDireccion.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable4Accent1);
                    pdfGridDireccion = addDatosProveedorGrid(pdfGridDireccion, proveedor);
                    pdfGridDireccion.Style.Font = contentFont;

                    pdfGridDireccion.Draw(page, new Syncfusion.Drawing.PointF(0, resultede.Bounds.Bottom + 5));


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
        }

        private static PdfGrid addIdPedidoYFechaToGrid(PdfGrid pdfGrid, Pedido pedido, double costePedido)
        {
            //Add 2 columns.
            pdfGrid.Columns.Add(2);

            //Add rows.
            PdfGridRow pdfGridRow0 = pdfGrid.Rows.Add();
            PdfGridRow pdfGridRow1 = pdfGrid.Rows.Add();

            pdfGridRow0.Cells[0].Value = "ID del pedido";
            pdfGridRow0.Cells[1].Value = pedido.PedidoId.ToString();

            pdfGridRow1.Cells[0].Value = "Fecha de compra";
            pdfGridRow1.Cells[1].Value = pedido.FechaPedido.ToString();

            return pdfGrid;
        }
        private static PdfGrid addLineasToGrid(PdfGrid pdfGrid, UsuarioModel user, List<ProductoModel> productos, List<LineaPedidoModel> lineas)
        {
            //Add 6 columns.
            pdfGrid.Columns.Add(6);

            //Add header.
            pdfGrid.Headers.Add(1);
            PdfGridRow pdfGridHeader = pdfGrid.Headers[0];

            pdfGridHeader.Cells[0].Value = "Producto";
            pdfGridHeader.Cells[1].Value = "% IVA";
            pdfGridHeader.Cells[2].Value = "Precio /ud";
            pdfGridHeader.Cells[3].Value = "Precio /ud. + IVA";
            pdfGridHeader.Cells[4].Value = "Cantidad";
            pdfGridHeader.Cells[5].Value = "Importe total + IVA";

            //Add rows.
            foreach (var producto in productos)
            {
                foreach (var linea in lineas)
                {
                    if (linea.ProductoLineaPedido == producto.ProductoId)
                    {
                        PdfGridRow pdfGridRow = pdfGrid.Rows.Add();
                        pdfGridRow.Cells[0].Value = producto.Nombre.ToString();
                        pdfGridRow.Cells[1].Value = producto.Iva.ToString();
                        pdfGridRow.Cells[2].Value = producto.Precio.ToString() + "e";
                        pdfGridRow.Cells[3].Value = FinalPriceCalculator.FinalPrice(producto).ToString() + "e";
                        pdfGridRow.Cells[4].Value = linea.Cantidad.ToString();
                        pdfGridRow.Cells[5].Value = FinalPriceCalculator.FinalPrice(linea.PrecioFinal, producto.Iva).ToString() + "e";
                    }
                }
            }
            return pdfGrid;
        }
        private static PdfGrid addPedidoToGrid(PdfGrid pdfGrid, Pedido pedido, double costePedido)
        {
            if (pedido.Envio)
            {
                //Add 2 columns.
                pdfGrid.Columns.Add(2);

                //Add rows.
                PdfGridRow pdfGridRow0 = pdfGrid.Rows.Add();
                PdfGridRow pdfGridRow1 = pdfGrid.Rows.Add();
                PdfGridRow pdfGridRow2 = pdfGrid.Rows.Add();

                pdfGridRow0.Cells[0].Value = "Importe";
                pdfGridRow0.Cells[1].Value = Math.Round(pedido.PrecioTotal - costePedido, 2).ToString() + "e";

                pdfGridRow1.Cells[0].Value = "Gastos de envío";
                pdfGridRow1.Cells[1].Value = costePedido.ToString();

                pdfGridRow2.Cells[0].Value = "Total";
                pdfGridRow2.Cells[1].Value = Math.Round(pedido.PrecioTotal, 2).ToString() + "e";

                return pdfGrid;
            }
            else
            {
                //Add three columns.
                pdfGrid.Columns.Add(2);

                //Add rows.
                PdfGridRow pdfGridRow0 = pdfGrid.Rows.Add();
                PdfGridRow pdfGridRow1 = pdfGrid.Rows.Add();

                pdfGridRow0.Cells[0].Value = "Importe";
                pdfGridRow0.Cells[1].Value = Math.Round(pedido.PrecioTotal - costePedido, 2).ToString() + "e";

                pdfGridRow1.Cells[0].Value = "Total";
                pdfGridRow1.Cells[1].Value = Math.Round(pedido.PrecioTotal, 2).ToString() + " €";

                return pdfGrid;
            }

        }
        private static PdfGrid addAlbaranToGrid(PdfGrid pdfGrid, AlbaranModel albaran)
        {
            //Add three columns.
            pdfGrid.Columns.Add(2);

            //Add rows.
            PdfGridRow pdfGridRow0 = pdfGrid.Rows.Add();
            PdfGridRow pdfGridRow1 = pdfGrid.Rows.Add();

            pdfGridRow0.Cells[0].Value = "ID del albaran";
            pdfGridRow0.Cells[1].Value = albaran.AlbaranId.ToString();
            pdfGridRow1.Cells[0].Value = "Fecha de entrega";
            pdfGridRow1.Cells[1].Value = albaran.FechaEntrega.ToShortDateString().ToString();

            return pdfGrid;
        }
        private static PdfGrid addLineasAlbaranEntregaToGrid(PdfGrid pdfGrid, List<ProductoModel> productos, List<LineaPedidoModel> lineas)
        {

            //Add three columns.
            pdfGrid.Columns.Add(6);

            //Add header.
            pdfGrid.Headers.Add(1);
            PdfGridRow pdfGridHeader = pdfGrid.Headers[0];

            pdfGridHeader.Cells[0].Value = "Producto";
            pdfGridHeader.Cells[1].Value = "% IVA";
            pdfGridHeader.Cells[2].Value = "Precio /ud";
            pdfGridHeader.Cells[3].Value = "Precio /ud. (+ IVA)";
            pdfGridHeader.Cells[4].Value = "Cantidad";
            pdfGridHeader.Cells[5].Value = "Importe total (+ IVA)";

            //Add rows.
            foreach (var producto in productos)
            {
                foreach (var linea in lineas)
                {
                    if (linea.ProductoLineaPedido == producto.ProductoId)
                    {
                        PdfGridRow pdfGridRow = pdfGrid.Rows.Add();
                        pdfGridRow.Cells[0].Value = producto.Nombre.ToString();
                        pdfGridRow.Cells[1].Value = producto.Iva.ToString();
                        pdfGridRow.Cells[2].Value = producto.Precio.ToString() + "e";
                        pdfGridRow.Cells[3].Value = FinalPriceCalculator.FinalPrice(producto).ToString() + "e";
                        pdfGridRow.Cells[4].Value = linea.Cantidad.ToString();
                        pdfGridRow.Cells[5].Value = FinalPriceCalculator.FinalPrice(linea.PrecioFinal, producto.Iva).ToString() + "e";
                    }
                }
            }
            return pdfGrid;
        }
        private static PdfGrid addLineasAlbaranRecepcionToGrid(PdfGrid pdfGrid, AlbaranModel albaran)
        {
            //Add 2 columns.
            pdfGrid.Columns.Add(2);

            //Add header.
            pdfGrid.Headers.Add(1);
            PdfGridRow pdfGridHeader = pdfGrid.Headers[0];

            pdfGridHeader.Cells[0].Value = "Producto";
            pdfGridHeader.Cells[1].Value = "Cantidad";

            PdfGridRow pdfGridRow = pdfGrid.Rows.Add();
            pdfGridRow.Cells[0].Value = albaran.ProductoAlbaran.ToString();
            pdfGridRow.Cells[1].Value = albaran.CantidadProductoAlbaran.ToString();
            return pdfGrid;
        }
        private static PdfGrid addDatosProveedorGrid(PdfGrid pdfGrid, ProveedorModel proveedor)
        {
            //Add 2 columns.
            pdfGrid.Columns.Add(2);

            //Add rows.
            PdfGridRow pdfGridRow0 = pdfGrid.Rows.Add();
            PdfGridRow pdfGridRow1 = pdfGrid.Rows.Add();
            PdfGridRow pdfGridRow2 = pdfGrid.Rows.Add();
            PdfGridRow pdfGridRow3 = pdfGrid.Rows.Add();
            PdfGridRow pdfGridRow4 = pdfGrid.Rows.Add();

            pdfGridRow0.Cells[0].Value = "Nombre";
            pdfGridRow0.Cells[1].Value = proveedor.Nombre.ToString();

            pdfGridRow1.Cells[0].Value = "Dirección";
            pdfGridRow1.Cells[1].Value = proveedor.Direccion.ToString();

            pdfGridRow2.Cells[0].Value = "Código Postal";
            pdfGridRow2.Cells[1].Value = proveedor.CodigoPostal.ToString();

            pdfGridRow3.Cells[0].Value = "Teléfono";
            pdfGridRow3.Cells[1].Value = proveedor.Telefono.ToString();

            pdfGridRow4.Cells[0].Value = "Email";
            pdfGridRow4.Cells[1].Value = (proveedor.Email != null ? proveedor.Email.ToString() : "");

            return pdfGrid;
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

            pdfGridRow3.Cells[0].Value = "Teléfono";
            pdfGridRow3.Cells[1].Value = user.Telefono.ToString();

            pdfGridRow4.Cells[0].Value = "DNI";
            pdfGridRow4.Cells[1].Value = user.Dni.ToString();

            pdfGridRow5.Cells[0].Value = "Dirección de entrega";
            pdfGridRow5.Cells[1].Value = user.Direccion.ToString();

            pdfGridRow6.Cells[0].Value = "Código Postal";
            pdfGridRow6.Cells[1].Value = user.CodigoPostal.ToString();

            pdfGridRow7.Cells[0].Value = "Población";
            pdfGridRow7.Cells[1].Value = user.Poblacion.ToString();

            return pdfGrid;
        }
    }

}