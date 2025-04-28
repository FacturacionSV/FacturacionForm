using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FacturacionForm.BaseDeDatos;
using FacturacionForm.entidades;
using GeneradorDocumentos;
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
            if (venta.TipoDTE=="01")
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
    }
}
