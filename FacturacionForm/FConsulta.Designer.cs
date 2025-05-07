namespace FacturacionForm
{
    partial class FConsulta
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            dataGridView1 = new DataGridView();
            idDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            fechaDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            tipoDTEDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            documentoJsonDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            numeroControlDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            codigoGeneracionDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            selloRecepcionDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            ventaDTOBindingSource = new BindingSource(components);
            button1 = new Button();
            button2 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ventaDTOBindingSource).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { idDataGridViewTextBoxColumn, fechaDataGridViewTextBoxColumn, tipoDTEDataGridViewTextBoxColumn, documentoJsonDataGridViewTextBoxColumn, numeroControlDataGridViewTextBoxColumn, codigoGeneracionDataGridViewTextBoxColumn, selloRecepcionDataGridViewTextBoxColumn });
            dataGridView1.DataSource = ventaDTOBindingSource;
            dataGridView1.Location = new Point(12, 33);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.Size = new Size(860, 394);
            dataGridView1.TabIndex = 0;
            // 
            // idDataGridViewTextBoxColumn
            // 
            idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            idDataGridViewTextBoxColumn.HeaderText = "Id";
            idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            idDataGridViewTextBoxColumn.ReadOnly = true;
            idDataGridViewTextBoxColumn.Visible = false;
            // 
            // fechaDataGridViewTextBoxColumn
            // 
            fechaDataGridViewTextBoxColumn.DataPropertyName = "Fecha";
            fechaDataGridViewTextBoxColumn.HeaderText = "Fecha";
            fechaDataGridViewTextBoxColumn.Name = "fechaDataGridViewTextBoxColumn";
            fechaDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tipoDTEDataGridViewTextBoxColumn
            // 
            tipoDTEDataGridViewTextBoxColumn.DataPropertyName = "TipoDTE";
            tipoDTEDataGridViewTextBoxColumn.HeaderText = "TipoDTE";
            tipoDTEDataGridViewTextBoxColumn.Name = "tipoDTEDataGridViewTextBoxColumn";
            tipoDTEDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // documentoJsonDataGridViewTextBoxColumn
            // 
            documentoJsonDataGridViewTextBoxColumn.DataPropertyName = "DocumentoJson";
            documentoJsonDataGridViewTextBoxColumn.HeaderText = "DocumentoJson";
            documentoJsonDataGridViewTextBoxColumn.Name = "documentoJsonDataGridViewTextBoxColumn";
            documentoJsonDataGridViewTextBoxColumn.ReadOnly = true;
            documentoJsonDataGridViewTextBoxColumn.Visible = false;
            // 
            // numeroControlDataGridViewTextBoxColumn
            // 
            numeroControlDataGridViewTextBoxColumn.DataPropertyName = "NumeroControl";
            numeroControlDataGridViewTextBoxColumn.HeaderText = "NumeroControl";
            numeroControlDataGridViewTextBoxColumn.Name = "numeroControlDataGridViewTextBoxColumn";
            numeroControlDataGridViewTextBoxColumn.ReadOnly = true;
            numeroControlDataGridViewTextBoxColumn.Width = 200;
            // 
            // codigoGeneracionDataGridViewTextBoxColumn
            // 
            codigoGeneracionDataGridViewTextBoxColumn.DataPropertyName = "CodigoGeneracion";
            codigoGeneracionDataGridViewTextBoxColumn.HeaderText = "CodigoGeneracion";
            codigoGeneracionDataGridViewTextBoxColumn.Name = "codigoGeneracionDataGridViewTextBoxColumn";
            codigoGeneracionDataGridViewTextBoxColumn.ReadOnly = true;
            codigoGeneracionDataGridViewTextBoxColumn.Width = 200;
            // 
            // selloRecepcionDataGridViewTextBoxColumn
            // 
            selloRecepcionDataGridViewTextBoxColumn.DataPropertyName = "SelloRecepcion";
            selloRecepcionDataGridViewTextBoxColumn.HeaderText = "SelloRecepcion";
            selloRecepcionDataGridViewTextBoxColumn.Name = "selloRecepcionDataGridViewTextBoxColumn";
            selloRecepcionDataGridViewTextBoxColumn.ReadOnly = true;
            selloRecepcionDataGridViewTextBoxColumn.Width = 200;
            // 
            // ventaDTOBindingSource
            // 
            ventaDTOBindingSource.DataSource = typeof(BaseDeDatos.VentaDTO);
            // 
            // button1
            // 
            button1.Location = new Point(888, 37);
            button1.Name = "button1";
            button1.Size = new Size(129, 23);
            button1.TabIndex = 1;
            button1.Text = "ANULAR";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(888, 80);
            button2.Name = "button2";
            button2.Size = new Size(129, 23);
            button2.TabIndex = 1;
            button2.Text = "VER PDF";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // FConsulta
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1045, 450);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(dataGridView1);
            Name = "FConsulta";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FConsulta";
            Load += FConsulta_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)ventaDTOBindingSource).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private BindingSource ventaDTOBindingSource;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn fechaDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn tipoDTEDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn documentoJsonDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn numeroControlDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn codigoGeneracionDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn selloRecepcionDataGridViewTextBoxColumn;
        private Button button1;
        private Button button2;
    }
}