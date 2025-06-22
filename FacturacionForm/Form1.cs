using FacturacionForm.Controladores;
using FacturacionForm.entidades;

namespace FacturacionForm
{
    public partial class Form1 : Form
    {
        int cantidad1 = 0, cantidad2 = 0, cantidad3 = 0, cantidad4 = 0;
        string descripcion1 = "", descripcion2 = "", descripcion3 = "", descripcion4 = "";
        decimal precio1 = 0, precio2 = 0, precio3 = 0, precio4 = 0;

        decimal totales = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Emisor emisor = new Emisor();
            textBoxDireccionEmisor.Text = emisor.Direccion;
            textBoxNombreEmisor.Text = emisor.NombreComercial;
            textBoxNRCEmisor.Text = emisor.NRC;
            textBoxNitEmisor.Text = emisor.NIT;
            textBoxEmailEmisor.Text = emisor.Email;
            textBoxTelefonoEmisor.Text = emisor.Telefono;


            //ESTILOS
            // Estilos de encabezado
            //dataGridView1.EnableHeadersVisualStyles = false;
            //dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkBlue;
            //dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
            //dataGridView1.DefaultCellStyle.Font = new Font("Arial", 14);
            //// Alternar color de filas
            //dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

            //// Estilos de selección
            //dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkOrange;
            //dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;

            //// Bordes y estilo de celdas
            //dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            //dataGridView1.GridColor = Color.Gray;


            //// Configurar tipos de columnas
            //dataGridView1.Columns["cantidad"].ValueType = typeof(int);
            //dataGridView1.Columns["descripcion"].ValueType = typeof(string);
            //dataGridView1.Columns["precio"].ValueType = typeof(decimal);
            //dataGridView1.Columns["total"].ValueType = typeof(decimal);

            //// Deshabilitar edición en la columna Total
            //dataGridView1.Columns["total"].ReadOnly = true;

        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //if (dataGridView1 == null || dataGridView1.Columns.Count == 0)
            //{
            //    return;
            //}
            //if (e.RowIndex < 0 || e.RowIndex >= dataGridView1.Rows.Count)
            //{
            //    return; // No hay una fila válida, salir sin hacer nada
            //}

            //// Verificar que la celda modificada sea "cantidad" o "precio"
            //if (e.ColumnIndex == dataGridView1.Columns["cantidad"].Index ||
            //    e.ColumnIndex == dataGridView1.Columns["precio"].Index)
            //{
            //    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

            //    // Validar que las celdas no sean nulas antes de procesar
            //    if (row.Cells["cantidad"].Value != null && row.Cells["precio"].Value != null)
            //    {
            //        int cantidad;
            //        decimal precio;

            //        // Intentar convertir los valores correctamente
            //        if (int.TryParse(row.Cells["cantidad"].Value.ToString(), out cantidad) &&
            //            decimal.TryParse(row.Cells["precio"].Value.ToString(), out precio))
            //        {
            //            row.Cells["total"].Value = cantidad * precio;
            //        }
            //    }
            //}

            //ActualizarTotal();


        }

