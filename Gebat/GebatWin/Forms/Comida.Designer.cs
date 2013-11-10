namespace GebatWin.Forms
{
    partial class Comida
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listaFoodIN = new GebatWindowComponents.Lists.ListaFoodIN();
            this.buttonInsert = new System.Windows.Forms.Button();
            this.comboFood = new GebatWindowComponents.Combos.ComboFood();
            this.comboType = new GebatWindowComponents.Combos.ComboType();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.radioButtonExistente = new System.Windows.Forms.RadioButton();
            this.textBoxNuevo = new System.Windows.Forms.TextBox();
            this.radioButtonNuevo = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.listaFood = new GebatWindowComponents.Lists.ListaFood();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonSalida = new System.Windows.Forms.Button();
            this.listaFoodOut = new GebatWindowComponents.Lists.ListaFoodOut();
            this.dateTimePickerSalida = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDownSalida = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.comboFoodSalida = new GebatWindowComponents.Combos.ComboFood();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSalida)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.listaFoodIN);
            this.groupBox1.Controls.Add(this.buttonInsert);
            this.groupBox1.Controls.Add(this.comboFood);
            this.groupBox1.Controls.Add(this.comboType);
            this.groupBox1.Controls.Add(this.dateTimePicker);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.numericUpDown);
            this.groupBox1.Controls.Add(this.radioButtonExistente);
            this.groupBox1.Controls.Add(this.textBoxNuevo);
            this.groupBox1.Controls.Add(this.radioButtonNuevo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(305, 343);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Entrada comida";
            // 
            // listaFoodIN
            // 
            this.listaFoodIN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listaFoodIN.FullRowSelect = true;
            this.listaFoodIN.GridLines = true;
            this.listaFoodIN.Location = new System.Drawing.Point(9, 208);
            this.listaFoodIN.MultiSelect = false;
            this.listaFoodIN.Name = "listaFoodIN";
            this.listaFoodIN.Size = new System.Drawing.Size(283, 129);
            this.listaFoodIN.TabIndex = 14;
            this.listaFoodIN.UseCompatibleStateImageBehavior = false;
            this.listaFoodIN.View = System.Windows.Forms.View.Details;
            // 
            // buttonInsert
            // 
            this.buttonInsert.Location = new System.Drawing.Point(64, 179);
            this.buttonInsert.Name = "buttonInsert";
            this.buttonInsert.Size = new System.Drawing.Size(75, 23);
            this.buttonInsert.TabIndex = 13;
            this.buttonInsert.Text = "Aceptar";
            this.buttonInsert.UseVisualStyleBackColor = true;
            this.buttonInsert.Click += new System.EventHandler(this.buttonInsert_Click);
            // 
            // comboFood
            // 
            this.comboFood.FormattingEnabled = true;
            this.comboFood.Location = new System.Drawing.Point(171, 70);
            this.comboFood.Name = "comboFood";
            this.comboFood.Size = new System.Drawing.Size(121, 21);
            this.comboFood.TabIndex = 12;
            // 
            // comboType
            // 
            this.comboType.FormattingEnabled = true;
            this.comboType.Location = new System.Drawing.Point(64, 124);
            this.comboType.Name = "comboType";
            this.comboType.Size = new System.Drawing.Size(121, 21);
            this.comboType.TabIndex = 11;
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Location = new System.Drawing.Point(64, 152);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Tipo: ";
            // 
            // numericUpDown
            // 
            this.numericUpDown.Location = new System.Drawing.Point(64, 97);
            this.numericUpDown.Name = "numericUpDown";
            this.numericUpDown.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown.TabIndex = 7;
            // 
            // radioButtonExistente
            // 
            this.radioButtonExistente.AutoSize = true;
            this.radioButtonExistente.Location = new System.Drawing.Point(171, 46);
            this.radioButtonExistente.Name = "radioButtonExistente";
            this.radioButtonExistente.Size = new System.Drawing.Size(68, 17);
            this.radioButtonExistente.TabIndex = 6;
            this.radioButtonExistente.TabStop = true;
            this.radioButtonExistente.Text = "Existente";
            this.radioButtonExistente.UseVisualStyleBackColor = true;
            this.radioButtonExistente.CheckedChanged += new System.EventHandler(this.radioButtonExistente_CheckedChanged);
            // 
            // textBoxNuevo
            // 
            this.textBoxNuevo.Location = new System.Drawing.Point(64, 70);
            this.textBoxNuevo.Name = "textBoxNuevo";
            this.textBoxNuevo.Size = new System.Drawing.Size(100, 20);
            this.textBoxNuevo.TabIndex = 4;
            // 
            // radioButtonNuevo
            // 
            this.radioButtonNuevo.AutoSize = true;
            this.radioButtonNuevo.Location = new System.Drawing.Point(64, 46);
            this.radioButtonNuevo.Name = "radioButtonNuevo";
            this.radioButtonNuevo.Size = new System.Drawing.Size(57, 17);
            this.radioButtonNuevo.TabIndex = 3;
            this.radioButtonNuevo.TabStop = true;
            this.radioButtonNuevo.Text = "Nuevo";
            this.radioButtonNuevo.UseVisualStyleBackColor = true;
            this.radioButtonNuevo.CheckedChanged += new System.EventHandler(this.radioButtonNuevo_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Fecha: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Cantidad: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre: ";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.textBoxSearch);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.listaFood);
            this.groupBox2.Location = new System.Drawing.Point(415, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 342);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Comida total";
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Location = new System.Drawing.Point(58, 17);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(100, 20);
            this.textBoxSearch.TabIndex = 2;
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Buscar: ";
            // 
            // listaFood
            // 
            this.listaFood.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listaFood.FullRowSelect = true;
            this.listaFood.GridLines = true;
            this.listaFood.Location = new System.Drawing.Point(6, 43);
            this.listaFood.MultiSelect = false;
            this.listaFood.Name = "listaFood";
            this.listaFood.Size = new System.Drawing.Size(188, 293);
            this.listaFood.TabIndex = 0;
            this.listaFood.UseCompatibleStateImageBehavior = false;
            this.listaFood.View = System.Windows.Forms.View.Details;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.buttonSalida);
            this.groupBox3.Controls.Add(this.listaFoodOut);
            this.groupBox3.Controls.Add(this.dateTimePickerSalida);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.numericUpDownSalida);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.comboFoodSalida);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(736, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(294, 343);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Salida comida";
            // 
            // buttonSalida
            // 
            this.buttonSalida.Location = new System.Drawing.Point(67, 119);
            this.buttonSalida.Name = "buttonSalida";
            this.buttonSalida.Size = new System.Drawing.Size(75, 23);
            this.buttonSalida.TabIndex = 7;
            this.buttonSalida.Text = "Aceptar";
            this.buttonSalida.UseVisualStyleBackColor = true;
            this.buttonSalida.Click += new System.EventHandler(this.buttonSalida_Click);
            // 
            // listaFoodOut
            // 
            this.listaFoodOut.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listaFoodOut.FullRowSelect = true;
            this.listaFoodOut.GridLines = true;
            this.listaFoodOut.Location = new System.Drawing.Point(6, 148);
            this.listaFoodOut.MultiSelect = false;
            this.listaFoodOut.Name = "listaFoodOut";
            this.listaFoodOut.Size = new System.Drawing.Size(282, 187);
            this.listaFoodOut.TabIndex = 6;
            this.listaFoodOut.UseCompatibleStateImageBehavior = false;
            this.listaFoodOut.View = System.Windows.Forms.View.Details;
            // 
            // dateTimePickerSalida
            // 
            this.dateTimePickerSalida.Location = new System.Drawing.Point(67, 93);
            this.dateTimePickerSalida.Name = "dateTimePickerSalida";
            this.dateTimePickerSalida.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerSalida.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 99);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Fecha: ";
            // 
            // numericUpDownSalida
            // 
            this.numericUpDownSalida.Location = new System.Drawing.Point(67, 67);
            this.numericUpDownSalida.Name = "numericUpDownSalida";
            this.numericUpDownSalida.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownSalida.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 69);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Cantidad: ";
            // 
            // comboFoodSalida
            // 
            this.comboFoodSalida.FormattingEnabled = true;
            this.comboFoodSalida.Location = new System.Drawing.Point(67, 40);
            this.comboFoodSalida.Name = "comboFoodSalida";
            this.comboFoodSalida.Size = new System.Drawing.Size(121, 21);
            this.comboFoodSalida.TabIndex = 1;
            this.comboFoodSalida.SelectedIndexChanged += new System.EventHandler(this.comboFoodSalida_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Nombre: ";
            // 
            // Comida
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1042, 367);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Comida";
            this.Text = "Gebat - Comida";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Comida_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSalida)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDown;
        private System.Windows.Forms.RadioButton radioButtonExistente;
        private System.Windows.Forms.TextBox textBoxNuevo;
        private System.Windows.Forms.RadioButton radioButtonNuevo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private GebatWindowComponents.Combos.ComboType comboType;
        private GebatWindowComponents.Combos.ComboFood comboFood;
        private GebatWindowComponents.Lists.ListaFoodIN listaFoodIN;
        private System.Windows.Forms.Button buttonInsert;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Label label5;
        private GebatWindowComponents.Lists.ListaFood listaFood;
        private GebatWindowComponents.Lists.ListaFoodOut listaFoodOut;
        private System.Windows.Forms.DateTimePicker dateTimePickerSalida;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericUpDownSalida;
        private System.Windows.Forms.Label label7;
        private GebatWindowComponents.Combos.ComboFood comboFoodSalida;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonSalida;
    }
}