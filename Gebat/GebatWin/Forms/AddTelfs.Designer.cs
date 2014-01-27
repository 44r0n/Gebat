namespace GebatWin.Forms
{
    partial class AddTelfs
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
            this.textBoxTelf = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonAddTelf = new System.Windows.Forms.Button();
            this.listViewTelfs = new System.Windows.Forms.ListView();
            this.buttonDelTelf = new System.Windows.Forms.Button();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxTelf);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.buttonAddTelf);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(224, 153);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Añadir nuevo teléfono";
            // 
            // textBoxTelf
            // 
            this.textBoxTelf.Location = new System.Drawing.Point(117, 57);
            this.textBoxTelf.MaxLength = 9;
            this.textBoxTelf.Name = "textBoxTelf";
            this.textBoxTelf.Size = new System.Drawing.Size(100, 20);
            this.textBoxTelf.TabIndex = 2;
            this.textBoxTelf.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Número de telefono:";
            // 
            // buttonAddTelf
            // 
            this.buttonAddTelf.Location = new System.Drawing.Point(77, 112);
            this.buttonAddTelf.Name = "buttonAddTelf";
            this.buttonAddTelf.Size = new System.Drawing.Size(75, 23);
            this.buttonAddTelf.TabIndex = 0;
            this.buttonAddTelf.Text = "Añadir";
            this.buttonAddTelf.UseVisualStyleBackColor = true;
            this.buttonAddTelf.Click += new System.EventHandler(this.buttonAddTelf_Click);
            // 
            // listViewTelfs
            // 
            this.listViewTelfs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listViewTelfs.FullRowSelect = true;
            this.listViewTelfs.GridLines = true;
            this.listViewTelfs.Location = new System.Drawing.Point(242, 12);
            this.listViewTelfs.MultiSelect = false;
            this.listViewTelfs.Name = "listViewTelfs";
            this.listViewTelfs.Scrollable = false;
            this.listViewTelfs.Size = new System.Drawing.Size(280, 116);
            this.listViewTelfs.TabIndex = 1;
            this.listViewTelfs.UseCompatibleStateImageBehavior = false;
            this.listViewTelfs.View = System.Windows.Forms.View.Details;
            this.listViewTelfs.SelectedIndexChanged += new System.EventHandler(this.listViewTelfs_SelectedIndexChanged);
            // 
            // buttonDelTelf
            // 
            this.buttonDelTelf.Enabled = false;
            this.buttonDelTelf.Location = new System.Drawing.Point(344, 142);
            this.buttonDelTelf.Name = "buttonDelTelf";
            this.buttonDelTelf.Size = new System.Drawing.Size(75, 23);
            this.buttonDelTelf.TabIndex = 2;
            this.buttonDelTelf.Text = "Eliminar";
            this.buttonDelTelf.UseVisualStyleBackColor = true;
            this.buttonDelTelf.Click += new System.EventHandler(this.buttonDelTelf_Click);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 0;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Número";
            this.columnHeader2.Width = 276;
            // 
            // AddTelfs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 183);
            this.Controls.Add(this.buttonDelTelf);
            this.Controls.Add(this.listViewTelfs);
            this.Controls.Add(this.groupBox1);
            this.Name = "AddTelfs";
            this.Load += new System.EventHandler(this.AddTelfs_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxTelf;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonAddTelf;
        private System.Windows.Forms.ListView listViewTelfs;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button buttonDelTelf;
    }
}