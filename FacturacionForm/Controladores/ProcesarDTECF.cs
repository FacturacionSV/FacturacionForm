using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using FacturacionForm.entidades;
using FacturacionForm.utilidades;

namespace FacturacionForm.Controladores
{
    public class ProcesarDTECF
    {

        public Emisor Emisor { get; set; }
        public Receptor Receptor { get; set; }
        public List<DetalleVenta> DetallesVenta { get; set; }


        public ProcesarDTECF(List<DetalleVenta> detalles, Emisor emisor, Receptor receptor)
        {
            Emisor = emisor;
            Receptor = receptor;
            DetallesVenta = detalles;

        }

        public bool EnviarDTE()
        {
            string dteJson = "";
            Guid codigoGeneracion = Guid.NewGuid();

            Random random = new Random();
            int i = random.Next(1, 100001); // Genera un número aleatorio entre 1 y 100000
            string correlativo = i.ToString().PadLeft(15, '0'); // Rellena con ceros a la izquierda para que tenga 15 caracteres
            // Generar número de control
            string numeroControl = "DTE-" + "01" + "-" + "ABCD1234" + "-" + correlativo;

            // ESQUEMA PARA UN DTE DE CONSUMIDOR FINAL DTE 01

            var identificacion = new
            {
                version = 1,
                ambiente = "00",
                tipoDte = "01",
                numeroControl = numeroControl,
                codigoGeneracion = codigoGeneracion.ToString().ToUpper(),
                tipoModelo = 1,
                tipoOperacion = 1,
                tipoContingencia = (string)null,
                motivoContin = (string)null,
                fecEmi = DateTime.Now.ToString("yyyy-MM-dd"),
                horEmi = DateTime.Now.ToString("HH:mm:ss"),
                tipoMoneda = "USD"
            };

            // Crear el objeto para el emisor
            var emisor = new
            {
                nit = Emisor.NIT,
                nrc = Emisor.NRC,
                nombre = Emisor.NombreComercial,
                codActividad = Emisor.CodigoActividadEconomica,
                descActividad = Emisor.ActividadEconomica,
                nombreComercial = Emisor.NombreComercial,
                tipoEstablecimiento = "02",
                direccion = new
                {
                    departamento = Emisor.CodigoDepartamento,
                    municipio = Emisor.CodigoDistrito,
                    complemento = Emisor.Direccion
                },
                telefono = Emisor.Telefono,
                codEstableMH = (string)null,
                codEstable = (string)null,
                codPuntoVentaMH = (string)null,
                codPuntoVenta = (string)null,
                correo = Emisor.Email
            };

            // Crear el objeto para el receptor
            var receptor = new
            {
                tipoDocumento = "37",
                numDocumento = (string)null,
                nrc = (string)null,
                nombre = Receptor.NombreComercial,
                codActividad = (string)null,
                descActividad = (string)null,
                direccion = new
                {
                    departamento = Receptor.CodigoDepartamento,
                    municipio = Receptor.CodigoDistrito,
                    complemento = Receptor.Direccion
                },
                telefono = Receptor.Telefono,
                correo = Receptor.Email
            };

            // Crear el cuerpo del documento
            var cuerpoDocumento = DetallesVenta
            .Select((item, index) => new
            {
                numItem = index + 1, // Índice + 1 para empezar desde 1
                tipoItem = 1,
                numeroDocumento = (string)null,
                cantidad = item.Cantidad,
                codigo = "0000"+index.ToString(),
                codTributo = (string)null,
                uniMedida = 59,
                descripcion = item.Descripcion,
                precioUni = item.Precio,
                montoDescu = 0.0,
                ventaNoSuj = 0.0,
                ventaExenta = 0.0,
                ventaGravada = item.Total,
                tributos = (string)null,
                psv = item.Precio,
                noGravado = 0.0,
                ivaItem = Math.Round(item.Total-(item.Total/1.13m),6)
            })
            .ToArray();

            // Crear el resumen
            // Calcular las variables primero
            decimal totalVenta = DetallesVenta.Sum(x => x.Total);
            decimal subTotalVentas = totalVenta;
            decimal subTotal = totalVenta;
            decimal montoTotalOperacion = totalVenta;
            decimal totalPagar = totalVenta;
            string totalLetras = new Conversor().ConvertirNumeroALetras(totalVenta);
            decimal totalIva = Math.Round(totalVenta - (totalVenta / 1.13m), 2);

            // Crear el objeto resumen con las variables
            var resumen = new
            {
                totalNoSuj = 0.00,
                totalExenta = 0.0,
                totalGravada = totalVenta,
                subTotalVentas = subTotalVentas,
                descuNoSuj = 0.0,
                descuExenta = 0.0,
                descuGravada = 0.0,
                porcentajeDescuento = 0,
                totalDescu = 0.0,
                tributos = (string)null,
                subTotal = subTotal,
                ivaRete1 = 0.00,
                reteRenta = 0.0,
                montoTotalOperacion = montoTotalOperacion,
                totalNoGravado = 0.0,
                totalPagar = totalPagar,
                totalLetras = totalLetras,
                totalIva = totalIva,
                saldoFavor = 0.0,
                condicionOperacion = 1,
                pagos = new[]
                {
                     new
                    {
                        codigo = "01",
                        montoPago = totalPagar,
                        referencia = "0000",
                        periodo = (string)null,
                        plazo = (string)null
                    }
                },
                    numPagoElectronico = "0"
                };

            // Crear la extensión
            var extension = new
            {
                nombEntrega = "ENCARGADO 1",
                docuEntrega = "00000000-0",
                nombRecibe = (string)null,
                docuRecibe = (string)null,
                observaciones = (string)null,
                placaVehiculo = (string)null
            };

            // Serializar el objeto JSON completo
            dteJson = JsonSerializer.Serialize(new
            {
                identificacion,
                documentoRelacionado = (string)null,
                emisor,
                receptor,
                ventaTercero = (string)null,
                cuerpoDocumento,
                resumen,
                extension,
                otrosDocumentos = (string)null,
                apendice = (string)null
            });


            //ENVIO A LA API
            var requestUnificado = new
            {
                Usuario = Emisor.NIT,
                Password = Emisor.ClaveApi,
                Ambiente = "00",
                DteJson = dteJson,
                Nit = Emisor.NIT,
                PasswordPrivado = Emisor.Clave,
                TipoDte = "01",
                CodigoGeneracion = codigoGeneracion,
                NumControl = numeroControl,
                VersionDte = 1
            };
            using (HttpClient client = new HttpClient())
            {
                // LLAMADA ÚNICA
                var response = client.PostAsJsonAsync("http://207.58.175.219:7122/api/procesar-dte", requestUnificado).Result;
                var responseData = response.Content.ReadAsStringAsync().Result;

                if (!response.IsSuccessStatusCode)
                    throw new Exception($"Error al procesar DTE: {responseData}");

                var resultado = JsonDocument.Parse(responseData).RootElement;
                var selloRecepcion = resultado.TryGetProperty("selloRecibido", out var sello)
                    ? sello.GetString()
                    : null;
            }






            return true;
        }

    }
}
