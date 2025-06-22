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

namespace FacturacionForm
{
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string correlativoCF = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el correlativo para CF:", "Correlativo CF", "");

            if (!string.IsNullOrEmpty(correlativoCF))
            {
                // Aquí ya tienes el correlativo guardado para enviarlo
                ManejadorBD manejador = new ManejadorBD();
                manejador.setCorrelativo("CF", int.Parse(correlativoCF));
                MessageBox.Show("Correlativo guardado: " + correlativoCF);

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string correlativoCF = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el correlativo para CCF:", "Correlativo CCF", "");

            if (!string.IsNullOrEmpty(correlativoCF))
            {
                // Aquí ya tienes el correlativo guardado para enviarlo
                ManejadorBD manejador = new ManejadorBD();
                manejador.setCorrelativo("CFF", int.Parse(correlativoCF));
                MessageBox.Show("Correlativo guardado: " + correlativoCF);

            }
        }
    }
}
