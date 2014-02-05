﻿namespace GebatWin.Forms
{
    partial class ExpedientePersonal
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
            this.buttonAceptar = new System.Windows.Forms.Button();
            this.textBoxObservaciones = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownIngresos = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonAddFamiliar = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.listaFamiliares = new GebatWindowComponents.Lists.ListaFamiliares();
            this.listaExpedientes = new GebatWindowComponents.Lists.ListaExpedientes();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIngresos)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonAceptar);
            this.groupBox1.Controls.Add(this.textBoxObservaciones);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numericUpDownIngresos);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(228, 263);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Nuevo Expediente";
            // 
            // buttonAceptar
            // 
            this.buttonAceptar.Location = new System.Drawing.Point(70, 225);
            this.buttonAceptar.Name = "buttonAceptar";
            this.buttonAceptar.Size = new System.Drawing.Size(75, 23);
            this.buttonAceptar.TabIndex = 4;
            this.buttonAceptar.Text = "Aceptar";
            this.buttonAceptar.UseVisualStyleBackColor = true;
            this.buttonAceptar.Click += new System.EventHandler(this.buttonAceptar_Click);
            // 
            // textBoxObservaciones
            // 
            this.textBoxObservaciones.Location = new System.Drawing.Point(99, 67);
            this.textBoxObservaciones.Multiline = true;
            this.textBoxObservaciones.Name = "textBoxObservaciones";
            this.textBoxObservaciones.Size = new System.Drawing.Size(120, 142);
            this.textBoxObservaciones.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Observaciones: ";
            // 
            // numericUpDownIngresos
            // 
            this.numericUpDownIngresos.Location = new System.Drawing.Point(99, 40);
            this.numericUpDownIngresos.Name = "numericUpDownIngresos";
            this.numericUpDownIngresos.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownIngresos.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ingresos: ";
            // 
            // buttonAddFamiliar
            // 
            this.buttonAddFamiliar.Enabled = false;
            this.buttonAddFamiliar.Location = new System.Drawing.Point(571, 414);
            this.buttonAddFamiliar.Name = "buttonAddFamiliar";
            this.buttonAddFamiliar.Size = new System.Drawing.Size(96, 23);
            this.buttonAddFamiliar.TabIndex = 3;
            this.buttonAddFamiliar.Text = "Añadir Familiares";
            this.buttonAddFamiliar.UseVisualStyleBackColor = true;
            this.buttonAddFamiliar.Click += new System.EventHandler(this.buttonAddFamiliar_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Enabled = false;
            this.buttonDelete.Location = new System.Drawing.Point(247, 414);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(75, 23);
            this.buttonDelete.TabIndex = 4;
            this.buttonDelete.Text = "Eliminar";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.button2_Click);
            // 
            // listaFamiliares
            // 
            this.listaFamiliares.FullRowSelect = true;
            this.listaFamiliares.GridLines = true;
            this.listaFamiliares.Location = new System.Drawing.Point(571, 53);
            this.listaFamiliares.MultiSelect = false;
            this.listaFamiliares.Name = "listaFamiliares";
            this.listaFamiliares.Size = new System.Drawing.Size(310, 355);
            this.listaFamiliares.TabIndex = 2;
            this.listaFamiliares.UseCompatibleStateImageBehavior = false;
            this.listaFamiliares.View = System.Windows.Forms.View.Details;
            // 
            // listaExpedientes
            // 
            this.listaExpedientes.FullRowSelect = true;
            this.listaExpedientes.GridLines = true;
            this.listaExpedientes.Location = new System.Drawing.Point(247, 53);
            this.listaExpedientes.MultiSelect = false;
            this.listaExpedientes.Name = "listaExpedientes";
            this.listaExpedientes.Size = new System.Drawing.Size(317, 355);
            this.listaExpedientes.TabIndex = 1;
            this.listaExpedientes.UseCompatibleStateImageBehavior = false;
            this.listaExpedientes.View = System.Windows.Forms.View.Details;
            this.listaExpedientes.SelectedIndexChanged += new System.EventHandler(this.listaExpedientes_SelectedIndexChanged);
            // 
            // ExpedientePersonal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(918, 499);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonAddFamiliar);
            this.Controls.Add(this.listaFamiliares);
            this.Controls.Add(this.listaExpedientes);
            this.Controls.Add(this.groupBox1);
            this.Name = "ExpedientePersonal";
            this.Text = "Expediente Personal";
            this.Load += new System.EventHandler(this.ExpedientePersonal_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIngresos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonAceptar;
        private System.Windows.Forms.TextBox textBoxObservaciones;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownIngresos;
        private System.Windows.Forms.Label label1;
        private GebatWindowComponents.Lists.ListaExpedientes listaExpedientes;
        private GebatWindowComponents.Lists.ListaFamiliares listaFamiliares;
        private System.Windows.Forms.Button buttonAddFamiliar;
        private System.Windows.Forms.Button buttonDelete;
    }
}