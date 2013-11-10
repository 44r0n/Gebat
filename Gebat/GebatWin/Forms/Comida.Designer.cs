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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.listaFood = new GebatWindowComponents.Lists.ListaFood();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown)).BeginInit();
            this.groupBox2.SuspendLayout();
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
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.listaFood);
            this.groupBox2.Location = new System.Drawing.Point(415, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 342);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Comida total";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Location = new System.Drawing.Point(830, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 343);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Salida comida";
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
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Buscar: ";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(58, 17);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 2;
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
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
        private GebatWindowComponents.Lists.ListaFood listaFood;
    }
}