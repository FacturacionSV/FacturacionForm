namespace FacturacionForm
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            groupBox1 = new GroupBox();
            textBoxEmailEmisor = new TextBox();
            label11 = new Label();
            textBoxTelefonoEmisor = new TextBox();
            label10 = new Label();
            textBoxNRCEmisor = new TextBox();
            label8 = new Label();
            textBoxNitEmisor = new TextBox();
            label7 = new Label();
            textBoxDireccionEmisor = new TextBox();
            label9 = new Label();
            textBoxNombreEmisor = new TextBox();
            label6 = new Label();
            dataGridView1 = new DataGridView();
            cantidad = new DataGridViewTextBoxColumn();
            descripcion = new DataGridViewTextBoxColumn();
            precio = new DataGridViewTextBoxColumn();
            total = new DataGridViewTextBoxColumn();
            label1 = new Label();
            label2 = new Label();
            textBoxSumas = new TextBox();
            label3 = new Label();
            textBoxRetencion = new TextBox();
            label4 = new Label();
            textBoxIva = new TextBox();
            label5 = new Label();
            textBoxTotal = new TextBox();
            button1 = new Button();
            groupBox2 = new GroupBox();
            textBoxEmailReceptor = new TextBox();
            label12 = new Label();
            textBox1TelReceptor = new TextBox();
            label13 = new Label();
            textBoxNRCReceptor = new TextBox();
            label14 = new Label();
            textBoxNitReceptor = new TextBox();
            label15 = new Label();
            textBoxDireccionReceptor = new TextBox();
            label16 = new Label();
            textBoxNombreReceptor = new TextBox();
            label17 = new Label();
            radioButtonCF = new RadioButton();
            radioButtonCFF = new RadioButton();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(textBoxEmailEmisor);
            groupBox1.Controls.Add(label11);
            groupBox1.Controls.Add(textBoxTelefonoEmisor);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(textBoxNRCEmisor);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(textBoxNitEmisor);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(textBoxDireccionEmisor);
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(textBoxNombreEmisor);
            groupBox1.Controls.Add(label6);
            groupBox1.FlatStyle = FlatStyle.Flat;
            groupBox1.Font = new Font("Segoe UI", 9.75F);
            groupBox1.Location = new Point(31, 18);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(579, 165);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Datos del emisor";
            // 
            // textBoxEmailEmisor
            // 
            textBoxEmailEmisor.Location = new Point(367, 84);
            textBoxEmailEmisor.Name = "textBoxEmailEmisor";
            textBoxEmailEmisor.Size = new Size(203, 25);
            textBoxEmailEmisor.TabIndex = 9;
            textBoxEmailEmisor.Text = "demo@gmail.com";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(366, 67);
            label11.Name = "label11";
            label11.Size = new Size(44, 17);
            label11.TabIndex = 8;
            label11.Text = "EMAIL";
            // 
            // textBoxTelefonoEmisor
            // 
            textBoxTelefonoEmisor.Location = new Point(367, 39);
            textBoxTelefonoEmisor.Name = "textBoxTelefonoEmisor";
            textBoxTelefonoEmisor.Size = new Size(203, 25);
            textBoxTelefonoEmisor.TabIndex = 3;
            textBoxTelefonoEmisor.Text = "76230990";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(366, 22);
            label10.Name = "label10";
            label10.Size = new Size(31, 17);
            label10.TabIndex = 2;
            label10.Text = "TEL.";
            // 
            // textBoxNRCEmisor
            // 
            textBoxNRCEmisor.Location = new Point(179, 84);
            textBoxNRCEmisor.Name = "textBoxNRCEmisor";
            textBoxNRCEmisor.Size = new Size(129, 25);
            textBoxNRCEmisor.TabIndex = 7;
            textBoxNRCEmisor.Text = "3477200";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(178, 67);
            label8.Name = "label8";
            label8.Size = new Size(34, 17);
            label8.TabIndex = 6;
            label8.Text = "NRC";
            // 
            // textBoxNitEmisor
            // 
            textBoxNitEmisor.Location = new Point(6, 84);
            textBoxNitEmisor.Name = "textBoxNitEmisor";
            textBoxNitEmisor.Size = new Size(167, 25);
            textBoxNitEmisor.TabIndex = 5;
            textBoxNitEmisor.Text = "04352208241018";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(5, 67);
            label7.Name = "label7";
            label7.Size = new Size(28, 17);
            label7.TabIndex = 4;
            label7.Text = "NIT";
            // 
            // textBoxDireccionEmisor
            // 
            textBoxDireccionEmisor.Location = new Point(6, 128);
            textBoxDireccionEmisor.Name = "textBoxDireccionEmisor";
            textBoxDireccionEmisor.Size = new Size(351, 25);
            textBoxDireccionEmisor.TabIndex = 11;
            textBoxDireccionEmisor.Text = "Chalatenango";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(5, 111);
            label9.Name = "label9";
            label9.Size = new Size(74, 17);
            label9.TabIndex = 10;
            label9.Text = "DIRECCION";
            // 
            // textBoxNombreEmisor
            // 
            textBoxNombreEmisor.Location = new Point(6, 39);
            textBoxNombreEmisor.Name = "textBoxNombreEmisor";
            textBoxNombreEmisor.Size = new Size(351, 25);
            textBoxNombreEmisor.TabIndex = 1;
            textBoxNombreEmisor.Text = "KEYJOTECH DEVELOPMENT, SOCIEDAD POR ACCIONES SIMPLIFICADA DE CAPITAL VARIABLE";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(5, 22);
            label6.Name = "label6";
            label6.Size = new Size(117, 17);
            label6.TabIndex = 0;
            label6.Text = "Nombre comercial";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { cantidad, descripcion, precio, total });
            dataGridView1.Location = new Point(31, 214);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(913, 304);
            dataGridView1.TabIndex = 2;
            dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
            dataGridView1.RowsRemoved += dataGridView1_RowsRemoved;
            // 
            // cantidad
            // 
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = null;
            cantidad.DefaultCellStyle = dataGridViewCellStyle1;
            cantidad.HeaderText = "CANTIDAD";
            cantidad.Name = "cantidad";
            // 
            // descripcion
            // 
            descripcion.HeaderText = "DESCRIPCION";
            descripcion.Name = "descripcion";
            descripcion.Width = 300;
            // 
            // precio
            // 
            dataGridViewCellStyle2.Format = "C2";
            dataGridViewCellStyle2.NullValue = null;
            precio.DefaultCellStyle = dataGridViewCellStyle2;
            precio.HeaderText = "PRECIO UNITARIO";
            precio.Name = "precio";
            precio.Width = 200;
            // 
            // total
            // 
            dataGridViewCellStyle3.Format = "C2";
            dataGridViewCellStyle3.NullValue = null;
            total.DefaultCellStyle = dataGridViewCellStyle3;
            total.HeaderText = "TOTAL";
            total.Name = "total";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(1031, 192);
            label1.Name = "label1";
            label1.Size = new Size(105, 32);
            label1.TabIndex = 3;
            label1.Text = "TOTALES";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(950, 248);
            label2.Name = "label2";
            label2.Size = new Size(58, 20);
            label2.TabIndex = 4;
            label2.Text = "SUMAS";
            // 
            // textBoxSumas
            // 
            textBoxSumas.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            textBoxSumas.Location = new Point(1044, 245);
            textBoxSumas.Name = "textBoxSumas";
            textBoxSumas.Size = new Size(195, 29);
            textBoxSumas.TabIndex = 5;
            textBoxSumas.TextAlign = HorizontalAlignment.Right;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(950, 281);
            label3.Name = "label3";
            label3.Size = new Size(88, 20);
            label3.TabIndex = 6;
            label3.Text = "RETENCION";
            // 
            // textBoxRetencion
            // 
            textBoxRetencion.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            textBoxRetencion.Location = new Point(1044, 278);
            textBoxRetencion.Name = "textBoxRetencion";
            textBoxRetencion.Size = new Size(195, 29);
            textBoxRetencion.TabIndex = 7;
            textBoxRetencion.TextAlign = HorizontalAlignment.Right;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(950, 314);
            label4.Name = "label4";
            label4.Size = new Size(31, 20);
            label4.TabIndex = 8;
            label4.Text = "IVA";
            // 
            // textBoxIva
            // 
            textBoxIva.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            textBoxIva.Location = new Point(1044, 311);
            textBoxIva.Name = "textBoxIva";
            textBoxIva.Size = new Size(195, 29);
            textBoxIva.TabIndex = 9;
            textBoxIva.TextAlign = HorizontalAlignment.Right;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(950, 347);
            label5.Name = "label5";
            label5.Size = new Size(50, 20);
            label5.TabIndex = 10;
            label5.Text = "TOTAL";
            // 
            // textBoxTotal
            // 
            textBoxTotal.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            textBoxTotal.Location = new Point(1044, 344);
            textBoxTotal.Name = "textBoxTotal";
            textBoxTotal.Size = new Size(195, 29);
            textBoxTotal.TabIndex = 11;
            textBoxTotal.TextAlign = HorizontalAlignment.Right;
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 26.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.Location = new Point(956, 388);
            button1.Name = "button1";
            button1.Size = new Size(283, 130);
            button1.TabIndex = 12;
            button1.Text = "FACTURAR";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(textBoxEmailReceptor);
            groupBox2.Controls.Add(label12);
            groupBox2.Controls.Add(textBox1TelReceptor);
            groupBox2.Controls.Add(label13);
            groupBox2.Controls.Add(textBoxNRCReceptor);
            groupBox2.Controls.Add(label14);
            groupBox2.Controls.Add(textBoxNitReceptor);
            groupBox2.Controls.Add(label15);
            groupBox2.Controls.Add(textBoxDireccionReceptor);
            groupBox2.Controls.Add(label16);
            groupBox2.Controls.Add(textBoxNombreReceptor);
            groupBox2.Controls.Add(label17);
            groupBox2.FlatStyle = FlatStyle.Flat;
            groupBox2.Font = new Font("Segoe UI", 9.75F);
            groupBox2.Location = new Point(659, 19);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(580, 165);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Datos del receptor";
            // 
            // textBoxEmailReceptor
            // 
            textBoxEmailReceptor.Location = new Point(366, 84);
            textBoxEmailReceptor.Name = "textBoxEmailReceptor";
            textBoxEmailReceptor.Size = new Size(203, 25);
            textBoxEmailReceptor.TabIndex = 9;
            textBoxEmailReceptor.Text = "cliente@gmail.com";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(365, 67);
            label12.Name = "label12";
            label12.Size = new Size(44, 17);
            label12.TabIndex = 8;
            label12.Text = "EMAIL";
            // 
            // textBox1TelReceptor
            // 
            textBox1TelReceptor.Location = new Point(366, 39);
            textBox1TelReceptor.Name = "textBox1TelReceptor";
            textBox1TelReceptor.Size = new Size(203, 25);
            textBox1TelReceptor.TabIndex = 3;
            textBox1TelReceptor.Text = "78451269";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(365, 22);
            label13.Name = "label13";
            label13.Size = new Size(31, 17);
            label13.TabIndex = 2;
            label13.Text = "TEL.";
            // 
            // textBoxNRCReceptor
            // 
            textBoxNRCReceptor.Location = new Point(179, 84);
            textBoxNRCReceptor.Name = "textBoxNRCReceptor";
            textBoxNRCReceptor.Size = new Size(129, 25);
            textBoxNRCReceptor.TabIndex = 7;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(178, 67);
            label14.Name = "label14";
            label14.Size = new Size(34, 17);
            label14.TabIndex = 6;
            label14.Text = "NRC";
            // 
            // textBoxNitReceptor
            // 
            textBoxNitReceptor.Location = new Point(6, 84);
            textBoxNitReceptor.Name = "textBoxNitReceptor";
            textBoxNitReceptor.Size = new Size(167, 25);
            textBoxNitReceptor.TabIndex = 5;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(5, 67);
            label15.Name = "label15";
            label15.Size = new Size(28, 17);
            label15.TabIndex = 4;
            label15.Text = "NIT";
            // 
            // textBoxDireccionReceptor
            // 
            textBoxDireccionReceptor.Location = new Point(6, 128);
            textBoxDireccionReceptor.Name = "textBoxDireccionReceptor";
            textBoxDireccionReceptor.Size = new Size(351, 25);
            textBoxDireccionReceptor.TabIndex = 11;
            textBoxDireccionReceptor.Text = "Chalatenango";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(5, 111);
            label16.Name = "label16";
            label16.Size = new Size(74, 17);
            label16.TabIndex = 10;
            label16.Text = "DIRECCION";
            // 
            // textBoxNombreReceptor
            // 
            textBoxNombreReceptor.Location = new Point(6, 39);
            textBoxNombreReceptor.Name = "textBoxNombreReceptor";
            textBoxNombreReceptor.Size = new Size(351, 25);
            textBoxNombreReceptor.TabIndex = 1;
            textBoxNombreReceptor.Text = "Cliente casual";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(5, 22);
            label17.Name = "label17";
            label17.Size = new Size(57, 17);
            label17.TabIndex = 0;
            label17.Text = "Nombre";
            // 
            // radioButtonCF
            // 
            radioButtonCF.AutoSize = true;
            radioButtonCF.Checked = true;
            radioButtonCF.Location = new Point(31, 189);
            radioButtonCF.Name = "radioButtonCF";
            radioButtonCF.Size = new Size(39, 19);
            radioButtonCF.TabIndex = 13;
            radioButtonCF.TabStop = true;
            radioButtonCF.Text = "CF";
            radioButtonCF.UseVisualStyleBackColor = true;
            radioButtonCF.CheckedChanged += radioButtonCF_CheckedChanged;
            // 
            // radioButtonCFF
            // 
            radioButtonCFF.AutoSize = true;
            radioButtonCFF.Location = new Point(76, 189);
            radioButtonCFF.Name = "radioButtonCFF";
            radioButtonCFF.Size = new Size(47, 19);
            radioButtonCFF.TabIndex = 13;
            radioButtonCFF.Text = "CCF";
            radioButtonCFF.UseVisualStyleBackColor = true;
            radioButtonCFF.CheckedChanged += radioButtonCFF_CheckedChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1268, 538);
            Controls.Add(radioButtonCFF);
            Controls.Add(radioButtonCF);
            Controls.Add(button1);
            Controls.Add(textBoxTotal);
            Controls.Add(label5);
            Controls.Add(textBoxIva);
            Controls.Add(label4);
            Controls.Add(textBoxRetencion);
            Controls.Add(label3);
            Controls.Add(textBoxSumas);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dataGridView1);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "Form1";
            Text = "Facturación";
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
        private DataGridView dataGridView1;
        private Label label1;
        private Label label2;
        private TextBox textBoxSumas;
        private Label label3;
        private TextBox textBoxRetencion;
        private Label label4;
        private TextBox textBoxIva;
        private Label label5;
        private TextBox textBoxTotal;
        private Button button1;
        private TextBox textBoxEmailEmisor;
        private Label label11;
        private TextBox textBoxTelefonoEmisor;
        private Label label10;
        private TextBox textBoxNRCEmisor;
        private Label label8;
        private TextBox textBoxNitEmisor;
        private Label label7;
        private TextBox textBoxDireccionEmisor;
        private Label label9;
        private TextBox textBoxNombreEmisor;
        private Label label6;
        private GroupBox groupBox2;
        private TextBox textBoxEmailReceptor;
        private Label label12;
        private TextBox textBox1TelReceptor;
        private Label label13;
        private TextBox textBoxNRCReceptor;
        private Label label14;
        private TextBox textBoxNitReceptor;
        private Label label15;
        private TextBox textBoxDireccionReceptor;
        private Label label16;
        private TextBox textBoxNombreReceptor;
        private Label label17;
        private DataGridViewTextBoxColumn cantidad;
        private DataGridViewTextBoxColumn descripcion;
        private DataGridViewTextBoxColumn precio;
        private DataGridViewTextBoxColumn total;
        private RadioButton radioButtonCF;
        private RadioButton radioButtonCFF;
    }
}
