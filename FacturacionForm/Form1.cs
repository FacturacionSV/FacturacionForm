using FacturacionForm.Controladores;
using FacturacionForm.entidades;

namespace FacturacionForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //ESTILOS
            // Estilos de encabezado
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkBlue;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 14);
            // Alternar color de filas
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

            // Estilos de selección
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkOrange;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;

            // Bordes y estilo de celdas
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.GridColor = Color.Gray;


            // Configurar tipos de columnas
            dataGridView1.Columns["cantidad"].ValueType = typeof(int);
            dataGridView1.Columns["descripcion"].ValueType = typeof(string);
            dataGridView1.Columns["precio"].ValueType = typeof(decimal);
            dataGridView1.Columns["total"].ValueType = typeof(decimal);

            // Deshabilitar edición en la columna Total
            dataGridView1.Columns["total"].ReadOnly = true;

        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1 == null || dataGridView1.Columns.Count == 0)
            {
                return;
            }
            if (e.RowIndex < 0 || e.RowIndex >= dataGridView1.Rows.Count)
            {
                return; // No hay una fila válida, salir sin hacer nada
            }

            // Verificar que la celda modificada sea "cantidad" o "precio"
            if (e.ColumnIndex == dataGridView1.Columns["cantidad"].Index ||
                e.ColumnIndex == dataGridView1.Columns["precio"].Index)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Validar que las celdas no sean nulas antes de procesar
                if (row.Cells["cantidad"].Value != null && row.Cells["precio"].Value != null)
                {
                    int cantidad;
                    decimal precio;

                    // Intentar convertir los valores correctamente
                    if (int.TryParse(row.Cells["cantidad"].Value.ToString(), out cantidad) &&
                        decimal.TryParse(row.Cells["precio"].Value.ToString(), out precio))
                    {
                        row.Cells["total"].Value = cantidad * precio;
                    }
                }
            }

            ActualizarTotal();


        }

        private void ActualizarTotal()
        {
            try
            {
                decimal totalGeneral = 0;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["total"].Value != null)
                    {
                        decimal totalFila;

                        if (decimal.TryParse(row.Cells["total"].Value.ToString(), out totalFila))
                        {
                            totalGeneral += totalFila;
                        }
                    }
                }

                textBoxSumas.Text = totalGeneral.ToString();
                textBoxTotal.Text = totalGeneral.ToString();
                textBoxIva.Text = 0.ToString();
                if (radioButtonCFF.Checked)
                {
                    decimal iva = Math.Round((totalGeneral/1.13m) * 0.13m,2);
                    textBoxSumas.Text = Math.Round((totalGeneral/1.13m),2).ToString();
                    textBoxIva.Text = iva.ToString();
                    textBoxTotal.Text = (totalGeneral).ToString();
                }


            }
            catch (Exception)
            {
                decimal totalGeneral = 0;
                textBoxSumas.Text = totalGeneral.ToString();
                textBoxTotal.Text = totalGeneral.ToString();
                textBoxIva.Text = totalGeneral.ToString();
            }
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

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow) // Evitar la última fila vacía
                    {
                        int cantidad = Convert.ToInt32(row.Cells["cantidad"].Value ?? 0);
                        string descripcion = row.Cells["descripcion"].Value?.ToString() ?? "";
                        decimal precio = Convert.ToDecimal(row.Cells["precio"].Value ?? 0);

                        // Crear objeto DetalleVenta y agregarlo a la lista
                        listaDetallesVenta.Add(new DetalleVenta(cantidad, descripcion, precio));
                    }
                }

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
                receptor.NIT = textBoxNitReceptor.Text.Trim().Replace("-","");
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
    }
}
