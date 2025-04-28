using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Net;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using ZXing;
using ZXing.QrCode;
using ZXing.QrCode.Internal;
using System.Drawing.Imaging;
using Font = iTextSharp.text.Font;
using Color = iTextSharp.text.BaseColor;
using Rectangle = iTextSharp.text.Rectangle;
using Image = iTextSharp.text.Image;

namespace GeneradorDocumentos
{
    public class GeneradorPdfDte03
    {
        private readonly BaseFont _fuenteBase;
        private readonly Font _fuenteTitulo;
        private readonly Font _fuenteSubtitulo;
        private readonly Font _fuenteNormal;
        private readonly Font _fuenteNegrita;
        private readonly Font _fuentePequena;
        private readonly Font _fuenteEncabezado;
        private readonly Color _colorPrimario = new Color(16, 78, 139); // Azul profesional
        private string sello;

        public GeneradorPdfDte03()
        {
            // Inicializar fuentes
            _fuenteBase = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            _fuenteTitulo = new Font(_fuenteBase, 14, Font.BOLD, _colorPrimario);
            _fuenteSubtitulo = new Font(_fuenteBase, 12, Font.BOLD, _colorPrimario);
            _fuenteEncabezado = new Font(_fuenteBase, 10, Font.BOLD, _colorPrimario);
            _fuenteNormal = new Font(_fuenteBase, 9, Font.NORMAL, Color.BLACK);
            _fuenteNegrita = new Font(_fuenteBase, 9, Font.BOLD, Color.BLACK);
            _fuentePequena = new Font(_fuenteBase, 8, Font.NORMAL, Color.BLACK);
        }

        /// <summary>
        /// Genera un PDF de un DTE tipo 03 en memoria
        /// </summary>
        /// <param name="jsonDte">El JSON del DTE tipo 03</param>
        /// <returns>Arreglo de bytes del PDF generado</returns>
        public byte[] GenerarPdfEnMemoria(string jsonDte, string sello)
        {
            this.sello = sello;
            using (MemoryStream ms = new MemoryStream())
            {
                // Deserializar el JSON
                dynamic dte = JsonConvert.DeserializeObject(jsonDte);

                // Crear el documento
                Document documento = new Document(PageSize.LETTER);
                PdfWriter writer = PdfWriter.GetInstance(documento, ms);
                documento.Open();

                // Agregar metadata al documento
                documento.AddAuthor("Sistema de Facturación Electrónica");
                documento.AddCreator("KeyjoTech Development");
                documento.AddTitle("Comprobante de Crédito Fiscal");

                // Generar el contenido del PDF
                AgregarEncabezado(documento, dte);
                AgregarDatosEmisorReceptor(documento, dte);
                AgregarDetalleProductos(documento, dte);
                AgregarResumenPago(documento, dte);
                AgregarNotasFinales(documento, dte);
                AgregarCodigoQR(documento, dte);

                documento.Close();
                return ms.ToArray();
            }
        }

        private void AgregarEncabezado(Document documento, dynamic dte)
        {
            PdfPTable tablaEncabezado = new PdfPTable(1);
            tablaEncabezado.WidthPercentage = 100;

            // Empresa (fuente 10px)
            Paragraph empresa = new Paragraph(dte.emisor.nombre.ToString(), new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD));
            empresa.Add(new Phrase("\n" + ObtenerDireccionCompleta(dte.emisor.direccion) + " | Tel: " + dte.emisor.telefono, new Font(Font.FontFamily.HELVETICA, 8)));
            tablaEncabezado.AddCell(new PdfPCell(empresa) { Border = Rectangle.NO_BORDER });