        private void ActualizarTotal()
        {
            //try
            //{
            //    decimal totalGeneral = 0;

            //    foreach (DataGridViewRow row in dataGridView1.Rows)
            //    {
            //        if (row.Cells["total"].Value != null)
            //        {
            //            decimal totalFila;

            //            if (decimal.TryParse(row.Cells["total"].Value.ToString(), out totalFila))
            //            {
            //                totalGeneral += totalFila;
            //            }
            //        }
            //    }

            //    textBoxSumas.Text = totalGeneral.ToString();
            //    textBoxTotal.Text = totalGeneral.ToString();
            //    textBoxIva.Text = 0.ToString();
            //    if (radioButtonCFF.Checked)
            //    {
            //        decimal iva = Math.Round((totalGeneral / 1.13m) * 0.13m, 2);
            //        textBoxSumas.Text = Math.Round((totalGeneral / 1.13m), 2).ToString();
            //        textBoxIva.Text = iva.ToString();
            //        textBoxTotal.Text = (totalGeneral).ToString();
            //    }


            //}
            //catch (Exception)
            //{
            //    decimal totalGeneral = 0;
            //    textBoxSumas.Text = totalGeneral.ToString();
            //    textBoxTotal.Text = totalGeneral.ToString();
            //    textBoxIva.Text = totalGeneral.ToString();
            //}
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            ActualizarTotal();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button1.Text = "Procesando";

            try
            {

                if (textBoxEmailReceptor.Text.Length == 0)
                {
                    MessageBox.Show("Escriba el EMAIL del receptor");
                    textBoxEmailReceptor.Focus();
                    return;
                }
                //EXTRAER LOS DATOS 
                List<DetalleVenta> listaDetallesVenta = new List<DetalleVenta>();

                
                        if (cantidad1>0)
                            listaDetallesVenta.Add(new DetalleVenta(cantidad1, descripcion1, precio1));
                        if (cantidad2 > 0)
                            listaDetallesVenta.Add(new DetalleVenta(cantidad2, descripcion3, precio2));
                        if (cantidad3 > 0)
                            listaDetallesVenta.Add(new DetalleVenta(cantidad3, descripcion3, precio3));
                        if (cantidad4 > 0)
                            listaDetallesVenta.Add(new DetalleVenta(cantidad4, descripcion4, precio4));



                //EMISOR
                Emisor emisor = new Emisor();
                emisor.NombreComercial = textBoxNombreEmisor.Text;
                emisor.Telefono = textBoxTelefonoEmisor.Text;
                emisor.Email = textBoxEmailEmisor.Text;
                emisor.Direccion = textBoxDireccionEmisor.Text;
                emisor.NIT = textBoxNitEmisor.Text;
                emisor.NRC = textBoxNRCEmisor.Text;

                //Receptor
                Receptor receptor = new Receptor();
                receptor.NombreComercial = textBoxNombreReceptor.Text;
                receptor.Telefono = textBox1TelReceptor.Text;
                receptor.Email = textBoxEmailReceptor.Text;
                receptor.Direccion = textBoxDireccionReceptor.Text;
                receptor.NIT = textBoxNitReceptor.Text.Trim().Replace("-", "");
                receptor.NRC = textBoxNRCReceptor.Text.Trim().Replace("-", "");
                receptor.CodigoActividadEconomica = textBoxCodActividad.Text;


                if (radioButtonCF.Checked)
                {

                    if (receptor == null ||
        string.IsNullOrEmpty(receptor.NombreComercial) ||
        string.IsNullOrEmpty(receptor.Telefono) ||
        string.IsNullOrEmpty(receptor.Email) ||
        string.IsNullOrEmpty(receptor.Direccion))
                    {
                        MessageBox.Show("Faltan datos requeridos del receptor para CF");
                        return;
                    }



                    ProcesarDTECF procesarDTECF = new ProcesarDTECF(listaDetallesVenta, emisor, receptor);
                    procesarDTECF.EnviarDTE();
                }
                else
                {


                    if (receptor == null ||
                        string.IsNullOrEmpty(receptor.NombreComercial) ||
                        string.IsNullOrEmpty(receptor.Telefono) ||
                        string.IsNullOrEmpty(receptor.Email) ||
                        string.IsNullOrEmpty(receptor.Direccion) ||
                        string.IsNullOrEmpty(receptor.NRC) ||
                        string.IsNullOrEmpty(receptor.CodigoActividadEconomica) ||
                        string.IsNullOrEmpty(receptor.NIT))
                    {
                        MessageBox.Show("Faltan datos requeridos del receptor para CCF");
                        return;
                    }
                    ProcesarDTECCF procesarDTECCF = new ProcesarDTECCF(listaDetallesVenta, emisor, receptor);
                    procesarDTECCF.EnviarDTE();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }
            finally
            {

                button1.Enabled = true;
                button1.Text = "FACTURAR";
            }

        }

        private void radioButtonCFF_CheckedChanged(object sender, EventArgs e)
        {
            ActualizarTotal();
        }

        private void radioButtonCF_CheckedChanged(object sender, EventArgs e)
        {
            ActualizarTotal();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FConsulta fConsulta = new FConsulta();
            fConsulta.Show();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            

            if (textBoxCantidad1.Text.Length>0)
            {
                if (textBoxDescripcion1.Text.Length<=0 || textBoxPrecio1.Text.Length<=0)
                {
                    MessageBox.Show("Faltan datos en el producto 1");
                    textBoxDescripcion1.Focus();
                    return;
                }
            }
            // Para textBoxCantidad2
            if (textBoxCantidad2.Text.Length > 0)
            {
                if (textBoxDescripcion2.Text.Length <= 0 || textBoxPrecio2.Text.Length <= 0)
                {
                    MessageBox.Show("Faltan datos en el producto 2");
                    textBoxDescripcion2.Focus();
                    return;
                }
            }

            // Para textBoxCantidad3
            if (textBoxCantidad3.Text.Length > 0)
            {
                if (textBoxDescripcion3.Text.Length <= 0 || textBoxPrecio3.Text.Length <= 0)
                {
                    MessageBox.Show("Faltan datos en el producto 3");
                    textBoxDescripcion3.Focus();
                    return;
                }
            }

            // Para textBoxCantidad4
            if (textBoxCantidad4.Text.Length > 0)
            {
                if (textBoxDescripcion4.Text.Length <= 0 || textBoxPrecio4.Text.Length <= 0)
                {
                    MessageBox.Show("Faltan datos en el producto 4");
                    textBoxDescripcion4.Focus();
                    return;
                }
            }



            int.TryParse(textBoxCantidad1.Text, out cantidad1);
            int.TryParse(textBoxCantidad2.Text, out cantidad2);
            int.TryParse(textBoxCantidad3.Text, out cantidad3);
            int.TryParse(textBoxCantidad4.Text, out cantidad4);

            descripcion1 = textBoxDescripcion1.Text;
            descripcion2 = textBoxDescripcion2.Text;
            descripcion3 = textBoxDescripcion3.Text;
            descripcion4 = textBoxDescripcion4.Text;

            decimal.TryParse(textBoxPrecio1.Text, out precio1);
            decimal.TryParse(textBoxPrecio2.Text, out precio2);
            decimal.TryParse(textBoxPrecio3.Text, out precio3);
            decimal.TryParse(textBoxPrecio4.Text, out precio4);


            if (radioButtonCF.Checked)
            {
                totales=precio1+ precio2+precio3+precio4;
                textBoxIva.Text = "";
                textBoxSumas.Text = totales.ToString();
                textBoxTotal.Text = totales.ToString();
            }

            if (radioButtonCFF.Checked)
            {
                totales = precio1 + precio2 + precio3 + precio4;
                decimal iva = totales * 0.13m;
                textBoxSumas.Text = totales.ToString();
                textBoxIva.Text = iva.ToString();
                totales += iva;
                textBoxTotal.Text = totales.ToString();
            }



        }

        private void solonumeros(object sender, KeyPressEventArgs e)
        {
            // Permitir números, punto decimal y tecla de retroceso
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '\b')
            {
                e.Handled = true;
            }

            // Permitir solo un punto decimal
            if (e.KeyChar == '.' && ((TextBox)sender).Text.Contains('.'))
            {
                e.Handled = true;
            }
        }
    }
}
