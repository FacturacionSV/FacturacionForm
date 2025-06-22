using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using FacturacionForm.BaseDeDatos;
using FacturacionForm.entidades;
using GeneradorDocumentos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static FacturacionForm.Controladores.ProcesarDTECF;

namespace FacturacionForm
{
    public partial class FConsulta : Form
    {
        public FConsulta()
        {
            InitializeComponent();
        }

        private void FConsulta_Load(object sender, EventArgs e)
        {
            ManejadorBD manejadorBD = new ManejadorBD();
            ventaDTOBindingSource.DataSource = manejadorBD.Todos();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            VentaDTO venta = (VentaDTO)ventaDTOBindingSource.Current;
            if (venta.TipoDTE == "01")
            {
                var generador = new GeneradorDocumentos.GeneradorPdfDte();

                try
                {
                    // Generar el PDF en memoria
                    byte[] pdfBytes = generador.GenerarPdfEnMemoria(venta.DocumentoJson, venta.SelloRecepcion);

                    // Opciones de uso:

                    // 1. Abrir el PDF en el navegador
                    generador.AbrirPdfEnNavegador(pdfBytes);

                    // 2. Guardar el PDF en un archivo
                    File.WriteAllBytes("DocumentoDTE.pdf", pdfBytes);

                    // 3. Si quieres enviarlo por correo electrónico
                    // EnviadorCorreos.EnviarFacturaElectronica(pdfBytes, venta.DocumentoJson, Receptor.Email);

                    Console.WriteLine("PDF generado exitosamente");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al generar el PDF: {ex.Message}");
                }
            }
            else
            {
                //PDF
                var generador = new GeneradorPdfDte03();

                try
                {
                    // Generar el PDF en memoria
                    byte[] pdfBytes = generador.GenerarPdfEnMemoria(venta.DocumentoJson, venta.SelloRecepcion);

                    // Opciones de uso:

                    // 1. Abrir el PDF en el navegador
                    generador.AbrirPdfEnNavegador(pdfBytes);

                    // 2. Guardar el PDF en un archivo
                    File.WriteAllBytes("DocumentoDTE.pdf", pdfBytes);

                    // 3. Si quieres enviarlo por correo electrónico
                    //EnviadorCorreos.EnviarFacturaElectronica(pdfBytes, venta.DocumentoJson, Receptor.Email);

                    Console.WriteLine("PDF generado exitosamente");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al generar el PDF: {ex.Message}");
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            VentaDTO venta = (VentaDTO)ventaDTOBindingSource.Current;
            var jsonObj = JObject.Parse(venta.DocumentoJson); // OBTENER EL JSON ORIGINAL PARA LEERLO EN CADA SECCION

            // Sección de Identificación
            var identificacion = new
            {
                version = 2,
                ambiente = jsonObj["identificacion"]["ambiente"].ToString(),
                codigoGeneracion = Guid.NewGuid().ToString().ToUpper(),
                fecAnula = DateTime.Now.ToString("yyyy-MM-dd"),
                horAnula = DateTime.Now.ToString("HH:mm:ss")
            };

            // Sección de Emisor
            var emisorJson = jsonObj["emisor"];
            string procesarSiNulo(JToken token) => token?.Type == JTokenType.Null ? null : token?.ToString();

            var emisor = new
            {
                nit = emisorJson["nit"].ToString(),
                nombre = emisorJson["nombre"].ToString(),
                tipoEstablecimiento = emisorJson["tipoEstablecimiento"].ToString(),
                nomEstablecimiento = emisorJson["nombreComercial"].ToString(),
                codEstableMH = procesarSiNulo(emisorJson["codEstableMH"]),
                codEstable = procesarSiNulo(emisorJson["codEstable"]),
                codPuntoVentaMH = procesarSiNulo(emisorJson["codPuntoVentaMH"]),
                codPuntoVenta = procesarSiNulo(emisorJson["codPuntoVenta"]),
                telefono = emisorJson["telefono"].ToString(),
                correo = emisorJson["correo"].ToString()
            };
            string tipoDocumento = (string)null; // asumiendo que es otro documento
            string numDocumento = string.IsNullOrEmpty(jsonObj["receptor"]["nit"]?.ToString()) ? null : jsonObj["receptor"]["nit"].ToString();
            if (venta.TipoDTE == "01")
            { // en consumidor final
                tipoDocumento = string.IsNullOrEmpty(jsonObj["receptor"]["tipoDocumento"]?.ToString()) ? null : jsonObj["receptor"]["tipoDocumento"].ToString();
                numDocumento = string.IsNullOrEmpty(jsonObj["receptor"]["numDocumento"]?.ToString()) ? null : jsonObj["receptor"]["numDocumento"].ToString();
            }
            else
            {
                //en credito fiscal puede usar el nit o el dui homologado
                numDocumento = string.IsNullOrEmpty(jsonObj["receptor"]["nit"]?.ToString()) ? null : jsonObj["receptor"]["nit"].ToString();

                if (numDocumento.Length == 14)
                {
                    tipoDocumento = "36";
                }
                if (numDocumento.Length == 9)
                {
                    tipoDocumento = "13";
                }

            }
            // Sección de Documento
            var documento = new
            {
                tipoDte = jsonObj["identificacion"]["tipoDte"].ToString(),
                codigoGeneracion = jsonObj["identificacion"]["codigoGeneracion"].ToString(),
                selloRecibido = venta.SelloRecepcion,
                numeroControl = jsonObj["identificacion"]["numeroControl"].ToString(),
                fecEmi = jsonObj["identificacion"]["fecEmi"].ToString(),
                montoIva = (string)null,
                codigoGeneracionR = (string)null,
                tipoDocumento = tipoDocumento,
                numDocumento = numDocumento,
                nombre = jsonObj["receptor"]["nombre"].ToString(),
                telefono = jsonObj["receptor"]["telefono"].ToString(),
                correo = "reclamaciones@dteelsalvador.info"
            };

            // Sección de Motivo
            var motivo = new
            {
                tipoAnulacion = 2,
                motivoAnulacion = "Anulación solicitada por el cliente",
                nombreResponsable = "CAJERO",
                tipDocResponsable = "37",
                numDocResponsable = "200001",
                nombreSolicita = "Cliente",
                tipDocSolicita = "37",
                numDocSolicita = "00001"
            };

            // Construcción del JSON final
            // Versión simplificada usando Newtonsoft.Json
            var jsonAnulacion = new JObject
            {
                { "identificacion", JObject.Parse(JsonConvert.SerializeObject(identificacion)) },
                { "emisor", JObject.Parse(JsonConvert.SerializeObject(emisor)) },
                { "documento", JObject.Parse(JsonConvert.SerializeObject(documento)) },
                { "motivo", JObject.Parse(JsonConvert.SerializeObject(motivo)) }
            };
            string jsonFormateado = jsonAnulacion.ToString(Newtonsoft.Json.Formatting.None);



            try
            {
                using (HttpClient client = new HttpClient())
                {



                    var requestAnulacion = new
                    {
                        Usuario = jsonObj["emisor"]["nit"].ToString(),
                        Password = new Emisor().ClaveApi,
                        Ambiente = jsonObj["identificacion"]["ambiente"].ToString(),
                        DteJson = jsonFormateado,  // <- Este ya tiene la estructura correcta de anulación
                        Nit = jsonObj["emisor"]["nit"].ToString(),
                        PasswordPrivado = new Emisor().Clave,
                        // (Omitir TipoDte, CodigoGeneracion, etc., porque ya están dentro del DteJson)
                    };

                    //  var json2 = JsonConvert.SerializeObject(requestUnificado);
                    //  Console.WriteLine(json2); // Verifica que el JSON esté bien

                    // LLAMADA ÚNICA a la API
                    var response = client.PostAsJsonAsync("http://207.58.175.219:9200/api/anular-dte", requestAnulacion).Result;
                    var responseData = response.Content.ReadAsStringAsync().Result;

                    //*****************

                    if (!response.IsSuccessStatusCode)
                        throw new Exception($"Error al procesar DTE : {responseData}");

                    var resultado2 = JsonDocument.Parse(responseData).RootElement;
                    string selloRecibido = resultado2.TryGetProperty("selloRecibido", out var sello2)
                        ? sello2.GetString()
                        : null;

                    // Actualizar los datos del proceso de anulacion
                    ManejadorBD manejadorBD = new ManejadorBD();
                    manejadorBD.ActualizarAnulacion((int)venta.Id, selloRecibido, jsonFormateado);

                    MessageBox.Show("Factura anulada correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);


                }
            }
            catch (Exception)
            {


            }


        }
    }
}