            // Título DTE (fuente 12px) - Cambiado a "COMPROBANTE DE CRÉDITO FISCAL"
            Paragraph titulo = new Paragraph("COMPROBANTE DE CRÉDITO FISCAL", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD));
            tablaEncabezado.AddCell(new PdfPCell(titulo) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER });

            // Info DTE (fuente 8px)
            PdfPCell celdaInfo = new PdfPCell();
            celdaInfo.Border = Rectangle.BOX;
            celdaInfo.Padding = 5f;
            celdaInfo.AddElement(new Paragraph("N° Control: " + dte.identificacion.numeroControl, new Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)));
            celdaInfo.AddElement(new Paragraph("Código: " + dte.identificacion.codigoGeneracion, new Font(Font.FontFamily.HELVETICA, 8)));
            celdaInfo.AddElement(new Paragraph("Sello: " + sello, new Font(Font.FontFamily.HELVETICA, 8)));
            celdaInfo.AddElement(new Paragraph("Fecha: " + FormatearFechaHora(dte.identificacion.fecEmi.ToString(), dte.identificacion.horEmi.ToString()), new Font(Font.FontFamily.HELVETICA, 8)));
            tablaEncabezado.AddCell(celdaInfo);

            documento.Add(tablaEncabezado);
        }

        private void AgregarDatosEmisorReceptor(Document documento, dynamic dte)
        {
            PdfPTable tablaDatos = new PdfPTable(2);
            tablaDatos.WidthPercentage = 100;
            tablaDatos.SetWidths(new float[] { 50f, 50f });

            // Datos del emisor
            PdfPCell celdaEmisor = new PdfPCell();
            celdaEmisor.BackgroundColor = new Color(240, 240, 240);
            celdaEmisor.PaddingLeft = 5f;
            celdaEmisor.PaddingRight = 5f;
            celdaEmisor.PaddingTop = 5f;
            celdaEmisor.PaddingBottom = 5f;

            Paragraph tituloEmisor = new Paragraph("DATOS DEL EMISOR", _fuenteEncabezado);
            tituloEmisor.Alignment = Element.ALIGN_LEFT;
            celdaEmisor.AddElement(tituloEmisor);

            Paragraph nitEmisor = new Paragraph("NIT: " + FormatearNit(dte.emisor.nit.ToString()), _fuenteNormal);
            nitEmisor.Alignment = Element.ALIGN_LEFT;
            celdaEmisor.AddElement(nitEmisor);

            Paragraph nrcEmisor = new Paragraph("NRC: " + dte.emisor.nrc, _fuenteNormal);
            nrcEmisor.Alignment = Element.ALIGN_LEFT;
            celdaEmisor.AddElement(nrcEmisor);

            Paragraph actividadEmisor = new Paragraph("Actividad: " + dte.emisor.descActividad, _fuenteNormal);
            actividadEmisor.Alignment = Element.ALIGN_LEFT;
            celdaEmisor.AddElement(actividadEmisor);

            Paragraph correoEmisor = new Paragraph("Correo: " + dte.emisor.correo, _fuenteNormal);
            correoEmisor.Alignment = Element.ALIGN_LEFT;
            celdaEmisor.AddElement(correoEmisor);

            tablaDatos.AddCell(celdaEmisor);

            // Datos del receptor
            PdfPCell celdaReceptor = new PdfPCell();
            celdaReceptor.BackgroundColor = new Color(240, 240, 240);
            celdaReceptor.PaddingLeft = 5f;
            celdaReceptor.PaddingRight = 5f;
            celdaReceptor.PaddingTop = 5f;
            celdaReceptor.PaddingBottom = 5f;

            Paragraph tituloReceptor = new Paragraph("DATOS DEL CLIENTE", _fuenteEncabezado);
            tituloReceptor.Alignment = Element.ALIGN_LEFT;
            celdaReceptor.AddElement(tituloReceptor);

            Paragraph nombreReceptor = new Paragraph("Nombre: " + dte.receptor.nombre, _fuenteNormal);
            nombreReceptor.Alignment = Element.ALIGN_LEFT;
            celdaReceptor.AddElement(nombreReceptor);

            Paragraph nitReceptor = new Paragraph("NIT: " + FormatearNit(dte.receptor.nit.ToString()), _fuenteNormal);
            nitReceptor.Alignment = Element.ALIGN_LEFT;
            celdaReceptor.AddElement(nitReceptor);

            Paragraph nrcReceptor = new Paragraph("NRC: " + dte.receptor.nrc, _fuenteNormal);
            nrcReceptor.Alignment = Element.ALIGN_LEFT;
            celdaReceptor.AddElement(nrcReceptor);

            Paragraph direccionReceptor = new Paragraph("Dirección: " + ObtenerDireccionCompleta(dte.receptor.direccion), _fuenteNormal);
            direccionReceptor.Alignment = Element.ALIGN_LEFT;
            celdaReceptor.AddElement(direccionReceptor);

            Paragraph telefonoReceptor = new Paragraph("Teléfono: " + dte.receptor.telefono, _fuenteNormal);
            telefonoReceptor.Alignment = Element.ALIGN_LEFT;
            celdaReceptor.AddElement(telefonoReceptor);

            Paragraph correoReceptor = new Paragraph("Correo: " + dte.receptor.correo, _fuenteNormal);
            correoReceptor.Alignment = Element.ALIGN_LEFT;
            celdaReceptor.AddElement(correoReceptor);

            tablaDatos.AddCell(celdaReceptor);

            documento.Add(tablaDatos);
            documento.Add(new Paragraph(" "));
        }

        private void AgregarDetalleProductos(Document documento, dynamic dte)
        {
            Paragraph tituloDetalle = new Paragraph("DETALLE DE PRODUCTOS", _fuenteSubtitulo);
            tituloDetalle.Alignment = Element.ALIGN_CENTER;
            documento.Add(tituloDetalle);
            documento.Add(new Paragraph(" ", _fuentePequena));

            PdfPTable tablaDetalle = new PdfPTable(6);
            tablaDetalle.WidthPercentage = 100;
            tablaDetalle.SetWidths(new float[] { 10f, 40f, 10f, 13f, 13f, 14f });

            // Encabezados
            CeldaEncabezado(tablaDetalle, "CANT.");
            CeldaEncabezado(tablaDetalle, "DESCRIPCIÓN");
            CeldaEncabezado(tablaDetalle, "UNIDAD");
            CeldaEncabezado(tablaDetalle, "PRECIO UNIT.");
            CeldaEncabezado(tablaDetalle, "DESCUENTO");
            CeldaEncabezado(tablaDetalle, "TOTAL");

            // Detalle de productos
            foreach (var item in dte.cuerpoDocumento)
            {
                CeldaDetalle(tablaDetalle, item.cantidad.ToString());
                CeldaDetalle(tablaDetalle, item.descripcion.ToString());
                CeldaDetalle(tablaDetalle, ObtenerUnidadMedida(item.uniMedida.ToString()));
                CeldaDetalle(tablaDetalle, string.Format("${0:0.00}", item.precioUni));
                CeldaDetalle(tablaDetalle, string.Format("${0:0.00}", item.montoDescu));
                CeldaDetalle(tablaDetalle, string.Format("${0:0.00}", item.ventaGravada));
            }

            documento.Add(tablaDetalle);
            documento.Add(new Paragraph(" "));
        }

        private void AgregarResumenPago(Document documento, dynamic dte)
        {
            PdfPTable tablaResumen = new PdfPTable(2);
            tablaResumen.WidthPercentage = 100;
            tablaResumen.SetWidths(new float[] { 65f, 35f });

            // Columna izquierda: Total en letras y forma de pago
            PdfPCell celdaIzquierda = new PdfPCell();
            celdaIzquierda.Border = Rectangle.BOX;
            celdaIzquierda.BorderColor = _colorPrimario;
            celdaIzquierda.PaddingLeft = 5f;
            celdaIzquierda.PaddingRight = 5f;
            celdaIzquierda.PaddingTop = 5f;
            celdaIzquierda.PaddingBottom = 5f;

            Paragraph tituloTotalLetras = new Paragraph("TOTAL EN LETRAS:", _fuenteEncabezado);
            tituloTotalLetras.Alignment = Element.ALIGN_LEFT;
            celdaIzquierda.AddElement(tituloTotalLetras);

            Paragraph totalLetras = new Paragraph(dte.resumen.totalLetras.ToString().ToUpper(), _fuenteNegrita);
            totalLetras.Alignment = Element.ALIGN_LEFT;
            celdaIzquierda.AddElement(totalLetras);

            Paragraph tituloPago = new Paragraph("CONDICIÓN DE PAGO:", _fuenteEncabezado);
            tituloPago.SpacingBefore = 10f;
            tituloPago.Alignment = Element.ALIGN_LEFT;
            celdaIzquierda.AddElement(tituloPago);

            string condicionPago = ObtenerCondicionPago(dte.resumen.condicionOperacion.ToString());
            Paragraph formaPago = new Paragraph(condicionPago, _fuenteNormal);
            formaPago.Alignment = Element.ALIGN_LEFT;
            celdaIzquierda.AddElement(formaPago);

            if (dte.resumen.pagos != null && dte.resumen.pagos.Count > 0)
            {
                foreach (var pago in dte.resumen.pagos)
                {
                    string formaPagoTexto = ObtenerFormaPago(pago.codigo.ToString());
                    Paragraph detallePago = new Paragraph($"- {formaPagoTexto}: ${pago.montoPago:0.00}", _fuenteNormal);
                    detallePago.Alignment = Element.ALIGN_LEFT;
                    celdaIzquierda.AddElement(detallePago);
                }
            }

            tablaResumen.AddCell(celdaIzquierda);

            // Columna derecha: Totales
            PdfPCell celdaDerecha = new PdfPCell();
            celdaDerecha.Border = Rectangle.BOX;
            celdaDerecha.BorderColor = _colorPrimario;
            celdaDerecha.PaddingLeft = 5f;
            celdaDerecha.PaddingRight = 5f;
            celdaDerecha.PaddingTop = 5f;
            celdaDerecha.PaddingBottom = 5f;

            // Subtabla para los totales
            PdfPTable tablaTotales = new PdfPTable(2);
            tablaTotales.WidthPercentage = 100;
            tablaTotales.SetWidths(new float[] { 60f, 40f });

            AgregarFilaTotales(tablaTotales, "SUMAS:", string.Format("${0:0.00}", dte.resumen.subTotal));

            // Agregar fila para el IVA (específico para crédito fiscal)
            AgregarFilaTotales(tablaTotales, "IVA 13%:", string.Format("${0:0.00}", dte.resumen.tributos[0].valor));

            if (dte.resumen.reteRenta > 0)
            {
                AgregarFilaTotales(tablaTotales, "RETENCIÓN:", string.Format("${0:0.00}", dte.resumen.reteRenta));
            }

            if (dte.resumen.totalDescu > 0)
            {
                AgregarFilaTotales(tablaTotales, "DESCUENTO:", string.Format("${0:0.00}", dte.resumen.totalDescu));
            }

            // Fila del total con fondo coloreado
            PdfPCell celdaLabelTotal = new PdfPCell(new Phrase("TOTAL A PAGAR:", _fuenteNegrita));
            celdaLabelTotal.BackgroundColor = _colorPrimario;
            celdaLabelTotal.BorderColor = _colorPrimario;
            celdaLabelTotal.HorizontalAlignment = Element.ALIGN_LEFT;
            tablaTotales.AddCell(celdaLabelTotal);

            PdfPCell celdaValorTotal = new PdfPCell(new Phrase(string.Format("${0:0.00}", dte.resumen.totalPagar), _fuenteNegrita));
            celdaValorTotal.BackgroundColor = _colorPrimario;
            celdaValorTotal.BorderColor = _colorPrimario;
            celdaValorTotal.HorizontalAlignment = Element.ALIGN_RIGHT;
            tablaTotales.AddCell(celdaValorTotal);

            celdaDerecha.AddElement(tablaTotales);
            tablaResumen.AddCell(celdaDerecha);

            documento.Add(tablaResumen);
            documento.Add(new Paragraph(" "));
        }
        public void AbrirPdfEnNavegador(byte[] pdfBytes)
        {
            // Crear un archivo temporal
            string rutaTemporal = Path.Combine(Path.GetTempPath(), $"DTE_{Guid.NewGuid()}.pdf");

            // Guardar los bytes en el archivo temporal
            File.WriteAllBytes(rutaTemporal, pdfBytes);

            // Abrir en el navegador predeterminado
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = rutaTemporal,
                UseShellExecute = true
            });
        }
        private void AgregarNotasFinales(Document documento, dynamic dte)
        {
            PdfPTable tablaNotas = new PdfPTable(1);
            tablaNotas.WidthPercentage = 100;

            PdfPCell celdaNotas = new PdfPCell();
            celdaNotas.Border = Rectangle.BOX;
            celdaNotas.BorderColor = new Color(200, 200, 200);
            celdaNotas.PaddingLeft = 5f;
            celdaNotas.PaddingRight = 5f;
            celdaNotas.PaddingTop = 5f;
            celdaNotas.PaddingBottom = 5f;

            // Título de notas
            Paragraph tituloNotas = new Paragraph("NOTAS IMPORTANTES", _fuenteEncabezado);
            tituloNotas.Alignment = Element.ALIGN_LEFT;
            celdaNotas.AddElement(tituloNotas);

            // Observaciones si existen
            if (dte.extension != null && !string.IsNullOrEmpty(dte.extension.observaciones?.ToString()))
            {
                Paragraph observaciones = new Paragraph("Observaciones: " + dte.extension.observaciones, _fuenteNormal);
                observaciones.Alignment = Element.ALIGN_LEFT;
                celdaNotas.AddElement(observaciones);
            }

            // Notas legales específicas para crédito fiscal
            Paragraph notaLegal = new Paragraph("Este documento es un Comprobante de Crédito Fiscal autorizado por la Dirección General de Impuestos Internos y es válido como documento tributario.", _fuentePequena);
            notaLegal.Alignment = Element.ALIGN_JUSTIFIED;
            celdaNotas.AddElement(notaLegal);

            Paragraph notaCredito = new Paragraph("El crédito fiscal correspondiente al IVA consignado en este documento puede ser utilizado como deducción de impuestos según la normativa tributaria vigente.", _fuentePequena);
            notaCredito.Alignment = Element.ALIGN_JUSTIFIED;
            celdaNotas.AddElement(notaCredito);

            tablaNotas.AddCell(celdaNotas);
            documento.Add(tablaNotas);
            documento.Add(new Paragraph(" "));
        }

        private void AgregarCodigoQR(Document documento, dynamic dte)
        {
            string urlConsulta = $"https://admin.factura.gob.sv/consultaPublica?" +
                                $"ambiente={dte.identificacion.ambiente}&" +
                                $"codGen={dte.identificacion.codigoGeneracion}&" +
                                $"fechaEmi={dte.identificacion.fecEmi.ToString("yyyy-MM-dd")}";

            BarcodeQRCode qrCode = new BarcodeQRCode(urlConsulta, 150, 150, null);
            Image imgQR = qrCode.GetImage();
            imgQR.ScaleAbsolute(100, 100);
            imgQR.Alignment = Element.ALIGN_CENTER;

            documento.Add(imgQR);
        }

        #region Métodos auxiliares (los mismos que en el original)
        private void CeldaEncabezado(PdfPTable tabla, string texto)
        {
            PdfPCell celda = new PdfPCell(new Phrase(texto, _fuenteNegrita));
            celda.BackgroundColor = _colorPrimario;
            celda.HorizontalAlignment = Element.ALIGN_CENTER;
            celda.VerticalAlignment = Element.ALIGN_MIDDLE;
            celda.Padding = 5f;
            tabla.AddCell(celda);
        }

        private void CeldaDetalle(PdfPTable tabla, string texto)
        {
            PdfPCell celda = new PdfPCell(new Phrase(texto, _fuenteNormal));
            celda.HorizontalAlignment = Element.ALIGN_CENTER;
            celda.VerticalAlignment = Element.ALIGN_MIDDLE;
            celda.Padding = 5f;
            tabla.AddCell(celda);
        }

        private void AgregarFilaTotales(PdfPTable tabla, string etiqueta, string valor)
        {
            PdfPCell celdaEtiqueta = new PdfPCell(new Phrase(etiqueta, _fuenteNormal));
            celdaEtiqueta.Border = Rectangle.BOTTOM_BORDER;
            celdaEtiqueta.BorderColor = new Color(200, 200, 200);
            celdaEtiqueta.HorizontalAlignment = Element.ALIGN_LEFT;
            tabla.AddCell(celdaEtiqueta);

            PdfPCell celdaValor = new PdfPCell(new Phrase(valor, _fuenteNormal));
            celdaValor.Border = Rectangle.BOTTOM_BORDER;
            celdaValor.BorderColor = new Color(200, 200, 200);
            celdaValor.HorizontalAlignment = Element.ALIGN_RIGHT;
            tabla.AddCell(celdaValor);
        }

        private string ObtenerTipoDte(string codigo)
        {
            switch (codigo)
            {
                case "01": return "FACTURA ELECTRÓNICA";
                case "03": return "COMPROBANTE DE CRÉDITO FISCAL";
                case "04": return "NOTA DE REMISIÓN";
                case "05": return "NOTA DE CRÉDITO";
                case "06": return "NOTA DE DÉBITO";
                case "07": return "COMPROBANTE DE RETENCIÓN";
                case "08": return "COMPROBANTE DE LIQUIDACIÓN";
                case "09": return "DOCUMENTO CONTABLE DE LIQUIDACIÓN";
                case "11": return "FACTURA DE EXPORTACIÓN";
                case "14": return "FACTURA DE SUJETO EXCLUIDO";
                default: return "DOCUMENTO TRIBUTARIO ELECTRÓNICO";
            }
        }

        private string ObtenerUnidadMedida(string codigo)
        {
            switch (codigo)
            {
                case "59": return "Unidad";
                case "58": return "Kg";
                case "37": return "Libra";
                case "13": return "Metro";
                case "16": return "Onza";
                default: return codigo;
            }
        }

        private string ObtenerCondicionPago(string codigo)
        {
            switch (codigo)
            {
                case "1": return "Contado";
                case "2": return "Crédito";
                default: return "Otra";
            }
        }

        private string ObtenerFormaPago(string codigo)
        {
            switch (codigo)
            {
                case "01": return "Efectivo";
                case "02": return "Tarjeta Débito";
                case "03": return "Tarjeta Crédito";
                case "04": return "Cheque";
                case "05": return "Transferencia";
                default: return "Otro";
            }
        }

        private string ObtenerDireccionCompleta(dynamic direccion)
        {
            if (direccion == null) return "No especificada";

            string departamento = ObtenerNombreDepartamento(direccion.departamento.ToString());
            string municipio = ObtenerNombreMunicipio(direccion.municipio.ToString());
            string complemento = direccion.complemento?.ToString() ?? "";

            return $"{complemento}, {municipio}, {departamento}";
        }

        private string ObtenerNombreDepartamento(string codigo)
        {
            switch (codigo)
            {
                case "01": return "Ahuachapán";
                case "02": return "Santa Ana";
                case "03": return "Sonsonate";
                case "04": return "Chalatenango";
                case "05": return "La Libertad";
                case "06": return "San Salvador";
                case "07": return "Cuscatlán";
                case "08": return "La Paz";
                case "09": return "Cabañas";
                case "10": return "San Vicente";
                case "11": return "Usulután";
                case "12": return "San Miguel";
                case "13": return "Morazán";
                case "14": return "La Unión";
                default: return codigo;
            }
        }

        private string ObtenerNombreMunicipio(string codigo)
        {
            if (codigo == "35" && ObtenerNombreDepartamento("04") == "Chalatenango")
                return "Chalatenango";

            return $"Municipio {codigo}";
        }

        private string FormatearFechaHora(string fecha, string hora)
        {
            try
            {
                DateTime fechaHora = DateTime.Parse($"{fecha} {hora}");
                return fechaHora.ToString("dd/MM/yyyy HH:mm:ss");
            }
            catch
            {
                return $"{fecha} {hora}";
            }
        }

        private string FormatearNit(string nit)
        {
            if (string.IsNullOrEmpty(nit) || nit.Length != 14) return nit;

            return $"{nit.Substring(0, 4)}-{nit.Substring(4, 6)}-{nit.Substring(10, 3)}-{nit.Substring(13, 1)}";
        }
        #endregion
    }
}