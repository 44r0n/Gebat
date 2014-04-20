namespace GebatWin.Forms
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
            this.buttonAddFamiliar = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonAddConcessions = new System.Windows.Forms.Button();
            this.buttonChangeState = new System.Windows.Forms.Button();
            this.listaConcesiones1 = new GebatWindowComponents.Lists.ListaConcesiones();
            this.listaFamiliares = new GebatWindowComponents.Lists.ListaFamiliares();
            this.listaExpedientes = new GebatWindowComponents.Lists.ListaExpedientes();
            this.buttonDeleteFamiliar = new System.Windows.Forms.Button();
            this.buttonDelConcesion = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonAceptar);
            this.groupBox1.Controls.Add(this.textBoxObservaciones);
            this.groupBox1.Controls.Add(this.label2);
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
            // buttonAddConcessions
            // 
            this.buttonAddConcessions.Enabled = false;
            this.buttonAddConcessions.Location = new System.Drawing.Point(888, 414);
            this.buttonAddConcessions.Name = "buttonAddConcessions";
            this.buttonAddConcessions.Size = new System.Drawing.Size(111, 23);
            this.buttonAddConcessions.TabIndex = 6;
            this.buttonAddConcessions.Text = "Añadir Concesiones";
            this.buttonAddConcessions.UseVisualStyleBackColor = true;
            this.buttonAddConcessions.Click += new System.EventHandler(this.buttonAddConcessions_Click);
            // 
            // buttonChangeState
            // 
            this.buttonChangeState.Enabled = false;
            this.buttonChangeState.Location = new System.Drawing.Point(1005, 414);
            this.buttonChangeState.Name = "buttonChangeState";
            this.buttonChangeState.Size = new System.Drawing.Size(89, 23);
            this.buttonChangeState.TabIndex = 7;
            this.buttonChangeState.Text = "Cambiar Estado";
            this.buttonChangeState.UseVisualStyleBackColor = true;
            this.buttonChangeState.Click += new System.EventHandler(this.button1_Click);
            // 
            // listaConcesiones1
            // 
            this.listaConcesiones1.FullRowSelect = true;
            this.listaConcesiones1.GridLines = true;
            this.listaConcesiones1.Location = new System.Drawing.Point(888, 53);
            this.listaConcesiones1.MultiSelect = false;
            this.listaConcesiones1.Name = "listaConcesiones1";
            this.listaConcesiones1.Size = new System.Drawing.Size(406, 355);
            this.listaConcesiones1.TabIndex = 5;
            this.listaConcesiones1.UseCompatibleStateImageBehavior = false;
            this.listaConcesiones1.View = System.Windows.Forms.View.Details;
            this.listaConcesiones1.SelectedIndexChanged += new System.EventHandler(this.listaConcesiones1_SelectedIndexChanged);
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
            this.listaFamiliares.SelectedIndexChanged += new System.EventHandler(this.listaFamiliares_SelectedIndexChanged);
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
            // buttonDeleteFamiliar
            // 
            this.buttonDeleteFamiliar.Enabled = false;
            this.buttonDeleteFamiliar.Location = new System.Drawing.Point(673, 414);
            this.buttonDeleteFamiliar.Name = "buttonDeleteFamiliar";
            this.buttonDeleteFamiliar.Size = new System.Drawing.Size(89, 23);
            this.buttonDeleteFamiliar.TabIndex = 8;
            this.buttonDeleteFamiliar.Text = "Eliminar Familiar";
            this.buttonDeleteFamiliar.UseVisualStyleBackColor = true;
            this.buttonDeleteFamiliar.Click += new System.EventHandler(this.buttonDeleteFamiliar_Click);
            // 
            // buttonDelConcesion
            // 
            this.buttonDelConcesion.Enabled = false;
            this.buttonDelConcesion.Location = new System.Drawing.Point(1100, 414);
            this.buttonDelConcesion.Name = "buttonDelConcesion";
            this.buttonDelConcesion.Size = new System.Drawing.Size(104, 23);
            this.buttonDelConcesion.TabIndex = 9;
            this.buttonDelConcesion.Text = "Eliminar Concesión";
            this.buttonDelConcesion.UseVisualStyleBackColor = true;
            this.buttonDelConcesion.Click += new System.EventHandler(this.buttonDelConcesion_Click);
            // 
            // ExpedientePersonal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1305, 484);
            this.Controls.Add(this.buttonDelConcesion);
            this.Controls.Add(this.buttonDeleteFamiliar);
            this.Controls.Add(this.buttonChangeState);
            this.Controls.Add(this.buttonAddConcessions);
            this.Controls.Add(this.listaConcesiones1);
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonAceptar;
        private System.Windows.Forms.TextBox textBoxObservaciones;
        private System.Windows.Forms.Label label2;
        private GebatWindowComponents.Lists.ListaExpedientes listaExpedientes;
        private GebatWindowComponents.Lists.ListaFamiliares listaFamiliares;
        private System.Windows.Forms.Button buttonAddFamiliar;
        private System.Windows.Forms.Button buttonDelete;
        private GebatWindowComponents.Lists.ListaConcesiones listaConcesiones1;
        private System.Windows.Forms.Button buttonAddConcessions;
        private System.Windows.Forms.Button buttonChangeState;
        private System.Windows.Forms.Button buttonDeleteFamiliar;
        private System.Windows.Forms.Button buttonDelConcesion;
    }
}