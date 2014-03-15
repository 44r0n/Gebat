namespace GebatWin
{
    partial class Principal
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cantidadesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gestionarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comidaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.delitosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tBCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expedientesPersonalesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.concesionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frescoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.editarToolStripMenuItem,
            this.gestionarToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(565, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.salirToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // editarToolStripMenuItem
            // 
            this.editarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cantidadesToolStripMenuItem});
            this.editarToolStripMenuItem.Name = "editarToolStripMenuItem";
            this.editarToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.editarToolStripMenuItem.Text = "Editar";
            // 
            // cantidadesToolStripMenuItem
            // 
            this.cantidadesToolStripMenuItem.Name = "cantidadesToolStripMenuItem";
            this.cantidadesToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.cantidadesToolStripMenuItem.Text = "Cantidades";
            this.cantidadesToolStripMenuItem.Click += new System.EventHandler(this.cantidadesToolStripMenuItem_Click);
            // 
            // gestionarToolStripMenuItem
            // 
            this.gestionarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.comidaToolStripMenuItem,
            this.delitosToolStripMenuItem,
            this.tBCToolStripMenuItem,
            this.expedientesPersonalesToolStripMenuItem,
            this.concesionesToolStripMenuItem});
            this.gestionarToolStripMenuItem.Name = "gestionarToolStripMenuItem";
            this.gestionarToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.gestionarToolStripMenuItem.Text = "Gestionar";
            // 
            // comidaToolStripMenuItem
            // 
            this.comidaToolStripMenuItem.Name = "comidaToolStripMenuItem";
            this.comidaToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.comidaToolStripMenuItem.Text = "Comida";
            this.comidaToolStripMenuItem.Click += new System.EventHandler(this.comidaToolStripMenuItem_Click);
            // 
            // delitosToolStripMenuItem
            // 
            this.delitosToolStripMenuItem.Name = "delitosToolStripMenuItem";
            this.delitosToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.delitosToolStripMenuItem.Text = "Delitos";
            this.delitosToolStripMenuItem.Click += new System.EventHandler(this.delitosToolStripMenuItem_Click);
            // 
            // tBCToolStripMenuItem
            // 
            this.tBCToolStripMenuItem.Name = "tBCToolStripMenuItem";
            this.tBCToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.tBCToolStripMenuItem.Text = "TBC";
            this.tBCToolStripMenuItem.Click += new System.EventHandler(this.tBCToolStripMenuItem_Click);
            // 
            // expedientesPersonalesToolStripMenuItem
            // 
            this.expedientesPersonalesToolStripMenuItem.Name = "expedientesPersonalesToolStripMenuItem";
            this.expedientesPersonalesToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.expedientesPersonalesToolStripMenuItem.Text = "Expedientes Personales";
            this.expedientesPersonalesToolStripMenuItem.Click += new System.EventHandler(this.expedientesPersonalesToolStripMenuItem_Click);
            // 
            // concesionesToolStripMenuItem
            // 
            this.concesionesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.frescoToolStripMenuItem});
            this.concesionesToolStripMenuItem.Name = "concesionesToolStripMenuItem";
            this.concesionesToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.concesionesToolStripMenuItem.Text = "Concesiones";
            // 
            // frescoToolStripMenuItem
            // 
            this.frescoToolStripMenuItem.Name = "frescoToolStripMenuItem";
            this.frescoToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.frescoToolStripMenuItem.Text = "Fresco";
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 275);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Principal";
            this.Text = "Gebat-Principal";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Principal_FormClosed);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cantidadesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gestionarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem comidaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tBCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem delitosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem expedientesPersonalesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem concesionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem frescoToolStripMenuItem;
    }
}