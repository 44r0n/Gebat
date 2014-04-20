namespace GebatWin.Forms
{
    partial class Concesiones
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePickerBegin = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerFinish = new System.Windows.Forms.DateTimePicker();
            this.textBoxObservations = new System.Windows.Forms.TextBox();
            this.listaConcesiones1 = new GebatWindowComponents.Lists.ListaConcesiones();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBoxFinishDate = new System.Windows.Forms.CheckBox();
            this.checkBoxFega = new System.Windows.Forms.CheckBox();
            this.comboBoxState = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Fecha Inicio: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Fecha Fin: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Observaciones: ";
            // 
            // dateTimePickerBegin
            // 
            this.dateTimePickerBegin.Location = new System.Drawing.Point(102, 13);
            this.dateTimePickerBegin.Name = "dateTimePickerBegin";
            this.dateTimePickerBegin.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerBegin.TabIndex = 3;
            // 
            // dateTimePickerFinish
            // 
            this.dateTimePickerFinish.Location = new System.Drawing.Point(102, 39);
            this.dateTimePickerFinish.Name = "dateTimePickerFinish";
            this.dateTimePickerFinish.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerFinish.TabIndex = 4;
            // 
            // textBoxObservations
            // 
            this.textBoxObservations.Location = new System.Drawing.Point(102, 66);
            this.textBoxObservations.Multiline = true;
            this.textBoxObservations.Name = "textBoxObservations";
            this.textBoxObservations.Size = new System.Drawing.Size(200, 112);
            this.textBoxObservations.TabIndex = 5;
            // 
            // listaConcesiones1
            // 
            this.listaConcesiones1.FullRowSelect = true;
            this.listaConcesiones1.GridLines = true;
            this.listaConcesiones1.Location = new System.Drawing.Point(329, 13);
            this.listaConcesiones1.MultiSelect = false;
            this.listaConcesiones1.Name = "listaConcesiones1";
            this.listaConcesiones1.Size = new System.Drawing.Size(436, 349);
            this.listaConcesiones1.TabIndex = 6;
            this.listaConcesiones1.UseCompatibleStateImageBehavior = false;
            this.listaConcesiones1.View = System.Windows.Forms.View.Details;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(102, 224);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Aceptar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBoxFinishDate
            // 
            this.checkBoxFinishDate.AutoSize = true;
            this.checkBoxFinishDate.Checked = true;
            this.checkBoxFinishDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxFinishDate.Location = new System.Drawing.Point(308, 44);
            this.checkBoxFinishDate.Name = "checkBoxFinishDate";
            this.checkBoxFinishDate.Size = new System.Drawing.Size(15, 14);
            this.checkBoxFinishDate.TabIndex = 8;
            this.checkBoxFinishDate.UseVisualStyleBackColor = true;
            this.checkBoxFinishDate.CheckedChanged += new System.EventHandler(this.checkBoxFinishDate_CheckedChanged);
            // 
            // checkBoxFega
            // 
            this.checkBoxFega.AutoSize = true;
            this.checkBoxFega.Location = new System.Drawing.Point(229, 188);
            this.checkBoxFega.Name = "checkBoxFega";
            this.checkBoxFega.Size = new System.Drawing.Size(50, 17);
            this.checkBoxFega.TabIndex = 9;
            this.checkBoxFega.Text = "Fega";
            this.checkBoxFega.UseVisualStyleBackColor = true;
            this.checkBoxFega.CheckedChanged += new System.EventHandler(this.checkBoxFega_CheckedChanged);
            // 
            // comboBoxState
            // 
            this.comboBoxState.FormattingEnabled = true;
            this.comboBoxState.Items.AddRange(new object[] {
            "Pendiente",
            "Suspendida",
            "Aprovada"});
            this.comboBoxState.Location = new System.Drawing.Point(102, 184);
            this.comboBoxState.Name = "comboBoxState";
            this.comboBoxState.Size = new System.Drawing.Size(121, 21);
            this.comboBoxState.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 188);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Estado: ";
            // 
            // Concesiones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 375);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBoxState);
            this.Controls.Add(this.checkBoxFega);
            this.Controls.Add(this.checkBoxFinishDate);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listaConcesiones1);
            this.Controls.Add(this.textBoxObservations);
            this.Controls.Add(this.dateTimePickerFinish);
            this.Controls.Add(this.dateTimePickerBegin);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Concesiones";
            this.Text = "Concesiones";
            this.Load += new System.EventHandler(this.Fresco_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePickerBegin;
        private System.Windows.Forms.DateTimePicker dateTimePickerFinish;
        private System.Windows.Forms.TextBox textBoxObservations;
        private GebatWindowComponents.Lists.ListaConcesiones listaConcesiones1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBoxFinishDate;
        private System.Windows.Forms.CheckBox checkBoxFega;
        private System.Windows.Forms.ComboBox comboBoxState;
        private System.Windows.Forms.Label label4;
    }
}